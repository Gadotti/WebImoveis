Imports System.Web
Imports System.Web.Services
Imports Utilitarios.Extensoes
Imports System.IO

Public Class ExportarImovel
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim imovelId As Integer = context.Request.QueryString("imovelid").ToInt

        'Instancia o objeto que irá gerar as informações conforme o layout
        Dim objImoveisSCBussiness As New Business.ImoveisSCBusiness(imovelId, context.Request.Url.Scheme & Uri.SchemeDelimiter & context.Request.Url.Authority)

        Dim ms As New MemoryStream(Encoding.UTF8.GetBytes(objImoveisSCBussiness.Exportar))

        context.Response.Charset = "iso-8859-1"
        context.Response.ContentType = "application/octet-stream"
        context.Response.AddHeader("Content-Disposition:", "attachment; filename=Imovel{0}-ImoveisSC.txt".Fill(imovelId))

        ms.WriteTo(context.Response.OutputStream)
        ms.Close()
        
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class