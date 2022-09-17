using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(A.Text, out var a);
            double.TryParse(B.Text, out var b);
            double.TryParse(H.Text, out var h);
            double.TryParse(Mass.Text, out var mass);

            if (a == 0 || b == 0 || h == 0)
            {
                MessageBox.Show("Введены невалидные значения в полях A, B, H.");
                return;
            }

            if (mass == 0)
            {
                MessageBox.Show("Масса не может быть '0' или поле пустое.");
                return;
            }

            var volume = a * b * h;
            var answerText = volume / mass;
            Answer.Text = $"Плотность материала: {Math.Round(answerText, 6)}";
        }

        private void NumberTextValidator(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            Regex regex = new Regex("^[^0-9\\,]+$");
            if (
                (textBox.Text.Contains(',') && e.Text == ",") ||
                (textBox.Text == "" && e.Text == ",")
            )
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = regex.IsMatch(e.Text);
            }
        }
    }
}