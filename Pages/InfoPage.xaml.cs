using MySql.Data.MySqlClient;
using Proyecto2Evaluacion.DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

namespace Proyecto2Evaluacion.Pages
{
    /// <summary>
    /// Lógica de interacción para InfoPage.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        public InfoPage()
        {
            InitializeComponent();

            // Llenar las tablas
            FillTopSoldProducts();
            FillProductsNoStock();
            FillTopExpensiveProducts();
            
        }

        private void FillTopSoldProducts()
        {
            string query = "SELECT pro.ProductName,  SUM(ordd.Quantity) AS TotalQuantity " +
                           "FROM orderdetails AS ordd " +
                           "JOIN products AS pro " +
                           "ON ordd.productID = pro.productID " +
                           "GROUP BY pro.ProductID " +
                           "ORDER BY TotalQuantity DESC " +
                           "LIMIT 5";

            FillDataGrid(query, dataGridTopSold);
        }

        private void FillProductsNoStock()
        {
            string query = "SELECT pro.ProductName " +
                           "FROM products AS pro " +
                           "WHERE pro.UnitsInStock = 0 " +
                           "GROUP BY pro.ProductID ";

            FillDataGrid(query, dataGridNoStock);
        }

        private void FillTopExpensiveProducts()
        {
            string query = "SELECT pro.ProductName, pro.UnitPrice " +
                           "FROM products AS pro " +
                           "GROUP BY pro.ProductID " +
                           "ORDER BY pro.UnitPrice DESC " +
                           "LIMIT 5";

            FillDataGrid(query, dataGridTopExpensive);
        }
        

        private void FillDataGrid(string query, DataGrid dataGrid)
        {
            

            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                {


                    connection.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGrid.ItemsSource = dataTable.DefaultView;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al llenar la tabla: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
                
        }
       
        
    }
}
