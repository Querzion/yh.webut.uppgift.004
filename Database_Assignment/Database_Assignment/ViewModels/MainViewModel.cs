using CommunityToolkit.Mvvm.ComponentModel;

namespace Database_Assignment.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";
}