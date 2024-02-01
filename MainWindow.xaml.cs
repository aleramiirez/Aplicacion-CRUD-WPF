using Proyecto2Evaluacion.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto2Evaluacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Page1());
            
        }

        private void goHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Page1()); 
        }

        private void goCreate_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Page2()); 
        }

        private void goRead_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ReadPage());
        }

        private void goUpdate_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new UpdatePage());
        }
        private void goDelete_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new DeletePage());
        }

        private void goInfo_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new InfoPage());
        }
    }
}
