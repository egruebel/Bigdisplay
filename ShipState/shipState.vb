Imports System.Drawing
Imports System.ComponentModel
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms.VisualStyles

Public Class ShipState
    
    Private deg As String = Chr(176)
    Const ImpossibleKey As String = "Never in a million years!.!,![!=!\!9!6!5!3!2!!!!!! - Nope, not even once"

    Private LeftWindLabelKey As String = ImpossibleKey
    Private RightWindLabelKey As String = ImpossibleKey

    Private _RoseSizeFactor As Single = 85.0!
    Private _ShipLengthFactor As Single = 90.0!
    Private _ShipVectorScale As Single = 12.0!
    Private _WindScale As Single = 100.0!

    Private Radius As Single
    Private Diameter As Single

    Private Rose As New clsCompassRose
    Private Attitude As New clsAttitudeGauge
    Private Ship As New clsShipQueue
    Private ShipVector As New clsVectorQueue(False)
    Private SpeedLogVector As New clsVectorQueue(False)
    Private Winds As New Dictionary(Of String, clsVectorQueue)
    Private Frames As Integer = 99
    Private FrameRate As Integer = 7
    Private AttitudeFrames As Integer = 20
    Private AttitudeFrameRate As Integer = 18

    Private Sub Me_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Static i As Integer
        Debug.Print(i.ToString)
        i += 1
        ScaleToDisplay()
        'Me.Refresh()
    End Sub

    Private Sub Me_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        Dim g As Graphics = e.Graphics

        Rose.Draw(g)
        Ship.Draw(g)
        ShipVector.Draw(g)
        SpeedLogVector.Draw(g)
        Attitude.Draw(g)
        For Each vq As clsVectorQueue In Winds.Values
            vq.Draw(g)
        Next
    End Sub
    Private Sub Me_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Debug.Print("Load Start")

        Ship.LineColor = Color.Azure

        ShipVector.BarbLengthRatio = 0.15
        ShipVector.BarbWidthRatio = 0.07
        ShipVector.LineColor = Color.Yellow

        SpeedLogVector.BarbLengthRatio = .15
        SpeedLogVector.BarbWidthRatio = .07
        SpeedLogVector.LineColor = Color.DarkRed

        Me.lblShipCmg.Text = ""
        Me.lblShipSmg.Text = ""
        Me.lblShipHeading.Text = ""
        Me.lblTrueWindAzimuthLeft.Text = ""
        Me.lblTrueWindSpeedLeft.Text = ""
        Me.lblTrueWindAzimuthRight.Text = ""
        Me.lblTrueWindSpeedRight.Text = ""
        Debug.Print("load, prescale")
        ScaleToDisplay()
        'Me.Refresh()
        
        Debug.Print("Load Done")
    End Sub

    Private Sub ScaleToDisplay()

        If Rose Is Nothing Then Exit Sub

        Dim Origin As New PointF
        Origin.X = Me.ClientRectangle.Width * 0.5!
        Origin.Y = Me.ClientRectangle.Height * 0.5!
        Dim MinSize As Single = Math.Min(Me.ClientRectangle.Height, Me.ClientRectangle.Width)
        Diameter = MinSize * (_RoseSizeFactor / 100.0!)
        Radius = Diameter / 2.0!


        Rose.Radius = Radius
        Rose.Origin = Origin
        Rose.CircleLineWidth = Radius * 0.021!
        Rose.BigTicLineWidth = Radius * 0.021!
        Rose.MediumTicLineWidth = Radius * 0.017!
        Rose.SmallTicLineWidth = Radius * 0.012!
        Rose.Update()

        Attitude.Radius = Radius / 3.6!
        Attitude.Origin = New PointF((Me.ClientRectangle.Right - Attitude.Radius) - 10, (me.ClientRectangle.Bottom - Attitude.Radius)- 10)
        Attitude.CircleLineWidth = Radius * 0.01!
        Attitude.BigTicLineWidth = Radius * 0.01!
        Attitude.MediumTicLineWidth = Radius * 0.007!
        Attitude.SmallTicLineWidth = Radius * 0.007!
        Attitude.TargetLineWidth = Radius * .008!
        Attitude.Update()

        Ship.Origin = Origin
        Ship.Length = Diameter * (_ShipLengthFactor / 100.0!)
        Ship.Width = Ship.Length * 0.2!
        Ship.LineWidth = Radius * 0.03!
        Ship.Update()

        ShipVector.Origin = Origin
        ShipVector.LineWidth = Radius * 0.029!
        ShipVector.ScaleFactor = Diameter / _ShipVectorScale   ' The speed indicated by a vector, length = rose diameter
        ShipVector.Update()

        SpeedLogVector.Origin = Origin
        SpeedLogVector.LineWidth = Radius * 0.029!
        SpeedLogVector.ScaleFactor = Diameter / _ShipVectorScale   ' The speed indicated by a vector, length = rose diameter
        SpeedLogVector.Update()

        For Each vq As clsVectorQueue In Winds.Values
            vq.Origin = Origin
            vq.LineWidth = Radius * 0.029!
            vq.ScaleFactor = Diameter / _WindScale
            vq.Update()
        Next


        Dim currentSize As Single = MinSize * 0.04!
        If currentSize < 4 Then currentSize = 4

        With lblTrueWindLeft
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = CType(.Height / 60, Integer)
            .Left = 0 '.Top
        End With

        With lblTrueWindSpeedLeft
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = lblTrueWindLeft.Bottom
            .Left = lblTrueWindLeft.Left
        End With

        With lblTrueWindAzimuthLeft
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = lblTrueWindSpeedLeft.Bottom
            .Left = lblTrueWindLeft.Left
        End With

        With lblTrueWindRight
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = lblTrueWindLeft.Top
            .Left = Me.Width - lblTrueWindRight.Width - 4
        End With

        With lblTrueWindSpeedRight
            .Size = lblTrueWindRight.Size
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = lblTrueWindSpeedLeft.Top
            .Left = lblTrueWindRight.Left
        End With

        With lblTrueWindAzimuthRight
            .Size = lblTrueWindRight.Size
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = lblTrueWindAzimuthLeft.Top
            .Left = lblTrueWindRight.Left
        End With

        With lblHead
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = Me.ClientRectangle.Bottom - .Height - CType(Height / 160, Integer)
            .Left = lblTrueWindLeft.Left
        End With

        With lblShipHeading
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = Me.ClientRectangle.Bottom - .Height - CType(Height / 160, Integer)
            .Left = lblHead.Right
        End With

        With lblShipSmg
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = lblShipHeading.Top - .Height ' - Height / 160
            .Left = lblTrueWindLeft.Left
        End With

        With lblShipCmg
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = lblShipSmg.Top - .Height '- Height / 60
            .Left = lblTrueWindLeft.Left
        End With

        With lblShip
            .Font = New Font(.Font.Name, currentSize, .Font.Style, .Font.Unit)
            .Top = lblShipCmg.Top - .Height '- Height / 60
            .Left = lblTrueWindLeft.Left
        End With

    End Sub

    <Browsable(True), _
   Description("The color of the compass rose.")> _
     Public Property RoseColor() As Color
        Get
            Return Rose.Color
        End Get
        Set(ByVal value As Color)
            Rose.Color = value
        End Set
    End Property
    <Browsable(True), _
        Description("The ship's attitude display.")> _
    Public Property AttitudeDisplay() As clsAttitudeGauge
        Get
            Return Attitude
        End Get
        Set(ByVal value As clsAttitudeGauge)
            Attitude = value
        End Set
    End Property
    <Browsable(True), _
        Description("Number of animation frames to render")> _
    Public Property AnimationFrames() As Integer
        Get
            Return Frames
        End Get
        Set(ByVal value As Integer)
            Frames = value
            
        End Set
    End Property
    <Browsable(True), _
        Description("Frame rendering rate in ms")> _
    Public Property AnimationFrameRate() As Integer
        Get
            Return FrameRate
        End Get
        Set(ByVal value As Integer)
            FrameRate = value
            
        End Set
    End Property
    <Browsable(True), _
        Description("Number of animation frames to render")> _
    Public Property AttitudeAnimationFrames() As Integer
        Get
            Return AttitudeFrames
        End Get
        Set(ByVal value As Integer)
            AttitudeFrames = value
            
        End Set
    End Property
    <Browsable(True), _
        Description("Frame rendering rate in ms")> _
    Public Property AttitudeAnimationFrameRate() As Integer
        Get
            Return AttitudeFrameRate
        End Get
        Set(ByVal value As Integer)
            AttitudeFrameRate = value
            
        End Set
    End Property
    <Browsable(True), _
   Description("Size of compass rose as a percentage of the smaller of either the control's width or height.")> _
    Public Property RoseSizeFactor() As Single
        Get
            Return _RoseSizeFactor
        End Get
        Set(ByVal value As Single)
            If Rose IsNot Nothing And value > 0 Then
                _RoseSizeFactor = value
            End If
        End Set
    End Property
    <Browsable(True), _
        Description("Determines if the attitude meter is visible or hidden.")> _
    Public Property AttitudeVisible() as Boolean
        Get
            Return Attitude.Visible
        End Get
        Set(value as Boolean)
            Attitude.Visible = value
        End Set
    End Property
    <Browsable(True), _
   Description("Determines if the ship outline and heading value are visible or hidden.")> _
    Public Property ShipVisible() As Boolean
        Get
            Return Ship.Visible
        End Get
        Set(ByVal value As Boolean)
            Ship.Visible = value
            lblShipHeading.Visible = value
        End Set
    End Property
    <Browsable(True), _
   Description("The color of the outline of the ship.")> _
    Public Property ShipColor() As Color
        Get
            Return Ship.LineColor
        End Get
        Set(ByVal value As Color)
            Ship.LineColor = value
        End Set
    End Property

    <Browsable(True), _
   Description("Size of ship as a percentage of the compass rose diameter.")> _
    Public Property ShipLengthFactor() As Single
        Get
            Return _ShipLengthFactor
        End Get
        Set(ByVal value As Single)
            _ShipLengthFactor = value
        End Set
    End Property
    Public Sub SetWindColor(ByVal key As String, ByVal color As Color)
        If Winds.ContainsKey(key) Then
            Dim thisWind = Winds.Item(key)
            thisWind.LineColor = color
            SynchWindLabels()
        End if 
    End Sub
    Public Sub SetHeading(ByVal Heading As Single)
        If Not Ship.Task Is Nothing Then
            If (not Ship.Task.IsCompleted) or (not Ship.Task.IsCanceled) Then
                Try
                    Ship.CancellationTokenSource.Cancel
                    Ship.Task.Wait
                Catch ex As Exception
                Finally
                    Ship.CancellationTokenSource = New CancellationTokenSource()
                    Ship.CancellationToken = Ship.CancellationTokenSource.Token
                End Try
            End If
        End If
        Ship.Task = Task.Run(Sub()
            Dim capturedToken as CancellationToken = Ship.CancellationToken
            Dim last as clsShip = Ship.GetLast
            Dim lastHead as Single = 0
            If (Not last Is Nothing) Then
                lastHead = last.Heading
            End If
            dim headDif = SignedAngularDifference(lastHead, Heading)
            dim cHead as Single
            for i as Integer = 0 to Frames
                if capturedToken.IsCancellationRequested Then
                    Exit For
                End If
                cHead = EaseCubic(i, lastHead, headDif, Frames)
                Ship.add(cHead)
                capturedToken.WaitHandle.WaitOne(FrameRate)
                'Thread.Sleep(FrameRate)
            Next
        End Sub, Ship.CancellationToken)
        lblShipHeading.Text = Format(Heading, "000") & deg
    End Sub
    <Browsable(True), _
        Description("The attitude to represent.")> _
    Public Sub SetAttitude(ByVal roll As Single, ByVal pitch As Single)
        If Not Attitude.Task Is Nothing Then
            If (not Attitude.Task.IsCompleted) or (not Attitude.Task.IsCanceled) Then
                Try
                    Attitude.CancellationTokenSource.Cancel
                    Attitude.Task.Wait
                Catch ex As Exception
                    'there could occasionally be a taskcancellation exception due to thread locking
                    'no way around this
                Finally
                    Attitude.CancellationTokenSource = New CancellationTokenSource()
                    Attitude.CancellationToken = Attitude.CancellationTokenSource.Token
                End Try
                
            End If
        End If

        Attitude.Task = Task.Run(Sub()
            Dim capturedToken As CancellationToken = Attitude.CancellationToken
            'get directional difference
            Dim lastRoll as Single = Attitude.Roll
            Dim lastPitch as Single = Attitude.Pitch
            Dim rollDif as Single = roll - lastRoll
            Dim pitchDif as Single = pitch - lastPitch
            for i = 0 To AttitudeFrames
                If(capturedToken.IsCancellationRequested)
                    Exit For
                End If
                Attitude.Roll = EaseCubic(i,lastRoll,rollDif,AttitudeFrames)
                Attitude.Pitch = EaseCubic(i,lastPitch,pitchDif,AttitudeFrames)
                capturedToken.WaitHandle.WaitOne(FrameRate)
            Next
        End Sub, Attitude.CancellationToken)
    End Sub
    <Browsable(True), _
        Description("The course and speed over ground the ship vector is to represent.")> _
    Public Sub SetSpeedLog(ByVal angle As Single, ByVal velocity As Single)
        If Not SpeedLogVector.Task Is Nothing Then
            If (Not SpeedLogVector.Task.IsCompleted) Or (Not SpeedLogVector.Task.IsCanceled ) Then
                Try
                    SpeedLogVector.CancellationTokenSource.Cancel
                    SpeedLogVector.Task.Wait
                Catch ex As Exception
                Finally
                    SpeedLogVector.CancellationTokenSource = New CancellationTokenSource()
                    SpeedLogVector.CancellationToken = SpeedLogVector.CancellationTokenSource.Token

                End Try
            End If
        End If
        SpeedLogVector.Task = Task.Run(Sub()
            dim capturedToken as CancellationToken = SpeedLogVector.CancellationToken
            Dim sw as new Stopwatch 
            sw.Start 
            'get the ship's heading
            Dim shipOrientation As clsShip = Ship.GetLast
            Dim head As Single = 0
            If(Not shipOrientation Is Nothing) Then
                head = shipOrientation.Heading
            End If
            angle = head + angle
            If (angle < 0.0) Then
                angle = angle + 360

            End If
            If (angle > 360.0) Then
                angle = angle - 360
            End If


            Dim last As clsVector = SpeedLogVector.GetLast
            Dim lastAngle as Single = 0
            Dim lastVelocity as Single = 0
            If(Not last Is Nothing) Then
                lastAngle = last.Direction
                lastVelocity = last.Magnitude
            End If
            Dim angleDif = SignedAngularDifference(lastAngle, angle)
            Dim velocityDif = velocity - lastVelocity
            Dim cAngle, cVelocity As Single
            
            for i As Integer = 0 to Frames
                If(capturedToken.IsCancellationRequested)
                    Exit For
                End If
                cAngle = EaseCubic(i,lastAngle, angleDif,Frames)
                cVelocity = EaseCubic(i,lastVelocity, velocityDif,Frames)
                SpeedLogVector.add(cAngle, cVelocity, cVelocity / 2.0!)
                capturedToken.WaitHandle.WaitOne(FrameRate)
            Next
            Debug.Print("SpeedLogExecTime" & sw.ElapsedMilliseconds)
        End Sub, SpeedLogVector.CancellationToken)
        'lblShipCmg.Text = Format(cog, "000") & deg
        'lblShipSmg.Text = sog.ToString & " kts."

    End Sub
    
    <Browsable(True), _
        Description("The course and speed over ground the ship vector is to represent.")> _
    Public Sub SetCogSog(ByVal cog As Single, ByVal sog As Single)
        If Not ShipVector.Task Is Nothing Then
            If (not ShipVector.Task.IsCompleted) or (not ShipVector.Task.IsCanceled) Then
                Try
                    ShipVector.CancellationTokenSource.Cancel
                    ShipVector.Task.Wait
                Catch ex As Exception
                Finally
                    ShipVector.CancellationTokenSource = New CancellationTokenSource()
                    ShipVector.CancellationToken = ShipVector.CancellationTokenSource.Token
                End Try
            End If
        End If
        ShipVector.Task = Task.Run(Sub()
            dim capturedToken as CancellationToken = ShipVector.CancellationToken
            Dim sw as new Stopwatch 
            sw.Start 
            Dim last As clsVector = ShipVector.GetLast
            Dim lastCog as Single = 0
            Dim lastSog as Single = 0
            If(Not last Is Nothing) Then
                lastCog = last.Direction
                lastSog = last.Magnitude
            End If
            Dim cogDif = SignedAngularDifference(lastCog, cog)
            Dim sogDif = sog - lastSog
            Dim cCog, cSog As Single
            
            for i As Integer = 0 to Frames
                If(capturedToken.IsCancellationRequested)
                    Exit For
                End If
                cCog = EaseCubic(i,lastCog, cogDif,Frames)
                cSog = EaseCubic(i,lastSog, sogDif,Frames)
                ShipVector.add(cCog, cSog, cSog / 2.0!)
                capturedToken.WaitHandle.WaitOne(FrameRate)
            Next
            Debug.Print("CogSogExecTime" & sw.ElapsedMilliseconds)
        End Sub, ShipVector.CancellationToken)
        lblShipCmg.Text = Format(cog, "000") & deg
        lblShipSmg.Text = sog.ToString & " kts."

    End Sub
    
    <Browsable(True), _
        Description("Wind Speed and direction wind is from.")> _
    Public Sub SetWind(ByVal key As String, ByVal DirFrom As Single, ByVal Speed As Single)
        Dim thisWind As clsVectorQueue
        If Winds.ContainsKey(key) Then
            thisWind = Winds.Item(key)
        Else 
            Return
        End If
        If Not thisWind.Task Is Nothing Then
            If (not thisWind.Task.IsCompleted) or (not thisWind.Task.IsCanceled) Then
                Try
                    thisWind.CancellationTokenSource.Cancel
                    thisWind.Task.Wait
                Catch ex As Exception
                Finally
                    thisWind.CancellationTokenSource = New CancellationTokenSource()
                    thisWind.CancellationToken = thisWind.CancellationTokenSource.Token
                End Try
            End If
        End If 
        thisWind.Task = Task.Run(Sub()
            Dim capturedToken as CancellationToken = thisWind.CancellationToken
            Dim sw as New Stopwatch
            sw.Reset 
            sw.Start 
            Dim last as clsVector = thisWind.GetLast 
            Dim lastDir As Single = 0
            Dim lastSpd As Single = 0
            If(not last is Nothing)
                lastDir = last.Direction
                lastSpd = last.Magnitude
            End If
            Dim speedDif = Speed - lastSpd
            Dim dirDif = SignedAngularDifference(lastDir, DirFrom)
            Dim cSpd, cDir As Single
            
            for i As Integer = 0 to Frames
                If(capturedToken.IsCancellationRequested)
                    Exit For
                End If
                cSpd = EaseCubic(i,lastSpd,speedDif,Frames)
                cDir = EaseCubic(i,lastDir, dirDif,Frames)
                Winds.Item(key).add(cDir, cSpd, (_WindScale / 2) * 0.75!)
                capturedToken.WaitHandle.WaitOne(FrameRate)
            Next
            Debug.Print("WindExecTime: " & sw.ElapsedMilliseconds)
        End Sub, thisWind.CancellationToken)
        UpdateWindLabel(key,DirFrom,Speed)
    End Sub

    Public Property ShipChaserCount() As Integer
        'Chaser count is in addition to the ship itself
        Get
            Return Ship.MaxCount - 1
        End Get
        Set(ByVal value As Integer)
            If value >= 0 Then
                Ship.MaxCount = value + 1
            Else
                Ship.MaxCount = 1
            End If
        End Set
    End Property

    Public Property ShipChaserFf() As Single
        Get
            Return CType(Ship.FadeFactor, Single)
        End Get
        Set(ByVal value As Single)
            Ship.FadeFactor = value
        End Set
    End Property
    Public Sub ShipTextColor(ByVal Color As Color)
        me.lblShip.ForeColor = Color
        Me.lblShipCmg.ForeColor = Color
        Me.lblShipSmg.ForeColor = Color
    End Sub
    Public Sub ShipHeadingTextColor(ByVal Color As Color)
        me.lblShipHeading.ForeColor = Color
        me.lblHead.ForeColor = Color
    End Sub
    <Browsable(True), _
    Description("Deternins if the ship vector and CMG and SMG values are visible or hidden.")> _
    Public Property ShipVectorVisible() As Boolean
        Get
            Return ShipVector.Visible
        End Get
        Set(ByVal value As Boolean)
            ShipVector.Visible = value
            lblShipCmg.Visible = value
            lblShipSmg.Visible = value
        End Set
    End Property
    
    <Browsable(True), _
    Description("Deternins if the ship vector and CMG and SMG values are visible or hidden.")> _
    Public Property SpeedLogVectorVisible() As Boolean
        Get
            Return SpeedLogVector.Visible
        End Get
        Set(ByVal value As Boolean)
            SpeedLogVector.Visible = value
        End Set
    End Property
    
    <Browsable(True), _
   Description("The color of the ship vector representing CMG and SMG.")> _
    Public Property ShipVectorColor() As Color
        Get
            Return ShipVector.LineColor
        End Get
        Set(ByVal value As Color)
            ShipVector.LineColor = value
        End Set
    End Property
    Public Property SpeedLogVectorColor() As Color
        Get
            Return SpeedLogVector.LineColor
        End Get
        Set(ByVal value As Color)
            SpeedLogVector.LineColor = value
        End Set
    End Property

    Public sub SpeedLogChaserSet(byval count As Integer, byval fadefactor As single)
        SpeedLogVector.MaxCount = 1
        SpeedLogVector.FadeFactor = fadefactor
    End sub

    Public Property ShipVectorChaserCount() As Integer
        'Chaser count is in addition to the vector itself
        Get
            Return ShipVector.MaxCount - 1
        End Get
        Set(ByVal value As Integer)
            If value >= 0 Then
                ShipVector.MaxCount = value + 1
            Else
                ShipVector.MaxCount = 1
            End If
        End Set
    End Property


    Public Property ShipVectorChaserFf() As Single
        Get
            Return CType(ShipVector.FadeFactor, Single)
        End Get
        Set(ByVal value As Single)
            ShipVector.FadeFactor = value
        End Set
    End Property

    <Browsable(True), _
     Description("The ship speed which produces a vector the diameter of the compass rose.")> _
     Public Property ShipVectorScale() As Single
        Get
            Return _ShipVectorScale
        End Get
        Set(ByVal value As Single)
            If value > 0 Then
                _ShipVectorScale = value
            End If
        End Set
    End Property

    

    <Browsable(True), _
   Description("Determins if wind vectors and wind values are visible or hidden.")> _
   Public Property WindVisible(ByVal key As String) As Boolean
        Get
            If Winds.ContainsKey(key) Then
                Return Winds.Item(key).Visible
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Boolean)
            If Winds.ContainsKey(key) Then
                Winds.Item(key).Visible = value
                If key = LeftWindLabelKey Then
                    lblTrueWindAzimuthLeft.Visible = value
                    lblTrueWindSpeedLeft.Visible = value
                ElseIf key = RightWindLabelKey Then
                    lblTrueWindAzimuthRight.Visible = value
                    lblTrueWindSpeedRight.Visible = value
                End If
            End If
        End Set
    End Property

    Public Sub WindAdd(ByVal key As String, ByVal LineColor As Color)

        If Not Winds.ContainsKey(key) Then
            Dim vq As New clsVectorQueue(True)
            ' Stuff here does not happen in ScaleToDisplay
            vq.MaxCount = 1
            vq.FadeFactor = 2
            vq.BarbWidthRatio = 0.25
            vq.BarbLengthRatio = 0.5
            vq.LineColor = LineColor
            vq.Visible = True
            Winds.Add(key, vq)
            ScaleToDisplay()
        End If
    End Sub

    Public Sub WindRemove(ByVal key As String)

        If Winds.ContainsKey(key) Then
            Dim vq As clsVectorQueue = Winds(key)
            Winds.Remove(key)
            vq.Dispose()
        End If

    End Sub

    <Browsable(True), _
   Description("The wind speed which produces a vector the diameter of the compass rose.")> _
   Public Property WindScale() As Single
        Get
            Return _WindScale
        End Get
        Set(ByVal value As Single)
            If value > 0 Then
                _WindScale = value
            End If
        End Set
    End Property
    
    Public Sub UpdateWindLabel(ByVal key As String, ByVal DirFrom As Single, ByVal Speed As Single)
        If Not Winds.ContainsKey(key) Then
            Return
        End If
        If key = LeftWindLabelKey Then
            lblTrueWindAzimuthLeft.Text = Format(DirFrom, "000") & deg
            lblTrueWindSpeedLeft.Text = Speed & " kts."
        ElseIf key = RightWindLabelKey Then
            lblTrueWindAzimuthRight.Text = Format(DirFrom, "000") & deg
            lblTrueWindSpeedRight.Text = Speed & " kts."
        End If
    End Sub

    Public Property WindChaserCount(ByVal key As String) As Integer
        'Chaser count is in addition to the vector itself
        Get
            If Winds.ContainsKey(key) Then
                Return Winds.Item(key).MaxCount - 1
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Integer)
            If Winds.ContainsKey(key) Then
                If value >= 0 Then
                    Winds.Item(key).MaxCount = value + 1
                Else
                    Winds.Item(key).MaxCount = 1
                End If
            End If
        End Set
    End Property


    Public Property WindChaserFf(ByVal key As String) As Single
        Get
            If Winds.ContainsKey(key) Then
                Return CType(Winds.Item(key).FadeFactor, Single)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Single)
            If Winds.ContainsKey(key) Then Winds.Item(key).FadeFactor = value
        End Set
    End Property

    Public Sub SetWindLabel(ByVal LabelIndex As Integer, ByVal key As String)
        
        Select Case LabelIndex
            Case 1
                LeftWindLabelKey = key
            Case 2
                RightWindLabelKey = key
            Case Else 'No valid Index- so, if is a valid key- point it away from a valid index 
                If Winds.ContainsKey(key) Then
                    If LeftWindLabelKey = key Then LeftWindLabelKey = ImpossibleKey
                    If RightWindLabelKey = key Then RightWindLabelKey = ImpossibleKey
                End If
        End Select

        SynchWindLabels()

    End Sub

    Private Sub SynchWindLabels()
        lblTrueWindAzimuthLeft.Text = ""
        lblTrueWindSpeedLeft.Text = ""
        lblTrueWindLeft.Text = ""
        lblTrueWindAzimuthRight.Text = ""
        lblTrueWindSpeedRight.Text = ""
        lblTrueWindRight.Text = ""

        If Winds.ContainsKey(LeftWindLabelKey) Then
            lblTrueWindLeft.Text = "True Wind " & LeftWindLabelKey
            lblTrueWindAzimuthLeft.ForeColor = Winds.Item(LeftWindLabelKey).LineColor
            lblTrueWindSpeedLeft.ForeColor = Winds.Item(LeftWindLabelKey).LineColor
            lblTrueWindLeft.ForeColor = Winds.Item(LeftWindLabelKey).LineColor
        End If

        If Winds.ContainsKey(RightWindLabelKey) Then
            lblTrueWindRight.Text = "True Wind " & RightWindLabelKey
            lblTrueWindAzimuthRight.ForeColor = Winds.Item(RightWindLabelKey).LineColor
            lblTrueWindSpeedRight.ForeColor = Winds.Item(RightWindLabelKey).LineColor
            lblTrueWindRight.ForeColor = Winds.Item(RightWindLabelKey).LineColor
        End If

    End Sub
    Private Function EaseCubic(ByVal t As integer, ByVal b As Single, ByVal c As Single, ByVal d As Integer) as Single
        dim f As Single = Convert.ToSingle(t/(d/2))
        't = t / (d/2)
        If (f < 1) Then
            Return c / 2 * f*f*f + b
        End If
        f = f - 2
        return c/2*((f)*f*f + 2) + b
    End Function

    Private Function SignedAngularDifference(ByVal x As Single, ByVal y As Single) As single
        dim a As Single = y-x
        dim b as Single = ((a+180) MOD 360) - 180
        'if(Math.Abs(b) > 180) Then
        Return b
        'End If
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Refresh()
    End Sub

End Class
