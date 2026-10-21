using SecourRapide237.MAUI.Models;
using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class HorsLignePage : ContentPage
{
    public HorsLignePage()
    {
        InitializeComponent();
        BindableLayout.SetItemsSource(ListeFiches, Fiches.Toutes);
        NombreLabel.Text = $"✓  {Fiches.Toutes.Count} guides enregistrés";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ReseauLabel.Text = Connectivity.Current.NetworkAccess == NetworkAccess.Internet
            ? "Vous êtes en ligne. Rien à télécharger : tout est déjà dans l'application."
            : "Vous êtes hors ligne. Tous les guides restent utilisables.";
    }

    private async void OnFicheTapped(object? sender, EventArgs e)
    {
        if (sender is BindableObject vue && vue.BindingContext is Fiche fiche)
            await Nav.OpenFiche(fiche.Id);
    }

    private async void OnGerer(object? sender, EventArgs e) =>
        await DisplayAlert("Téléchargements",
            "Les guides sont intégrés à l'application : il n'y a rien à télécharger, ils sont toujours disponibles sans internet.", "OK");
}
