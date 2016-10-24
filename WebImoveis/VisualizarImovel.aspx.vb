Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes
Imports Utilitarios.Utils
Imports Business
Imports Utilitarios

Public Class VisualizarImovel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                If Request.Params("imovelid") IsNot Nothing Then 'Fazer mensagem de erro else
                    Dim objImovel As Imovel = Imovel.Busca(Request.Params("imovelid").ToString.ToInt)
                    If objImovel IsNot Nothing Then 'Fazer mensagem de erro else

                        CarregaRedesSociais()

                        'Carregar campos
                        CarregaCampos(objImovel)

                        'Incrementa visualizador do imóvel
                        objImovel.QtdeVisitas += 1
                        objImovel.Salvar()

                        'Altera titulo do página
                        If Not String.IsNullOrEmpty(objImovel.Referencia) Then
                            Me.Title = "Imóvel referência {0}".Fill(objImovel.Referencia)
                        Else
                            Me.Title = "Imóvel código {0}".Fill(objImovel.Id)
                        End If

                        'Carrega galeria de fotos
                        ltrGaleriaFotos.Text = FotoBusiness.CarregaGaleria(objImovel.Id, "Fotos")

                        If Not String.IsNullOrEmpty(objImovel.Referencia) Then
                            txtAssunto.Text = "Imóvel Referência: " & objImovel.Referencia
                        Else
                            txtAssunto.Text = "Imóvel Código: " & objImovel.Id.ToString
                        End If

                        If Not Request.Cookies("meusimoveis") Is Nothing AndAlso Request.Cookies("meusimoveis").Value.IndexOf(objImovel.Id.ToString) >= 0 Then
                            lnkDermarcarImovel.Visible = True
                            lnkMarcarImovel.Visible = False
                        Else
                            lnkDermarcarImovel.Visible = False
                            lnkMarcarImovel.Visible = True
                        End If
                    End If
                End If
            End If
        Catch wrkErro As Exception
            ExibirAlerta("Ocorreu um erro inesperado. Detalhes: " & wrkErro.Message)
        End Try
    End Sub

    Private Sub CarregaRedesSociais()
        Dim url As String = Request.Url.ToString

        ltrFacebook.Text = "<fb:like class='facebook' href='{0}' layout='standard' width='100' show_faces='false'></fb:like>".Fill(url)
        ltrOrkut.Text = "<a href=""http://promote.orkut.com/preview?nt=orkut.com&tt=Achei%20um%20im%C3%B3vel%20interessante!&du={0}"" target=""_blank""><img src=""Images/pt_BR_orkut_regular-001.gif"" /></a>".Fill(url)

    End Sub


    Public Sub ExibirAlerta(ByVal mensagem As String)
        ClientScript.RegisterStartupScript(Me.GetType, "mensagem", "alert('" & mensagem.Replace("""", "").Replace("'", "") & "');", True)
    End Sub

    Private Sub CarregaCampos(ByVal objImovel As Imovel)
        With objImovel
            lblReferencia.Text = .Referencia.ToString
            lblTipoImovel.Text = .TipoImovelDescricao
            lblValor.Text = "R$ " & FormataValor(.Valor)
            lblCidade.Text = .CidadeDescricao & "/" & .EstadoSigla
            lblBairro.Text = IIf(String.IsNullOrEmpty(.BairroDescricao), "*Não informado", .BairroDescricao).ToString
            lblAreaConstruida.Text = FormataValor(.AreaConstruida) & " m²"
            lblTipoNegocio.Text = .TipoNegocioDescricao
            lblEndereco.Text = IIf(String.IsNullOrEmpty(.Endereco), "*Não informado", .Endereco).ToString

            'Se for terreno não é necessário mostrar o tipo de material
            If .TipoImovel.Equals(TipoImovel.enumTipoImovel.Terreno) Then
                divTipoMaterial.Visible = False
                divDormSalaBanh.Visible = False
                lblAreaConstruida.Text = "-"
                lblAreaConstruidaDescricao.Visible = False
            Else
                divTipoMaterial.Visible = True
                divDormSalaBanh.Visible = True
                lblAreaConstruida.Visible = True
                lblAreaConstruidaDescricao.Visible = True
                lblTipoMaterial.Text = .TipoMaterialDescricao
            End If

            If .ValorCondominio.Equals(0) Then
                divValorCondominio.Visible = False
            Else
                divValorCondominio.Visible = True
                lblValorCondominio.Text = FormataValor(.ValorCondominio).ToString
            End If

            lblAreaTerreno.Text = IIf(.AreaTerreno.Equals(0), "*Não informado", FormataValor(.AreaTerreno) & " m²").ToString
            lblQtdeDormitorio.Text = .QtdeDormitorio.ToString
            lblQtdeSuite.Text = .QtdeSuite.ToString
            lblQtdeBanheiro.Text = .QtdeBanheiro.ToString
            lblQtdeSala.Text = .QtdeSala.ToString
            lblQtdeGaragem.Text = .QtdeGaragem.ToString
            If Not String.IsNullOrEmpty(.Descricao) Then
                divDescricao.Visible = True
                lblDescricao.Text = .Descricao.Replace(Environment.NewLine, "<br/>")
            Else
                divDescricao.Visible = False
            End If
            If Not String.IsNullOrEmpty(.CondicaoVenda) Then
                divCondicaoVenda.Visible = True
                lblCondicaoVenda.Text = .CondicaoVenda.Replace(Environment.NewLine, "<br/>")
            Else
                divCondicaoVenda.Visible = False
            End If

            ltrListaPossui.Text = ImovelBusiness.MontaListaPossui(objImovel)
        End With
    End Sub

    Private Sub LimparCampos()
        txtNome.Text = String.Empty
        txtEmail.Text = String.Empty
        txtContato.Text = String.Empty
        txtAssunto.Text = String.Empty
        txtMensagem.Text = String.Empty
    End Sub

    Private Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            'Realiza o envio do e-mail
            Dim objParametro As DataAccess.DataAccess.Parametros = DataAccess.DataAccess.Parametros.Busca
            Utils.Mail(txtEmail.Text, "[Site] - " & txtAssunto.Text, txtMensagem.Text & Environment.NewLine & "Contato: " & txtContato.Text,
                       objParametro.Email, objParametro.EmailSenha, objParametro.SmtpHost, objParametro.SmtpPorta)
            LimparCampos()
            ExibirAlerta("E-mail enviado com sucesso! Obrigado pelo contato.")

        Catch wrkErro As Exception
            ExibirAlerta("Ocorreu um erro inesperado. Detalhes: " & wrkErro.Message)
        End Try
    End Sub

    Private Sub lnkMarcarImovel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMarcarImovel.Click
        Try
            Dim imoveis As String = String.Empty
            If Not Request.Cookies("meusimoveis") Is Nothing Then
                imoveis = Request.Cookies("meusimoveis").Value
            End If

            Response.Cookies("meusimoveis").Expires = Now.AddHours(48)
            Response.Cookies("meusimoveis").Value = imoveis & Request.Params("imovelid").ToString & "-"

            lnkDermarcarImovel.Visible = True
            lnkMarcarImovel.Visible = False
        Catch wrkErro As Exception
            ExibirAlerta("Ocorreu um erro inesperado. Detalhes: " & wrkErro.Message)
        End Try
    End Sub

    Private Sub lnkDermarcarImovel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDermarcarImovel.Click
        Try
            Dim imoveis As String = Request.Cookies("meusimoveis").Value.Replace(Request.Params("imovelid").ToString & "-", String.Empty)

            Response.Cookies("meusimoveis").Expires = Now.AddHours(48)
            Response.Cookies("meusimoveis").Value = imoveis

            lnkDermarcarImovel.Visible = False
            lnkMarcarImovel.Visible = True
        Catch wrkErro As Exception
            ExibirAlerta("Ocorreu um erro inesperado. Detalhes: " & wrkErro.Message)
        End Try
    End Sub
End Class