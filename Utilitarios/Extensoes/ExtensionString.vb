Imports System.Text.RegularExpressions

Namespace Extensoes
    Public Module ExtensionString

        <System.Runtime.CompilerServices.Extension()> _
        Public Function DeixaSoNumero(ByVal texto As String, Optional ByVal valorDefault As String = "0") As String
            Dim retorno As String = Regex.Replace(texto, "\D", "")
            If retorno.Length.Equals(0) Then
                Return valorDefault
            Else
                Return retorno
            End If
        End Function

        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToInt(ByVal texto As String) As Integer
            Return Convert.ToInt32(texto.DeixaSoNumero)
        End Function

        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToInt64(ByVal texto As String) As Int64
            Return Convert.ToInt64(texto.DeixaSoNumero)
        End Function

        <System.Runtime.CompilerServices.Extension()> _
        Public Function Fill(ByVal texto As String, ByVal ParamArray parametros As Object()) As String
            Return String.Format(texto, parametros)
        End Function

        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToAspasPontoVirgula(ByVal texto As String) As String
            Return """" & texto & """" & ";"
        End Function

    End Module
End Namespace