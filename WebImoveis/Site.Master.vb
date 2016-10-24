Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes

Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                CarregaCombos()
            End If
        Catch wrkErro As Exception
            ExibirAlerta("Ocorreu um erro inesperado. Detalhes: " & wrkErro.Message)
        End Try
    End Sub

    Private Sub CarregaCombos()
        'Carrega ddl TipoImovel
        If ddlTipoImovel.Items.Count.Equals(0) Then
            ddlTipoImovel.DataSource = TipoImovel.Busca
            ddlTipoImovel.DataValueField = "Id"
            ddlTipoImovel.DataTextField = "Descricao"
            ddlTipoImovel.DataBind()
            ddlTipoImovel.Items.Insert(0, New ListItem("Todos", "0"))
        End If
        '========================

        CarregaCidades(1)
    End Sub

    Private Sub CarregaCidades(ByVal pEstadoId As Integer)
        If ddlCidade.Items.Count.Equals(0) Then
            ddlCidade.DataSource = Cidade.Busca(pEstadoId)
            ddlCidade.DataValueField = "Id"
            ddlCidade.DataTextField = "Descricao"
            ddlCidade.DataBind()
            ddlCidade.Items.Insert(0, New ListItem("Todas", "0"))
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Se tiver referencia, envia somente ela
            If Not String.IsNullOrEmpty(txtReferencia.Text) Then
                Response.Redirect("~/Consulta.aspx?referencia={0}".Fill(txtReferencia.Text.Trim), False)
            Else
                'Outros filtros
                Dim redirect As New Text.StringBuilder
                redirect.Append("~/Consulta.aspx?")
                redirect.Append("cidadeId={0}".Fill(ddlCidade.SelectedValue.ToInt))
                redirect.Append("&tipoImovel={0}".Fill(ddlTipoImovel.SelectedValue.ToInt))

                If txtValorDe.Text.ToInt > 0 Then
                    redirect.Append("&valorDe={0}".Fill(txtValorDe.Text.ToInt))
                End If
                If txtValorAte.Text.ToInt > 0 Then
                    redirect.Append("&valorAte={0}".Fill(txtValorAte.Text.ToInt))
                End If

                'Direciona para página de consultas
                Response.Redirect(redirect.ToString, False)

            End If
        Catch wrkErro As Exception
            ExibirAlerta("Ocorreu um erro inesperado. Detalhes: " & wrkErro.Message)
        End Try
    End Sub

    Public Sub ExibirAlerta(ByVal mensagem As String)
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('" & mensagem.Replace("""", "").Replace("'", "") & "');", True)
    End Sub
End Class