Imports Utilitarios
Imports DataAccess.DataAccess

Public Class Contato
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Dim objParametro As Parametros = Parametros.Busca
                ltrContato.Text = objParametro.Contato
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub LimparCampos()
        txtNome.Text = String.Empty
        txtEmail.Text = String.Empty
        txtContato.Text = String.Empty
        txtAssunto.Text = String.Empty
        txtMensagem.Text = String.Empty
    End Sub

    Private Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            'Realiza o envio do e-mail
            Dim objParametro As Parametros = Parametros.Busca
            Utils.Mail(txtEmail.Text, "[Site] - " & txtAssunto.Text, txtMensagem.Text & Environment.NewLine & "Contato: " & txtContato.Text,
                       objParametro.Email, objParametro.EmailSenha, objParametro.SmtpHost, objParametro.SmtpPorta)
            LimparCampos()
            Master.ExibirAlerta("E-mail enviado com sucesso! Obrigado pelo contato.")

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub
End Class