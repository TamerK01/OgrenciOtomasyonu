using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ogrenciOtomasyonu
{
    public partial class frmBilgiAkaDuzenle : Form
    {
        public frmBilgiAkaDuzenle()
        {
            InitializeComponent();
        }

        SqlBaglantisi baglan = new SqlBaglantisi();

        string akaNo;
        string sonucDeger;

        private void frmBilgiAkaDuzenle_Load(object sender, EventArgs e)
        {
            panelOgrDuzenle.Enabled = false;
            gboxOgrDuzenle.Enabled = false;
            txtAraAkaAdi.Enabled = false;
            txtAraAkaSoyadi.Enabled = false;
            txtAraAkaTC.Enabled = false;
            txtAraAkaNo.Enabled = true;
            rbAkaNo.Checked = true;
            txtAraAkaNo.Focus();
        }

        private void rbAkaNo_CheckedChanged(object sender, EventArgs e)
        {
            txtAraAkaAdi.Enabled = false;
            txtAraAkaSoyadi.Enabled = false;
            txtAraAkaTC.Enabled = false;
            txtAraAkaNo.Enabled = true;
            txtAraAkaNo.Focus();
        }

        private void rbAkaTC_CheckedChanged(object sender, EventArgs e)
        {
            txtAraAkaAdi.Enabled = false;
            txtAraAkaSoyadi.Enabled = false;
            txtAraAkaTC.Enabled = true;
            txtAraAkaNo.Enabled = false;
            txtAraAkaNo.Focus();
        }

        private void rbAkaAdi_CheckedChanged(object sender, EventArgs e)
        {
            txtAraAkaAdi.Enabled = true;
            txtAraAkaSoyadi.Enabled = false;
            txtAraAkaTC.Enabled = false;
            txtAraAkaNo.Enabled = false;
            txtAraAkaNo.Focus();
        }

        private void rbAkaSoyadi_CheckedChanged(object sender, EventArgs e)
        {
            txtAraAkaAdi.Enabled = false;
            txtAraAkaSoyadi.Enabled = true;
            txtAraAkaTC.Enabled = false;
            txtAraAkaNo.Enabled = false;
            txtAraAkaNo.Focus();
        }

        private void cboxDoldur()
        {
            if (baglan.baglan.State != ConnectionState.Open) { baglan.baglan.Open(); }
            baglan.sorgu1.Parameters.Clear();
            baglan.dataReader.Close();
            baglan.sorgu1.CommandText = "SELECT bolumKodu,bolumAdi,bolumBaskani FROM tbl_bolumler";
            baglan.sorgu1.Connection = baglan.baglan;
            baglan.da = new System.Data.SqlClient.SqlDataAdapter(baglan.sorgu1);
            DataSet ds = new DataSet();
            baglan.da.Fill(ds);
            try
            {
                cboxDuzenleAkaBolum.Text            = "Bölüm Seçiniz";
                cboxDuzenleAkaBolum.DataSource      = ds.Tables[0];
                cboxDuzenleAkaBolum.DisplayMember   = "bolumAdi";
                cboxDuzenleAkaBolum.ValueMember     = "bolumKodu";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
            finally
            {
                if (baglan.baglan.State == ConnectionState.Open) { baglan.baglan.Close(); }
            }
        } 

        private void btnAkaAra_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.baglan.Open();
                if (baglan.baglan.State != ConnectionState.Open) { baglan.baglan.Open(); }
                string akaTC;
                string akaAdi;
                string akaSoyadi;
                string akaTel;
                string akaAdres;
                string akaBolum;
                //baglan.sorgu = new SqlCommand();
                baglan.sorgu.Connection = baglan.baglan;

                if (rbAkaNo.Checked)
                {
                    try
                    {
                        baglan.sorgu.Parameters.Clear();
                        baglan.sorgu.CommandText = "SELECT * FROM tbl_akademisyenler aka INNER JOIN tbl_bolumler bol ON aka.akaBolum = bol.bolumKodu WHERE akaNo = @akaNo";
                        baglan.sorgu.Parameters.AddWithValue("@akaNo", txtAraAkaNo.Text);

                        baglan.dataReader = baglan.sorgu.ExecuteReader();
                        if (baglan.dataReader.Read())
                        {
                            baglan.dataReader.Close();
                            baglan.dataReader = baglan.sorgu.ExecuteReader();
                            if (baglan.dataReader.Read())
                            {
                                if (baglan.dataReader["akaNo"].ToString().Equals(txtAraAkaNo.Text))
                                {
                                    akaNo       = baglan.dataReader["akaNo"].ToString();
                                    akaTC       = baglan.dataReader["akaTC"].ToString();
                                    akaAdi      = baglan.dataReader["akaAdi"].ToString();
                                    akaSoyadi   = baglan.dataReader["akaSoyadi"].ToString();
                                    akaTel      = baglan.dataReader["akaTel"].ToString();
                                    akaAdres    = baglan.dataReader["akaAdres"].ToString();
                                    akaBolum    = baglan.dataReader["bolumAdi"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenleAkaTC.Text = akaTC;
                                    txtDuzenleAkaAdi.Text = akaAdi;
                                    txtDuzenleAkaSoyadi.Text = akaSoyadi;
                                    txtDuzenleAkaTel.Text = akaTel;
                                    txtDuzenleAkaAdres.Text = akaAdres;
                                    cboxDuzenleAkaBolum.Text = akaBolum;
                                    lblAkaNo.Text = akaNo;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Akademisyen Numarası Bulunamadı!");
                        }                
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (rbAkaTC.Checked)
                {
                    try
                    {
                        baglan.sorgu.Parameters.Clear();
                        baglan.sorgu.CommandText = "SELECT * FROM tbl_akademisyenler aka INNER JOIN tbl_bolumler bol ON aka.akaBolum = bol.bolumKodu WHERE akaTC = @akaTC";
                        baglan.sorgu.Parameters.AddWithValue("@akaTC", txtAraAkaTC.Text);

                        baglan.dataReader = baglan.sorgu.ExecuteReader();
                        if (baglan.dataReader.Read())
                        {
                            baglan.dataReader.Close();
                            baglan.dataReader = baglan.sorgu.ExecuteReader();
                            if (baglan.dataReader.Read())
                            {
                                if (baglan.dataReader["akaTC"].ToString().Equals(txtAraAkaTC.Text))
                                {
                                    akaNo = baglan.dataReader["akaNo"].ToString();
                                    akaTC = baglan.dataReader["akaTC"].ToString();
                                    akaAdi = baglan.dataReader["akaAdi"].ToString();
                                    akaSoyadi = baglan.dataReader["akaSoyadi"].ToString();
                                    akaTel = baglan.dataReader["akaTel"].ToString();
                                    akaAdres = baglan.dataReader["akaAdres"].ToString();
                                    akaBolum = baglan.dataReader["bolumAdi"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenleAkaTC.Text = akaTC;
                                    txtDuzenleAkaAdi.Text = akaAdi;
                                    txtDuzenleAkaSoyadi.Text = akaSoyadi;
                                    txtDuzenleAkaTel.Text = akaTel;
                                    txtDuzenleAkaAdres.Text = akaAdres;
                                    cboxDuzenleAkaBolum.Text = akaBolum;
                                    lblAkaNo.Text = akaNo;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Akademisyen TC Bulunamadı!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Böyle Bir Arama Sonucu Bulunamadı!", ex.Message);
                    }

                }
                else if (rbAkaAdi.Checked)
                {
                    try
                    {
                        baglan.sorgu.Parameters.Clear();
                        baglan.sorgu.CommandText = "SELECT * FROM tbl_akademisyenler aka INNER JOIN tbl_bolumler bol ON aka.akaBolum = bol.bolumKodu WHERE akaAdi = @akaAdi";
                        baglan.sorgu.Parameters.AddWithValue("@akaAdi", txtAraAkaAdi.Text);

                        baglan.dataReader = baglan.sorgu.ExecuteReader();
                        if (baglan.dataReader.Read())
                        {
                            baglan.dataReader.Close();
                            baglan.dataReader = baglan.sorgu.ExecuteReader();
                            if (baglan.dataReader.Read())
                            {
                                if (baglan.dataReader["akaAdi"].ToString().Equals(txtAraAkaAdi.Text))
                                {
                                    akaNo = baglan.dataReader["akaNo"].ToString();
                                    akaTC = baglan.dataReader["akaTC"].ToString();
                                    akaAdi = baglan.dataReader["akaAdi"].ToString();
                                    akaSoyadi = baglan.dataReader["akaSoyadi"].ToString();
                                    akaTel = baglan.dataReader["akaTel"].ToString();
                                    akaAdres = baglan.dataReader["akaAdres"].ToString();
                                    akaBolum = baglan.dataReader["bolumAdi"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenleAkaTC.Text = akaTC;
                                    txtDuzenleAkaAdi.Text = akaAdi;
                                    txtDuzenleAkaSoyadi.Text = akaSoyadi;
                                    txtDuzenleAkaTel.Text = akaTel;
                                    txtDuzenleAkaAdres.Text = akaAdres;
                                    cboxDuzenleAkaBolum.Text = akaBolum;
                                    lblAkaNo.Text = akaNo;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Akademisyen Adı Bulunamadı!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Böyle Bir Arama Sonucu Bulunamadı!", ex.Message);
                    }

                }
                else if (rbAkaSoyadi.Checked)
                {
                    try
                    {
                        baglan.sorgu.Parameters.Clear();
                        baglan.sorgu.CommandText = "SELECT * FROM tbl_akademisyenler aka INNER JOIN tbl_bolumler bol ON aka.akaBolum = bol.bolumKodu WHERE akaSoyadi = @akaSoyadi";
                        baglan.sorgu.Parameters.AddWithValue("@akaSoyadi", txtAraAkaSoyadi.Text);

                        baglan.dataReader = baglan.sorgu.ExecuteReader();
                        if (baglan.dataReader.Read())
                        {
                            baglan.dataReader.Close();
                            baglan.dataReader = baglan.sorgu.ExecuteReader();
                            if (baglan.dataReader.Read())
                            {
                                if (baglan.dataReader["akaSoyadi"].ToString().Equals(txtAraAkaSoyadi.Text))
                                {
                                    akaNo = baglan.dataReader["akaNo"].ToString();
                                    akaTC = baglan.dataReader["akaTC"].ToString();
                                    akaAdi = baglan.dataReader["akaAdi"].ToString();
                                    akaSoyadi = baglan.dataReader["akaSoyadi"].ToString();
                                    akaTel = baglan.dataReader["akaTel"].ToString();
                                    akaAdres = baglan.dataReader["akaAdres"].ToString();
                                    akaBolum = baglan.dataReader["bolumAdi"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenleAkaTC.Text = akaTC;
                                    txtDuzenleAkaAdi.Text = akaAdi;
                                    txtDuzenleAkaSoyadi.Text = akaSoyadi;
                                    txtDuzenleAkaTel.Text = akaTel;
                                    txtDuzenleAkaAdres.Text = akaAdres;
                                    cboxDuzenleAkaBolum.Text = akaBolum;
                                    lblAkaNo.Text = akaNo;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Akademisyen Soyadi Bulunamadı!");
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
                if (baglan.baglan.State == ConnectionState.Open) { baglan.baglan.Close(); }
            }
        }

        private void btnAkaSil_Click(object sender, EventArgs e)
        {
            if (cbDuzenleAkaSil.Checked)
            {
                if (MessageBox.Show("Akademisyeni Gerçekten Silmek İstiyor musunuz?", "Öğrenci Otomasyonu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    try
                    {
                        if (baglan.baglan.State != ConnectionState.Open) { baglan.baglan.Open(); }
                        baglan.sorgu.Connection = baglan.baglan;
                        baglan.sorgu1.Connection = baglan.baglan;
                        baglan.sorgu.Parameters.Clear();
                        baglan.sorgu1.Parameters.Clear();
                        baglan.sorgu1.CommandText = "DELETE FROM tbl_akademisyenSistem WHERE akaNo = @akaNo";
                        baglan.sorgu1.Parameters.AddWithValue("@akaNo", akaNo);
                        baglan.sorgu.CommandText = "DELETE FROM tbl_akademisyenler WHERE akaNo = @akaNo";
                        baglan.sorgu.Parameters.AddWithValue("@akaNo", akaNo);
                        baglan.sorgu1.ExecuteNonQuery();
                        baglan.sorgu.ExecuteNonQuery();
                        MessageBox.Show("Akademisyen Silindi!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (baglan.baglan.State == ConnectionState.Open) { baglan.baglan.Close(); }
                    }
                }
                else
                {
                    MessageBox.Show("Akademisyen Silinmedi!");
                }
            }
            else
            {
                MessageBox.Show("Akademisyen Silmek İçin Onaylamanız Gerekmektedir!");
            }
        }

        private void btnAkaDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDuzenleAkaDuzenle.Checked)
                {
                    baglan.baglan.Open();
                    if (baglan.baglan.State != ConnectionState.Open) { baglan.baglan.Open(); }
                    baglan.sorgu.Connection = baglan.baglan;
                    baglan.sorgu.Parameters.Clear();
                    baglan.sorgu.CommandText = "UPDATE tbl_akademisyenler SET akaTC=@akaTC, akaAdi=@akaAdi, akaSoyadi=@akaSoyadi, akaTel=@akaTel, akaAdres=@akaAdres, akaBolum=@akaBolum WHERE akaNo=@akaNo";
                    baglan.sorgu.Parameters.AddWithValue("@akaNo", akaNo.Trim());
                    baglan.sorgu.Parameters.AddWithValue("@akaTC", txtDuzenleAkaTC.Text.Trim());
                    baglan.sorgu.Parameters.AddWithValue("@akaAdi", txtDuzenleAkaAdi.Text.Trim());
                    baglan.sorgu.Parameters.AddWithValue("@akaSoyadi", txtDuzenleAkaSoyadi.Text.Trim());
                    baglan.sorgu.Parameters.AddWithValue("@akaTel", txtDuzenleAkaTel.Text.Trim());
                    baglan.sorgu.Parameters.AddWithValue("@akaAdres", txtDuzenleAkaAdres.Text.Trim());
                    baglan.sorgu.Parameters.AddWithValue("@akaBolum", cboxDuzenleAkaBolum.SelectedValue.ToString().Trim());
                    baglan.sorgu.ExecuteNonQuery();
                    MessageBox.Show("Akademisyen Düzenlendi!");
                }
                else
                {
                    MessageBox.Show("Akademisyen Düzenlemek İçin Onayınız Gerekmektedir!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (baglan.baglan.State == ConnectionState.Open) { baglan.baglan.Close(); }
            }
        }

    }
}
