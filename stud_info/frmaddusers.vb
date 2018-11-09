Imports MySql.Data.MySqlClient
Public Class frmaddusers
    Dim myDbConnect As New DbConnect
    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Try
            If txtusername.Text = String.Empty Or txtpassword.Text = String.Empty Or txtcpassword.Text = String.Empty Then
                MessageBox.Show("Required fields are empty!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If txtpassword.Text = txtcpassword.Text Then
                    txtpassword.Text = txtcpassword.Text
                    myDbConnect.openConnect()
                    Dim mysqlString = "INSERT INTO tblusers (usernames, password) VALUES ('" & txtusername.Text & "','" & txtpassword.Text & "')"
                    Dim sqlCmd As New MySqlCommand(mysqlString, myDbConnect.mySqlConString)
                    sqlCmd.ExecuteNonQuery()
                    sqlCmd.Dispose()
                    MessageBox.Show(" 1 record successfully saved!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btncancel.PerformClick()
                Else
                    MessageBox.Show(" Password did not match!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As MySqlException
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myDbConnect.closeConnect()
            GC.Collect()
        End Try
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        txtusername.Clear()
        txtpassword.Clear()
        txtcpassword.Clear()
    End Sub
End Class