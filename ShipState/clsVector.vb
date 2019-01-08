Imports System.Drawing.Drawing2D

Public Class clsVector

    Private _Magnitude As Single
    Private _Direction As Single

    Private _PointsIn As Boolean
    Private _BarbWidthRatio As Single
    Private _BarbLengthRatio As Single

    Private _Origin As PointF
    Private _TipRadius As Single
    Private _ScaleFactor As Single

    Private PenVector As New Pen(Color.Yellow, 4)
    Private gpVector As New GraphicsPath
    Private TransformMatrix As New Matrix

    Friend Sub New(ByVal PointsIn As Boolean)

        PenVector.EndCap = LineCap.Round
        PenVector.StartCap = LineCap.Round
        _PointsIn = PointsIn

    End Sub
    Friend Sub Draw(ByVal g As Graphics)

        'Show it  
        'g.SmoothingMode = SmoothingMode.AntiAlias
        g.DrawPath(PenVector, gpVector)

    End Sub
    
    Friend Sub Update()

        ' Create figure and add it to GraphicsPath
        DrawVector()

        ' Start with a fresh matrix and move vector to the origin, tip at (0,0)
        TransformMatrix.Reset()
        TransformMatrix.Translate(_Origin.X, _Origin.Y)

        ' Rotate vector to point in  required direction
        TransformMatrix.Rotate(_Direction)

        ' Move vector along radial, tip from (0,0) to tip radius
        TransformMatrix.Translate(0, -_TipRadius * _ScaleFactor)

        ' Commit changes
        gpVector.Transform(TransformMatrix)

    End Sub

    Private Sub DrawVector()

        ' Create a vector
        ' If PointsIn then it is pointing south with tip at (0,0) Gives direction "from"
        ' If PointsIn = false it is pointing north with tip at (0,0) Gives direction "toward"
        ' Remember up is decreasing in GDI coordinates

        Const zero As Single = 0
        Dim ShaftLen As Single = _Magnitude * _ScaleFactor
        Dim BarbLen As Single = ShaftLen * _BarbLengthRatio
        Dim BarbWid As Single = ShaftLen * _BarbWidthRatio

        ' Determin sense of vector: points in or out
        ' Remember up is decreasing in GDI coordinates
        ' If PointsIn then it is pointing south with tip at (0,0)
        ' If PointsIn = false it is pointing north with tip at (0,0)
        Dim mult As Integer = 1    ' Points out (up/north)
        If PointsIn Then mult = -1 ' Points in (down/south)

        ' Start with clear GraphicsPath, then add shaft first
        gpVector.Reset()
        gpVector.AddLine(zero, mult * ShaftLen, zero, zero)

        ' Now the barb. No need to draw the barb if BarbWidth is zero; AND, there seems
        ' to be a bug- if BarbWidth is zero and you try to draw it, an Out of Memory 
        ' exception occurs... So don't do it.
        If BarbWid > 0 Then
            gpVector.StartFigure()
            gpVector.AddLine(-BarbWid, mult * BarbLen, zero, zero) ' Upper left to tip
            gpVector.AddLine(zero, zero, BarbWid, mult * BarbLen)  ' Tip to lower right
        End If

    End Sub

    Friend Sub Clear()
        gpVector.Reset()
    End Sub
    Friend Property Origin() As PointF
        Get
            Return _Origin
        End Get
        Set(ByVal value As PointF)
            _Origin = value
        End Set
    End Property
    Friend Property Direction() As Single
        Get
            Return _Direction
        End Get
        Set(ByVal value As Single)
            _Direction = value
        End Set
    End Property

    Friend Property TipRadius() As Single
        Get
            Return _TipRadius
        End Get
        Set(ByVal value As Single)
            _TipRadius = value
        End Set
    End Property
    Friend Property Magnitude() As Single
        Get
            Return _Magnitude
        End Get
        Set(ByVal value As Single)
            _Magnitude = value
        End Set
    End Property
    Friend Property ScaleFactor() As Single
        Get
            Return _ScaleFactor
        End Get
        Set(ByVal value As Single)
            _ScaleFactor = value
        End Set
    End Property
    Friend Property PointsIn() As Boolean
        Get
            Return _PointsIn
        End Get
        Set(ByVal value As Boolean)
            _PointsIn = value
        End Set
    End Property
    Friend Property LineWidth() As Single
        Get
            Return PenVector.Width
        End Get
        Set(ByVal value As Single)
            PenVector.Width = value
        End Set
    End Property
    Friend Property LineColor() As Color
        Get
            Return PenVector.Color
        End Get
        Set(ByVal value As Color)
            PenVector.Color = value
        End Set
    End Property
    Friend Property BarbWidthRatio() As Single
        Get
            Return _BarbWidthRatio
        End Get
        Set(ByVal value As Single)
            _BarbWidthRatio = value
        End Set
    End Property
    Friend Property BarbLengthRatio() As Single
        Get
            Return _BarbLengthRatio
        End Get
        Set(ByVal value As Single)
            _BarbLengthRatio = value
        End Set
    End Property

    Friend Sub Dispose()

        PenVector.Dispose()
        gpVector.Dispose()
        TransformMatrix.Dispose()

    End Sub
    Protected Overrides Sub Finalize()

        PenVector.Dispose()
        gpVector.Dispose()
        TransformMatrix.Dispose()

    End Sub

End Class
