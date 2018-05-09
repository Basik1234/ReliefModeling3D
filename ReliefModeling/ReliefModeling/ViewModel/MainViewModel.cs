using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using ReliefModeling.Model;
using ReliefModeling.Services;
using ReliefModeling.Model.Controls;

namespace ReliefModeling.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private const string ImagePath =
            "C:\\Users\\Basik\\Desktop\\Univer\\Git\\3D Relief Modeling\\ReliefModeling\\ReliefModeling\\Resource\\save7.png";
        private const string LegendPath =
            "C:\\Users\\Basik\\Desktop\\Univer\\Git\\3D Relief Modeling\\ReliefModeling\\ReliefModeling\\Resource\\save7_legend.png";

        private BitmapImage _bitmapImage;
        private BitmapImage _bitmapLegend;
        private Image _image;
        private Image _legendImage;
        private RelayCommand _commandLoadImage;
        private RelayCommand _commandLoadLegend;
        private RelayCommand _commandConver2DTo3D;
        private RelayCommand _commandMouseDown;
        
        public readonly View3D View3D = new View3D();
        
        #endregion

        #region Properties

        public Logger Logger => Logger.Instance;
        
        public BitmapImage BitmapImage
        {
            get => _bitmapImage;
            set
            {
                _bitmapImage = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage BitmapLegend
        {
            get => _bitmapLegend;
            set
            {
                _bitmapLegend = value;
                OnPropertyChanged();
            }
        }

        public Image LegendImage
        {
            get => _legendImage;
            set
            {
                _legendImage = value;
                BitmapLegend = value.ToBitmapImage();
            }
        }

        private Image Image
        {
            get => _image;
            set
            {
                _image = value;
                BitmapImage = value.ToBitmapImage();
            }
        }

        #endregion
        
        #region Command
        
        public RelayCommand CommandLoadLegend
        {
            get
            {
                return _commandLoadLegend ?? (_commandLoadLegend = new RelayCommand(obj =>
                {
                    try
                    {
                        Logger.Instance.WriteLine($"Загрузка: {LegendPath}");
                        LegendImage = new Bitmap(LegendPath);
                        
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.WriteLine($"Ошибка: {e.Message}");
                    }
                }));
            }
        }
        public RelayCommand CommandLoadImage
        {
            get
            {
                return _commandLoadImage ?? (_commandLoadImage = new RelayCommand(obj =>
                {
                    try
                    {
                        Logger.Instance.WriteLine($"Загрузка: {ImagePath}");
                        Image = new Bitmap(ImagePath);
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.WriteLine($"Ошибка: {e.Message}");
                    }
                }));
            }
        }
        public RelayCommand CommandConver2DTo3D
        {
            get
            {
                return _commandConver2DTo3D ?? (_commandConver2DTo3D = new RelayCommand(obj =>
                {
                    try
                    {
                        Logger.Instance.WriteLine($"Конвертируем: {BitmapImage.ToString()}");
                        View3D.Shape = Image.To3D(LegendImage);
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.WriteLine($"Ошибка: {e.Message}");
                    }
                }));
            }
        }
        public RelayCommand CommandMouseDown
        {
            get
            {
                return _commandMouseDown ?? (_commandMouseDown = new RelayCommand(obj =>
                {
                    try
                    {
                        Logger.Instance.WriteLine($"CommandMouseDown");
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.WriteLine($"Ошибка: {e.Message}");
                    }
                }));
            }
        }
        
        #endregion
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}