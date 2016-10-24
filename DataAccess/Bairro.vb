Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class Bairro
        Public Overridable Property Id As Integer
        Public Overridable Property CidadeId As Integer
        Public Overridable Property Descricao As String
        Public Overridable Property QtdePesquisa As Integer

        Public Overridable ReadOnly Property CidadeDescricao As String
            Get
                Return Cidade.BuscaDescricao(CidadeId)
            End Get
        End Property

        Public Overridable ReadOnly Property EstadoId As Integer
            Get
                Return Cidade.BuscaCidade(CidadeId).EstadoId
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Sub New(ByVal pId As Integer)
            Id = pId
        End Sub

        Public Shared Function Busca(ByVal pCidadeId As Integer) As List(Of Bairro)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            Dim retorno As New List(Of Bairro)

            Try
                Dim listaBairro As IList = mSession.CreateCriteria(GetType(Bairro)).Add(Restrictions.Eq("CidadeId", pCidadeId)).List

                For Each item In listaBairro
                    retorno.Add(CType(item, Bairro))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.OrderBy(Function(T) T.Descricao).ToList
        End Function

        Public Shared Function BuscaDescricao(ByVal pBairroId As Integer) As String

            Dim retorno As String = String.Empty
            If pBairroId > 0 Then
                Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

                Try
                    retorno = DirectCast(mSession.Load(GetType(Bairro), pBairroId), Bairro).Descricao

                Finally
                    If mSession.IsOpen Then
                        mSession.Close()
                    End If
                End Try
            End If

            Return retorno
        End Function

        Public Shared Function BuscaBairro(ByVal pBairroId As Integer) As Bairro
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New Bairro

            Try
                retorno = DirectCast(mSession.Load(GetType(Bairro), pBairroId), Bairro)
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function MaisPesquisados() As List(Of Bairro)
            Dim retorno As New List(Of Bairro)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                Dim listaImovel As IList = mSession.CreateCriteria(GetType(Bairro)).
                    AddOrder(Order.Desc("QtdePesquisa")).SetMaxResults(5).List

                For Each item In listaImovel
                    retorno.Add(CType(item, Bairro))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Sub IncrementaPesquisa(ByVal pBairroId As Integer)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession

            Try
                'Mantem as estatísticas
                Dim objBairro As Bairro = BuscaBairro(pBairroId)
                objBairro.QtdePesquisa += 1

                mSession.SaveOrUpdate(objBairro)
                mSession.Flush()
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try
                  
        End Sub

        Public Overridable Sub Salvar()
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession

            Try
                'Mantem as estatísticas
                If Me.Id > 0 Then
                    Me.QtdePesquisa = BuscaBairro(Me.Id).QtdePesquisa
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

        Public Shared Function PermiteExclusao(ByVal pBairroId As Integer) As Boolean
            Dim retorno As Integer
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession

            Try
                retorno = mSession.CreateCriteria(GetType(Imovel)).Add(Restrictions.Eq("BairroId", pBairroId)).List.Count

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.Equals(0)
        End Function

    End Class
End Namespace

