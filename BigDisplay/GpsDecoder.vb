Public Class GpsDecoder
    Public Shared Function ToSignedDecimal(ByVal value As String, ByVal unit As String) As String
        dim ddmmss As Double
        If(Not Double.TryParse(value,ddmmss)) Then
            Return "ERROR"
        End If
        unit = unit.ToUpper
        If Not((unit = "N") Or (unit = "S") Or (unit = "E") Or (unit = "W")) Then
            Return "ERROR"
        End If

        ddmmss = (ddmmss / 100)

        Dim degrees As Integer = CInt(ddmmss)

        Dim minutesseconds As Double = ((ddmmss - degrees) * 100) / 60.0

        Dim val As double
        'south and west are negative
        if (unit = "S" or unit = "W") then
            val = (degrees + minutesseconds) * -1
            val = Math.Round(val, 4, MidpointRounding.AwayFromZero)
            return val.ToString("00.0000") & Chr(176) & unit
        End If

        val = degrees + minutesseconds
        val = Math.Round(val, 4, MidpointRounding.AwayFromZero)
        Return val.ToString("00.0000") & Chr(176) & unit
    End Function

    Public Shared Function ToDegMinSec(ByVal value As String, ByVal unit As String) As String
        dim ddmmss As Double
        If(Not Double.TryParse(value,ddmmss)) Then
            Return "ERROR"
        End If
        unit = unit.ToUpper
        If Not((unit = "N") Or (unit = "S") Or (unit = "E") Or (unit = "W")) Then
            Return "ERROR"
        End If

        Dim dd,mm,ss as string

        if (unit = "E" or unit = "W") then
            dd = Left(value,3)
            mm = Mid(value,4,2)
        Else 
            dd = Left(value,2)
            mm = Mid(value,3,2)
        End If

        Dim decimalIndex = value.IndexOf(".")
        ss = Right(value, value.Length - (decimalIndex))

        Dim secVal As double = Double.Parse(ss) * 60

        Return dd & Chr(176) & " " & mm & "'" & " " & secVal.ToString("00.00") & "''" & unit
    End Function
End Class
