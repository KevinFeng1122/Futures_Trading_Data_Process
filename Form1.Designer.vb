<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label_data_source = New System.Windows.Forms.Label
        Me.Button_Data_Source = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DataName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataWeight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Button_Weight_Get = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label_buy_path = New System.Windows.Forms.Label
        Me.Button_Buy_Path = New System.Windows.Forms.Button
        Me.Button_Sell_Path = New System.Windows.Forms.Button
        Me.Label_sell_path = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.ProgressBar_data = New System.Windows.Forms.ProgressBar
        Me.Label7 = New System.Windows.Forms.Label
        Me.Button_Start_Process = New System.Windows.Forms.Button
        Me.OpenFileDialog_Data_Source = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog_Report_buy = New System.Windows.Forms.SaveFileDialog
        Me.SaveFileDialog_Report_sell = New System.Windows.Forms.SaveFileDialog
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer
        Me.LineShape3 = New Microsoft.VisualBasic.PowerPacks.LineShape
        Me.LineShape2 = New Microsoft.VisualBasic.PowerPacks.LineShape
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape
        Me.Label8 = New System.Windows.Forms.Label
        Me.Button_Cancle_Process = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "数据文件："
        '
        'Label_data_source
        '
        Me.Label_data_source.Location = New System.Drawing.Point(74, 17)
        Me.Label_data_source.Name = "Label_data_source"
        Me.Label_data_source.Size = New System.Drawing.Size(181, 40)
        Me.Label_data_source.TabIndex = 1
        Me.Label_data_source.Text = "路径"
        '
        'Button_Data_Source
        '
        Me.Button_Data_Source.Location = New System.Drawing.Point(261, 13)
        Me.Button_Data_Source.Name = "Button_Data_Source"
        Me.Button_Data_Source.Size = New System.Drawing.Size(49, 23)
        Me.Button_Data_Source.TabIndex = 2
        Me.Button_Data_Source.Text = "选择"
        Me.Button_Data_Source.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataName, Me.DataWeight})
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DataGridView1.Location = New System.Drawing.Point(39, 105)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(243, 115)
        Me.DataGridView1.TabIndex = 3
        '
        'DataName
        '
        Me.DataName.HeaderText = "数据"
        Me.DataName.Name = "DataName"
        Me.DataName.ReadOnly = True
        Me.DataName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataWeight
        '
        Me.DataWeight.HeaderText = "权重"
        Me.DataWeight.Name = "DataWeight"
        '
        'Button_Weight_Get
        '
        Me.Button_Weight_Get.Enabled = False
        Me.Button_Weight_Get.Location = New System.Drawing.Point(261, 74)
        Me.Button_Weight_Get.Name = "Button_Weight_Get"
        Me.Button_Weight_Get.Size = New System.Drawing.Size(49, 23)
        Me.Button_Weight_Get.TabIndex = 4
        Me.Button_Weight_Get.Text = "确认"
        Me.Button_Weight_Get.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 270)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "持买仓量报表输出："
        '
        'Label_buy_path
        '
        Me.Label_buy_path.Location = New System.Drawing.Point(46, 289)
        Me.Label_buy_path.Name = "Label_buy_path"
        Me.Label_buy_path.Size = New System.Drawing.Size(209, 36)
        Me.Label_buy_path.TabIndex = 7
        Me.Label_buy_path.Text = "路径"
        '
        'Button_Buy_Path
        '
        Me.Button_Buy_Path.Location = New System.Drawing.Point(261, 268)
        Me.Button_Buy_Path.Name = "Button_Buy_Path"
        Me.Button_Buy_Path.Size = New System.Drawing.Size(49, 23)
        Me.Button_Buy_Path.TabIndex = 8
        Me.Button_Buy_Path.Text = "选择"
        Me.Button_Buy_Path.UseVisualStyleBackColor = True
        '
        'Button_Sell_Path
        '
        Me.Button_Sell_Path.Location = New System.Drawing.Point(261, 325)
        Me.Button_Sell_Path.Name = "Button_Sell_Path"
        Me.Button_Sell_Path.Size = New System.Drawing.Size(49, 23)
        Me.Button_Sell_Path.TabIndex = 11
        Me.Button_Sell_Path.Text = "选择"
        Me.Button_Sell_Path.UseVisualStyleBackColor = True
        '
        'Label_sell_path
        '
        Me.Label_sell_path.Location = New System.Drawing.Point(46, 348)
        Me.Label_sell_path.Name = "Label_sell_path"
        Me.Label_sell_path.Size = New System.Drawing.Size(209, 36)
        Me.Label_sell_path.TabIndex = 10
        Me.Label_sell_path.Text = "路径"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 330)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(113, 12)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "持卖仓量报表输出："
        '
        'ProgressBar_data
        '
        Me.ProgressBar_data.Location = New System.Drawing.Point(73, 409)
        Me.ProgressBar_data.Name = "ProgressBar_data"
        Me.ProgressBar_data.Size = New System.Drawing.Size(174, 18)
        Me.ProgressBar_data.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 412)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "处理进度"
        '
        'Button_Start_Process
        '
        Me.Button_Start_Process.Location = New System.Drawing.Point(253, 407)
        Me.Button_Start_Process.Name = "Button_Start_Process"
        Me.Button_Start_Process.Size = New System.Drawing.Size(40, 23)
        Me.Button_Start_Process.TabIndex = 14
        Me.Button_Start_Process.Text = "开始"
        Me.Button_Start_Process.UseVisualStyleBackColor = True
        '
        'OpenFileDialog_Data_Source
        '
        Me.OpenFileDialog_Data_Source.FileName = "OpenFileDialog1"
        Me.OpenFileDialog_Data_Source.Multiselect = True
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape3, Me.LineShape2, Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(346, 440)
        Me.ShapeContainer1.TabIndex = 15
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape3
        '
        Me.LineShape3.Name = "LineShape3"
        Me.LineShape3.X1 = 11
        Me.LineShape3.X2 = 333
        Me.LineShape3.Y1 = 390
        Me.LineShape3.Y2 = 390
        '
        'LineShape2
        '
        Me.LineShape2.Name = "LineShape2"
        Me.LineShape2.X1 = 9
        Me.LineShape2.X2 = 331
        Me.LineShape2.Y1 = 262
        Me.LineShape2.Y2 = 262
        '
        'LineShape1
        '
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 15
        Me.LineShape1.X2 = 337
        Me.LineShape1.Y1 = 66
        Me.LineShape1.Y2 = 66
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 79)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 12)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "数据特性设置"
        '
        'Button_Cancle_Process
        '
        Me.Button_Cancle_Process.Enabled = False
        Me.Button_Cancle_Process.Location = New System.Drawing.Point(299, 407)
        Me.Button_Cancle_Process.Name = "Button_Cancle_Process"
        Me.Button_Cancle_Process.Size = New System.Drawing.Size(40, 23)
        Me.Button_Cancle_Process.TabIndex = 17
        Me.Button_Cancle_Process.Text = "取消"
        Me.Button_Cancle_Process.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(196, 312)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(52, 31)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "test"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 236)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "排位前"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(86, 232)
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(40, 21)
        Me.NumericUpDown1.TabIndex = 20
        Me.NumericUpDown1.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(137, 236)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 12)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "的数据名高亮标注"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 440)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button_Cancle_Process)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button_Start_Process)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ProgressBar_data)
        Me.Controls.Add(Me.Button_Sell_Path)
        Me.Controls.Add(Me.Label_sell_path)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button_Buy_Path)
        Me.Controls.Add(Me.Label_buy_path)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button_Weight_Get)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button_Data_Source)
        Me.Controls.Add(Me.Label_data_source)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "GGG期货数据处理软件"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label_data_source As System.Windows.Forms.Label
    Friend WithEvents Button_Data_Source As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button_Weight_Get As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label_buy_path As System.Windows.Forms.Label
    Friend WithEvents Button_Buy_Path As System.Windows.Forms.Button
    Friend WithEvents Button_Sell_Path As System.Windows.Forms.Button
    Friend WithEvents Label_sell_path As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar_data As System.Windows.Forms.ProgressBar
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button_Start_Process As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog_Data_Source As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog_Report_buy As System.Windows.Forms.SaveFileDialog
    Friend WithEvents SaveFileDialog_Report_sell As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape3 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents LineShape2 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DataName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button_Cancle_Process As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
