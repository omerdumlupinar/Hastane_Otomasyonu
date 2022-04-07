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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        private void lnkUye_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayıt fhk = new FrmHastaKayıt();
            fhk.Show();
        }
        SqlBaglantısı bgl = new SqlBaglantısı();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from Tbl_Hastalar where HastaTc=@p1 and HastaSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",mskTc.Text);
            komut.Parameters.AddWithValue("@p2",txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmHastaDetay fhd = new FrmHastaDetay();
                fhd.tc = mskTc.Text;
                fhd.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Hatalı Hasta Girişi Lütfen Bilgilerinizi Kontrol Edip Tekrar Giriniz.","Dikkat",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            bgl.baglanti().Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            FrmGirisler fg = new FrmGirisler();
            fg.Show();
            this.Hide();
        }
    }
}
