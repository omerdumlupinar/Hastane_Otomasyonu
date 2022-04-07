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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }

        SqlBaglantısı bgl = new SqlBaglantısı();
        public void listele()
        {
            //DataGride Doktorları çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da= new SqlDataAdapter("select * from  Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmBrans_Load(object sender, EventArgs e)
        {
            txtBransid.Enabled = false;
            listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("insert into Tbl_Branslar (BransAd) values(@p1)", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", txtBransAd.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Kaydedildi","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dia = new DialogResult();
            dia = MessageBox.Show("Silmek İstediğinize Eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dia == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from Tbl_Branslar where Bransid=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtBransid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                txtBransAd.Clear();
                txtBransAd.Clear();


            }
            else
                MessageBox.Show("Silme İşlemi İptal Edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtBransid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            
        }
    }
}
