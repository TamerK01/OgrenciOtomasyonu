using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ogrenciOtomasyonu
{
    class GnoHesapla
    {
        public SqlBaglantisi sqlBaglan;
        public int ogrNo;
        public float ogrGno;
        public GnoHesapla()
        {
            try
            {
                sqlBaglan = new SqlBaglantisi();
                if (sqlBaglan.baglan.State != ConnectionState.Open) { sqlBaglan.baglan.Open(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } //GnoHesapla Çağırıldığında İlk Çalışacak Foksiyon

        public void HesaplaGno(int ogrNo){
            try
            {
                if (sqlBaglan.baglan.State != ConnectionState.Open) { sqlBaglan.baglan.Open(); }
                sqlBaglan.sorgu.Connection = sqlBaglan.baglan;
                sqlBaglan.sorgu.Parameters.Clear();
                sqlBaglan.sorgu.CommandText = "SELECT * FROM tbl_ogrenciler WHERE ogrNo = @ogrNo";
                sqlBaglan.sorgu.Parameters.AddWithValue("@ogrNo", ogrNo);
                sqlBaglan.dataReader = sqlBaglan.sorgu.ExecuteReader();
                if (sqlBaglan.dataReader.Read())
                {
                    MessageBox.Show("Aranan Öğrencinin GNOsu " + sqlBaglan.dataReader["ogrGNO"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlBaglan.baglan.State == ConnectionState.Open) { sqlBaglan.baglan.Close(); }
            }
        }


    }
}
