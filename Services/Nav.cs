using SecourRapide237.MAUI.Pages;

namespace SecourRapide237.MAUI.Services;

/// <summary>Toute la navigation de l'application passe par ici.</summary>
public static class Nav
{
    /// <summary>Va sur une page principale (celles de la barre du bas). Vide la pile.</summary>
    public static Task GoTab(string route) => Shell.Current.GoToAsync($"//{route}");

    /// <summary>Ouvre une page empilée (avec bouton retour).</summary>
    public static Task Push(string route) => Shell.Current.GoToAsync(route);

    /// <summary>
    /// Retour. Si la page est empilée on la ferme ; sinon on va sur la page indiquée
    /// (ou sur l'Accueil par défaut).
    /// </summary>
    public static async Task BackAsync(string? routeParDefaut = null)
    {
        if (Shell.Current.Navigation.NavigationStack.Count > 1)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        await GoTab(string.IsNullOrWhiteSpace(routeParDefaut) ? "HomePage" : routeParDefaut);
    }

    /// <summary>Ouvre la fiche d'une situation (saignement = parcours en 2 pages).</summary>
    public static Task OpenFiche(string id) =>
        id == "saignement"
            ? Push(nameof(SaignementProtegerPage))
            : Push($"{nameof(FichePage)}?id={id}");
}
