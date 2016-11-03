using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Sql Kütüphanesi Bağlandı.
using System.Data.Sql;
using System.Data.SqlClient;

namespace ogrenciOtomasyonu
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        SqlBaglantisi sqlBaglan = new SqlBaglantisi();

        private void btnAdminGiris_Click(object sender, EventArgs e)
        {
            frmBilgiGiris frmBilgiGiris = new frmBilgiGiris();
            frmBilgiGiris.Show();
            this.Hide();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (sqlBaglan.baglan.State == ConnectionState.Closed) { sqlBaglan.baglan.Open(); }
            sqlBaglan.sorgu = new SqlCommand();
            sqlBaglan.sorgu.Parameters.Clear();
            sqlBaglan.sorgu.CommandText = "SELECT * FROM tbl_ayarlar";
            sqlBaglan.sorgu.Connection = sqlBaglan.baglan;
            GnoHesapla Gno = new GnoHesapla();
            try
            {
                sqlBaglan.dataReader = sqlBaglan.sorgu.ExecuteReader();
                if (sqlBaglan.dataReader.Read())
                {
                    if (sqlBaglan.dataReader["ogrGiris"].Equals(0)) btnOgrenciGiris.Enabled = false; //Öğrenciler Sisteme Girebilecek mi?
                    if (sqlBaglan.dataReader["akaGiris"].Equals(0)) btnAkaGiris.Enabled = false; //Öğrenciler Sisteme Girebilecek mi?
                }
                Gno.HesaplaGno(2016103001);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlBaglan.baglan.State == ConnectionState.Open) { sqlBaglan.baglan.Close(); }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Otomasyonu Tamamen Kapatmak İstiyor musunuz?", "Öğrenci Otomasyonu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Dispose(true);
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
