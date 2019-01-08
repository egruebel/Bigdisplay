Imports System.Drawing.Drawing2D
Imports System.Threading
Imports System.Threading.Tasks

Public Class clsCompassRose

    Private Const PiBy180 As Single = Math.PI / 180.0!

    Private _Origin As PointF
    Private _Radius As Single
    Private _SmallTicsAngle As Single = 10.0!
    Private _MediumTicsAngle As Single = 30.0!
    Private _BigTicsAngle As Single = 90.0!

    Private PenCircle As New Pen(Color.Black, 7)
    Private PenBigTics As New Pen(Color.Black, 7)
    Private PenMediumTics As New Pen(Color.Black, 4)
    Private PenSmallTics As New Pen(Color.Black, 2)

    Private gpCircle As New GraphicsPath
    Private gpBigTics As New GraphicsPath
    Private gpMediumTics As New GraphicsPath
    Private gpSmallTics As New GraphicsPath

    Private TransformMatrix As New Matrix

    


    Friend Sub New()

    End Sub

    Public Overridable Sub Update()

        ' Raise this event after any change of size, position or tick spacing/size
        ' No need to raise this event after a change to a figure's Pen

        ' Create figures as GraphicsPaths
        DrawCircle()
        DrawSmallTics()
        DrawMediumTics()
        DrawBigTics()
 

        'Move the the GPs to the Origin
        TransformMatrix.Reset()
        TransformMatrix.Translate(_Origin.X, _Origin.Y)
        gpSmallTics.Transform(TransformMatrix)
        gpMediumTics.Transform(TransformMatrix)
        gpBigTics.Transform(TransformMatrix)
        gpCircle.Transform(TransformMatrix)

    End Sub

    Friend Overridable Sub Draw(ByVal g As Graphics)

        'Show 'em
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.DrawPath(PenCircle, gpCircle)
        g.DrawPath(PenSmallTics, gpSmallTics)
        g.DrawPath(PenMediumTics, gpMediumTics)
        g.DrawPath(PenBigTics, gpBigTics)

    End Sub

#Region "Class Properties"
    Friend Property Origin() As PointF
        Get
            Return _Origin
        End Get
        Set(ByVal value As PointF)
            _Origin = value
        End Set
    End Property

    Friend Property Radius() As Single
        Get
            Return _Radius
        End Get
        Set(ByVal value As Single)
            _Radius = value
        End Set
    End Property

    Friend Property CircleLineWidth() As Single
        Get
            Return PenCircle.Width
        End Get
        Set(ByVal value As Single)
            PenCircle.Width = value
        End Set
    End Property

    Friend Property SmallTicLineWidth() As Single
        Get
            Return PenSmallTics.Width
        End Get
        Set(ByVal value As Single)
            PenSmallTics.Width = value
        End Set
    End Property

    Friend Property MediumTicLineWidth() As Single
        Get
            Return PenMediumTics.Width
        End Get
        Set(ByVal value As Single)
            PenMediumTics.Width = value
        End Set
    End Property

    Friend Property BigTicLineWidth() As Single
        Get
            Return PenBigTics.Width
        End Get
        Set(ByVal value As Single)
            PenBigTics.Width = value
        End Set
    End Property

    Friend Property BigTicsAngle() As Single
        Get
            Return _BigTicsAngle
        End Get
        Set(ByVal value As Single)
            _BigTicsAngle = value
        End Set
    End Property

    Friend Property MediumTicsAngle() As Single
        Get
            Return _MediumTicsAngle
        End Get
        Set(ByVal value As Single)
            _MediumTicsAngle = value
        End Set
    End Property

    Friend Property SmallTicsAngle() As Single
        Get
            Return _SmallTicsAngle
        End Get
        Set(ByVal value As Single)
            _SmallTicsAngle = value
        End Set
    End Property

    Friend Property Color() As Color
        Get
            Return PenCircle.Color
        End Get
        Set(ByVal value As Color)
            PenCircle.Color = value
            PenBigTics.Color = value
            PenMediumTics.Color = value
            PenSmallTics.Color = value
        End Set
    End Property
#End Region

    Private Sub DrawCircle()
        gpCircle.Reset()
        Dim diameter As Single = _Radius * 2
        gpCircle.AddEllipse(-_Radius, -_Radius, diameter, diameter)
    End Sub

    Private Sub DrawSmallTics()
        gpSmallTics.Reset()
        For theta As Single = 0 To 359.99999 Step _SmallTicsAngle
            AddRadial(gpSmallTics, theta, _Radius * 0.95!, _Radius)
        Next
    End Sub

    Private Sub DrawMediumTics()
        gpMediumTics.Reset()
        For theta As Single = 0 To 359.99999 Step _MediumTicsAngle
            AddRadial(gpMediumTics, theta, _Radius * 0.91!, _Radius)
        Next
    End Sub

    Private Sub DrawBigTics()
        gpBigTics.Reset()
        For theta As Single = 0 To 359.99999 Step _BigTicsAngle
            AddRadial(gpBigTics, theta, _Radius * 0.8!, _Radius)
        Next
    End Sub

    Private Sub AddRadial(ByVal gp As GraphicsPath, ByVal Angle As Single, ByVal r1 As Single, ByVal r2 As Single)

        Dim RadAngle As Double = -(Angle + 180) * PiBy180
        Dim x1 As Single = CType(r1 * Math.Sin(RadAngle), Single)
        Dim y1 As Single = CType(r1 * Math.Cos(RadAngle), Single)
        Dim x2 As Single = CType(r2 * Math.Sin(RadAngle), Single)
        Dim y2 As Single = CType(r2 * Math.Cos(RadAngle), Single)

        gp.StartFigure()
        gp.AddLine(x1, y1, x2, y2)

    End Sub


    Protected Overrides Sub Finalize()

        PenCircle.Dispose()
        PenBigTics.Dispose()
        PenMediumTics.Dispose()
        PenSmallTics.Dispose()

        gpCircle.Dispose()
        gpBigTics.Dispose()
        gpMediumTics.Dispose()
        gpSmallTics.Dispose()

        TransformMatrix.Dispose()

    End Sub
End Class
