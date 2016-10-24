Imports NHibernate

Namespace DataAccess
    Public Class TipoMaterial
        Public Overridable Property Id As Integer
        Public Overridable Property Descricao As String

        Public Shared Function Busca() As List(Of TipoMaterial)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            Dim retorno As New List(Of TipoMaterial)

            Try
                Dim lista As IList = mSession.CreateCriteria(GetType(TipoMaterial)).List()

                For Each item In lista
                    retorno.Add(CType(item, TipoMaterial))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.OrderBy(Function(T) T.Descricao).ToList
        End Function

        Public Shared Function BuscaDescricao(ByVal pTipoMaterialId As Integer) As String
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As String

            Try
                retorno = DirectCast(mSession.Load(GetType(TipoMaterial), pTipoMaterialId), TipoMaterial).Descricao

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

    End Class
End Namespace
