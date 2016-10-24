Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class Estado
        Public Overridable Property Id As Integer
        Public Overridable Property Descricao As String
        Public Overridable Property Sigla As String

        Public Shared Function Busca() As List(Of Estado)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            Dim retorno As New List(Of Estado)

            Try
                Dim listaEstados As IList = mSession.CreateCriteria(GetType(Estado)).List()

                For Each item In listaEstados
                    retorno.Add(CType(item, Estado))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.OrderBy(Function(T) T.Descricao).ToList
        End Function

        Public Shared Function BuscaSigla(ByVal pEstadoId As Integer) As String
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As String

            Try
                retorno = DirectCast(mSession.Load(GetType(Estado), pEstadoId), Estado).Sigla

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function Busca(ByVal pEstadoId As Integer) As Estado
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New Estado

            Try
                retorno = DirectCast(mSession.Load(GetType(Estado), pEstadoId), Estado)

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

        Public Shared Function PermiteExclusao(ByVal pEstadoId As Integer) As Boolean
            Dim retorno As Integer
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                retorno = mSession.CreateCriteria(GetType(Imovel)).Add(Restrictions.Eq("EstadoId", pEstadoId)).List.Count

                If retorno.Equals(0) Then
                    retorno = mSession.CreateCriteria(GetType(Cidade)).Add(Restrictions.Eq("EstadoId", pEstadoId)).List.Count
                End If

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.Equals(0)
        End Function

    End Class
End Namespace
