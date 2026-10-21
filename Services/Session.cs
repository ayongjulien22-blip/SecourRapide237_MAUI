using System.Security.Cryptography;
using System.Text;

namespace SecourRapide237.MAUI.Services;

/// <summary>
/// Compte local facultatif, stocké sur le téléphone. Il n'y a pas de serveur :
/// le compte sert surtout à personnaliser l'application.
/// </summary>
public static class Session
{
    public static string Nom => Preferences.Default.Get("nom", string.Empty);
    public static string Telephone => Preferences.Default.Get("telephone", string.Empty);
    public static bool ADesCompte => !string.IsNullOrEmpty(Preferences.Default.Get("mdp_hash", string.Empty));
    public static bool EstInvite => Preferences.Default.Get("invite", true);

    public static string NomAffiche => string.IsNullOrWhiteSpace(Nom) ? "Utilisateur invité" : Nom;

    public static string Normaliser(string telephone) =>
        new string(telephone.Where(c => char.IsDigit(c) || c == '+').ToArray());

    public static void Enregistrer(string nom, string telephone, string motDePasse)
    {
        Preferences.Default.Set("nom", nom.Trim());
        Preferences.Default.Set("telephone", Normaliser(telephone));
        Preferences.Default.Set("mdp_hash", Hacher(motDePasse));
        Preferences.Default.Set("invite", false);
    }

    public static bool Verifier(string telephone, string motDePasse) =>
        Normaliser(telephone) == Telephone &&
        Hacher(motDePasse) == Preferences.Default.Get("mdp_hash", string.Empty);

    public static void Ouvrir(bool invite) => Preferences.Default.Set("invite", invite);

    public static void Deconnecter() => Preferences.Default.Set("invite", true);

    private static string Hacher(string motDePasse) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("SecourRapide237|" + motDePasse)));
}
