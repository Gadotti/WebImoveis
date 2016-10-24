Imports NHibernate

Namespace DataAccess
    Public Class TipoNegocio
        Public Overridable Property Id As Integer
        Public Overridable Property Descricao As String

        Public Shared Function Busca() As List(Of TipoNegocio)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            Dim retorno As New List(Of TipoNegocio)

            Try
                Dim lista As IList = mSession.CreateCriteria(GetType(TipoNegocio)).List()

                For Each item In lista
                    retorno.Add(CType(item, TipoNegocio))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.OrderBy(Function(T) T.Descricao).ToList
        End Function

        Public Shared Function BuscaDescricao(ByVal pTipoNegocioId As Integer) As String
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As String

            Try
                retorno = DirectCast(mSession.Load(GetType(TipoNegocio), pTipoNegocioId), TipoNegocio).Descricao

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

    End Class
End Namespace
