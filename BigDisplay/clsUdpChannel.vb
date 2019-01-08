Imports System.Windows
Imports System.Net
Imports System.Net.Sockets
Imports System.Text


Public Class clsUdpChannel

    Public Event OpenSuccess(ByVal Identifier As String)
    Public Event OpenFailure(ByVal Identifier As String, ByVal ErrMsg As String)
    Public Delegate Sub ConsumerDelegate(ByVal s As String)
    
    Private _LastErrorText As String = ""
    Private _TimeOfLastRx As DateTime
    Private _ListenPort As Integer

    Private Consumer As ConsumerDelegate
    Private f As Form
    Private UdpState As UdpClientState = New UdpClientState

    Friend Sub ReceiveCallback(ByVal ar As IAsyncResult)

        ' Once we have chosen to close the port, stop processing messages
        If Not UdpState.IsOpen Then Exit Sub

        ' Get the data and write to screen, remember when
        Dim receiveBytes As Byte() = UdpState.u.EndReceive(ar, UdpState.e)
        Dim Buffer As String = Encoding.ASCII.GetString(receiveBytes)
        'Console.WriteLine(Buffer)
        _TimeOfLastRx = Now

        ' Pass the data along to specifeid consumer
        f.BeginInvoke(Consumer, New Object() {Buffer})

        ' Listen for the next data
        UdpState.u.BeginReceive(New AsyncCallback(AddressOf ReceiveCallback), UdpState)

    End Sub
    Friend Sub New(ByVal frm As Form, ByVal Method As ConsumerDelegate)

        f = frm
        Consumer = Method
        _TimeOfLastRx = Now

    End Sub

    Friend Sub OpenPort(ByVal ListenPort As Integer)

        _ListenPort = ListenPort

        UdpState.e = New IPEndPoint(IPAddress.Any, ListenPort)

        Try
            UdpState.u = New UdpClient(UdpState.e)
            UdpState.u.BeginReceive(New AsyncCallback(AddressOf ReceiveCallback), UdpState)
            UdpState.IsOpen = True
            RaiseEvent OpenSuccess(_ListenPort.ToString)
        Catch ex As System.Net.Sockets.SocketException
            Dim msg As String = ""
            Select Case ex.ErrorCode
                Case 10048
                    msg = "Cannot open UDP port " & ListenPort & " because it is already in use on this computer."
                Case Else
                    msg = "Cannot open UDP port: " & ListenPort & ". Error code = " & ex.ErrorCode.ToString
            End Select

            UdpState.IsOpen = False
            _LastErrorText = msg
            RaiseEvent OpenFailure(_ListenPort.ToString, msg)
        Finally
        End Try

    End Sub
    Friend Sub ClosePortUdp()

        UdpState.IsOpen = False

        ' Be sure the queue is empty before shutting down async receive
        System.Threading.Thread.Sleep(60)
        If Not UdpState.u Is Nothing Then UdpState.u.Close()

        UdpState.IsOpen = False

    End Sub

    Friend ReadOnly Property TimeOfLastRx() As DateTime
        Get
            Return _TimeOfLastRx
        End Get
    End Property

    Friend ReadOnly Property LastErrorText() As String
        Get
            Return _LastErrorText
            _LastErrorText = ""
        End Get
    End Property

    Friend ReadOnly Property IsOpen() As Boolean
        Get
            Return UdpState.IsOpen
        End Get
    End Property


    ''
    '' <Private class>
    ''

    Private Class UdpClientState

        Friend u As UdpClient
        Friend e As IPEndPoint
        Friend IsOpen As Boolean

        Friend ReadOnly Property IsOpenString() As String
            Get
                If IsOpen Then
                    Return "UDP port open"
                Else
                    Return "UDP port closed"
                End If
            End Get
        End Property

    End Class

    ''
    '' </Private class>
    ''

End Class