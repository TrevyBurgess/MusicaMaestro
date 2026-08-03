using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using CyberFeedForward.MusicaMaestro.Views;
using Windows.System;

namespace CyberFeedForward.MusicaMaestro;

public sealed partial class MainWindow : Window
{
    private const double MinOpenPaneLength = 120.0;
    private const double MaxOpenPaneLength = 600.0;

    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
        AppWindow.SetIcon("Assets/AppIcon.ico");

        PaneResizer.Visibility = NavView.IsPaneOpen ? Visibility.Visible : Visibility.Collapsed;
        PaneResizer.Margin = new Thickness(NavView.OpenPaneLength - PaneResizer.Width, 0, 0, 0);

        NavView.RegisterPropertyChangedCallback(NavigationView.IsPaneOpenProperty, OnNavViewIsPaneOpenChanged);
        NavView.RegisterPropertyChangedCallback(NavigationView.OpenPaneLengthProperty, OnNavViewOpenPaneLengthChanged);
    }

    private void TitleBar_PaneToggleRequested(TitleBar _, object __)
    {
        NavView.IsPaneOpen = !NavView.IsPaneOpen;
    }

    private void TitleBar_BackRequested(TitleBar _, object __)
    {
        NavFrame.GoBack();
    }

    private void NavView_SelectionChanged(NavigationView _, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            NavFrame.Navigate(typeof(SettingsView));
        }
        else if (args.SelectedItem is NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "home":
                    NavFrame.Navigate(typeof(HomeView));
                    break;
                case "library":
                    NavFrame.Navigate(typeof(LibraryView));
                    break;
                case "editor":
                    NavFrame.Navigate(typeof(EditorView));
                    break;
                case "about":
                    NavFrame.Navigate(typeof(AboutView));
                    break;
                default:
                    throw new InvalidOperationException($"Unknown navigation item tag: {item.Tag}");
            }
        }
    }

    private void OnNavViewIsPaneOpenChanged(DependencyObject sender, DependencyProperty dp)
    {
        PaneResizer.Visibility = NavView.IsPaneOpen ? Visibility.Visible : Visibility.Collapsed;
    }

    private void OnNavViewOpenPaneLengthChanged(DependencyObject sender, DependencyProperty dp)
    {
        PaneResizer.Margin = new Thickness(NavView.OpenPaneLength - PaneResizer.Width, 0, 0, 0);
    }

    private void PaneResizer_DragDelta(object _, DragDeltaEventArgs e)
    {
        if (!NavView.IsPaneOpen)
        {
            return;
        }

        double newWidth = NavView.OpenPaneLength + e.HorizontalChange;
        NavView.OpenPaneLength = Math.Clamp(newWidth, MinOpenPaneLength, MaxOpenPaneLength);
    }

    private void PaneResizer_KeyDown(object _, KeyRoutedEventArgs e)
    {
        if (!NavView.IsPaneOpen)
        {
            return;
        }

        double delta = e.Key switch
        {
            VirtualKey.Left => -10.0,
            VirtualKey.Right => 10.0,
            _ => 0.0
        };

        if (delta == 0.0)
        {
            return;
        }

        double newWidth = NavView.OpenPaneLength + delta;
        NavView.OpenPaneLength = Math.Clamp(newWidth, MinOpenPaneLength, MaxOpenPaneLength);
        e.Handled = true;
    }
}
