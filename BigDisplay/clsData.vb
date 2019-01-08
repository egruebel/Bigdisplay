
Public Class clsData

    Public Delegate Sub StaleDataDelegate()

    Private _Name As String
    Private _ListenPort As Integer
    Private _TimeoutSeconds As Integer

    Private ErrMsgBox As frmNonModelMsgBox
    Private StaleData As StaleDataDelegate

    Private WithEvents Chan As clsUdpChannel
    Private WithEvents tmr As Timer


    Friend Sub New(ByVal Name As String, ByVal ListenPort As Integer, ByVal Parser As clsUdpChannel.ConsumerDelegate, ByVal TimeoutSeconds As Integer, ByVal stale As StaleDataDelegate)

        _Name = Name
        _ListenPort = ListenPort
        _TimeoutSeconds = TimeoutSeconds
        StaleData = stale

        Chan = New clsUdpChannel(frmMain, Parser)
        Chan.OpenPort(_ListenPort)
        tmr = New Timer
        tmr.Interval = _TimeoutSeconds * 1000
        tmr.Enabled = True

    End Sub

    Friend Sub Close()
        Chan.ClosePortUdp()
    End Sub

    Private Sub HandleTimer(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmr.Tick

        If Not Chan.IsOpen Then Chan.OpenPort(_ListenPort)

        If DateDiff(DateInterval.Second, Chan.TimeOfLastRx, Now) > _TimeoutSeconds Then
            Debug.Print(_Name & " " & DateDiff(DateInterval.Second, Chan.TimeOfLastRx, Now).ToString)
            frmMain.BeginInvoke(StaleData)
        End If

    End Sub

    Private Sub HandleOpenSuccess(ByVal s As String) Handles Chan.OpenSuccess

        If ErrMsgBox IsNot Nothing Then
            ErrMsgBox.Close()
            ErrMsgBox.Dispose()
            ErrMsgBox = Nothing
        End If

    End Sub

    Private Sub HandleOpenFailure(ByVal Ident As String, ByVal ErrMsg As String) Handles Chan.OpenFailure

        If ErrMsgBox Is Nothing Then
            ErrMsgBox = New frmNonModelMsgBox
            ErrMsgBox.Label1.Text = ErrMsg
            ErrMsgBox.Text = _Name
            ErrMsgBox.Show()
        End If

    End Sub

End Class
