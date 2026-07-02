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
    /// Interaction logic for Emailview.xaml
    /// </summary>
    public partial class Emailview : Window
    {
        public Emailview()
        {
            InitializeComponent();
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_From.Text) ||
                string.IsNullOrWhiteSpace(txt_Subject.Text) ||
                string.IsNullOrWhiteSpace(txt_Body.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin trước khi gửi phản hồi!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Simulate sending feedback email successfully
            MessageBox.Show($"Báo cáo phản hồi đã được gửi thành công đến Ban Dự Án!\nChúng tôi sẽ liên hệ lại với bạn qua email: {txt_From.Text} trong thời gian sớm nhất.", "Gửi thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
