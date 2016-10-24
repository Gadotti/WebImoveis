Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes

Public Class CadastroCliente
    Inherits System.Web.UI.Page

    Public Enum enumMtPrincipal
        vwConsulta
        vwEdicao
    End Enum

    Public Property ListaCliente As List(Of Cliente)
        Get
            If Session.Item("ListaCliente") Is Nothing Then
                Return Nothing
            End If
            Return CType(Session.Item("ListaCliente"), List(Of Cliente))
        End Get
        Set(ByVal value As List(Of Cliente))
            Session.Item("ListaCliente") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                CarregaGridClientes()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub CarregaGridClientes()
        ListaCliente = Cliente.Busca
        If ListaCliente.Count > 0 Then
            gvCliente.DataSource = Cliente.Busca
            gvCliente.DataBind()
            gvCliente.Visible = True
        Else
            gvCliente.Visible = False
        End If

        Select Case ListaCliente.Count
            Case 0
                lblTotalRegistro.Text = "Nenhum cliente encontrado nesta pesquisa."
                Master.ExibirAlerta(lblTotalRegistro.Text)
            Case 1
                lblTotalRegistro.Text = "Foi encontrado 1 cliente."
            Case Else
                lblTotalRegistro.Text = "Foram encontrados {0} clientes nesta pesquisa.".Fill(ListaCliente.Count)
        End Select

        mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwConsulta
    End Sub

    Private Sub LimpaCampos()
        txtId.Text = "0"
        txtCpfCnpj.Text = String.Empty
        txtEmail.Text = String.Empty
        txtNome.Text = String.Empty
        txtObservacoes.Text = String.Empty
        txtSobrenome.Text = String.Empty
        txtTelefoneCelular.Text = String.Empty
        txtTelefoneComercial.Text = String.Empty
        txtTelefoneResidencial.Text = String.Empty
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
            txtNome.Focus()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try
            Dim objCliente As New Cliente
            If txtId.Text.ToInt > 0 Then
                objCliente.Id = txtId.Text.ToInt
            End If
            objCliente.CpfCnpj = txtCpfCnpj.Text.DeixaSoNumero
            objCliente.Email = txtEmail.Text
            objCliente.Nome = txtNome.Text
            objCliente.Observacoes = txtObservacoes.Text
            objCliente.Sobrenome = txtSobrenome.Text
            objCliente.TelefoneCelular = txtTelefoneCelular.Text
            objCliente.TelefoneComercial = txtTelefoneComercial.Text
            objCliente.TelefoneResidencial = txtTelefoneResidencial.Text

            objCliente.Salvar()

            CarregaGridClientes()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcluir.Click
        Try

            Dim objCliente As New Cliente
            objCliente.Id = txtId.Text.ToInt
            objCliente.Excluir()

            CarregaGridClientes()
            
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub gvCliente_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCliente.PageIndexChanging
        Try
            gvCliente.PageIndex = e.NewPageIndex
            gvCliente.DataSource = ListaCliente
            gvCliente.DataBind()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub gvCliente_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCliente.RowCommand
        Try
            If e.CommandName.Equals("editar") Then
                Dim objTipoImovel As Cliente = Cliente.Busca(e.CommandArgument.ToString.ToInt)
                With objTipoImovel
                    txtId.Text = .Id.ToString
                    txtNome.Text = .Nome
                    txtSobrenome.Text = .Sobrenome
                    txtCpfCnpj.Text = .CpfCnpj
                    txtTelefoneResidencial.Text = .TelefoneResidencial
                    txtTelefoneComercial.Text = .TelefoneComercial
                    txtTelefoneCelular.Text = .TelefoneCelular
                    txtEmail.Text = .Email
                    txtObservacoes.Text = .Observacoes
                End With

                mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwEdicao
                txtNome.Focus()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub
End Class