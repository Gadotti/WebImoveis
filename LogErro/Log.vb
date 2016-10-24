Public Class Log
    Public Shared Sub Erro(ByVal pErro As System.Exception)
        Elmah.ErrorLog.GetDefault(Nothing).Log(New Elmah.Error(pErro))
    End Sub
End Class
