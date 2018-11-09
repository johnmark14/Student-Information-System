Imports MySql.Data.MySqlClient
Public Class frmlogin
    Dim myDbConnect As New DbConnect
    Private Sub btncheck_Click(sender As Object, e As EventArgs) Handles btncheck.Click
        myDbConnect.testConnect()
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        If txtusername.Text = String.Empty Or txtpassword.Text = String.Empty Then
            MessageBox.Show("Username and Password are required!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Try
                myDbConnect.openConnect()
                Dim sqlString As String = "SELECT * FROM tblusers WHERE usernames='" & txtusername.Text & "' AND password='" & txtpassword.Text & "'"
                Dim sqlCmd As New MySqlCommand(sqlString, myDbConnect.mySqlConString)
                Dim sqlReader As MySqlDataReader = sqlCmd.ExecuteReader

                Dim username As String = ""
                Dim password As String = ""

                While sqlReader.Read
                    username = sqlReader!usernames
                    password = sqlReader!password
                End While
                sqlReader.Close()

                If txtusername.Text = username And txtpassword.Text = password Then
                    MessageBox.Show("Welcome user: " & username, "User Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    frmmain.Show()
                    Me.Hide()
                Else
                    MessageBox.Show("Username or Password is incorrect!", "User Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Me.Close()
    End Sub
End Class
