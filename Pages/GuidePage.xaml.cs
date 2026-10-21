using SecourRapide237.MAUI.Models;
using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class GuidePage : ContentPage
{
    public GuidePage()
    {
        InitializeComponent();
        BindableLayout.SetItemsSource(ListeFiches, Fiches.Toutes);
    }

    private async void OnFicheTapped(object? sender, EventArgs e)
    {
        if (sender is BindableObject vue && vue.BindingContext is Fiche fiche)
            await Nav.OpenFiche(fiche.Id);
    }

    private async void OnHorsLigneTapped(object? sender, EventArgs e) => await Nav.GoTab("HorsLignePage");
}
