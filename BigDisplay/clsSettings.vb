Imports System.ComponentModel
Imports System.Xml.Serialization

Public Enum MetPriorityEnum
    Bow
    Trans
End Enum
Public Enum GpsFormatEnum
    GGARaw
    DD_MM_SS
    SignedDecimal
End Enum
Public Class clsSettings
    Public Property AnimationFrames As Integer = 90
    Public Property AnimationFrameRate as Integer = 8

    <XmlElement(GetType(XmlColor))>
    Public Property SS_BackColor As Color = Color.LightSkyBlue
    '<XmlElement(NameOf(SS_BackColor))>
    '<Browsable(False)>
    'public Property SS_BackColor_RGB as Integer
    '    Get
    '        Return SS_BackColor.ToArgb()
    '    End Get
    '    Set(value as Integer)
    '        SS_BackColor = Color.FromArgb(value)
    '    End Set
    'End Property

    
    <XmlElement(GetType(XmlColor))>
    Public Property SS_RoseColor As Color = Color.Azure
    <XmlElement(GetType(XmlColor))>
    Public Property SS_ShipVectorColor As Color = Color.Gold
    <XmlElement(GetType(XmlColor))>
    Public Property SS_ShipColor As Color = Color.Black
    <XmlElement(GetType(XmlColor))>
    Public Property SS_WindColorStbd As Color = Color.ForestGreen
    <XmlElement(GetType(XmlColor))>
    Public Property SS_WindColorPort As Color = Color.DarkRed
    <XmlElement(GetType(XmlColor))>
    Public Property SS_WindColorBow As Color = Color.DarkBlue
    <XmlElement(GetType(XmlColor))>
    Public Property SS_WindColorPS As Color = Color.Black

    public Property HeadingChannel As Int16 = 16004
    public Property HeadingQualifier As String = "$HEHDT"
    Public Property HeadingDelimiter As Char = ","c
    Public Property HeadingMinLength As Integer = 2
    Public Property HeadingIndex As Int16 = 1

    Public Property SpdLogChannel As Int16 = 16014
    Public Property SpdLogQualifier As String = "$VBW"
    Public Property SpdLogDelimiter As Char = ","c
    Public Property SpdLogMinLength As Integer = 2
    Public Property SpdLogVelocityLongitudinalIndex As Int16 = 1
    Public Property SpdLogVelocityTransverseIndex As Int16 = 2
    Public Property SpdLogDataValidIndex As Int16 = 3

    Public Property SpdLogVectorColor As Color = Color.Aquamarine


    Public Property WindChannel As Int16 = 16007
    public Property WindQualifier As String = "$PTRUEWIND"
    Public Property WindDelimiter As Char = ","c
    Public Property WindMinLength As Integer = 9
    Public Property WindSpdIndex_Stbd As Int16 = 2
    Public Property WindDirIndex_Stbd As Int16 = 3
    Public Property WindSpdIndex_Port As Int16 = 4
    Public Property WindDirIndex_Port As Int16 = 5
    Public Property WindSpdIndex_Bow As Int16 = 8
    Public Property WindDirIndex_Bow As Int16 = 9
    Public Property WindSpdIndex_PS As Int16 = 10
    Public Property WindDirIndex_PS As Int16 = 11

    Public Property GpsFormat As GpsFormatEnum = GpsFormatEnum.SignedDecimal

    Public Property GpsChannel As Int16 = 16002
    public Property GpsQualifier_GGA As String = "$GPGGA"
    public Property GpsQualifier_VTG As String = "$GPVTG"
    Public Property GpsDelimiter As Char = ","c
    Public Property GpsMinLength_GGA As Integer = 12
    Public Property GpsMinLength_VTG As Integer = 6
    Public Property GpsIndex_Lat As Int16 = 2
    Public Property GpsIndex_LatNS As Int16 = 3
    Public Property GpsIndex_Lon As Int16 = 4
    Public Property GpsIndex_LonEW As Int16 = 5
    Public Property GpsIndex_Qual As Int16 = 6
    Public Property GpsLabel_Qual As String = "Fix Quality"
    Public Property GpsIndex_COG As Int16 = 1
    Public Property GpsNumFormat_COG As String = "0"
    Public Property GpsLabel_COG As String = "COG (^176^T)"
    Public Property GpsIndex_SOG As Int16 = 5
    Public Property GpsNumFormat_SOG As String = "0.0"
    Public Property GpsLabel_SOG As String = "SOG (Kts)"

    Public Property MetPriority As MetPriorityEnum = MetPriorityEnum.Bow 

    Public Property MetChannel As Int16 = 16006
    public Property MetQualifier As String = "$PMET"
    Public Property MetDelimiter As Char = ","c
    Public Property MetMinLength As Integer = 11
    Public Property MetIndex_Temp As Int16 = 1
    Public Property MetNumFormat_Temp As String = "0.0"
    Public Property MetLabel_Temp As String = "" 
    Public Property MetIndex_WetB As Int16 = 9
    Public Property MetNumFormat_WetB As String = "0.0"
    Public Property MetLabel_WetB As String = "Wet Bulb T (^176^C)"
    Public Property MetIndex_SST5 As Int16 = 11
    Public Property MetNumFormat_SST5 As String = "0.0"
    Public Property MetLabel_SST5 As String = "Sea Temp 5m (^176^C)"
    Public Property MetIndex_SST1 As Int16 = 10
    Public Property MetNumFormat_SST1 As String = "0.0"
    Public Property MetLabel_SST1 As String = "Sea Temp 1m (^176^C)"
    Public Property MetIndex_Baro As Int16 = 5
    Public Property MetNumFormat_Baro As String = "0"
    Public Property MetLabel_Baro As String = "Baro Pres (mBar)"
    Public Property MetIndex_Hum As Int16 = 3
    Public Property MetNumFormat_Hum As String = "0"
    Public Property MetLabel_Hum As String = "Rel Humidity (%)"

    Public Property DepthChannel As Int16 = 16008
    public Property DepthQualifier As String = "$PKEL99"
    Public Property DepthDelimiter As Char = ","c
    Public Property DepthMinLength As Integer = 6
    Public Property DepthIndex As Int16 = 6
    Public Property DepthLabel As String = "Depth"
    Public Property DepthNumFormat As String = "0"

    Public Property TsalChannel As Int16 = 16009
    public Property TsalQualifier As String = ""
    Public Property TsalDelimiter As Char = " "C
    Public Property TsalMinLength As Integer = 5
    Public Property TsalIndex_Sal As Int16 = 2
    Public Property TsalNumFormat_Sal As String = "0.00"
    Public Property TsalLabel_Sal As String = "Sea Salinity (PSU)"
    Public Property TsalIndex_Fluor As Int16 = 4
    Public Property TsalNumFormat_Fluor As String = "0.00"
    Public Property TsalLabel_Fluor As String = "Fluor (Volts)"
    Public Property TsalIndex_Temp As Int16 = 3
    Public Property TsalNumFormat_Temp As String = "0.0"
    Public Property TsalLabel_Temp As String = "Sea Temp Intake (^176^C)"

    Public Property BowMetChannel As Int16 = 16011
    public Property BowMetQualifier As String = "$WIXDR"
    Public Property BowMetDelimiter As Char = ","c
    Public Property BowMetMinLength As Integer = 12
    Public Property BowMetIndex_Temp As Int16 = 2
    Public Property BowMetNumFormat_Temp As String = "0.0"
    Public Property BowMetLabel_Temp As String = "Air Temp (^176^C)"
    Public Property BowMetIndex_Baro As Int16 = 10
    Public Property BowMetNumFormat_Baro As String = "0"
    Public Property BowMetLabel_Baro As String = "Baro Pres (mBar)"
    Public Property BowMetIndex_Hum As Int16 = 6
    Public Property BowMetNumFormat_Hum As String = "0"
    Public Property BowMetLabel_Hum As String = "Rel Humidity (%)"

    Public Property RadChannel As Int16 = 16012
    public Property RadQualifier As String = "$WIR01"
    Public Property RadDelimiter As Char = ","c
    Public Property RadMinLength As Integer = 11
    Public Property RadIndex_PIR As Int16 = 5
    Public Property RadLabel_PIR As String = "PIR W/m^178^"
    Public Property RadNumFormat_PIR As String = "0"
    Public Property RadIndex_PSP As Int16 = 8
    Public Property RadLabel_PSP As String = "PSP W/m^178^"
    Public Property RadNumFormat_PSP As String = "0"

    Public Property AttitudeChannel As Int16 = 16005
    public Property AttitudeQualifier As String = "$GPPAT"
    Public Property AttitudeDelimiter As Char = ","c
    Public Property AttitudeMinLength As Integer = 10
    Public Property AttitudeIndex_Roll As Int16 = 2
    Public Property AttitudeIndex_Pitch As Int16 = 3
    Public Property AttitudeIndex_Quality as Int16 = 4
    Public Property AttitudePitchDegreesPerTick As Single = 4
    Public Property AttitudeQualityMax As Single = 2.0
    Public Property AttitudeAnimationFrames As Integer = 20
    Public Property AttitudeAnimationFrameRate As Integer = 18
    <XmlElement(GetType(XmlColor))>
    Public Property AttitudeRoseColor As Color = Color.Black
    <XmlElement(GetType(XmlColor))>
    Public Property AttitudeHorizonColor As Color = Color.MidnightBlue
    <XmlElement(GetType(XmlColor))>
    Public Property AttitudeWaterColor As Color = Color.CornflowerBlue
    <XmlElement(GetType(XmlColor))>
    Public Property AttitudeTargetColor As Color = Color.Brown


    Public Function Clone() As clsSettings
        Return DirectCast(Me.MemberwiseClone(), clsSettings)
    End Function
End Class
