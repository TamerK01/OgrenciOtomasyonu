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
    public partial class frmBilgiDersDuzenle : Form
    {
        public frmBilgiDersDuzenle()
        {
            InitializeComponent();
        }

        SqlConnection baglan;
        SqlCommand sorgu;
        SqlDataReader dataReader;
        SqlCommand sorgu1;
        string dersID;

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
                cboxDuzenleDersBolum.Text = "Bölüm Seçiniz";
                cboxDuzenleDersBolum.DataSource = ds.Tables[0];
                cboxDuzenleDersBolum.DisplayMember = "bolumAdi";
                cboxDuzenleDersBolum.ValueMember = "bolumKodu";

                if (baglan.State == ConnectionState.Open) { baglan.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
        }

        private void frmBilgiDersDuzenle_Load(object sender, EventArgs e)
        {

        }

        private void btndersAra_Click(object sender, EventArgs e)
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                string dersKodu, dersAdi, dersKredi, dersBolum, dersSinif;
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;

                if (rbdersKodu.Checked)
                {
                    try
                    {
                        sorgu.CommandText = "SELECT * FROM tbl_dersler ders INNER JOIN tbl_bolumler bolum ON ders.dersBolum = bolum.bolumKodu WHERE dersKodu = @dersKodu";
                        //sorgu.CommandText = "SELECT * FROM tbl_dersler WHERE dersKodu = @dersKodu";
                        sorgu.Parameters.AddWithValue("@dersKodu", txtAraDersKodu.Text);

                        dataReader = sorgu.ExecuteReader();
                        if (dataReader.Read())
                        {
                            dataReader.Close();
                            dataReader = sorgu.ExecuteReader();
                            while (dataReader.Read())
                            {
                                if (dataReader["dersKodu"].ToString().Equals(txtAraDersKodu.Text))
                                {
                                    dersID = dataReader["id"].ToString();
                                    dersKodu = dataReader["dersKodu"].ToString();
                                    dersAdi = dataReader["dersAdi"].ToString();
                                    dersKredi = dataReader["dersKredi"].ToString();
                                    dersBolum = dataReader["bolumAdi"].ToString();
                                    dersSinif = dataReader["dersSinif"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenleDersKodu.Text = dersKodu;
                                    txtDuzenleDersAdi.Text = dersAdi;
                                    txtDuzenleDersKredi.Text = dersKredi;
                                    txtDuzenleDersSinif.Text = dersSinif;
                                    cboxDuzenleDersBolum.Text = dersBolum;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Ders Kodu Bulunamadı!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (rbdersAdi.Checked)
                {
                    try
                    {
                        sorgu.CommandText = "SELECT * FROM tbl_dersler WHERE dersAdi = @dersAdi";
                        sorgu.Parameters.AddWithValue("@dersAdi", txtAraDersAdi.Text.Trim());

                        dataReader = sorgu.ExecuteReader();
                        if (dataReader.Read())
                        {
                            dataReader.Close();
                            dataReader = sorgu.ExecuteReader();
                            while (dataReader.Read())
                            {
                                if (dataReader["dersAdi"].ToString().Equals(txtAraDersAdi.Text.Trim()))
                                {
                                    dersID = dataReader["id"].ToString();
                                    dersKodu = dataReader["dersKodu"].ToString();
                                    dersAdi = dataReader["dersAdi"].ToString();
                                    dersKredi = dataReader["dersKredi"].ToString();
                                    dersBolum = dataReader["dersBolum"].ToString();
                                    dersSinif = dataReader["dersSinif"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur();

                                    txtDuzenleDersKodu.Text = dersKodu;
                                    txtDuzenleDersAdi.Text = dersAdi;
                                    txtDuzenleDersKredi.Text = dersKredi;
                                    txtDuzenleDersSinif.Text = dersSinif;
                                    cboxDuzenleDersBolum.Text = dersBolum;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Arama Sonucu : Böyle Bir Ders Adı Bulunamadı!");
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

        private void rbdersKodu_CheckedChanged(object sender, EventArgs e)
        {
            txtAraDersKodu.Enabled = true;
            txtAraDersAdi.Enabled = false;
            txtAraDersKodu.Focus();
        }

        private void rbdersAdi_CheckedChanged(object sender, EventArgs e)
        {
            txtAraDersAdi.Enabled = true;
            txtAraDersKodu.Enabled = false;
            txtAraDersAdi.Focus();
        }

        private void btndersDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDuzenleDersDuzenle.Checked)
                {
                    sqlBaglan();
                    if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                    sorgu.Connection = baglan;
                    sorgu.Parameters.Clear();
                    sorgu.CommandText = "UPDATE tbl_dersler SET dersKodu=@dersKodu, dersAdi=@dersAdi, dersKredi=@dersKredi, dersBolum=@dersBolum, dersSinif=@dersSinif WHERE id=@id";
                    sorgu.Parameters.AddWithValue("@id", dersID.Trim());
                    sorgu.Parameters.AddWithValue("@dersKodu", txtDuzenleDersKodu.Text.Trim());
                    sorgu.Parameters.AddWithValue("@dersAdi", txtDuzenleDersAdi.Text.Trim());
                    sorgu.Parameters.AddWithValue("@dersKredi", txtDuzenleDersKredi.Text.Trim());
                    sorgu.Parameters.AddWithValue("@dersSinif", txtDuzenleDersSinif.Text.Trim());
                    sorgu.Parameters.AddWithValue("@dersBolum", cboxDuzenleDersBolum.SelectedValue.ToString().Trim());
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Ders Düzenlendi!");
                }
                else
                {
                    MessageBox.Show("Bölümü Düzenlemek İçin Onayınız Gerekmektedir!");
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

        private void btndersSil_Click(object sender, EventArgs e)
        {
            if (cbDuzenleDersSil.Checked)
            {
                if (MessageBox.Show("Dersi Gerçekten Silmek İstiyor musunuz?", "Öğrenci Otomasyonu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    try
                    {
                        sqlBaglan();
                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu = new SqlCommand();
                        sorgu.Connection = baglan;
                        sorgu.CommandText = "DELETE FROM tbl_dersler WHERE id = @id";
                        sorgu.Parameters.AddWithValue("@id", dersID.Trim());
                        sorgu.ExecuteNonQuery();
                        MessageBox.Show("Ders Silindi!");
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
                    MessageBox.Show("Ders Silinmedi!");
                }
            }
            else
            {
                MessageBox.Show("Dersi Silmek İçin Onaylamanız Gerekmektedir!");
            }
        }
    }
}
