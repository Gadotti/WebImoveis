Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class TipoImovelEQV
        Public Overridable Property Id As Integer
        Public Overridable Property SistemaId As Integer
        Public Overridable Property TipoImovelId As Integer
        Public Overridable Property TipoImovelEQV As String
        Public Overridable Property TipoPesquisaEQV As String

        Public Shared Function Busca(ByVal pTipoImovelId As Integer) As TipoImovelEQV
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New TipoImovelEQV

            Try
                Dim lista As IList = mSession.CreateCriteria(GetType(TipoImovelEQV)).Add(
                        Restrictions.Eq("TipoImovelId", pTipoImovelId)).Add(
                        Restrictions.Eq("SistemaId", 1)).List

                If lista IsNot Nothing Then
                    retorno = DirectCast(lista(0), TipoImovelEQV)
                End If

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function


    End Class
End Namespace
