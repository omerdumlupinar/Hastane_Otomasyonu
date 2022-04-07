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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;
        SqlBaglantısı bgl = new SqlBaglantısı();


        public void yenile()
        {
            //Randevu Geçmiş Randeular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select* from Tbl_Randevular Where HastaTc='" + lblTc.Text + "' and RandevuDurum='" + lblDurumTrue.Text + "'"
                , bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Aktif Randevu
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select* from Tbl_Randevular Where HastaTc='" + lblTc.Text + "' and RandevuDurum='" + lblDurumFalse.Text + "'", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;



            //Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad From Tbl_Hastalar where HastaTc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0]+ " " +dr[1];
            }
            bgl.baglanti().Close();


            //Randevu durumları
            yenile();

            //Branş Çekme

            SqlCommand komut2 = new SqlCommand("select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
            
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            cmbDoktor.Text = " ";
            SqlCommand komut3 = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
                komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
                SqlDataReader dr = komut3.ExecuteReader();
                while (dr.Read())
                {
                    cmbDoktor.Items.Add(dr[0] + "" + dr[1]);
                }
            
            bgl.baglanti().Close();


        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,RandevuSikayet,RandevuDurum,HastaTc) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut3.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut3.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut3.Parameters.AddWithValue("@p4", cmbDoktor.Text);
            komut3.Parameters.AddWithValue("@p5", rchSikayet.Text);
            komut3.Parameters.AddWithValue("@p6", lblDurumFalse.Text);          
            komut3.Parameters.AddWithValue("@p7", lblTc.Text);          
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            yenile();
            MessageBox.Show("Randevunuz Oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fbd = new FrmBilgiDuzenle();
            fbd.tc = lblTc.Text;
            fbd.Show();

        }

        private void label8_Click(object sender, EventArgs e)
        {
            FrmGirisler fg = new FrmGirisler();
            fg.Show();
            this.Hide();
        }
    }
}
