Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class TipoImovel

        Public Enum enumTipoImovel
            Apartamento = 1
            Casa = 2
            Terreno = 3
            Sitio = 4
            PredioComercial = 5
            Fazenda = 6
            Chacara = 7
        End Enum

        Public Overridable Property Id As Integer
        Public Overridable Property Descricao As String
        Public Overridable Property QtdePesquisa As Integer

        Public Shared Function Busca() As List(Of TipoImovel)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            Dim retorno As New List(Of TipoImovel)

            Try
                Dim lista As IList = mSession.CreateCriteria(GetType(TipoImovel)).List()

                For Each item In lista
                    retorno.Add(CType(item, TipoImovel))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.OrderBy(Function(T) T.Descricao).ToList
        End Function

        Public Shared Function BuscaDescricao(ByVal pTipoImovelId As Integer) As String
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As String

            Try
                retorno = DirectCast(mSession.Load(GetType(TipoImovel), pTipoImovelId), TipoImovel).Descricao

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function Busca(ByVal pTipoImovelId As Integer) As TipoImovel
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New TipoImovel

            Try
                retorno = DirectCast(mSession.Load(GetType(TipoImovel), pTipoImovelId), TipoImovel)

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Sub IncrementaPesquisa(ByVal pTipoImovelId As Integer)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession

            Try
                'Mantem as estatísticas
                Dim objTipoImovel As TipoImovel = Busca(pTipoImovelId)
                objTipoImovel.QtdePesquisa += 1

                mSession.SaveOrUpdate(objTipoImovel)
                mSession.Flush()
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

        End Sub

        Public Shared Function MaisPesquisados() As List(Of TipoImovel)
            Dim retorno As New List(Of TipoImovel)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                Dim listaImovel As IList = mSession.CreateCriteria(GetType(TipoImovel)).
                    AddOrder(Order.Desc("QtdePesquisa")).SetMaxResults(5).List

                For Each item In listaImovel
                    retorno.Add(CType(item, TipoImovel))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Overridable Sub Salvar()
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession

            Try
                'Mantem as estatísticas
                If Me.Id > 0 Then
                    Me.QtdePesquisa = Busca(Me.Id).QtdePesquisa
                End If

                mSession.SaveOrUpdate(Me)
                mSession.Flush()
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

        End Sub

        Public Overridable Sub Excluir()
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                mSession.Delete(Me)
                mSession.Flush()
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try
        End Sub

    End Class
End Namespace
