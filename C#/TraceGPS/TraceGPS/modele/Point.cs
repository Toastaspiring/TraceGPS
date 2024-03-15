// Projet TraceGPS
// fichier : modele/Point.cs
// Rôle : la classe Point représente un point géographique
// Dernière mise à jour : 7/9/2018 par dP CARTRON

using System;

namespace TraceGPS
{
    public class Point
    {
        // attributs protégés -------------------------------------------------------------------------

        protected double _latitude;   // la latitude du point (en degrés décimaux)
        protected double _longitude;  // la longitude du point (en degrés décimaux)
        protected double _altitude;   // l'altitude du point (en mètres)

        // Constructeurs ------------------------------------------------------------------------------

        // Constructeur sans paramètre
        public Point()
        {   _latitude = 0;
            _longitude = 0;
            _altitude = 0;
        }
        // Constructeur avec paramètres
        // paramètre uneLatitude : latitude du nouveau point (en degrés décimaux)
        // paramètre uneLongitude : longitude du nouveau point (en degrés décimaux)
        // paramètre uneAltitude : altitude du nouveau point (en mètres)
        public Point(double uneLatitude, double uneLongitude, double uneAltitude)
        {   _latitude = uneLatitude;
            _longitude = uneLongitude;
            _altitude = uneAltitude;
        }

        // Accesseurs ---------------------------------------------------------------------------------

        // Accesseur fournissant la latitude du point (en degrés décimaux)
        // retourne la latitude du point (en degrés décimaux)
        public double getLatitude()
        {   return _latitude;
        }
        // Mutateur pour modifier la latitude du point
        // paramètre uneLatitude : la nouvelle latitude du point (en degrés décimaux)
        public void setLatitude(double uneLatitude)
        {   this._latitude = uneLatitude;
        }
        // Accesseur fournissant la longitude du point (en degrés décimaux)
        // retourne la longitude du point (en degrés décimaux)
        public double getLongitude()
        {   return _longitude;
        }
        // Mutateur pour modifier la longitude du point
        // paramètre uneLatitude : la nouvelle longitude du point (en degrés décimaux)
        public void setLongitude(double uneLongitude)
        {   this._longitude = uneLongitude;
        }
        // Accesseur fournissant l'altitude du point (en mètres)
        // retourne l'altitude du point (en mètres)
        public double getAltitude()
        {   return _altitude;
        }
        // Mutateur pour modifier l'altitude du point
        // paramètre uneLatitude : la nouvelle altitude du point (en mètres)
        public void setAltitude(double uneAltitude)
        {   this._altitude = uneAltitude;
        }

        // Méthodes -----------------------------------------------------------------------------------

        // Fournit une chaine contenant toutes les données de l'objet
        public virtual String toString()
        {   String msg = "";
            msg += "Latitude :\t" + this._latitude.ToString("000.000") + "\n";
            msg += "Longitude :\t" + this._longitude.ToString("000.000") + "\n";
            msg += "Altitude :\t" + this._altitude.ToString("000.000") + "\n";
            return msg;
        }

        // Calcul de la distance (en Km) entre 2 points géographiques.<br>
        // Ce code est transposé du forum JavaScript suivant :<br>
        // www.clubic.com/forum/programmation/calcul-de-distance-entre-deux-coordonnees-gps-id178494-page1.html<br>
        // CETTE FONCTION EST A TESTER ABSOLUMENT<br>
        // (on pourra par exemple utiliser le site http://www.lexilogos.com/calcul_distances.htm)
        // 
        // paramètre latitude1  : latitude point 1 (en degrés décimaux)
        // paramètre longitude1 : longitude point 1 (en degrés décimaux)
        // paramètre latitude2  : latitude point 2 (en degrés décimaux)
        // paramètre longitude2 : longitude point 2 (en degrés décimaux)
        // retourne           : la distance (en Km) entre les 2 points
        private static double getDistanceBetween(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            if (latitude1 == latitude2 && longitude1 == longitude2) return 0;

            double a = Math.PI / 180;
            latitude1 = latitude1 * a;
            latitude2 = latitude2 * a;
            longitude1 = longitude1 * a;
            longitude2 = longitude2 * a;
            double t1 = Math.Sin(latitude1) * Math.Sin(latitude2);
            double t2 = Math.Cos(latitude1) * Math.Cos(latitude2);
            double t3 = Math.Cos(longitude1 - longitude2);
            double t4 = t2 * t3;
            double t5 = t1 + t4;
            double rad_dist = Math.Atan(-t5 / Math.Sqrt(-t5 * t5 + 1)) + 2 * Math.Atan(1);
            return (rad_dist * 3437.74677 * 1.1508) * 1.6093470878864446;
        }

        // Calcul de la distance (en Km) entre 2 points géographiques.
        // paramètre autrePoint : le deuxième point (le premier est l'instance courante)
        // retourne           : la distance (en Km) entre les 2 points
        public double getDistance(Point autrePoint)
        {
            double lat1 = this._latitude;
            double long1 = this._longitude;
            double lat2 = autrePoint._latitude;
            double long2 = autrePoint._longitude;
            double dist = getDistanceBetween(lat1, long1, lat2, long2);
            return dist;
        }

        // Calcul de la distance (en Km) entre 2 points géographiques.
        // paramètre point1 : le premier point
        // paramètre point2 : le second point
        // retourne       : la distance (en Km) entre les 2 points
        public static double getDistance(Point point1, Point point2)
        {
            double lat1 = point1._latitude;
            double long1 = point1._longitude;
            double lat2 = point2._latitude;
            double long2 = point2._longitude;
            double dist = getDistanceBetween(lat1, long1, lat2, long2);
            return dist;
        }

    } // fin de la classe
} // fin du namespace
