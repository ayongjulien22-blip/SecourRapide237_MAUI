using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        var telephone = PhoneEntry.Text?.Trim() ?? string.Empty;
        var motDePasse = PasswordEntry.Text ?? string.Empty;

        if (telephone.Length == 0 || motDePasse.Length == 0)
        {
            await DisplayAlert("Champs requis", "Entrez votre numéro de téléphone et votre mot de passe.", "OK");
            return;
        }

        if (!Session.ADesCompte)
        {
            await DisplayAlert("Aucun compte",
                "Aucun compte n'existe encore sur cet appareil. Créez-en un (facultatif) ou continuez sans compte.", "OK");
            return;
        }

        if (!Session.Verifier(telephone, motDePasse))
        {
            await DisplayAlert("Identifiants incorrects", "Numéro de téléphone ou mot de passe incorrect.", "OK");
            return;
        }

        Session.Ouvrir(invite: false);
        await Nav.GoTab("HomePage");
    }

    private async void OnGuestClicked(object? sender, EventArgs e)
    {
        Session.Ouvrir(invite: true);
        await Nav.GoTab("HomePage");
    }

    private async void OnRegisterClicked(object? sender, EventArgs e) =>
        await Nav.Push(nameof(RegisterPage));

    private void OnTogglePassword(object? sender, EventArgs e) =>
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;

    private async void OnForgotPassword(object? sender, EventArgs e) =>
        await DisplayAlert("Mot de passe oublié",
            "Le compte est enregistré uniquement sur cet appareil. Vous pouvez créer un nouveau compte, ou continuer sans compte.", "OK");
}
