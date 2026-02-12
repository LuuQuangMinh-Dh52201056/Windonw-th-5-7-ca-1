using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.MyModels
{
    public class HangHoaVM : CBaseMVVM
    {
        private hoadonContext context;
        private List<Hanghoa> m_listHangHoa;
        public List<Hanghoa> ListHangHoa
        {
            get { return m_listHangHoa; }
            set
            {
                m_listHangHoa = value;
                NotifyPropertyChanged("ListHangHoa");
            }
        }
        public Hanghoa m_selectedHangHoa;
        public Hanghoa SelectedHangHoa
        {
            get { return m_selectedHangHoa; }
            set
            {
                m_selectedHangHoa = value;
                if (m_selectedHangHoa != null)
                {
                    mahang = m_selectedHangHoa.Mahang;
                    tenhang = m_selectedHangHoa.Tenhang;
                    dvt = m_selectedHangHoa.Dvt;
                    dongia = m_selectedHangHoa.Dongia.ToString();
                }
            }
        }

        public RelayCommand lenhThem { get; set; }
        public RelayCommand lenhSua { get; set; }
        public RelayCommand lenhXoa { get; set; }

        private string m_mahang { get; set; }
        private string m_tenhang { get; set; }
        private string m_dvt { get; set; }
        private string m_dongia { get; set; }

        public string mahang { get { return m_mahang; } set { m_mahang = value; NotifyPropertyChanged("mahang"); } }
        public string tenhang { get { return m_tenhang; } set { m_tenhang = value; NotifyPropertyChanged("tenhang"); } }
        public string dvt { get { return m_dvt; } set { m_dvt = value; NotifyPropertyChanged("dvt"); } }
        public string dongia { get { return m_dongia; } set { m_dongia = value; NotifyPropertyChanged("dongia"); } }

        public HangHoaVM()
        {
            context = new hoadonContext();
            ListHangHoa = context.Hanghoas.ToList();

            lenhThem = new RelayCommand(lenhThem_Execute, lenhThem_CanExecute);
            lenhXoa = new RelayCommand(lenhXoa_Execute, lenhXoa_CanExecute);
            lenhSua = new RelayCommand(lenhSua_Execute, lenhSua_CanExecute);
        }


        public virtual void lenhThem_Execute(object parameter)
        {
            Hanghoa hh = new Hanghoa()
            {
                Mahang = mahang,
                Tenhang = tenhang,
                Dvt = dvt,
                Dongia = double.Parse(dongia)
            };

            context.Hanghoas.Add(hh);
            context.SaveChanges();

            ListHangHoa = context.Hanghoas.ToList();
        }
        public virtual bool lenhThem_CanExecute(object parameter)
        {
            if (string.IsNullOrEmpty(mahang) || string.IsNullOrEmpty(tenhang) || string.IsNullOrEmpty(dvt) || string.IsNullOrEmpty(dongia))
            {
                return false;
            }

            double dg;
            if (double.TryParse(dongia, out dg) == false)
            {
                return false;
            }

            if (context.Hanghoas.Find(mahang) != null)
            {
                return false;
            }

            return true;
        }

        public virtual void lenhXoa_Execute(object parameter)
        {
            MessageBoxResult ok = MessageBox.Show("Bạn có chắc muốn xóa hàng hóa này?", "Thong Bao", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (ok != MessageBoxResult.OK)
            {
                return;
            }

            Hanghoa x = context.Hanghoas.Find(SelectedHangHoa.Mahang);
            if (x != null)
            {
                context.Hanghoas.Remove(x);
                context.SaveChanges();
                ListHangHoa = context.Hanghoas.ToList();
            }
        }
        public virtual bool lenhXoa_CanExecute(object parameter)
        {
            if (SelectedHangHoa == null)
            {
                return false;
            }

            if (context.Chitiethoadons.Count(t => t.Mahang == SelectedHangHoa.Mahang) > 0)
            {
                return false;
            }

            return true;
        }


        public virtual void lenhSua_Execute(object parameter)
        {
            MessageBoxResult ok = MessageBox.Show("Bạn có chắc muốn sua hàng hóa này?", "Thong Bao", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (ok != MessageBoxResult.OK)
            {
                return;
            }

            Hanghoa x = context.Hanghoas.Find(SelectedHangHoa.Mahang);
            if (x != null)
            {
                x.Tenhang = tenhang;
                x.Dvt = dvt;
                x.Dongia = double.Parse(dongia);
                context.SaveChanges();
                ListHangHoa = context.Hanghoas.ToList();
            }
        }
        public virtual bool lenhSua_CanExecute(object parameter)
        {
            if (string.IsNullOrEmpty(mahang) || string.IsNullOrEmpty(tenhang) || string.IsNullOrEmpty(dvt) || string.IsNullOrEmpty(dongia))
            {
                return false;
            }

            double dg;
            if (double.TryParse(dongia, out dg) == false)
            {
                return false;
            }

            if (context.Hanghoas.Find(mahang) == null)
            {
                return false;
            }

            return true;
        }
    }
}
