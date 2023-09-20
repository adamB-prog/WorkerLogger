using System.Windows;
using WorkerLogger.Domain.Entities.Authentication;
using WorkerLogger.WPFClient.ViewModels;

namespace WorkerLogger.WPFClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(TokenModel model)
    {

        InitializeComponent();
        (this.DataContext as MainWindowViewModel)?.SetToken(model);
    }
}
