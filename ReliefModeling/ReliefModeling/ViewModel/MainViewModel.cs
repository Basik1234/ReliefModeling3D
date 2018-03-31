using System;
using System.ComponentModel;
using System.Drawing;
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
            "C:\\Users\\Basik\\Desktop\\Univer\\Git\\3D Relief Modeling\\ReliefModeling\\ReliefModeling\\Resource\\image.bmp";

        private BitmapImage _bitmapImage;
        private string _logTextBox;
        private Image _image;
        private RelayCommand _commandLoadImage;
        private RelayCommand _commandConver2DTo3D;
        
        public readonly View3D View3D = new View3D();
        
        #endregion

        #region Properties

        public BitmapImage BitmapImage
        {
            get => _bitmapImage;
            set
            {
                _bitmapImage = value;
                OnPropertyChanged();
            }
        }
        public string LogTextBox
        {
            get => _logTextBox;
            set
            {
                _logTextBox = value;
                OnPropertyChanged();
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
        
        public RelayCommand CommandLoadImage
        {
            get
            {
                return _commandLoadImage ?? (_commandLoadImage = new RelayCommand(obj =>
                {
                    try
                    {
                        Image = new Bitmap(ImagePath);
                        LogTextBox += $"Загрузка: {ImagePath} \n";
                    }
                    catch (Exception e)
                    {
                        LogTextBox += $"Неудалось: {e.Message} \n";
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
                        View3D.Shape = Image.To3D();
                        LogTextBox += $"Конвертируем: {BitmapImage.ToString()} \n";
                    }
                    catch (Exception e)
                    {
                        LogTextBox += $"Неудалось: {e.Message} \n";
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