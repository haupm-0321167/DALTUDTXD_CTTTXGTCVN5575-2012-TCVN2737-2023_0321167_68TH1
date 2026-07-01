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
using System.Windows.Shapes;

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Views.POPUP_PHAMHAU
{
    /// <summary>
    /// Interaction logic for taitrongview.xaml
    /// </summary>
    public partial class taitrongview : Window
    {


        private ObservableCollection<TaiTrongItem> dsTinhTai =
     new ObservableCollection<TaiTrongItem>();
        private ObservableCollection<TaiTrongItem> dsHoatTai =
     new ObservableCollection<TaiTrongItem>();

        private Dictionary<string, Diadiem> dsTinh =
           new Dictionary<string, Diadiem>();
        private double B_Nha = 0;
        private double H_Nha = 0;
        private int currentStep = 1;
        public taitrongview()
        {
            InitializeComponent();
            LoadJson();
            LoadTinh();
            LoadTinhTai();
            LoadHoatTai();

            // Pre-select house type based on user's initial selection in ChonnhaPage
            if (!string.IsNullOrEmpty(ChonnhaPage.LoaiMai))
            {
                foreach (ComboBoxItem item in cbb_loainha.Items)
                {
                    string content = item.Content.ToString();
                    if (content.Contains(ChonnhaPage.LoaiMai) ||
                        (ChonnhaPage.LoaiMai == "Mái dốc 1 mái" && content == "Nhà 1 mái dốc") ||
                        (ChonnhaPage.LoaiMai == "2 mái dốc" && content == "Nhà 2 mái dốc"))
                    {
                        cbb_loainha.SelectedItem = item;
                        break;
                    }
                }
            }
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
        private void LoadTinh()
        {
            cbb_tinh.Items.Clear();

            foreach (string tinh in dsTinh.Keys)
            {
                cbb_tinh.Items.Add(tinh);
            }
        }
        private void cbb_tinh_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            if (cbb_tinh.SelectedItem == null) return;

            string tinh = cbb_tinh.SelectedItem.ToString();

            cbb_huyen.Items.Clear();
            cbb_phuong.Items.Clear();



            foreach (string huyen in dsTinh[tinh].quan_huyen.Keys)
            {
                cbb_huyen.Items.Add(huyen);
            }
        }

        private void cbb_huyen_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            if (cbb_tinh.SelectedItem == null ||
         cbb_huyen.SelectedItem == null)
                return;

            string tinh = cbb_tinh.SelectedItem.ToString();

            string huyen = cbb_huyen.SelectedItem.ToString();

            cbb_phuong.Items.Clear();

            if (dsTinh[tinh].quan_huyen == null)
                return;

            foreach (string xa in
                dsTinh[tinh].quan_huyen[huyen])
            {
                cbb_phuong.Items.Add(xa);
            }



        }

        private void Cbb_phuong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txt_vunggio == null || txt_aplucgio == null || txt_diahinh == null)
                return;

            if (cbb_tinh.SelectedItem == null ||
       cbb_huyen.SelectedItem == null ||
       cbb_phuong.SelectedItem == null)
                return;

            string tinh =
                cbb_tinh.SelectedItem.ToString();

            Diadiem data = dsTinh[tinh];

            txt_vunggio.Text =
                data.vung_gio;

            txt_aplucgio.Text =
                data.ap_luc_gio.ToString();

            txt_diahinh.Text =
                data.dia_hinh;
        }
    }
}
