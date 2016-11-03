using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ogrenciOtomasyonu
{
    public partial class frmBilgiHarfSisEkle : Form
    {
        public frmBilgiHarfSisEkle()
        {
            InitializeComponent();
        }

        SqlConnection baglan;
        SqlCommand sorgu;
        SqlCommand sorgu1;
        int ogrNo;
        SqlDataReader dataReader;
        SqlTransaction myTransaction;

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

        private void frmBilgiHarfSisEkle_Load(object sender, EventArgs e)
        {
            
        }

        private void btnHarfEkle_Click(object sender, EventArgs e)
        {
            if (txtharfAdi.Text == "" || txtharfDegeri.Text == "")
            {
                MessageBox.Show("Boş Alan Bırakamazsınız. Lütfen Tekrardan Kontrol Ediniz.");
            }
            else
            {
                try
                {
                    sqlBaglan();
                    if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                    sorgu = new SqlCommand(null, baglan);
                    sorgu.Parameters.Clear();
                    sorgu.CommandText = "INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES (@harfAdi, @harfNotu)";
                    sorgu.Parameters.AddWithValue("@harfAdi", txtharfAdi.Text.Trim());
                    sorgu.Parameters.AddWithValue("@harfNotu", txtharfDegeri.Text.Trim());

                    if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Harfli Not Kaydı Başarılı!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (baglan.State == ConnectionState.Open) { baglan.Close(); }
                }
            }
        }
    }
}
