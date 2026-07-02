using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Models
{
    public class TaiTrongItem
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _tenVatLieu;
        public string TenVatLieu
        {
            get => _tenVatLieu;
            set
            {
                _tenVatLieu = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(TenVatLieu)));
            }
        }

        private double _gtc;
        public double Gtc
        {
            get => _gtc;
            set
            {
                _gtc = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Gtc)));
            }
        }

        private double _n1;
        public double N1
        {
            get => _n1;
            set
            {
                _n1 = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(N1)));
            }
        }

        private double _b;
        public double B
        {
            get => _b;
            set
            {
                _b = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(B)));
            }
        }

        private double _gtt;
        public double Gtt
        {
            get => _gtt;
            set
            {
                _gtt = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Gtt)));
            }
        }


        private string _LoaiHoatTai;
        public string LoaiHoatTai
        {
            get => _LoaiHoatTai;
            set
            {
                _LoaiHoatTai = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(LoaiHoatTai)));
            }
        }

        private double _ptc;
        public double Ptc
        {
            get => _ptc;
            set
            {
                _ptc = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Ptc)));
            }
        }

        private double _n2;
        public double N2
        {
            get => _n2;
            set
            {
                _n2 = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(N2)));
            }
        }



        private double _ptt;
        public double Ptt
        {
            get => _ptt;
            set
            {
                _ptt = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Ptt)));
            }
        }
    }
}
