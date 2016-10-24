Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes

Public Class MeusImoveis
    Inherits System.Web.UI.Page

    Private Property ListaImovel As List(Of Imovel)
        Get
            If Session.Item("MinhaListaImovel") Is Nothing Then
                Return Nothing
            End If
            Return CType(Session.Item("MinhaListaImovel"), List(Of Imovel))
        End Get
        Set(ByVal value As List(Of Imovel))
            Session.Item("MinhaListaImovel") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                'Limpa Sessions
                ListaImovel = Nothing

                Consultar()

            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub Consultar()

        If Not Request.Cookies("meusimoveis") Is Nothing AndAlso Not String.IsNullOrEmpty(Request.Cookies("meusimoveis").Value) Then

            Dim lista As String = Request.Cookies("meusimoveis").Value
            lista = lista.Substring(0, lista.Length - 1)

            ListaImovel = Imovel.Busca(lista.Split(CChar("-")))

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
                    lblTotalRegistro.Text = "Você não marcou imóveis. Maque seus imóveis favoritos em 'Marcar este imóvel'."

                Case 1
                    lblTotalRegistro.Text = "Foi encontrado 1 imóvel."
                Case Else
                    lblTotalRegistro.Text = "Foram encontrados {0} imóveis marcados por você.".Fill(ListaImovel.Count)
            End Select
        Else
            lblTotalRegistro.Text = "Você não marcou imóveis. Maque seus imóveis favoritos em 'Marcar este imóvel'."
        End If

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