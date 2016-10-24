Imports DataAccess.DataAccess
Imports Utilitarios.Utils

Public Class Admin
    Inherits System.Web.UI.MasterPage

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
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Valida usuário conectado
            If UsuarioConectado Is Nothing Then
                'Verifica se no cookie contém o usuário autenticado
                If Not Request.Cookies(CookieUsuario) Is Nothing AndAlso Not String.IsNullOrEmpty(Request.Cookies(CookieUsuario).Value) Then
                    Dim lista As String() = Request.Cookies(CookieUsuario).Value.Split(CChar("-"))

                    'Obtem usuário
                    Dim objUsuario As Usuario = Usuario.AutenticaUsuario(lista(0), Decriptografa(HttpUtility.UrlDecode(lista(1))))

                    'Verifica se foi autenticado
                    If objUsuario Is Nothing Then
                        'Redireciona para pagina login
                        Response.Redirect("~/Admin/Login.aspx", False)
                    Else
                        'Armazena usuario novamente
                        UsuarioConectado = objUsuario
                    End If
                Else
                    'Redireciona para pagina login
                    Response.Redirect("~/Admin/Login.aspx", False)
                End If


            End If

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    'Registra trigger para postback
    Public Sub RegisterPostBackControl(ByVal control As Web.UI.Control)
        ScriptManager1.RegisterPostBackControl(control)
    End Sub

    Public Sub ExibirAlerta(ByVal mensagem As String)
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('" & mensagem.Replace(Chr(34), "").Replace("'", "") & "');", True)
    End Sub

    Public Sub ExecutaFuncaoScript(ByVal funcao As String)
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), funcao, True)
    End Sub

End Class