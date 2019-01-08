<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.SuspendLayout
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Location = New System.Drawing.Point(1, 1)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(795, 385)
        Me.PropertyGrid1.TabIndex = 0
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Location = New System.Drawing.Point(536, 409)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(106, 23)
        Me.btnSaveExit.TabIndex = 1
        Me.btnSaveExit.Text = "Save and Exit"
        Me.btnSaveExit.UseVisualStyleBackColor = true
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(669, 409)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 2
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = true
        '
        'SettingsDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnSaveExit)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Name = "SettingsDialog"
        Me.Text = "Settings"
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents PropertyGrid1 As PropertyGrid
    Friend WithEvents btnSaveExit As Button
    Friend WithEvents btnApply As Button
End Class
