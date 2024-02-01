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
using System.Windows.Shapes;

namespace Proyecto2Evaluacion
{

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el nombre de usuario y la contraseña ingresados
            string username = txtUser.Text.Trim();
            string password = txtPass.Password.Trim();

            // Verificar si el usuario y la contraseña son válidos
            if ((username.Equals("ale", StringComparison.OrdinalIgnoreCase) || username.Equals("dani", StringComparison.OrdinalIgnoreCase)) &&
                password.Equals("admin", StringComparison.Ordinal))
            {
                // Si son válidos, abrir la nueva ventana (MainWindows)
                MainWindow mainWindows = new MainWindow();
                mainWindows.Show();

                // Cerrar la ventana de inicio de sesión actual
                this.Close();
            }
            else
            {
                // Si no son válidos, mostrar un mensaje de error
                MessageBox.Show("Credenciales incorrectas. Por favor, inténtelo de nuevo.", "Error de inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
