Public Class Sobre
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim ano As String = (Now.Year - 1986).ToString
            lblAno.Text = ano
            lblAno2.Text = ano
        End If
    End Sub

End Class