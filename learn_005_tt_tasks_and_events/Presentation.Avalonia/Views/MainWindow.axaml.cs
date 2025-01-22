using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Newtonsoft.Json;

namespace Presentation.Avalonia.Views;

public partial class MainWindow : Window
{
    private readonly string _url =
        "https://downloads.raspberrypi.com/raspios_oldstable_lite_arm64/images/raspios_oldstable_lite_arm64-2024-10-28/2024-10-22-raspios-bullseye-arm64-lite.img.xz";

    private readonly string _localPath = @"/home/querzion/Downloads/2024-10-22-raspios-bullseye-arm64-lite.img.xz";
    
    public MainWindow()
    {
        InitializeComponent();
        
        // Part 3
        Task.Run(async () =>
        {
            Debug.WriteLine($"CURRENT TASK: {Task.CurrentId}, CURRENT THREAD: {Environment.CurrentManagedThreadId}");
            var result = await GetCustomersAsync();

            // To fix the issue with the result not showing up; use a dispatcher to drag a task to the main thread.
            // WPF Dispatcher.Invoke(() => x);
            // Avalonia >>
            Dispatcher.UIThread.Invoke(() => StatusMessages.ItemsSource = result);
        });
    }
    

    // Part 1
    // private void Btn_Execute_Click(object? sender, RoutedEventArgs e)
    // {
    //     StatusMessages.Items.Clear();
    //     StatusMessages.Items.Add("Downloading ...");
    //     Download();
    //     StatusMessages.Items.Add("Download finished.");
    // }
    
    // Part 2 & 3
    private async void Btn_Execute_Click(object? sender, RoutedEventArgs e)
    {
        // // Part 2
        // StatusMessages.Items.Clear();
        // StatusMessages.Items.Add("Downloading ...");
        // await DownloadAsync();
        // StatusMessages.Items.Add("Download finished.");
        
        // // Part 3
        // StatusMessages.ItemsSource = null!;
        StatusMessages.ItemsSource = await GetCustomersAsync();
    }
    
    // Part 1
    // Synchronious (Sequential) (This should not be used out of several reasons,
    // one is that locks up the main process, second is that it's deprecated.)
    // private void Download()
    // {
    //     using var client = new WebClient();
    //     client.DownloadFile(_url, _localPath);
    // }
    
    // Part 2
    // Asynchronius (Paralell) (This is the method that should be used.)
    // private async Task DownloadAsync()
    // {
    //     using var client = new HttpClient();
    //     client.Timeout = TimeSpan.FromMinutes(10);
    //     
    //     var response = await client.GetAsync(_url);
    //
    //     using var fs = new FileStream(_localPath, FileMode.CreateNew);
    //     await response.Content.CopyToAsync(fs);
    // }

    private void Btn_Reset_Click(object? sender, RoutedEventArgs e)
    {
        // // Part 1 & 2
        // StatusMessages.ItemsSource = null!;
        //
        // if(File.Exists(_localPath))
        //     File.Exists(_localPath);
        
        // // Part 3
    }
    
    // Part 3
    private async Task<IEnumerable<string>> GetCustomersAsync()
    {
        using var client = new HttpClient();
        // Get a webapi that doesn't exist, so this will not work until a webapi is created.
        var response = await client.GetAsync("https://localhost:7129/api/customers");
        
        return JsonConvert.DeserializeObject<IEnumerable<string>>(await response.Content.ReadAsStringAsync())!;
    }
}