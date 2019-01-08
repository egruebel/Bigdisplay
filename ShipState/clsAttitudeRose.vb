Imports System.Drawing.Drawing2D

Public Class clsAttitudeRose
    Inherits clsCompassRose

    Private _penTarget As New Pen(Color.Black, 7)
    Private gpTarget As New GraphicsPath

    Private Sub DrawTarget()
        gpTarget.Reset()
        gpTarget.AddLine(10,8,4,6)
    End Sub

    Friend Overrides Sub Draw(ByVal g As Graphics)
        g.DrawPath(_penTarget, gpTarget)
        MyBase.Draw(g)
    End Sub

    Public Overrides Sub Update()
        DrawTarget 
        MyBase.Update()
    End Sub

End Class
