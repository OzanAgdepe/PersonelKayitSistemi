using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;

namespace PersonelKayıtSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=Ozan\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        void FillChart()
        {
            SqlConnection chartStat = new SqlConnection("Data Source=Ozan\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

            DataTable dt=new DataTable();
            chartStat.Open();
            SqlDataAdapter da= new SqlDataAdapter("select PerMaas,PerMeslek from Tbl_Personel",chartStat);
            da.Fill(dt);
            chart1.DataSource=dt;
            chartStat.Close();

            chart1.Series["PerMaas"].XValueMember = "PerMeslek";
            chart1.Series["PerMaas"].YValueMembers = "PerMaas";
            chart1.Titles.Add("Mesleklere göre Maaş İstatistikleri");
        }


        void temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            cmbSehir.Text = "";
            mskMaas.Text = "";
            txtMeslek.Text = "";
            txtAd.Focus();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'personelVeriTabaniDataSet.Tbl_Personel' table. You can move, or remove it, as needed.
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
            FillChart();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("insert into Tbl_Personel(PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek) values(@p2,@p3,@p4,@p5,@p6)",baglanti);
           
            //komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.Parameters.AddWithValue("@p2", txtAd.Text);
            komut.Parameters.AddWithValue("@p3", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p4", cmbSehir.Text);
            komut.Parameters.AddWithValue("@p5", mskMaas.Text);
            komut.Parameters.AddWithValue("@p6", txtMeslek.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel EKlendi");
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            cmbSehir.Text = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            mskMaas.Text = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            txtMeslek.Text = dataGridView1.Rows[secim].Cells[5].Value.ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand sil = new SqlCommand("Delete From Tbl_Personel Where PersonId=@p1",baglanti);
            sil.Parameters.AddWithValue("@p1",txtID.Text);
            sil.ExecuteNonQuery() ;

            baglanti.Close();
            MessageBox.Show("Silme işlemi Gerçekleşti");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand guncelle = new SqlCommand("Update Tbl_Personel Set PerAd=@p1, PerSoyad=@p2, PerSehir=@p3, PerMaas=@p4, PerMeslek=@p5 where personId=@p6",baglanti);

            guncelle.Parameters.AddWithValue("@p1",txtAd.Text);
            guncelle.Parameters.AddWithValue("@p2",txtSoyad.Text);
            guncelle.Parameters.AddWithValue("@p3", cmbSehir.Text);
            guncelle.Parameters.AddWithValue("@p4", mskMaas.Text);
            guncelle.Parameters.AddWithValue("@p5", txtMeslek.Text);
            guncelle.Parameters.AddWithValue("@p6", txtID.Text);
            guncelle.ExecuteNonQuery() ;

            baglanti.Close() ;

            MessageBox.Show("Personel güncellemesi tamamlandı...");
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
