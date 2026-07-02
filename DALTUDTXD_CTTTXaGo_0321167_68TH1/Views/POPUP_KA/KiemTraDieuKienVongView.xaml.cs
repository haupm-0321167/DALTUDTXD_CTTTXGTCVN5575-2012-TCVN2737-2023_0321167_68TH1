using DALTUDTXD_CTTTXaGo_0321167_68TH1.Data;
using DALTUDTXD_CTTTXaGo_0321167_68TH1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing;

namespace DALTUDTXD_CTTTXaGo_0321167_68TH1.Views.POPUP_KA
{
    /// <summary>
    /// Interaction logic for KiemTraDieuKienVongView.xaml
    /// </summary>
    public partial class KiemTraDieuKienVongView : Window
    {
        private ObservableCollection<KTDoVong> dsDoVong =
        new ObservableCollection<KTDoVong>();
        private ObservableCollection<KTDoVong> dsKiemTraDoVong =
        new ObservableCollection<KTDoVong>();
        private ObservableCollection<KiemTraVong> dsKiemTra =
         new ObservableCollection<KiemTraVong>();
        public KiemTraDieuKienVongView()
        {
            InitializeComponent();
            double Ptcx1 = GlobalData.Ptcx_CVV1;
            double Ptcy1 = GlobalData.Ptcy_CVV1;
            double Ptcx2 = GlobalData.Ptcx_CVV2;
            double Ptcy2 = GlobalData.Ptcy_CVV2;
            double Ix = GlobalData.Jx;
            double L = GlobalData.NhipXaGo;
            double B1 = GlobalData.B1;

            TaoDuLieuMacDinh();

            dgDoVong.ItemsSource = dsDoVong;
            dgKiemTraDoVong.ItemsSource = dsKiemTraDoVong;

            if (B1 >= 0 && B1 <= 3)
            {
                cbb_DiemGiang.SelectedIndex = (int)B1;
            }
            else
            {
                cbb_DiemGiang.SelectedIndex = 0;
            }
        }

