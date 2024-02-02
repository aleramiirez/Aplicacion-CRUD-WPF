﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto2Evaluacion.Pages
{
    /// <summary>
    /// Lógica de interacción para UpdateProduct.xaml
    /// </summary>
    public partial class UpdatePage : Page
    {
        public UpdatePage()
        {
            InitializeComponent();
            Loaded += UpdateProduct_Loaded; 
        }

        // Este método se ejecuta cuando la página se carga. Su propósito es llenar los ComboBox productComboBox y
        // categoryComboBox con datos al iniciar la página
        private void UpdateProduct_Loaded(object sender, RoutedEventArgs e)
        {
            FillProductComboBox();

            FillCategoryComboBox();
        }

        // Este método llena el ComboBox productComboBox con nombres de productos
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

        // Similar a FillProductComboBox, este método llena el ComboBox categoryComboBox con nombres de categorías
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

        // Este método se ejecuta cuando cambia la selección en productComboBox. Si hay un producto seleccionado,
        // llama al método FillProductDetails para cargar los detalles del producto seleccionado en los controles de la página
        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (productComboBox.SelectedItem != null)
            {
                string selectedProduct = productComboBox.SelectedItem.ToString();
                FillProductDetails(selectedProduct);
            }
        }

        // Este método obtiene los detalles del producto seleccionado (selectedProduct) de la base de datos y los muestra en
        // los controles (productNameTextBox, unitPriceTextBox, y categoryComboBox) en la página
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
                            System.Data.DataTable dataTable = new System.Data.DataTable();
                            adapter.Fill(dataTable);

                            // Asignar los nuevos datos a los controles en la página
                            productNameTextBox.Text = dataTable.Rows[0]["ProductName"].ToString();
                            unitPriceTextBox.Text = dataTable.Rows[0]["UnitPrice"].ToString();

                            // Obtener el CategoryID del producto seleccionado
                            int categoryID = Convert.ToInt32(dataTable.Rows[0]["CategoryID"]);

                            // Seleccionar la categoría correspondiente en el ComboBox
                            SelectCategoryByID(categoryID);
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

        // Este método selecciona la categoría correspondiente en el categoryComboBox utilizando el CategoryID
        private void SelectCategoryByID(int categoryID)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT CategoryName FROM categories WHERE CategoryID = @categoryID";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@categoryID", categoryID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categoryName = reader["CategoryName"].ToString();
                                categoryComboBox.SelectedItem = categoryName;
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

        // Este método se ejecuta cuando el usuario hace clic en el botón de actualización. Verifica si hay un producto
        // seleccionado en productComboBox y, si es así, solicita confirmación al usuario antes de realizar la actualización
        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productComboBox.SelectedItem != null)
            {
                string selectedProduct = productComboBox.SelectedItem.ToString();

                MessageBoxResult result = MessageBox.Show($"¿Está seguro de que desea actualizar el producto '{selectedProduct}'?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(DataBase.DataBase.conexion.ConnectionString))
                        {
                            connection.Open();

                            string productName = productNameTextBox.Text;
                            string category = categoryComboBox.SelectedItem?.ToString();

                            // Obtén el CategoryID utilizando el método CategoryID
                            int categoryID = CategoryID(category);

                            decimal unitPrice = decimal.Parse(unitPriceTextBox.Text);

                            string updateQuery = "UPDATE products SET ProductName = @productName, CategoryID = @categoryID, UnitPrice = @unitPrice WHERE ProductName = @selectedProduct";
                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@productName", productName);
                                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                                cmd.Parameters.AddWithValue("@unitPrice", unitPrice);
                                cmd.Parameters.AddWithValue("@selectedProduct", selectedProduct);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show($"Producto '{selectedProduct}' actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                                    // Limpiar los TextBox después de actualizar el producto si lo deseas
                                    productNameTextBox.Text = "";
                                    unitPriceTextBox.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show($"No se pudo actualizar el producto '{selectedProduct}'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar el producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto antes de intentar actualizar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Este método obtiene el CategoryID de una categoría dada (categoryName) de la base de datos
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
    }
}
