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
    /// Interaction logic for Errorview.xaml
    /// </summary>
    public partial class Errorview : Window
    {
        public string ErrorMessage { get; set; }
        public string UserDescription { get; set; }

        public Errorview(string error)
        {
            InitializeComponent();
            ErrorMessage = error;
            DataContext = this;
        }


        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string category = (cbbCategory.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Khác";
                string email = txtContactEmail.Text;
                string desc = txtUserDescription.Text;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(desc))
                {
                    MessageBox.Show("Vui lòng nhập Email liên hệ và Mô tả chi tiết lỗi!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string log = $"========================================\n" +
                             $"Time: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                             $"Category: {category}\n" +
                             $"Contact Email: {email}\n" +
                             $"System Error: {ErrorMessage}\n" +
                             $"User Note: {desc}\n" +
                             $"========================================\n\n";

                System.IO.File.AppendAllText("error_log.txt", log);

                MessageBox.Show("Báo cáo lỗi của bạn đã được ghi lại thành công!\nĐội ngũ kỹ thuật sẽ sớm phản hồi qua email của bạn.", "Báo cáo lỗi thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu báo cáo lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