        private void TaoDuLieuMacDinh()
        {

            double Ptcx1 = GlobalData.Ptcx_CVV1;
            double Ptcy1 = GlobalData.Ptcy_CVV1;
            double Ptcx2 = GlobalData.Ptcx_CVV2;
            double Ptcy2 = GlobalData.Ptcy_CVV2;
            double Ix = GlobalData.Jx;
            double Iy = GlobalData.Jy;
            double L = GlobalData.NhipXaGo;
            double B1 = GlobalData.B1;
            double E = 2100000; // daN/cm²

            double fx_0_cvv1 = 0;
            double fx_0_cvv2 = 0;
            double fy_0_cvv1 = 0;
            double fy_0_cvv2 = 0;
            double fv_0_cvv1 = 0;
            double fv_0_cvv2 = 0;

            double fx_1_05_cvv1 = 0;
            double fx_1_05_cvv2 = 0;
            double fy_1_05_cvv1 = 0;
            double fy_1_05_cvv2 = 0;
            double fv_1_05_cvv1 = 0;
            double fv_1_05_cvv2 = 0;

            double fx_1_021_cvv1 = 0;
            double fx_1_021_cvv2 = 0;
            double fy_1_021_cvv1 = 0;
            double fy_1_021_cvv2 = 0;
            double fv_1_021_cvv1 = 0;
            double fv_1_021_cvv2 = 0;

            double fx_2_05_cvv1 = 0;
            double fx_2_05_cvv2 = 0;
            double fy_2_05_cvv1 = 0;
            double fy_2_05_cvv2 = 0;
            double fv_2_05_cvv1 = 0;
            double fv_2_05_cvv2 = 0;

            double fx_2_0149_cvv1 = 0;
            double fx_2_0149_cvv2 = 0;
            double fy_2_0149_cvv1 = 0;
            double fy_2_0149_cvv2 = 0;
            double fv_2_0149_cvv1 = 0;
            double fv_2_0149_cvv2 = 0;

            double fx_3_05_cvv1 = 0;
            double fx_3_05_cvv2 = 0;
            double fy_3_05_cvv1 = 0;
            double fy_3_05_cvv2 = 0;
            double fv_3_05_cvv1 = 0;
            double fv_3_05_cvv2 = 0;

            double fx_3_011_cvv1 = 0;
            double fx_3_011_cvv2 = 0;
            double fy_3_011_cvv1 = 0;
            double fy_3_011_cvv2 = 0;
            double fv_3_011_cvv1 = 0;
            double fv_3_011_cvv2 = 0;

            if (B1 == 0)
            {
                fx_0_cvv1 = (5 * Ptcx1 * L * L * L * L) * 1000 / (384 * E * Iy * 1 * 0.0001);
                fx_0_cvv2 = (5 * Ptcx2 * L * L * L * L) * 1000 / (384 * E * Iy * 1 * 0.0001);
                fy_0_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_0_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fv_0_cvv1 = Math.Sqrt(fx_0_cvv1 * fx_0_cvv1 + fy_0_cvv1 * fy_0_cvv1);
                fv_0_cvv2 = Math.Sqrt(fx_0_cvv2 * fx_0_cvv2 + fy_0_cvv2 * fy_0_cvv2);

                fx_1_05_cvv1 = (0 * Ptcx1 * L * L * L * L) * 1000 / (E * Iy * 1 * 0.0001);
                fx_1_05_cvv2 = (0 * Ptcx2 * L * L * L * L) * 1000 / (E * Iy * 1 * 0.0001);
                fy_1_05_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_1_05_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fv_1_05_cvv1 = Math.Sqrt(fx_1_05_cvv1 * fx_1_05_cvv1 + fy_1_05_cvv1 * fy_1_05_cvv1);
                fv_1_05_cvv2 = Math.Sqrt(fx_1_05_cvv2 * fx_1_05_cvv2 + fy_1_05_cvv2 * fy_1_05_cvv2);

                fx_1_021_cvv1 = (1 * Ptcx1 * L * L * L * L) * 1000 / (2954 * E * Iy * 1 * 0.0001);
                fx_1_021_cvv2 = (1 * Ptcx2 * L * L * L * L) * 1000 / (2954 * E * Iy * 1 * 0.0001);
                fy_1_021_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_1_021_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fv_1_021_cvv1 = Math.Sqrt(fx_1_021_cvv1 * fx_1_021_cvv1 + fy_1_021_cvv1 * fy_1_021_cvv1);
                fv_1_021_cvv2 = Math.Sqrt(fx_1_021_cvv2 * fx_1_021_cvv2 + fy_1_021_cvv2 * fy_1_021_cvv2);

                fx_2_05_cvv1 = (1.136 * Ptcx1 * L * L * L * L) * 1000 / (27000 * E * Iy * 1 * 0.0001);
                fx_2_05_cvv2 = (1.136 * Ptcx2 * L * L * L * L) * 1000 / (27000 * E * Iy * 1 * 0.0001);
                fy_2_05_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_2_05_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fv_2_05_cvv1 = Math.Sqrt(fx_2_05_cvv1 * fx_2_05_cvv1 + fy_2_05_cvv1 * fy_2_05_cvv1);
                fv_2_05_cvv2 = Math.Sqrt(fx_2_05_cvv2 * fx_2_05_cvv2 + fy_2_05_cvv2 * fy_2_05_cvv2);

                fx_2_0149_cvv1 = (2.3 * Ptcx1 * L * L * L * L) * 1000 / (27000 * E * Iy * 1 * 0.0001);
                fx_2_0149_cvv2 = (2.3 * Ptcx2 * L * L * L * L) * 1000 / (27000 * E * Iy * 1 * 0.0001);
                fy_2_0149_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_2_0149_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fv_2_0149_cvv1 = Math.Sqrt(fx_2_0149_cvv1 * fx_2_0149_cvv1 + fy_2_0149_cvv1 * fy_2_0149_cvv1);
                fv_2_0149_cvv2 = Math.Sqrt(fx_2_0149_cvv2 * fx_2_0149_cvv2 + fy_2_0149_cvv2 * fy_2_0149_cvv2);

                fx_3_05_cvv1 = (0 * Ptcx1 * L * L * L * L) * 1000 / (E * Iy * 1 * 0.0001);
                fx_3_05_cvv2 = (0 * Ptcx2 * L * L * L * L) * 1000 / (E * Iy * 1 * 0.0001);
                fy_3_05_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_3_05_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fv_3_05_cvv1 = Math.Sqrt(fx_3_05_cvv1 * fx_3_05_cvv1 + fy_3_05_cvv1 * fy_3_05_cvv1);
                fv_3_05_cvv2 = Math.Sqrt(fx_3_05_cvv2 * fx_3_05_cvv2 + fy_3_05_cvv2 * fy_3_05_cvv2);

                fx_3_011_cvv1 = (6.5 * Ptcx1 * L * L * L * L) * 1000 / (25600 * E * Iy * 1 * 0.0001);
                fx_3_011_cvv2 = (6.5 * Ptcx2 * L * L * L * L) * 1000 / (25600 * E * Iy * 1 * 0.0001);
                fy_3_011_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_3_011_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fv_3_011_cvv1 = Math.Sqrt(fx_3_011_cvv1 * fx_3_011_cvv1 + fy_3_011_cvv1 * fy_3_011_cvv1);
                fv_3_011_cvv2 = Math.Sqrt(fx_3_011_cvv2 * fx_3_011_cvv2 + fy_3_011_cvv2 * fy_3_011_cvv2);

            }
            else
            {
                fx_0_cvv1 = (5 * Ptcx1 * L * L * L * L) * 1000 / (384 * E * Iy * 2 * 0.0001);
                fx_0_cvv2 = (5 * Ptcx2 * L * L * L * L) * 1000 / (384 * E * Iy * 2 * 0.0001);
                fy_0_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_0_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fv_0_cvv1 = Math.Sqrt(fx_0_cvv1 * fx_0_cvv1 + fy_0_cvv1 * fy_0_cvv1);
                fv_0_cvv2 = Math.Sqrt(fx_0_cvv2 * fx_0_cvv2 + fy_0_cvv2 * fy_0_cvv2);
                fx_1_05_cvv1 = (5 * Ptcx1 * L * L * L * L) * 1000 / (384 * E * Iy * 2 * 0.0001);
                fx_1_05_cvv2 = (5 * Ptcx2 * L * L * L * L) * 1000 / (384 * E * Iy * 2 * 0.0001);
                fy_1_05_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_1_05_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fv_1_05_cvv1 = Math.Sqrt(fx_1_05_cvv1 * fx_1_05_cvv1 + fy_1_05_cvv1 * fy_1_05_cvv1);
                fv_1_05_cvv2 = Math.Sqrt(fx_1_05_cvv2 * fx_1_05_cvv2 + fy_1_05_cvv2 * fy_1_05_cvv2);

                fx_1_021_cvv1 = (1 * Ptcx1 * L * L * L * L) * 1000 / (2954 * E * Iy * 2 * 0.0001);
                fx_1_021_cvv2 = (1 * Ptcx2 * L * L * L * L) * 1000 / (2954 * E * Iy * 2 * 0.0001);
                fy_1_021_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_1_021_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fv_1_021_cvv1 = Math.Sqrt(fx_1_021_cvv1 * fx_1_021_cvv1 + fy_1_021_cvv1 * fy_1_021_cvv1);
                fv_1_021_cvv2 = Math.Sqrt(fx_1_021_cvv2 * fx_1_021_cvv2 + fy_1_021_cvv2 * fy_1_021_cvv2);

                fx_2_05_cvv1 = (1.136 * Ptcx1 * L * L * L * L) * 1000 / (27000 * E * Iy * 2 * 0.0001);
                fx_2_05_cvv2 = (1.136 * Ptcx2 * L * L * L * L) * 1000 / (27000 * E * Iy * 2 * 0.0001);
                fy_2_05_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_2_05_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fv_2_05_cvv1 = Math.Sqrt(fx_2_05_cvv1 * fx_2_05_cvv1 + fy_2_05_cvv1 * fy_2_05_cvv1);
                fv_2_05_cvv2 = Math.Sqrt(fx_2_05_cvv2 * fx_2_05_cvv2 + fy_2_05_cvv2 * fy_2_05_cvv2);

                fx_2_0149_cvv1 = (2.3 * Ptcx1 * L * L * L * L) * 1000 / (27000 * E * Iy * 2 * 0.0001);
                fx_2_0149_cvv2 = (2.3 * Ptcx2 * L * L * L * L) * 1000 / (27000 * E * Iy * 2 * 0.0001);
                fy_2_0149_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_2_0149_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fv_2_0149_cvv1 = Math.Sqrt(fx_2_0149_cvv1 * fx_2_0149_cvv1 + fy_2_0149_cvv1 * fy_2_0149_cvv1);
                fv_2_0149_cvv2 = Math.Sqrt(fx_2_0149_cvv2 * fx_2_0149_cvv2 + fy_2_0149_cvv2 * fy_2_0149_cvv2);

                fx_3_05_cvv1 = (0 * Ptcx1 * L * L * L * L) * 1000 / (E * Iy * 2 * 0.0001);
                fx_3_05_cvv2 = (0 * Ptcx2 * L * L * L * L) * 1000 / (E * Iy * 2 * 0.0001);
                fy_3_05_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_3_05_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fv_3_05_cvv1 = Math.Sqrt(fx_3_05_cvv1 * fx_3_05_cvv1 + fy_3_05_cvv1 * fy_3_05_cvv1);
                fv_3_05_cvv2 = Math.Sqrt(fx_3_05_cvv2 * fx_3_05_cvv2 + fy_3_05_cvv2 * fy_3_05_cvv2);

                fx_3_011_cvv1 = (6.5 * Ptcx1 * L * L * L * L) * 1000 / (25600 * E * Iy * 2 * 0.0001);
                fx_3_011_cvv2 = (6.5 * Ptcx2 * L * L * L * L) * 1000 / (25600 * E * Iy * 2 * 0.0001);
                fy_3_011_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_3_011_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fv_3_011_cvv1 = Math.Sqrt(fx_3_011_cvv1 * fx_3_011_cvv1 + fy_3_011_cvv1 * fy_3_011_cvv1);
                fv_3_011_cvv2 = Math.Sqrt(fx_3_011_cvv2 * fx_3_011_cvv2 + fy_3_011_cvv2 * fy_3_011_cvv2);
            }

            dsDoVong.Add(new KTDoVong()
            {

                ToHop = "CVV1",
                ViTri = "0.5L",
                SoDiemGiang = 0,
                Fx = Math.Round(fx_0_cvv1, 1),
                Fy = Math.Round(fy_0_cvv1, 1),
                Fv = Math.Round(fv_0_cvv1, 1)

            }
            );
            dsDoVong.Add(new KTDoVong()
            {
                ToHop = "CVV2",
                ViTri = "0.5L",
                SoDiemGiang = 0,
                Fx = Math.Round(fx_0_cvv2, 1),
                Fy = Math.Round(fy_0_cvv2, 1),
                Fv = Math.Round(fv_0_cvv2, 1)
            });



            dsDoVong.Add(new KTDoVong()
            {

                ToHop = "CVV1",
                ViTri = "0.5L",
                SoDiemGiang = 1,
                Fx = Math.Round(fx_1_05_cvv1, 1),
                Fy = Math.Round(fy_1_05_cvv1, 1),
                Fv = Math.Round(fv_1_05_cvv1, 1)



            }
           );
            dsDoVong.Add(new KTDoVong()
            {
                ToHop = "CVV2",
                ViTri = "0.5L",
                SoDiemGiang = 1,
                Fx = Math.Round(fx_1_05_cvv2, 1),
                Fy = Math.Round(fy_1_05_cvv2, 1),
                Fv = Math.Round(fv_1_05_cvv2, 1)
            });


            dsDoVong.Add(new KTDoVong()
            {

                ToHop = "CVV1",
                ViTri = "0.21L",
                SoDiemGiang = 1,
                Fx = Math.Round(fx_1_021_cvv1, 1),
                Fy = Math.Round(fy_1_021_cvv1, 1),
                Fv = Math.Round(fv_1_021_cvv1, 1)


            }
          );
            dsDoVong.Add(new KTDoVong()
            {
                ToHop = "CVV2",
                ViTri = "0.21L",
                SoDiemGiang = 1,
                Fx = Math.Round(fx_1_021_cvv2, 1),
                Fy = Math.Round(fy_1_021_cvv2, 1),
                Fv = Math.Round(fv_1_021_cvv2, 1)
            });



            dsDoVong.Add(new KTDoVong()
            {

                ToHop = "CVV1",
                ViTri = "0.5L",
                SoDiemGiang = 2,
                Fx = Math.Round(fx_2_05_cvv1, 1),
                Fy = Math.Round(fy_2_05_cvv1, 1),
                Fv = Math.Round(fv_2_05_cvv1, 1)

            }
          );
            dsDoVong.Add(new KTDoVong()
            {
                ToHop = "CVV2",
                ViTri = "0.5L",
                SoDiemGiang = 2,
                Fx = Math.Round(fx_2_05_cvv2, 1),
                Fy = Math.Round(fy_2_05_cvv2, 1),
                Fv = Math.Round(fv_2_05_cvv2, 1)
            });

            dsDoVong.Add(new KTDoVong()
            {

                ToHop = "CVV1",
                ViTri = "0.149L",
                SoDiemGiang = 2,
                Fx = Math.Round(fx_2_0149_cvv1, 1),
                Fy = Math.Round(fy_2_0149_cvv1, 1),
                Fv = Math.Round(fv_2_0149_cvv1, 1)

            }
          );
            dsDoVong.Add(new KTDoVong()
            {
                ToHop = "CVV2",
                ViTri = "0.149L",
                SoDiemGiang = 2,
                Fx = Math.Round(fx_2_0149_cvv2, 1),
                Fy = Math.Round(fy_2_0149_cvv2, 1),
                Fv = Math.Round(fv_2_0149_cvv2, 1)
            });

            dsDoVong.Add(new KTDoVong()
            {

                ToHop = "CVV1",
                ViTri = "0.5L",
                SoDiemGiang = 3,
                Fx = Math.Round(fx_3_05_cvv1, 1),
                Fy = Math.Round(fy_3_05_cvv1, 1),
                Fv = Math.Round(fv_3_05_cvv1, 1)

            }
          );
            dsDoVong.Add(new KTDoVong()
            {
                ToHop = "CVV2",
                ViTri = "0.5L",
                SoDiemGiang = 3,
                Fx = Math.Round(fx_3_05_cvv2, 1),
                Fy = Math.Round(fy_3_05_cvv2, 1),
                Fv = Math.Round(fv_3_05_cvv2, 1)
            });

            dsDoVong.Add(new KTDoVong()
            {

                ToHop = "CVV1",
                ViTri = "0.11L",
                SoDiemGiang = 3,
                Fx = Math.Round(fx_3_011_cvv1, 1),
                Fy = Math.Round(fy_3_011_cvv1, 1),
                Fv = Math.Round(fv_3_011_cvv1, 1)

            }
          );
            dsDoVong.Add(new KTDoVong()
            {
                ToHop = "CVV2",
                ViTri = "0.11L",
                SoDiemGiang = 3,
                Fx = Math.Round(fx_3_011_cvv2, 1),
                Fy = Math.Round(fy_3_011_cvv2, 1),
                Fv = Math.Round(fv_3_011_cvv2, 1)
            });

        }

