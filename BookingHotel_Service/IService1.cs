using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BookingHotel_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        //service login
        [OperationContract]
        string Login(string username, string password);

        //service register
        [OperationContract]
        string Register(string username, string password, string nama);

        //service nambah transaksi
        [OperationContract]
        string TambahTransaksi(DateTime tgl_checkin, string nama, int ktp, int totalhari);

        //nambah kamar, status jadi inprogress dan avaibility berkurang
        [OperationContract]
        string TambahDetailTransaksi(int id_trans, int id_kamar, int subtotal);

        [OperationContract]
        //service update/hapus kamar , sama avaibility jadi nambah
        string RemoveDetailTransaksi(int id_detailtrans);


        //cekot mengupdate transaksi total
        [OperationContract]
        string CekotTrans(int id_trans, int total);

        /*[OperationContract]
        //tampil detail transais, semua kamar yang dibook untuk 1 transaksi
        List<TampilDetailTrans> TampilDetailTrans(int *//*id_trans*//*);*/



        [OperationContract]
        [WebGet(UriTemplate = "DetailTrans", ResponseFormat = WebMessageFormat.Json)] //untuk membuat slash, selalu relative
        List<DetailTrans> GetAllTrans(int id_trans); //mendapatkan kumpulan mahasiswa

        [OperationContract]
        [WebGet(UriTemplate = "DetailTrans/id_trans={id_trans}", ResponseFormat = WebMessageFormat.Json)]
        List<DetailTrans> GetDetailTransByIDTrans(int id_trans);




        //kalo mau aja
        [OperationContract]
        //service checkout kamar
        string CekotKamar(string status, int avaibility);



    }

    [DataContract]
    public class DetailTrans
    {
        private string _status, _kodekamar; // _ adalah kesepakatan //variabel lokal
        private int _id_detailtrans, _id_trans, _id_kamar, _subtotal, _harga; 



        [DataMember(Order = 1)] //mengirim data untuk mengurutkan
        public int id_detailtrans
        {
            get { return _id_detailtrans; }
            set { _id_detailtrans = value; }
        }

        [DataMember(Order = 2)] //mengirim data untuk mengurutkan
        public int id_trans
        {
            get { return _id_trans; }
            set { _id_trans = value; }
        }

        [DataMember(Order = 3)] //mengirim data untuk mengurutkan
        public int id_kamar
        {
            get { return _id_kamar; }
            set { _id_kamar = value; }
        }

        [DataMember(Order = 4)] //mengirim data untuk mengurutkan
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember(Order = 5)] //mengirim data untuk mengurutkan
        public int subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        [DataMember(Order = 6)] //mengirim data untuk mengurutkan
        public string kodekamar
        {
            get { return _kodekamar; }
            set { _kodekamar = value; }
        }
        [DataMember(Order = 7)] //mengirim data untuk mengurutkan
        public int harga
        {
            get { return _harga; }
            set { _harga = value; }
        }

    }





}
