Public Class SettingsDialog
    'Private callerWindow As frmMain
    Private settings As clsSettings
    Private Sub SettingsDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'callerWindow = CallerWindow
        settings = callerWindow.settings
        PropertyGrid1.SelectedObject = settings
    End Sub
    Public Property CallerWindow As frmMain 
    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        callerWindow.ApplySettings()
    End Sub

    Private Sub btnSaveExit_Click(sender As Object, e As EventArgs) Handles btnSaveExit.Click
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class