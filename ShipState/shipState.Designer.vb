<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShipState
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.lblTrueWindSpeedLeft = New System.Windows.Forms.Label()
        Me.lblTrueWindAzimuthLeft = New System.Windows.Forms.Label()
        Me.lblTrueWindLeft = New System.Windows.Forms.Label()
        Me.lblTrueWindSpeedRight = New System.Windows.Forms.Label()
        Me.lblTrueWindAzimuthRight = New System.Windows.Forms.Label()
        Me.lblTrueWindRight = New System.Windows.Forms.Label()
        Me.lblShip = New System.Windows.Forms.Label()
        Me.lblShipSmg = New System.Windows.Forms.Label()
        Me.lblShipCmg = New System.Windows.Forms.Label()
        Me.lblShipHeading = New System.Windows.Forms.Label()
        Me.lblHead = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout
        '
        'lblTrueWindSpeedLeft
        '
        Me.lblTrueWindSpeedLeft.AutoSize = true
        Me.lblTrueWindSpeedLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblTrueWindSpeedLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTrueWindSpeedLeft.Location = New System.Drawing.Point(8, 34)
        Me.lblTrueWindSpeedLeft.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTrueWindSpeedLeft.Name = "lblTrueWindSpeedLeft"
        Me.lblTrueWindSpeedLeft.Size = New System.Drawing.Size(66, 18)
        Me.lblTrueWindSpeedLeft.TabIndex = 5
        Me.lblTrueWindSpeedLeft.Text = "12.3 Kts."
        '
        'lblTrueWindAzimuthLeft
        '
        Me.lblTrueWindAzimuthLeft.AutoSize = true
        Me.lblTrueWindAzimuthLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblTrueWindAzimuthLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTrueWindAzimuthLeft.Location = New System.Drawing.Point(8, 55)
        Me.lblTrueWindAzimuthLeft.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTrueWindAzimuthLeft.Name = "lblTrueWindAzimuthLeft"
        Me.lblTrueWindAzimuthLeft.Size = New System.Drawing.Size(38, 18)
        Me.lblTrueWindAzimuthLeft.TabIndex = 4
        Me.lblTrueWindAzimuthLeft.Text = "349*"
        '
        'lblTrueWindLeft
        '
        Me.lblTrueWindLeft.AutoSize = true
        Me.lblTrueWindLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblTrueWindLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTrueWindLeft.Location = New System.Drawing.Point(8, 13)
        Me.lblTrueWindLeft.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTrueWindLeft.Name = "lblTrueWindLeft"
        Me.lblTrueWindLeft.Size = New System.Drawing.Size(76, 18)
        Me.lblTrueWindLeft.TabIndex = 3
        Me.lblTrueWindLeft.Text = "True Wind"
        '
        'lblTrueWindSpeedRight
        '
        Me.lblTrueWindSpeedRight.BackColor = System.Drawing.Color.Transparent
        Me.lblTrueWindSpeedRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTrueWindSpeedRight.Location = New System.Drawing.Point(128, 34)
        Me.lblTrueWindSpeedRight.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTrueWindSpeedRight.Name = "lblTrueWindSpeedRight"
        Me.lblTrueWindSpeedRight.Size = New System.Drawing.Size(66, 19)
        Me.lblTrueWindSpeedRight.TabIndex = 12
        Me.lblTrueWindSpeedRight.Text = "12.3 Kts."
        Me.lblTrueWindSpeedRight.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTrueWindAzimuthRight
        '
        Me.lblTrueWindAzimuthRight.BackColor = System.Drawing.Color.Transparent
        Me.lblTrueWindAzimuthRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTrueWindAzimuthRight.Location = New System.Drawing.Point(156, 54)
        Me.lblTrueWindAzimuthRight.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTrueWindAzimuthRight.Name = "lblTrueWindAzimuthRight"
        Me.lblTrueWindAzimuthRight.Size = New System.Drawing.Size(38, 19)
        Me.lblTrueWindAzimuthRight.TabIndex = 11
        Me.lblTrueWindAzimuthRight.Text = "349*"
        Me.lblTrueWindAzimuthRight.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTrueWindRight
        '
        Me.lblTrueWindRight.AutoSize = true
        Me.lblTrueWindRight.BackColor = System.Drawing.Color.Transparent
        Me.lblTrueWindRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTrueWindRight.Location = New System.Drawing.Point(118, 12)
        Me.lblTrueWindRight.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTrueWindRight.Name = "lblTrueWindRight"
        Me.lblTrueWindRight.Size = New System.Drawing.Size(76, 18)
        Me.lblTrueWindRight.TabIndex = 10
        Me.lblTrueWindRight.Text = "True Wind"
        Me.lblTrueWindRight.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblShip
        '
        Me.lblShip.AutoSize = true
        Me.lblShip.BackColor = System.Drawing.Color.Transparent
        Me.lblShip.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblShip.Location = New System.Drawing.Point(10, 122)
        Me.lblShip.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblShip.Name = "lblShip"
        Me.lblShip.Size = New System.Drawing.Size(29, 18)
        Me.lblShip.TabIndex = 16
        Me.lblShip.Text = "mg"
        '
        'lblShipSmg
        '
        Me.lblShipSmg.AutoSize = true
        Me.lblShipSmg.BackColor = System.Drawing.Color.Transparent
        Me.lblShipSmg.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblShipSmg.Location = New System.Drawing.Point(8, 164)
        Me.lblShipSmg.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblShipSmg.Name = "lblShipSmg"
        Me.lblShipSmg.Size = New System.Drawing.Size(60, 18)
        Me.lblShipSmg.TabIndex = 15
        Me.lblShipSmg.Text = "12.2 kts"
        '
        'lblShipCmg
        '
        Me.lblShipCmg.AutoSize = true
        Me.lblShipCmg.BackColor = System.Drawing.Color.Transparent
        Me.lblShipCmg.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblShipCmg.Location = New System.Drawing.Point(10, 142)
        Me.lblShipCmg.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblShipCmg.Name = "lblShipCmg"
        Me.lblShipCmg.Size = New System.Drawing.Size(38, 18)
        Me.lblShipCmg.TabIndex = 14
        Me.lblShipCmg.Text = "349*"
        '
        'lblShipHeading
        '
        Me.lblShipHeading.AutoSize = true
        Me.lblShipHeading.BackColor = System.Drawing.Color.Transparent
        Me.lblShipHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblShipHeading.Location = New System.Drawing.Point(56, 190)
        Me.lblShipHeading.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShipHeading.Name = "lblShipHeading"
        Me.lblShipHeading.Size = New System.Drawing.Size(38, 18)
        Me.lblShipHeading.TabIndex = 13
        Me.lblShipHeading.Text = "349*"
        '
        'lblHead
        '
        Me.lblHead.AutoSize = true
        Me.lblHead.BackColor = System.Drawing.Color.Transparent
        Me.lblHead.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblHead.Location = New System.Drawing.Point(8, 190)
        Me.lblHead.Margin = New System.Windows.Forms.Padding(0)
        Me.lblHead.Name = "lblHead"
        Me.lblHead.Size = New System.Drawing.Size(47, 18)
        Me.lblHead.TabIndex = 17
        Me.lblHead.Text = "Head:"
        Me.lblHead.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Timer1
        '
        Me.Timer1.Enabled = true
        Me.Timer1.Interval = 15
        '
        'ShipState
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 14!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Controls.Add(Me.lblHead)
        Me.Controls.Add(Me.lblShip)
        Me.Controls.Add(Me.lblShipSmg)
        Me.Controls.Add(Me.lblShipCmg)
        Me.Controls.Add(Me.lblShipHeading)
        Me.Controls.Add(Me.lblTrueWindSpeedRight)
        Me.Controls.Add(Me.lblTrueWindAzimuthRight)
        Me.Controls.Add(Me.lblTrueWindRight)
        Me.Controls.Add(Me.lblTrueWindSpeedLeft)
        Me.Controls.Add(Me.lblTrueWindAzimuthLeft)
        Me.Controls.Add(Me.lblTrueWindLeft)
        Me.DoubleBuffered = true
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Name = "ShipState"
        Me.Size = New System.Drawing.Size(200, 215)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents lblTrueWindSpeedLeft As System.Windows.Forms.Label
    Friend WithEvents lblTrueWindAzimuthLeft As System.Windows.Forms.Label
    Friend WithEvents lblTrueWindLeft As System.Windows.Forms.Label
    Friend WithEvents lblTrueWindSpeedRight As System.Windows.Forms.Label
    Friend WithEvents lblTrueWindAzimuthRight As System.Windows.Forms.Label
    Friend WithEvents lblTrueWindRight As System.Windows.Forms.Label
    Friend WithEvents Timer1 As Timer
    Public WithEvents lblShip As Label
    Public WithEvents lblShipSmg As Label
    Public WithEvents lblShipCmg As Label
    Public WithEvents lblShipHeading As Label
    Public WithEvents lblHead As Label
End Class
