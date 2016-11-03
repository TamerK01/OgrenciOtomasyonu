using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ogrenciOtomasyonu
{
    public partial class frmBilgiPersonelEkle : Form
    {
        public frmBilgiPersonelEkle()
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

        private String md5Sifrele(String sifrelenecekMetin)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
            dizi = md5.ComputeHash(dizi);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        private int PerNoOlustur()
        {
            if (baglan.State != ConnectionState.Open) { baglan.Open(); }
            string akaNo1;
            sorgu = new SqlCommand();
            sorgu.CommandText = "SELECT akaNo FROM tbl_akademisyenler";
            sorgu.Connection = baglan;
            Random rnd = new Random();
            akaNo1 = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + rnd.Next(1, 100);
            try
            {
                dataReader = sorgu.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader["akaNo"].Equals(akaNo))
                    {
                        akaNo1 = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + rnd.Next(1, 100);
                    }
                }
                if (baglan.State == ConnectionState.Open) { baglan.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
            finally
            {
                if (baglan.State == ConnectionState.Open) { baglan.Close(); }
            }

            akaNo = Int32.Parse(akaNo1);
            return akaNo;
        }

        private void frmBilgiPersonelEkle_Load(object sender, EventArgs e)
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

        private void btnPersonelKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtperAdi.Text == "" || txtperSoyadi.Text == "" || txtperTC.Text == "" || txtperTelefon.Text == "" || txtperAdres.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakamazsınız. Lütfen Tekrardan Kontrol Ediniz.");
                }
                else
                {
                    try
                    {
                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        int PersonelNo = PerNoOlustur();

                        sorgu = new SqlCommand(null, baglan);
                        sorgu.CommandText = "INSERT INTO tbl_bilgiIslem (bilgiNo,bilgiTC,bilgiAdi,bilgiSoyadi,bilgiBolum,bilgiTel,bilgiAdres) VALUES (@bilgiNo, @bilgiTC, @bilgiAdi, @bilgiSoyadi, @bilgiBolum, @bilgiTel, @bilgiAdres)";
                        sorgu.Parameters.AddWithValue("@bilgiNo", PersonelNo);
                        sorgu.Parameters.AddWithValue("@bilgiTC", txtperTC.Text);
                        sorgu.Parameters.AddWithValue("@bilgiAdi", txtperAdi.Text);
                        sorgu.Parameters.AddWithValue("@bilgiSoyadi", txtperSoyadi.Text);
                        sorgu.Parameters.AddWithValue("@bilgiTel", txtperTelefon.Text);
                        sorgu.Parameters.AddWithValue("@bilgiAdres", txtperAdres.Text);
                        sorgu.Parameters.AddWithValue("@bilgiBolum", cboxBolum.SelectedValue.ToString());

                        sorgu1 = new SqlCommand(null, baglan);
                        sorgu1.CommandText = "INSERT INTO tbl_bilgiIslemSistemi (bilgiNo,bilgiSifre,bilgiGiris,bilgiYetki) VALUES (@bilgiNo1, @bilgiSifre, @bilgiGiris, @bilgiYetki)";
                        sorgu1.Parameters.AddWithValue("@bilgiNo1", PersonelNo);
                        sorgu1.Parameters.AddWithValue("@bilgiSifre", md5Sifrele(txtperTC.Text));
                        sorgu1.Parameters.AddWithValue("@bilgiGiris", DateTime.Now);
                        sorgu1.Parameters.AddWithValue("@bilgiYetki", 0);


                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu.ExecuteNonQuery();
                        sorgu1.ExecuteNonQuery();
                        MessageBox.Show("Bilgi İşlem Personeli Kaydı Başarılı! - Bilgi İşlem Personeli Numarası : " + PersonelNo);
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
