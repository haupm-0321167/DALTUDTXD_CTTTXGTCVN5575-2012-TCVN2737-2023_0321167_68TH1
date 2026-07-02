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

        private static void InsertImage(WorksheetPart worksheetPart, string imagePath, int colIndex, int rowIndex)
        {
            DrawingsPart drawingsPart;
            if (worksheetPart.DrawingsPart == null)
            {
                drawingsPart = worksheetPart.AddNewPart<DrawingsPart>();
                worksheetPart.Worksheet.Append(new DocumentFormat.OpenXml.Spreadsheet.Drawing() { Id = worksheetPart.GetIdOfPart(drawingsPart) });
            }
            else
            {
                drawingsPart = worksheetPart.DrawingsPart;
            }

            ImagePart imagePart = drawingsPart.AddImagePart(ImagePartType.Png);
            using (FileStream stream = new FileStream(imagePath, FileMode.Open))
            {
                imagePart.FeedData(stream);
            }

            if (drawingsPart.WorksheetDrawing == null)
            {
                drawingsPart.WorksheetDrawing = new DocumentFormat.OpenXml.Drawing.Spreadsheet.WorksheetDrawing();
            }

            var worksheetDrawing = drawingsPart.WorksheetDrawing;

            var fromMarker = new DocumentFormat.OpenXml.Drawing.Spreadsheet.FromMarker()
            {
                ColumnId = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ColumnId(colIndex.ToString()),
                ColumnOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ColumnOffset("0"),
                RowId = new DocumentFormat.OpenXml.Drawing.Spreadsheet.RowId(rowIndex.ToString()),
                RowOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.RowOffset("0")
            };

            var toMarker = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ToMarker()
            {
                ColumnId = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ColumnId((colIndex + 5).ToString()), // span 5 columns
                ColumnOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ColumnOffset("0"),
                RowId = new DocumentFormat.OpenXml.Drawing.Spreadsheet.RowId((rowIndex + 10).ToString()), // span 10 rows
                RowOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.RowOffset("0")
            };

            string rId = drawingsPart.GetIdOfPart(imagePart);

            var nonVisualPictureProperties = new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureProperties(
                new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties() { Id = (uint)(worksheetDrawing.ChildElements.Count + 1), Name = "Chart_" + rId },
                new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureDrawingProperties());

            var picture = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture(
                nonVisualPictureProperties,
                new DocumentFormat.OpenXml.Drawing.Spreadsheet.BlipFill(
                    new DocumentFormat.OpenXml.Drawing.Blip() { Embed = rId, CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print },
                    new DocumentFormat.OpenXml.Drawing.Stretch(new DocumentFormat.OpenXml.Drawing.FillRectangle())),
                new DocumentFormat.OpenXml.Drawing.Spreadsheet.ShapeProperties(
                    new DocumentFormat.OpenXml.Drawing.Transform2D(
                        new DocumentFormat.OpenXml.Drawing.Offset() { X = 0, Y = 0 },
                        new DocumentFormat.OpenXml.Drawing.Extents() { Cx = 0, Cy = 0 }),
                    new DocumentFormat.OpenXml.Drawing.PresetGeometry() { Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle }));

            var twoCellAnchor = new DocumentFormat.OpenXml.Drawing.Spreadsheet.TwoCellAnchor(
                fromMarker,
                toMarker,
                picture,
                new DocumentFormat.OpenXml.Drawing.Spreadsheet.ClientData());

            worksheetDrawing.Append(twoCellAnchor);
        }

        public static void ExportToExcel(string filePath, ObservableCollection<KiemTraBen> dsKiemTraBen, ObservableCollection<KiemTraVong> dsKiemTraVong)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();


                var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                var stylesheet = new DocumentFormat.OpenXml.Spreadsheet.Stylesheet();
                var fonts = new DocumentFormat.OpenXml.Spreadsheet.Fonts(
                    new DocumentFormat.OpenXml.Spreadsheet.Font(new DocumentFormat.OpenXml.Spreadsheet.FontSize { Val = 11 }, new DocumentFormat.OpenXml.Spreadsheet.FontName { Val = "Arial" }), // 0: Default
                    new DocumentFormat.OpenXml.Spreadsheet.Font(new DocumentFormat.OpenXml.Spreadsheet.FontSize { Val = 16 }, new DocumentFormat.OpenXml.Spreadsheet.Bold(), new DocumentFormat.OpenXml.Spreadsheet.Color { Rgb = "4A7033" }, new DocumentFormat.OpenXml.Spreadsheet.FontName { Val = "Arial" }), // 1: Title (Dark Green)
                    new DocumentFormat.OpenXml.Spreadsheet.Font(new DocumentFormat.OpenXml.Spreadsheet.FontSize { Val = 12 }, new DocumentFormat.OpenXml.Spreadsheet.Bold(), new DocumentFormat.OpenXml.Spreadsheet.Color { Rgb = "FFFFFF" }, new DocumentFormat.OpenXml.Spreadsheet.FontName { Val = "Arial" }), // 2: Section Header (White Text)
                    new DocumentFormat.OpenXml.Spreadsheet.Font(new DocumentFormat.OpenXml.Spreadsheet.FontSize { Val = 11 }, new DocumentFormat.OpenXml.Spreadsheet.Bold(), new DocumentFormat.OpenXml.Spreadsheet.FontName { Val = "Arial" }), // 3: Table Header Bold
                    new DocumentFormat.OpenXml.Spreadsheet.Font(new DocumentFormat.OpenXml.Spreadsheet.FontSize { Val = 11 }, new DocumentFormat.OpenXml.Spreadsheet.FontName { Val = "Arial" }, new DocumentFormat.OpenXml.Spreadsheet.Italic()), // 4: Italic
                    new DocumentFormat.OpenXml.Spreadsheet.Font(new DocumentFormat.OpenXml.Spreadsheet.FontSize { Val = 11 }, new DocumentFormat.OpenXml.Spreadsheet.Bold(), new DocumentFormat.OpenXml.Spreadsheet.Color { Rgb = "15803D" }, new DocumentFormat.OpenXml.Spreadsheet.FontName { Val = "Arial" }), // 5: Green Bold (ĐẠT)
                    new DocumentFormat.OpenXml.Spreadsheet.Font(new DocumentFormat.OpenXml.Spreadsheet.FontSize { Val = 11 }, new DocumentFormat.OpenXml.Spreadsheet.Bold(), new DocumentFormat.OpenXml.Spreadsheet.Color { Rgb = "B91C1C" }, new DocumentFormat.OpenXml.Spreadsheet.FontName { Val = "Arial" })  // 6: Red Bold (KHÔNG ĐẠT)
                );

                var fills = new DocumentFormat.OpenXml.Spreadsheet.Fills(
                    new DocumentFormat.OpenXml.Spreadsheet.Fill(new DocumentFormat.OpenXml.Spreadsheet.PatternFill { PatternType = PatternValues.None }), // 0: None
                    new DocumentFormat.OpenXml.Spreadsheet.Fill(new DocumentFormat.OpenXml.Spreadsheet.PatternFill { PatternType = PatternValues.Gray125 }), // 1: Gray125 (reserved by Excel)
                    new DocumentFormat.OpenXml.Spreadsheet.Fill(new DocumentFormat.OpenXml.Spreadsheet.PatternFill(new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor { Rgb = "6C9D56" }) { PatternType = PatternValues.Solid }), // 2: Olive Green for Section (matching background.png)
                    new DocumentFormat.OpenXml.Spreadsheet.Fill(new DocumentFormat.OpenXml.Spreadsheet.PatternFill(new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor { Rgb = "D2EDB2" }) { PatternType = PatternValues.Solid })  // 3: Light Green for Table Header
                );

                var borders = new DocumentFormat.OpenXml.Spreadsheet.Borders(
                    new DocumentFormat.OpenXml.Spreadsheet.Border(), // 0: None
                    new DocumentFormat.OpenXml.Spreadsheet.Border( // 1: Thin border all around
                        new DocumentFormat.OpenXml.Spreadsheet.LeftBorder(new DocumentFormat.OpenXml.Spreadsheet.Color { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DocumentFormat.OpenXml.Spreadsheet.RightBorder(new DocumentFormat.OpenXml.Spreadsheet.Color { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DocumentFormat.OpenXml.Spreadsheet.TopBorder(new DocumentFormat.OpenXml.Spreadsheet.Color { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DocumentFormat.OpenXml.Spreadsheet.BottomBorder(new DocumentFormat.OpenXml.Spreadsheet.Color { Auto = true }) { Style = BorderStyleValues.Thin }
                    )
                );

                var cellFormats = new DocumentFormat.OpenXml.Spreadsheet.CellFormats(
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat { FontId = 0, FillId = 0, BorderId = 0 }, // 0: Default
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat { FontId = 1, FillId = 0, BorderId = 0 }, // 1: Title (Arial 16pt Bold)
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat { FontId = 4, FillId = 0, BorderId = 0 }, // 2: Subtitle (Arial 11pt Italic)
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat { FontId = 2, FillId = 2, BorderId = 0 }, // 3: Section Header (Arial 12pt Bold + Light Blue-Grey Fill)
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat(new DocumentFormat.OpenXml.Spreadsheet.Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center })
                    { FontId = 3, FillId = 3, BorderId = 1, ApplyAlignment = true }, // 4: Table Header Centered
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat(new DocumentFormat.OpenXml.Spreadsheet.Alignment { Horizontal = HorizontalAlignmentValues.Left, Vertical = VerticalAlignmentValues.Center })
                    { FontId = 0, FillId = 0, BorderId = 1, ApplyAlignment = true }, // 5: Table Cell Normal Left
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat(new DocumentFormat.OpenXml.Spreadsheet.Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center })
                    { FontId = 0, FillId = 0, BorderId = 1, ApplyAlignment = true }, // 6: Table Cell Normal Center
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat(new DocumentFormat.OpenXml.Spreadsheet.Alignment { Horizontal = HorizontalAlignmentValues.Right, Vertical = VerticalAlignmentValues.Center })
                    { FontId = 0, FillId = 0, BorderId = 1, ApplyAlignment = true }, // 7: Table Cell Normal Right
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat(new DocumentFormat.OpenXml.Spreadsheet.Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center })
                    { FontId = 5, FillId = 0, BorderId = 1, ApplyAlignment = true }, // 8: Green Bold Center (ĐẠT)
                    new DocumentFormat.OpenXml.Spreadsheet.CellFormat(new DocumentFormat.OpenXml.Spreadsheet.Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center })
                    { FontId = 6, FillId = 0, BorderId = 1, ApplyAlignment = true }  // 9: Red Bold Center (KHÔNG ĐẠT)
                );

                stylesheet.Append(fonts);
                stylesheet.Append(fills);
                stylesheet.Append(borders);
                stylesheet.Append(cellFormats);

                stylesPart.Stylesheet = stylesheet;
                stylesPart.Stylesheet.Save();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Columns columns = new Columns();
                columns.Append(new Column() { Min = 1, Max = 1, Width = 38, CustomWidth = true }); // Col A
                columns.Append(new Column() { Min = 2, Max = 2, Width = 18, CustomWidth = true }); // Col B
                columns.Append(new Column() { Min = 3, Max = 3, Width = 12, CustomWidth = true }); // Col C
                columns.Append(new Column() { Min = 4, Max = 4, Width = 28, CustomWidth = true }); // Col D
                columns.Append(new Column() { Min = 5, Max = 5, Width = 18, CustomWidth = true }); // Col E
                worksheetPart.Worksheet.InsertAt(columns, 0);


                sheetData.AppendChild(CreateRow(CreateCell("BÁO CÁO KẾT QUẢ TÍNH TOÁN VÀ THIẾT KẾ XÀ GỒ", 1)));
                sheetData.AppendChild(CreateRow(CreateCell("Chương trình tính toán xà gồ", 2)));
                sheetData.AppendChild(CreateRow());


                sheetData.AppendChild(CreateRow(CreateCell("1. THÔNG SỐ TIẾT DIỆN VÀ ĐẶC TRƯNG HÌNH HỌC", 3)));
                sheetData.AppendChild(CreateRow(
                    CreateCell("Đặc trưng tiết diện xà gồ", 4),
                    CreateCell("Giá trị", 4),
                    CreateCell("Đơn vị", 4)
                ));

                var sp = GlobalData.SelectedPurlin;
                sheetData.AppendChild(CreateRow(CreateCell("Loại tiết diện xà gồ", 5), CreateCell(sp?.Loai ?? "XG Z cán nóng", 6), CreateCell("-", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Chiều cao tiết diện H", 5), CreateCell((sp?.Height ?? 150.0).ToString("F1"), 7), CreateCell("mm", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Chiều rộng cánh B", 5), CreateCell((sp?.Width ?? 62.0).ToString("F1"), 7), CreateCell("mm", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Chiều dài mép gấp c", 5), CreateCell((sp?.Lip ?? 5.0).ToString("F1"), 7), CreateCell("mm", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Chiều dày tiết diện t", 5), CreateCell((sp?.Thickness ?? 2.30).ToString("F2"), 7), CreateCell("mm", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Chiều dài nhịp L", 5), CreateCell(GlobalData.NhipXaGo.ToString("F2"), 7), CreateCell("m", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Diện tích tiết diện A", 5), CreateCell((sp?.A ?? 6.90).ToString("F2"), 7), CreateCell("cm2", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Trọng lượng đơn vị G", 5), CreateCell((sp?.G ?? 5.42).ToString("F2"), 7), CreateCell("kg/m", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Mô-men quán tính Jx", 5), CreateCell(GlobalData.Jx.ToString("F2"), 7), CreateCell("cm4", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Mô-men quán tính Jy", 5), CreateCell(GlobalData.Jy.ToString("F2"), 7), CreateCell("cm4", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Mô-men kháng uốn Wx", 5), CreateCell(GlobalData.Wx.ToString("F2"), 7), CreateCell("cm3", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Mô-men kháng uốn Wy", 5), CreateCell(GlobalData.Wy.ToString("F2"), 7), CreateCell("cm3", 6)));

                sheetData.AppendChild(CreateRow());


                sheetData.AppendChild(CreateRow(CreateCell("2. CÁC THÀNH PHẦN TẢI TRỌNG THIẾT KẾ", 3)));
                sheetData.AppendChild(CreateRow(
                    CreateCell("Tải trọng tác dụng", 4),
                    CreateCell("Giá trị tính toán", 4),
                    CreateCell("Đơn vị", 4)
                ));
                sheetData.AppendChild(CreateRow(CreateCell("Tĩnh tải tác dụng (tự trọng + mái tôn)", 5), CreateCell(GlobalData.TongTinhTai.ToString("F2"), 7), CreateCell("kg/m", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Hoạt tải tác dụng (sửa chữa, gió tĩnh)", 5), CreateCell(GlobalData.TongHoatTai.ToString("F2"), 7), CreateCell("kg/m", 6)));
                sheetData.AppendChild(CreateRow(CreateCell("Tải trọng gió tác dụng (hút / đẩy)", 5), CreateCell(GlobalData.TaiTrongGio.ToString("F2"), 7), CreateCell("kg/m", 6)));

                sheetData.AppendChild(CreateRow());

                sheetData.AppendChild(CreateRow(CreateCell("3. KẾT QUẢ KIỂM TRA ĐIỀU KIỆN BỀN (STRENGTH CHECK)", 3)));
                sheetData.AppendChild(CreateRow(
                    CreateCell("Tổ hợp tải trọng tính toán", 4),
                    CreateCell("Ứng suất σtd (kg/cm2)", 4),
                    CreateCell("So sánh", 4),
                    CreateCell("Ứng suất cho phép [σ] (kg/cm2)", 4),
                    CreateCell("Kết luận", 4)
                ));

                int thIndex = 1;
                foreach (var item in dsKiemTraBen)
                {
                    string tohopName = $"TH{thIndex}: " + (thIndex == 1 ? "1*TT + 1*HT" : "1*TT + 1*W");
                    string nhanXet = item.NhanXet;
                    if (nhanXet == "OK") nhanXet = "ĐẠT";
                    else if (nhanXet == "NOT OK") nhanXet = "KHÔNG ĐẠT";

                    uint resultStyle = nhanXet == "ĐẠT" ? (uint)8 : (uint)9;

                    sheetData.AppendChild(CreateRow(
                        CreateCell(tohopName, 5),
                        CreateCell(item.SigmaTd.ToString("F2"), 7),
                        CreateCell(item.DauSoSanh ?? "<=", 6),
                        CreateCell(item.SigmaChoPhep.ToString("F2"), 7),
                        CreateCell(nhanXet, resultStyle)
                    ));
                    thIndex++;
                }

                sheetData.AppendChild(CreateRow());


                sheetData.AppendChild(CreateRow(CreateCell("4. KẾT QUẢ KIỂM TRA ĐIỀU KIỆN VÕNG (DEFLECTION CHECK)", 3)));
                sheetData.AppendChild(CreateRow(
                    CreateCell("Tổ hợp tải trọng tiêu chuẩn", 4),
                    CreateCell("Độ võng f (mm)", 4),
                    CreateCell("So sánh", 4),
                    CreateCell("Độ võng giới hạn [f] (mm)", 4),
                    CreateCell("Kết luận", 4)
                ));

                thIndex = 1;
                foreach (var item in dsKiemTraVong)
                {
                    string tohopName = $"TH{thIndex}: " + (thIndex == 1 ? "1*TT tiêu chuẩn + 1*HT tiêu chuẩn" : "1*TT tiêu chuẩn + 1*W tiêu chuẩn");
                    string nhanXet = item.NhanXet;
                    if (nhanXet == "OK") nhanXet = "ĐẠT";
                    else if (nhanXet == "NOT OK") nhanXet = "KHÔNG ĐẠT";

                    uint resultStyle = nhanXet == "ĐẠT" ? (uint)8 : (uint)9;

                    sheetData.AppendChild(CreateRow(
                        CreateCell(tohopName, 5),
                        CreateCell(item.DoVong.ToString("F2"), 7),
                        CreateCell(item.DauSoSanh ?? "<=", 6),
                        CreateCell(item.DoVongChoPhep.ToString("F2"), 7),
                        CreateCell(nhanXet, resultStyle)
                    ));
                    thIndex++;
                }

                sheetData.AppendChild(CreateRow());


                sheetData.AppendChild(CreateRow(CreateCell("5. BIỂU ĐỒ NỘI LỰC VÀ ĐỘ VÕNG DẦM LIÊN TỤC 5 NHỊP", 3)));
                sheetData.AppendChild(CreateRow());


                MergeCells mergeCells = new MergeCells();
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:E1") });
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A2:E2") });
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A4:C4") });
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A19:C19") });
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A25:E25") });
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A30:E30") });
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A35:E35") });
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);


                Sheets sheetsList = workbookPart.Workbook.Sheets;
                if (sheetsList == null)
                {
                    sheetsList = workbookPart.Workbook.AppendChild(new Sheets());
                }
                string relationshipId = workbookPart.GetIdOfPart(worksheetPart);
                Sheet sheet = new Sheet() { Id = relationshipId, SheetId = 1, Name = "Thiet Ke Xa Go" };
                sheetsList.Append(sheet);


                double nhipVal = GlobalData.NhipXaGo;
                if (nhipVal <= 0) nhipVal = 6.0;
                double loadQVal = (GlobalData.TongTinhTai + GlobalData.TongHoatTai) / 100.0;
                if (loadQVal <= 0) loadQVal = 10.0;

                string imgMoment = string.Empty;
                string imgShear = string.Empty;
                string imgDeflection = string.Empty;

                try
                {
                    imgMoment = GenerateDiagramImage("Moment", nhipVal, loadQVal);
                    imgShear = GenerateDiagramImage("Shear", nhipVal, loadQVal);
                    imgDeflection = GenerateDiagramImage("Deflection", nhipVal, loadQVal);

                    InsertImage(worksheetPart, imgMoment, 0, 36); // Col A, Row 37
                    InsertImage(worksheetPart, imgShear, 0, 47); // Col A, Row 48
                    InsertImage(worksheetPart, imgDeflection, 0, 58); // Col A, Row 59
                }
                catch (Exception)
                {

                }

                workbookPart.Workbook.Save();


                try
                {
                    if (File.Exists(imgMoment)) File.Delete(imgMoment);
                    if (File.Exists(imgShear)) File.Delete(imgShear);
                    if (File.Exists(imgDeflection)) File.Delete(imgDeflection);
                }
                catch (Exception) { }
            }
        }

        private static Row CreateRow(params Cell[] cells)
        {
            Row row = new Row();
            foreach (var cell in cells)
            {
                row.Append(cell);
            }
            return row;
        }

        private static Cell CreateCell(string text)
        {
            return new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(text ?? string.Empty)
            };
        }

        private static Cell CreateCell(string text, uint styleIndex)
        {
            return new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(text ?? string.Empty),
                StyleIndex = styleIndex
            };
        }

        private static double[] SolveSupportMoments(int numSpans, double spanL, double loadQ)
        {
            int numSupports = numSpans + 1;
            double[] M = new double[numSupports];
            if (numSpans == 1)
            {
                M[0] = 0;
                M[1] = 0;
                return M;
            }

            int n = numSpans - 1;
            double[] a = new double[n];
            double[] b = new double[n];
            double[] c = new double[n];
            double[] d = new double[n];

            for (int i = 0; i < n; i++)
            {
                a[i] = 1.0;
                b[i] = 4.0;
                c[i] = 1.0;
                d[i] = -0.5 * loadQ * spanL * spanL;
            }

            double[] cPrime = new double[n];
            double[] dPrime = new double[n];

            cPrime[0] = c[0] / b[0];
            dPrime[0] = d[0] / b[0];

            for (int i = 1; i < n; i++)
            {
                double denom = b[i] - a[i] * cPrime[i - 1];
                cPrime[i] = c[i] / denom;
                dPrime[i] = (d[i] - a[i] * dPrime[i - 1]) / denom;
            }

            double[] x = new double[n];
            x[n - 1] = dPrime[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = dPrime[i] - cPrime[i] * x[i + 1];
            }

            M[0] = 0;
            M[numSpans] = 0;
            for (int i = 0; i < n; i++)
            {
                M[i + 1] = x[i];
            }

            return M;
        }

        private static double GetValAtCoordinate(int spanIndex, double localX, string type, double spanL, double loadQ, double[] supportMoments)
        {
            if (supportMoments == null || spanIndex < 0 || spanIndex >= supportMoments.Length - 1) return 0;
            double M_left = supportMoments[spanIndex];
            double M_right = supportMoments[spanIndex + 1];

            if (type == "Moment")
            {
                return M_left * (1.0 - localX / spanL) + M_right * (localX / spanL) + (loadQ * localX * (spanL - localX)) / 2.0;
            }
            else if (type == "Shear")
            {
                return (M_right - M_left) / spanL + (loadQ * (spanL - 2.0 * localX)) / 2.0;
            }
            else
            {
                double Jx = GlobalData.Jx;
                if (Jx <= 0) Jx = 200.0;
                double E = 2.1e5;
                double I = Jx * 10000.0;

                double q_N_mm = loadQ;
                double L_mm = spanL * 1000.0;
                double x_mm = localX * 1000.0;

                double y_mm = (M_left * 1000000.0 * (x_mm * x_mm / 2.0 - x_mm * x_mm * x_mm / (6.0 * L_mm) - L_mm * x_mm / 3.0) +
                               M_right * 1000000.0 * (x_mm * x_mm * x_mm / (6.0 * L_mm) - L_mm * x_mm / 6.0) +
                               q_N_mm * (L_mm * x_mm * x_mm * x_mm / 12.0 - x_mm * x_mm * x_mm * x_mm / 24.0 - L_mm * L_mm * L_mm * x_mm / 24.0)) / (E * I);

                return Math.Abs(y_mm);
            }
        }
    }
}
