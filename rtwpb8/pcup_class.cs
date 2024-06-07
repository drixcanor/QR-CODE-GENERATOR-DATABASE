using MySql.Data.MySqlClient;
using rtwpb8;
using System;
using System.Windows.Forms;


namespace rtwpb8
{
    class pcup_class
    {
        public static dbconn dbconnect;
        public static MySqlCommand cmd;
        public static MySqlDataReader Myreader;

        public static dbconn dbconnect2;
        public static MySqlCommand cmd2;
        public static MySqlDataReader Myreader2;

        public static Boolean isConn = false;
        public static string[] dgrec = new string[] { "Cydric", "Mayor" };
        public static DialogResult btnResult;

        //public static string id;

        public class ItemWithIdAndName
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public ItemWithIdAndName(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return $"{Id}: {Name}";
            }
        }

        //public static bool Login(string username, string password)
        //{
        //    dbconnect = new dbconn();
        //    if (dbconnect.Openconnection())
        //    {
        //        // Query to check if the provided username and password exist in tbl_users
        //        cmd = new MySqlCommand("SELECT COUNT(*) FROM tbl_users WHERE user_name = @Username AND user_password = @Password", dbconnect.myconnect);
        //        cmd.Parameters.AddWithValue("@Username", username);
        //        cmd.Parameters.AddWithValue("@Password", password);

        //        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

        //        dbconnect.Closeconnection();


        //    }

        //    return false; // Connection to the database failed or login was unsuccessful
        //}



    }
}
