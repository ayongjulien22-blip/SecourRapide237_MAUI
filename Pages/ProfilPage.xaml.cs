using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class ProfilPage : ContentPage
{
    public ProfilPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        NomLabel.Text = Session.EstInvite ? "Utilisateur invité" : Session.NomAffiche;
        TelLabel.Text = string.IsNullOrEmpty(Session.Telephone) || Session.EstInvite ? "Non renseigné" : Session.Telephone;
        CompteLabel.Text = Session.EstInvite ? "Sans compte" : "Compte local";
        ModeLabel.Text = Connectivity.Current.NetworkAccess == NetworkAccess.Internet ? "En ligne" : "Hors ligne actif";
        VersionLabel.Text = AppInfo.Current.VersionString;
        ConnexionButton.Text = Session.EstInvite ? "Se connecter" : "Se déconnecter";
    }

    private async void OnHorsLigne(object? sender, EventArgs e) => await Nav.GoTab("HorsLignePage");

    private async void OnDeconnexion(object? sender, EventArgs e)
    {
        Session.Deconnecter();
        await Nav.GoTab("LoginPage");
    }
}
