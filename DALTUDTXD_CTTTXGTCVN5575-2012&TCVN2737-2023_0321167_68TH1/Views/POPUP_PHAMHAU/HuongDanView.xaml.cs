using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for HuongDanView.xaml
    /// </summary>
    public partial class HuongDanView : Window
    {
        public HuongDanView()
        {
            InitializeComponent();
        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                string videoUrl = "https://youtu.be/4xq4W3UAk-k";
                Process.Start(new ProcessStartInfo(videoUrl) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở liên kết video: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnThuyetMinh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                string docUrl = "https://docs.google.com/document/d/1e2xVSWa_rGl5IovUc6HSFZTQoN4NRy_CqaZt3y6ROso/edit?copiedFromTrash&tab=t.0\n";
                Process.Start(new ProcessStartInfo(docUrl) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở thuyết minh: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnHDSD_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                string pdfUrl = "https://docs.google.com/document/d/1X5X5y7z9Lz97_B703_96TH1_huong_dan_su_dung/edit?usp=sharing";
                Process.Start(new ProcessStartInfo(pdfUrl) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở tài liệu hướng dẫn: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
