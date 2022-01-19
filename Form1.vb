Imports System.Data.Odbc
Public Class Form1
    Dim Conn As OdbcConnection
    Dim Cmd As OdbcCommand
    Dim Ds As DataSet
    Dim Da As OdbcDataAdapter
    Dim Rd As OdbcDataReader
    Dim MyDB As String
    Sub Koneksi()
        ' Memanggil database yaitu nama database kita adalah db_universitas
        MyDB = "Driver={MySQL ODBC 3.51 Driver};Database=vb;Server=localhost;uid=root"
        Conn = New OdbcConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub

    Sub KondisiAwal()
        tbNim.Text = ""
        tbNama.Text = ""
        tbAlamat.Text = ""
        tbJurusan.Text = ""

        tbNim.Enabled = False
        tbNama.Enabled = False
        tbAlamat.Enabled = False
        tbJurusan.Enabled = False

        tbNim.MaxLength = 15

        btnSimpan.Text = "INPUT"
        btnEdit.Text = "EDIT"
        btnHapus.Text = "HAPUS"
        btnTutup.Text = "TUUTUP"

        btnSimpan.Enabled = True
        btnEdit.Enabled = True
        btnHapus.Enabled = True
        btnTutup.Enabled = True

        Call Koneksi()

        Da = New OdbcDataAdapter("Select * From mahasiswa", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "mahasiswa")
        DataGridView1.DataSource = Ds.Tables("mahasiswa")
    End Sub

    Sub FieldAktif()
        tbNim.Enabled = True
        tbNama.Enabled = True
        tbAlamat.Enabled = True
        tbJurusan.Enabled = True

        tbNim.Focus()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If btnSimpan.Text = "INPUT" Then
            btnSimpan.Text = "SIMPAN"

            btnEdit.Enabled = False
            btnHapus.Enabled = False
            btnTutup.Text = "BATAL"

            Call FieldAktif()
        Else
            If tbNim.Text = "" Or tbNama.Text = "" Or tbAlamat.Text = "" Or tbJurusan.Text = "" Then
                MsgBox("Pastikan semua field terisi")
            Else
                Call Koneksi()

                Dim InputData As String = "Insert into mahasiswa values('" & tbNim.Text & "','" & tbNama.Text & "','" & tbAlamat.Text & "','" & tbJurusan.Text & "')"
                Cmd = New OdbcCommand(InputData, Conn)
                Cmd.ExecuteNonQuery()

                MsgBox("Input data berhasil")

                Call KondisiAwal()
            End If
        End If
    End Sub
End Class
