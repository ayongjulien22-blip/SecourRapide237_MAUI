namespace SecourRapide237.MAUI.Services;

public static class Urgence
{
    /// <summary>Ouvre le composeur téléphonique avec le numéro (l'utilisateur appuie sur "appeler").</summary>
    public static async Task AppelerAsync(Page page, string numero)
    {
        try
        {
            if (PhoneDialer.Default.IsSupported)
            {
                HistoriqueService.Ajouter("Appel aux secours", $"Numéro composé : {numero}");
                PhoneDialer.Default.Open(numero);
                return;
            }
        }
        catch (Exception)
        {
            // on affiche le message ci-dessous
        }

        await page.DisplayAlert("Appel impossible",
            $"Cet appareil ne peut pas passer d'appel. Composez le {numero} depuis un téléphone.", "OK");
    }
}
