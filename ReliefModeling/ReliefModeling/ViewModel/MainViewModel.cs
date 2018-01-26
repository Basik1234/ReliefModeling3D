using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using ReliefModeling.Services;
using ReliefModeling.Model;

namespace ReliefModeling.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private const string ImagePath =
            "C:\\Users\\Basik\\Desktop\\Univer\\Git\\3D Relief Modeling\\ReliefModeling\\ReliefModeling\\Resource\\image.bmp";

        #region Fields
        
        private BitmapImage _bitmapImage;
        private Shape _shape;
        private string _logTextBox;
        private RelayCommand _commandLoadImage;
        private RelayCommand _commandConver2DTo3D;
        
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
        
        public Shape Shape
                {
                    get => _shape;
                    set
                    {
                        _shape = value;
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
        
        public RelayCommand CommandLoadImage
                {
                    get { return _commandLoadImage ?? (_commandLoadImage = new RelayCommand(obj =>
                    {
                        try
                        {
                            BitmapImage = new BitmapImage(new Uri(ImagePath));
                            LogTextBox += $"Загрузка: {BitmapImage.ToString()} \n";
                        }
                        catch (Exception e)
                        {
                            LogTextBox += $"Неудалось: {e.Message} \n";
                        }
                        
                    })); }
                }
        
        public RelayCommand CommandConver2DTo3D
                {
                    get { return _commandConver2DTo3D ?? (_commandConver2DTo3D = new RelayCommand(obj =>
                    {
                        try
                        {
                            Shape = Convertor.Convert2DTo3D(BitmapImage);
                            LogTextBox += $"Конвертируем: {BitmapImage.ToString()} \n";
                        }
                        catch (Exception e)
                        {
                            LogTextBox += $"Неудалось: {e.Message} \n";
                        }
                        
                    })); }
                }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}