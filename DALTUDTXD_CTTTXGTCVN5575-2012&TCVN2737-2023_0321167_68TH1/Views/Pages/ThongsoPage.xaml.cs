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



    }
}
