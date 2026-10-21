using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class SaignementProtegerPage : ContentPage
{
    public SaignementProtegerPage()
    {
        InitializeComponent();
    }

    private async void OnNextClicked(object? sender, EventArgs e) =>
        await Nav.Push(nameof(SaignementCompressionPage));
}
