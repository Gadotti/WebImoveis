Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class Foto
        Public Overridable Property Id As Integer
        Public Overridable Property ImovelId As Integer
        Public Overridable Property Sequencia As Integer
        Public Overridable Property NomeArquivo As String
        Public Overridable Property Descricao As String

        Public Overridable ReadOnly Property VisualizacaoImagem(Optional ByVal pAdmin As Boolean = False) As String
            Get
                If pAdmin Then
                    Return "../HandlerImage.ashx?foto=" & NomeArquivo & "&imovelid=" & ImovelId.ToString
                Else
                    Return "./HandlerImage.ashx?foto=" & NomeArquivo & "&imovelid=" & ImovelId.ToString
                End If
            End Get
        End Property

        Public Overridable ReadOnly Property VisualizacaoImagemDestaque As String
            Get
                Return VisualizacaoImagem & "&destaque=true"
            End Get
        End Property

        Public Overridable ReadOnly Property VisualizacaoImagemMiniatura(Optional ByVal pAdmin As Boolean = False) As String
            Get
                Return VisualizacaoImagem(pAdmin) & "&miniatura=true"
            End Get
        End Property

        Public Overridable ReadOnly Property VisualizacaoImagemMiniaturaAdmin() As String
            Get
                Return VisualizacaoImagemMiniatura(True)
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

        Public Shared Function Destaque(ByVal pImovelId As Integer) As String
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As String = String.Empty

            Try
                Dim listaFoto As IList = mSession.CreateCriteria(GetType(Foto)).
                    Add(Restrictions.Eq("ImovelId", pImovelId)).
                    AddOrder(Order.Asc("Sequencia")).
                    List()

                If listaFoto IsNot Nothing AndAlso listaFoto.Count > 0 Then
                    retorno = CType(listaFoto(0), Foto).VisualizacaoImagemDestaque
                End If

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Function Lista(ByVal pImovelId As Integer) As List(Of Foto)
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New List(Of Foto)

            Try
                Dim listaFoto As IList = mSession.CreateCriteria(GetType(Foto)).Add(Restrictions.Eq("ImovelId", pImovelId)).List()

                For Each item In listaFoto
                    retorno.Add(CType(item, Foto))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.OrderBy(Function(T) T.Sequencia).ToList
        End Function

    End Class
End Namespace
