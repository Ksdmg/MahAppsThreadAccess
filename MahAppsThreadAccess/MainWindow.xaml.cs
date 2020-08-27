using ControlzEx.Theming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace MahAppsThreadAccess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread childThread = new Thread(x =>
           {
               ChildWindow child = new ChildWindow();
               child.Closed += Child_Closed;
               child.Show();
               Dispatcher.Run();
                });
            childThread.SetApartmentState(ApartmentState.STA);
            childThread.IsBackground = true;
            childThread.Name = "ChildThread";
            childThread.Start();
        }

        private void Child_Closed(object sender, EventArgs e)
        {
            ((ChildWindow)sender).Closed -= Child_Closed;
            ((ChildWindow)sender).Dispatcher.InvokeShutdown();
        }
    }
}
