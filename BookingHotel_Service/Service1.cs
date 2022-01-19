using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BookingHotel_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {

        string constring = "Data Source=DESKTOP-8OQRTV4;Initial Catalog=BookingHotel;Persist Security Info=True; User ID=sa;Password=123";
        SqlConnection connection;
        SqlCommand com;

        
        public string Login(string username, string password)
        {
            string nama = "";

            string sql = "select nama from Akun where username = '" + username + "' and password = '" + password + "'";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                nama = reader.GetString(0);
            }
            return nama;
        }



        public string Register(string username, string password, string nama)
        {
            try
            {
                string sql = "insert into Akun values('" + username + "', '" + password + "','" + nama + "')";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    
       
        //tambah trans
        public string TambahTransaksi(DateTime tgl_checkin, string nama, int ktp, int totalhari)
        {


            string sqlFormattedDate1 = tgl_checkin.ToString("yyyy-MM-dd HH:mm:ss.fff");
          //  string sqlFormattedDate2 = tgl_checkout.ToString("yyyy-MM-dd HH:mm:ss.fff");


            string a = "gagal";
            try
            {


                string sql = "INSERT INTO Trans(tgl_checkin, nama_pelanggan, ktp_pelanggan, total_hari) VALUES " +
                    "('" + sqlFormattedDate1 + "', '" + nama+ "', '" + ktp+ "', '" + totalhari+ "')";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();


                a = "sukses";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return a;



        }



        //tambah detail trans, tambah di table detail trans, update avaibility di table tipe kamar -- per kamar, hitung subtotal dan grandtotal

        public string TambahDetailTransaksi(int id_trans, int id_kamar, int subtotal)
        {
         
            try
            {
                string sql = "insert into [dbo].[DetailTrans](id_trans, id_kamar, status ,subtotal) values(" + id_trans+ ", " + id_kamar+ ",'proses', "+subtotal+")";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();


                string sql2 = "update Tipe_Kamar set avaibility = avaibility - " +1 + " where id_kamar= '" + id_kamar + "' ";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();



                connection.Close();

                return "sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }




        }

        //hapus detail trans, , update avaibility di table tipe kamar ++ per kamar, hitung grandtotal dan ssubtotal

        public string RemoveDetailTransaksi(int id_detailtrans)
        {
            try
            {
                string sql = "delete from [dbo].[DetailTrans] where id_detailtrans ="+ id_detailtrans;
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses hapus";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        //update total di tambe transaksi
        public string CekotTrans(int id_trans, int total)
        {
            try
            {
                string sql = "update Trans set total =" + total + "where id_transaksi =" + id_trans;
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses update";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }


        //kalo mau aja
        //update status sama avaibility

        public string CekotKamar(string status, int avaibility)
        {
            try
            {
                string sql = "update Trans set total =" + status + "where id_transaksi" + avaibility;
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses update";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }





        public List<DetailTrans> GetAllTrans(int id_trans)
        {
            string sql = "select id_detailtrans, id_trans, d.id_kamar, status, subtotal, k.kode_kamar, k.harga from DetailTrans d join Tipe_Kamar k on d.id_kamar = k.id_kamar where id_trans = " + id_trans;

            List<DetailTrans> detailtrans = new List<DetailTrans>();
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
           
            try
            {
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    DetailTrans dtl = new DetailTrans();
                    dtl.id_detailtrans = reader.GetInt32(0);
                    dtl.id_trans = reader.GetInt32(1);
                    dtl.id_kamar = reader.GetInt32(2);
                    dtl.status = reader.GetString(3);
                    dtl.subtotal = reader.GetInt32(4);
                    dtl.kodekamar = reader.GetString(5);
                    dtl.harga = reader.GetInt32(6);


                    detailtrans.Add(dtl);

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(sql);
            }
            return detailtrans;


        }

        public List<DetailTrans> GetDetailTransByIDTrans(int id_trans)
        {
            string sql = "select id_detailtrans, id_trans, d.id_kamar, status, subtotal, k.kode_kamar, k.harga from DetailTrans d join Tipe_Kamar k on d.id_kamar = k.id_kamar where id_trans = "+id_trans;

        //    List<DetailTrans> detailtrans = new List<DetailTrans>();
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            List<DetailTrans> detailtrans = new List<DetailTrans>();

            try
            {
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    DetailTrans dtl = new DetailTrans();
                    dtl.id_detailtrans = reader.GetInt32(0);
                    dtl.id_trans = reader.GetInt32(1);
                    dtl.id_kamar = reader.GetInt32(2);
                    dtl.status = reader.GetString(3);
                    dtl.subtotal = reader.GetInt32(4);
                    dtl.kodekamar = reader.GetString(5);
                    dtl.harga = reader.GetInt32(6);


                //    detailtrans.Add(dtl);

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(sql);
            }
            return detailtrans;
        }
    }
}
