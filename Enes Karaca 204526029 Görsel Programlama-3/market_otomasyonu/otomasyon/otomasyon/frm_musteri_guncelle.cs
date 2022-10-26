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
    public partial class frm_musteri_guncelle : Form
    {
        public int kul_id;

        public frm_musteri_guncelle()
        {
            InitializeComponent();
        }

        private void frm_musteri_guncelle_Load(object sender, EventArgs e)
        {
            MessageBox.Show(kul_id.ToString());

            string sql = " select * from musteri where m_id=" + kul_id;
            SqlConnection con = new SqlConnection(Form1.baglanti);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;


            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows == true)
            {
                txt_mad.Text = Convert.ToString(dr["m_adi"]);
                txt_msoyad.Text = Convert.ToString(dr["m_soyadi"]);
                int durumu = Convert.ToInt32(dr["m_cinsiyeti"]);

                if (durumu == 0)
                {
                    rb_kiz.Checked = true;
                }
                else
                {
                    rb_erkek.Checked = true;
                }
                txt_mfirma.Text = Convert.ToString(dr["m_firma"]);
                txt_mtelefon.Text = Convert.ToString(dr["m_tel"]);
                txt_mmail.Text = Convert.ToString(dr["m_mail"]);
               



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int m_erkek= 0;
            if (rb_erkek.Checked == true) { m_erkek = 1; }


            string sql = " update musteri set m_adi='" + txt_mad.Text + "', " +
                         " m_soyadi='" + txt_msoyad.Text + "',  " +
                         " m_cinsiyeti='" + m_erkek + "', m_firma='" + txt_mfirma.Text + "', "+
                         " m_tel='" + txt_mtelefon.Text + "', m_mail='" + txt_mmail.Text + "' where m_id=" + kul_id;

            SqlConnection con = new SqlConnection(Form1.baglanti);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Güncelleme Gerçekleşti");

            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
