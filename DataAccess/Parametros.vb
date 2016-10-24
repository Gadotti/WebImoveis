Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class Parametros
        Public Overridable Property Id As Integer
        Public Overridable Property Email As String
        Public Overridable Property EmailSenha As String
        Public Overridable Property SmtpHost As String
        Public Overridable Property SmtpPorta As Integer
        Public Overridable Property Titulo As String
        Public Overridable Property Contato As String


        Public Shared Function Busca() As Parametros
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New Parametros

            Try
                retorno = DirectCast(mSession.Load(GetType(Parametros), 1), Parametros)

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
    End Class
End Namespace
