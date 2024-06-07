using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;


namespace rtwpb8
{
    class dbconn : IDisposable
    {
        public MySqlConnection myconnect;

        public dbconn()
        {
            
            Initialize();
        }

        public void Initialize()
        {

            //string connectionString = "Server=sql12.freesqldatabase.com; Database=sql12708861; User=sql12708861; Password=7KWwzP86iK; Port=3306";
            //string connectionString = "Server=premium296.web-hosting.com; Database=kenkarlo_pcup_db; User=kenkarlo_cydric; Password=2x40xp~ggqG=; Port=";
            //string connectionString = "Server=sql12.freesqldatabase.com; Database=sql12667698; User=sql12667698; Password='HzpnEQ6Lyk'; Port=3306;  Convert Zero Datetime = true";
            string connectionString = "Server=localhost; Database=rtwpb8_db; User=root; Password=''; Port=3306;";
            myconnect = new MySqlConnection(connectionString);
        }

        public bool Openconnection()
        {
            try
            {
                myconnect.Open();
                //MessageBox.Show("Successfully Connected to pcup_db Database");
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact Administtrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid Username/Password, Please try again.");
                        break;

                }
                return false;
            }
        }

        public bool Closeconnection()
        {
            try
            {
                myconnect.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            myconnect.Dispose();
        }
    }
}