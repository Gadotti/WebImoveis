Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes
Imports Utilitarios
Imports System.IO
Imports Business
Imports System.Globalization

Public Class CadastroImovel
    Inherits System.Web.UI.Page

    Private Enum enumMtwImovel
        Cadastro
        Foto
        Finalizacao
    End Enum

    Private Enum enumColunaDtgFotos
        Imagem
        Sequencia
        Descricao
        Excluir
    End Enum

    Public Property ImovelId As Integer
        Get
            If Session("ImovelId") Is Nothing Then
                Return Nothing
            End If

            Return Session("ImovelId").ToString.ToInt
        End Get
        Set(ByVal value As Integer)
            Session("ImovelId") = value
        End Set
    End Property

    Public Property ListaFotos As List(Of Foto)
        Get
            Return CType(Session("ListaFotos"), List(Of Foto))
        End Get
        Set(ByVal value As List(Of Foto))
            Session("ListaFotos") = value
        End Set
    End Property

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Adicionar trigger no updatepanel da Master page
            Master.RegisterPostBackControl(btnGravar)
            Master.RegisterPostBackControl(btnEnviarFoto)
            '===============================================

            If Not Me.IsPostBack Then

                'Limpa session
                ImovelId = 0
                ListaFotos = Nothing
                '=============

                'Carrega dos dropdownlist necessários
                CarregaDDls()
                '====================================

                'Carrega as cidades e bairros
                CarregaCidades(ddlEstado.SelectedValue.ToInt)
                CarregaBairros(ddlCidade.SelectedValue.ToInt)
                '============================

                'Verifica se é uma edição de imóvel, se for, carrega seu id
                If Not String.IsNullOrEmpty(Request.Params("imovelid")) Then
                    ImovelId = Request.Params("imovelid").ToInt
                    CarregaCampos()
                    btnExcluir.Visible = True
                End If
                '==================================

                txtReferencia.Focus()
            End If

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub ddlCidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCidade.SelectedIndexChanged
        Try
            CarregaBairros(ddlCidade.SelectedValue.ToInt)
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Try
            CarregaCidades(ddlEstado.SelectedValue.ToInt)
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnEnviarFoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviarFoto.Click
        Try
            'Verificar se a session ainda está ativa
            If ImovelId.Equals(0) Then
                Master.ExibirAlerta("Sessão expirada. Edite o imóvel e tente novamente")
                Exit Try
            End If

            GravaAlteracoesFotos()

            'Verifica se há arquivo selecionado
            If uplFoto.HasFile Then

                'Verifica se existe o diretorio
                Dim wrkDiretorio As String = Path.Combine(Server.MapPath("~/Fotos"), ImovelId.ToString)
                If Not Directory.Exists(wrkDiretorio) Then
                    Directory.CreateDirectory(wrkDiretorio)
                End If
                '==============================

                'Controi nome que possuirá o arquivo
                Dim wrkFileName As String = Now.Year &
                    Now.Month &
                    Now.Day &
                    Now.Hour &
                    Now.Minute &
                    Now.Second &
                    Now.Millisecond &
                    Path.GetExtension(uplFoto.FileName)
                '===================================

                'Grava arquivo no diretorio
                FotoBusiness.GravaFoto(uplFoto.PostedFile, wrkDiretorio, wrkFileName)
                'uplFoto.SaveAs(Path.Combine(wrkDiretorio, wrkFileName))
                '==========================

                'Verifica se existe lista
                If ListaFotos Is Nothing Then
                    ListaFotos = New List(Of Foto)
                End If
                '========================

                'Cria objeto de foto
                Dim objFoto As New Foto
                objFoto.NomeArquivo = wrkFileName
                objFoto.ImovelId = ImovelId
                objFoto.Sequencia = (ListaFotos.Count + 1) * 10
                objFoto.Salvar()
                '===================

                'Adicionar foto à lista
                ListaFotos.Add(objFoto)
                '======================

                'Carrega grid de fotos
                dtgFotos.DataSource = ListaFotos.OrderBy(Function(T) T.Sequencia)
                dtgFotos.DataBind()
                '=====================

                'Habilita botoes
                btnExcluirFotos.Visible = True
            End If
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnGravar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGravar.Click

        Try
            'Grava imovel no banco de dados
            Dim objImovel As Imovel = CarregaObjetoImovel()
            objImovel.Salvar()
            '==============================

            'Obtem o id gravado
            ImovelId = objImovel.Id
            '==================

            'Muda paa tela de fotos
            mtwImovel.ActiveViewIndex = enumMtwImovel.Foto
            '=======================


        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnFinalizarFotos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalizarFotos.Click
        Try
            'Verificar se a session ainda está ativa
            If ImovelId.Equals(0) Then
                Master.ExibirAlerta("Sessão expirada. Edite o imóvel e tente novamente")
                Exit Try
            End If

            'Grava ultima alteração nas fotos
            GravaAlteracoesFotos()

            'Mostra Painel de imóvel gravado com sucesso
            lblImovelId.Text = ImovelId.ToString
            ImovelId = 0
            ListaFotos = Nothing
            mtwImovel.ActiveViewIndex = enumMtwImovel.Finalizacao
            '===========================================

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub dtgFotos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dtgFotos.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.Header
                    'Adicinar script para selecionar todos
                    Dim wrkChk As CheckBox = CType(e.Row.Cells(0).FindControl("chkExcluirTodos"), CheckBox)
                    wrkChk.Attributes("Onclick") = "javascript:SelecionaTodos(" & wrkChk.ClientID.ToString & ", 'chkExcluir')"
                Case DataControlRowType.DataRow
                    'Adicinar script para abrir visualização da imagem
                    Dim imgButton As ImageButton = CType(e.Row.Cells(enumColunaDtgFotos.Imagem).FindControl("imgNmArqvFoto"), ImageButton)
                    imgButton.Attributes("OnClick") = "javascript:window.open('" &
                        imgButton.ImageUrl.ToString &
                        "'); return false;"


                    Dim txtDescricaoFoto As TextBox = CType(e.Row.Cells(enumColunaDtgFotos.Descricao).FindControl("txtDescricaoFoto"), TextBox)
                    txtDescricaoFoto.Attributes("OnKeyPress") = "javascript:MaxLengthMultiLine(this,255);"

            End Select
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnExcluirFotos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcluirFotos.Click
        Try
            'Passa por todas os itens verificando
            '  Tem q fazer um for ao contrario, (baixo para cima)
            For ind = (dtgFotos.Rows.Count - 1) To 0 Step -1
                Dim linha As GridViewRow = dtgFotos.Rows(ind)
                If CType(linha.FindControl("chkExcluir"), CheckBox).Checked Then
                    'Obtem Id
                    Dim fotoId As Integer = dtgFotos.DataKeys(linha.DataItemIndex).Value.ToString.ToInt

                    'Obtem objeto
                    Dim objFoto As Foto = ListaFotos.Find(Function(T) T.Id = fotoId)

                    'Obtem path do arquivo
                    Dim arquivo As String = Path.Combine(My.Settings.DiretorioFotos, ImovelId.ToString, objFoto.NomeArquivo)

                    'Retira da lista
                    ListaFotos.Remove(objFoto)

                    'Realiza exclusão
                    objFoto.Excluir()

                    'Remove arquivo fisico
                    If File.Exists(arquivo) Then
                        File.Delete(arquivo)
                    End If
                End If
            Next

            'Carrega grid de fotos
            dtgFotos.DataSource = ListaFotos.OrderBy(Function(T) T.Sequencia)
            dtgFotos.DataBind()

        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnNovoCadastro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNovoCadastro.Click
        Try
            Response.Redirect("~/Admin/CadastroImovel.aspx", False)
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnLimpar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpar.Click
        Try
            LimpaCampos()
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub

    Private Sub btnExcluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcluir.Click
        Try
            Dim objImovel As Imovel = Imovel.Busca(ImovelId)
            objImovel.Excluir(My.Settings.DiretorioFotos)

            Response.Redirect("~/Admin/Default.aspx", False)

            Master.ExibirAlerta("Imóvel excluído com sucesso")
        Catch wrkErro As Exception
            Elmah.ErrorSignal.FromCurrentContext().Raise(wrkErro)
            Master.ExibirAlerta("Ocorreu um erro inesperado. O erro já foi registrado e será analisado.")
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub CarregaCampos()
        Dim objImovel As Imovel = Imovel.Busca(ImovelId)

        With objImovel
            txtId.Text = .Id.ToString
            txtReferencia.Text = .Referencia
            ddlTipoNegocio.SelectedValue = .TipoNegocio.ToString
            ddlEstado.SelectedValue = .EstadoId.ToString
            CarregaCidades(.EstadoId)
            ddlCidade.SelectedValue = .CidadeId.ToString
            CarregaBairros(.CidadeId)
            ddlBairro.SelectedValue = .BairroId.ToString
            txtEnderenco.Text = .Endereco
            ddlTipoImovel.SelectedValue = .TipoImovel.ToString
            ddlTipoMaterial.SelectedValue = .TipoMaterial.ToString
            txtValor.Text = Utils.FormataValor(.Valor)
            txtValorCondominio.Text = Utils.FormataValor(.ValorCondominio)
            txtAreaConstruida.Text = Utils.FormataValor(.AreaConstruida)
            txtAreaTerreno.Text = Utils.FormataValor(.AreaTerreno)
            txtQtdeDormitorio.Text = .QtdeDormitorio.ToString
            txtQtdeSuite.Text = .QtdeSuite.ToString
            txtQtdeBanheiro.Text = .QtdeBanheiro.ToString
            txtQtdeSala.Text = .QtdeSala.ToString
            txtQtdeGaragem.Text = .QtdeGaragem.ToString
            txtDescricao.Text = .Descricao
            txtCondicaoVenda.Text = .CondicaoVenda
            InCozinha.Checked = .InCozinha
            InCopa.Checked = .InCopa
            InPiscina.Checked = .InPiscina
            InAreaServico.Checked = .InAreaServico
            InDependenciaEmpregada.Checked = .InDependenciaEmpregada
            InChurrasqueira.Checked = .InChurrasqueira
            InCloset.Checked = .InCloset
            InAdega.Checked = .InAdega
            InAreaFesta.Checked = .InAreaFesta
            InAreaJogos.Checked = .InAreaJogos
            InLareira.Checked = .InLareira
            InSacada.Checked = .InSacada
            InEscritorio.Checked = .InEscritorio
            InGasCentral.Checked = .InGasCentral
            InPortaoEletronico.Checked = .InPortaoEletronico
            InMobiliado.Checked = .InMobiliado
            InLancamento.Checked = .InLancamento
            InCobertura.Checked = .InCobertura
            InPronto.Checked = .InPronto
            InDestaque.Checked = .Destaque
            InPorteiroEletronico.Checked = .InPorteiroEletronico
            InLavabo.Checked = .InLavabo
            InTerraco.Checked = .InTerraco
            InPublicar.Checked = .InPublicar
            If .DataFechamento = Nothing OrElse .DataFechamento.Equals(Date.MinValue) Then
                txtDataFechamento.Text = String.Empty
            Else
                txtDataFechamento.Text = Format(.DataFechamento, "dd/MM/yyyy")
            End If
            'Carrega Fotos
            ListaFotos = Foto.Lista(ImovelId)
            dtgFotos.DataSource = ListaFotos
            dtgFotos.DataBind()
        End With

    End Sub

    Private Sub LimpaCampos()
        txtId.Text = String.Empty
        txtReferencia.Text = String.Empty
        ddlTipoNegocio.SelectedIndex = 0
        ddlEstado.SelectedIndex = 0
        ddlCidade.SelectedIndex = 0
        ddlBairro.SelectedIndex = 0
        txtEnderenco.Text = String.Empty
        ddlTipoImovel.SelectedIndex = 0
        ddlTipoMaterial.SelectedIndex = 0
        txtValor.Text = String.Empty
        txtValorCondominio.Text = String.Empty
        txtAreaConstruida.Text = String.Empty
        txtAreaTerreno.Text = String.Empty
        txtQtdeDormitorio.Text = String.Empty
        txtQtdeSuite.Text = String.Empty
        txtQtdeBanheiro.Text = String.Empty
        txtQtdeSala.Text = String.Empty
        txtQtdeGaragem.Text = String.Empty
        txtDescricao.Text = String.Empty
        txtCondicaoVenda.Text = String.Empty
        InCozinha.Checked = False
        InCopa.Checked = False
        InPiscina.Checked = False
        InAreaServico.Checked = False
        InDependenciaEmpregada.Checked = False
        InChurrasqueira.Checked = False
        InCloset.Checked = False
        InAdega.Checked = False
        InAreaFesta.Checked = False
        InAreaJogos.Checked = False
        InLareira.Checked = False
        InSacada.Checked = False
        InEscritorio.Checked = False
        InGasCentral.Checked = False
        InPortaoEletronico.Checked = False
        InMobiliado.Checked = False
        InLancamento.Checked = False
        InCobertura.Checked = False
        InPronto.Checked = False
        InDestaque.Checked = False
        InPorteiroEletronico.Checked = False
        InLavabo.Checked = False
        InTerraco.Checked = False
        InPublicar.Checked = True
        txtDataFechamento.Text = String.Empty
    End Sub

    Private Sub GravaAlteracoesFotos()
        If ListaFotos IsNot Nothing Then
            'Passa por cada linha do grid
            For Each linha As GridViewRow In dtgFotos.Rows
                'Obtem o ID
                Dim fotoId As Integer = dtgFotos.DataKeys(linha.DataItemIndex).Value.ToString.ToInt

                If fotoId > 0 Then
                    'Altera o objeto de foto
                    Dim objFoto As Foto = ListaFotos.Find(Function(T) T.Id = fotoId)
                    objFoto.ImovelId = ImovelId
                    objFoto.Descricao = CType(linha.FindControl("txtDescricaoFoto"), TextBox).Text
                    objFoto.Sequencia = CType(linha.FindControl("txtNrSequ"), TextBox).Text.ToInt
                    objFoto.Salvar()
                Else
                    Master.ExibirAlerta("Não foi possível obter identificador da foto ao gravar as alterações.")
                End If
                
            Next
        End If
    End Sub

    Private Function CarregaObjetoImovel() As Imovel
        'Regional settings para datas
        Dim ciNewFormat As New CultureInfo(CultureInfo.CurrentCulture.ToString())
        ciNewFormat.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture = ciNewFormat
        '============================

        Dim objImovel As New Imovel
        Dim objOldImovel As Imovel = Nothing
        With objImovel

            'Prevenção caso a session seja perdida
            If ImovelId.Equals(0) AndAlso Not String.IsNullOrEmpty(Request.Params("imovelid")) Then
                ImovelId = Request.Params("imovelid").ToInt
            End If
            '=====================================

            If ImovelId > 0 Then
                .Id = ImovelId

                'Mantem as variáveis necessárias
                objOldImovel = Imovel.Busca(ImovelId)
                .QtdeVisitas = objOldImovel.QtdeVisitas
            End If
            .Referencia = txtReferencia.Text
            .TipoNegocio = ddlTipoNegocio.SelectedValue.ToInt
            .EstadoId = ddlEstado.SelectedValue.ToInt
            .CidadeId = ddlCidade.SelectedValue.ToInt
            .BairroId = ddlBairro.SelectedValue.ToInt
            .Endereco = txtEnderenco.Text
            .TipoImovel = ddlTipoImovel.SelectedValue.ToInt
            .TipoMaterial = ddlTipoMaterial.SelectedValue.ToInt
            .Valor = txtValor.Text.ToInt64
            .ValorCondominio = txtValorCondominio.Text.ToInt
            .AreaConstruida = txtAreaConstruida.Text.ToInt
            .AreaTerreno = txtAreaTerreno.Text.ToInt
            .QtdeDormitorio = txtQtdeDormitorio.Text.ToInt
            .QtdeSuite = txtQtdeSuite.Text.ToInt
            .QtdeBanheiro = txtQtdeBanheiro.Text.ToInt
            .QtdeSala = txtQtdeSala.Text.ToInt
            .QtdeGaragem = txtQtdeGaragem.Text.ToInt
            .InCozinha = InCozinha.Checked
            .InCopa = InCopa.Checked
            .InPiscina = InPiscina.Checked
            .InAreaServico = InAreaServico.Checked
            .InDependenciaEmpregada = InDependenciaEmpregada.Checked
            .InChurrasqueira = InChurrasqueira.Checked
            .InCloset = InCloset.Checked
            .InAdega = InAdega.Checked
            .InAreaFesta = InAreaFesta.Checked
            .InAreaJogos = InAreaJogos.Checked
            .InLareira = InLareira.Checked
            .InSacada = InSacada.Checked
            .InEscritorio = InEscritorio.Checked
            .InGasCentral = InGasCentral.Checked
            .InPortaoEletronico = InPortaoEletronico.Checked
            .InMobiliado = InMobiliado.Checked
            .InLancamento = InLancamento.Checked
            .InCobertura = InCobertura.Checked
            .InPronto = InPronto.Checked
            If txtDescricao.Text.Length > 500 Then .Descricao = txtDescricao.Text.Substring(0, 500) Else .Descricao = txtDescricao.Text
            If txtCondicaoVenda.Text.Length > 255 Then .CondicaoVenda = txtCondicaoVenda.Text.Substring(0, 255) Else .CondicaoVenda = txtCondicaoVenda.Text
            .Destaque = InDestaque.Checked
            .InPorteiroEletronico = InPorteiroEletronico.Checked
            .InLavabo = InLavabo.Checked
            .InTerraco = InTerraco.Checked
            .DataAlteracao = Now
            If Master.UsuarioConectado IsNot Nothing Then
                .UsuarioId = Master.UsuarioConectado.Id
            End If
            If Not String.IsNullOrEmpty(txtDataFechamento.Text) AndAlso IsDate(txtDataFechamento.Text) Then
                .DataFechamento = CDate(IIf(String.IsNullOrEmpty(txtDataFechamento.Text), Date.MinValue, CDate(txtDataFechamento.Text)))
                .InPublicar = False
            Else
                .InPublicar = InPublicar.Checked
                .DataFechamento = Date.MinValue
            End If
        End With

        Return objImovel
    End Function

    Private Sub CarregaDDls()
        'Carrega ddl de estado
        If ddlEstado.Items.Count.Equals(0) Then
            ddlEstado.DataSource = Estado.Busca
            ddlEstado.DataValueField = "Id"
            ddlEstado.DataTextField = "Descricao"
            ddlEstado.DataBind()
        End If
        '=======================

        'Carrega ddl TipoNegocio
        If ddlTipoNegocio.Items.Count.Equals(0) Then
            ddlTipoNegocio.DataSource = TipoNegocio.Busca.OrderByDescending(Function(T) T.Descricao)
            ddlTipoNegocio.DataValueField = "Id"
            ddlTipoNegocio.DataTextField = "Descricao"
            ddlTipoNegocio.DataBind()
        End If
        '=========================

        'Carrega ddl TipoImovel
        If ddlTipoImovel.Items.Count.Equals(0) Then
            ddlTipoImovel.DataSource = TipoImovel.Busca
            ddlTipoImovel.DataValueField = "Id"
            ddlTipoImovel.DataTextField = "Descricao"
            ddlTipoImovel.DataBind()
        End If
        '========================

        'Carrega ddl TipoMaterial
        If ddlTipoMaterial.Items.Count.Equals(0) Then
            ddlTipoMaterial.DataSource = TipoMaterial.Busca
            ddlTipoMaterial.DataValueField = "Id"
            ddlTipoMaterial.DataTextField = "Descricao"
            ddlTipoMaterial.DataBind()
        End If
        '========================

    End Sub

    Private Sub CarregaCidades(ByVal pEstadoId As Integer)
        ddlCidade.DataSource = Cidade.Busca(pEstadoId)
        ddlCidade.DataValueField = "Id"
        ddlCidade.DataTextField = "Descricao"
        ddlCidade.DataBind()
    End Sub

    Private Sub CarregaBairros(ByVal pCidadeId As Integer)
        ddlBairro.DataSource = Bairro.Busca(pCidadeId)
        ddlBairro.DataValueField = "Id"
        ddlBairro.DataTextField = "Descricao"
        ddlBairro.DataBind()
    End Sub
#End Region

    
End Class