        private void cbb_DiemGiang_SelectionChanged(
      object sender,
      SelectionChangedEventArgs e)
        {
            if (cbb_DiemGiang.SelectedItem == null)
                return;

            int soDiemGiang = Convert.ToInt32(
                ((ComboBoxItem)cbb_DiemGiang.SelectedItem).Content);

            var dsLoc = dsDoVong
                .Where(x => x.SoDiemGiang == soDiemGiang && x.Fv.HasValue)
                .ToList();


            if (!dsLoc.Any())
                return;

            double fvMax = dsLoc.Max(x => x.Fv.Value);

            double DoVongChoPhep = GlobalData.NhipXaGo * 1000 / 200;

            dsKiemTraDoVong.Clear();
            dsKiemTraDoVong.Add(new KTDoVong()
            {
                DoVongXG = Math.Round(fvMax, 1),
                SoSanh = "<=",
                DoVongChoPhep = Math.Round(DoVongChoPhep, 1),
                NhanXet1 = fvMax <= DoVongChoPhep
                                ? "Thỏa mãn"
                                : "Không thỏa mãn"
            });


            dsKiemTra.Clear();
            var groups = dsLoc.GroupBy(x => x.ToHop);
            foreach (var g in groups)
            {
                double fvMaxGroup = g.Max(x => x.Fv.Value);
                dsKiemTra.Add(new KiemTraVong()
                {
                    ToHop = g.Key,
                    DoVong = Math.Round(fvMaxGroup, 1),
                    DauSoSanh = "<=",
                    DoVongChoPhep = Math.Round(DoVongChoPhep, 1),
                    NhanXet = fvMaxGroup <= DoVongChoPhep ? "Thỏa mãn" : "Không thỏa mãn"
                });
            }
        }
        public static ObservableCollection<KiemTraVong> TinhToanKiemTraVong(int soDiemGiang)
        {
            double Ptcx1 = GlobalData.Ptcx_CVV1;
            double Ptcy1 = GlobalData.Ptcy_CVV1;
            double Ptcx2 = GlobalData.Ptcx_CVV2;
            double Ptcy2 = GlobalData.Ptcy_CVV2;
            double Ix = GlobalData.Jx;
            double Iy = GlobalData.Jy;
            double L = GlobalData.NhipXaGo;
            double B1 = GlobalData.B1;
            double E = 2100000; // daN/cm²

            double fx_0_cvv1 = 0, fx_0_cvv2 = 0, fy_0_cvv1 = 0, fy_0_cvv2 = 0;
            double fx_1_05_cvv1 = 0, fx_1_05_cvv2 = 0, fy_1_05_cvv1 = 0, fy_1_05_cvv2 = 0;
            double fx_1_021_cvv1 = 0, fx_1_021_cvv2 = 0, fy_1_021_cvv1 = 0, fy_1_021_cvv2 = 0;
            double fx_2_05_cvv1 = 0, fx_2_05_cvv2 = 0, fy_2_05_cvv1 = 0, fy_2_05_cvv2 = 0;
            double fx_2_0149_cvv1 = 0, fx_2_0149_cvv2 = 0, fy_2_0149_cvv1 = 0, fy_2_0149_cvv2 = 0;
            double fx_3_05_cvv1 = 0, fx_3_05_cvv2 = 0, fy_3_05_cvv1 = 0, fy_3_05_cvv2 = 0;
            double fx_3_011_cvv1 = 0, fx_3_011_cvv2 = 0, fy_3_011_cvv1 = 0, fy_3_011_cvv2 = 0;

            if (Iy <= 0) Iy = 100.0;
            if (Ix <= 0) Ix = 100.0;
            if (L <= 0) L = 6.0;

            if (B1 == 0)
            {
                fx_0_cvv1 = (5 * Ptcx1 * L * L * L * L) * 1000 / (384 * E * Iy * 1 * 0.0001);
                fx_0_cvv2 = (5 * Ptcx2 * L * L * L * L) * 1000 / (384 * E * Iy * 1 * 0.0001);
                fy_0_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_0_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);

                fx_1_05_cvv1 = 0;
                fx_1_05_cvv2 = 0;
                fy_1_05_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_1_05_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);

