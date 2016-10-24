Imports Utilitarios.Utils
Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes

Public Class Consulta
    Inherits System.Web.UI.Page

    Private Property ListaImovel As List(Of Imovel)
        Get
            If Session.Item("ListaImovel") Is Nothing Then
                Return Nothing
            End If
            Return CType(Session.Item("ListaImovel"), List(Of Imovel))
        End Get
        Set(ByVal value As List(Of Imovel))
            Session.Item("ListaImovel") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                'Limpa Sessions
                ListaImovel = Nothing

                'Carrega os dropdownlist necessários
                CarregaDDls()
                '====================================

                'Carrega as cidades e bairros
                CarregaCidades(ddlEstado.SelectedValue.ToInt)
                CarregaBairros(ddlCidade.SelectedValue.ToInt)
                '============================            

                'Obtem requests
                If Not String.IsNullOrEmpty(Request.Params("referencia")) Then
                    txtReferencia.Text = Request.Params("referencia").ToString.Trim
                    btnConsultar_Click(Nothing, Nothing)
                ElseIf Not String.IsNullOrEmpty(Request.Params("lancamento")) Then
                    CarregaLancamentos()
                Else
                    'Verifica outros parametros, TipoImovel:
                    If Not String.IsNullOrEmpty(Request.Params("tipoimovel")) Then
                        ddlTipoImovel.SelectedValue = Request.Params("tipoimovel").ToString

                        'Verifica estado
                        If Not String.IsNullOrEmpty(Request.Params("estadoId")) Then
                            ddlEstado.SelectedValue = Request.Params("estadoId").ToString
                        End If
                        CarregaCidades(ddlEstado.SelectedValue.ToInt)

                        'Verifica cidade
                        If Not String.IsNullOrEmpty(Request.Params("cidadeId")) Then
                            ddlCidade.SelectedValue = Request.Params("cidadeId").ToString
                        End If

                        'Verifica valores de/até
                        If Not String.IsNullOrEmpty(Request.Params("valorDe")) Then
                            txtValorDe.Text = FormataValor(Request.Params("valorDe"))
                        End If
                        If Not String.IsNullOrEmpty(Request.Params("valorAte")) Then
                            txtValorAte.Text = FormataValor(Request.Params("valorAte"))
                        End If

                        btnConsultar_Click(Nothing, Nothing)
                    End If
                End If

            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        CarregaCidades(ddlEstado.SelectedValue.ToInt)
    End Sub

    Private Sub ddlCidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCidade.SelectedIndexChanged
        Try
            CarregaBairros(ddlCidade.SelectedValue.ToInt)
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub CarregaDDls()
        'Carrega ddl de estado
        If ddlEstado.Items.Count.Equals(0) Then
            ddlEstado.DataSource = Estado.Busca
            ddlEstado.DataValueField = "Id"
            ddlEstado.DataTextField = "Descricao"
            ddlEstado.DataBind()
        End If
        '=======================

        'Carrega ddl TipoImovel
        If ddlTipoImovel.Items.Count.Equals(0) Then
            ddlTipoImovel.DataSource = TipoImovel.Busca
            ddlTipoImovel.DataValueField = "Id"
            ddlTipoImovel.DataTextField = "Descricao"
            ddlTipoImovel.DataBind()
            ddlTipoImovel.Items.Insert(0, New ListItem("Todos", "0"))
        End If
        '========================

        'Carrega ddl TipoMaterial
        If ddlTipoMaterial.Items.Count.Equals(0) Then
            ddlTipoMaterial.DataSource = TipoMaterial.Busca
            ddlTipoMaterial.DataValueField = "Id"
            ddlTipoMaterial.DataTextField = "Descricao"
            ddlTipoMaterial.DataBind()
            ddlTipoMaterial.Items.Insert(0, New ListItem("Todos", "0"))
        End If
        '========================

    End Sub

    Private Sub CarregaCidades(ByVal pEstadoId As Integer)
        ddlCidade.DataSource = Cidade.Busca(pEstadoId)
        ddlCidade.DataValueField = "Id"
        ddlCidade.DataTextField = "Descricao"
        ddlCidade.DataBind()
        ddlCidade.Items.Insert(0, New ListItem("Todos", "0"))
    End Sub

    Private Sub CarregaBairros(ByVal pCidadeId As Integer)
        Dim listaBairros As List(Of Bairro) = Bairro.Busca(pCidadeId)
        ddlBairro.DataSource = listaBairros
        ddlBairro.DataValueField = "Id"
        ddlBairro.DataTextField = "Descricao"
        ddlBairro.DataBind()
        ddlBairro.Items.Insert(0, New ListItem("Todos", "0"))

        cblBairros.DataSource = listaBairros
        cblBairros.DataValueField = "Id"
        cblBairros.DataTextField = "Descricao"
        cblBairros.DataBind()
        cblBairros.Items.Insert(0, New ListItem("Todos", "0"))
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Try
            If Not divBuscaAvancada.Visible Then
                'Busca básica
                ListaImovel = Imovel.Busca(txtReferencia.Text, ddlEstado.SelectedValue.ToInt,
                                           ddlCidade.SelectedValue.ToInt, ddlBairro.SelectedValue.ToInt,
                                           ddlTipoImovel.SelectedValue.ToInt, ddlTipoMaterial.SelectedValue.ToInt,
                                           txtValorDe.Text.ToInt, txtValorAte.Text.ToInt)
            Else
                'Obtem lista de bairros
                Dim listaBairros As New List(Of Bairro)
                For indBairro As Integer = 0 To cblBairros.Items.Count - 1
                    If cblBairros.Items(indBairro).Selected Then
                        'Recupera o valor através do CheckBoxList1.Items[i].Value;
                        listaBairros.Add(New Bairro(cblBairros.Items(indBairro).Value.ToInt))
                    End If
                Next
                '=======================

                'Obtem lista de itens
                Dim listaItens As New List(Of ImovelItens)
                For indItem As Integer = 0 To cblItensImovel.Items.Count - 1
                    If cblItensImovel.Items(indItem).Selected Then
                        'Recupera o valor através do CheckBoxList1.Items[i].Value;
                        listaItens.Add(New ImovelItens(cblItensImovel.Items(indItem).Value))
                    End If
                Next
                '===================

                'Busca avançada..
                ListaImovel = Imovel.Busca(ddlEstado.SelectedValue.ToInt, ddlCidade.SelectedValue.ToInt,
                                           ddlTipoImovel.SelectedValue.ToInt, ddlTipoMaterial.SelectedValue.ToInt,
                                           txtValorDe.Text.ToInt, txtValorAte.Text.ToInt,
                                           txtAreaConstruideDe.Text.ToInt, txtAreaConstruideAte.Text.ToInt,
                                           txtQtdeDomitorio.Text.ToInt, txtQtdeSuite.Text.ToInt,
                                           listaBairros, listaItens)
                '================
            End If
            

            If ListaImovel.Count > 0 Then
                dtgImovel.Visible = True
                dtgImovel.DataSource = ListaImovel
                dtgImovel.DataBind()
            Else
                dtgImovel.Visible = False
            End If

            divTotalRegistro.Visible = True
            Select Case ListaImovel.Count
                Case 0
                    lblTotalRegistro.Text = "Nenhum imóvel encontrado nesta pesquisa."
                    Master.ExibirAlerta(lblTotalRegistro.Text)
                Case 1
                    lblTotalRegistro.Text = "Foi encontrado 1 imóvel."
                Case Else
                    lblTotalRegistro.Text = "Foram encontrados {0} imóveis nesta pesquisa.".Fill(ListaImovel.Count)
            End Select


        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub CarregaLancamentos()
        Try
            ListaImovel = Imovel.BuscaLancamentos()

            If ListaImovel.Count > 0 Then
                dtgImovel.Visible = True
                dtgImovel.DataSource = ListaImovel
                dtgImovel.DataBind()
            Else
                dtgImovel.Visible = False
            End If

            divTotalRegistro.Visible = True
            Select Case ListaImovel.Count
                Case 0
                    lblTotalRegistro.Text = "Nenhum imóvel encontrado nesta pesquisa."
                    Master.ExibirAlerta(lblTotalRegistro.Text)
                Case 1
                    lblTotalRegistro.Text = "Foi encontrado 1 imóvel."
                Case Else
                    lblTotalRegistro.Text = "Foram encontrados {0} imóveis nesta pesquisa.".Fill(ListaImovel.Count)
            End Select

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub dtgImovel_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dtgImovel.PageIndexChanging
        Try
            dtgImovel.PageIndex = e.NewPageIndex
            dtgImovel.DataSource = ListaImovel
            dtgImovel.DataBind()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnAbrirBuscaAvancada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbrirBuscaAvancada.Click
        Try
            pnlBairro.Visible = False
            btnAbrirBuscaAvancada.Visible = False
            divBuscaAvancada.Visible = True

            cblItensImovel.DataSource = ImovelItens.CarregaLista
            cblItensImovel.DataValueField = "NomeCampo"
            cblItensImovel.DataTextField = "Descricao"
            cblItensImovel.DataBind()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnAbrirBuscaBasica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbrirBuscaBasica.Click
        Try
            pnlBairro.Visible = True
            btnAbrirBuscaAvancada.Visible = True
            divBuscaAvancada.Visible = False
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub
End Class