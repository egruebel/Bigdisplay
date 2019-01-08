'
'   Date        Version     Comments
'   ----        -------     --------
'   25 Mar 09   0.2.0.1     Changed depth index from 2 to 6, added refresh to clear: gyro, wind and made good vector
'   16 May 09   0.2.0.3     Added 3rd Wind, patch for lost RMY met. on 16006
Imports System.Xml
Imports ShipState

Public Class frmMain

    'Private settings as New clsSettings

    Const KeyStbd As String = "STBD"
    Const KeyPort As String = "PORT"
    Const KeyBow1 As String = "BOW1"
    Const KeyBow2 As String = "Bow"
    Const KeyPS As String = "Mast"
    Const UdpErrorString As String = "UDP Channel Error"

    Private ChanGyro As clsData
    Private ChanWind As clsData
    Private ChanGps As clsData
    Private ChanMet As clsData
    Private ChanDepth As clsData
    Private ChanTsal As clsData
    Private ChanRmyBow As clsData
    Private ChanRad As clsData
    Private ChanAttitude As clsData

    Private lwLatLon As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwSog As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwCog As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwQual As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwAirT As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwRHum As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwBaro As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwWetB As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwSST1 As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwSST5 As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwSSTF As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwSal As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwFluor As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwPIR As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwPSP As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)
    Private lwDepth As New ctlLabeledWindow(ctlLabeledWindow.LabelLocations.Top)

    Private lw(,) As ctlLabeledWindow = _
    { _
        {lwSog, lwCog, lwQual}, _
        {lwAirT, lwRHum, lwBaro}, _
        {lwWetB, lwPIR, lwPSP}, _
        {lwSST1, lwSST5, lwSSTF}, _
        {lwSal, lwFluor, lwDepth} _
    }



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        scMain.Panel1.SuspendLayout()
        scMain.Panel2.SuspendLayout()
        SuspendLayout()

        Me.Text = My.Application.Info.AssemblyName & "  v" & My.Application.Info.Version.ToString

        'read settings from xml
        ReadSettingsFromFile()

        ' Splitter fills the form, ShipState fills left panel
        scMain.Dock = DockStyle.Fill
        scMain.Panel1.Controls.Add(ss)
        ss.Dock = DockStyle.Fill

        ss.WindAdd(KeyStbd, settings.SS_WindColorStbd)
        'ss.SetWindLabel(2, KeyStbd)

        ss.WindAdd(KeyPort, settings.SS_WindColorPort)
        'ss.SetWindLabel(1, KeyPort)

        ss.WindAdd(KeyBow2, settings.SS_WindColorBow)
        ss.SetWindLabel(2, KeyBow2)

        ss.WindAdd(KeyPS, settings.SS_WindColorPS)
        ss.SetWindLabel(1, KeyPS)

        ' Set up form on screen
        Me.MinimumSize = New Size(150, 100)

        ' Right panel
        CreateWindows()

        ApplySettings()

        scMain.Panel1.ResumeLayout(False)
        scMain.Panel2.ResumeLayout(False)
        ResumeLayout(False)

        SetInitialPositionSize()

        Me.Show()

        ' Open data channels
        ChanGyro = New clsData("Gyro", settings.HeadingChannel, AddressOf ParseGyro, 5, AddressOf ClearGyro) '16004
        ChanWind = New clsData("True Wind", settings.WindChannel, AddressOf ParseWind, 5, AddressOf ClearWind) '16007
        ChanGps = New clsData("GPS", settings.GpsChannel, AddressOf ParseGps, 5, AddressOf ClearGPS) '16002
        ChanMet = New clsData("RM Young", settings.MetChannel, AddressOf ParseMet, 10, AddressOf ClearMet) '16006
        ChanDepth = New clsData("Depth", settings.DepthChannel, AddressOf ParseDepth, 5, AddressOf ClearDepth) '16008
        ChanTsal = New clsData("Tsal", settings.TsalChannel, AddressOf ParseTsal, 8, AddressOf ClearTsal) '16009
        ChanRmyBow = New clsData("RmyBow", settings.BowMetChannel, AddressOf ParseRmyBow, 5, AddressOf ClearRmyBow) '16011
        ChanRad = New clsData("Rad", settings.RadChannel, AddressOf ParseRad, 5, AddressOf ClearRad) '16012
        ChanAttitude = New clsData("Rad", settings.AttitudeChannel, AddressOf ParseAttitude, 5, AddressOf ClearAttitude) '16012


    End Sub
    Public Sub ApplySettings
        ' Animation setup
        ss.AnimationFrames = settings.AnimationFrames
        ss.AnimationFrameRate = settings.AnimationFrameRate

        ' Set up ShipState
        ss.BackColor = settings.SS_BackColor
        ss.RoseColor = settings.SS_RoseColor
        ss.ShipVectorColor = settings.SS_ShipVectorColor
        ss.ShipColor = settings.SS_ShipColor
        ss.AttitudeDisplay.RoseColor = settings.AttitudeRoseColor
        ss.AttitudeDisplay.TargetColor = settings.AttitudeTargetColor
        ss.AttitudeDisplay.HorizonColor = settings.AttitudeHorizonColor
        ss.AttitudeDisplay.WaterColor = settings.AttitudeWaterColor
        ss.SetWindColor(KeyPort, settings.SS_WindColorPort)
        ss.SetWindColor(KeyStbd, settings.SS_WindColorStbd)
        ss.SetWindColor(KeyBow2, settings.SS_WindColorBow)
        ss.SetWindColor(KeyPS, settings.SS_WindColorPS)

        'Label boxes
        lwSog.lblLabel.Text = FormatLabel(settings.GpsLabel_SOG)
        lwCog.lblLabel.Text = FormatLabel(settings.GpsLabel_COG)
        lwQual.lblLabel.Text = FormatLabel(settings.GpsLabel_Qual)
        lwAirT.lblLabel.Text = FormatLabel(settings.BowMetLabel_Temp)
        lwRHum.lblLabel.Text = FormatLabel(settings.BowMetLabel_Hum)
        lwBaro.lblLabel.Text = FormatLabel(settings.BowMetLabel_Baro)
        lwWetB.lblLabel.Text = FormatLabel(settings.MetLabel_WetB)
        lwPIR.lblLabel.Text = FormatLabel(settings.RadLabel_PIR)
        lwPSP.lblLabel.Text = FormatLabel(settings.RadLabel_PSP)
        lwSST1.lblLabel.Text = FormatLabel(settings.MetLabel_SST1)
        lwSST5.lblLabel.Text = FormatLabel(settings.MetLabel_SST5)
        lwSSTF.lblLabel.Text = FormatLabel(settings.TsalLabel_Temp)
        lwSal.lblLabel.Text = FormatLabel(settings.TsalLabel_Sal)
        lwFluor.lblLabel.Text = FormatLabel(settings.TsalLabel_Fluor)
        lwDepth.lblLabel.Text = FormatLabel(settings.DepthLabel)
    End Sub

    Public Property settings as clsSettings = New clsSettings

    Private Sub CreateWindows()

        scMain.Panel2.Controls.Add(lwSog)
        scMain.Panel2.Controls.Add(lwCog)
        scMain.Panel2.Controls.Add(lwQual)
        scMain.Panel2.Controls.Add(lwAirT)
        scMain.Panel2.Controls.Add(lwRHum)
        scMain.Panel2.Controls.Add(lwBaro)
        scMain.Panel2.Controls.Add(lwWetB)
        scMain.Panel2.Controls.Add(lwPIR)
        scMain.Panel2.Controls.Add(lwPSP)
        scMain.Panel2.Controls.Add(lwSST1)
        scMain.Panel2.Controls.Add(lwSST5)
        scMain.Panel2.Controls.Add(lwSSTF)
        scMain.Panel2.Controls.Add(lwSal)
        scMain.Panel2.Controls.Add(lwFluor)
        scMain.Panel2.Controls.Add(lwDepth)
        scMain.Panel2.Controls.Add(lwLatLon)

        For Each lw As ctlLabeledWindow In scMain.Panel2.Controls
            lw.lblLabel.BorderStyle = BorderStyle.None
            lw.lblWindow.Text = ""
            lw.lblWindow.BorderStyle = BorderStyle.Fixed3D
            lw.WindowPercent = 0.7
            lw.LabelFontSizeMult = 0.75
            lw.WindowFontSizeMult = 0.85
        Next

        ' Do this after loop, Lat/Lon window is different
        lwLatLon.WindowPercent = 0.9
        lwLatLon.WindowBorderStyle = BorderStyle.None
        lwLatLon.LabelText = ""
        lwLatLon.Text = vbLf ' So it starts out as two lines of text 

    End Sub
    Function FormatLabel(ByVal label As String) As String
        'if empty or null
        If String.IsNullOrEmpty(label) then Return "" 
        'get start index
        Dim replaceStart as Integer = label.IndexOf("^")
        If(replaceStart < 0) Then Return label
        'get end index
        Dim replaceEnd as Integer = label.IndexOf("^",replaceStart + 1)
        If(replaceStart < 0) then Return label

        Dim intString as String = Mid(label,replaceStart+2, (replaceEnd - replaceStart)-1)
        Dim intVal as Integer
        Dim utf8Symbol as String
        If Integer.TryParse(intString,intVal) Then
            utf8Symbol = Chr(intVal)
        End If
        'recursion!!
        Dim newLabel = Strings.Left(label,replaceStart) & utf8Symbol & Strings.Right(label, (label.Length - replaceEnd) - 1)
        Return FormatLabel(newLabel)
    End Function
    Private Sub ParseAttitude(ByVal s As String)
        Dim buffer() As String
        Dim buf() As String
        Static Data As String

        Data &= s
        buffer = ExtractStrings(Data, vbCrLf, 100)

        buf = ParseLatest(buffer, settings.AttitudeQualifier, settings.AttitudeDelimiter)
        If buf.Length >= settings.AttitudeMinLength Then
            Dim roll As Single = CSng(Val(buf(settings.AttitudeIndex_Roll)))
            Dim pitch As Single = CSng(Val(buf(settings.AttitudeIndex_Pitch)))
            Dim qual As Single = CSng(Val(buf(settings.AttitudeIndex_Quality)))
            If(qual <= settings.AttitudeQualityMax) Then
                'do something
                ss.SetAttitude(roll, pitch)
            End If
        End If
    End Sub
    Private Sub ParseTsal(ByVal s As String)

        Dim buf() As String
        Static delim() As String = {settings.TsalDelimiter}

        buf = s.Split(delim, StringSplitOptions.RemoveEmptyEntries)
        If buf.Length >= settings.TsalMinLength Then
            lwSal.Text = SafeFormat(buf(settings.TsalIndex_Sal), settings.TsalNumFormat_Sal)
            lwFluor.Text = SafeFormat(buf(settings.TsalIndex_Fluor), settings.TsalNumFormat_Fluor)
            lwSSTF.Text = SafeFormat(buf(settings.TsalIndex_Temp), settings.TsalNumFormat_Temp)

        End If

    End Sub
    Private Sub ParseRad(ByVal s As String)
        Dim buffer() As String
        Dim buf() As String
        Static Data As String

        Data &= s
        buffer = ExtractStrings(Data, vbCrLf, 100)

        buf = ParseLatest(buffer, settings.RadQualifier, settings.RadDelimiter)

        If buf.Length >= settings.RadMinLength Then
            lwPIR.Text = SafeFormat(buf(settings.RadIndex_PIR), settings.RadNumFormat_PIR)
            lwPSP.Text = SafeFormat(buf(settings.RadIndex_PSP), settings.RadNumFormat_PSP)
        End If
    End Sub
    Private Sub ClearRad()
        lwPIR.Text = ""
        lwPSP.Text = ""
    End Sub
    Private Sub ParseDepth(ByVal s As String)

        Dim buffer() As String
        Dim buf() As String
        Static Data As String

        Data &= s
        buffer = ExtractStrings(Data, vbCrLf, 100)

        buf = ParseLatest(buffer, settings.DepthQualifier, settings.DepthDelimiter)
        If buf.Length >= settings.DepthMinLength Then
            lwDepth.Text = SafeFormat(buf(settings.DepthIndex), settings.DepthNumFormat)
        End If

    End Sub

    Private Sub ParseWind(ByVal s As String)

        Dim buffer() As String
        Dim buf() As String
        Static Data As String

        Data &= s
        buffer = ExtractStrings(Data, vbCrLf, 100)

        buf = ParseLatest(buffer, settings.WindQualifier, settings.WindDelimiter)

        If buf.Length >= settings.WindMinLength Then
            'stbd
            Dim WindSpd As Single = CSng(Val(buf(settings.WindSpdIndex_Stbd)))
            Dim WindDir As Single = CSng(Val(buf(settings.WindDirIndex_Stbd)))
            ss.SetWind(KeyStbd, WindDir, WindSpd)
            'port
            WindSpd = CSng(Val(buf(settings.WindSpdIndex_Port)))
            WindDir = CSng(Val(buf(settings.WindDirIndex_Port)))
            ss.SetWind(KeyPort, WindDir, WindSpd)
            'bow2
            WindSpd = CSng(Val(buf(settings.WindSpdIndex_Bow)))
            WindDir = CSng(Val(buf(settings.WindDirIndex_Bow)))
            ss.SetWind(KeyBow2, WindDir, WindSpd)
            'PS
            WindSpd = CSng(Val(buf(settings.WindSpdIndex_PS)))
            WindDir = CSng(Val(buf(settings.WindDirIndex_PS)))
            ss.SetWind(KeyPS, WindDir, WindSpd)

            ss.WindVisible(KeyStbd) = True
            ss.WindVisible(KeyPort) = True
            ss.WindVisible(KeyBow2) = True
            ss.WindVisible(KeyPS) = True
            'ss.Refresh()
        End If

    End Sub

    Private Sub ParseGyro(ByVal s As String)

        Dim buffer() As String
        Dim buf() As String
        Static Data As String

        Data &= s
        buffer = ExtractStrings(Data, vbCrLf, 100)

        buf = ParseLatest(buffer, settings.HeadingQualifier, settings.HeadingDelimiter)
        If buf.Length >= settings.HeadingMinLength Then
            ss.SetHeading(CSng(Val(buf(settings.HeadingIndex))))
            ss.ShipVisible = True
            'ss.Refresh()
        End If

    End Sub

    Private Sub ParseGps(ByVal s As String)

        Dim buffer() As String
        Dim buf() As String
        Static Data As String

        Data &= s
        buffer = ExtractStrings(Data, vbCrLf, 500)

        buf = ParseLatest(buffer, settings.GpsQualifier_GGA, settings.GpsDelimiter)
        If buf.Length >= settings.GpsMinLength_GGA Then
            lwQual.Text = buf(settings.GpsIndex_Qual)
            Dim lat as String = buf(settings.GpsIndex_Lat)
            Dim lon as String = buf(settings.GpsIndex_Lon)
            Dim ns as String = buf(settings.GpsIndex_LatNS)
            Dim ew as String = buf(settings.GpsIndex_LonEW)
            Select Case settings.GpsFormat
                Case GpsFormatEnum.GGARaw
                    lwLatLon.Text = FormatNmeaLL(lat) & 
                                    " " & 
                                    ns & 
                                    vbCrLf & 
                                    FormatNmeaLL(lon) & 
                                    " " & 
                                    ew
                Case GpsFormatEnum.SignedDecimal
                    lwLatLon.Text = GpsDecoder.ToSignedDecimal(lat,ns) & vbCrLf & GpsDecoder.ToSignedDecimal(lon,ew)
                Case GpsFormatEnum.DD_MM_SS
                    lwLatLon.Text = GpsDecoder.ToDegMinSec(lat,ns) & vbCrLf & GpsDecoder.ToDegMinSec(lon,ew)
            End Select

        End If

        buf = ParseLatest(buffer, settings.GpsQualifier_VTG, settings.GpsDelimiter)
        If buf.Length >= settings.GpsMinLength_VTG Then
            Dim Cog As Single = CSng(Val(buf(settings.GpsIndex_COG)))
            Dim Sog As Single = CSng(Val(buf(settings.GpsIndex_SOG)))
            ss.SetCogSog(Cog, Sog)
            lwCog.Text = SafeFormat(buf(settings.GpsIndex_COG), settings.GpsNumFormat_COG)
            lwSog.Text = SafeFormat(buf(settings.GpsIndex_SOG), settings.GpsNumFormat_SOG)
        End If

        ss.ShipVectorVisible = True
        'ss.Refresh()

    End Sub

    Private Sub ParseRmyBow(ByVal s As String)
        Dim buffer() As String
        Dim buf() As String
        Static Data As String

        Data &= s
        buffer = ExtractStrings(Data, vbCrLf, 100)

        buf = ParseLatest(buffer, settings.BowMetQualifier, settings.BowMetDelimiter)
        If buf.Length >= settings.BowMetMinLength Then
            If(settings.MetPriority = MetPriorityEnum.Bow) Then
                lwAirT.Text = SafeFormat(buf(settings.BowMetIndex_Temp), settings.BowMetNumFormat_Temp) 
                lwBaro.Text = SafeFormat(buf(settings.BowMetIndex_Baro), settings.BowMetNumFormat_Baro)
                lwRHum.Text = SafeFormat(buf(settings.BowMetIndex_Hum), settings.BowMetNumFormat_Hum)
            End If
            
        End If
    End Sub

    Private Sub ParseMet(ByVal s As String)

        Dim buffer() As String
        Dim buf() As String
        Static Data As String

        Data &= s
        buffer = ExtractStrings(Data, vbCrLf, 200)

        buf = ParseLatest(buffer, settings.MetQualifier, settings.MetDelimiter)
        If buf.Length >= settings.MetMinLength Then
            If settings.MetPriority = MetPriorityEnum.Trans Then
                lwAirT.Text = SafeFormat(buf(settings.MetIndex_Temp), settings.MetNumFormat_Temp)
                lwBaro.Text = SafeFormat(buf(settings.BowMetIndex_Baro), settings.MetNumFormat_Baro)
                lwRHum.Text = SafeFormat(buf(settings.BowMetIndex_Hum), settings.MetNumFormat_Hum)
            End If

            lwWetB.Text = SafeFormat(buf(settings.MetIndex_WetB), settings.MetNumFormat_WetB) '& deg '& dot & StrCtoF(buf(9), 0) & deg
            lwSST5.Text = SafeFormat(buf(settings.MetIndex_SST5), settings.MetNumFormat_SST5) '& deg
            lwSST1.Text = SafeFormat(buf(settings.MetIndex_SST1), settings.MetNumFormat_SST1) ' & deg

        End If

    End Sub

    Private Function ExtractStrings(ByRef data As String, ByVal Term As String, ByVal MaxDataSize As Integer) As String()

        Dim buffer() As String

        buffer = Strings.Split(data, Term)
        data = buffer(buffer.GetUpperBound(0))
        If data.Length > MaxDataSize Then data = ""
        Array.Resize(Of String)(buffer, buffer.Length - 1)

        Return buffer

    End Function

    Private Function ParseLatest(ByVal buffer() As String, ByVal Ident As String, ByVal delim As String) As String()

        For i As Integer = buffer.GetUpperBound(0) To 0 Step -1
            Dim pos As Integer = buffer(i).LastIndexOf(Ident)

            If pos >= 0 Then
                ' mid() is 1 based, LastIndexOf() is 0 based
                Return Strings.Split(Strings.Mid(buffer(i), pos + 1), delim)
            End If
        Next

        Dim null() As String = {""}
        Return null

    End Function

    Private Function SafeFormat(ByVal s As String, ByVal FmtStr As String) As String

        If IsNumeric(s) Then
            SafeFormat = Strings.Format(Val(s), FmtStr)
        Else
            SafeFormat = ""
        End If

    End Function
    Private Function FormatNmeaLL(ByVal s As String) As String

        Dim p As Integer
        Dim min As String
        Dim deg As String

        'IntPart = fix(nmea / 100.0);
        'dec = IntPart + (nmea - IntPart * 100.0) / 60.0;


        p = InStr(s, ".")
        If p = 0 Then p = Len(s) + 1
        If p < 4 Then
            FormatNmeaLL = ""
            Exit Function
        End If
        deg = Strings.Left(s, p - 3)
        If Strings.Left(deg, 1) = "0" Then deg = Mid(deg, 2)
        min = Mid(s, p - 2, 6)
        FormatNmeaLL = deg & Chr(176) & " " & min & "'"

    End Function
    Private Function StrCtoF(ByVal DegC As String, ByVal prec As Integer) As String
        Return String.Format("{0:0.0}", 32 + Val(DegC) * 1.8)
    End Function

    Private Sub SetInitialPositionSize()

        Me.Location = My.Settings.MainFormLocation
        Me.Width = Math.Max(Me.MinimumSize.Width, My.Settings.MainFormSize.Width)
        Me.Height = Math.Max(Me.MinimumSize.Height, My.Settings.MainFormSize.Height)
        scMain.SplitterDistance = My.Settings.SplitterDistance

    End Sub

    Private Sub SaveInitialPositionSize()

        Try
            If WindowState <> FormWindowState.Minimized Then
                My.Settings.MainFormSize = Me.Size
                My.Settings.MainFormLocation = Me.Location
                My.Settings.SplitterDistance = scMain.SplitterDistance
            End If
        Catch
        End Try
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        SaveInitialPositionSize()

        Try
            ChanGps.Close()
            ChanWind.Close()
            ChanGyro.Close()
            ChanMet.Close()
            ChanDepth.Close()
            ChanTsal.Close()
        Catch
        End Try
    End Sub

    Private Sub LayoutWindows() Handles scMain.SplitterMoved, scMain.Layout

        If lw Is Nothing Then Exit Sub
        If lwLatLon Is Nothing Then Exit Sub

        Dim RowCount As Integer = lw.GetLength(0)
        Dim ColCount As Integer = lw.GetLength(1)
        Dim x As Integer

        With scMain.Panel2.ClientRectangle
            lwLatLon.Bounds = New Rectangle(0, 0, .Width, CInt(.Height * 0.25))

            Dim RowGap As Integer = CInt(.Height * 0.025)
            Dim ColGap As Integer = CInt(.Width * 0.01)
            Dim margin As Integer = CInt(.Width * 0.02)
            Dim w As Integer = CInt((.Width - 2 * margin - (ColCount - 1) * ColGap) / ColCount)
            Dim h As Integer = CInt((.Height - ((RowCount - 1) + 3) * RowGap - lwLatLon.Height) / RowCount)

            Dim y As Integer = lwLatLon.Bottom + 2 * RowGap ' Start below Lat/Lon
            For i = 0 To lw.GetUpperBound(0)
                For j As Integer = 0 To lw.GetUpperBound(1)
                    x = margin + j * (ColGap + w)
                    If lw(i, j) IsNot Nothing Then lw(i, j).Bounds = New Rectangle(x, y, w, h)
                Next
                y = lw(i, 0).Bottom + RowGap
            Next
        End With
    End Sub

    Private Sub ClearDepth()
        lwDepth.Text = ""
    End Sub

    Private Sub ClearGPS()
        ss.ShipVectorVisible = False
        'ss.Refresh()
        lwLatLon.Text = ""
        lwCog.Text = ""
        lwSog.Text = ""
        lwQual.Text = ""
    End Sub

    Private Sub ClearTsal()
        lwSal.Text = ""
        lwFluor.Text = ""
        lwSSTF.Text = ""
    End Sub
    Private Sub ClearRmyBow()
        lwAirT.Text = ""
        lwBaro.Text = ""
        lwRHum.Text = ""
    End Sub

    Private Sub ClearMet()
        lwAirT.Text = ""
        lwBaro.Text = ""
        lwFluor.Text = ""
        lwPIR.Text = ""
        lwPSP.Text = ""
        lwRHum.Text = ""
        lwSal.Text = ""
        lwSST1.Text = ""
        lwSST5.Text = ""
        lwWetB.Text = ""
    End Sub

    Private Sub ClearWind()
        ss.WindVisible(KeyStbd) = False
        ss.WindVisible(KeyPort) = False
        ss.WindVisible(KeyBow2) = False
        ss.WindVisible(KeyPS) = False
        'ss.Refresh()
    End Sub

    Private Sub ClearGyro()
        ss.ShipVisible = False
        'ss.Refresh()
    End Sub
    Private Sub ClearAttitude()
        'ss.AttitudeVisible = False
    End Sub

    Private Sub ReadSettingsFromFile()
        Dim serializer As new Xml.Serialization.XmlSerializer(settings.GetType)
        Try
            Using reader As XmlReader = XmlReader.Create("BigDisplay.Settings.xml")
                settings = CType(serializer.Deserialize(reader), clsSettings)
            End Using
        Catch ex As Exception
            settings = New clsSettings()
            SaveSettingsToFile()
        End Try
    End Sub
    Private Sub SaveSettingsToFile()
        Dim serializer As new Xml.Serialization.XmlSerializer(settings.GetType)
        Dim writerSettings as new XmlWriterSettings
        writerSettings.Indent = true
        Using writer As XmlWriter = XmlWriter.Create("BigDisplay.Settings.xml", writerSettings)
            serializer.Serialize(writer, settings)
        End Using
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Dim settingsDialog as New SettingsDialog
        settingsDialog.CallerWindow = me
        Dim result = settingsDialog.ShowDialog()
        If(result = DialogResult.OK) Then
            SaveSettingsToFile()
        End If
    End Sub
End Class
