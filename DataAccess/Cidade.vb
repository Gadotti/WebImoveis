Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class Cidade
        Public Overridable Property Id As Integer
        Public Overridable Property EstadoId As Integer
        Public Overridable Property Descricao As String

        Public Overridable ReadOnly Property EstadoSigla As String
            Get
                Return Estado.BuscaSigla(EstadoId)
            End Get
        End Property

        Public Shared Function Busca(ByVal pEstadoId As Integer) As List(Of Cidade)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            Dim retorno As New List(Of Cidade)

            Try
                Dim listaCidade As IList = mSession.CreateCriteria(GetType(Cidade)).Add(Restrictions.Eq("EstadoId", pEstadoId)).List()

                For Each item In listaCidade
                    retorno.Add(CType(item, Cidade))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.OrderBy(Function(T) T.Descricao).ToList
        End Function

        Public Shared Function BuscaDescricao(ByVal pCidadeId As Integer) As String
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As String

            Try
                retorno = DirectCast(mSession.Load(GetType(Cidade), pCidadeId), Cidade).Descricao

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function BuscaCidade(ByVal pCidadeId As Integer) As Cidade
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New Cidade

            Try
                retorno = DirectCast(mSession.Load(GetType(Cidade), pCidadeId), Cidade)

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

        Public Shared Function PermiteExclusao(ByVal pCidadeId As Integer) As Boolean
            Dim retorno As Integer
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                retorno = mSession.CreateCriteria(GetType(Imovel)).Add(Restrictions.Eq("CidadeId", pCidadeId)).List.Count

                If retorno.Equals(0) Then
                    retorno = mSession.CreateCriteria(GetType(Bairro)).Add(Restrictions.Eq("CidadeId", pCidadeId)).List.Count
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

