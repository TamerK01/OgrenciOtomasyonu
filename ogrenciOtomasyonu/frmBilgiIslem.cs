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
    public partial class frmBilgiIslem : Form
    {
        SqlConnection baglan;
        SqlCommand sorgu;
        SqlCommand sorgu1;
        SqlDataReader dataReader;

        public frmBilgiIslem()
        {
            InitializeComponent();
        }

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

        private void refOgr()
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;
                string ogrCount;
                string maxGNO;

                try
                {
                    sorgu.CommandText = "SELECT COUNT(1) AS sayiOgr, MAX(ogrGNO) AS maxGNO FROM tbl_ogrenciler";
                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ogrCount    = dataReader["sayiOgr"].ToString();
                        maxGNO      = dataReader["maxGNO"].ToString();
                        lblToplamOgr.Text   = ogrCount;
                        lblEnYukGNO.Text    = maxGNO;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void refDers()
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;
                string dersAdi, dersKredi, dersBolum, dersSinif;
                int toplamDers = 0;

                try
                {
                    sorgu.CommandText = "SELECT * FROM tbl_dersler";

                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        dataReader = sorgu.ExecuteReader();
                        lboxDersler.Items.Clear();
                        lboxDersler.Items.Add("ADI-KREDISI-BOLUMU-SINIFI");
                        while (dataReader.Read())
                        {
                            toplamDers++;
                            dersAdi = dataReader["dersAdi"].ToString();
                            dersKredi = dataReader["dersKredi"].ToString();
                            dersBolum = dataReader["dersBolum"].ToString();
                            dersSinif = dataReader["dersSinif"].ToString();
                            lboxDersler.Items.Add(dersAdi+"-"+dersKredi+"-"+dersBolum+"-"+dersSinif);
                        }

                        lblToplamDers.Text = toplamDers.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Arama Sonucu : Böyle Bir Harfli Not Adı Bulunamadı!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void refBol()
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;
                string bolumKodu, bolumAdi, bolumBaskani;
                int toplamBolum = 0;

                try
                {
                    sorgu.CommandText = "SELECT * FROM tbl_bolumler";

                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        dataReader = sorgu.ExecuteReader();
                        lboxBolumler.Items.Clear();
                        lboxBolumler.Items.Add("KODU-ADI-BASKANI");
                        while (dataReader.Read())
                        {

                            bolumKodu = dataReader["bolumKodu"].ToString();
                            bolumAdi = dataReader["bolumAdi"].ToString();
                            bolumBaskani = dataReader["bolumBaskani"].ToString();

                            toplamBolum++;
                            lboxBolumler.Items.Add(bolumKodu + "-" + bolumAdi + "-" + bolumBaskani);
                           
                        }
                        lblToplamBolum.Text = toplamBolum.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Arama Sonucu : Böyle Bir Harfli Not Adı Bulunamadı!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void refAka()
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;
                string akaCount;

                try
                {
                    sorgu.CommandText = "SELECT COUNT(1) AS sayiAka FROM tbl_akademisyenler";
                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        akaCount = dataReader["sayiAka"].ToString(); ;
                        lblToplamAka.Text = akaCount;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void refNotlar()
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;
                string harfAdi, harfNotu;
                int toplamHarfliNotlar = 0;

                try
                {
                    sorgu.CommandText = "SELECT * FROM tbl_harfSistemi";

                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        dataReader = sorgu.ExecuteReader();
                        lboxHarfliNotlar.Items.Clear();
                        lboxHarfliNotlar.Items.Add("HARF ADI - HARF DEĞERİ");
                        while (dataReader.Read())
                        {
                            toplamHarfliNotlar++;
                            harfAdi = dataReader["harfAdi"].ToString();
                            harfNotu = dataReader["harfNotu"].ToString();
                            lboxHarfliNotlar.Items.Add(harfAdi + " - " + harfNotu);
                        }

                        lblToplamHarfliNotlar.Text = toplamHarfliNotlar.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Arama Sonucu : Böyle Bir Harfli Not Adı Bulunamadı!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void frmBilgiIslem_Load(object sender, EventArgs e)
        {
            frmMain frmMain = new frmMain();
            frmMain.Visible = false;
            tsslblBilgiNoAdi.Text = frmBilgiGiris.bilgiNoAdi;
        }

        private void frmBilgiIslem_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain frmMain = new frmMain();
            frmMain.Show();
            this.Hide();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBilgiOgrEkle frmOgrEkle = new frmBilgiOgrEkle();
            frmOgrEkle.Show();
        }

        private void düzenleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBilgiAyarlar frmAyarlar = new frmBilgiAyarlar();
            frmAyarlar.Show();
        }

        private void düzenlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBilgiOgrDuzenle frmOgrDuzenle = new frmBilgiOgrDuzenle();
            frmOgrDuzenle.Show();
        }

        private void ekleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBilgiAkaEkle frmBilgiAkaEkle = new frmBilgiAkaEkle();
            frmBilgiAkaEkle.Show();
        }

        private void ekleToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmBilgiAyarlar frmBilgiAyarlar = new frmBilgiAyarlar();
            frmBilgiAyarlar.Show();
        }

        private void düzenlemeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBilgiAkaDuzenle frmBilgiAkaDuzenle = new frmBilgiAkaDuzenle();
            frmBilgiAkaDuzenle.Show();
        }

        private void ekleToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmBilgiPersonelEkle frmBilgiPersonelEkle = new frmBilgiPersonelEkle();
            frmBilgiPersonelEkle.Show();
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBilgiPersonelDuzenle frmBilgiPersonelDuzenle = new frmBilgiPersonelDuzenle();
            frmBilgiPersonelDuzenle.Show();
        }

        private void ekleToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmBilgiHarfSisEkle frmBilgiHarfSisEkle = new frmBilgiHarfSisEkle();
            frmBilgiHarfSisEkle.Show();
        }

        private void düzenleToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmBilgiHarfSisDuzenle frmBilgiHarfSisDuzenle = new frmBilgiHarfSisDuzenle();
            frmBilgiHarfSisDuzenle.Show();
        }

        private void btnRefOgr_Click(object sender, EventArgs e)
        {
            refOgr();
        }

        private void btnRefBol_Click(object sender, EventArgs e)
        {
            refBol();
        }

        private void btnRefAka_Click(object sender, EventArgs e)
        {
            refAka();
        }

        private void btnRefDers_Click(object sender, EventArgs e)
        {
            refDers();
        }

        private void btnRefNotlar_Click(object sender, EventArgs e)
        {
            refNotlar();
        }

        private void ekleToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmBilgiBolumEkle frmBilgiBolumEkle = new frmBilgiBolumEkle();
            frmBilgiBolumEkle.Show();
        }

        private void düzenleSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBilgiBolumDuzenle frmBilgiBolumDuzenle = new frmBilgiBolumDuzenle();
            frmBilgiBolumDuzenle.Show();
        }

        private void ekleToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmBilgiDersEkle frmBilgiDersEkle = new frmBilgiDersEkle();
            frmBilgiDersEkle.Show();
        }

        private void düzenleSilToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBilgiDersDuzenle frmBilgiDersDuzenle = new frmBilgiDersDuzenle();
            frmBilgiDersDuzenle.Show();
        }

    }
}
