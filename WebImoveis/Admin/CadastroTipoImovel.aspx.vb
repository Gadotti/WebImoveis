Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes

Public Class CadastroTipoImovel
    Inherits System.Web.UI.Page

    Public Enum enumMtPrincipal
        vwConsulta
        vwEdicao
    End Enum


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                CarregaGridTipoImovel()
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub CarregaGridTipoImovel()
        gvTipoImovel.DataSource = TipoImovel.Busca
        gvTipoImovel.DataBind()
        mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwConsulta
    End Sub

    Private Sub gvTipoImovel_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTipoImovel.RowCommand
        Try
            If e.CommandName.Equals("editar") Then
                Dim objTipoImovel As TipoImovel = TipoImovel.Busca(e.CommandArgument.ToString.ToInt)
                txtId.Text = objTipoImovel.Id.ToString
                txtDescricao.Text = objTipoImovel.Descricao

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

            mtPrincipal.ActiveViewIndex = enumMtPrincipal.vwEdicao
            txtDescricao.Focus()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try
            Dim objTipoImoivel As New TipoImovel
            If txtId.Text.ToInt > 0 Then
                objTipoImoivel.Id = txtId.Text.ToInt
            End If
            objTipoImoivel.Descricao = txtDescricao.Text
            objTipoImoivel.Salvar()

            CarregaGridTipoImovel()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcluir.Click
        Try
            If Imovel.BuscaPorTipoImoivel(txtId.Text.ToInt).Equals(0) Then
                Dim objTipoImoivel As New TipoImovel
                objTipoImoivel.Id = txtId.Text.ToInt
                objTipoImoivel.Excluir()

                CarregaGridTipoImovel()
            Else
                Master.ExibirAlerta("Não é permitido excluir este Tipo de Imóvel pois sendo utilizado em 1 ou mais imóveis.")
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub
End Class