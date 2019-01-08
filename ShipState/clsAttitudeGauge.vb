Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Threading
Imports System.Threading.Tasks

Public Class clsAttitudeGauge

    Private Const PiBy180 As Single = Math.PI / 180.0!

    Private _Origin As PointF
    Private _Radius As Single
    Private _SmallTicsAngle As Single = 10.0!
    Private _MediumTicsAngle As Single = 30.0!
    Private _BigTicsAngle As Single = 90.0!

    Private _roll as Single = -10.5
    Private _pitch As Single = -10.5

    Private PenCircle As New Pen(Color.Black, 7)
    Private PenBigTics As New Pen(Color.Black, 7)
    Private PenMediumTics As New Pen(Color.Black, 4)
    Private PenSmallTics As New Pen(Color.Black, 2)
    Private PenTarget As New Pen(Color.Brown, 2)
    Private PenHorizon As New Pen(Color.DarkBlue,2)
    Private BrushHorizon As SolidBrush = New SolidBrush(Color.CornflowerBlue)

    Private gpCircle As New GraphicsPath
    Private gpBigTics As New GraphicsPath
    Private gpMediumTics As New GraphicsPath
    Private gpSmallTics As New GraphicsPath
    Private gpTarget as New GraphicsPath
    Private gpWater as New GraphicsPath
    Private gpHorizon as New GraphicsPath
    Private gpPitchText as New GraphicsPath
    Private gpRollText as New GraphicsPath
    
    Private TransformMatrix As New Matrix
    Private StaticMatrix As New Matrix
    Private TargetMatrix As New Matrix

    Private qLock As New Object
    Public Task As Task
    Public CancellationTokenSource As New CancellationTokenSource 
    Public CancellationToken As CancellationToken = CancellationTokenSource.Token


    Friend Sub New()
        
    End Sub

    Public Overridable Sub Update()

        ' Raise this event after any change of size, position or tick spacing/size
        ' No need to raise this event after a change to a figure's Pen

        ' Create figures as GraphicsPaths
        SyncLock qLock
            DrawWater()
            DrawHorizon()
            DrawCircle()
            DrawSmallTics()
            DrawMediumTics()
            DrawBigTics()
            DrawText()
            DrawTarget()
            

            'Move the the GPs to the Origin
            TransformMatrix.Reset()
            StaticMatrix.Reset()
            TargetMatrix.Reset()

            TransformMatrix.Translate(_Origin.X, _Origin.Y)
            StaticMatrix.Translate(_Origin.X, _Origin.Y)
            TargetMatrix.Translate(_Origin.X, _Origin.Y + (DisplayPitch)) 'move up or down on the y axis for pitch

            TransformMatrix.Rotate(Roll)
            'TargetMatrix.Translate(0,roll)
        
            gpSmallTics.Transform(TransformMatrix)
            gpMediumTics.Transform(TransformMatrix)
            gpBigTics.Transform(TransformMatrix)
            gpCircle.Transform(TransformMatrix)

            gpTarget.Transform(TargetMatrix)

            gpHorizon.Transform(StaticMatrix)
            gpWater.Transform(StaticMatrix)
            gpPitchText.Transform(StaticMatrix)
            gpRollText.Transform(StaticMatrix)
        End SyncLock
        

    End Sub
    
    Friend Overridable Sub Draw(ByVal g As Graphics)

        'Show 'em
        If Not Visible Then Exit Sub

        g.SmoothingMode = SmoothingMode.AntiAlias

        SyncLock qLock
            g.FillPath(BrushHorizon, gpWater)
            g.DrawPath(PenHorizon,gpHorizon)
            g.DrawPath(PenCircle, gpCircle)
            g.DrawPath(PenSmallTics, gpSmallTics)
            g.DrawPath(PenMediumTics, gpMediumTics)
            g.DrawPath(PenBigTics, gpBigTics)
            g.DrawPath(PenTarget, gpTarget)
            g.FillPath(New SolidBrush(PenTarget.Color), gpPitchText)
            g.FillPath(New SolidBrush(PenCircle.Color), gpRollText)
        End SyncLock
        
        


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
    Friend Property Roll() As Single
       Get
           Return _roll
       End Get
        Set(value As Single)
            _roll = value
            Update()
        End Set
    End Property
    Friend Property Pitch() As Single
        Get
            Return _pitch
        End Get
        Set(value As Single)
            _pitch = value
            Update()
        End Set
    End Property
    Friend ReadOnly Property DisplayPitch as Single
        get
            Return (Pitch / PitchDegreesPerTick) * PitchLineSpacing * -1 'scaled pitch so that n degrees equals one line on the moving scale
        End Get
    End Property
    Friend Property Radius() As Single
        Get
            Return _Radius
        End Get
        Set(ByVal value As Single)
            _Radius = value
        End Set
    End Property
    Friend Property PitchLineSpacing as Single = 4
    Friend Property PitchDegreesPerTick As Single = 4
    Friend Property Font as FontFamily = new FontFamily(GenericFontFamilies.Monospace)
    Friend ReadOnly Property FontSize as Single

    get
        Return  Radius * .29!
    End Get
    End Property
    Friend Property TargetLineWidth() As Single
        Get
            Return PenTarget.Width
        End Get
        Set(ByVal value As Single)
            PenTarget.Width = value
        End Set
    End Property
    Friend Property Visible() as Boolean = True

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

    Public Property RoseColor() As Color
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
    Public Property WaterColor() As Color
        Get
            Return BrushHorizon.Color
        End Get
        Set(ByVal value As Color)
            BrushHorizon = New SolidBrush(value)
        End Set
    End Property
    Public Property HorizonColor() As Color
        Get
            Return PenHorizon.Color
        End Get
        Set(ByVal value As Color)
            PenHorizon.Color = value
        End Set
    End Property
    Public Property TargetColor() As Color
        Get
            Return PenTarget.Color
        End Get
        Set(ByVal value As Color)
            PenTarget.Color = value
        End Set
    End Property
