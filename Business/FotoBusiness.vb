Imports DataAccess.DataAccess
Imports Utilitarios.Utils
Imports System.IO
Imports Utilitarios.Extensoes

Public Class FotoBusiness
    Const GaleriaFotosTD As String =
        "<td bgcolor=""#D4D0C8"" class=""texto"">" &
        "   <a href=""@Link"" @First rel=""lightbox[roadtrip]"" title=""@Descricao"">" &
        "       <img src=""@Foto"" border=""0"" width=""100""height=""80"" alt=""""/></a>" &
        "</td>"


    Public Shared Function CarregaGaleria(ByVal pImovelId As Integer, ByVal pDiretorioFotos As String) As String
        Dim objFoto As List(Of Foto) = Foto.Lista(pImovelId)

        'Verifica se existe fotos
        If objFoto Is Nothing OrElse objFoto.Count.Equals(0) Then
            Return String.Empty
        End If

        Dim retorno As New Text.StringBuilder

        'Trata cara imóvel
        Dim abreTR As Boolean = True
        Dim first As Boolean = True
        For Each item As Foto In objFoto
            Dim linhatable As String = String.Empty

            'Nova linha
            If abreTR Then
                linhatable = "<tr>"
            End If

            'Monta conteúdo
            linhatable = linhatable & GaleriaFotosTD
            linhatable = linhatable.Replace("@Link", "{0}/{1}/{2}".Fill(pDiretorioFotos, pImovelId, item.NomeArquivo)) 'item.VisualizacaoImagem)
            linhatable = linhatable.Replace("@Foto", item.VisualizacaoImagemMiniatura)
            linhatable = linhatable.Replace("@Descricao", item.Descricao)
            If first Then
                linhatable = linhatable.Replace("@First", "id=""the_first""")
                first = False
            Else
                linhatable = linhatable.Replace("@First", String.Empty)
            End If

            'Fecha linha
            If Not abreTR Then
                linhatable = linhatable & "</tr>"
            End If

            'Troca valor indicador
            abreTR = Not abreTR

            'Armazena conteúdo
            retorno.Append(linhatable)
        Next

        'Verifica se é qtde impar para fechar o tr
        If objFoto.Count Mod 2 > 0 Then
            retorno.Append("<td></td></tr>")
        End If

        'Retorna resultado
        Return retorno.ToString

    End Function

    Public Shared Sub GravaFoto(ByVal pArquivo As System.Web.HttpPostedFile, ByVal pDiretorio As String, ByVal pFileName As String)
        Dim imagemDestrino As String = Path.Combine(pDiretorio, pFileName)

        Dim maxWidth As Integer = 600
        Dim maxHeight As Integer = 500

        Dim objStream As Stream = pArquivo.InputStream
        Dim objBinaryReader As BinaryReader = New BinaryReader(objStream)
        Dim arrBytes() As Byte = objBinaryReader.ReadBytes(objStream.Length)

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

        'Tratapoção e limite de tamanho das imagens
        If imageBitmap.Height < imageBitmap.Width Then
            'Então é formato paisagem
            If imageBitmap.Width > maxWidth Then
                maxHeight = Convert.ToInt32(imageBitmap.Height * maxWidth / imageBitmap.Width)
            Else
                maxWidth = imageBitmap.Width
                maxHeight = imageBitmap.Height
            End If
        Else
            'Senão é retrato
            If imageBitmap.Height > maxHeight Then
                maxWidth = Convert.ToInt32(imageBitmap.Width * maxWidth / imageBitmap.Height)
            Else
                maxWidth = imageBitmap.Width
                maxHeight = imageBitmap.Height
            End If
        End If
        '=========================================

        ' Atribui o tamanho à nova imagem.
        Dim imageModificada As System.Drawing.Bitmap = New System.Drawing.Bitmap(maxWidth, maxHeight)

        ' Define o desenho da nova imagem.
        Dim graphic As Drawing.Graphics = Drawing.Graphics.FromImage(imageModificada)
        graphic.DrawImage(imageBitmap, New System.Drawing.Rectangle(0, 0, imageModificada.Width, imageModificada.Height), 0, 0, imageBitmap.Width, imageBitmap.Height, System.Drawing.GraphicsUnit.Pixel)
        graphic.Dispose()
        imageBitmap.Dispose()

        ' Salva a nova imagem no objeto de mem�ria.
        'Dim objMemoryStreamModificado As MemoryStream = New MemoryStream
        'imageModificada.Save(objMemoryStreamModificado, System.Drawing.Imaging.ImageFormat.Bmp)
        'objMemoryStreamModificado.Dispose()

        'Salva a imagem no local desejado
        imageModificada.Save(imagemDestrino, Drawing.Imaging.ImageFormat.Jpeg)
        imageModificada.Dispose()

    End Sub
End Class
