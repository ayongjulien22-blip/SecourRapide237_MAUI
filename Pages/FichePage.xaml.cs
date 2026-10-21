using SecourRapide237.MAUI.Models;
using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

// Page unique pour toutes les situations (brûlure, fracture, étouffement...).
// On y arrive avec : Nav.OpenFiche("brulure")  ->  FichePage?id=brulure
[QueryProperty(nameof(Id), "id")]
public partial class FichePage : ContentPage
{
    private Fiche? _fiche;

    public FichePage()
    {
        InitializeComponent();
    }

    public string Id
    {
        set => Charger(value);
    }

    private void Charger(string? id)
    {
        _fiche = Fiches.Trouver(id);
        if (_fiche == null)
            return;

        Entete.Title = _fiche.Titre;
        CategorieLabel.Text = _fiche.Categorie;
        AccrocheLabel.Text = _fiche.Accroche;
        IntroLabel.Text = _fiche.Intro;
        EmojiLabel.Text = _fiche.Emoji;
        Illustration.BackgroundColor = _fiche.Teinte;
        BindableLayout.SetItemsSource(ListeEtapes, _fiche.Etapes);

        AlerteCard.IsVisible = !string.IsNullOrWhiteSpace(_fiche.Alerte);
        AlerteLabel.Text = "⚠️  " + _fiche.Alerte;
    }

    private async void OnAppelerClicked(object? sender, EventArgs e) => await Urgence.AppelerAsync(this, "112");

    private async void OnTermineClicked(object? sender, EventArgs e)
    {
        if (_fiche != null)
            HistoriqueService.Ajouter(_fiche.Titre, "Geste de premiers secours effectué");

        await DisplayAlert("Enregistré", "Le geste a été ajouté à votre historique.", "OK");
        await Nav.GoTab("HomePage");
    }
}
