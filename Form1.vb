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
End Class
