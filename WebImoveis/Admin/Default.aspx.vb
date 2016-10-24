Imports DataAccess.DataAccess

Public Class _Default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                CarregaMaisVisitados()
                CarregaBairrosPesquisados()
                CarregaTipoImovelPesquisados()
                CarregaEstatistica()

                lblImoveisPublicados.Text = Imovel.QuantidadePublicados().ToString
                lblImoveisFechados.Text = Imovel.QuantidadeFechados().ToString
            End If

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub CarregaMaisVisitados()
        dtgMaisVisitados.DataSource = Imovel.MaisVisitados()
        dtgMaisVisitados.DataBind()
    End Sub

    Private Sub CarregaBairrosPesquisados()
        dtgBairros.DataSource = Bairro.MaisPesquisados
        dtgBairros.DataBind()
    End Sub

    Private Sub CarregaTipoImovelPesquisados()
        gvTipoImoveis.DataSource = TipoImovel.MaisPesquisados
        gvTipoImoveis.DataBind()
    End Sub

    Private Sub CarregaEstatistica()
        gvEstatisticas.DataSource = Estatistica.Busca
        gvEstatisticas.DataBind()
    End Sub

End Class