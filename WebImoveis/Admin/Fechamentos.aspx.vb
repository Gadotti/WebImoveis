Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes
Imports System.Globalization

Public Class Fechamentos
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

                txtDataFechamentoDe.Text = "01/" & Now.Month & "/" & Now.Year
                txtDataFechamentoAte.Text = Now.AddMonths(1).AddDays(Now.Day * -1).Day & "/" & Now.Month & "/" & Now.Year
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Try
            'Regional settings para datas
            Dim ciNewFormat As New CultureInfo(CultureInfo.CurrentCulture.ToString())
            ciNewFormat.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture = ciNewFormat
            '============================

            'Valida campos
            If Not IsDate(txtDataFechamentoDe.Text) Then
                Master.ExibirAlerta("Formato de data inválido. Formato correto: dd/MM/yyyy.")
                txtDataFechamentoDe.Focus()
                Return
            ElseIf Not IsDate(txtDataFechamentoAte.Text) Then
                Master.ExibirAlerta("Formato de data inválido. Formato correto: dd/MM/yyyy.")
                txtDataFechamentoAte.Focus()
                Return
            End If

            ListaImovel = Imovel.BuscaFechamento(CDate(txtDataFechamentoDe.Text), CDate(txtDataFechamentoAte.Text))

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

    Private Sub EditaImovel(ByVal pImovelId As Integer)
        Response.Redirect(String.Format("~/Admin/CadastroImovel.aspx?imovelid={0}", pImovelId), False)
    End Sub

    Private Sub dtgImovel_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dtgImovel.RowCommand
        Try
            If e.CommandName.Equals("editar") Then
                EditaImovel(CInt(e.CommandArgument.ToString))
            End If
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
End Class