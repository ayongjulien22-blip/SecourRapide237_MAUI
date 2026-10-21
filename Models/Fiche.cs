namespace SecourRapide237.MAUI.Models;

public class Fiche
{
    public string Id { get; init; } = string.Empty;
    public string Titre { get; init; } = string.Empty;       // "Brûlure"
    public string Emoji { get; init; } = string.Empty;
    public Color Teinte { get; init; } = Colors.LightGray;   // fond de l'icône
    public string Categorie { get; init; } = string.Empty;   // petit texte rouge en capitales
    public string Accroche { get; init; } = string.Empty;   // grand titre de la fiche
    public string Intro { get; init; } = string.Empty;
    public List<Etape> Etapes { get; init; } = new();
    public string Alerte { get; init; } = string.Empty;      // encadré orange "attention"
}
