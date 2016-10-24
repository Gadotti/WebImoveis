Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes
Imports System.IO

Public Class EdicaoImovel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Carrega dos dropdownlist necessários
                CarregaDDls()
                '====================================

                'Carrega as cidades e bairros
                CarregaCidades(ddlEstado.SelectedValue.ToInt)
                CarregaBairros(ddlCidade.SelectedValue.ToInt)
                '============================                
            End If

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Try
            CarregaCidades(ddlEstado.SelectedValue.ToInt)
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
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
        ddlBairro.DataSource = Bairro.Busca(pCidadeId)
        ddlBairro.DataValueField = "Id"
        ddlBairro.DataTextField = "Descricao"
        ddlBairro.DataBind()
        ddlBairro.Items.Insert(0, New ListItem("Todos", "0"))
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Try
            dtgImovel.DataSource = Imovel.Busca(txtReferencia.Text, ddlEstado.SelectedValue.ToInt,
                                                ddlCidade.SelectedValue.ToInt, ddlBairro.SelectedValue.ToInt,
                                                ddlTipoImovel.SelectedValue.ToInt, ddlTipoMaterial.SelectedValue.ToInt,
                                                txtValor.Text.ToInt, txtValor.Text.ToInt, False)
            dtgImovel.DataBind()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub EditaImovel(ByVal pImovelId As Integer)
        Response.Redirect(String.Format("~/Admin/CadastroImovel.aspx?imovelid={0}", pImovelId), False)
    End Sub

    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditar.Click
        Try
            Dim imovelId As Integer = txtImovelId.Text.ToInt
            If imovelId > 0 Then
                'Verifica se existe o imóvel
                If Imovel.Existe(imovelId) Then
                    EditaImovel(imovelId)
                Else
                    Master.ExibirAlerta("Nenhuma imóvel encontrado com o código: " & imovelId.ToString)
                End If
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub dtgImovel_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dtgImovel.RowCommand
        Try
            Dim imovelId As Integer = e.CommandArgument.ToString.ToInt

            If e.CommandName.Equals("editar") Then
                EditaImovel(imovelId)
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub


End Class