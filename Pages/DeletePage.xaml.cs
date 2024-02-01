using MySql.Data.MySqlClient;
using System;
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
    /// Lógica de interacción para DeletePage.xaml
    /// </summary>
    public partial class DeletePage : Page
    {
        public DeletePage()
        {
            InitializeComponent();
            Loaded += ReadPage_Loaded; //Para cargar los datos en el combobox
            productComboBox.SelectionChanged += ProductComboBox_SelectionChanged; // Para manejar la selección en el combobox
        }

        private void ReadPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Llena el ComboBox con nombres de productos
            FillProductComboBox();
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

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            if (productComboBox.SelectedItem != null)
            {
                string selectedProduct = productComboBox.SelectedItem.ToString();

                MessageBoxResult result = MessageBox.Show($"¿Está seguro de que desea eliminar el producto '{selectedProduct}'?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                        {
                            connection.Open();

                            string deleteQuery = "DELETE FROM products WHERE ProductName = @selectedProduct";
                            using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@selectedProduct", selectedProduct);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show($"Producto '{selectedProduct}' eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                                    // Actualizar el ComboBox y limpiar el DataGrid después de la eliminación
                                    productComboBox.Items.Remove(selectedProduct);
                                    ClearProductDetails();
                                }
                                else
                                {
                                    MessageBox.Show($"No se pudo eliminar el producto '{selectedProduct}'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto antes de intentar eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearProductDetails()
        {
            // Limpiar los datos actuales del DataGrid
            dataGridProductDetails.ItemsSource = null;
        }
    }
}
