Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class Usuario
        Public Overridable Property Id As Integer
        Public Overridable Property Login As String
        Public Overridable Property Senha As String
        Public Overridable Property Nome As String
        Public Overridable Property Email As String
        Public Overridable Property DataCadastro As Date

        Public Shared Function AutenticaUsuario(ByVal pLogin As String, ByVal pSenha As String) As Usuario
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            Dim retorno As Usuario = Nothing

            Try
                Dim listaUsuario As IList = mSession.CreateCriteria(GetType(Usuario)).
                    Add(Restrictions.Eq("Login", pLogin.ToLower.Trim)).
                    Add(Restrictions.Eq("Senha", pSenha.ToLower.Trim)).List

                If listaUsuario IsNot Nothing AndAlso listaUsuario.Count > 0 Then
                    retorno = CType(listaUsuario(0), Usuario)
                End If

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

        Public Shared Function Busca(ByVal pUsuarioId As Integer) As Usuario
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New Usuario

            Try
                retorno = DirectCast(mSession.Load(GetType(Usuario), pUsuarioId), Usuario)

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function Busca() As List(Of Usuario)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New List(Of Usuario)

            Try
                Dim lista As IList = mSession.CreateCriteria(GetType(Usuario)).List()

                For Each item In lista
                    retorno.Add(CType(item, Usuario))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.OrderBy(Function(T) T.Nome).ToList
        End Function

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