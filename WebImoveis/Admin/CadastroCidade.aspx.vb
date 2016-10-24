Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes

Public Class CadastroCidade
    Inherits System.Web.UI.Page

    Public Enum enumMtPrincipal
        vwConsulta
        vwEdicao
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                CarregaDdls()
                CarregaGridCidade()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub CarregaDdls()
        If ddlEstadoConsulta.Items.Count.Equals(0) Then
            ddlEstadoConsulta.DataSource = Estado.Busca
            ddlEstadoConsulta.DataValueField = "Id"
            ddlEstadoConsulta.DataTextField = "Descricao"
            ddlEstadoConsulta.DataBind()
        End If

        If ddlEstado.Items.Count.Equals(0) Then
            ddlEstado.DataSource = Estado.Busca
            ddlEstado.DataValueField = "Id"
            ddlEstado.DataTextField = "Descricao"
            ddlEstado.DataBind()
        End If
    End Sub

    Private Sub CarregaGridCidade()
        gvCidade.DataSource = Cidade.Busca(ddlEstadoConsulta.SelectedValue.ToInt)
        gvCidade.DataBind()
        mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwConsulta
    End Sub

    Private Sub ddlEstadoConsulta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoConsulta.SelectedIndexChanged
        Try
            CarregaGridCidade()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub gvCidade_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCidade.RowCommand
        Try
            If e.CommandName.Equals("editar") Then
                Dim objCidade As Cidade = Cidade.BuscaCidade(e.CommandArgument.ToString.ToInt)
                txtId.Text = objCidade.Id.ToString
                txtDescricao.Text = objCidade.Descricao
                ddlEstado.SelectedValue = objCidade.EstadoId.ToString

                mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwEdicao
                txtDescricao.Focus()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
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
            txtId.Text = "0"
            txtDescricao.Text = String.Empty
            ddlEstado.SelectedIndex = 0

            mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwEdicao
            ddlEstado.Focus()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try
            Dim objCidade As New Cidade
            If txtId.Text.ToInt > 0 Then
                objCidade.Id = txtId.Text.ToInt
            End If
            objCidade.Descricao = txtDescricao.Text
            objCidade.EstadoId = ddlEstado.SelectedValue.ToInt
            objCidade.Salvar()

            CarregaGridCidade()

            Master.ExibirAlerta("Operação realizada com sucesso.")
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcluir.Click
        Try
            Dim cidadeId As Integer = txtId.Text.ToInt
            If cidadeId > 0 Then
                If Cidade.PermiteExclusao(txtId.Text.ToInt) Then
                    Dim objCidade As New Cidade
                    objCidade.Id = txtId.Text.ToInt
                    objCidade.Excluir()

                    CarregaGridCidade()

                    Master.ExibirAlerta("Operação realizada com sucesso.")
                Else
                    Master.ExibirAlerta("Não é permitido excluir esta Cidade. Já esta sendo utilizado por um imóvel.")
                End If
            Else
                btnCancelar_Click(Nothing, Nothing)
            End If
            
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub
End Class