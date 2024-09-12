<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.ss = New ShipState.ShipState()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.scMain,System.ComponentModel.ISupportInitialize).BeginInit
        Me.scMain.SuspendLayout
        Me.MenuStrip1.SuspendLayout
        Me.SuspendLayout
        '
        'ss
        '
        Me.ss.AnimationFrameRate = 8
        Me.ss.AnimationFrames = 95
        Me.ss.BackColor = System.Drawing.Color.DodgerBlue
        Me.ss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ss.ForeColor = System.Drawing.Color.Black
        Me.ss.Location = New System.Drawing.Point(32, 160)
        Me.ss.Name = "ss"
        Me.ss.RoseColor = System.Drawing.Color.LightGray
        Me.ss.RoseSizeFactor = 85!
        Me.ss.ShipChaserCount = 0
        Me.ss.ShipChaserFf = 2!
        Me.ss.ShipColor = System.Drawing.Color.Azure
        Me.ss.ShipLengthFactor = 90!
        Me.ss.ShipVectorChaserCount = 0
        Me.ss.ShipVectorChaserFf = 2!
        Me.ss.ShipVectorColor = System.Drawing.Color.Yellow
        Me.ss.ShipVectorScale = 12!
        Me.ss.ShipVectorVisible = true
        Me.ss.SpeedLogVectorVisible = true
        Me.ss.SpeedLogChaserSet(0, 2!)
        Me.ss.ShipVisible = true
        Me.ss.Size = New System.Drawing.Size(200, 200)
        Me.ss.TabIndex = 2
        Me.ss.WindScale = 100!
        '
        'scMain
        '
        Me.scMain.Location = New System.Drawing.Point(72, 24)
        Me.scMain.Name = "scMain"
        Me.scMain.Size = New System.Drawing.Size(150, 100)
        Me.scMain.TabIndex = 3
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 7!)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(0)
        Me.MenuStrip1.Size = New System.Drawing.Size(533, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(52, 24)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 392)
        Me.Controls.Add(Me.ss)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.Text = "Form1"
        CType(Me.scMain,System.ComponentModel.ISupportInitialize).EndInit
        Me.scMain.ResumeLayout(false)
        Me.MenuStrip1.ResumeLayout(false)
        Me.MenuStrip1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents ss As ShipState.ShipState
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
End Class
