using SimpleCalculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleCalculator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = (MainWindowViewModel)DataContext;
            Left = ViewModel.Settings.MainWindowStatus.Left;
            Top = ViewModel.Settings.MainWindowStatus.Top;
            Width = ViewModel.Settings.MainWindowStatus.Width;
            Height = ViewModel.Settings.MainWindowStatus.Height;
        }
    }
}
