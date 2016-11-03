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
using System.Security.Cryptography;

namespace ogrenciOtomasyonu
{
    public partial class frmBilgiAkaEkle : Form
    {
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

        public frmBilgiAkaEkle()
        {
            InitializeComponent();
        }

        private int AkaNoOlustur()
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

        private void frmBilgiAkaEkle_Load(object sender, EventArgs e)
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

        private void btnOgrenciKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtogrAdi.Text == "" || txtogrSoyadi.Text == "" || txtogrTC.Text == "" || txtogrTelefon.Text == "" || txtogrAdres.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakamazsınız. Lütfen Tekrardan Kontrol Ediniz.");
                }
                else
                {
                    try
                    {
                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        int AkademisyenNo = AkaNoOlustur();

                        sorgu = new SqlCommand(null, baglan);
                        sorgu.CommandText = "INSERT INTO tbl_akademisyenler (akaNo,akaTC,akaAdi,akaSoyadi,akaTel,akaAdres,akaBolum) VALUES (@akaNo, @akaTC, @akaAdi, @akaSoyadi, @akaTel, @akaAdres, @akaBolum)";
                        sorgu.Parameters.AddWithValue("@akaNo", AkademisyenNo);
                        sorgu.Parameters.AddWithValue("@akaTC", txtogrTC.Text);
                        sorgu.Parameters.AddWithValue("@akaAdi", txtogrAdi.Text);
                        sorgu.Parameters.AddWithValue("@akaSoyadi", txtogrSoyadi.Text);
                        sorgu.Parameters.AddWithValue("@akaTel", txtogrTelefon.Text);
                        sorgu.Parameters.AddWithValue("@akaAdres", txtogrAdres.Text);
                        sorgu.Parameters.AddWithValue("@akaBolum", cboxBolum.SelectedValue.ToString());

                        sorgu1 = new SqlCommand(null, baglan);
                        sorgu1.CommandText = "INSERT INTO tbl_akademisyenSistem (akaNo,akaSifre,akaGiris,akaYetki) VALUES (@akaNo1, @akaSifre, @akaGiris, @akaYetki)";
                        sorgu1.Parameters.AddWithValue("@akaNo1", AkademisyenNo);
                        sorgu1.Parameters.AddWithValue("@akaSifre", md5Sifrele(txtogrTC.Text));
                        sorgu1.Parameters.AddWithValue("@akaGiris", DateTime.Now);
                        sorgu1.Parameters.AddWithValue("@akaYetki", 0);


                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu.ExecuteNonQuery();
                        sorgu1.ExecuteNonQuery();
                        MessageBox.Show("Akademisyen Kaydı Başarılı!");
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
