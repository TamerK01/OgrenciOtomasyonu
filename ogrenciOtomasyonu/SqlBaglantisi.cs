using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ogrenciOtomasyonu
{
    class SqlBaglantisi
    {
        public SqlConnection baglan;
        public string connectionString;
        public SqlCommand sorgu;
        public SqlDataReader dataReader;
        public SqlCommand sorgu1;
        public SqlDataAdapter da;

        public SqlBaglantisi(){
            try
            {
                connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\ogrenciOtomasyonu\ogrenciOtomasyonu\SqlOtomasyon.mdf;Integrated Security=True;Connect Timeout=30";
                //connectionString = "Data Source=DESKTOP-3S1EN4U;Initial Catalog=ogrOto;Integrated Security=True";
                baglan = new SqlConnection(connectionString);
                sorgu = new SqlCommand();
                sorgu1 = new SqlCommand();
                da = new SqlDataAdapter();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
