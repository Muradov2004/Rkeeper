using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Rkeeper.Stores;
using Rkeeper.View.LoginRegisterView;
using Rkeeper.ViewModel.Command;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

internal class AdminVM : BaseVM
{
    private readonly NavigationStore _navigation;

    public ICommand? AddFoodToMenuCommand { get; set; }
    public ICommand? TableCommand { get; set; }
    public ICommand? LogFileCommand { get; set; }
    public ICommand? LogoutCommand { get; set; }
    public ICommand? DownloadPDFCommand { get; set; }

    public AdminVM(NavigationStore navigation)
    {
        _navigation = navigation;
        AddFoodToMenuCommand = new RelayCommand(ExecuteAddFoodToMenuCommand);
        TableCommand = new RelayCommand(ExecuteTableCommand);
        LogFileCommand = new RelayCommand(ExecuteLogFileCommand);
        LogoutCommand = new RelayCommand(ExecuteLogoutCommand);
        DownloadPDFCommand = new RelayCommand(ExecuteDownloadPDFCommand);
    }

    private void ExecuteDownloadPDFCommand(object? obj)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\History.pdf";
        string txtPath = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"Resources\History.txt";
        string history = File.ReadAllText(txtPath);

        string[] lines = history.Split('\n');

        int linesPerPage = 55;

        PdfDocument document = new();
        int lineIndex = 0;

        while (lineIndex < lines.Length)
        {
            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new("Arial", 12);
            XBrush brush = XBrushes.Black;

            double currentY = 20;

            for (int i = 0; i < linesPerPage && lineIndex < lines.Length; i++)
            {
                gfx.DrawString(lines[lineIndex], font, brush, 10, currentY);
                currentY += 15;
                lineIndex++;
            }
        }

        document.Save(path);
    }

    private void ExecuteAddFoodToMenuCommand(object? obj)
    {
        _navigation.CurrentVM = new MenuFoodVM(_navigation);
    }
    private void ExecuteTableCommand(object? obj)
    {
        _navigation.CurrentVM = new AdminTableVM(_navigation);
    }

    private void ExecuteLogFileCommand(object? obj)
    {
        _navigation.CurrentVM = new LogfileVM(_navigation);
    }

    private void ExecuteLogoutCommand(object? obj)
    {
        NavigationStore navigationStore = new();

        navigationStore.CurrentVM = new LoginVM(navigationStore);

        LoginRegisterWindow loginRegisterWindow = new()
        {
            DataContext = new MainVM(navigationStore)
        };
        loginRegisterWindow.Show();

        var window = Window.GetWindow(obj as DependencyObject);

        window?.Close();
    }


}
