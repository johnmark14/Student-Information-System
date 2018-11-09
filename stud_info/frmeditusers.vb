Imports MySql.Data.MySqlClient
Public Class frmeditusers
    Dim myDbConnect As New DbConnect
    Dim id As String
    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Dim empty = From txt In Me.GroupBox1.Controls.OfType(Of TextBox)()
                    Where txt.Text = String.Empty
                    Select txt.Name
        If empty.Any Then
            MessageBox.Show("Required fields are empty!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Try
                myDbConnect.openConnect()
                Dim sqlString As String = "SELECT * FROM tblusers WHERE usernames='" & txtusername.Text & "' AND password='" & txtpassword.Text & "'"
                Dim sqlCmd As New MySqlCommand(sqlString, myDbConnect.mySqlConString)
                Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader

                Dim username As String = ""
                Dim password As String = ""

                While sqlReader.Read
                    id = sqlReader!idtblusers
                    username = sqlReader!usernames
                    password = sqlReader!password
                End While
                sqlReader.Close()
                sqlCmd.Dispose()

                If txtusername.Text = username And txtpassword.Text = password Then
                    If txtnpassword.Text = txtcpassword.Text Then
                        Try
                            Dim updatesqlstring = "UPDATE tblusers SET usernames = '" & txtnusername.Text & "', password ='" & txtnpassword.Text & "' WHERE (idtblusers ='" & id & "')"
                            Dim updatesqlCmd As New MySqlCommand(updatesqlstring, myDbConnect.mySqlConString)
                            updatesqlCmd.ExecuteNonQuery()
                            updatesqlCmd.Dispose()
                            MessageBox.Show(" 1 record successfully Updated!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            btncancel.PerformClick()
                        Catch ex As MySqlException
                            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                Else
                    MessageBox.Show("Username or Password is incorrect!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As MySqlException
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                myDbConnect.closeConnect()
                GC.Collect()
            End Try

        End If
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Dim ctl As Control
        For Each ctl In Me.GroupBox1.Controls
            If TypeOf ctl Is TextBox Then ctl.Text = String.Empty
        Next
    End Sub
End Class