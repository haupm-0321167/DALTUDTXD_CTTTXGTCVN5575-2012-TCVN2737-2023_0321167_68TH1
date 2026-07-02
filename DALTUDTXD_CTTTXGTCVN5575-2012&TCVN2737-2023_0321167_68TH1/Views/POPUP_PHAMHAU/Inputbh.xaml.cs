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

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Views.POPUP_PHAMHAU
{
    /// <summary>
    /// Interaction logic for Inputbh.xaml
    /// </summary>
    public partial class Inputbh : Window
    {
        public double B { get; private set; }
        public double H { get; private set; }
        public Inputbh()
        {
            InitializeComponent();
        }
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txt_b.Text, out double b) &&
                double.TryParse(txt_h.Text, out double h))
            {
                B = b;
                H = h;

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Nhập đúng giá trị b và h!");
            }
        }
    }
}
