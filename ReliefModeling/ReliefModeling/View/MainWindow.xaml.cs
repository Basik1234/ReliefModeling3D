﻿using System.Windows;

using ReliefModeling.ViewModel;

namespace ReliefModeling.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //TODO сделать растягивалки как в unity
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void MainWindow_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                ControlView3DObject.Child = vm.View3D;
            }
        }
    }
}