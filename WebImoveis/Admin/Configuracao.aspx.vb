Imports DataAccess.DataAccess

Public Class Configuracao
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                CarregaParametros()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub CarregaParametros()
        Dim objParametros As Parametros = Parametros.Busca
        txtEmail.Text = objParametros.Email
        txtEmailSenha.Text = objParametros.EmailSenha
        txtSmtpHost.Text = objParametros.SmtpHost
        txtSmtpPorta.Text = objParametros.SmtpPorta.ToString
        txtTitulo.Text = objParametros.Titulo
    End Sub

    Private Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try
            Dim objParametros As Parametros = Parametros.Busca
            With objParametros
                .Email = txtEmail.Text
                .EmailSenha = txtEmailSenha.Text
                .SmtpHost = txtSmtpHost.Text
                .SmtpPorta = Convert.ToInt32(txtSmtpPorta.Text)
                .Titulo = txtTitulo.Text
                .Contato = objParametros.Contato
            End With

            objParametros.Salvar()

            Master.ExibirAlerta("Operação realizada com sucesso.")
            
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub
End Class