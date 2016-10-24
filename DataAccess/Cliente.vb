Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class Cliente
        Public Overridable Property Id As Integer
        Public Overridable Property Nome As String
        Public Overridable Property Sobrenome As String
        Public Overridable Property CpfCnpj As String
        Public Overridable Property TelefoneResidencial As String
        Public Overridable Property TelefoneComercial As String
        Public Overridable Property TelefoneCelular As String
        Public Overridable Property Email As String
        Public Overridable Property Observacoes As String

        Public Overridable ReadOnly Property Telefones As String
            Get
                Return TelefoneCelular &
                    IIf(String.IsNullOrEmpty(TelefoneResidencial), "", " / " & TelefoneResidencial).ToString &
                    IIf(String.IsNullOrEmpty(TelefoneComercial), "", " / " & TelefoneComercial).ToString
            End Get
        End Property

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

        Public Shared Function Busca(ByVal pClienteId As Integer) As Cliente
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New Cliente

            Try
                retorno = DirectCast(mSession.Load(GetType(Cliente), pClienteId), Cliente)

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function Busca() As List(Of Cliente)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New List(Of Cliente)

            Try
                Dim listaCliente As IList = mSession.CreateCriteria(GetType(Cliente)).List()

                For Each item In listaCliente
                    retorno.Add(CType(item, Cliente))
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

