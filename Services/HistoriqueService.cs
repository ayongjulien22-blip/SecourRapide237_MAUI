using System.Text.Json;
using System.Text.Json.Serialization;
using SecourRapide237.MAUI.Models;

namespace SecourRapide237.MAUI.Services;

[JsonSerializable(typeof(List<Intervention>))]
internal partial class HistoriqueJsonContext : JsonSerializerContext
{
}

/// <summary>Historique des actions, stocké sur le téléphone (Preferences).</summary>
public static class HistoriqueService
{
    private const string Cle = "historique";
    private const int Maximum = 50;

    public static List<Intervention> Charger()
    {
        var json = Preferences.Default.Get(Cle, string.Empty);
        if (string.IsNullOrWhiteSpace(json))
            return new List<Intervention>();

        try
        {
            return JsonSerializer.Deserialize(json, HistoriqueJsonContext.Default.ListIntervention)
                   ?? new List<Intervention>();
        }
        catch (Exception)
        {
            return new List<Intervention>();
        }
    }

    public static void Ajouter(string titre, string detail)
    {
        var liste = Charger();
        liste.Insert(0, new Intervention { Date = DateTime.Now, Titre = titre, Detail = detail });

        if (liste.Count > Maximum)
            liste.RemoveRange(Maximum, liste.Count - Maximum);

        Preferences.Default.Set(Cle, JsonSerializer.Serialize(liste, HistoriqueJsonContext.Default.ListIntervention));
    }

    public static void Vider() => Preferences.Default.Remove(Cle);
}
