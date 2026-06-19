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

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Views.Pages
{
    /// <summary>
    /// Interaction logic for ChonhaPage.xaml
    /// </summary>
    public partial class ChonhaPage : Page
    {
        public static string LoaiMai = "";
        public ChonhaPage()
        {
            InitializeComponent();
        }
        private void btn_MaiBang_Click(object sender, RoutedEventArgs e)
        {
            LoaiMai = "Mái bằng";
            MoThongSo();
        }

        private void btn_MaiDoc1_Click(object sender, RoutedEventArgs e)
        {
            LoaiMai = "Mái dốc 1 mái";
            MoThongSo();
        }

        private void btn_MaiDoc2_Click(object sender, RoutedEventArgs e)
        {
            LoaiMai = "2 mái dốc";
            MoThongSo();
        }

        private void btn_MaiVom_Click(object sender, RoutedEventArgs e)
        {
            LoaiMai = "Mái vòm";
            MoThongSo();
        }
        private void BtnTiep_Click(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow as MainWindow;

            if (main != null)
            {
                main.MainFrame.Navigate(new ThongsoPage());
            }
        }

        private void MoThongSo()
        {
            var main = Application.Current.MainWindow as MainWindow;
            main.MainFrame.Navigate(new ThongsoPage());
        }
    }
}

   