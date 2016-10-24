Imports DataAccess.DataAccess
Imports Utilitarios.Utils

Public Class Login
    Inherits System.Web.UI.Page

    Private Const CookieUsuario As String = "webimoveis-usuario"

    Public Property UsuarioConectado As Usuario
        Get
            If Session.Item("Usuario") Is Nothing Then
                Return Nothing
            Else
                Return CType(Session.Item("Usuario"), Usuario)
            End If
        End Get
        Set(ByVal value As Usuario)
            Session.Item("Usuario") = value
            Response.Cookies(CookieUsuario).Value = value.Login & "-" & HttpUtility.UrlEncode(Criptografa(value.Senha))
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then

                'Seta propriedade iniciais
                divMensagemErro.Visible = False
                txtLogin.Focus()
                Session.Clear()
                Response.Cookies.Remove(CookieUsuario)
            End If

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnEntrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEntrar.Click
        Try
            'Obtem usuário
            Dim objUsuario As Usuario = Usuario.AutenticaUsuario(txtLogin.Text, txtSenha.Text)

            'Verifica se foi autenticado
            If objUsuario Is Nothing Then
                divMensagemErro.Visible = True
            Else
                'Armazena usuario
                UsuarioConectado = objUsuario

                'Redireciona para pagina default
                Response.Redirect("~/Admin/Default.aspx", False)
            End If


        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Public Sub ExibirAlerta(ByVal mensagem As String)
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('" & mensagem & "');", True)
    End Sub
End Class