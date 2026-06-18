using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ContactView.xaml
    /// </summary>
    public partial class ContactView : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Contact> Contacts { get; set; }

        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ContactView()
        {
            InitializeComponent();
            DataContext = this;

            Contacts = new ObservableCollection<Contact>()
         {
            new Contact { Ten="Phạm Minh Hậu", SDT="0375832691", ViTri="Quản lý phần mềm"},
            new Contact { Ten="Nguyễn Kim Anh", SDT="0987654321", ViTri="Quản lý phần Hiển Thị"},
            new Contact { Ten="Nguyễn Thị Anh", SDT="0369852147", ViTri="Quản lý phần Thiết Kế"}
         };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
