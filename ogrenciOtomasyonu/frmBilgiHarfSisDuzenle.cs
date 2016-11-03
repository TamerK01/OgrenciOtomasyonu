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
    public partial class frmBilgiHarfSisDuzenle : Form
    {
        public frmBilgiHarfSisDuzenle()
        {
            InitializeComponent();
        }

        SqlConnection baglan;
        SqlCommand sorgu;
        SqlDataReader dataReader;
        SqlCommand sorgu1;
        string harfAdi;
        string harfNotu;
        string harfID;

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

        private void btnAra_Click(object sender, EventArgs e)
        {
            try
            {
                sqlBaglan();
                if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                sorgu = new SqlCommand();
                sorgu.Connection = baglan;

                try
                {
                    sorgu.CommandText = "SELECT * FROM tbl_harfSistemi WHERE harfAdi = @harfAdi";
                    sorgu.Parameters.AddWithValue("@harfAdi", txtAraHarfAdi.Text);

                    dataReader = sorgu.ExecuteReader();
                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        dataReader = sorgu.ExecuteReader();
                        while (dataReader.Read())
                        {
                            if (dataReader["harfAdi"].ToString().Equals(txtAraHarfAdi.Text.Trim()))
                            {
                                harfID = dataReader["id"].ToString();
                                harfAdi = dataReader["harfAdi"].ToString();
                                harfNotu = dataReader["harfNotu"].ToString();

                                panelOgrDuzenle.Enabled = true;
                                gboxOgrDuzenle.Enabled = true;

                                lblHarfAdi.Text = harfAdi;
                                txtDuzenleHarfDeger.Text = harfNotu;
                            }
                        }
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
                    sorgu.CommandText = "UPDATE tbl_harfSistemi SET harfNotu=@harfNotu WHERE id=@harfID";
                    sorgu.Parameters.AddWithValue("@harfID", harfID);
                    sorgu.Parameters.AddWithValue("@harfNotu", float.Parse(txtDuzenleHarfDeger.Text.Trim()));
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Harfli Not Düzenlendi!");
                }
                else
                {
                    MessageBox.Show("Harfli Notu Düzenlemek İçin Onayınız Gerekmektedir!");
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
                if (MessageBox.Show("Harfli Notu Gerçekten Silmek İstiyor musunuz?", "Öğrenci Otomasyonu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    try
                    {
                        sqlBaglan();
                        if (baglan.State != ConnectionState.Open) { baglan.Open(); }
                        sorgu = new SqlCommand();
                        sorgu.Connection = baglan;
                        sorgu.CommandText = "DELETE FROM tbl_harfSistemi WHERE harfAdi = @harfAdi";
                        sorgu.Parameters.AddWithValue("@harfAdi", harfAdi);
                        sorgu.ExecuteNonQuery();
                        sorgu.ExecuteNonQuery();
                        MessageBox.Show("Harfli Not Silindi!");
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
                    MessageBox.Show("Harfli Not Silinmedi!");
                }
            }
            else
            {
                MessageBox.Show("Harfli Notu Silmek İçin Onaylamanız Gerekmektedir!");
            }
        }
    }
}
