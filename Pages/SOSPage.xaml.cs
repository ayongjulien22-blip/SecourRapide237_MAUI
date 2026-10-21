using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class SOSPage : ContentPage
{
    private PositionInfo? _position;

    public SOSPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ActualiserPositionAsync();
    }

    private async Task ActualiserPositionAsync()
    {
        LieuLabel.Text = "Recherche de votre position…";
        CoordLabel.Text = string.Empty;
        StatutLabel.Text = string.Empty;

        _position = await LocationService.ObtenirAsync();

        if (_position == null)
        {
            LieuLabel.Text = "Position indisponible";
            CoordLabel.Text = "Activez le GPS et autorisez la localisation, puis touchez « Actualiser ».";
            return;
        }

        LieuLabel.Text = _position.Lieu;
        CoordLabel.Text = _position.CoordonneesTexte;
        StatutLabel.Text = _position.EnDirect
            ? "Position GPS actuelle"
            : "Dernière position connue (GPS indisponible)";
    }

    private async void OnAppeler112(object? sender, EventArgs e) => await Urgence.AppelerAsync(this, "112");

    private async void OnAppeler119(object? sender, EventArgs e) => await Urgence.AppelerAsync(this, "119");

    private async void OnActualiser(object? sender, EventArgs e) => await ActualiserPositionAsync();

    private async void OnCopier(object? sender, EventArgs e)
    {
        if (_position == null)
        {
            await DisplayAlert("Position indisponible", "Aucune position à copier pour le moment.", "OK");
            return;
        }

        await Clipboard.Default.SetTextAsync($"{_position.CoordonneesTexte} {_position.LienCarte}");
        await DisplayAlert("Copié", "Votre position a été copiée.", "OK");
    }

    private async void OnSms(object? sender, EventArgs e) => await Nav.GoTab("SMSPage");
}
