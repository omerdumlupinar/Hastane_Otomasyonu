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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string tc;
        SqlBaglantısı bgl = new SqlBaglantısı();

        public void yenile()
        {
            //DataGride Branşları çekme
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from  Tbl_Branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //DataGride Doktorları çekme
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (DoktorAd+' '+ DoktorSoyad) As 'Doktorlar' , DoktorBrans from  Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branş Çekme

            SqlCommand komut4 = new SqlCommand("select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                cmbBrans.Items.Add(dr4[0]);
            }
            bgl.baglanti().Close();
        }
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {

            yenile();
            txtid.Enabled = false;
            //Giriş Formundan Tc Çekme
            lblSekreterTc.Text = tc;

            // Sekreter Ad-Soyad Çekme
            SqlCommand komut = new SqlCommand("select sekreterAdSoyad From Tbl_Sekreter where SekreterTc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblSekreterTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {

                lblSekreterAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();


            


        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            if (rchDuyuru.Text==" ")
            {
                MessageBox.Show("Bol Alanı Doldurunuz...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            else
            {
                SqlCommand komut5 = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)", bgl.baglanti());
                komut5.Parameters.AddWithValue("@d1", rchDuyuru.Text);
                komut5.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Duyuru Oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,HastaTc,RandevuDurum,RandevuSikayet) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",mskTarih.Text);
            komut3.Parameters.AddWithValue("@p2",mskSaat.Text);
            komut3.Parameters.AddWithValue("@p3",cmbBrans.Text);
            komut3.Parameters.AddWithValue("@p4",cmbDoktor.Text);
            komut3.Parameters.AddWithValue("@p5",mskTc.Text);
            komut3.Parameters.AddWithValue("@p7",rchSikayet.Text);
            if (chkDurum.Checked==false)
            {
                komut3.Parameters.AddWithValue("@p6", chkDurum.Checked=false);
            }
            if (chkDurum.Checked == true)
            {
                komut3.Parameters.AddWithValue("@p6", chkDurum.Checked = true);
            }
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevunuz Oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);



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

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fdp = new FrmDoktorPaneli();
            fdp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmBrans fb = new FrmBrans();
            fb.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fd = new FrmRandevuListesi();
            fd.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            yenile();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmDuyurular fd = new FrmDuyurular();
            fd.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            FrmGirisler fg = new FrmGirisler();
            fg.Show();
            this.Hide();
        }
    }
}
