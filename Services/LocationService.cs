using System.Globalization;

namespace SecourRapide237.MAUI.Services;

public record PositionInfo(double Latitude, double Longitude, string Lieu, bool EnDirect)
{
    public string CoordonneesTexte =>
        $"{Format(Latitude, 'N', 'S')}, {Format(Longitude, 'E', 'W')}";

    public string LienCarte =>
        $"https://maps.google.com/?q={Latitude.ToString(CultureInfo.InvariantCulture)},{Longitude.ToString(CultureInfo.InvariantCulture)}";

    private static string Format(double valeur, char positif, char negatif) =>
        $"{Math.Abs(valeur).ToString("0.0000", CultureInfo.InvariantCulture)}°{(valeur >= 0 ? positif : negatif)}";
}

public static class LocationService
{
    /// <summary>
    /// Position GPS actuelle. Si le GPS est indisponible, renvoie la dernière position
    /// connue (EnDirect = false), ou null s'il n'y en a aucune.
    /// </summary>
    public static async Task<PositionInfo?> ObtenirAsync()
    {
        try
        {
            var statut = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (statut != PermissionStatus.Granted)
                statut = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (statut == PermissionStatus.Granted)
            {
                var loc = await Geolocation.Default.GetLocationAsync(
                    new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10)));
                loc ??= await Geolocation.Default.GetLastKnownLocationAsync();

                if (loc != null)
                {
                    var lieu = await NomDuLieuAsync(loc.Latitude, loc.Longitude);
                    var position = new PositionInfo(loc.Latitude, loc.Longitude, lieu, true);
                    Sauvegarder(position);
                    return position;
                }
            }
        }
        catch (Exception)
        {
            // GPS désactivé, non supporté (PC), délai dépassé... on retombe sur la dernière position.
        }

        return DerniereConnue();
    }

    private static async Task<string> NomDuLieuAsync(double latitude, double longitude)
    {
        try
        {
            var lieux = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);
            var lieu = lieux?.FirstOrDefault();
            if (lieu != null)
            {
                var texte = string.Join(", ",
                    new[] { lieu.Locality ?? lieu.SubLocality, lieu.AdminArea }
                        .Where(s => !string.IsNullOrWhiteSpace(s)));
                if (texte.Length > 0)
                    return texte;
            }
        }
        catch (Exception)
        {
            // Le nom du lieu demande internet : sans réseau on garde juste les coordonnées.
        }

        return "Position détectée";
    }

    private static void Sauvegarder(PositionInfo p)
    {
        Preferences.Default.Set("pos_lat", p.Latitude);
        Preferences.Default.Set("pos_lon", p.Longitude);
        Preferences.Default.Set("pos_lieu", p.Lieu);
    }

    private static PositionInfo? DerniereConnue()
    {
        if (!Preferences.Default.ContainsKey("pos_lat"))
            return null;

        return new PositionInfo(
            Preferences.Default.Get("pos_lat", 0d),
            Preferences.Default.Get("pos_lon", 0d),
            Preferences.Default.Get("pos_lieu", "Dernière position connue"),
            false);
    }
}
