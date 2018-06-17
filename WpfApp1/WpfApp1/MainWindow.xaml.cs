using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn = null;
        public MainWindow()
        {
            //SqlConnection
            InitializeComponent();
            SqlConnection conn = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"COMP1000\SQLEXPRESS";
            builder.InitialCatalog = "library";
            builder.IntegratedSecurity = true;
            conn.ConnectionString = builder.ConnectionString;

            //SqlCommand
            SqlCommand command = new SqlCommand("select * from authors", conn);

            
            //SqlDataReader
            SqlDataReader reader = null;
            int line = 0;
            try
            {

                conn.Open();
                reader = command.ExecuteReader();

                

                while (reader.Read())
                {
                    if(line == 0)
                    {
                        for(int i = 0; i<reader.FieldCount; i++)
                        {
                            Console.Write($"{reader.GetName(i)} ");
                        }
                    }
                    Console.WriteLine($"{reader[0].ToString()}\t {reader["firstname"]}\t {reader.GetString(2)}");
                    line++;
                }
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if(conn != null)
                {
                    conn.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        
    }
}
