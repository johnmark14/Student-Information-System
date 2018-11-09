Imports MySql.Data.MySqlClient
Public Class DbConnect
    Private mySqlConnect As MySqlConnection
    Private sqlCmd As MySqlCommand
    '''<summary>
    ''' Constructor
    ''' This initialize database creadentials
    ''' </summary>
    Public Sub New()
        mySqlConnect = New MySqlConnection
        mySqlConnect.ConnectionString =
            "server=localhost;" &
            "port=3306;" &
            "username=root;" &
            "password=jmBeats14;" &
            "database=systemdb;"
    End Sub
    Public Sub testConnect()
        Try
            mySqlConnect.Open()
            MessageBox.Show("Connected to the Database!", "System Connection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As MySqlException
            MessageBox.Show(ex.Message, "System Connection", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            mySqlConnect.Close()
        End Try
    End Sub
    Public Function mySqlConString() As Object
        Return mySqlConnect
    End Function
    Public Sub openConnect()
        Try
            mySqlConnect.Open()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message, "System Connection", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub closeConnect()
        Try
            mySqlConnect.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message, "System Connection", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
