using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ProgLabKursII
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FirstPageClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Page1());
        }

        private void SecondPageClick(object sender, RoutedEventArgs e)
        {
            Main.Navigate(new Page2());
        }
    }
}
