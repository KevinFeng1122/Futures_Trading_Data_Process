Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.String
Imports Path = System.IO.Path
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Threading

Public Class Form1
    Dim datafilepath(99) As String 'used to save selected xlses' full path(including directory,name,extension name)
    Dim DataFilePureName() As String 'used to save above files' pure name(without directory and extension name)
    Dim DataFileWeight() As Double 'used to save each data file's weight
    Dim WeightCheckOk As Boolean = False 'used to indicate the sum of each file's weight. when false, the process cannot start.
    Dim ReportBuyFilePath As String = ""
    Dim ReportSellFilePath As String = ""
    Dim ProgressBarValue As Integer = 0 '0 to 100
    Dim ProcessThread As Thread

    Private Sub Button_Data_Source_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Data_Source.Click
        ReDim datafilepath(99) 'clear the old elements in this array
        With OpenFileDialog_Data_Source
            '.Filter = "Excel97-2003工作簿（*.xls）|*.xls|Excel2007工作簿（*.xlsx）|*.xlsx"
            If .ShowDialog = System.Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            Dim filepath As String
            Dim i As Short = 0
            For Each filepath In .FileNames
                datafilepath(i) = filepath
                i = i + 1
            Next filepath
            Label_data_source.Text = Path.GetDirectoryName(.FileName) & "目录中"
        End With
        Dim num As Short = GetArrayEffectiveElementNum(datafilepath)
        DataFilePureName = GetPureFileName(datafilepath) '???seems a redim included
        DataGridView1.Rows.Clear()
        For index As Integer = 0 To (num - 1)
            Dim filename As String = DataFilePureName(index)
            Dim row() As String = {filename, "0"}
            DataGridView1.Rows.Add(row)
        Next
        Button_Weight_Get.Enabled = True
    End Sub

    Private Function GetArrayEffectiveElementNum(ByVal array_in As String()) As Short
        'return the number of effective elements in an array
        'e.g str_arr(3)={"0","1",NOTHING}
        'the returned number is 2
        Dim i As Short = 0
        Do Until array_in(i) = Nothing
            i = i + 1
        Loop
        Return i
    End Function

    Private Function GetPureFileName(ByVal array_in As String()) As String()
        Dim ElementsNumber As Short = GetArrayEffectiveElementNum(array_in)
        Dim PureFileName() As String
        ReDim PureFileName(ElementsNumber - 1)
        For index As Integer = 0 To (ElementsNumber - 1)
            PureFileName(index) = Path.GetFileNameWithoutExtension(array_in(index))
        Next
        Return PureFileName
    End Function

    Private Sub Button_Weight_Get_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Weight_Get.Click
        Dim DataFileNum As Short = DataFilePureName.GetLength(0)
        ReDim DataFileWeight(DataFileNum - 1)
        Dim CheckSum As Double = 0
        For index As Integer = 0 To DataFileNum - 1
            Dim WeightString = DataGridView1.Rows(index).Cells(1).Value
            Try
                DataFileWeight(index) = Double.Parse(WeightString)
            Catch ex As Exception
                MsgBox(ex.Message)
                WeightCheckOk = False
                Exit Sub
            End Try
            CheckSum = CheckSum + DataFileWeight(index)
        Next
        If CheckSum = 1 Then
            MsgBox("权重和正确")
            WeightCheckOk = True
        Else
            MsgBox("权重和有误，请检查！")
            WeightCheckOk = False
        End If
    End Sub

    Private Sub Button_Buy_Path_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Buy_Path.Click
        With SaveFileDialog_Report_buy
            .DefaultExt = "xlsx"
            .FileName = "持买仓量报告" & GetCurrentDate()
            .Filter = "Excel97-2003工作簿（*.xls）|*.xls|Excel2007工作簿（*.xlsx）|*.xlsx"
            .FilterIndex = 1
            .OverwritePrompt = True
            .Title = "请选择保存的位置"
        End With
        If SaveFileDialog_Report_buy.ShowDialog = Windows.Forms.DialogResult.OK Then
            ReportBuyFilePath = SaveFileDialog_Report_buy.FileName
            Label_buy_path.Text = ReportBuyFilePath
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Button_Sell_Path_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Sell_Path.Click
        With SaveFileDialog_Report_sell
            .DefaultExt = "xlsx"
            .FileName = "持卖仓量报告" & GetCurrentDate()
            .Filter = "Excel97-2003工作簿（*.xls）|*.xls|Excel2007工作簿（*.xlsx）|*.xlsx"
            .FilterIndex = 1
            .OverwritePrompt = True
            .Title = "请选择保存的位置"
        End With
        If SaveFileDialog_Report_sell.ShowDialog = Windows.Forms.DialogResult.OK Then
            ReportSellFilePath = SaveFileDialog_Report_sell.FileName
            Label_sell_path.Text = ReportSellFilePath
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Button_Start_Process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Start_Process.Click
        Dim PathCheckOk As Boolean = False
        PathCheckOk = (Label_buy_path.Text <> "路径") And (Label_sell_path.Text <> "路径")
        If Not (PathCheckOk And WeightCheckOk) Then
            MsgBox("请检查：" & vbCr & "（1）权重值是否已经确认，且加和正确" & _
                vbCr & "（2）持买仓量和持卖仓量的报告路是否均已选择")
            Exit Sub
        End If
        If MsgBox("点击OK开始处理数据", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        ' start a new thread to process data
        Button_Cancle_Process.Enabled = True
        ProcessThread = New Thread(AddressOf ProcessData)
        ProcessThread.Start()
    End Sub

    Private Function GetCurrentDate() As String
        Dim s As String
        s = DateTime.Today.Year()
        s = s & "-" & DateTime.Today.Month()
        s = s & "-" & DateTime.Today.Day()
        Return s
    End Function

    Private Sub ProcessData()

        'Progress Bar
        ProgressBarValue = 0
        AccessControl()

        Dim app As Excel.Application
        app = New Excel.Application()
        Dim wbs As Excel.Workbooks = app.Workbooks

        'set up the framework of the report excel of 持买仓量
        Dim wb_buy As Excel.Workbook = wbs.Add(True)
        wb_buy.SaveAs(ReportBuyFilePath)
        Dim ws_buy_ShanghaiCopper As Excel.Worksheet
        ws_buy_ShanghaiCopper = wb_buy.Worksheets.Add()
        ws_buy_ShanghaiCopper.Name = "沪铜"
        'fill the contents of framework into worksheet'沪铜'
        ws_buy_ShanghaiCopper.Cells(1, 2) = "持买仓量报表"
        ws_buy_ShanghaiCopper.Cells(2, 1) = "名次"
        ws_buy_ShanghaiCopper.Cells(2, 2) = "公司名称"
        ws_buy_ShanghaiCopper.Cells(2, 3) = "加权平均数"
        ws_buy_ShanghaiCopper.Columns(1).ColumnWidth = 5
        ws_buy_ShanghaiCopper.Columns(2).ColumnWidth = 12
        ws_buy_ShanghaiCopper.Columns(3).ColumnWidth = 10
        For j As Integer = 0 To (DataFilePureName.Count - 1)
            ws_buy_ShanghaiCopper.Cells(2, (2 * j + 4)) = DataFilePureName(j) & "持买仓量"
            ws_buy_ShanghaiCopper.Columns((2 * j + 4)).ColumnWidth = 8
            ws_buy_ShanghaiCopper.Cells(2, (2 * j + 5)) = DataFilePureName(j) & "排名"
            ws_buy_ShanghaiCopper.Columns((2 * j + 5)).ColumnWidth = 8
            ws_buy_ShanghaiCopper.Cells(1, (2 * j + 4)) = "【权重】"
            ws_buy_ShanghaiCopper.Cells(1, (2 * j + 5)) = DataFileWeight(j)
        Next
        ws_buy_ShanghaiCopper.Columns.WrapText = True
        'copy the worksheet'沪铜' to other 6 items
        Dim SheetName() As String = {"螺纹", "橡胶", "白银", "PTA", "焦炭", "塑料"}
        'name-index:"沪铜"1, "螺纹"2 , "橡胶"3 , "白银"4 , "PTA"5 , "焦炭"6 , "塑料"7
        For i As Integer = 0 To 5
            ws_buy_ShanghaiCopper.Copy(, wb_buy.Worksheets(i + 1))
            wb_buy.Worksheets(i + 2).name = SheetName(i)
        Next
        wb_buy.Save()

        'set up the framework of the report excel of 持卖仓量
        Dim wb_sell As Excel.Workbook = wbs.Add(True)
        wb_sell.SaveAs(ReportSellFilePath)
        Dim ws_sell_ShanghaiCopper As Excel.Worksheet
        ws_sell_ShanghaiCopper = wb_sell.Worksheets.Add()
        ws_sell_ShanghaiCopper.Name = "沪铜"
        'fill the contents of framework into worksheet'沪铜'
        ws_sell_ShanghaiCopper.Cells(1, 2) = "持卖仓量报表"
        ws_sell_ShanghaiCopper.Cells(2, 1) = "名次"
        ws_sell_ShanghaiCopper.Cells(2, 2) = "公司名称"
        ws_sell_ShanghaiCopper.Cells(2, 3) = "加权平均数"
        ws_sell_ShanghaiCopper.Columns(1).ColumnWidth = 5
        ws_sell_ShanghaiCopper.Columns(2).ColumnWidth = 12
        ws_sell_ShanghaiCopper.Columns(3).ColumnWidth = 10
        For k As Integer = 0 To (DataFilePureName.Count - 1)
            ws_sell_ShanghaiCopper.Cells(2, (2 * k + 4)) = DataFilePureName(k) & "持卖仓量"
            ws_sell_ShanghaiCopper.Columns((2 * k + 4)).ColumnWidth = 8
            ws_sell_ShanghaiCopper.Cells(2, (2 * k + 5)) = DataFilePureName(k) & "排名"
            ws_sell_ShanghaiCopper.Columns((2 * k + 5)).ColumnWidth = 8
            ws_sell_ShanghaiCopper.Cells(1, (2 * k + 4)) = "【权重】"
            ws_sell_ShanghaiCopper.Cells(1, (2 * k + 5)) = DataFileWeight(k)
        Next
        ws_sell_ShanghaiCopper.Columns.WrapText = True
        'copy the worksheet'沪铜' to other 6 items
        For i As Integer = 0 To 5
            ws_sell_ShanghaiCopper.Copy(, wb_sell.Worksheets(i + 1))
            wb_sell.Worksheets(i + 2).name = SheetName(i)
        Next
        wb_sell.Save()

        'Progress Bar
        ProgressBarValue = 1
        AccessControl()

        'filling report with data
        Dim WorksheetName() As String = {"沪铜", "螺纹", "橡胶", "白银", "PTA", "焦炭", "塑料"}
        Dim DataFileNumber As Short = GetArrayEffectiveElementNum(datafilepath)
        For index_datafile = 0 To (DataFileNumber - 1)
            Dim wb_some_data_file As Excel.Workbook = wbs.Open(datafilepath(index_datafile), Type.Missing, True, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlPlatform.xlWindows, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
            For index_worksheet = 0 To 6
                Dim ws_some_worksheet As Excel.Worksheet = wb_some_data_file.Worksheets(WorksheetName(index_worksheet))

                '↓填充持买仓量表格
                Dim BuyCompanyName_some_worksheet As String '某数据文件中某表的某行的持买仓量公司名称
                Dim BuyAmount_some_worksheet As Integer '某数据文件中某表的某行的持买仓量
                Dim BuyRank_some_worksheet As Short '某数据文件中某表的某行的持买仓量排名
                Dim index_row_some_worksheet As Short = 3 '某数据文件中某表的某行的行号
                BuyCompanyName_some_worksheet = ws_some_worksheet.Cells(index_row_some_worksheet, 5).text '此处若使用value属性会导致异常出现
                Do Until BuyCompanyName_some_worksheet = "" Or BuyCompanyName_some_worksheet = " " Or BuyCompanyName_some_worksheet = "  " Or index_row_some_worksheet = 200
                    BuyAmount_some_worksheet = ws_some_worksheet.Cells(index_row_some_worksheet, 6).value
                    BuyRank_some_worksheet = index_row_some_worksheet - 2
                    '在wb_buy持买仓量的Worksheets(WorksheetName(index_worksheet))中的“公司名称”中查询
                    '是否有相同的项，有的话在该处写入持买仓量和排名；没有的话，
                    '创建新的项，然后写入量和排名
                    Dim CompanyName_wb_buy As String
                    Dim index_row_wb_buy As Short = 3
                    Do
                        CompanyName_wb_buy = wb_buy.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_buy, 2).text
                        If BuyCompanyName_some_worksheet = CompanyName_wb_buy Then
                            index_row_wb_buy += 1
                            Exit Do
                        End If
                        index_row_wb_buy += 1
                    Loop Until CompanyName_wb_buy = "" Or CompanyName_wb_buy = " " Or CompanyName_wb_buy = "  "
                    index_row_wb_buy -= 1
                    '上面获得了写入数据的位置（行号），下面写入数据
                    wb_buy.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_buy, 2) = BuyCompanyName_some_worksheet
                    wb_buy.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_buy, 4 + 2 * index_datafile) = BuyAmount_some_worksheet
                    wb_buy.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_buy, 5 + 2 * index_datafile) = BuyRank_some_worksheet
                    If BuyRank_some_worksheet <= NumericUpDown1.Value Then
                        wb_buy.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_buy, 5 + 2 * index_datafile).Interior.ColorIndex = 28 '单元格背景色浅青绿色
                    End If
                    '进入下一行数据
                    index_row_some_worksheet += 1
                    BuyCompanyName_some_worksheet = ws_some_worksheet.Cells(index_row_some_worksheet, 5).text
                Loop

                '↓填充持卖仓量表格
                Dim SellCompanyName_some_worksheet As String '某数据文件中某表的某行的持卖仓量公司名称
                Dim SellAmount_some_worksheet As Integer '某数据文件中某表的某行的持卖仓量
                Dim SellRank_some_worksheet As Short '某数据文件中某表的某行的持卖仓量排名
                index_row_some_worksheet = 3 '某数据文件中某表的某行的行号重新置为3
                SellCompanyName_some_worksheet = ws_some_worksheet.Cells(index_row_some_worksheet, 8).text '此处若使用value属性会导致异常出现
                Do Until SellCompanyName_some_worksheet = "" Or SellCompanyName_some_worksheet = " " Or SellCompanyName_some_worksheet = "  " Or index_row_some_worksheet = 200
                    SellAmount_some_worksheet = ws_some_worksheet.Cells(index_row_some_worksheet, 9).value
                    SellRank_some_worksheet = index_row_some_worksheet - 2
                    '在wb_sell持卖仓量的Worksheets(WorksheetName(index_worksheet))中的“公司名称”中查询
                    '是否有相同的项，有的话在该处写入持卖仓量和排名；没有的话，
                    '创建新的项，然后写入量和排名
                    Dim CompanyName_wb_sell As String
                    Dim index_row_wb_sell As Short = 3
                    Do
                        CompanyName_wb_sell = wb_sell.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_sell, 2).text
                        If SellCompanyName_some_worksheet = CompanyName_wb_sell Then
                            index_row_wb_sell += 1
                            Exit Do
                        End If
                        index_row_wb_sell += 1
                    Loop Until CompanyName_wb_sell = "" Or CompanyName_wb_sell = " " Or CompanyName_wb_sell = "  "
                    index_row_wb_sell -= 1
                    '上面获得了写入数据的位置（行号），下面写入数据
                    wb_sell.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_sell, 2) = SellCompanyName_some_worksheet
                    wb_sell.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_sell, 4 + 2 * index_datafile) = SellAmount_some_worksheet
                    wb_sell.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_sell, 5 + 2 * index_datafile) = SellRank_some_worksheet
                    If SellRank_some_worksheet <= NumericUpDown1.Value Then
                        wb_sell.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_sell, 5 + 2 * index_datafile).Interior.ColorIndex = 28 '单元格背景色浅青绿色
                    End If
                    '进入下一行数据
                    index_row_some_worksheet += 1
                    SellCompanyName_some_worksheet = ws_some_worksheet.Cells(index_row_some_worksheet, 8).text
                Loop

            Next '进入下一个worksheet
            wb_some_data_file.Close(Type.Missing, Type.Missing, Type.Missing)
            wb_some_data_file = Nothing

            'Progress Bar
            ProgressBarValue = 1 + (index_datafile + 1) * 90 / DataFileNumber
            AccessControl()

        Next '进入下一个数据文件


        'futher calculation and rank

        '计算持买仓量数据
 
        Dim buy_range_row(6) As Short
        Dim buy_value_rank_array(6, 200, 2) As Integer
        For index_worksheet = 0 To 6
            Dim CompanyName_wb_buy As String
            Dim index_row_wb_buy As Short = 3
            CompanyName_wb_buy = wb_buy.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_buy, 2).text
            Do Until (CompanyName_wb_buy = "" Or CompanyName_wb_buy = " " Or CompanyName_wb_buy = "  ")
                Dim CalculatedResult As Integer
                For index_data_buy_amount = 0 To GetArrayEffectiveElementNum(datafilepath) - 1
                    CalculatedResult = CalculatedResult + wb_buy.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_buy, (index_data_buy_amount * 2 + 4)).value * DataFileWeight(index_data_buy_amount)
                Next
                wb_buy.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_buy, 3) = CalculatedResult
                buy_value_rank_array(index_worksheet, index_row_wb_buy, 1) = CalculatedResult
                CalculatedResult = 0
                index_row_wb_buy += 1
                CompanyName_wb_buy = wb_buy.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_buy, 2).text
            Loop
            buy_range_row(index_worksheet) = index_row_wb_buy - 1
        Next
        '排序
        For index_worksheet_sort = 0 To 6
            '三维数组中得出顺序
            For index_value = 3 To buy_range_row(index_worksheet_sort)
                Dim value_ As Integer = buy_value_rank_array(index_worksheet_sort, index_value, 1)
                Dim larger_num As Short = 0
                For index_value_2 = 3 To buy_range_row(index_worksheet_sort)
                    If value_ < buy_value_rank_array(index_worksheet_sort, index_value_2, 1) Then
                        larger_num += 1
                    End If
                Next
                buy_value_rank_array(index_worksheet_sort, index_value, 2) = larger_num + 1
            Next

            '将顺序值写入表格中
            For index_row_wb_buy_2 = 3 To buy_range_row(index_worksheet_sort)
                wb_buy.Worksheets(index_worksheet_sort + 1).cells(index_row_wb_buy_2, 1) = buy_value_rank_array(index_worksheet_sort, index_row_wb_buy_2, 2)
            Next
        Next

        'Progress Bar
        ProgressBarValue = 95
        AccessControl()

        '计算持卖仓量数据
        Dim sell_range_row(6) As Short
        Dim sell_value_rank_array(6, 200, 2) As Integer
        For index_worksheet = 0 To 6
            Dim CompanyName_wb_sell As String
            Dim index_row_wb_sell As Short = 3
            CompanyName_wb_sell = wb_sell.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_sell, 2).text
            Do Until (CompanyName_wb_sell = "" Or CompanyName_wb_sell = " " Or CompanyName_wb_sell = "  ")
                Dim CalculatedResult As Integer
                For index_data_sell_amount = 0 To GetArrayEffectiveElementNum(datafilepath) - 1
                    CalculatedResult = CalculatedResult + wb_sell.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_sell, (index_data_sell_amount * 2 + 4)).value * DataFileWeight(index_data_sell_amount)
                Next
                wb_sell.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_sell, 3) = CalculatedResult
                sell_value_rank_array(index_worksheet, index_row_wb_sell, 1) = CalculatedResult
                CalculatedResult = 0
                index_row_wb_sell += 1
                CompanyName_wb_sell = wb_sell.Worksheets(WorksheetName(index_worksheet)).cells(index_row_wb_sell, 2).text
            Loop
            sell_range_row(index_worksheet) = index_row_wb_sell - 1
        Next
        '排序
        For index_worksheet_sort = 0 To 6
            '三维数组中得出顺序
            For index_value = 3 To sell_range_row(index_worksheet_sort)
                Dim value_ As Integer = sell_value_rank_array(index_worksheet_sort, index_value, 1)
                Dim larger_num As Short = 0
                For index_value_2 = 3 To sell_range_row(index_worksheet_sort)
                    If value_ < sell_value_rank_array(index_worksheet_sort, index_value_2, 1) Then
                        larger_num += 1
                    End If
                Next
                sell_value_rank_array(index_worksheet_sort, index_value, 2) = larger_num + 1
            Next

            '将顺序值写入表格中
            For index_row_wb_sell_2 = 3 To sell_range_row(index_worksheet_sort)
                wb_sell.Worksheets(index_worksheet_sort + 1).cells(index_row_wb_sell_2, 1) = sell_value_rank_array(index_worksheet_sort, index_row_wb_sell_2, 2)
            Next
        Next

        'Progress Bar
        ProgressBarValue = 99
        AccessControl()

        'close the files above and clear related ram
        wb_buy.Save()
        wb_buy.Close(Type.Missing, Type.Missing, Type.Missing)
        wb_sell.Save()
        wb_sell.Close(Type.Missing, Type.Missing, Type.Missing)
        wbs.Close()
        app.Quit()
        wb_buy = Nothing
        wb_sell = Nothing
        wbs = Nothing
        app = Nothing
        GC.Collect()

        'Progress Bar
        ProgressBarValue = 100
        AccessControl()
        MsgBox("处理完成")
    End Sub

    Private Sub AccessControl()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf AccessControl))
        Else
            ' Code wasn't working in the threading sub
            ProgressBar_data.Value = ProgressBarValue
        End If
    End Sub

    Private Sub Button_Cancle_Process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancle_Process.Click
        If ProcessThread.IsAlive = True Then
            ProcessThread.Abort()
            MsgBox("请手动打开任务管理器，然后终止由于中断操作而尚未关闭的Excel程序的进程。否则，注销计算机前使用本软件可能会引起异常！")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim CompanyName(20000) As String
        CompanyName(0) = 1
        Dim CompanyNumber As Short
        CompanyNumber = GetArrayEffectiveElementNum(CompanyName)
        MsgBox(CompanyNumber.ToString)
        '该例子证明了【VB.NET数组的特性】：
        '1.索引：从0开始
        '2.定义时括号中的数字为最后一个元素的索引号，因此数组的length为该数字+1
        '例如Dim CompanyName(20000) As String，CompanyName共包含20001个元素
        '3.未初始化的元素默认为NOTHING
        'Excel表格编号从(1,1)开始
    End Sub
End Class
