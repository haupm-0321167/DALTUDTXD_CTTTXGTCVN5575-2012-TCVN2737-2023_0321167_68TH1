using DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Data;
using DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for ThongsoPage.xaml
    /// </summary>
    public partial class ThongsoPage : Page
    {
        private Dictionary<string, Diadiem> dsTinh =
            new Dictionary<string, Diadiem>();
        private List<XaGo> dsXaGo =
            new List<XaGo>();
        private XaGo xaGoDangChon;
        ObservableCollection<XagoModels> danhSach = new ObservableCollection<XagoModels>();
        int id = 1;
        public ThongsoPage()
        {
            InitializeComponent();
            MessageBox.Show("Bạn chọn: " + ChonnhaPage.LoaiMai);

            LoadJson();

            dgCot.ItemsSource = danhSach;
        }
        private void LoadJson()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;

                string projectPath = Directory.GetParent(baseDir).Parent.Parent.FullName;

                string path = System.IO.Path.Combine(projectPath, "Data", "diadiem.json");

                string json = File.ReadAllText(path);

                dsTinh = JsonConvert.DeserializeObject
                    <Dictionary<string, Diadiem>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được file JSON!\n" + ex.Message);
            }
        }





        private void rbt_huybo_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }








        private void btn_them_click(object sender, RoutedEventArgs e)
        {

            try
            {
                var selectedLoai = (cbb_LoaiXG.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (string.IsNullOrEmpty(selectedLoai))
                {
                    MessageBox.Show("Chọn loại xà gồ!");
                    return;
                }

                XagoModels xg = new XagoModels()
                {
                    Id = id++,
                    Height = double.Parse(txt_H.Text),
                    Width = double.Parse(txt_B.Text),
                    Lip = double.Parse(txt_C.Text),
                    Thickness = double.Parse(txt_t.Text),
                    Length = double.Parse(txt_D.Text),
                    ExtraWidth = double.Parse(txt_B1.Text),

                    A = xaGoDangChon.S,
                    G = xaGoDangChon.P,
                    Ix = xaGoDangChon.Jx,
                    Iy = xaGoDangChon.Jy,
                    Wx = xaGoDangChon.Wx,
                    Wy = xaGoDangChon.Wy,

                    Loai = selectedLoai
                };

                danhSach.Add(xg);
                dgCot.SelectedItem = xg;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btn_xoa_click(object sender, RoutedEventArgs e)
        {
            if (dgCot.SelectedItem != null)
            {
                danhSach.Remove((XagoModels)dgCot.SelectedItem);
            }
            else
            {
                MessageBox.Show("Chọn dòng cần xóa!");
            }
        }

        private void cbb_LoaiXG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cbb_LoaiXG.SelectedItem as ComboBoxItem;
            if (item == null) return;

            string selected = item.Content.ToString();

            if (selected.Contains("XG C cán nóng"))
            {
                imgSoDo.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Assets/Images/C_can_nong.png"));
            }

            else if (selected.Contains("XG C dập nguội"))
            {
                imgSoDo.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Assets/Images/C_dap_nguoi.png"));
            }
            else if (selected.Contains("XG Z cán nóng"))
            {
                imgSoDo.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Assets/Images/Z_can_nong.png"));
            }
            else if (selected.Contains("XG thép hộp chữ nhật"))
            {
                imgSoDo.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Assets/Images/chunhat.png"));
            }
            else if (selected.Contains("XG thép hộp vuông"))
            {
                imgSoDo.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Assets/Images/vuong.png"));
            }
            if (cbb_LoaiXG.SelectedItem == null)
                return;

            string loai =
                ((ComboBoxItem)cbb_LoaiXG.SelectedItem)
                .Content.ToString();

            switch (loai)
            {
                case "XG C dập nguội":
                    LoadXaGo("xagocnguoi.json");
                    break;

                case "XG Z dập nguội":
                    LoadXaGo("xagozdn.json");
                    break;

                case "XG C cán nóng":
                    LoadXaGo("xagocnong.json");
                    break;

                case "XG Z cán nóng":
                    LoadXaGo("xagozn.json");
                    break;

                case "XG thép hộp chữ nhật":
                    LoadXaGo("xagohop.json");
                    break;

                case "XG thép hộp vuông":
                    LoadXaGo("xagov.json");
                    break;

            }
        }


        private void LoadXaGo(string fileName)
        {
            try
            {
                string baseDir =
                    AppDomain.CurrentDomain.BaseDirectory;

                string projectPath =
                    Directory.GetParent(baseDir)
                    .Parent
                    .Parent
                    .FullName;

                string path =
                    System.IO.Path.Combine(
                        projectPath,
                        "Data",
                        fileName);

                string json =
                    File.ReadAllText(path);

                dsXaGo =
                    JsonConvert.DeserializeObject<List<XaGo>>(json);

                cbb_SoHieu.Items.Clear();

                foreach (var xg in dsXaGo)
                {
                    cbb_SoHieu.Items.Add(xg.SoHieu);
                }

                cbb_SoHieu.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbb_SoHieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_SoHieu.SelectedItem == null)
                return;

            string soHieu =
                cbb_SoHieu.SelectedItem.ToString();

            xaGoDangChon =
    dsXaGo.FirstOrDefault(
        x => x.SoHieu == soHieu);

            if (xaGoDangChon == null)
                return;

            txt_H.Text = xaGoDangChon.H.ToString();

            txt_B.Text = xaGoDangChon.B.ToString();

            txt_C.Text = xaGoDangChon.C.ToString();

            txt_t.Text = xaGoDangChon.t.ToString();

            txt_B1.Text = xaGoDangChon.B1.ToString();

            txt_D.Text = xaGoDangChon.D.ToString();
        }
        private void btn_KiemTra_Click(object sender, RoutedEventArgs e)
        {
            if (dgCot.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng trong bảng!");
                return;
            }

            var data = dgCot.SelectedItem as XagoModels;



            if (data == null)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
                return;
            }
            GlobalData.B1 = data.ExtraWidth;
            GlobalData.A = data.A;
            GlobalData.G = data.G;

            GlobalData.Jx = data.Ix;
            GlobalData.Jy = data.Iy;

            GlobalData.Wx = data.Wx;
            GlobalData.Wy = data.Wy;
            var main = Application.Current.MainWindow as MainWindow;

            if (main != null)
            {
                main.MainFrame.Navigate(new HomePage(data));
            }

        }

    }
}
