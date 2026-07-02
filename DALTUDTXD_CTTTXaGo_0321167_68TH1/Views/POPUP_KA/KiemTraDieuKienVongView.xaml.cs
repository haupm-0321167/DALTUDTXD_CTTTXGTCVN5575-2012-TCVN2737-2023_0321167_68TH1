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


    }
}
