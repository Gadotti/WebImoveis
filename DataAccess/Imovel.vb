Imports System
Imports System.Collections
Imports NHibernate.Cfg
Imports NHibernate
Imports NHibernate.Exceptions
Imports NHibernate.Connection
Imports Iesi.Collections
Imports log4net
Imports NHibernate.Criterion
Imports System.IO

Namespace DataAccess
    Public Class Imovel
        Public Overridable Property Id As Integer
        Public Overridable Property Referencia As String
        Public Overridable Property DataInclusao As Date
        Public Overridable Property DataAlteracao As Date
        Public Overridable Property Valor As Int64
        Public Overridable Property ValorCondominio As Integer
        Public Overridable Property EstadoId As Integer
        Public Overridable Property CidadeId As Integer
        Public Overridable Property BairroId As Integer
        Public Overridable Property Endereco As String
        Public Overridable Property TipoImovel As Integer
        Public Overridable Property TipoMaterial As Integer
        Public Overridable Property TipoNegocio As Integer
        Public Overridable Property AreaConstruida As Integer
        Public Overridable Property AreaTerreno As Integer
        Public Overridable Property QtdeDormitorio As Integer
        Public Overridable Property QtdeSuite As Integer
        Public Overridable Property QtdeBanheiro As Integer
        Public Overridable Property QtdeSala As Integer
        Public Overridable Property QtdeGaragem As Integer
        Public Overridable Property InCozinha As Boolean
        Public Overridable Property InCopa As Boolean
        Public Overridable Property InPiscina As Boolean
        Public Overridable Property InAreaServico As Boolean
        Public Overridable Property InDependenciaEmpregada As Boolean
        Public Overridable Property InChurrasqueira As Boolean
        Public Overridable Property InCloset As Boolean
        Public Overridable Property InAdega As Boolean
        Public Overridable Property InAreaFesta As Boolean
        Public Overridable Property InAreaJogos As Boolean
        Public Overridable Property InLareira As Boolean
        Public Overridable Property InSacada As Boolean
        Public Overridable Property InEscritorio As Boolean
        Public Overridable Property InGasCentral As Boolean
        Public Overridable Property InPortaoEletronico As Boolean
        Public Overridable Property InMobiliado As Boolean
        Public Overridable Property InLancamento As Boolean
        Public Overridable Property InCobertura As Boolean
        Public Overridable Property InPronto As Boolean
        Public Overridable Property Descricao As String
        Public Overridable Property CondicaoVenda As String
        Public Overridable Property Destaque As Boolean
        Public Overridable Property InPorteiroEletronico As Boolean
        Public Overridable Property InLavabo As Boolean
        Public Overridable Property InTerraco As Boolean
        Public Overridable Property DataFechamento As Date
        Public Overridable Property InPublicar As Boolean
        Public Overridable Property UsuarioId As Integer
        Public Overridable Property QtdeVisitas As Integer

        Public Overridable ReadOnly Property EstadoSigla As String
            Get
                Return Estado.BuscaSigla(EstadoId)
            End Get
        End Property

        Public Overridable ReadOnly Property CidadeDescricao As String
            Get
                Return Cidade.BuscaDescricao(CidadeId)
            End Get
        End Property

        Public Overridable ReadOnly Property BairroDescricao As String
            Get
                Return Bairro.BuscaDescricao(BairroId)
            End Get
        End Property

        Public Overridable ReadOnly Property TipoImovelDescricao As String
            Get
                Return DataAccess.TipoImovel.BuscaDescricao(TipoImovel)
            End Get
        End Property

        Public Overridable ReadOnly Property TipoNegocioDescricao As String
            Get
                Return DataAccess.TipoNegocio.BuscaDescricao(TipoNegocio)
            End Get
        End Property

        Public Overridable ReadOnly Property TipoMaterialDescricao As String
            Get
                Return DataAccess.TipoMaterial.BuscaDescricao(TipoMaterial)
            End Get
        End Property

        Public Overridable Sub Salvar()
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            'Dim mTransaction As ITransaction = mSession.BeginTransaction

            Try
                'Persistindo o objeto no banco de dados
                mSession.SaveOrUpdate(Me)
                mSession.Flush()
                'mTransaction.Commit()
                'mSession.Close()
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
                'Catch ex As Exception
                'mTransaction.Rollback()

                'Throw ex
            End Try

        End Sub

        Public Sub Excluir(ByVal diretorioFotos As String)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            Dim mTransaction As ITransaction = mSession.BeginTransaction

            Try
                Dim imovelId As Integer = Me.Id

                'Busca e deleta as fotos
                Dim objFotos As List(Of Foto) = Foto.Lista(imovelId)
                For Each item In objFotos
                    mSession.Delete(item)
                Next
                mSession.Flush()
                '======================

                'Deleta objeto Imovel
                mSession.Delete(Me)
                mSession.Flush()
                '====================

                'Se tudo ocorreu bem, exclui os arquivo físicos
                Dim wrkDiretorio As String = Path.Combine(diretorioFotos, imovelId.ToString)
                If Directory.Exists(wrkDiretorio) Then
                    Directory.Delete(wrkDiretorio, True)
                End If
                '==============================================

                mTransaction.Commit()
                mSession.Close()

            Catch ex As Exception
                mTransaction.Rollback()
                Throw ex
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try
        End Sub

        Public Shared Function Existe(ByVal pImovelId As Integer) As Boolean
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As Integer = 0

            Try
                Dim criteria As ICriteria = mSession.CreateCriteria(GetType(Imovel)).
                Add(Restrictions.Eq("Id", pImovelId)).
                SetProjection(Projections.CountDistinct("Id"))

                retorno = Convert.ToInt32(criteria.UniqueResult)
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            If retorno > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Shared Function Busca(ByVal pImovelId As Integer) As Imovel
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As Imovel

            Try
                retorno = DirectCast(mSession.Load(GetType(Imovel), pImovelId), Imovel)

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function BuscaFechamento(ByVal pDataFechamentoDe As Date, ByVal pDataFechamentoAte As Date) As List(Of Imovel)
            Dim retorno As New List(Of Imovel)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                Dim listaImovel As IList = mSession.CreateCriteria(GetType(Imovel)).
                    Add(Restrictions.Between("DataFechamento", pDataFechamentoDe, pDataFechamentoAte)).
                    List()

                For Each item In listaImovel
                    retorno.Add(CType(item, Imovel))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function BuscaDestaques() As List(Of Imovel)
            Dim retorno As New List(Of Imovel)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                Dim listaImovel As IList = mSession.CreateCriteria(GetType(Imovel)).
                    Add(Restrictions.Eq("Destaque", True)).
                    Add(Restrictions.Eq("InPublicar", True)).
                    List()

                For Each item In listaImovel
                    retorno.Add(CType(item, Imovel))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function BuscaPorTipoImoivel(ByVal pTipoImovelId As Integer) As Integer
            Dim retorno As Integer
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                retorno = mSession.CreateCriteria(GetType(Imovel)).
                    Add(Restrictions.Eq("TipoImovel", pTipoImovelId)).
                    Add(Restrictions.Eq("InPublicar", True)).
                    List.Count()

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function Busca(ByVal pReferencia As String, ByVal pEstadoId As Integer, _
                                     ByVal pCidadeId As Integer, ByVal pBairroId As Integer, _
                                     ByVal pTipoImovel As Integer, ByVal pTipoMaterial As Integer, _
                                     ByVal pValorDe As Integer, ByVal pValorAte As Integer, _
                                     Optional ByVal pApenasPublicados As Boolean = True) As List(Of Imovel)

            Dim retorno As New List(Of Imovel)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                Dim criteria As ICriteria = mSession.CreateCriteria(GetType(Imovel))

                If Not String.IsNullOrEmpty(pReferencia) Then
                    criteria.Add(Restrictions.Eq("Referencia", pReferencia))
                End If
                If pEstadoId > 0 Then
                    criteria.Add(Restrictions.Eq("EstadoId", pEstadoId))
                End If
                If pCidadeId > 0 Then
                    criteria.Add(Restrictions.Eq("CidadeId", pCidadeId))
                End If
                If pBairroId > 0 Then
                    criteria.Add(Restrictions.Eq("BairroId", pBairroId))
                    Bairro.IncrementaPesquisa(pBairroId)
                End If
                If pTipoImovel > 0 Then
                    criteria.Add(Restrictions.Eq("TipoImovel", pTipoImovel))
                    DataAccess.TipoImovel.IncrementaPesquisa(pTipoImovel)
                End If
                If pTipoMaterial > 0 Then
                    criteria.Add(Restrictions.Eq("TipoMaterial", pTipoMaterial))
                End If
                If pValorDe > 0 Then
                    criteria.Add(Restrictions.Ge("Valor", pValorDe))
                End If
                If pValorAte > 0 Then
                    criteria.Add(Restrictions.Le("Valor", pValorAte))
                    Estatistica.IncrementaPesquisa(pValorAte)
                End If

                If pApenasPublicados Then
                    criteria.Add(Restrictions.Eq("InPublicar", True))
                End If

                Dim listaImovel As IList = criteria.List

                For Each item In listaImovel
                    retorno.Add(CType(item, Imovel))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.OrderBy(Function(T) T.TipoImovel).
                    ThenBy(Function(T) T.CidadeId).
                    ThenBy(Function(T) T.BairroId).ToList
            Else
                Return retorno
            End If


        End Function

        Public Shared Function BuscaLancamentos() As List(Of Imovel)
            Dim retorno As New List(Of Imovel)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                Dim criteria As ICriteria = mSession.CreateCriteria(GetType(Imovel))
                criteria.Add(Restrictions.Eq("InLancamento", True))

                Dim listaImovel As IList = criteria.List

                For Each item In listaImovel
                    retorno.Add(CType(item, Imovel))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.OrderBy(Function(T) T.TipoImovel).
                    ThenBy(Function(T) T.CidadeId).
                    ThenBy(Function(T) T.BairroId).ToList
            Else
                Return retorno
            End If
        End Function

        Public Shared Function Busca(ByVal pListaId As String()) As List(Of Imovel)
            Dim retorno As New List(Of Imovel)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                Dim criteria As ICriteria = mSession.CreateCriteria(GetType(Imovel))
                criteria.Add(Restrictions.In("Id", pListaId))

                Dim listaImovel As IList = criteria.List

                For Each item In listaImovel
                    retorno.Add(CType(item, Imovel))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.OrderBy(Function(T) T.TipoImovel).
                    ThenBy(Function(T) T.CidadeId).
                    ThenBy(Function(T) T.BairroId).ToList
            Else
                Return retorno
            End If
        End Function

        Public Shared Function QuantidadePublicados() As Integer
            Dim retorno As Integer = 0
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                retorno = mSession.CreateCriteria(GetType(Imovel)).
                    Add(Restrictions.Eq("InPublicar", True)).SetProjection(Projections.Count("Id")).UniqueResult(Of Integer)()

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function QuantidadeFechados() As Integer
            Dim retorno As Integer = 0
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                retorno = mSession.CreateCriteria(GetType(Imovel)).
                    Add(Restrictions.Gt("DataFechamento", Date.MinValue)).SetProjection(Projections.Count("Id")).UniqueResult(Of Integer)()

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function MaisVisitados() As List(Of Imovel)
            Dim retorno As New List(Of Imovel)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                Dim listaImovel As IList = mSession.CreateCriteria(GetType(Imovel)).
                    AddOrder(Order.Desc("QtdeVisitas")).SetMaxResults(5).List

                For Each item In listaImovel
                    retorno.Add(CType(item, Imovel))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno

        End Function

        Public Shared Function Busca(ByVal pEstadoId As Integer, ByVal pCidadeId As Integer,
                                     ByVal pTipoImovel As Integer, ByVal pTipoMaterial As Integer, _
                                     ByVal pValorDe As Integer, ByVal pValorAte As Integer, _
                                     ByVal pAreaConstruidaDe As Integer, ByVal pAreaConstruidaAte As Integer, _
                                     ByVal pQtdeDormitorio As Integer, ByVal pQtdeSuite As Integer, _
                                     ByVal pListaBairros As List(Of Bairro), ByVal pListaItens As List(Of ImovelItens)) As List(Of Imovel)

            Dim retorno As New List(Of Imovel)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                Dim criteria As ICriteria = mSession.CreateCriteria(GetType(Imovel))

                criteria.Add(Restrictions.Eq("InPublicar", True))

                If pEstadoId > 0 Then
                    criteria.Add(Restrictions.Eq("EstadoId", pEstadoId))
                End If
                If pCidadeId > 0 Then
                    criteria.Add(Restrictions.Eq("CidadeId", pCidadeId))
                End If
                If pTipoImovel > 0 Then
                    criteria.Add(Restrictions.Eq("TipoImovel", pTipoImovel))
                    DataAccess.TipoImovel.IncrementaPesquisa(pTipoImovel)
                End If
                If pTipoMaterial > 0 Then
                    criteria.Add(Restrictions.Eq("TipoMaterial", pTipoMaterial))
                End If
                If pValorDe > 0 Then
                    criteria.Add(Restrictions.Ge("Valor", pValorDe))
                End If
                If pValorAte > 0 Then
                    criteria.Add(Restrictions.Le("Valor", pValorAte))
                End If
                If pAreaConstruidaDe > 0 Then
                    criteria.Add(Restrictions.Ge("AreaConstruida", pAreaConstruidaDe))
                End If
                If pAreaConstruidaAte > 0 Then
                    criteria.Add(Restrictions.Le("AreaConstruida", pAreaConstruidaAte))
                    Estatistica.IncrementaPesquisa(pValorAte)
                End If
                If pQtdeDormitorio > 0 Then
                    criteria.Add(Restrictions.Ge("QtdeDormitorio", pQtdeDormitorio))
                End If
                If pQtdeSuite > 0 Then
                    criteria.Add(Restrictions.Ge("QtdeSuite", pQtdeSuite))
                End If

                'Verifica os itens selecionados
                For Each item As ImovelItens In pListaItens
                    criteria.Add(Restrictions.Eq(item.NomeCampo, True))
                Next
                '==============================

                'Percorre os bairros selecionados
                If pListaBairros.Count > 0 AndAlso pListaBairros(0).Id > 0 Then
                    Dim bairros(pListaBairros.Count) As String
                    For ind As Integer = 0 To pListaBairros.Count - 1
                        bairros(ind) = pListaBairros(ind).Id.ToString
                        Bairro.IncrementaPesquisa(pListaBairros(ind).Id)
                    Next
                    criteria.Add(Restrictions.In("BairroId", bairros))
                End If
                '================================

                Dim listaImovel As IList = criteria.List

                For Each item In listaImovel
                    retorno.Add(CType(item, Imovel))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.OrderBy(Function(T) T.TipoImovel).
                    ThenBy(Function(T) T.CidadeId).
                    ThenBy(Function(T) T.BairroId).ToList
            Else
                Return retorno
            End If


        End Function

    End Class
End Namespace