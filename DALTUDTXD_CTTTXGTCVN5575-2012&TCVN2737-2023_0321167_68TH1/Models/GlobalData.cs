using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Models
{
    public class GlobalData
    {
        public string Truonghop { get; set; }
        public string TruongHop { get; set; }

        public static double TaiTrongGio { get; set; }

        public static double TongTinhTai { get; set; }

        public static double TongHoatTai { get; set; }
        public static double DoDocMai { get; set; }
        public static double NhipXaGo { get; set; }
        public static double TyGiang { get; set; }
        public static double A { get; set; }
        public static double G { get; set; }
        public static double B1 { get; set; }
        public static double Fy { get; set; }
        public static double Jx { get; set; }
        public static double Jy { get; set; }

        public static double Wx { get; set; }
        public static double Wy { get; set; }
        public static double Px { get; set; }
        public static double Py { get; set; }
        public static double Mx { get; set; }
        public static double My { get; set; }
        public static double Ptcx { get; set; }
        public static double Ptcy { get; set; }
        public static double Wtcx { get; set; }
        public static double Wtcy { get; set; }
        public static double Ptcx_CVV1 { get; set; }
        public static double Ptcy_CVV1 { get; set; }
        public static double Ptcx_CVV2 { get; set; }
        public static double Ptcy_CVV2 { get; set; }
        public static XagoModels SelectedPurlin { get; set; }
        public static List<ToHopTaiTrong> DsNoiLucTinhToan { get; set; }
    = new List<ToHopTaiTrong>();

        public static List<ToHopTaiTrong> DsNoiLucTieuChuan { get; set; }
            = new List<ToHopTaiTrong>();

    }
}
