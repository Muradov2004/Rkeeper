using iTextSharp.text;
using iTextSharp.text.pdf;
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
        Document doc = new Document();
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
        string txtPath = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"Resources\History.txt";
        string history = File.ReadAllText(txtPath);
        doc.Open();
        Paragraph paragraph = new Paragraph(history);
        doc.Add(paragraph);
        doc.Close();
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

        LoginRegisterWindow loginRegisterWindow = new LoginRegisterWindow()
        {
            DataContext = new MainVM(navigationStore)
        };
        loginRegisterWindow.Show();

        var window = Window.GetWindow(obj as DependencyObject);

        window?.Close();
    }


}
