using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using CyberFeedForward.MusicaMaestro.Models;
using CyberFeedForward.MusicaMaestro.Views;
using Windows.Graphics;
using Windows.System;

namespace CyberFeedForward.MusicaMaestro;

public sealed partial class MainWindow : Window
{
    private const double MinOpenPaneLength = 120.0;
    private const double MaxOpenPaneLength = 600.0;
    private readonly SettingsModel _settings;
    private bool _hasRestoredPosition;

    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
        AppWindow.SetIcon("Assets/AppIcon.ico");

        var f11Accelerator = new KeyboardAccelerator { Key = VirtualKey.F11 };
        f11Accelerator.Invoked += OnFullScreenAcceleratorInvoked;
        Content.KeyboardAccelerators.Add(f11Accelerator);

        _settings = new SettingsModel();
        NavView.OpenPaneLength = _settings.NavigationPaneWidth;
        NavView.IsPaneOpen = _settings.IsNavigationPaneOpen;

        PaneResizer.Visibility = NavView.IsPaneOpen ? Visibility.Visible : Visibility.Collapsed;
        PaneResizer.Margin = new Thickness(NavView.OpenPaneLength - PaneResizer.Width, 0, 0, 0);

        NavView.RegisterPropertyChangedCallback(NavigationView.IsPaneOpenProperty, OnNavViewIsPaneOpenChanged);
        NavView.RegisterPropertyChangedCallback(NavigationView.OpenPaneLengthProperty, OnNavViewOpenPaneLengthChanged);

        Activated += OnActivated;
        Closed += OnClosed;
    }

    private void OnActivated(object _, WindowActivatedEventArgs __)
    {
        if (_hasRestoredPosition)
        {
            return;
        }

        _hasRestoredPosition = true;

        if (_settings.MainWindowWidth > 0 && _settings.MainWindowHeight > 0)
        {
            AppWindow.Move(new PointInt32(_settings.MainWindowX, _settings.MainWindowY));
            AppWindow.Resize(new SizeInt32(_settings.MainWindowWidth, _settings.MainWindowHeight));
        }
    }

    private void OnClosed(object _, WindowEventArgs __)
    {
        _settings.MainWindowX = AppWindow.Position.X;
        _settings.MainWindowY = AppWindow.Position.Y;
        _settings.MainWindowWidth = AppWindow.Size.Width;
        _settings.MainWindowHeight = AppWindow.Size.Height;
    }

    private void TitleBar_PaneToggleRequested(TitleBar _, object __)
    {
        NavView.IsPaneOpen = !NavView.IsPaneOpen;
        _settings.IsNavigationPaneOpen = NavView.IsPaneOpen;
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
                case "musicsynthesis":
                    NavFrame.Navigate(typeof(MusicSynthesisView));
                    break;
                case "help":
                    NavFrame.Navigate(typeof(HelpView));
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
        _settings.NavigationPaneWidth = NavView.OpenPaneLength;
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

    private void OnFullScreenAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        if (AppWindow.Presenter.Kind == AppWindowPresenterKind.FullScreen)
        {
            AppWindow.SetPresenter(AppWindowPresenterKind.Default);
        }
        else
        {
            AppWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
        }

        args.Handled = true;
    }
}
