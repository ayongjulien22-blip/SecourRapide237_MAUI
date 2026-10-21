using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var enLigne = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        StatutLabel.Text = enLigne ? "En ligne · guides disponibles hors ligne" : "Mode hors ligne actif";
    }

    // Chaque carte de situation envoie son identifiant dans CommandParameter.
    private async void OnCategorie(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is string id)
            await Nav.OpenFiche(id);
    }

    private async void OnSos(object? sender, EventArgs e) => await Nav.GoTab("SOSPage");

    private async void OnGuide(object? sender, EventArgs e) => await Nav.GoTab("GuidePage");

    private async void OnProfil(object? sender, EventArgs e) => await Nav.GoTab("ProfilPage");

    private async void OnNotifications(object? sender, EventArgs e) =>
        await DisplayAlert("Notifications", "Aucune notification pour le moment.", "OK");
}
