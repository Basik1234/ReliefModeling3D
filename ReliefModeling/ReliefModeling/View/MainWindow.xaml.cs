using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using OpenTK;
using ReliefModeling.Model;
using ReliefModeling.Model.Controls;
using ReliefModeling.ViewModel;

namespace ReliefModeling.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void MainWindow_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                ControlView3DObject.Child = vm.view3D;
            }
        }
    }
}