﻿using MySql.Data.MySqlClient;
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

            Loaded += ReadPage_Loaded; 
            productComboBox.SelectionChanged += ProductComboBox_SelectionChanged;
            productComboBox2.SelectionChanged += ProductComboBox_SelectionChanged2;
        }

        // Este método se ejecuta cuando la página se carga. Su propósito es llenar los ComboBox productComboBox y productComboBox2
        // con datos al iniciar la página
        private void ReadPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Llena el ComboBox con nombres de productos
            FillProductComboBox();
            FillProductComboBox2();
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

        // Este método se ejecuta cuando cambia la selección en el productComboBox. Si hay un producto seleccionado,
        // llama al método FillProductDetails para cargar los detalles del producto seleccionado en el dataGridProductDetails
        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (productComboBox.SelectedItem != null)
            {
                string selectedProduct = productComboBox.SelectedItem.ToString();
                FillProductDetails(selectedProduct);
            }
        }

        // Este método obtiene los detalles del producto seleccionado (selectedProduct) de la base de datos y los muestra
        // en el dataGridProductDetails
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

        // Similar a FillProductComboBox, este método llena el ComboBox productComboBox2 con nombres de categorías
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

        // Este método se ejecuta cuando cambia la selección en productComboBox2. Si hay una categoría seleccionada, llama al
        // método FillProductDetails2 para cargar los detalles de los productos asociados a esa categoría en el dataGridProductDetails2
        private void ProductComboBox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            if (productComboBox2.SelectedItem != null)
            {
                string selectedProduct = productComboBox2.SelectedItem.ToString();
                FillProductDetails2(selectedProduct);
            }
        }

        // Similar a FillProductDetails, este método obtiene los detalles de los productos asociados a la categoría seleccionada
        // y los muestra en el dataGridProductDetails2
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
