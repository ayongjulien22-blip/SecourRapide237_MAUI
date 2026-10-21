using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class SaignementCompressionPage : ContentPage
{
    public SaignementCompressionPage()
    {
        InitializeComponent();
    }

    private async void OnAppelerClicked(object? sender, EventArgs e) => await Urgence.AppelerAsync(this, "112");

    private async void OnTermineClicked(object? sender, EventArgs e)
    {
        HistoriqueService.Ajouter("Saignement", "Compression directe effectuée");
        await DisplayAlert("Enregistré", "Le geste a été ajouté à votre historique.", "OK");
        await Nav.GoTab("HomePage");
    }
}
