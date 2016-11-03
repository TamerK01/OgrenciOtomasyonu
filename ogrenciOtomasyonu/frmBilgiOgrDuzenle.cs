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
    public partial class frmBilgiOgrDuzenle : Form
    {
        SqlConnection baglan;
        SqlCommand sorgu;
        SqlDataReader dataReader;
        SqlCommand sorgu1;
        string ogrNo;
        SqlTransaction trans = null;

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

        public frmBilgiOgrDuzenle()
        {
            InitializeComponent();
        }

        private void frmOgrDuzenle_Load(object sender, EventArgs e)
        {
            panelOgrDuzenle.Enabled     = false;
            gboxOgrDuzenle.Enabled      = false;
            txtAraOgrAdi.Enabled        = false;
            txtAraOgrSoyadi.Enabled     = false;
            txtAraOgrTC.Enabled         = false;
            txtAraOgrNo.Enabled         = true;
            rbOgrNo.Checked             = true;
            txtAraOgrNo.Focus();
        }

        private void rbOgrNo_CheckedChanged(object sender, EventArgs e)
        {
            txtAraOgrAdi.Enabled        = false;
            txtAraOgrSoyadi.Enabled     = false;
            txtAraOgrTC.Enabled         = false;
            txtAraOgrNo.Enabled         = true;
            txtAraOgrNo.Focus();
        }

        private void rbOgrTC_CheckedChanged(object sender, EventArgs e)
        {
            txtAraOgrAdi.Enabled        = false;
            txtAraOgrSoyadi.Enabled     = false;
            txtAraOgrTC.Enabled         = true;
            txtAraOgrNo.Enabled         = false;
            txtAraOgrTC.Focus();
        }

        private void rbOgrAdi_CheckedChanged(object sender, EventArgs e)
        {
            txtAraOgrAdi.Enabled        = true;
            txtAraOgrSoyadi.Enabled     = false;
            txtAraOgrTC.Enabled         = false;
            txtAraOgrNo.Enabled         = false;
            txtAraOgrAdi.Focus();
        }

        private void rbOgrSoyadi_CheckedChanged(object sender, EventArgs e)
        {
            txtAraOgrAdi.Enabled        = false;
            txtAraOgrSoyadi.Enabled     = true;
            txtAraOgrTC.Enabled         = false;
            txtAraOgrNo.Enabled         = false;
            txtAraOgrSoyadi.Focus();
        }

        private void cboxDoldur()
        {
            sqlBaglan();
            if (baglan.State != ConnectionState.Open) { baglan.Open(); }
            sorgu1 = new SqlCommand();
            sorgu1.CommandText = "SELECT bolumKodu,bolumAdi FROM tbl_bolumler";
            sorgu1.Connection = baglan;
            SqlDataAdapter da = new SqlDataAdapter(sorgu1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            try
            {
                cboxDuzenleOgrBolum.Text = "Bölüm Seçiniz";
                cboxDuzenleOgrBolum.DataSource = ds.Tables[0];
                cboxDuzenleOgrBolum.DisplayMember = "bolumAdi";
                cboxDuzenleOgrBolum.ValueMember = "bolumKodu";
                if (baglan.State == ConnectionState.Open) { baglan.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                string ogrTC;
                string ogrAdi;
                string ogrSoyadi;
                string ogrTel;
                string ogrAdres;
                string ogrBolum;
                string ogrGNO;
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;

                if(rbOgrNo.Checked){
                    sorgu.CommandText = "SELECT * FROM tbl_ogrenciler ogr INNER JOIN tbl_bolumler bol ON ogr.ogrBolum = bol.bolumKodu WHERE ogrNo = @ogrNo";
                    sorgu.Parameters.AddWithValue("@OgrNo",txtAraOgrNo.Text);
                    
                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        dataReader = sorgu.ExecuteReader();
                        while (dataReader.Read())
                        {
                            if (dataReader["ogrNo"].ToString().Equals(txtAraOgrNo.Text))
                            {
                                ogrNo = dataReader["ogrNo"].ToString();
                                ogrTC = dataReader["ogrTC"].ToString();
                                ogrAdi = dataReader["ogrAdi"].ToString();
                                ogrSoyadi = dataReader["ogrSoyadi"].ToString();
                                ogrTel = dataReader["ogrTel"].ToString();
                                ogrAdres = dataReader["ogrAdres"].ToString();
                                ogrBolum = dataReader["bolumAdi"].ToString();
                                ogrGNO = dataReader["ogrGNO"].ToString();

                                panelOgrDuzenle.Enabled     = true;
                                gboxOgrDuzenle.Enabled      = true;

                                cboxDoldur();

                                txtDuzenleOgrTC.Text = ogrTC;
                                txtDuzenleOgrAdi.Text = ogrAdi;
                                txtDuzenleOgrSoyadi.Text = ogrSoyadi;
                                txtDuzenleOgrTel.Text = ogrTel;
                                txtDuzenleOgrAdres.Text = ogrAdres;
                                cboxDuzenleOgrBolum.Text = ogrBolum;
                                txtDuzenleOgrGNO.Text = ogrGNO;
                                lblOgrNo.Text = ogrNo;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Arama Sonucu : Böyle Bir Öğrenci Numarası Bulunamadı!");
                    }
                }else if(rbOgrTC.Checked){
                    sorgu.CommandText = "SELECT * FROM tbl_ogrenciler ogr INNER JOIN tbl_bolumler bol ON ogr.ogrBolum = bol.bolumKodu WHERE ogrTC = @ogrTC";
                    sorgu.Parameters.AddWithValue("@ogrTC", txtAraOgrTC.Text);

                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        dataReader = sorgu.ExecuteReader();
                        while (dataReader.Read())
                        {
                            if (dataReader["ogrTC"].ToString().Equals(txtAraOgrNo.Text))
                            {
                                ogrNo = dataReader["ogrNo"].ToString();
                                ogrTC = dataReader["ogrTC"].ToString();
                                ogrAdi = dataReader["ogrAdi"].ToString();
                                ogrSoyadi = dataReader["ogrSoyadi"].ToString();
                                ogrTel = dataReader["ogrTel"].ToString();
                                ogrAdres = dataReader["ogrAdres"].ToString();
                                ogrBolum = dataReader["bolumAdi"].ToString();
                                ogrGNO = dataReader["ogrGNO"].ToString();

                                panelOgrDuzenle.Enabled = true;
                                gboxOgrDuzenle.Enabled = true;

                                cboxDoldur();

                                txtDuzenleOgrTC.Text = ogrTC;
                                txtDuzenleOgrAdi.Text = ogrAdi;
                                txtDuzenleOgrSoyadi.Text = ogrSoyadi;
                                txtDuzenleOgrTel.Text = ogrTel;
                                txtDuzenleOgrAdres.Text = ogrAdres;
                                cboxDuzenleOgrBolum.Text = ogrBolum;
                                txtDuzenleOgrGNO.Text = ogrGNO;
                                lblOgrNo.Text = ogrNo;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Arama Sonucu : Böyle Bir Öğrenci TC Bulunamadı!");
                    }

                }else if (rbOgrAdi.Checked) {
                    sorgu.CommandText = "SELECT * FROM tbl_ogrenciler ogr INNER JOIN tbl_bolumler bol ON ogr.ogrBolum = bol.bolumKodu WHERE ogrAdi = @ogrAdi";
                    sorgu.Parameters.AddWithValue("@ogrAdi", txtAraOgrAdi.Text);

                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        dataReader = sorgu.ExecuteReader();
                        while (dataReader.Read())
                        {
                            if (dataReader["ogrAdi"].ToString().Equals(txtAraOgrNo.Text))
                            {
                                ogrNo = dataReader["ogrNo"].ToString();
                                ogrTC = dataReader["ogrTC"].ToString();
                                ogrAdi = dataReader["ogrAdi"].ToString();
                                ogrSoyadi = dataReader["ogrSoyadi"].ToString();
                                ogrTel = dataReader["ogrTel"].ToString();
                                ogrAdres = dataReader["ogrAdres"].ToString();
                                ogrBolum = dataReader["bolumAdi"].ToString();
                                ogrGNO = dataReader["ogrGNO"].ToString();

                                panelOgrDuzenle.Enabled = true;
                                gboxOgrDuzenle.Enabled = true;

                                cboxDoldur();

                                txtDuzenleOgrTC.Text = ogrTC;
                                txtDuzenleOgrAdi.Text = ogrAdi;
                                txtDuzenleOgrSoyadi.Text = ogrSoyadi;
                                txtDuzenleOgrTel.Text = ogrTel;
                                txtDuzenleOgrAdres.Text = ogrAdres;
                                cboxDuzenleOgrBolum.Text = ogrBolum;
                                lblOgrNo.Text = ogrNo;
                                txtDuzenleOgrGNO.Text = ogrGNO;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Arama Sonucu : Böyle Bir Öğrenci Adı Bulunamadı!");
                    }

                }else if (rbOgrSoyadi.Checked) {
                    sorgu.CommandText = "SELECT * FROM tbl_ogrenciler ogr INNER JOIN tbl_bolumler bol ON ogr.ogrBolum = bol.bolumKodu WHERE ogrSoyadi = @ogrSoyadi";
                    sorgu.Parameters.AddWithValue("@ogrSoyadi", txtAraOgrSoyadi.Text);

                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        dataReader = sorgu.ExecuteReader();
                        while (dataReader.Read())
                        {
                            if (dataReader["ogrSoyadi"].ToString().Equals(txtAraOgrNo.Text))
                            {
                                ogrNo = dataReader["ogrNo"].ToString();
                                ogrTC = dataReader["ogrTC"].ToString();
                                ogrAdi = dataReader["ogrAdi"].ToString();
                                ogrSoyadi = dataReader["ogrSoyadi"].ToString();
                                ogrTel = dataReader["ogrTel"].ToString();
                                ogrAdres = dataReader["ogrAdres"].ToString();
                                ogrBolum = dataReader["bolumAdi"].ToString();
                                ogrGNO = dataReader["ogrGNO"].ToString();

                                panelOgrDuzenle.Enabled = true;
                                gboxOgrDuzenle.Enabled = true;

                                cboxDoldur();

                                txtDuzenleOgrTC.Text = ogrTC;
                                txtDuzenleOgrAdi.Text = ogrAdi;
                                txtDuzenleOgrSoyadi.Text = ogrSoyadi;
                                txtDuzenleOgrTel.Text = ogrTel;
                                txtDuzenleOgrAdres.Text = ogrAdres;
                                cboxDuzenleOgrBolum.Text = ogrBolum;
                                txtDuzenleOgrGNO.Text = ogrGNO;
                                lblOgrNo.Text = ogrNo;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Arama Sonucu : Böyle Bir Öğrenci Soyadi Bulunamadı!");
                    }

                }else {
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (cbDuzenleOgrSil.Checked)
            {
                if (MessageBox.Show("Öğrenciyi Gerçekten Silmek İstiyor musunuz?", "Öğrenci Otomasyonu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    
                    try
                    {
                        sqlBaglan();
                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu = new SqlCommand();
                        sorgu1 = new SqlCommand();
                        sorgu.Connection = baglan;
                        sorgu1.Connection = baglan;
                        sorgu1.CommandText = "DELETE FROM tbl_ogrenciSistem WHERE ogrNo = @ogrNo";
                        sorgu1.Parameters.AddWithValue("@ogrNo", ogrNo);
                        sorgu.CommandText = "DELETE FROM tbl_ogrenciler WHERE ogrNo = @ogrNo";
                        sorgu.Parameters.AddWithValue("@ogrNo", ogrNo);
                        sorgu1.ExecuteNonQuery();
                        sorgu.ExecuteNonQuery();
                        MessageBox.Show("Öğrenci Silindi!");
                        this.Refresh();
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
                    MessageBox.Show("Öğrenci Silinmedi!");
                }
            }
            else
            {
                MessageBox.Show("Öğrenciyi Silmek İçin Onaylamanız Gerekmektedir!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDuzenleOgrDuzenle.Checked)
                {
                    sqlBaglan();
                    if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                    sorgu.Connection = baglan;
                    sorgu.Parameters.Clear();
                    sorgu.CommandText = "UPDATE tbl_ogrenciler SET ogrTC=@ogrTC, ogrAdi=@ogrAdi, ogrSoyadi=@ogrSoyadi, ogrTel=@ogrTel, ogrAdres=@ogrAdres, ogrBolum=@ogrBolum WHERE ogrNo=@ogrNo";
                    sorgu.Parameters.AddWithValue("@ogrNo", ogrNo.Trim());
                    sorgu.Parameters.AddWithValue("@ogrTC", txtDuzenleOgrTC.Text.Trim());
                    sorgu.Parameters.AddWithValue("@ogrAdi", txtDuzenleOgrAdi.Text.Trim());
                    sorgu.Parameters.AddWithValue("@ogrSoyadi", txtDuzenleOgrSoyadi.Text.Trim());
                    sorgu.Parameters.AddWithValue("@ogrTel", txtDuzenleOgrTel.Text.Trim());
                    sorgu.Parameters.AddWithValue("@ogrAdres", txtDuzenleOgrAdres.Text.Trim());
                    sorgu.Parameters.AddWithValue("@ogrBolum", cboxDuzenleOgrBolum.SelectedValue.ToString().Trim());
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Öğrenci Düzenlendi!");
                }
                else
                {
                    MessageBox.Show("Öğrenciyi Düzenlemek İçin Onayınız Gerekmektedir!");
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
