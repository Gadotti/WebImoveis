Namespace DataAccess
    Public Class ImovelItens
        Public Shared Lista As String() = {
            "Cozinha",
            "Copa",
            "Piscina",
            "Área de Servico",
            "Dep. de Empregada",
            "Churrasqueira",
            "Closet",
            "Adega",
            "Área de Festas",
            "Área de Jogos",
            "Lareira",
            "Sacada",
            "Escritório",
            "Gás Central",
            "Portão Eletrônico",
            "Mobiliado",
            "Lançamento",
            "Cobertura",
            "Pronta Entrega",
            "Porteiro Eletrônico",
            "Lavabo",
            "Terraço"}

        Public Shared Campos As String() = {
            "InCozinha",
            "InCopa",
            "InPiscina",
            "InAreaServico",
            "InDependenciaEmpregada",
            "InChurrasqueira",
            "InCloset",
            "InAdega",
            "InAreaFesta",
            "InAreaJogos",
            "InLareira",
            "InSacada",
            "InEscritorio",
            "InGasCentral",
            "InPortaoEletronico",
            "InMobiliado",
            "InLancamento",
            "InCobertura",
            "InPronto",
            "InPorteiroEletronico",
            "InLavabo",
            "InTerraco"}

        Public Sub New()
        End Sub

        Public Sub New(ByVal pNomeCampo As String)
            NomeCampo = pNomeCampo
        End Sub

        Public Property Id As Integer
        Public Property Descricao As String
        Public Property NomeCampo As String

        Public Shared Function CarregaLista() As List(Of ImovelItens)
            Dim retorno As New List(Of ImovelItens)
            For ind As Integer = 0 To Lista.Count - 1
                Dim item As New ImovelItens
                item.Id = ind
                item.Descricao = Lista(ind)
                item.NomeCampo = Campos(ind)
                retorno.Add(item)
            Next

            Return retorno.OrderBy(Function(T) T.Descricao).ToList
        End Function

    End Class
End Namespace

