<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlLabeledWindow
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblWindow = New System.Windows.Forms.Label
        Me.lblLabel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblWindow
        '
        Me.lblWindow.BackColor = System.Drawing.Color.Gainsboro
        Me.lblWindow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWindow.CausesValidation = False
        Me.lblWindow.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.lblWindow.Location = New System.Drawing.Point(80, 96)
        Me.lblWindow.Name = "lblWindow"
        Me.lblWindow.Size = New System.Drawing.Size(100, 23)
        Me.lblWindow.TabIndex = 0
        Me.lblWindow.Text = "1234567890"
        Me.lblWindow.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblLabel
        '
        Me.lblLabel.BackColor = System.Drawing.Color.Plum
        Me.lblLabel.CausesValidation = False
        Me.lblLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.lblLabel.Location = New System.Drawing.Point(112, 32)
        Me.lblLabel.Name = "lblLabel"
        Me.lblLabel.Size = New System.Drawing.Size(56, 24)
        Me.lblLabel.TabIndex = 1
        Me.lblLabel.Text = "Label1"
        Me.lblLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ctlLabeledWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Goldenrod
        Me.Controls.Add(Me.lblLabel)
        Me.Controls.Add(Me.lblWindow)
        Me.DoubleBuffered = True
        Me.Name = "ctlLabeledWindow"
        Me.Size = New System.Drawing.Size(248, 194)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblWindow As System.Windows.Forms.Label
    Friend WithEvents lblLabel As System.Windows.Forms.Label

End Class
