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

namespace ogrenciOtomasyonu
{
    public partial class frmBilgiAyarlar : Form
    {
        SqlConnection baglan;
        SqlCommand sorgu;
        SqlCommand sorgu1;
        SqlCommand sorgu2;
        SqlDataReader dataReader;
        string bilgiNo;
        public static string bilgiNoAdi;

        private void sqlBaglan()
        {
            try
            {
                //baglan = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\uzMan\\Documents\\Visual Studio 2013\\Projects\\ogrenciOtomasyonu\\ogrenciOtomasyonu\\ogrenciOtomasyon.mdf;Integrated Security=True;Connect Timeout=30");
                baglan = new SqlConnection("Data Source=DESKTOP-3S1EN4U;Initial Catalog=ogrOto;Integrated Security=True");
                baglan.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public frmBilgiAyarlar()
        {
            InitializeComponent();
        }

        private void btnAyarGüncelle_Click(object sender, EventArgs e)
        {

            sqlBaglan();
            sorgu = new SqlCommand();
            sorgu.CommandText = "UPDATE tbl_ayarlar SET ogrKayit = @ogrKayit, akaKayit = @akaKayit, bilgiKayit = @bilgiKayit, ogrGiris = @ogrGiris, akaGiris = @akaGiris";
            int OgrenciGiris = (cboxOgrGiris.Text.Equals("Açık")) ? 1 : 0;
            int AkademisyenGiris = (cboxAkaGiris.Text.Equals("Açık")) ? 1 : 0;
            int OgrenciKayit = (cboxOgrKayit.Text.Equals("Açık")) ? 1 : 0;
            int AkademisyenKayit = (cboxAkaKayit.Text.Equals("Açık")) ? 1 : 0;
            int BilgiKayit = (cboxBilgiKayit.Text.Equals("Açık")) ? 1 : 0;

            sorgu.Parameters.AddWithValue("@ogrKayit", OgrenciKayit);
            sorgu.Parameters.AddWithValue("@akaKayit", AkademisyenKayit);
            sorgu.Parameters.AddWithValue("@bilgiKayit", BilgiKayit);
            sorgu.Parameters.AddWithValue("@ogrGiris", OgrenciGiris);
            sorgu.Parameters.AddWithValue("@akaGiris", AkademisyenGiris);

            try
            {
                sorgu.Connection = baglan;
                sorgu.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            
            this.Close();
        }

        private void frmAyarlar_Load(object sender, EventArgs e)
        {
            sqlBaglan();
            sorgu = new SqlCommand();
            sorgu.CommandText = "SELECT * FROM tbl_ayarlar";
            sorgu.Connection = baglan;
            dataReader = sorgu.ExecuteReader();

            if (dataReader.Read())
            {
                string akaGiris     = (dataReader["akaGiris"].Equals(1)) ? "Açık" : "Kapalı";
                string ogrGiris     = (dataReader["ogrGiris"].Equals(1)) ? "Açık" : "Kapalı";
                string ogrKayit     = (dataReader["ogrKayit"].Equals(1)) ? "Açık" : "Kapalı";
                string akaKayit     = (dataReader["akaKayit"].Equals(1)) ? "Açık" : "Kapalı";
                string bilgiKayit   = (dataReader["bilgiKayit"].Equals(1)) ? "Açık" : "Kapalı";

                string akaGiris1    = (!dataReader["akaGiris"].Equals(1)) ? "Açık" : "Kapalı";
                string ogrGiris1    = (!dataReader["ogrGiris"].Equals(1)) ? "Açık" : "Kapalı";
                string ogrKayit1    = (!dataReader["ogrKayit"].Equals(1)) ? "Açık" : "Kapalı";
                string akaKayit1    = (!dataReader["akaKayit"].Equals(1)) ? "Açık" : "Kapalı";
                string bilgiKayit1  = (!dataReader["bilgiKayit"].Equals(1)) ? "Açık" : "Kapalı";

                cboxAkaGiris.Items.Add(akaGiris);
                cboxOgrGiris.Items.Add(ogrGiris);
                cboxOgrKayit.Items.Add(ogrKayit);
                cboxAkaKayit.Items.Add(akaKayit);
                cboxBilgiKayit.Items.Add(bilgiKayit);

                cboxAkaGiris.Items.Add(akaGiris1);
                cboxOgrGiris.Items.Add(ogrGiris1);
                cboxOgrKayit.Items.Add(ogrKayit1);
                cboxAkaKayit.Items.Add(akaKayit1);
                cboxBilgiKayit.Items.Add(bilgiKayit1);

                cboxAkaGiris.Text = akaGiris;
                cboxOgrGiris.Text = ogrGiris;
                cboxOgrKayit.Text = ogrKayit;
                cboxAkaKayit.Text = akaKayit;
                cboxBilgiKayit.Text = bilgiKayit;
            }
        }
    }
}
