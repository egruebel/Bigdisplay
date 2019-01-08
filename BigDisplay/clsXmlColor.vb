Imports System.Xml.Serialization

Public Class XmlColor
    
    Private _color As Color = Color.Black
    
    Public Sub New()
        MyBase.New
    End Sub
    
    Public Sub New(ByVal c As Color)
        MyBase.New
        Me._color = c
    End Sub
    
    Public Function ToColor() As Color
        Return Me._color
    End Function
    
    Public Sub FromColor(ByVal c As Color)
        Me._color = c
    End Sub
    
    Public Overloads Shared Widening Operator CType(ByVal x As XmlColor) As Color
        Return x.ToColor
    End Operator
    
    Public Overloads Shared Narrowing Operator CType(ByVal c As Color) As XmlColor
        Return New XmlColor(c)
    End Operator
    
    <XmlAttribute()>  _
    Public Property Web As String
        Get
            Return ColorTranslator.ToHtml(Me._color)
        End Get
        Set
            Try 
                If (Alpha = 255) Then
                    Me._color = ColorTranslator.FromHtml(value)
                Else
                    Me._color = Color.FromArgb(Alpha, ColorTranslator.FromHtml(value))
                End If
                
            Catch  ex As Exception
                Me._color = Color.Black
            End Try
            
        End Set
    End Property
    
    <XmlAttribute()>  _
    Public Property Alpha As Byte
        Get
            Return Me._color.A
        End Get
        Set
            If (value <> Me._color.A) Then
                Me._color = Color.FromArgb(value, Me._color)
            End If
            
        End Set
    End Property
    
    Public Function ShouldSerializeAlpha() As Boolean
        Return (Me.Alpha < 255)
    End Function
End Class
