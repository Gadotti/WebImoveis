Imports Utilitarios.Extensoes
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

Public Class Utils
    Public Shared Function FormataValor(ByVal pValor As Object) As String
        Dim valor As String = pValor.ToString.DeixaSoNumero
        If valor.Length.Equals(0) Then
            valor = "0,00"
        ElseIf (valor.Length < 3) Then
            valor = valor + ",00"
        Else
            Dim tam As Integer = valor.Length
            If (tam >= 3 AndAlso tam < 6) Then
                valor = valor.Substring(0, tam - 2) + "," + valor.Substring(tam - 2)
            ElseIf (tam >= 6 AndAlso tam < 9) Then
                valor = valor.Substring(0, tam - 5) + "." + valor.Substring(tam - 5, 3) + "," + valor.Substring(tam - 2)
            ElseIf (tam >= 9 AndAlso tam < 12) Then
                valor = valor.Substring(0, tam - 8) + "." + valor.Substring(tam - 8, 3) + "." + valor.Substring(tam - 5, 3) + "," + valor.Substring(tam - 2)
            ElseIf (tam >= 12 AndAlso tam < 15) Then
                valor = valor.Substring(0, tam - 11) + "." + valor.Substring(tam - 11, 3) + "." + valor.Substring(tam - 8, 3) + "." + valor.Substring(tam - 5, 3) + "," + valor.Substring(tam - 2)
            End If
        End If

        Return valor
    End Function

    Public Shared Sub Mail(ByVal para As String, ByVal assunto As String, ByVal mensagem As String,
                           ByVal remetente As String, ByVal senha As String, ByVal smtphost As String,
                           ByVal smtpporta As Integer)
        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        SmtpServer.Credentials = New Net.NetworkCredential(remetente, senha)
        SmtpServer.Port = smtpporta
        SmtpServer.Host = smtphost
        mail = New MailMessage()
        mail.From = New MailAddress(remetente)

        mail.To.Add(para)
        mail.Subject = assunto
        mail.Body = mensagem
        SmtpServer.Send(mail)
    End Sub


    Public Shared Function Criptografa(ByVal valor As String) As String
        Dim memory As MemoryStream = Nothing

        Try
            Using csp As New DESCryptoServiceProvider()

                'Chave de 8 bits
                csp.Key = Encoding.UTF8.GetBytes("KW9N4J5G")
                csp.IV = Encoding.UTF8.GetBytes("93MF8SKR")

                memory = New MemoryStream()

                Using crypto As New CryptoStream(memory, csp.CreateEncryptor(), CryptoStreamMode.Write)
                    Using reader As New StreamWriter(crypto)
                        'Criptografa
                        reader.WriteLine(valor)
                    End Using
                End Using
            End Using

            Return Encoding.Default.GetString(memory.ToArray())
        Finally
            If Not memory Is Nothing Then
                memory.Close()
            End If
        End Try
    End Function

    Public Shared Function Decriptografa(ByVal valor As String) As String
        Dim buffer() As Byte = Nothing
        Dim memory As MemoryStream = Nothing

        Try
            Using csp As New DESCryptoServiceProvider()

                'Chave de 8 bits
                csp.Key = Encoding.UTF8.GetBytes("KW9N4J5G")
                csp.IV = Encoding.UTF8.GetBytes("93MF8SKR")

                memory = New MemoryStream(Encoding.Default.GetBytes(valor))

                Using crypto As New CryptoStream(memory, csp.CreateDecryptor(), CryptoStreamMode.Read)
                    Using reader As New StreamReader(crypto)
                        'Criptografa
                        valor = reader.ReadToEnd()
                    End Using
                End Using
            End Using

            Return valor
        Finally
            If Not memory Is Nothing Then
                memory.Close()
            End If
        End Try
    End Function

End Class
