using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace otomasyon
{
    public partial class frm_musteri_kayit : Form
    {
        public frm_musteri_kayit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int m_erkek = 0;

            if (rb_erkek.Checked == true)
            {
                m_erkek = 1;
            }

            string sql_text = "insert into musteri values('" + txt_mad.Text + "'" +
               ",'" + txt_msoyad.Text + "', '" + m_erkek+ "' , '" + txt_mfirma.Text + "'"+
               ", '" + txt_mtelefon.Text + "', '" + txt_mmail.Text + "' )";

            SqlConnection baglanti = new SqlConnection(Form1.baglanti);
            baglanti.Open();

            SqlCommand cmd = new SqlCommand(sql_text, baglanti);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            baglanti.Close();

            MessageBox.Show("Kayıt İşlemi Gerçekleşti");
            txt_mad.Clear();
            txt_msoyad.Clear();
            txt_mfirma.Clear();
            txt_mtelefon.Clear();
            txt_mmail.Clear();

            fnk_liste_musteri_doldur();
        }

        public void fnk_liste_musteri_doldur()
            {
                string sql = " select m_id as 'Müşteri Kodu', m_adi as 'Müşteri Adı', m_soyadi as 'Müşteri Soyad', " +
                             " case m_cinsiyeti when 0 then 'Kadın' else 'Erkek' end as 'Müşteri Cinsiyeti', " +
                             " m_firma as 'Müşteri Firma',"+
                             " m_tel as 'Müşteri Telefon', m_mail as 'Müşteri Mail' "+
                             " from musteri order by m_id desc";

                SqlConnection baglan = new SqlConnection(Form1.baglanti);
                baglan.Open();

                SqlDataAdapter da = new SqlDataAdapter(sql, baglan);

                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView2.DataSource = ds.Tables[0];
                baglan.Close();
            }

            

        

        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Müşteri Kodu"].Value);

            frm_musteri_guncelle gun = new frm_musteri_guncelle();
            gun.kul_id = id;
            gun.ShowDialog();
            fnk_liste_musteri_doldur();

        }

        private void silToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult mesaj = new DialogResult();
            mesaj = MessageBox.Show("Seçili Kaydı Silmek İstiyormusunuz", "Silme İşlemi", MessageBoxButtons.YesNo);

            if (mesaj == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Müşteri Kodu"].Value);

                string sql = " delete from musteri where m_id=" + id;

                SqlConnection con = new SqlConnection(Form1.baglanti);
                con.Open();

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                con.Close();
                MessageBox.Show("Silme İşlemi Gerçekleşti");

                fnk_liste_musteri_doldur(); 


            }
        }

        private void frm_musteri_kayit_Load(object sender, EventArgs e)
        {
            fnk_liste_musteri_doldur();
        }

        private void frm_musteri_kayit_Activated(object sender, EventArgs e)
        {
            fnk_liste_musteri_doldur();
        }
    }
}
