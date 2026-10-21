using SecourRapide237.MAUI.Services;

namespace SecourRapide237.MAUI.Pages;

public partial class SMSPage : ContentPage
{
    private PositionInfo? _position;
    private readonly bool _pret;   // évite d'utiliser les contrôles pendant InitializeComponent

    public SMSPage()
    {
        InitializeComponent();
        _pret = true;
        ProcheEntry.Text = Preferences.Default.Get("proche_tel", string.Empty);
        MettreAJourApercu();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _position = await LocationService.ObtenirAsync();
        PositionAlerte.IsVisible = _position == null && PositionCheck.IsChecked;
        MettreAJourApercu();
    }

    private string ConstruireMessage()
    {
        var message = (MessageEditor.Text ?? string.Empty).Trim();

        if (PositionCheck.IsChecked && _position != null)
            message += $"\nMa position : {_position.CoordonneesTexte}\n{_position.LienCarte}";

        return message;
    }

    private void MettreAJourApercu()
    {
        if (!_pret)
            return;

        ApercuLabel.Text = ConstruireMessage();
    }

    private void OnMessageChanged(object? sender, TextChangedEventArgs e) => MettreAJourApercu();

    private void OnPositionChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (!_pret)
            return;

        PositionAlerte.IsVisible = _position == null && PositionCheck.IsChecked;
        MettreAJourApercu();
    }

    private async void OnPreparerSms(object? sender, EventArgs e)
    {
        var numero = Session.Normaliser(ProcheEntry.Text ?? string.Empty);
        var message = ConstruireMessage();

        if (numero.Count(char.IsDigit) < 8)
        {
            await DisplayAlert("Numéro manquant", "Entrez le numéro de la personne à prévenir.", "OK");
            return;
        }

        if (message.Length == 0)
        {
            await DisplayAlert("Message vide", "Écrivez un message avant de continuer.", "OK");
            return;
        }

        Preferences.Default.Set("proche_tel", numero);

        try
        {
            if (Sms.Default.IsComposeSupported)
            {
                HistoriqueService.Ajouter("SMS préparé", $"Message pour {numero}");
                await Sms.Default.ComposeAsync(new SmsMessage(message, numero));
                return;
            }
        }
        catch (Exception)
        {
            // on propose la copie ci-dessous
        }

        await Clipboard.Default.SetTextAsync(message);
        await DisplayAlert("SMS non disponible",
            "Cet appareil ne peut pas envoyer de SMS. Le message a été copié : collez-le dans votre messagerie.", "OK");
    }
}
