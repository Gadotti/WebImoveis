Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes

Public Class CadastroUsuario
    Inherits System.Web.UI.Page

    Public Enum enumMtPrincipal
        vwConsulta
        vwEdicao
    End Enum

    Public Property ListaUsuario As List(Of Usuario)
        Get
            If Session.Item("ListaUsuario") Is Nothing Then
                Return Nothing
            End If
            Return CType(Session.Item("ListaUsuario"), List(Of Usuario))
        End Get
        Set(ByVal value As List(Of Usuario))
            Session.Item("ListaUsuario") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                CarregaGridUsuario()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub CarregaGridUsuario()
        ListaUsuario = Usuario.Busca
        If ListaUsuario.Count > 0 Then
            gvUsuario.DataSource = Usuario.Busca
            gvUsuario.DataBind()
            gvUsuario.Visible = True
        Else
            gvUsuario.Visible = False
        End If

        Select Case ListaUsuario.Count
            Case 0
                lblTotalRegistro.Text = "Nenhum usuário encontrado nesta pesquisa."
                Master.ExibirAlerta(lblTotalRegistro.Text)
            Case 1
                lblTotalRegistro.Text = "Foi encontrado 1 usuário."
            Case Else
                lblTotalRegistro.Text = "Foram encontrados {0} usuários nesta pesquisa.".Fill(ListaUsuario.Count)
        End Select

        mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwConsulta
    End Sub

    Private Sub LimpaCampos()
        txtId.Text = "0"
        txtLogin.Text = String.Empty
        txtEmail.Text = String.Empty
        txtNome.Text = String.Empty
        txtSenha.Text = String.Empty
        txtSenhaRepetirSenha.Text = String.Empty
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwConsulta
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnNovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNovo.Click
        Try
            LimpaCampos()
            mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwEdicao
            txtLogin.Focus()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try
            If Not txtSenha.Text.Equals(txtSenhaRepetirSenha.Text) Then
                Master.ExibirAlerta("As senhas digitadas não conferem.")
                Return
            End If

            Dim objUsuario As New Usuario
            With objUsuario
                If txtId.Text.ToInt > 0 Then
                    .Id = txtId.Text.ToInt
                End If
                .Login = txtLogin.Text
                .Email = txtEmail.Text
                .Nome = txtNome.Text

                If .Id > 0 And String.IsNullOrEmpty(txtSenha.Text) Then
                    .Senha = Usuario.Busca(.Id).Senha
                Else
                    .Senha = txtSenha.Text
                End If

                .Salvar()
            End With

            CarregaGridUsuario()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcluir.Click
        Try

            Dim objUsuario As New Usuario
            objUsuario.Id = txtId.Text.ToInt
            objUsuario.Excluir()

            CarregaGridUsuario()

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub gvUsuario_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvUsuario.PageIndexChanging
        Try
            gvUsuario.PageIndex = e.NewPageIndex
            gvUsuario.DataSource = ListaUsuario
            gvUsuario.DataBind()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub gvUsuario_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvUsuario.RowCommand
        Try
            If e.CommandName.Equals("editar") Then
                Dim objUsuario As Usuario = Usuario.Busca(e.CommandArgument.ToString.ToInt)
                With objUsuario
                    txtId.Text = .Id.ToString
                    txtNome.Text = .Nome
                    txtLogin.Text = .Login
                    txtEmail.Text = .Email
                End With

                mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwEdicao
                txtLogin.Focus()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

End Class