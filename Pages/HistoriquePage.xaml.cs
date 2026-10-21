using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class HistoriquePage : ContentPage
{
    public HistoriquePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Actualiser();
    }

    private void Actualiser()
    {
        var liste = HistoriqueService.Charger();
        BindableLayout.SetItemsSource(ListeInterventions, liste);

        VideLabel.IsVisible = liste.Count == 0;
        EffacerButton.IsVisible = liste.Count > 0;
    }

    private async void OnEffacer(object? sender, EventArgs e)
    {
        var ok = await DisplayAlert("Effacer l'historique", "Supprimer toutes les interventions enregistrées ?", "Effacer", "Annuler");
        if (!ok)
            return;

        HistoriqueService.Vider();
        Actualiser();
    }
}
