using SecourRapide237.MAUI.Models;

namespace SecourRapide237.MAUI.Services;

/// <summary>
/// Contenu des fiches de premiers secours. Tout est embarqué dans l'application :
/// aucune connexion n'est nécessaire.
/// </summary>
public static class Fiches
{
    private static List<Etape> E(params (string titre, string detail)[] etapes) =>
        etapes.Select((x, i) => new Etape { Numero = i + 1, Titre = x.titre, Detail = x.detail }).ToList();

    public static readonly IReadOnlyList<Fiche> Toutes = new List<Fiche>
    {
        // Le saignement a ses 2 pages dédiées (SaignementProtegerPage puis SaignementCompressionPage).
        new Fiche
        {
            Id = "saignement", Titre = "Saignement", Emoji = "🩸", Teinte = Color.FromArgb("#FFD5D5"),
            Categorie = "SAIGNEMENT ABONDANT", Accroche = "Arrêter le saignement",
            Intro = "Comprimer la plaie, c'est le geste qui sauve."
        },
        new Fiche
        {
            Id = "brulure", Titre = "Brûlure", Emoji = "🔥", Teinte = Color.FromArgb("#FFE3C8"),
            Categorie = "BRÛLURE", Accroche = "Refroidir, protéger, alerter",
            Intro = "Plus on refroidit tôt, moins la brûlure est profonde.",
            Etapes = E(
                ("Refroidissez tout de suite", "Passez la brûlure sous l'eau fraîche (pas glacée) pendant au moins 10 minutes, 20 si possible."),
                ("Retirez ce qui serre", "Enlevez bijoux et vêtements non collés à la peau, avant que la zone gonfle."),
                ("Protégez", "Couvrez avec un linge propre et humide ou un film alimentaire. Ne percez pas les cloques."),
                ("Faites alerter", "Appelez le 112 / 119 si la brûlure est plus grande que la paume de la victime, au visage, aux mains, ou si la victime est un enfant.")),
            Alerte = "N'appliquez jamais de beurre, d'huile ou de dentifrice sur une brûlure."
        },
        new Fiche
        {
            Id = "fracture", Titre = "Fracture", Emoji = "🦴", Teinte = Color.FromArgb("#C8E8FF"),
            Categorie = "FRACTURE", Accroche = "Ne bougez plus, immobilisez",
            Intro = "Douleur vive, déformation, impossibilité de bouger : pensez à la fracture.",
            Etapes = E(
                ("Ne déplacez pas la victime", "Laissez-la dans la position où elle se sent le mieux, surtout en cas de choc à la tête, au cou ou au dos."),
                ("Immobilisez le membre", "Soutenez-le tel qu'il est avec des vêtements ou des coussins, sans essayer de le redresser."),
                ("Couvrez et rassurez", "Gardez la victime au chaud et parlez-lui calmement. Ne lui donnez ni à boire ni à manger."),
                ("Faites alerter", "Appelez le 112 / 119 et décrivez la blessure. Si la peau est ouverte, couvrez la plaie avec un linge propre sans appuyer.")),
            Alerte = "Ne tentez jamais de remettre un os en place."
        },
        new Fiche
        {
            Id = "etouffement", Titre = "Étouffement", Emoji = "🫁", Teinte = Color.FromArgb("#E0D5FF"),
            Categorie = "ÉTOUFFEMENT", Accroche = "Claques dans le dos, puis compressions",
            Intro = "La personne ne peut plus parler, tousser ni respirer.",
            Etapes = E(
                ("Faites-la tousser", "Si elle tousse, encouragez-la à continuer. N'intervenez que si la toux devient inefficace."),
                ("5 claques dans le dos", "Penchez-la en avant et donnez 5 claques vigoureuses entre les omoplates avec le plat de la main."),
                ("5 compressions abdominales", "Placez-vous derrière elle, poing au-dessus du nombril, tirez d'un coup sec vers vous et vers le haut."),
                ("Alternez et alertez", "Alternez 5 claques et 5 compressions jusqu'à ce que l'objet sorte. Faites appeler le 112 / 119.")),
            Alerte = "Nourrisson ou femme enceinte : ne faites pas de compressions abdominales. Appelez les secours immédiatement."
        },
        new Fiche
        {
            Id = "inconscient", Titre = "Perte de connaissance", Emoji = "😴", Teinte = Color.FromArgb("#D5E0FF"),
            Categorie = "PERTE DE CONNAISSANCE", Accroche = "Vérifier la respiration",
            Intro = "La victime ne répond pas quand on lui parle ou qu'on la secoue doucement.",
            Etapes = E(
                ("Appelez à l'aide", "Faites appeler le 112 / 119 ou utilisez le bouton SOS de l'application."),
                ("Libérez les voies aériennes", "Basculez doucement sa tête en arrière en soulevant le menton."),
                ("Vérifiez la respiration", "Regardez, écoutez et sentez pendant 10 secondes si le ventre et la poitrine se soulèvent."),
                ("Elle respire : PLS", "Mettez-la sur le côté (position latérale de sécurité) et surveillez sa respiration jusqu'à l'arrivée des secours."),
                ("Elle ne respire pas", "Commencez les compressions thoraciques au centre de la poitrine, environ 100 à 120 par minute, sans vous arrêter.")),
            Alerte = "Ne donnez ni à boire ni à manger à une personne inconsciente."
        },
        new Fiche
        {
            Id = "accident", Titre = "Accident de la route", Emoji = "🚗", Teinte = Color.FromArgb("#FFD8A0"),
            Categorie = "ACCIDENT DE LA ROUTE", Accroche = "Protéger d'abord, secourir ensuite",
            Intro = "Un deuxième accident est le plus grand danger : protégez la zone avant tout.",
            Etapes = E(
                ("Protégez la zone", "Garez-vous en sécurité, allumez vos feux de détresse, enfilez un gilet et signalez le danger aux autres conducteurs."),
                ("Coupez les contacts", "Faites couper le moteur des véhicules accidentés. Interdisez de fumer."),
                ("Alertez", "Appelez le 112 / 119 : lieu précis (route, point de repère), nombre de victimes, leur état."),
                ("Ne déplacez pas les victimes", "Sauf danger immédiat (incendie). Parlez-leur pour les garder éveillées et surveillez leur respiration.")),
            Alerte = "Ne retirez pas le casque d'un motard conscient."
        },
    };

    public static Fiche? Trouver(string? id) => Toutes.FirstOrDefault(f => f.Id == id);
}
