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

    Private Sub tbNim_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNim.KeyPress
        If e.KeyChar = Chr(13) Then
            ' setelah tekan entar maka Koneksi() akan terpanggil
            Call Koneksi()
            ' lalu ketika kita masukan nim di textbox1 (tekan enter) 
            ' selanjutnya tolong panggilkan table mahasiswa dan tolong isi isi form textbox2 dan textbox3 dan textbox4 

            Cmd = New OdbcCommand("Select * from mahasiswa where nim='" & tbNim.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            ' if kita panggil nim lalu tekan enter maka otomatis textbox2, textbox3, dan textbox4 akan terisi secara otomatis
            If Rd.HasRows Then
                tbNama.Text = Rd.Item("nama")
                tbAlamat.Text = Rd.Item("alamat")
                tbJurusan.Text = Rd.Item("jurusan")
            Else
                ' Jika nim yang kita ketikan salah, maka akan menampilkan alert atau message ("data tidak ada")
                MsgBox("Data Tidak Ada")
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        'If Button2 edit kita klik maka dia akan berubah menjadi tulisan "update"
        If btnEdit.Text = "EDIT" Then
            btnEdit.Text = "UPDATE"
            'button1 dan button2 akan kita false artinya tidak berfungsi
            btnSimpan.Enabled = False
            btnHapus.Enabled = False
            'button4 kita ganti tulisan menjadi batal
            btnTutup.Text = "BATAL"
            ' Lalu kita panggil FieldAktif() yang mana textbox1, textbox2, textbox3 dan textbox 4 kita aktifkan
            Call FieldAktif()
        Else
            ' ini adalah validasi jika textbox1, textbox2, textbox3 dan textbox 4 tidak terisi maka akan muncul alert ("pastikan semua terisi)
            If tbNim.Text = "" Or tbNama.Text = "" Or tbAlamat.Text = "" Or tbJurusan.Text = "" Then
                MsgBox("Pastikan semua field terisi")
            Else
                ' jika semua terisi panggil Koneksi()
                Call Koneksi()
                ' kita update table mahasiswa
                Dim EditData As String = "Update mahasiswa set nama='" & tbNama.Text & "',alamat='" & tbAlamat.Text & "',jurusan='" & tbJurusan.Text & "'where nim='" & tbNim.Text & "'"
                Cmd = New OdbcCommand(EditData, Conn)
                Cmd.ExecuteNonQuery()
                ' jika berhasil tampilkan alert / message ("edit data berhasil")
                MsgBox("Edit data berhasil")
                ' setelah semua sudah tolong tampilkan KondisiAwal()
                Call KondisiAwal()
            End If
        End If
    End Sub
End Class
