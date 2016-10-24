Imports NHibernate
Imports NHibernate.Criterion

Namespace DataAccess
    Public Class Estatistica

        Public Enum Campos
            Ate100 = 1
            Ate150 = 2
            Ate250 = 3
            Ate400 = 4
            Acima400 = 0
        End Enum

        Public Overridable Property Id As Integer
        Public Overridable Property Descricao As String
        Public Overridable Property QtdePesquisa As Integer

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

        Public Shared Function Busca(ByVal pEstatisticaId As Integer) As Estatistica
            Dim mSession As ISession = ConnectionFactory.GetConnection.OpenSession
            Dim retorno As New Estatistica

            Try
                retorno = DirectCast(mSession.Load(GetType(Estatistica), pEstatisticaId), Estatistica)

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno
        End Function

        Public Shared Sub IncrementaPesquisa(ByVal pValor As Integer)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession

            Try
                Dim objEstatistica As Estatistica = Nothing
                Select Case pValor
                    Case Is <= 10000000 '100.000,00
                        objEstatistica = Busca(Campos.Ate100)
                    Case Is <= 15000000 '150.000,00
                        objEstatistica = Busca(Campos.Ate150)
                    Case Is <= 25000000 '250.000,00
                        objEstatistica = Busca(Campos.Ate250)
                    Case Is <= 40000000 '400.000,00
                        objEstatistica = Busca(Campos.Ate400)
                    Case Is > 40000000 '400.000,00
                        objEstatistica = Busca(Campos.Acima400)
                End Select

                If objEstatistica IsNot Nothing Then
                    objEstatistica.QtdePesquisa += 1
                    mSession.SaveOrUpdate(objEstatistica)
                    mSession.Flush()
                End If

            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

        End Sub

        Public Shared Function Busca() As List(Of Estatistica)
            Dim mFactory As ISessionFactory = ConnectionFactory.GetConnection
            Dim mSession As ISession = mFactory.OpenSession
            Dim retorno As New List(Of Estatistica)

            Try
                Dim lista As IList = mSession.CreateCriteria(GetType(Estatistica)).List()

                For Each item In lista
                    retorno.Add(CType(item, Estatistica))
                Next
            Finally
                If mSession.IsOpen Then
                    mSession.Close()
                End If
            End Try

            Return retorno.OrderBy(Function(T) T.Id).ToList
        End Function

    End Class
End Namespace
