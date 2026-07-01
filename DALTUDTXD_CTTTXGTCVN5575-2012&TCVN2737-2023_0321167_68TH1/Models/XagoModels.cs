using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Models
{
    public class XagoModels : INotifyPropertyChanged
    {

        private int id;

        private double extraWidth;

        private double height, width, thickness, length, lip;

        public double A { get; set; }
        public double G { get; set; }

        public double Ix { get; set; }
        public double Iy { get; set; }

        public double Wx { get; set; }
        public double Wy { get; set; }


        private string loai;
        public string Loai
        {
            get => loai;
            set { loai = value; OnChanged(nameof(Loai)); }
        }
        public double Radius { get; set; } = 5;
        public int Id
        {
            get => id;
            set { id = value; OnChanged(nameof(Id)); }
        }

        public double Height
        {
            get => height;
            set { height = value; OnChanged(nameof(Height)); }
        }
        public double Width
        {
            get => width;
            set { width = value; OnChanged(nameof(Width)); }
        }
        public double ExtraWidth
        {
            get => extraWidth;
            set { extraWidth = value; OnChanged(nameof(ExtraWidth)); }
        }
        public double Thickness
        {
            get => thickness;
            set { thickness = value; OnChanged(nameof(Thickness)); }
        }
        public double Length
        {
            get => length;
            set { length = value; OnChanged(nameof(Length)); }
        }
        public double Lip
        {
            get => lip;
            set { lip = value; OnChanged(nameof(Lip)); }
        } // mép gập
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Loai)) return false;

                if (Height <= 0 || Width <= 0 || Thickness <= 0 || Length < 0)
                    return false;

                if (Loai.Contains("C") || Loai.Contains("Z"))
                    return ExtraWidth >= 0;

                return true;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    }
    public class XaGo
    {
        public string SoHieu { get; set; }

        public double H { get; set; }

        public double B { get; set; }

        public double C { get; set; }

        public double t { get; set; }

        public double B1 { get; set; }
        public double D { get; set; }

        public double S { get; set; }      // Diện tích
        public double P { get; set; }      // Trọng lượng

        public double Jx { get; set; }
        public double Jy { get; set; }

        public double Wx { get; set; }
        public double Wy { get; set; }


    }
}
