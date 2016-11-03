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
    public partial class frmBilgiDersEkle : Form
    {
        public frmBilgiDersEkle()
        {
            InitializeComponent();
        }

        SqlConnection baglan;
        SqlCommand sorgu;
        SqlCommand sorgu1;
        int akaNo;
        SqlDataReader dataReader;

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

        private void frmBilgiDersEkle_Load(object sender, EventArgs e)
        {
            sqlBaglan();
            sorgu = new SqlCommand();
            sorgu.CommandText = "SELECT * FROM tbl_bolumler";
            sorgu.Connection = baglan;
            SqlDataAdapter da = new SqlDataAdapter(sorgu);
            DataSet ds = new DataSet();
            da.Fill(ds);
            try
            {
                cboxBolum.Text = "Bölüm Seçiniz";
                cboxBolum.DataSource = ds.Tables[0];
                cboxBolum.DisplayMember = "bolumAdi";
                cboxBolum.ValueMember = "bolumKodu";
                baglan.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
        }

        private void btnDersKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtdersKodu.Text == "" || txtdersAdi.Text == "" || txtdersKredi.Text == "" || txtdersSinif.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakamazsınız. Lütfen Tekrardan Kontrol Ediniz.");
                }
                else
                {
                    try
                    {
                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }

                        sorgu = new SqlCommand(null, baglan);
                        sorgu.CommandText = "INSERT INTO tbl_dersler (dersKodu,dersAdi,dersKredi,dersSinif,dersBolum) VALUES (@dersKodu, @dersAdi, @dersKredi, @dersSinif, @dersBolum)";
                        sorgu.Parameters.AddWithValue("@dersKodu", txtdersKodu.Text.Trim());
                        sorgu.Parameters.AddWithValue("@dersAdi", txtdersAdi.Text.Trim());
                        sorgu.Parameters.AddWithValue("@dersKredi", txtdersKredi.Text.Trim());
                        sorgu.Parameters.AddWithValue("@dersSinif", txtdersSinif.Text.Trim());
                        sorgu.Parameters.AddWithValue("@dersBolum", cboxBolum.SelectedValue.ToString().Trim());

                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu.ExecuteNonQuery();
                        MessageBox.Show("Ders Kaydı Başarılı!");
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
                if (baglan.State == ConnectionState.Open)
                {
                    if (baglan.State == ConnectionState.Open) { baglan.Close(); }
                }
            }
        }
    }
}
