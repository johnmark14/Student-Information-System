Imports MySql.Data.MySqlClient
Public Class frmmain
    Dim myDbConnect As New DbConnect
    Dim id As String

    Private Function controlIsEmpty() As Boolean
        Dim empty = From txt In Me.GroupBox1.Controls.OfType(Of TextBox)()
                    Where txt.Text = String.Empty
                    Select txt.Name

        If empty.Any Or cbgender.Text = String.Empty Or cbcourse.Text = String.Empty Or dtpdob.Value.Date = DateTime.Now.Date Then
            MessageBox.Show("Invalid Form", "Students' Information System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btninsert_Click(sender As Object, e As EventArgs) Handles btninsert.Click
        If controlIsEmpty() Then
            Try
                myDbConnect.openConnect()
                Dim sqlstring As String = "INSERT INTO tblstudents (lastname, firstname, gender, dob, address, email, course, guardian) VALUES ('" _
                & txtlastname.Text & "','" _
                & txtfirstname.Text & "','" _
                & cbgender.Text & "','" _
                & dtpdob.Text & "','" _
                & txtaddress.Text & "','" _
                & txtemail.Text & "','" _
                & cbcourse.Text & "','" _
                & txtguardian.Text & "')"

                Dim sqlCmd As New MySqlCommand(sqlstring, myDbConnect.mySqlConString)
                sqlCmd.ExecuteNonQuery()
                sqlCmd.Dispose()
                MessageBox.Show(" 1 record successfully saved!", "tudents' Information System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As MySqlException
                MessageBox.Show(ex.Message, "tudents' Information System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                myDbConnect.closeConnect()
                GC.Collect()
                fillListview()
                btncancel.PerformClick()
            End Try
        End If
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Dim ctl As Control
        For Each ctl In Me.GroupBox1.Controls
            If TypeOf ctl Is TextBox Then ctl.Text = String.Empty
            If TypeOf ctl Is ComboBox Then ctl.Text = String.Empty
            If TypeOf ctl Is DateTimePicker Then ctl.Text = Date.Now
        Next
        btninsert.Enabled = True
        btndelete.Enabled = False
        btnupdate.Enabled = False
    End Sub

    Private Sub fillListview()
        Try
            myDbConnect.openConnect()
            Dim sqlstring As String = "SELECT * FROM tblstudents"
            Dim sqlCmd As New MySqlCommand(sqlstring, myDbConnect.mySqlConString)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader
            lvinfo.Items.Clear()
            While (sqlReader.Read())
                With lvinfo.Items.Add(sqlReader("idtblstudents"))
                    .Subitems.add(sqlReader("lastname"))
                    .Subitems.add(sqlReader("firstname"))
                    .Subitems.add(sqlReader("gender"))
                    .Subitems.add(sqlReader("dob"))
                    .Subitems.add(getCurrentAge(sqlReader("dob")))
                    .Subitems.add(sqlReader("address"))
                    .Subitems.add(sqlReader("email"))
                    .Subitems.add(sqlReader("course"))
                    .Subitems.add(sqlReader("guardian"))
                End With
            End While
            sqlCmd.Dispose()
            sqlReader.Dispose()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message, "Students' Information System", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myDbConnect.closeConnect()
            GC.Collect()
        End Try
    End Sub

    Private Sub frmmain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillListview()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Try
            myDbConnect.openConnect()
            Dim sqlstring As String = "SELECT * FROM tblstudents where lastname like  '%" & txtsearch.Text & "%' OR firstname like '%" & txtsearch.Text & "%'"
            Dim sqlCmd As New MySqlCommand(sqlstring, myDbConnect.mySqlConString)
            Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader
            lvinfo.Items.Clear()
            While (sqlReader.Read())
                With lvinfo.Items.Add(sqlReader("idtblstudents"))
                    .Subitems.add(sqlReader("lastname"))
                    .Subitems.add(sqlReader("firstname"))
                    .Subitems.add(sqlReader("gender"))
                    .Subitems.add(sqlReader("dob"))
                    .Subitems.add(getCurrentAge(sqlReader("dob")))
                    .Subitems.add(sqlReader("address"))
                    .Subitems.add(sqlReader("email"))
                    .Subitems.add(sqlReader("course"))
                    .Subitems.add(sqlReader("guardian"))
                End With
            End While
            sqlCmd.Dispose()
            sqlReader.Dispose()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message, "tudents' Information System", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myDbConnect.closeConnect()
            GC.Collect()
        End Try
    End Sub

    Private Sub frmmain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        frmlogin.Show()
    End Sub

    Private Sub lvinfo_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvinfo.MouseDoubleClick
        Dim item As ListViewItem

        For Each item In lvinfo.SelectedItems
            id = lvinfo.Items(item.Index).SubItems(0).Text
            txtlastname.Text = lvinfo.Items(item.Index).SubItems(1).Text
            txtfirstname.Text = lvinfo.Items(item.Index).SubItems(2).Text
            cbgender.Text = lvinfo.Items(item.Index).SubItems(3).Text
            dtpdob.Text = lvinfo.Items(item.Index).SubItems(4).Text
            txtaddress.Text = lvinfo.Items(item.Index).SubItems(6).Text
            txtemail.Text = lvinfo.Items(item.Index).SubItems(7).Text
            cbcourse.Text = lvinfo.Items(item.Index).SubItems(8).Text
            txtguardian.Text = lvinfo.Items(item.Index).SubItems(9).Text
        Next
        btninsert.Enabled = False
        btnupdate.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        If controlIsEmpty() Then
            Try
                myDbConnect.openConnect()
                Dim sqlstring = "UPDATE tblstudents SET lastname = '" & txtlastname.Text & "', firstname ='" & txtfirstname.Text & "', gender='" & cbgender.Text & "', dob ='" & dtpdob.Text & "', address ='" & txtaddress.Text & "', email ='" & txtemail.Text & "', course ='" & cbcourse.Text & "', guardian ='" & txtguardian.Text & "' WHERE (idtblstudents ='" & id & "')"
                Dim sqlCmd As New MySqlCommand(sqlstring, myDbConnect.mySqlConString)
                sqlCmd.ExecuteNonQuery()
                sqlCmd.Dispose()
                MessageBox.Show(" 1 record successfully Updated!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As MySqlException
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                myDbConnect.closeConnect()
                GC.Collect()
                fillListview()
                btncancel.PerformClick()
            End Try
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If controlIsEmpty() Then
            Try
                myDbConnect.openConnect()
                Dim sqlstring = "DELETE FROM tblstudents WHERE (idtblstudents ='" & id & "')"
                Dim sqlCmd As New MySqlCommand(sqlstring, myDbConnect.mySqlConString)
                sqlCmd.ExecuteNonQuery()
                sqlCmd.Dispose()
                MessageBox.Show(" 1 record successfully Deleted!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As MySqlException
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                myDbConnect.closeConnect()
                GC.Collect()
                fillListview()
                btncancel.PerformClick()
            End Try
        End If
    End Sub

    Private Sub AddUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddUserToolStripMenuItem.Click
        frmaddusers.ShowDialog()
    End Sub

    Private Sub dtpdob_ValueChanged(sender As Object, e As EventArgs) Handles dtpdob.ValueChanged
        txtage.Text = getCurrentAge(dtpdob.Text)
    End Sub

    Private Function getCurrentAge(ByVal Dob As Date) As Integer
        Dim age As Integer
        age = Today.Year - Dob.Year
        If (Dob > Today.AddYears(-age)) Then
            age -= 1
        End If
        Return age
    End Function

    Private Sub EditUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditUserToolStripMenuItem.Click
        frmeditusers.ShowDialog()
    End Sub
End Class