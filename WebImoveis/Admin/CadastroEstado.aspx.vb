Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes

Public Class CadastroEstado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                LimpaCampos()
                CarregaGridEstados()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub LimpaCampos()
        txtId.Text = "0"
        txtDescricao.Text = String.Empty
        txtSigla.Text = String.Empty
    End Sub

    Public Sub HabilitarCampos(ByVal pHabilitar As Boolean)
        txtDescricao.Enabled = pHabilitar
        txtSigla.Enabled = pHabilitar

        btnSalvar.Visible = pHabilitar
        btnCancelar.Visible = pHabilitar
        btnExcluir.Visible = pHabilitar
        btnNovo.Visible = Not pHabilitar
    End Sub

    Public Sub CarregaGridEstados()
        gvEstado.DataSource = Estado.Busca
        gvEstado.DataBind()
    End Sub

    Private Sub btnNovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNovo.Click
        Try
            LimpaCampos()
            HabilitarCampos(True)
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            LimpaCampos()
            HabilitarCampos(False)
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try
            Dim objEstado As New Estado
            objEstado.Id = txtId.Text.ToInt
            objEstado.Descricao = txtDescricao.Text
            objEstado.Sigla = txtSigla.Text.ToUpper
            objEstado.Salvar()

            LimpaCampos()
            HabilitarCampos(False)

            CarregaGridEstados()

            Master.ExibirAlerta("Operação realizada com sucesso.")
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcluir.Click
        Try
            Dim estadoId As Integer = txtId.Text.ToInt
            If estadoId > 0 Then
                If Estado.PermiteExclusao(estadoId) Then
                    Dim objEstado As Estado = Estado.Busca(estadoId)
                    objEstado.Excluir()

                    LimpaCampos()
                    HabilitarCampos(False)

                    CarregaGridEstados()

                    Master.ExibirAlerta("Operação realizada com sucesso.")
                Else
                    Master.ExibirAlerta("Não é permitido excluir este Estado. Já esta sendo utilizado por um imóvel ou cidade.")
                End If
            Else
                btnCancelar_Click(Nothing, Nothing)
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub gvEstado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvEstado.RowCommand
        Try
            If e.CommandName.Equals("editar") Then
                Dim objEstado As Estado = Estado.Busca(e.CommandArgument.ToString.ToInt)
                txtId.Text = objEstado.Id.ToString
                txtDescricao.Text = objEstado.Descricao
                txtSigla.Text = objEstado.Sigla

                HabilitarCampos(True)
                txtDescricao.Focus()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub
End Class