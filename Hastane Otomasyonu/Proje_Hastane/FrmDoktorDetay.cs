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

namespace Proje_Hastane
{
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        public string tc;
     
        SqlBaglantısı bgl = new SqlBaglantısı();

        public void listele()
        {
            //Randevuları Çekme
            DataTable dt = new DataTable();
            Boolean durum = false;
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular  where RandevuDurum='" + durum + "' and RandevuDoktor='" + lblAdSoyad.Text + "'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
       


        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {

            //Tc Çekme
            lblTc.Text = tc;
            
            //Ad Soyad çekme
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorTc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0]+""+dr[1];
            }
            bgl.baglanti().Close();

            //Randevuları Çekme
            listele();

        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fd = new FrmDuyurular();
            fd.Show();


        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnbilgiDuzenle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle fdbd = new FrmDoktorBilgiDuzenle();
            fdbd.tcc = lblTc.Text;
            fdbd.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FrmGirisler fg = new FrmGirisler();
            fg.Show();
            this.Hide();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            hastaTc.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string durum;
            durum = "True";
            SqlCommand komut = new SqlCommand("Update Tbl_Randevular set RandevuDurum=@p1 where Randevuid=@p2",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", durum.ToString()); 
            komut.Parameters.AddWithValue("@p2",hastaTc.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            rchSikayet.Clear();
        }
    }
}
