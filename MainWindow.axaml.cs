// The namespace now correctly matches your x:Class value
namespace everything_avalonia;

using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;



// A simple class to hold our search result data
public class SearchResult
{
  public string FullPath { get; set; } = "";
  public string Name => System.IO.Path.GetFileName(FullPath);
}

// The class is partial and correctly matches the x:Class name "MainWindow"
public partial class MainWindow : Window
{
    private readonly TextBox _searchTextBox;
    private readonly ListBox _resultsListBox;

    public MainWindow()
    {
        // This will now work because the files are in sync
        InitializeComponent();

        _searchTextBox = this.FindControl<TextBox>("SearchTextBox")!;
        _resultsListBox = this.FindControl<ListBox>("ResultsListBox")!;
        var searchButton = this.FindControl<Button>("SearchButton")!;

        searchButton.Click += (s, e) => PerformSearch();
        _searchTextBox.KeyDown += (s, e) => 
        {
            if (e.Key == Key.Enter) PerformSearch();
        };
        _resultsListBox.DoubleTapped += ResultsListBox_DoubleTapped;

        this.Closing += (s, e) => EverythingSDK.Everything_CleanUp();
    }

    private void PerformSearch()
    {
        string query = _searchTextBox.Text ?? "";
        if (string.IsNullOrWhiteSpace(query))
        {
            _resultsListBox.ItemsSource = null;
            return;
        }

      EverythingSDK.Everything_SetSearchW(query);

        if (!EverythingSDK.Everything_QueryW(true))
        {
            Console.WriteLine("Everything query failed!");
            return;
        }

        int numResults = EverythingSDK.Everything_GetNumResults();
        var resultsList = new List<SearchResult>();
        StringBuilder pathBuilder = new StringBuilder(260);

        for (int i = 0; i < numResults; i++)
        {
            EverythingSDK.Everything_GetResultFullPathW(i, pathBuilder, pathBuilder.Capacity);
            resultsList.Add(new SearchResult { FullPath = pathBuilder.ToString() });
        }

        _resultsListBox.ItemsSource = resultsList;
    }
    
    private void ResultsListBox_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (_resultsListBox.SelectedItem is SearchResult selectedResult)
        {
            Process.Start("explorer.exe", $"/select,\"{selectedResult.FullPath}\"");
        }
    }
}