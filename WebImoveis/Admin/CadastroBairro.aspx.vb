Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes

Public Class CadastroBairro
    Inherits System.Web.UI.Page

    Public Enum enumMtPrincipal
        vwConsulta
        vwEdicao
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                CarregaDdls()
                CarregaGridBairro()
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

        If ddlCidadeConsulta.Items.Count.Equals(0) Then
            ddlCidadeConsulta.DataSource = Cidade.Busca(ddlEstadoConsulta.SelectedValue.ToInt)
            ddlCidadeConsulta.DataValueField = "Id"
            ddlCidadeConsulta.DataTextField = "Descricao"
            ddlCidadeConsulta.DataBind()
        End If

        If ddlCidade.Items.Count.Equals(0) Then
            ddlCidade.DataSource = Cidade.Busca(ddlEstado.SelectedValue.ToInt)
            ddlCidade.DataValueField = "Id"
            ddlCidade.DataTextField = "Descricao"
            ddlCidade.DataBind()
        End If
    End Sub

    Private Sub CarregaGridBairro()
        gvBairro.DataSource = Bairro.Busca(ddlCidadeConsulta.SelectedValue.ToInt)
        gvBairro.DataBind()
        mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwConsulta
    End Sub

    Private Sub ddlEstadoConsulta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoConsulta.SelectedIndexChanged
        Try
            ddlCidadeConsulta.DataSource = Cidade.Busca(ddlEstadoConsulta.SelectedValue.ToInt)
            ddlCidadeConsulta.DataValueField = "Id"
            ddlCidadeConsulta.DataTextField = "Descricao"
            ddlCidadeConsulta.DataBind()

            CarregaGridBairro()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Try
            ddlCidadeConsulta.DataSource = Cidade.Busca(ddlEstado.SelectedValue.ToInt)
            ddlCidadeConsulta.DataValueField = "Id"
            ddlCidadeConsulta.DataTextField = "Descricao"
            ddlCidadeConsulta.DataBind()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub ddlCidadeConsulta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCidadeConsulta.SelectedIndexChanged
        Try
            CarregaGridBairro()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub gvBairro_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvBairro.RowCommand
        Try
            If e.CommandName.Equals("editar") Then
                Dim objBairro As Bairro = Bairro.BuscaBairro(e.CommandArgument.ToString.ToInt)
                txtId.Text = objBairro.Id.ToString
                txtDescricao.Text = objBairro.Descricao
                ddlEstado.SelectedValue = objBairro.EstadoId.ToString

                'Carrega ddl
                ddlCidade.DataSource = Cidade.Busca(ddlEstado.SelectedValue.ToInt)
                ddlCidade.DataValueField = "Id"
                ddlCidade.DataTextField = "Descricao"
                ddlCidade.DataBind()
                '===========

                ddlCidade.SelectedValue = objBairro.CidadeId.ToString

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

            'Carrega ddl
            ddlCidade.DataSource = Cidade.Busca(ddlEstado.SelectedValue.ToInt)
            ddlCidade.DataValueField = "Id"
            ddlCidade.DataTextField = "Descricao"
            ddlCidade.DataBind()
            ddlCidade.SelectedIndex = 0
            '===========

            mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwEdicao
            ddlEstado.Focus()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try
            Dim objBairro As New Bairro
            If txtId.Text.ToInt > 0 Then
                objBairro.Id = txtId.Text.ToInt
            End If
            objBairro.Descricao = txtDescricao.Text
            objBairro.CidadeId = ddlCidade.SelectedValue.ToInt
            objBairro.Salvar()

            CarregaGridBairro()

            Master.ExibirAlerta("Operação realizada com sucesso.")
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcluir.Click
        Try
            Dim bairroId As Integer = txtId.Text.ToInt
            If bairroId > 0 Then
                If Bairro.PermiteExclusao(txtId.Text.ToInt) Then
                    Dim objBairro As New Bairro
                    objBairro.Id = txtId.Text.ToInt
                    objBairro.Excluir()

                    CarregaGridBairro()

                    Master.ExibirAlerta("Operação realizada com sucesso.")
                Else
                    Master.ExibirAlerta("Não é permitido excluir este Bairro. Já esta sendo utilizado por um imóvel.")
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