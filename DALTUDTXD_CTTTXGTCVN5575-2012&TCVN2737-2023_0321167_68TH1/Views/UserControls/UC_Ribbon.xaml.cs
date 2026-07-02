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

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UC_Ribbon.xaml
    /// </summary>
    public partial class UC_Ribbon : UserControl
    {
        public Frame Mainframe { get; set; }

        public UC_Ribbon()
        {
            InitializeComponent();
        }

        private void rbt_KetNoiEtabs_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Tạo view KetNoiEtabsView trước khi gọi
            // KetNoiEtabsView ketNoiEtabsView = new KetNoiEtabsView();
            // ketNoiEtabsView.ShowDialog();
        }

        private void rbt_Khaibaotaitrong_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Tạo view taitrongview trước khi gọi
            // taitrongview MV = new taitrongview();
            // MV.Show();
        }

        private void rbt_TohopTaitrong_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Tạo view Tohoptaitrongview trước khi gọi
            // Tohoptaitrongview MV = new Tohoptaitrongview();
            // MV.ShowDialog();
        }

        private void Btn2D_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnMatcatxago_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnMoment_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBieudoluccat_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBieudodovong_Click(object sender, RoutedEventArgs e)
        {
        }

        private void rbt_huongdan_Click(object sender, RoutedEventArgs e)
        {
        }

        private void rbt_Lienhe_Click(object sender, RoutedEventArgs e)
        {
        }

        private void rbt_Baoloi_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