                fx_1_021_cvv1 = (1 * Ptcx1 * L * L * L * L) * 1000 / (2954 * E * Iy * 1 * 0.0001);
                fx_1_021_cvv2 = (1 * Ptcx2 * L * L * L * L) * 1000 / (2954 * E * Iy * 1 * 0.0001);
                fy_1_021_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_1_021_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);

                fx_2_05_cvv1 = (1.136 * Ptcx1 * L * L * L * L) * 1000 / (27000 * E * Iy * 1 * 0.0001);
                fx_2_05_cvv2 = (1.136 * Ptcx2 * L * L * L * L) * 1000 / (27000 * E * Iy * 1 * 0.0001);
                fy_2_05_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_2_05_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);

                fx_2_0149_cvv1 = (2.3 * Ptcx1 * L * L * L * L) * 1000 / (27000 * E * Iy * 1 * 0.0001);
                fx_2_0149_cvv2 = (2.3 * Ptcx2 * L * L * L * L) * 1000 / (27000 * E * Iy * 1 * 0.0001);
                fy_2_0149_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_2_0149_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);

                fx_3_05_cvv1 = 0;
                fx_3_05_cvv2 = 0;
                fy_3_05_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_3_05_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);

                fx_3_011_cvv1 = (6.5 * Ptcx1 * L * L * L * L) * 1000 / (25600 * E * Iy * 1 * 0.0001);
                fx_3_011_cvv2 = (6.5 * Ptcx2 * L * L * L * L) * 1000 / (25600 * E * Iy * 1 * 0.0001);
                fy_3_011_cvv1 = (5 * Ptcy1 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
                fy_3_011_cvv2 = (5 * Ptcy2 * L * L * L * L) * 1000 / (384 * E * Ix * 1 * 0.0001);
            }
            else
            {
                fx_0_cvv1 = (5 * Ptcx1 * L * L * L * L) * 1000 / (384 * E * Iy * 2 * 0.0001);
                fx_0_cvv2 = (5 * Ptcx2 * L * L * L * L) * 1000 / (384 * E * Iy * 2 * 0.0001);
                fy_0_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_0_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);

                fx_1_05_cvv1 = (5 * Ptcx1 * L * L * L * L) * 1000 / (384 * E * Iy * 2 * 0.0001);
                fx_1_05_cvv2 = (5 * Ptcx2 * L * L * L * L) * 1000 / (384 * E * Iy * 2 * 0.0001);
                fy_1_05_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_1_05_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);

                fx_1_021_cvv1 = (1 * Ptcx1 * L * L * L * L) * 1000 / (2954 * E * Iy * 2 * 0.0001);
                fx_1_021_cvv2 = (1 * Ptcx2 * L * L * L * L) * 1000 / (2954 * E * Iy * 2 * 0.0001);
                fy_1_021_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_1_021_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);

                fx_2_05_cvv1 = (1.136 * Ptcx1 * L * L * L * L) * 1000 / (27000 * E * Iy * 2 * 0.0001);
                fx_2_05_cvv2 = (1.136 * Ptcx2 * L * L * L * L) * 1000 / (27000 * E * Iy * 2 * 0.0001);
                fy_2_05_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_2_05_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);

                fx_2_0149_cvv1 = (2.3 * Ptcx1 * L * L * L * L) * 1000 / (27000 * E * Iy * 2 * 0.0001);
                fx_2_0149_cvv2 = (2.3 * Ptcx2 * L * L * L * L) * 1000 / (27000 * E * Iy * 2 * 0.0001);
                fy_2_0149_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_2_0149_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);

                fx_3_05_cvv1 = 0;
                fx_3_05_cvv2 = 0;
                fy_3_05_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_3_05_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);

                fx_3_011_cvv1 = (6.5 * Ptcx1 * L * L * L * L) * 1000 / (25600 * E * Iy * 2 * 0.0001);
                fx_3_011_cvv2 = (6.5 * Ptcx2 * L * L * L * L) * 1000 / (25600 * E * Iy * 2 * 0.0001);
                fy_3_011_cvv1 = (0.00707 * Ptcy1 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
                fy_3_011_cvv2 = (0.00707 * Ptcy2 * L * L * L * L) * 1000 / (E * Ix * 1 * 0.0001);
            }

            var localList = new List<KTDoVong>();
            if (soDiemGiang == 0)
            {
                localList.Add(new KTDoVong { ToHop = "CVV1", Fv = Math.Sqrt(fx_0_cvv1 * fx_0_cvv1 + fy_0_cvv1 * fy_0_cvv1) });
                localList.Add(new KTDoVong { ToHop = "CVV2", Fv = Math.Sqrt(fx_0_cvv2 * fx_0_cvv2 + fy_0_cvv2 * fy_0_cvv2) });
            }
            else if (soDiemGiang == 1)
            {
                localList.Add(new KTDoVong { ToHop = "CVV1", Fv = Math.Max(Math.Sqrt(fx_1_05_cvv1 * fx_1_05_cvv1 + fy_1_05_cvv1 * fy_1_05_cvv1), Math.Sqrt(fx_1_021_cvv1 * fx_1_021_cvv1 + fy_1_021_cvv1 * fy_1_021_cvv1)) });
                localList.Add(new KTDoVong { ToHop = "CVV2", Fv = Math.Max(Math.Sqrt(fx_1_05_cvv2 * fx_1_05_cvv2 + fy_1_05_cvv2 * fy_1_05_cvv2), Math.Sqrt(fx_1_021_cvv2 * fx_1_021_cvv2 + fy_1_021_cvv2 * fy_1_021_cvv2)) });
            }
            else if (soDiemGiang == 2)
            {
                localList.Add(new KTDoVong { ToHop = "CVV1", Fv = Math.Max(Math.Sqrt(fx_2_05_cvv1 * fx_2_05_cvv1 + fy_2_05_cvv1 * fy_2_05_cvv1), Math.Sqrt(fx_2_0149_cvv1 * fx_2_0149_cvv1 + fy_2_0149_cvv1 * fy_2_0149_cvv1)) });
                localList.Add(new KTDoVong { ToHop = "CVV2", Fv = Math.Max(Math.Sqrt(fx_2_05_cvv2 * fx_2_05_cvv2 + fy_2_05_cvv2 * fy_2_05_cvv2), Math.Sqrt(fx_2_0149_cvv2 * fx_2_0149_cvv2 + fy_2_0149_cvv2 * fy_2_0149_cvv2)) });
            }
            else // 3
            {
                localList.Add(new KTDoVong { ToHop = "CVV1", Fv = Math.Max(Math.Sqrt(fx_3_05_cvv1 * fx_3_05_cvv1 + fy_3_05_cvv1 * fy_3_05_cvv1), Math.Sqrt(fx_3_011_cvv1 * fx_3_011_cvv1 + fy_3_011_cvv1 * fy_3_011_cvv1)) });
                localList.Add(new KTDoVong { ToHop = "CVV2", Fv = Math.Max(Math.Sqrt(fx_3_05_cvv2 * fx_3_05_cvv2 + fy_3_05_cvv2 * fy_3_05_cvv2), Math.Sqrt(fx_3_011_cvv2 * fx_3_011_cvv2 + fy_3_011_cvv2 * fy_3_011_cvv2)) });
            }

            double DoVongChoPhep = L * 1000 / 200;
            var result = new ObservableCollection<KiemTraVong>();
            foreach (var item in localList)
            {
                double fv = item.Fv ?? 0;
                result.Add(new KiemTraVong()
                {
                    ToHop = item.ToHop,
                    DoVong = Math.Round(fv, 1),
                    DauSoSanh = "<=",
                    DoVongChoPhep = Math.Round(DoVongChoPhep, 1),
                    NhanXet = fv <= DoVongChoPhep ? "ĐẠT" : "KHÔNG ĐẠT"
                });
            }
            return result;
        }

        private void btn_XuatExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "BaoCao_ThietKe_XaGo.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {

                    ObservableCollection<KiemTraBen> dsKiemTraBen = new ObservableCollection<KiemTraBen>();
                    double Wx = GlobalData.Wx;
                    double Wy = GlobalData.Wy;
                    double fy = GlobalData.Fy > 0 ? GlobalData.Fy : 2450.0;
                    double B1 = GlobalData.B1;


                    foreach (var th in GlobalData.DsNoiLucTinhToan)
                    {
                        double sigmaTd;
                        if (B1 == 0)
                        {
                            sigmaTd = 100 * (Math.Abs(th.Mx) / (Wx <= 0 ? 1 : Wx) + Math.Abs(th.My) / (Wy <= 0 ? 1 : Wy));
                        }
                        else
                        {
                            sigmaTd = 100 * (0.5 * Math.Abs(th.Mx) / (Wx <= 0 ? 1 : Wx) + 0.5 * Math.Abs(th.My) / (Wy <= 0 ? 1 : Wy));
                        }

                        dsKiemTraBen.Add(new KiemTraBen()
                        {
                            ToHop = th.Truonghop,
                            SigmaTd = Math.Round(sigmaTd, 2),
                            DauSoSanh = "<=",
                            SigmaChoPhep = fy,
                            NhanXet = sigmaTd <= fy ? "ĐẠT" : "KHÔNG ĐẠT"
                        });
                    }

                    ExportToExcel(saveFileDialog.FileName, dsKiemTraBen, dsKiemTra);
                    MessageBox.Show("Xuất báo cáo Excel thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                    }
                    catch (Exception)
                    {
                    }
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        private static string GenerateDiagramImage(string type, double spanL, double loadQ)
        {
            int width = 800;
            int height = 300;
            string tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{type}_{Guid.NewGuid()}.png");

            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height))
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


                    using (System.Drawing.SolidBrush bgBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255)))
                    {
                        g.FillRectangle(bgBrush, 0, 0, width, height);
                    }

                    using (System.Drawing.Pen borderPen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(226, 232, 240), 1.5f))
                    {
                        g.DrawRectangle(borderPen, 0, 0, width - 1, height - 1);
                    }


                    int paddingLeft = 50;
                    int paddingRight = 50;
                    int paddingTop = 60;
                    int paddingBottom = 60;
                    int graphW = width - paddingLeft - paddingRight;
                    int graphH = height - paddingTop - paddingBottom;
                    int baselineY = paddingTop + graphH / 2;

                    if (type == "Deflection")
                    {
                        baselineY = paddingTop + 30;
                    }


                    double[] supportMoments = SolveSupportMoments(5, spanL, loadQ);
                    double maxAbsVal = 0.001;
                    int numPoints = 300;
                    double totalL = 5 * spanL;

                    List<System.Drawing.PointF> points = new List<System.Drawing.PointF>();

                    for (int i = 0; i <= numPoints; i++)
                    {
                        double globalX = (i / (double)numPoints) * totalL;
                        int spanIndex = (int)(globalX / spanL);
                        if (spanIndex >= 5) spanIndex = 4;
                        double localX = globalX - spanIndex * spanL;

                        double val = GetValAtCoordinate(spanIndex, localX, type, spanL, loadQ, supportMoments);
                        if (Math.Abs(val) > maxAbsVal)
                        {
                            maxAbsVal = Math.Abs(val);
                        }
                    }


                    for (int i = 0; i <= numPoints; i++)
                    {
                        double globalX = (i / (double)numPoints) * totalL;
                        int spanIndex = (int)(globalX / spanL);
                        if (spanIndex >= 5) spanIndex = 4;
                        double localX = globalX - spanIndex * spanL;

                        double val = GetValAtCoordinate(spanIndex, localX, type, spanL, loadQ, supportMoments);

                        float px = paddingLeft + (float)((globalX / totalL) * graphW);
                        float py;
                        if (type == "Deflection")
                        {
                            py = baselineY + (float)((val / maxAbsVal) * (graphH - 40));
                        }
                        else
                        {
                            py = baselineY - (float)((val / maxAbsVal) * (graphH / 2.2));
                        }

                        points.Add(new System.Drawing.PointF(px, py));
                    }


                    using (System.Drawing.Pen basePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(148, 163, 184), 1.5f))
                    {
                        g.DrawLine(basePen, paddingLeft, baselineY, paddingLeft + graphW, baselineY);
                    }


                    System.Drawing.Color curveColor = System.Drawing.Color.FromArgb(108, 157, 86); // Olive Green for Moment (from BackGround.png)
                    System.Drawing.Color hatchColor = System.Drawing.Color.FromArgb(40, 108, 157, 86);
                    if (type == "Shear")
                    {
                        curveColor = System.Drawing.Color.FromArgb(225, 29, 72);
                        hatchColor = System.Drawing.Color.FromArgb(30, 225, 29, 72);
                    }
                    else if (type == "Deflection")
                    {
                        curveColor = System.Drawing.Color.FromArgb(37, 99, 235);
                        hatchColor = System.Drawing.Color.FromArgb(30, 37, 99, 235);
                    }

                    using (System.Drawing.Pen hatchPen = new System.Drawing.Pen(hatchColor, 1f))
                    {
                        foreach (var pt in points)
                        {
                            g.DrawLine(hatchPen, pt.X, baselineY, pt.X, pt.Y);
                        }
                    }

                    using (System.Drawing.Pen curvePen = new System.Drawing.Pen(curveColor, 2.5f))
                    {
                        g.DrawLines(curvePen, points.ToArray());
                    }


                    using (System.Drawing.SolidBrush supportBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(108, 157, 86))) // Olive green support markers
                    using (System.Drawing.Font font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold))
                    using (System.Drawing.SolidBrush textBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(51, 65, 85))) // Slate dark gray text
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            float sx = paddingLeft + (float)((i / 5.0) * graphW);
                            float sy = baselineY;

                            System.Drawing.PointF[] triangle = new System.Drawing.PointF[]
                            {
                                new System.Drawing.PointF(sx, sy),
                                new System.Drawing.PointF(sx - 6, sy + 10),
                                new System.Drawing.PointF(sx + 6, sy + 10)
                            };
                            g.FillPolygon(supportBrush, triangle);

                            string label = (i + 1).ToString();
                            System.Drawing.SizeF labelSize = g.MeasureString(label, font);
                            g.DrawString(label, font, textBrush, sx - labelSize.Width / 2, sy + 12);
                        }
                    }


                    string titleText = $"BIỂU ĐỒ BENDING MOMENT (My) - kNm";
                    if (type == "Shear") titleText = $"BIỂU ĐỒ LỰC CẮT (V) - kN";
                    else if (type == "Deflection") titleText = $"BIỂU ĐỒ ĐỘ VÕNG (f) - mm";

                    using (System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold))
                    using (System.Drawing.SolidBrush titleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(108, 157, 86))) // Olive green title
                    {
                        g.DrawString(titleText, titleFont, titleBrush, 20, 20);
                    }
                }
                bmp.Save(tempFile, System.Drawing.Imaging.ImageFormat.Png);
            }

            return tempFile;
        }


    }
}
