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
using System.Security.Cryptography; //Kayıt olurken şifreyi md5 e çevirecemiz kütüphane

namespace ogrenciOtomasyonu
{
    public partial class frmBilgiGiris : Form
    {
        SqlConnection baglan;
        SqlCommand sorgu;
        SqlCommand sorgu1;
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

        //MD5 şifreleme yapan fonksiyon
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

        public frmBilgiGiris()
        {
            InitializeComponent();
        }

        private string bilgiNoOlustur()
        {
            sqlBaglan();
            sorgu = new SqlCommand();
            //sorgu.CommandText = "SELECT bilgiSifre,bilgiAdi,bilgiSoyadi FROM tbl_bilgiIslemSistemi WHERE bilgiNo = @girisBilgiNo";
            sorgu.CommandText = "SELECT bilgiNo FROM tbl_bilgiIslem";
            sorgu.Connection = baglan;
            Random rnd = new Random();
            bilgiNo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + rnd.Next(1, 100);
            try
            {
                dataReader = sorgu.ExecuteReader();
                // Kullanımı dataReader["bilgiKayit"]
                //if (dataReader.Read()){ MessageBox.Show(dataReader["ogrKayit"].ToString());}
                while(dataReader.Read())
                {
                    if (dataReader["bilgiNo"].Equals(bilgiNo))
                    {
                        bilgiNo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + rnd.Next(1, 100);
                    }
                }
                baglan.Close();
                dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
            return bilgiNo;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtKayitAdi.Text == "" || txtKayitAdres.Text == "" || txtKayitBolum.Text == "" || txtKayitSifre.Text == "" || txtKayitSoyadi.Text == "" || txtKayitTC.Text == "" || txtKayitTel.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakamazsınız Lütfen Tekrar Kontrol Ediniz.");
                }
                else
                {
                    if (txtKayitSifreOnay.Text.Equals(txtKayitSifre.Text))
                    {
                        sqlBaglan();
                        sorgu = new SqlCommand(null, baglan);
                        sorgu1 = new SqlCommand(null, baglan);
                        sorgu.CommandText = "INSERT INTO tbl_bilgiIslem (bilgiNo,bilgiAdi,bilgiSoyadi,bilgiTC,bilgiBolum,bilgiTel,bilgiAdres) VALUES (@bilgiNo,@bilgiAdi,@bilgiSoyadi,@bilgiTC,@bilgiBolum,@bilgiTel,@bilgiAdres)";
                        sorgu1.CommandText = "INSERT INTO tbl_bilgiIslemSistemi (bilgiNo,bilgiSifre,bilgiGiris,bilgiYetki) VALUES (@bilgiNo,@bilgiSifre,@bilgiGiris,@bilgiYetki)";

                        sorgu.Parameters.AddWithValue("@bilgiNo", bilgiNoOlustur());
                        sorgu.Parameters.AddWithValue("@bilgiAdi", txtKayitAdi.Text);
                        sorgu.Parameters.AddWithValue("@bilgiSoyadi", txtKayitSoyadi.Text);
                        sorgu.Parameters.AddWithValue("@bilgiTC", txtKayitTC.Text);
                        sorgu.Parameters.AddWithValue("@bilgiBolum", txtKayitBolum.Text);
                        sorgu.Parameters.AddWithValue("@bilgiTel", txtKayitTel.Text);
                        sorgu.Parameters.AddWithValue("@bilgiAdres", txtKayitAdres.Text);

                        sorgu1.Parameters.AddWithValue("@bilgiNo", bilgiNoOlustur());
                        sorgu1.Parameters.AddWithValue("@bilgiSifre", md5Sifrele(txtKayitSifre.Text));
                        sorgu1.Parameters.AddWithValue("@bilgiGiris", DateTime.Now);
                        sorgu1.Parameters.AddWithValue("@bilgiYetki", 0);

                        baglan.Open();
                        sorgu.ExecuteNonQuery();
                        sorgu1.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Başarılı!");
                    }
                    else
                    {
                        MessageBox.Show("Şifreler Uyuşmuyor. Lütfen Tekrar Deneyiniz.");
                        MessageBox.Show(txtKayitSifre.Text.ToString() + " " + txtKayitSifreOnay.Text.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmBilgiGiris_Load(object sender, EventArgs e)
        {
            sqlBaglan();
            sorgu = new SqlCommand();
            sorgu.CommandText = "SELECT * FROM tbl_ayarlar";
            sorgu.Connection = baglan;
            try
            {
                dataReader = sorgu.ExecuteReader();
                // Kullanımı dataReader["bilgiKayit"]
                //if (dataReader.Read()){ MessageBox.Show(dataReader["ogrKayit"].ToString());}
                if (dataReader.Read())
                {
                    if (dataReader["bilgiKayit"].Equals(0)) groupBox2.Enabled = false; //bilgiKayit sistemine bilgi işlem personeli kaydi olacak mı?
                }
                baglan.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBilgiGiris_Click(object sender, EventArgs e)
        {
            if (txtBilgiGirisNo.Text == "" || txtBilgiGirisSifre.Text == "")
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız!");
            }
            else
            {
                sqlBaglan();
                sorgu = new SqlCommand();
                //sorgu.CommandText = "SELECT bilgiSifre,bilgiAdi,bilgiSoyadi FROM tbl_bilgiIslemSistemi WHERE bilgiNo = @girisBilgiNo";
                sorgu.CommandText = "SELECT bilSis.bilgiNo,bilIs.bilgiAdi,bilIs.bilgiSoyadi,bilSis.bilgiSifre FROM tbl_bilgiIslem bilIs INNER JOIN tbl_bilgiIslemSistemi bilSis ON bilIs.bilgiNo  = bilSis.bilgiNo WHERE bilIs.bilgiNo=@girisBilgiNo";
                sorgu.Parameters.AddWithValue("@girisBilgiNo", txtBilgiGirisNo.Text);
                sorgu.Connection = baglan;
                try
                {
                    dataReader = sorgu.ExecuteReader();
                    // Kullanımı dataReader["bilgiKayit"]
                    //if (dataReader.Read()){ MessageBox.Show(dataReader["ogrKayit"].ToString());}
                    if (dataReader.Read())
                    {
                        if (dataReader["bilgiSifre"].Equals(md5Sifrele(txtBilgiGirisSifre.Text)))
                        {
                            bilgiNoAdi = dataReader["bilgiNo"].ToString() + " - " + dataReader["bilgiAdi"].ToString() + " " + dataReader["bilgiSoyadi"].ToString();
                            sorgu1 = new SqlCommand();
                            sorgu1.CommandText = "UPDATE tbl_bilgiIslemSistemi SET bilgiGiris = @bilgiGiris WHERE bilgiNo = @bilgiNo";
                            sorgu1.Parameters.AddWithValue("@bilgiGiris", DateTime.Now);
                            sorgu1.Parameters.AddWithValue("@bilgiNo", dataReader["bilgiNo"]);
                            dataReader.Close();
                            sorgu1.Connection = baglan;
                            sorgu1.ExecuteNonQuery();
                            frmBilgiIslem frmBilgiIslem = new frmBilgiIslem();
                            frmMain frmMain = new frmMain();
                            this.Hide();
                            frmBilgiIslem.Show();
                            frmMain.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı Bulunamadı veya Yanlış Şifre Kullanımı");
                        }
                    }
                    baglan.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata : " + ex.Message);
                }
            }
        }

        private void frmBilgiGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmMain frmMain = new frmMain();
            //frmMain.Visible = true;
            //frmMain.Close();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(bilgiNoOlustur());
        }

        private void frmBilgiGiris_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain frmMain = new frmMain();
            frmMain.Visible = true;
            frmMain.Show();
            this.Hide();
        }

    }
}
