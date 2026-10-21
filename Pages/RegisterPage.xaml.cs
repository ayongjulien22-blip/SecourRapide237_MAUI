using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnCreateClicked(object? sender, EventArgs e)
    {
        var nom = NomEntry.Text?.Trim() ?? string.Empty;
        var telephone = Session.Normaliser(PhoneEntry.Text ?? string.Empty);
        var motDePasse = PasswordEntry.Text ?? string.Empty;
        var confirmation = ConfirmEntry.Text ?? string.Empty;

        if (nom.Length == 0)
        {
            await DisplayAlert("Nom manquant", "Entrez votre prénom et votre nom.", "OK");
            return;
        }

        if (telephone.Count(char.IsDigit) < 8)
        {
            await DisplayAlert("Numéro invalide", "Entrez un numéro de téléphone valide (ex. +237 6 50 12 34 56).", "OK");
            return;
        }

        if (motDePasse.Length < 6)
        {
            await DisplayAlert("Mot de passe trop court", "Le mot de passe doit contenir au moins 6 caractères.", "OK");
            return;
        }

        if (motDePasse != confirmation)
        {
            await DisplayAlert("Mots de passe différents", "Les deux mots de passe ne sont pas identiques.", "OK");
            return;
        }

        Session.Enregistrer(nom, telephone, motDePasse);
        Preferences.Default.Set("position_autorisee", PositionCheck.IsChecked);

        await Nav.GoTab("HomePage");
    }

    private async void OnGuestClicked(object? sender, EventArgs e)
    {
        Session.Ouvrir(invite: true);
        await Nav.GoTab("HomePage");
    }

    private async void OnGoLogin(object? sender, EventArgs e) => await Nav.BackAsync("LoginPage");
}
