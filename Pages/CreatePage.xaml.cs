using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            Loaded += CreateProduct_Loaded; // Manejador de evento para cargar las categorías al inicio
        }

        private void CreateProduct_Loaded(object sender, RoutedEventArgs e)
        {
            // Llena el ComboBox con las categorías existentes al cargar la página
            FillCategoryComboBox();
        }

        private void FillCategoryComboBox()
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
                                categoryComboBox.Items.Add(categoryName);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las categorías: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int CategoryID(string categoryName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT CategoryID FROM categories WHERE CategoryName = @categoryName";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@categoryName", categoryName);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int categoryID = Convert.ToInt32(reader["CategoryID"]);
                                return categoryID;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las categorías: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return 0;
        }

        private void CreateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                {
                    connection.Open();

                    string productName = productNameTextBox.Text;
                    string categoryName = categoryComboBox.SelectedItem?.ToString();

                    // Obtén el CategoryID utilizando el método CategoryID
                    int categoryID = CategoryID(categoryName);

                    decimal unitPrice = decimal.Parse(unitPriceTextBox.Text);

                    string insertQuery = "INSERT INTO products (ProductName, CategoryID, UnitPrice) VALUES (@productName, @categoryID, @unitPrice)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@productName", productName);
                        cmd.Parameters.AddWithValue("@categoryID", categoryID);
                        cmd.Parameters.AddWithValue("@unitPrice", unitPrice);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Producto '{productName}' creado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            // Limpiar los TextBox después de crear el producto si lo deseas
                            productNameTextBox.Text = "";
                            unitPriceTextBox.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("No se pudo crear el producto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
