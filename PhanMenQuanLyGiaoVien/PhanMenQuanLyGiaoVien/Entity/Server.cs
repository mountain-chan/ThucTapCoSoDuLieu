using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMenQuanLyGiaoVien.Entity
{
    public class Server
    {
        private static readonly string tenServer = @"DESKTOP-SS70L95\LYCHAN";
        private static readonly string tenDataBase = "QuanLyGV";

        private static readonly string strConnect = "Data Source=" + tenServer + ";Initial Catalog=" +
            "" + tenDataBase + ";Integrated Security=True";


        public static bool ThucHienCauLenh(string strQuerry)
        {
            try
            {
                SqlConnection Kn = new SqlConnection(strConnect);
                Kn.Open();
                SqlCommand command = new SqlCommand(strQuerry, Kn);
                command.ExecuteNonQuery();
                Kn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DataTable LayDuLieuBang(string strQuerry)
        {
            try
            {
                SqlConnection Kn = new SqlConnection(strConnect);
                Kn.Open();
                SqlCommand command = new SqlCommand(strQuerry, Kn);
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                DataTable data = new DataTable();
                sqlData.Fill(data);
                Kn.Close();
                return data;
            }
            catch
            {
                MessageBox.Show("Lấy dữ liệu lỗi");
                return null;
            }

        }
    }
}
