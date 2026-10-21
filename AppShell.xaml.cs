using SecourRapide237.MAUI.Pages;

namespace SecourRapide237.MAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Pages "empilées" : on y arrive avec GoToAsync(nomDeLaPage) et on en sort avec ".."
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(SaignementProtegerPage), typeof(SaignementProtegerPage));
        Routing.RegisterRoute(nameof(SaignementCompressionPage), typeof(SaignementCompressionPage));
        Routing.RegisterRoute(nameof(FichePage), typeof(FichePage));
    }

    // Bouton retour physique d'Android (et geste retour) :
    //  - page empilée      -> comportement normal (revient à la page précédente)
    //  - SMS / Hors ligne  -> revient à SOS / Guide
    //  - autre page        -> revient à l'Accueil
    //  - Accueil / Connexion -> quitte l'application
    protected override bool OnBackButtonPressed()
    {
        if (Navigation.NavigationStack.Count > 1)
            return base.OnBackButtonPressed();

        var lieu = CurrentState?.Location?.OriginalString ?? string.Empty;

        if (lieu.Contains("SMSPage")) { _ = GoToAsync("//SOSPage"); return true; }
        if (lieu.Contains("HorsLignePage")) { _ = GoToAsync("//GuidePage"); return true; }

        if (!lieu.Contains("HomePage") && !lieu.Contains("LoginPage"))
        {
            _ = GoToAsync("//HomePage");
            return true;
        }

        return base.OnBackButtonPressed();
    }
}
