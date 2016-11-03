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
    public partial class frmBilgiBolumEkle : Form
    {
        public frmBilgiBolumEkle()
        {
            InitializeComponent();
        }

        SqlConnection baglan;
        SqlCommand sorgu;

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

        private void frmBilgiBolumEkle_Load(object sender, EventArgs e)
        {
            sqlBaglan();
            sorgu = new SqlCommand();
            sorgu.CommandText = "SELECT akaAdi+ ' ' +akaSoyadi AS ADSOYAD FROM tbl_akademisyenler";
            sorgu.Connection = baglan;
            SqlDataAdapter da = new SqlDataAdapter(sorgu);
            DataSet ds = new DataSet();
            da.Fill(ds);
            try
            {
                cboxBolumBaskani.Text = "Bölüm Seçiniz";
                cboxBolumBaskani.DataSource = ds.Tables[0];
                cboxBolumBaskani.DisplayMember = "ADSOYAD";
                cboxBolumBaskani.ValueMember = "ADSOYAD";
                baglan.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
        }

        private void btnBolumKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbolumKodu.Text == "" || txtbolumAdi.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakamazsınız. Lütfen Tekrardan Kontrol Ediniz.");
                }
                else
                {
                    try
                    {
                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }

                        sorgu = new SqlCommand(null, baglan);
                        sorgu.CommandText = "INSERT INTO tbl_bolumler (bolumKodu,bolumAdi,bolumBaskani) VALUES (@bolumKodu, @bolumAdi, @bolumBaskani)";
                        sorgu.Parameters.AddWithValue("@bolumKodu", txtbolumKodu.Text);
                        sorgu.Parameters.AddWithValue("@bolumAdi", txtbolumAdi.Text);
                        sorgu.Parameters.AddWithValue("@bolumBaskani", cboxBolumBaskani.SelectedValue.ToString());

                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu.ExecuteNonQuery();
                        MessageBox.Show("Bölüm Kaydı Başarılı!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                MessageBox.Show(ex.Message, " Message: {0}");
            }
            finally
            {
                 if (baglan.State == ConnectionState.Open) { baglan.Close(); }
            }
        }
    }
}
