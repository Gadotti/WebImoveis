Imports DataAccess.DataAccess
Imports Utilitarios.Extensoes
Imports Utilitarios.Utils

Public Class ImoveisSCBusiness

    Public Property ObjImovel As Imovel
    Public Property Url As String

    Public Sub New(ByVal pImovelId As Integer, ByVal pUrl As String)
        ObjImovel = Imovel.Busca(pImovelId)
        Url = pUrl & "/"
    End Sub

    Public Function Exportar() As String
        Dim texto As New Text.StringBuilder
        Dim objTipoImovelEQV As TipoImovelEQV = TipoImovelEQV.Busca(ObjImovel.TipoImovel)

        'Código Anúncio (Utilizado pelo Anunciante)
        texto.Append(ObjImovel.Referencia.ToAspasPontoVirgula)

        'Tipo Imóvel **
        texto.Append(objTipoImovelEQV.TipoImovelEQV.ToAspasPontoVirgula)

        'Tipo Negócio **
        If ObjImovel.TipoNegocio.Equals(1) Then
            texto.Append("Compra".ToAspasPontoVirgula)
        Else
            texto.Append("Aluga".ToAspasPontoVirgula)
        End If

        'Tipo Pesquisa (Residencial, Comercial / Industrial, Rural ou Aluguel de Temporada) **
        texto.Append(objTipoImovelEQV.TipoPesquisaEQV.ToAspasPontoVirgula)

        'Cidade **
        texto.Append(ObjImovel.CidadeDescricao.ToAspasPontoVirgula)

        'Bairro **
        texto.Append(ObjImovel.BairroDescricao.ToAspasPontoVirgula)

        'Endereço
        texto.Append(ObjImovel.Endereco.ToAspasPontoVirgula)

        'Título
        texto.Append(ObjImovel.TipoImovelDescricao.ToAspasPontoVirgula)

        'Texto
        texto.Append(ObjImovel.Descricao.Replace(Environment.NewLine, String.Empty).ToAspasPontoVirgula)

        'Observações
        texto.Append(String.Empty.ToAspasPontoVirgula)

        'Agenciador (Corretor Responsável pelo Anúncio)
        texto.Append(String.Empty.ToAspasPontoVirgula)

        'Qt Suítes **
        texto.Append(ObjImovel.QtdeSuite.ToString.ToAspasPontoVirgula)

        'Qt Dormitórios **
        texto.Append(ObjImovel.QtdeDormitorio.ToString.ToAspasPontoVirgula)

        'Link para site personalizado do Anúncio
        texto.Append((Url & "VisualizarImovel.aspx?imovelid={0}".Fill(ObjImovel.Id)).ToAspasPontoVirgula)

        'Qt Pessoas * **
        texto.Append(String.Empty.ToAspasPontoVirgula)

        'Qt Mínima de Dias *
        texto.Append(String.Empty.ToAspasPontoVirgula)

        'Vagas na Garagem **
        texto.Append(ObjImovel.QtdeGaragem.ToString.ToAspasPontoVirgula)

        'Situação Financeira **
        texto.Append("Quitado".ToAspasPontoVirgula)

        'Fase da Obra **
        texto.Append("Concluído".ToAspasPontoVirgula)

        'Área (com duas casas decimais)
        If ObjImovel.AreaConstruida > 0 Then
            texto.Append(FormataValor(ObjImovel.AreaConstruida).ToAspasPontoVirgula)
        Else
            texto.Append(FormataValor(ObjImovel.AreaTerreno).ToAspasPontoVirgula)
        End If

        'Unidade de Medida **
        texto.Append("m²".ToAspasPontoVirgula)

        'Valor (com duas casas de centavos)
        texto.Append(FormataValor(ObjImovel.Valor).ToAspasPontoVirgula)

        'Moeda **
        texto.Append("R$".ToAspasPontoVirgula)

        'Vista para o Mar * **
        texto.Append(String.Empty.ToAspasPontoVirgula)

        'Churrasqueira * **
        If ObjImovel.InChurrasqueira Then
            texto.Append("S".ToAspasPontoVirgula)
        Else
            texto.Append(String.Empty.ToAspasPontoVirgula)
        End If

        'Ar-Condicionado * **
        texto.Append(String.Empty.ToAspasPontoVirgula)

        'Dependência Completa de Empregada **
        If ObjImovel.InDependenciaEmpregada Then
            texto.Append("S".ToAspasPontoVirgula)
        Else
            texto.Append(String.Empty.ToAspasPontoVirgula)
        End If

        'Proprietário
        texto.Append(String.Empty.ToAspasPontoVirgula)

        'Fotos
        '============================================================================
        Dim objFotos As List(Of Foto) = Foto.Lista(ObjImovel.Id)
        Dim cont As Integer = 1
        For Each Foto In objFotos
            'Link Imagem:
            texto.Append((Url & "Fotos/" & ObjImovel.Id & "/" & Foto.NomeArquivo).ToAspasPontoVirgula)

            'No maxima 15 imagens
            If cont.Equals(15) Then
                Exit For
            End If

            cont += 1
        Next

        'Verifica se foi menos que 15 fotos
        If cont < 15 Then
            For ind = cont To 15
                texto.Append(String.Empty.ToAspasPontoVirgula)
            Next
        End If
        '============================================================================

        'Realiza o retorno das informações
        Return texto.ToString

    End Function
    
End Class
