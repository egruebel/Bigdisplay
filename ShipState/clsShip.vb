Imports System.Drawing.Drawing2D

Public Class clsShip

    Private _Origin As PointF
    Private _Length As Single
    Private _Width As Single
    Private _Heading As Single

    Private gpShip As New GraphicsPath
    Private PenShip As New Pen(Color.Black, 5)

    Private TransformMatrix As New Matrix

    Friend Sub New()
  
    End Sub

    Private Sub DrawShip()
        ' Draw the ship pointing north, with (0,0) midships and halfway forward.

        gpShip.Reset()
        Const Bow As Integer = 0
        Const PortFwd As Integer = 1
        Const PortQtr As Integer = 2
        Const PortAft As Integer = 3
        Const StbdAft As Integer = 4
        Const StbdQtr As Integer = 5
        Const StbdFwd As Integer = 6

        Dim HalfWidth As Single = _Width * 0.5!
        Dim HalfLength As Single = _Length * 0.5!

        Dim pts(6) As PointF
        pts(Bow).X = 0 : pts(Bow).Y = -HalfLength 'Up (north) is decreasing height
        pts(PortFwd).X = -HalfWidth : pts(PortFwd).Y = -HalfLength * 0.5!
        pts(PortQtr).X = -HalfWidth : pts(PortQtr).Y = +HalfLength * 0.95!
        pts(PortAft).X = -HalfWidth * 0.87! : pts(PortAft).Y = +HalfLength
        pts(StbdAft).X = +HalfWidth * 0.87! : pts(StbdAft).Y = +HalfLength
        pts(StbdQtr).X = +HalfWidth : pts(StbdQtr).Y = +HalfLength * 0.95!
        pts(StbdFwd).X = +HalfWidth : pts(StbdFwd).Y = -HalfLength * 0.5!

        gpShip.AddPolygon(pts)
    End Sub

    Public Sub Update()

        ' Raise this event after any change of size, position or heading
        ' No need to raise this event after a change to a figure's Pen

        ' Create figure and add it to GraphicsPath
        DrawShip()

        'Move it to origin and then rotate it
        TransformMatrix.Reset()
        TransformMatrix.Translate(_Origin.X, _Origin.Y)
        TransformMatrix.Rotate(_Heading)
        gpShip.Transform(TransformMatrix)

    End Sub
    Friend Sub Draw(ByVal g As Graphics)

        'Show it
        'g.SmoothingMode = SmoothingMode.AntiAlias
        g.DrawPath(PenShip, gpShip)

    End Sub

    Friend Sub clear()

        gpShip.Reset()

    End Sub

    Friend Property Origin() As PointF
        Get
            Return _Origin
        End Get
        Set(ByVal value As PointF)
            _Origin = value
        End Set
    End Property

    Friend Property Heading() As Single
        Get
            Return _Heading
        End Get
        Set(ByVal value As Single)
            _Heading = value
        End Set
    End Property

    Friend Property Length() As Single
        Get
            Return _Length
        End Get
        Set(ByVal value As Single)
            _Length = value
        End Set
    End Property

    Friend Property Width() As Single
        Get
            Return _Width
        End Get
        Set(ByVal value As Single)
            _Width = value
        End Set
    End Property

    Friend Property LineWidth() As Single
        Get
            Return PenShip.Width
        End Get
        Set(ByVal value As Single)
            PenShip.Width = value
        End Set
    End Property

    Friend Property LineColor() As Color
        Get
            Return PenShip.Color
        End Get
        Set(ByVal value As Color)
            PenShip.Color = value
        End Set
    End Property

    Friend Sub Dispose()

        PenShip.Dispose()
        gpShip.Dispose()
        TransformMatrix.Dispose()

    End Sub
    Protected Overrides Sub Finalize()

        PenShip.Dispose()
        gpShip.Dispose()
        TransformMatrix.Dispose()

    End Sub
End Class
