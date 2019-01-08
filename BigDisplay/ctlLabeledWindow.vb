Imports System.Drawing

Public Class ctlLabeledWindow

    Private _LabelFontSizeMult As Single = 1.0!
    Private _WindowFontSizeMult As Single = 1.0!
    Private _LabelLocation As LabelLocations
    Private _WindowPercent As Single = 0.5

    Private cLabel As String = "Label"
    Private cWindow As String = "Window"

    Private DropCase As Char() = New Char() {"g"c, "j"c, "p"c, "q"c, "y"c, "_"c}


    Public Enum LabelLocations
        Top = 1
        Right = 2
        Bottom = 3
        Left = 4
    End Enum

    Public Sub New(ByRef LabelLocation As LabelLocations)

        Me.InitializeComponent()
        _LabelLocation = LabelLocation

    End Sub

    Private Sub lbl_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles lblWindow.SizeChanged, lblLabel.SizeChanged


        ' Dim PixelFudgeFactor As Integer = 3
        ' Dim MinFontSize As Integer = 9  'Can't read smaller than this

        Dim lbl As Label = CType(sender, Label)
        With lbl
            Dim LineCount As Integer = SubStringCount(vbLf, lbl.Text) + 1
            Dim NewFontSize As Single = (.Font.Size / .PreferredHeight) * .Height
            'If .Text.IndexOfAny(DropCase) > -1 Then
            '    NewFontSize = NewFontSize * 0.95!
            'Else
            '    NewFontSize = NewFontSize * 1.02!

            'End If

            If lbl.Tag Is cLabel Then
                NewFontSize = (.ClientRectangle.Height * _LabelFontSizeMult) / LineCount
            Else
                NewFontSize = (.ClientRectangle.Height * _WindowFontSizeMult) / LineCount
            End If

            If NewFontSize > 0 Then
                .Font = New Font(.Font.Name, NewFontSize, .Font.Style, GraphicsUnit.Pixel)
            End If
        End With



    End Sub

    Private Function SubStringCount(ByVal SubString As String, ByVal SourceString As String) As Integer
        Dim count As Integer = -1
        Dim lastIndex As Integer = -1

        Do
            lastIndex = SourceString.IndexOf(SubString, lastIndex + 1)
            count += 1
        Loop Until lastIndex < 0

        Return count

    End Function

    Private Sub ctlLabeledWindow_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        'Debug.Print("SIze")
        FitControls()
    End Sub
    Private Sub FitControls()

        Dim RoundUp As Integer
        Dim RoundDn As Integer

        Select Case _LabelLocation
            Case LabelLocations.Top
                RoundUp = CInt(Math.Ceiling(Me.ClientRectangle.Height * _WindowPercent))
                RoundDn = CInt(Math.Floor(Me.ClientRectangle.Height * (1 - _WindowPercent)))
                lblLabel.Bounds = New Rectangle(0, 0, Me.ClientRectangle.Width, RoundDn)
                lblWindow.Bounds = New Rectangle(0, RoundDn, Me.ClientRectangle.Width, RoundUp)

            Case LabelLocations.Right
                RoundUp = CInt(Math.Ceiling(Me.ClientRectangle.Width / 2))
                RoundDn = CInt(Math.Floor(Me.ClientRectangle.Width / 2))
                lblLabel.Bounds = New Rectangle(RoundUp, 0, RoundDn, Me.ClientRectangle.Height)
                lblWindow.Bounds = New Rectangle(0, 0, RoundUp, Me.ClientRectangle.Height)

            Case LabelLocations.Bottom
                RoundUp = CInt(Math.Ceiling(Me.ClientRectangle.Height / 2))
                RoundDn = CInt(Math.Floor(Me.ClientRectangle.Height / 2))
                lblLabel.Bounds = New Rectangle(0, RoundUp, Me.ClientRectangle.Width, RoundDn)
                lblWindow.Bounds = New Rectangle(0, 0, Me.ClientRectangle.Width, RoundUp)

            Case LabelLocations.Left
                RoundUp = CInt(Math.Ceiling(Me.ClientRectangle.Width / 2))
                RoundDn = CInt(Math.Floor(Me.ClientRectangle.Width / 2))
                lblLabel.Bounds = New Rectangle(0, 0, RoundDn, Me.ClientRectangle.Height)
                lblWindow.Bounds = New Rectangle(RoundDn, 0, RoundUp, Me.ClientRectangle.Height)

        End Select
    End Sub


    Private Sub ctlLabeledWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.BackColor = Color.Transparent
        lblLabel.BackColor = Color.Transparent
        lblWindow.BackColor = Color.Transparent

        lblLabel.Tag = cLabel
        lblWindow.Tag = cWindow

        lblLabel.Size = lblWindow.Size

        Select Case _LabelLocation
            Case LabelLocations.Top
                lblLabel.TextAlign = ContentAlignment.TopCenter
                lblWindow.TextAlign = ContentAlignment.MiddleCenter

            Case LabelLocations.Right
                lblLabel.TextAlign = ContentAlignment.TopLeft
                lblWindow.TextAlign = ContentAlignment.TopRight

            Case LabelLocations.Bottom
                lblLabel.TextAlign = ContentAlignment.TopCenter
                lblWindow.TextAlign = ContentAlignment.TopCenter

            Case LabelLocations.Left
                lblLabel.TextAlign = ContentAlignment.TopRight
                lblWindow.TextAlign = ContentAlignment.TopLeft
        End Select

    End Sub
    Public Property LabelFontSizeMult() As Single
        Get
            Return _LabelFontSizeMult
        End Get
        Set(ByVal value As Single)
            _LabelFontSizeMult = value
        End Set
    End Property

    Public Property WindowFontSizeMult() As Single
        Get
            Return _WindowFontSizeMult
        End Get
        Set(ByVal value As Single)
            _WindowFontSizeMult = value
        End Set
    End Property
    Public Overrides Property Text() As String
        Get
            Return lblWindow.Text
        End Get
        Set(ByVal value As String)
            lblWindow.Text = value
        End Set
    End Property
    Public Property WindowPercent() As Single
        Get
            Return _WindowPercent
        End Get
        Set(ByVal value As Single)
            _WindowPercent = value
        End Set
    End Property

    Public Property LabelText() As String
        Get
            Return lblLabel.Text
        End Get
        Set(ByVal value As String)
            lblLabel.Text = value
        End Set
    End Property

    Public Property WindowText() As String
        Get
            Return lblWindow.Text
        End Get
        Set(ByVal value As String)
            lblWindow.Text = value
        End Set
    End Property

    Public Property LabelBorderStyle() As BorderStyle
        Get
            Return lblLabel.BorderStyle
        End Get
        Set(ByVal value As BorderStyle)
            lblLabel.BorderStyle = value
        End Set
    End Property

    Public Property WindowBorderStyle() As BorderStyle
        Get
            Return lblWindow.BorderStyle
        End Get
        Set(ByVal value As BorderStyle)
            lblWindow.BorderStyle = value
        End Set
    End Property

    Public Property LabelFont() As Font
        Get
            Return lblLabel.Font
        End Get
        Set(ByVal value As Font)
            lblLabel.Font = value
        End Set
    End Property

    Public Property WindowFont() As Font
        Get
            Return lblWindow.Font
        End Get
        Set(ByVal value As Font)
            lblWindow.Font = value
        End Set
    End Property

    Public Property LabelBackColor() As Color
        Get
            Return lblLabel.BackColor
        End Get
        Set(ByVal value As Color)
            lblLabel.BackColor = value
        End Set
    End Property

    Public Property WindowBackColor() As Color
        Get
            Return lblWindow.BackColor
        End Get
        Set(ByVal value As Color)
            lblWindow.BackColor = value
        End Set
    End Property

    Public Property LabelForeColor() As Color
        Get
            Return lblLabel.ForeColor
        End Get
        Set(ByVal value As Color)
            lblLabel.ForeColor = value
        End Set
    End Property

    Public Property WindowForeColor() As Color
        Get
            Return lblWindow.ForeColor
        End Get
        Set(ByVal value As Color)
            lblWindow.ForeColor = value
        End Set
    End Property

End Class
