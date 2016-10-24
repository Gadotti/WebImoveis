Imports System
Imports System.Collections.Generic
Imports System.Text
Imports NHibernate
Imports NHibernate.Cfg

Public Class ConnectionFactory
    Public Property objConf As Configuration
    Public Shared Property objFactory As ISessionFactory

    Public Sub New()
        Try
            'objConf = New Configuration
            'objConf.AddXmlFile("E:\Sistemas\WebImoveis\DataAccess\Imovel.hbm.xml")
            'objFactory = objConf.BuildSessionFactory

            objConf = New Configuration
            'definindo o assembly para carregar os arquivo .hbm.xml  que fazem parte do mesmo
            objConf.AddAssembly("WebImoveis")
            'definindo o dialeto do banco de dados
            objConf.SetProperty("hibernate.dialect", "NHibernate.Dialect.MySQLDialect")
            objFactory = objConf.BuildSessionFactory
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function GetConnection() As ISessionFactory
        If objFactory Is Nothing Then
            Dim objGlobal As New ConnectionFactory
        End If
        Return objFactory
    End Function

End Class
