Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Drawing

Public Class HandlerImage
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal pObjContext As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim wrkPathImg As String = pObjContext.Request.QueryString("imovelid").ToString & "\" & pObjContext.Request.QueryString("foto").ToString
        Dim strSrcImagemOriginal As String = Path.Combine(My.Settings.DiretorioFotos, wrkPathImg)

        'Verifica se é para gerar destaque
        Dim destaque As Boolean = False
        If pObjContext.Request.QueryString("destaque") IsNot Nothing Then
            destaque = pObjContext.Request.QueryString("destaque").ToString.ToLower.Equals("true")
        End If
        '=================================

        'Verifica se é para gerar miniatura
        Dim miniatura As Boolean = False
        If pObjContext.Request.QueryString("miniatura") IsNot Nothing Then
            miniatura = pObjContext.Request.QueryString("miniatura").ToString.ToLower.Equals("true")
        End If
        '=================================

        Dim maxWidth As Integer = 600
        Dim maxHeight As Integer = 450

        Dim strContentType As String = "image/jpeg"

        Dim objStream As Stream = (New StreamReader(strSrcImagemOriginal)).BaseStream
        Dim objBinaryReader As BinaryReader = New BinaryReader(objStream)
        Dim arrBytes() As Byte = objBinaryReader.ReadBytes(CType(objStream.Length, Integer))

        objStream.Dispose()
        objStream.Close()
        objBinaryReader.Dispose()
        objBinaryReader.Close()

        ' Le os bytes da imagem original.
        Dim objMemoryStream As MemoryStream = New MemoryStream(arrBytes)

        ' Monta uma nova imagem.
        Dim imageBitmap As System.Drawing.Bitmap = CType(System.Drawing.Image.FromStream(objMemoryStream), System.Drawing.Bitmap)
        objMemoryStream.Dispose()
        objMemoryStream.Close()

        If miniatura Then
            maxWidth = 100
            maxHeight = 80
        ElseIf destaque Then
            maxWidth = 262
            maxHeight = 216
        Else
            maxWidth = imageBitmap.Width
            maxHeight = imageBitmap.Height
        End If

        ' Atribui o tamanho à nova imagem.
        Dim imageModificada As System.Drawing.Bitmap = New System.Drawing.Bitmap(maxWidth, maxHeight)

        ' Define o desenho da nova imagem.
        Dim graphic As Graphics = Graphics.FromImage(imageModificada)
        graphic.DrawImage(imageBitmap, New System.Drawing.Rectangle(0, 0, imageModificada.Width, imageModificada.Height), 0, 0, imageBitmap.Width, imageBitmap.Height, System.Drawing.GraphicsUnit.Pixel)
        graphic.Dispose()
        imageBitmap.Dispose()

        Dim objMemoryStreamModificado As MemoryStream = New MemoryStream

        ' Salva a nova imagem no objeto de mem�ria.
        imageModificada.Save(objMemoryStreamModificado, System.Drawing.Imaging.ImageFormat.Bmp)
        imageModificada.Dispose()

        ' Recupera os bytes da imagem modificada.
        arrBytes = objMemoryStreamModificado.GetBuffer
        objMemoryStreamModificado.Dispose()
        objMemoryStreamModificado.Close()

        ' Monta a imagem.
        pObjContext.Response.ContentType = strContentType
        pObjContext.Response.BinaryWrite(arrBytes)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
End Class
