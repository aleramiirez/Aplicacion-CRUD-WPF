using MySql.Data.MySqlClient;
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
    /// Lógica de interacción para ReadPage.xaml
    /// </summary>
    public partial class ReadPage : Page
    {
        public ReadPage()
        {
            InitializeComponent();

            Loaded += ReadPage_Loaded; //Para cargar los datos en el combobox
            productComboBox.SelectionChanged += ProductComboBox_SelectionChanged; // Para manejar la selección en el combobox
            productComboBox2.SelectionChanged += ProductComboBox_SelectionChanged2;
        }

        private void ReadPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Llena el ComboBox con nombres de productos
            FillProductComboBox();
            FillProductComboBox2();
        }

        private void FillProductComboBox()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT ProductName FROM products";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string productName = reader["ProductName"].ToString();
                                productComboBox.Items.Add(productName);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (productComboBox.SelectedItem != null)
            {
                string selectedProduct = productComboBox.SelectedItem.ToString();
                FillProductDetails(selectedProduct);
            }
        }

        private void FillProductDetails(string selectedProduct)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM products WHERE ProductName = @selectedProduct";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Usar parámetro para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@selectedProduct", selectedProduct);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Limpiar los datos actuales del DataGrid
                            dataGridProductDetails.ItemsSource = null;

                            // Asignar los nuevos datos al DataGrid
                            dataGridProductDetails.ItemsSource = dataTable.DefaultView;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los detalles del producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // SEGUNDA PARTE

        private void FillProductComboBox2()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT CategoryName FROM categories";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categoryName = reader["CategoryName"].ToString();
                                productComboBox2.Items.Add(categoryName);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProductComboBox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            if (productComboBox2.SelectedItem != null)
            {
                string selectedProduct = productComboBox2.SelectedItem.ToString();
                FillProductDetails2(selectedProduct);
            }
        }

        private void FillProductDetails2(string selectedProduct)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM products AS pro JOIN categories AS cat ON pro.CategoryID = cat.CategoryID WHERE categoryName = @selectedProduct";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Usar parámetro para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@selectedProduct", selectedProduct);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Limpiar los datos actuales del DataGrid
                            dataGridProductDetails2.ItemsSource = null;

                            // Asignar los nuevos datos al DataGrid
                            dataGridProductDetails2.ItemsSource = dataTable.DefaultView;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los detalles del producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
