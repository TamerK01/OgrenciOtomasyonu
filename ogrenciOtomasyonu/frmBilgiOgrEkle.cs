using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ogrenciOtomasyonu
{
    public partial class frmBilgiOgrEkle : Form
    {
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

        public frmBilgiOgrEkle()
        {
            InitializeComponent();
        }

        private void frmOgrEkle_Load(object sender, EventArgs e)
        {
            sqlBaglan();
            sorgu = new SqlCommand();
            sorgu.CommandText = "SELECT bolumKodu,bolumAdi FROM tbl_bolumler";
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

        private int OgrNoOlustur()
        {
            if (baglan.State != ConnectionState.Open) { baglan.Open(); }
            string ogrNo1;
            sorgu = new SqlCommand();
            sorgu.CommandText = "SELECT ogrNo FROM tbl_ogrenciler";
            sorgu.Connection = baglan;
            Random rnd = new Random();
            ogrNo1 = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + rnd.Next(1, 100);
            try
            {
                dataReader = sorgu.ExecuteReader();
                while(dataReader.Read())
                {
                    if (dataReader["ogrNo"].Equals(ogrNo))
                    {
                        ogrNo1 = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + rnd.Next(1, 100);
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

            ogrNo = Int32.Parse(ogrNo1);
            return ogrNo;
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

        private void btnOgrenciKaydet_Click_1(object sender, EventArgs e)
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
                        int OgrenciNo = OgrNoOlustur();

                        sorgu = new SqlCommand(null, baglan);
                        sorgu.CommandText = "INSERT INTO tbl_ogrenciler VALUES (@ogrNo, @ogrTC, @ogrAdi, @ogrSoyadi, @ogrTel, @ogrAdres, @ogrBolum, @ogrGNO)";
                        sorgu.Parameters.AddWithValue("@ogrNo", OgrenciNo);
                        sorgu.Parameters.AddWithValue("@ogrTC", txtogrTC.Text);
                        sorgu.Parameters.AddWithValue("@ogrAdi", txtogrAdi.Text);
                        sorgu.Parameters.AddWithValue("@ogrSoyadi", txtogrSoyadi.Text);
                        sorgu.Parameters.AddWithValue("@ogrTel", txtogrTelefon.Text);
                        sorgu.Parameters.AddWithValue("@ogrAdres", txtogrAdres.Text);
                        sorgu.Parameters.AddWithValue("@ogrBolum", cboxBolum.SelectedValue.ToString());
                        sorgu.Parameters.AddWithValue("@ogrGNO", 0);

                        sorgu1 = new SqlCommand(null, baglan);
                        sorgu1.CommandText = "INSERT INTO tbl_ogrenciSistem VALUES (@ogrNo1, @ogrSifre, @ogrGiris, @ogrYetki)";
                        sorgu1.Parameters.AddWithValue("@ogrNo1", OgrenciNo);
                        sorgu1.Parameters.AddWithValue("@ogrSifre", md5Sifrele(txtogrTC.Text));
                        sorgu1.Parameters.AddWithValue("@ogrGiris", DateTime.Now);
                        sorgu1.Parameters.AddWithValue("@ogrYetki", 0);


                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu.ExecuteNonQuery();
                        sorgu1.ExecuteNonQuery();
                        MessageBox.Show("Öğrenci Kaydı Başarılı! - Öğrenci Numarası : " + OgrenciNo);
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
                try
                {
                    myTransaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    MessageBox.Show(ex2.Message, "  Message: {0}");
                }
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
