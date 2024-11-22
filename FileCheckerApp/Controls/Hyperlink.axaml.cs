using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System.Diagnostics;

namespace FileCheckerApp.Controls;

public partial class Hyperlink : UserControl
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<Hyperlink, string>(nameof(Text));

    public static readonly StyledProperty<string> UrlProperty =
        AvaloniaProperty.Register<Hyperlink, string>(nameof(Url));

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Url
    {
        get => GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }

    public Hyperlink()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void OnLinkClicked(object? sender, PointerPressedEventArgs e)
    {
        if (!string.IsNullOrEmpty(Url))
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Url,
                    UseShellExecute = true
                });
            }
            catch
            {
                // Maneja errores si no se puede abrir el enlace
            }
        }
    }
}