#End Region

    Private Sub DrawCircle()
        gpCircle.Reset()
        Dim diameter As Single = _Radius * 2
        gpCircle.AddEllipse(-_Radius, -_Radius, diameter, diameter)
    End Sub
    Private Sub DrawWater()
        gpWater.Reset()
        Dim diameter As Single = _Radius * 2
        gpWater.StartFigure()
        gpWater.AddArc(-_Radius,-_Radius,diameter,diameter,0,180)
        gpWater.CloseFigure()
        
    End Sub
    Private Sub DrawHorizon()
        gpHorizon.Reset()
        gpHorizon.AddLine(-Radius,0,Radius,0)
    End Sub
    Private Sub DrawText()
        gpPitchText.Reset()
        gpRollText.Reset()
        Dim width as Single = _Radius * 1.3!
        Dim height as Single = height * .15!

        'render text just below horizon
        Dim textBox as RectangleF = new RectangleF(PitchLineSpacing + 2, 0, width, height)
        'Dim font as New FontFamily(GenericFontFamilies.Monospace)
        gpPitchText.StartFigure()
        gpPitchText.AddString(Pitch.ToString("0.0") & Chr(176), Font, FontStyle.Bold, FontSize * .75!, textBox, New StringFormat())
        gpPitchText.CloseFigure()
        textBox = New RectangleF(-PitchLineSpacing * 3, Radius * -1.4!, width, height)
        gpRollText.StartFigure()
        gpRollText.AddString(Roll.ToString("0.0") & Chr(176),Font, FontStyle.Bold, FontSize, textBox, New StringFormat())
        gpRollText.CloseFigure()
    End Sub
    Private Sub DrawTarget()
        gpTarget.Reset()

        Dim height as Single = _Radius * .75!
        Dim width as Single = height * .15!
        gpTarget.StartFigure()
        gpTarget.AddLine(0,height,0,-height) 'center stem
        gpTarget.CloseFigure()
        
        Dim leaves As Integer = 8 '4 leaves on each side of stem
        PitchLineSpacing = height / leaves 'spacing between leaves
        Dim yPos As Single = (leaves / 2) * PitchLineSpacing * -1 'start at the top and draw leaves working down
        for i As integer = 0 To leaves
            gpTarget.StartFigure()
            If(i = leaves / 2)
                gpTarget.AddLine(-(width * 1.6!),yPos,width * 1.6!,yPos) 'a little wider at the horizon
            else
                gpTarget.AddLine(-width,yPos,width,yPos)
            End If
            
            gpTarget.CloseFigure()
            yPos += PitchLineSpacing
        Next
        
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
