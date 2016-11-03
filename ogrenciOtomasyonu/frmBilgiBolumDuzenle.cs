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
    public partial class frmBilgiBolumDuzenle : Form
    {
        public frmBilgiBolumDuzenle()
        {
            InitializeComponent();
        }

        SqlConnection baglan;
        SqlCommand sorgu;
        SqlDataReader dataReader;
        SqlCommand sorgu1;
        string bolID;

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

        private void cboxDoldur(string deger)
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
                if (deger.Equals("cboxDuzenleBolBaskani"))
                {
                    cboxDuzenleBolBaskani.Text = "Bölüm Seçiniz";
                    cboxDuzenleBolBaskani.DataSource = ds.Tables[0];
                    cboxDuzenleBolBaskani.DisplayMember = "bolumBaskani";
                    cboxDuzenleBolBaskani.ValueMember = "bolumBaskani";
                }
                else if (deger.Equals("cboxBolumBaskani"))
                {
                    cboxBolumBaskani.Text = "Bölüm Seçiniz";
                    cboxBolumBaskani.DataSource = ds.Tables[0];
                    cboxBolumBaskani.DisplayMember = "bolumBaskani";
                    cboxBolumBaskani.ValueMember = "bolumBaskani";
                }

                if (baglan.State == ConnectionState.Open) { baglan.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata : " + ex.Message);
            }
        }

        private void frmBilgiBolumDuzenle_Load(object sender, EventArgs e)
        {
            panelOgrDuzenle.Enabled = false;
            gboxOgrDuzenle.Enabled = false;
            txtAraBolAdi.Enabled = false;
            txtAraBolKodu.Enabled = true;
            rbBolKodu.Checked = true;
            txtAraBolKodu.Focus();
        }

        private void rbBolKodu_CheckedChanged(object sender, EventArgs e)
        {
            cboxBolumBaskani.Enabled = false;
            txtAraBolAdi.Enabled = false;
            txtAraBolKodu.Enabled = true;
            txtAraBolKodu.Focus();
        }

        private void rbBolAdi_CheckedChanged(object sender, EventArgs e)
        {
            cboxBolumBaskani.Enabled = false;
            txtAraBolAdi.Enabled = true;
            txtAraBolKodu.Enabled = false;
            txtAraBolAdi.Focus();
        }

        private void rbAkaAdi_CheckedChanged(object sender, EventArgs e)
        {
            cboxBolumBaskani.Enabled = true;
            txtAraBolAdi.Enabled = false;
            txtAraBolKodu.Enabled = false;
            cboxBolumBaskani.Focus();
            cboxDoldur("cboxBolumBaskani");
        }

        private void btnBolAra_Click(object sender, EventArgs e)
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                string bolumKodu, bolumAdi, bolumBaskani;
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;

                if (rbBolKodu.Checked)
                {
                    try
                    {
                        sorgu.CommandText = "SELECT * FROM tbl_bolumler WHERE bolumKodu = @bolumKodu";
                        sorgu.Parameters.AddWithValue("@bolumKodu", txtAraBolKodu.Text.Trim());

                        dataReader = sorgu.ExecuteReader();
                        if (dataReader.Read())
                        {
                            dataReader.Close();
                            dataReader = sorgu.ExecuteReader();
                            while (dataReader.Read())
                            {
                                if (dataReader["bolumKodu"].ToString().Equals(txtAraBolKodu.Text))
                                {
                                    bolID = dataReader["id"].ToString();
                                    bolumKodu = dataReader["bolumKodu"].ToString();
                                    bolumAdi = dataReader["bolumAdi"].ToString();
                                    bolumBaskani = dataReader["bolumBaskani"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur("cboxDuzenleBolBaskani");

                                    txtDuzenleBolKodu.Text = bolumKodu;
                                    txtDuzenleBolAdi.Text = bolumAdi;
                                    cboxDuzenleBolBaskani.Text = bolumBaskani;
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
                else if (rbBolAdi.Checked)
                {
                    try
                    {
                        sorgu.CommandText = "SELECT * FROM tbl_bolumler WHERE bolumAdi = @bolumAdi";
                        sorgu.Parameters.AddWithValue("@bolumAdi", txtAraBolAdi.Text.Trim());

                        dataReader = sorgu.ExecuteReader();
                        if (dataReader.Read())
                        {
                            dataReader.Close();
                            dataReader = sorgu.ExecuteReader();
                            while (dataReader.Read())
                            {
                                if (dataReader["bolumAdi"].ToString().Equals(txtAraBolAdi.Text.Trim()))
                                {
                                    bolID = dataReader["id"].ToString();
                                    bolumKodu = dataReader["bolumKodu"].ToString();
                                    bolumAdi = dataReader["bolumAdi"].ToString();
                                    bolumBaskani = dataReader["bolumBaskani"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur("cboxDuzenleBolBaskani");

                                    txtDuzenleBolKodu.Text = bolumKodu;
                                    txtDuzenleBolAdi.Text = bolumAdi;
                                    cboxDuzenleBolBaskani.Text = bolumBaskani;
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
                        sorgu.CommandText = "SELECT * FROM tbl_bolumler WHERE bolumBaskani = @bolumBaskani";
                        sorgu.Parameters.AddWithValue("@bolumBaskani", cboxBolumBaskani.SelectedValue.ToString().Trim());

                        dataReader = sorgu.ExecuteReader();
                        if (dataReader.Read())
                        {
                            dataReader.Close();
                            dataReader = sorgu.ExecuteReader();
                            while (dataReader.Read())
                            {
                                if (dataReader["bolumBaskani"].ToString().Equals(cboxBolumBaskani.SelectedValue.ToString().Trim()))
                                {
                                    bolID = dataReader["id"].ToString();
                                    bolumKodu = dataReader["bolumKodu"].ToString();
                                    bolumAdi = dataReader["bolumAdi"].ToString();
                                    bolumBaskani = dataReader["bolumBaskani"].ToString();

                                    panelOgrDuzenle.Enabled = true;
                                    gboxOgrDuzenle.Enabled = true;

                                    cboxDoldur("cboxDuzenleBolBaskani");

                                    txtDuzenleBolKodu.Text = bolumKodu;
                                    txtDuzenleBolAdi.Text = bolumAdi;
                                    cboxDuzenleBolBaskani.Text = bolumBaskani;
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

        private void btnABolDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDuzenleBolDuzenle.Checked)
                {
                    sqlBaglan();
                    if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                    sorgu.Connection = baglan;
                    sorgu.Parameters.Clear();
                    sorgu.CommandText = "UPDATE tbl_bolumler SET bolumKodu=@bolumKodu, bolumAdi=@bolumAdi, bolumBaskani=@bolumBaskani WHERE id=@id";
                    sorgu.Parameters.AddWithValue("@id", bolID.Trim());
                    sorgu.Parameters.AddWithValue("@bolumKodu", txtDuzenleBolKodu.Text.Trim());
                    sorgu.Parameters.AddWithValue("@bolumAdi", txtDuzenleBolAdi.Text.Trim());
                    sorgu.Parameters.AddWithValue("@bolumBaskani", cboxDuzenleBolBaskani.SelectedValue.ToString().Trim());
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Bölüm Düzenlendi!");
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

        private void btnBolSil_Click(object sender, EventArgs e)
        {
            if (cbDuzenleBolSil.Checked)
            {
                if (MessageBox.Show("Bölümü Gerçekten Silmek İstiyor musunuz?", "Öğrenci Otomasyonu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    try
                    {
                        sqlBaglan();
                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu = new SqlCommand();
                        sorgu.Connection = baglan;
                        sorgu.CommandText = "DELETE FROM tbl_bolumler WHERE id = @id";
                        sorgu.Parameters.AddWithValue("@id", bolID.Trim());
                        sorgu.ExecuteNonQuery();
                        MessageBox.Show("Bölüm Silindi!");
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
                    MessageBox.Show("Bölüm Silinmedi!");
                }
            }
            else
            {
                MessageBox.Show("Bölümü Silmek İçin Onaylamanız Gerekmektedir!");
            }
        }
    }
}
