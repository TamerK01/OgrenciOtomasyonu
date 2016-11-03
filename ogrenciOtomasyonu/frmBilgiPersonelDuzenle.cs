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
    public partial class frmBilgiPersonelDuzenle : Form
    {
        public frmBilgiPersonelDuzenle()
        {
            InitializeComponent();
        }

        SqlConnection baglan;
        SqlCommand sorgu;
        SqlDataReader dataReader;
        SqlCommand sorgu1;
        string bilgiNo;

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

        private void frmBilgiPersonelDuzenle_Load(object sender, EventArgs e)
        {
            panelOgrDuzenle.Enabled = false;
            gboxOgrDuzenle.Enabled = false;
            txtAraPerAdi.Enabled = false;
            txtAraPerSoyadi.Enabled = false;
            txtAraPerTC.Enabled = false;
            txtAraPerNo.Enabled = true;
            rbPerNo.Checked = true;
            txtAraPerNo.Focus();
        }

        private void rbPerNo_CheckedChanged(object sender, EventArgs e)
        {
            txtAraPerAdi.Enabled = false;
            txtAraPerSoyadi.Enabled = false;
            txtAraPerTC.Enabled = false;
            txtAraPerNo.Enabled = true;
            txtAraPerNo.Focus();
        }

        private void rbPerTC_CheckedChanged(object sender, EventArgs e)
        {
            txtAraPerAdi.Enabled = false;
            txtAraPerSoyadi.Enabled = false;
            txtAraPerTC.Enabled = true;
            txtAraPerNo.Enabled = false;
            txtAraPerNo.Focus();
        }

        private void rbPerAdi_CheckedChanged(object sender, EventArgs e)
        {
            txtAraPerAdi.Enabled = true;
            txtAraPerSoyadi.Enabled = false;
            txtAraPerTC.Enabled = false;
            txtAraPerNo.Enabled = false;
            txtAraPerNo.Focus();
        }

        private void rbPerSoyadi_CheckedChanged(object sender, EventArgs e)
        {
            txtAraPerAdi.Enabled = false;
            txtAraPerSoyadi.Enabled = true;
            txtAraPerTC.Enabled = false;
            txtAraPerNo.Enabled = false;
            txtAraPerNo.Focus();
        }

        private void btnPerAra_Click(object sender, EventArgs e)
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                string bilgiTC;
                string bilgiAdi;
                string bilgiSoyadi;
                string bilgiTel;
                string bilgiAdres;
                string bilgiBolum;
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;

                if (rbPerNo.Checked)
                {
                    try
                    {
                        sorgu.CommandText = "SELECT * FROM tbl_bilgiIslem bil INNER JOIN tbl_bolumler bol ON bil.bilgiBolum = bol.bolumKodu WHERE bilgiNo = @bilgiNo";
                        sorgu.Parameters.AddWithValue("@bilgiNo", txtAraPerNo.Text);

                        dataReader = sorgu.ExecuteReader();
                        if (dataReader.Read())
                        {
                            dataReader.Close();
                            dataReader = sorgu.ExecuteReader();
                            while (dataReader.Read())
                            {
                                if (dataReader["bilgiNo"].ToString().Equals(txtAraPerNo.Text))
                                {
                                    bilgiNo = dataReader["bilgiNo"].ToString();
                                    bilgiTC = dataReader["bilgiTC"].ToString();
                                    bilgiAdi = dataReader["bilgiAdi"].ToString();
                                    bilgiSoyadi = dataReader["bilgiSoyadi"].ToString();
                                    bilgiTel = dataReader["bilgiTel"].ToString();
                                    bilgiAdres = dataReader["bilgiAdres"].ToString();
                                    bilgiBolum = dataReader["bilgiBolum"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenlePerTC.Text = bilgiTC;
                                    txtDuzenlePerAdi.Text = bilgiAdi;
                                    txtDuzenlePerSoyadi.Text = bilgiSoyadi;
                                    txtDuzenlePerTel.Text = bilgiTel;
                                    txtDuzenlePerAdres.Text = bilgiAdres;
                                    cboxDuzenlePerBolum.Text = bilgiBolum;
                                    lblPerNo.Text = bilgiNo;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Personel Numarası Bulunamadı!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (rbPerTC.Checked)
                {
                    try
                    {
                        sorgu.CommandText = "SELECT * FROM tbl_bilgiIslem bil INNER JOIN tbl_bolumler bol ON bil.bilgiBolum = bol.bolumKodu WHERE bilgiTC = @bilgiTC";
                        sorgu.Parameters.AddWithValue("@bilgiTC", txtAraPerTC.Text);

                        dataReader = sorgu.ExecuteReader();
                        if (dataReader.Read())
                        {
                            dataReader.Close();
                            dataReader = sorgu.ExecuteReader();
                            while (dataReader.Read())
                            {
                                if (dataReader["bilgiTC"].ToString().Equals(txtAraPerTC.Text))
                                {
                                    bilgiNo = dataReader["bilgiNo"].ToString();
                                    bilgiTC = dataReader["bilgiTC"].ToString();
                                    bilgiAdi = dataReader["bilgiAdi"].ToString();
                                    bilgiSoyadi = dataReader["bilgiSoyadi"].ToString();
                                    bilgiTel = dataReader["bilgiTel"].ToString();
                                    bilgiAdres = dataReader["bilgiAdres"].ToString();
                                    bilgiBolum = dataReader["bilgiBolum"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenlePerTC.Text = bilgiTC;
                                    txtDuzenlePerAdi.Text = bilgiAdi;
                                    txtDuzenlePerSoyadi.Text = bilgiSoyadi;
                                    txtDuzenlePerTel.Text = bilgiTel;
                                    txtDuzenlePerAdres.Text = bilgiAdres;
                                    cboxDuzenlePerBolum.Text = bilgiBolum;
                                    lblPerNo.Text = bilgiNo;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Personel TC Bulunamadı!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Böyle Bir Arama Sonucu Bulunamadı!", ex.Message);
                    }

                }
                else if (rbPerAdi.Checked)
                {
                    try
                    {
                        sorgu.CommandText = "SELECT * FROM tbl_bilgiIslem bil INNER JOIN tbl_bolumler bol ON bil.bilgiBolum = bol.bolumKodu WHERE bilgiAdi = @bilgiAdi";
                        sorgu.Parameters.AddWithValue("@bilgiAdi", txtAraPerAdi.Text);

                        dataReader = sorgu.ExecuteReader();
                        if (dataReader.Read())
                        {
                            dataReader.Close();
                            dataReader = sorgu.ExecuteReader();
                            while (dataReader.Read())
                            {
                                if (dataReader["bilgiAdi"].ToString().Equals(txtAraPerAdi.Text))
                                {
                                    bilgiNo = dataReader["bilgiNo"].ToString();
                                    bilgiTC = dataReader["bilgiTC"].ToString();
                                    bilgiAdi = dataReader["bilgiAdi"].ToString();
                                    bilgiSoyadi = dataReader["bilgiSoyadi"].ToString();
                                    bilgiTel = dataReader["bilgiTel"].ToString();
                                    bilgiAdres = dataReader["bilgiAdres"].ToString();
                                    bilgiBolum = dataReader["bilgiBolum"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenlePerTC.Text = bilgiTC;
                                    txtDuzenlePerAdi.Text = bilgiAdi;
                                    txtDuzenlePerSoyadi.Text = bilgiSoyadi;
                                    txtDuzenlePerTel.Text = bilgiTel;
                                    txtDuzenlePerAdres.Text = bilgiAdres;
                                    cboxDuzenlePerBolum.Text = bilgiBolum;
                                    lblPerNo.Text = bilgiNo;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Personel Adı Bulunamadı!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Böyle Bir Arama Sonucu Bulunamadı!", ex.Message);
                    }

                }
                else if (rbPerSoyadi.Checked)
                {
                    try
                    {
                        sorgu.CommandText = "SELECT * FROM tbl_bilgiIslem bil INNER JOIN tbl_bolumler bol ON bil.bilgiBolum = bol.bolumKodu WHERE bilgiSoyadi = @bilgiSoyadi";
                        sorgu.Parameters.AddWithValue("@bilgiSoyadi", txtAraPerSoyadi.Text);

                        dataReader = sorgu.ExecuteReader();
                        if (dataReader.Read())
                        {
                            dataReader.Close();
                            dataReader = sorgu.ExecuteReader();
                            while (dataReader.Read())
                            {
                                if (dataReader["bilgiSoyadi"].ToString().Equals(txtAraPerSoyadi.Text))
                                {
                                    bilgiNo = dataReader["bilgiNo"].ToString();
                                    bilgiTC = dataReader["bilgiTC"].ToString();
                                    bilgiAdi = dataReader["bilgiAdi"].ToString();
                                    bilgiSoyadi = dataReader["bilgiSoyadi"].ToString();
                                    bilgiTel = dataReader["bilgiTel"].ToString();
                                    bilgiAdres = dataReader["bilgiAdres"].ToString();
                                    bilgiBolum = dataReader["bilgiBolum"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenlePerTC.Text = bilgiTC;
                                    txtDuzenlePerAdi.Text = bilgiAdi;
                                    txtDuzenlePerSoyadi.Text = bilgiSoyadi;
                                    txtDuzenlePerTel.Text = bilgiTel;
                                    txtDuzenlePerAdres.Text = bilgiAdres;
                                    cboxDuzenlePerBolum.Text = bilgiBolum;
                                    lblPerNo.Text = bilgiNo;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Personel Soyadi Bulunamadı!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Böyle Bir Arama Sonucu Bulunamadı!", ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen Bir Arama Yöntemi Seçiniz!");
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

        private void cboxDoldur()
        {
            sqlBaglan();
            if (baglan.State != ConnectionState.Open) { baglan.Open(); }
            sorgu1 = new SqlCommand();
            sorgu1.CommandText = "SELECT bolumKodu,bolumAdi,bolumBaskani FROM tbl_bolumler";
            sorgu1.Connection = baglan;
            SqlDataAdapter da = new SqlDataAdapter(sorgu1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            try
            {
                cboxDuzenlePerBolum.Text = "Bölüm Seçiniz";
                cboxDuzenlePerBolum.DataSource = ds.Tables[0];
                cboxDuzenlePerBolum.DisplayMember = "bolumAdi";
                cboxDuzenlePerBolum.ValueMember = "bolumKodu";

                if (baglan.State == ConnectionState.Open) { baglan.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
        }

        private void btnPerSil_Click(object sender, EventArgs e)
        {
            if (cbDuzenlePerSil.Checked)
            {
                if (MessageBox.Show("Personeli Gerçekten Silmek İstiyor musunuz?", "Öğrenci Otomasyonu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    try
                    {
                        sqlBaglan();
                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu = new SqlCommand();
                        sorgu1 = new SqlCommand();
                        sorgu.Connection = baglan;
                        sorgu1.Connection = baglan;
                        sorgu1.Parameters.Clear();
                        sorgu.Parameters.Clear();
                        sorgu1.CommandText = "DELETE FROM tbl_bilgiIslemSistemi WHERE bilgiNo = @bilgiNo";
                        sorgu1.Parameters.AddWithValue("@bilgiNo", bilgiNo);
                        sorgu.CommandText = "DELETE FROM tbl_bilgiIslem WHERE bilgiNo = @bilgiNo";
                        sorgu.Parameters.AddWithValue("@bilgiNo", bilgiNo);
                        sorgu1.ExecuteNonQuery();
                        sorgu.ExecuteNonQuery();
                        MessageBox.Show("Personel Silindi!");
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
                else
                {
                    MessageBox.Show("Personel Silinmedi!");
                }
            }
            else
            {
                MessageBox.Show("Personeli Silmek İçin Onaylamanız Gerekmektedir!");
            }
        }

        private void btnPerDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDuzenlePerDuzenle.Checked)
                {
                    sqlBaglan();
                    if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                    sorgu.Connection = baglan;
                    sorgu.Parameters.Clear();
                    sorgu.CommandText = "UPDATE tbl_bilgiIslem SET bilgiTC=@bilgiTC, bilgiAdi=@bilgiAdi, bilgiSoyadi=@bilgiSoyadi, bilgiTel=@bilgiTel, bilgiAdres=@bilgiAdres, bilgiBolum=@bilgiBolum WHERE bilgiNo=@bilgiNo";
                    sorgu.Parameters.AddWithValue("@bilgiNo", bilgiNo.Trim());
                    sorgu.Parameters.AddWithValue("@bilgiTC", txtDuzenlePerTC.Text.Trim());
                    sorgu.Parameters.AddWithValue("@bilgiAdi", txtDuzenlePerAdi.Text.Trim());
                    sorgu.Parameters.AddWithValue("@bilgiSoyadi", txtDuzenlePerSoyadi.Text.Trim());
                    sorgu.Parameters.AddWithValue("@bilgiTel", txtDuzenlePerTel.Text.Trim());
                    sorgu.Parameters.AddWithValue("@bilgiAdres", txtDuzenlePerAdres.Text.Trim());
                    sorgu.Parameters.AddWithValue("@bilgiBolum", cboxDuzenlePerBolum.SelectedValue.ToString().Trim());
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Personel Düzenlendi!");
                }
                else
                {
                    MessageBox.Show("Personeli Düzenlemek İçin Onayınız Gerekmektedir!");
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
    }
}
