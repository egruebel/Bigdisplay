Public Class frmNonModelMsgBox


    Private Sub frmNonModelMsgBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnOk.Left = CInt(Me.ClientRectangle.Width / 2.0! - btnOk.Width / 2.0!)

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.Close()
        Me.Dispose()
    End Sub

End Class