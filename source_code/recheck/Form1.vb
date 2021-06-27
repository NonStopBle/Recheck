Public Class Form1
    Dim auto_mode As Integer
    Dim act_auto_mode As Integer
    Dim anti_double As Integer
    Dim count_checker As Integer
    Dim counter_scan As Integer
    Dim one_connect As Integer
    Dim lock_counter As Integer
    Dim listport_x As New ComboBox
    Dim keyword As New RichTextBox
    Dim get_post As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
        Me.Visible = False
        CheckForIllegalCrossThreadCalls = False

        automode.Start()
    End Sub

    Private Sub run_check_Tick(sender As Object, e As EventArgs) Handles run_check.Tick

        If one_connect = 0 Then
            '     Try
            Try
                read_data.Close()
                read_data.BaudRate = 9600
                read_data.PortName = listport_x.SelectedItem
                read_data.Open()
                '  Label4.Text = "OUT OF ONE CONNECT"
                one_connect = 1
            Catch ex As Exception
                count_checker += 1
                one_connect = 0
                anti_double = 0
                run_check.Stop()
            End Try

            '    Catch ex As Exception


            '   End Try

        Else

            Try
                keyword.LoadFile("resources/renic_verify.dll", RichTextBoxStreamType.PlainText)

                get_post = read_data.ReadExisting
                ' MsgBox(keyword.Text)

                counter_scan += 1


                'Label4.Text = "COUNTER SCAN : " + counter_scan.ToString
                If counter_scan < 20 Then
                    '
                    If get_post.Contains(keyword.Text) Then

                        ' Label4.Text = "FOUNDED"
                        'read_data.Write("PASS")

                        auto_mode = 3


                        '  read_data.Write("PASS")
                        '  read_data.Write("PASS")
                        ' read_data.Write("PASS")
                        'read_data.Write("PASS")
                        'read_data.Write("PASS")
                        ' lock_counter = 1

                        ' anti_double = 0
                        ' Label1.Text = read_data.PortName
                        ' Label2.Text = counter_scan
                        ' Label3.Text = one_connect
                        ' run_check.Stop()
                    Else

                    End If
                Else
                    'Label4.Text = "CLOSE PORT"
                    read_data.Close()


                    count_checker += 1
                    anti_double = 0
                    '  Label4.Text = "RESET VARIABLE"
                    run_check.Stop()
                End If
            Catch ex As Exception
                ' Application.Exit()
            End Try




        End If
    End Sub

    Private Sub Connect()
        '  Try
        act_auto_mode = 1
        If anti_double = 0 Then
            Try
                ' Label4.Text = "SELECT PORT"
                listport_x.SelectedIndex = count_checker '// SELECT PORT AVAILABLE
                ' Label4.Text = "RESET PORT"
                one_connect = 0 '//RESET VAR
                counter_scan = 0 '//RESET SCAN VAR
                '// CLOSE PORT
                '  Label4.Text = "OUT OF DOUBLE"
                anti_double = 1

            Catch ex As Exception
                restart()

            End Try
        Else

            'Label4.Text = "RUN CHECKING"
            run_check.Start() '// RUN CHECKING
        End If

        '  Catch ex As Exception

        ' End Try
    End Sub
    Private Sub Var_Restore()
        auto_mode = 0
        counter_scan = 0
        count_checker = 0
        anti_double = 0
        run_check.Stop()
    End Sub

    Private Sub restart()
        Try
            read_data.Close()
            keyword.Text = "ERR_NOT_PORT : " + count_checker.ToString
            keyword.SaveFile("resources/renic_comport.dll", RichTextBoxStreamType.PlainText)
            Try

                Using p2 As New Process
                    With p2.StartInfo
                        .CreateNoWindow = True
                        .FileName = "cmd.exe"
                        .RedirectStandardInput = True
                        .RedirectStandardOutput = True
                        .UseShellExecute = False


                    End With
                    p2.Start()

                    p2.StandardInput.WriteLine("start recheck.exe")
                    ' RichTextBox2.Text = p2.StandardOutput.Read
                    p2.StandardInput.WriteLine("exit")
                End Using
            Catch ex As Exception

            End Try
            Application.Exit()
        Catch ex As Exception
            Application.Restart()
        End Try


    End Sub
    Dim write_one As Integer
    Private Sub automode_Tick(sender As Object, e As EventArgs) Handles automode.Tick

        If act_auto_mode = 0 Then
            auto_mode += 1
        Else

        End If
        Select Case auto_mode
            Case 1
                SCAN()
            Case 2
                Connect()
            Case 3
                Try
                    act_auto_mode = 1
                    keyword.Text = read_data.PortName
                    keyword.SaveFile("resources/renic_comport.dll", RichTextBoxStreamType.PlainText)
                    read_data.Close()
                    lock_counter = 0
                    Application.Exit()
                Catch ex As Exception

                End Try
        End Select

    End Sub

    Private Sub WRITE()
        Try
            keyword.SaveFile("resources/renic_verify.dll", RichTextBoxStreamType.PlainText)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SCAN()
        Try
            listport_x.Items.Clear()
            Dim myPort As Array
            Dim i As Integer
            myPort = IO.Ports.SerialPort.GetPortNames()
            listport_x.Items.AddRange(myPort)
            i = listport_x.Items.Count
            i = i - i
            Try
                ' MsgBox(myPort)
                '  combo_selector.SelectedIndex = i
                act_auto_mode = 0
            Catch ex As Exception
                act_auto_mode = 1
                listport_x.Text = ""
                listport_x.Items.Clear()
                Return
            End Try
        Catch ex As Exception

        End Try

    End Sub


End Class
