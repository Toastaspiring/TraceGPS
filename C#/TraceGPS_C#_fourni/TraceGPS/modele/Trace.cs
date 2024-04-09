// Projet TraceGPS
// fichier : modele/Trace.cs
// Rôle : la classe Trace représente une trace au moyen d'une collection d'objets PointDeTrace
// Dernière mise à jour : 1/11/2021 par dp

using System;
using System.Collections;
using System.Windows.Forms;

namespace TraceGPS
{
    public class Trace
    {
        // membres privés -----------------------------------------------------------------------------

        private int _id;				        // l'identifiant de la trace
        private DateTime _dateHeureDebut;	    // date et heure de début (utilise un objet java.util.Date)
        private DateTime _dateHeureFin;		    // date et heure de fin (utilise un objet java.util.Date)
        private bool _terminee;		            // true si la trace est terminée, false sinon
        private int _idUtilisateur;		        // l'identifiant de l'utilisateur ayant créé la trace
        private double _distance;		        // la distance totale
        private ArrayList _lesPointsDeTrace;    // la collection d'objets PointDeTrace

        // Constructeurs ------------------------------------------------------------------------------
        // Constructeur sans paramètre
        public Trace()
        {
            _id = 0;
            _dateHeureDebut = DateTime.MinValue;
            _dateHeureFin = DateTime.MinValue;
            _terminee = false;
            _idUtilisateur = 0;
            _distance = 0;
            _lesPointsDeTrace = new ArrayList();
        }

        // Constructeur avec 5 paramètres
        public Trace(int unId, DateTime uneDateHeureDebut, DateTime uneDateHeureFin, bool terminee, int unIdUtilisateur)
        {
            _id = unId;
            _dateHeureDebut = uneDateHeureDebut;
            _dateHeureFin = uneDateHeureFin;
            _terminee = terminee;
            _idUtilisateur = unIdUtilisateur;
            _distance = 0;
            _lesPointsDeTrace = new ArrayList();
        }

        // Constructeur avec 6 paramètres
        public Trace(int unId, DateTime uneDateHeureDebut, DateTime uneDateHeureFin, bool terminee, int unIdUtilisateur, double uneDistance)
        {
            _id = unId;
            _dateHeureDebut = uneDateHeureDebut;
            _dateHeureFin = uneDateHeureFin;
            _terminee = terminee;
            _idUtilisateur = unIdUtilisateur;
            _distance = uneDistance;
            _lesPointsDeTrace = new ArrayList();
        }

        // Accesseurs ---------------------------------------------------------------------------------

        public int getId() { return _id; }
        public void setId(int unId) { this._id = unId; }

        public DateTime getDateHeureDebut()
        {
            if (_lesPointsDeTrace.Count == 0)
                return _dateHeureDebut;
            else
            {
                PointDeTrace premierPoint = (PointDeTrace)_lesPointsDeTrace[0];
                return premierPoint.getDateHeure();
            }
        }
        public void setDateHeureDebut(DateTime uneDateHeureDebut) { this._dateHeureDebut = uneDateHeureDebut; }

        public DateTime getDateHeureFin()
        {
            if (_lesPointsDeTrace.Count == 0)
                return _dateHeureFin;
            else
            {
                int positionDernierPoint = _lesPointsDeTrace.Count - 1;
                PointDeTrace dernierPoint = (PointDeTrace)_lesPointsDeTrace[positionDernierPoint];
                return dernierPoint.getDateHeure();
            }
        }
        public void setDateHeureFin(DateTime uneDateHeureFin) { this._dateHeureFin = uneDateHeureFin; }

        public bool getTerminee() { return _terminee; }
        public void setTerminee(bool terminee) { this._terminee = terminee; }

        public int getIdUtilisateur() { return _idUtilisateur; }
        public void setIdUtilisateur(int unIdUtilisateur) { this._idUtilisateur = unIdUtilisateur; }	

        public ArrayList getLesPointsDeTrace() { return _lesPointsDeTrace; }

        // Méthodes publiques -------------------------------------------------------------------------

        // Fournit le nombre de points de passage
        // retourne : le nombre de points de passage
        public int getNombrePoints()
        {
            return _lesPointsDeTrace.Count;
        }

        // Fournit le point central du parcours
        // retourne : un objet Point (ou null si collection vide)
        public Point getCentre()
        {
            if (_lesPointsDeTrace.Count == 0)
                return null;
            else
            {
                // au départ, les valeurs extrêmes sont celles du premier point
                PointDeTrace premierPoint = (PointDeTrace) _lesPointsDeTrace[0];
                double latMini = premierPoint.getLatitude();
                double latMaxi = premierPoint.getLatitude();
                double longMini = premierPoint.getLongitude();
                double longMaxi = premierPoint.getLongitude();
                // parcours des autres points (à partir de la position 1)
                for (int i = 1; i < _lesPointsDeTrace.Count; i++)
                {
                    PointDeTrace lePoint = (PointDeTrace) _lesPointsDeTrace[i];
                    if (lePoint.getLatitude() < latMini) latMini = lePoint.getLatitude();
                    if (lePoint.getLatitude() > latMaxi) latMaxi = lePoint.getLatitude();
                    if (lePoint.getLongitude() < longMini) longMini = lePoint.getLongitude();
                    if (lePoint.getLongitude() > longMaxi) longMaxi = lePoint.getLongitude();
                }
                double latCentre = (latMini + latMaxi) / 2;
                double longCentre = (longMini + longMaxi) / 2;
                Point leCentre = new Point(latCentre, longCentre, 0);
                return leCentre;
            }
        }

        // Fournit le dénivelé (en m) entre le point le plus bas et le point le plus haut du parcours
        // retourne : le dénivelé en mètres (ou 0 si la collection est vide)
        public double getDenivele()
        {
            if (_lesPointsDeTrace.Count == 0)
                return 0;
            else
            {
                // au départ, les valeurs extrêmes sont celles du premier point
                PointDeTrace premierPoint = (PointDeTrace) _lesPointsDeTrace[0];
                double altMini = premierPoint.getAltitude();
                double altMaxi = premierPoint.getAltitude();
                // parcours des autres points (à partir de la position 1)
                for (int i = 1; i < _lesPointsDeTrace.Count; i++)
                {
                    PointDeTrace lePoint = (PointDeTrace) _lesPointsDeTrace[i];
                    if (lePoint.getAltitude() < altMini) altMini = lePoint.getAltitude();
                    if (lePoint.getAltitude() > altMaxi) altMaxi = lePoint.getAltitude();
                }
                double denivele = altMaxi - altMini;
                return denivele;
            }
        }

        // Calcule la durée totale du parcours (en secondes) à partir des heures de passage
        // au premier et au dernier point de trace.
        // retourne : la durée en secondes (ou 0 si la collection est vide)
        //public long getDureeEnSecondes()
        //{
        //    if (_lesPointsDeTrace.Count == 0)
        //        return 0;
        //    else
        //    {
        //        PointDeTrace premierPoint = (PointDeTrace)_lesPointsDeTrace[0];
        //        int positionDernierPoint = _lesPointsDeTrace.Count - 1;
        //        PointDeTrace dernierPoint = (PointDeTrace)_lesPointsDeTrace[positionDernierPoint];
        //        TimeSpan  duree = dernierPoint.getDateHeure().Subtract(premierPoint.getDateHeure());
        //        return (long) duree.TotalSeconds ;										// en secondes
        //    }
        //}
        public long getDureeEnSecondes()
        {
            if (_lesPointsDeTrace.Count == 0)
                return 0;
            else
            {
                int positionDernierPoint = _lesPointsDeTrace.Count - 1;
                PointDeTrace dernierPoint = (PointDeTrace)_lesPointsDeTrace[positionDernierPoint];
                return dernierPoint.getTempsCumule();
            }
        }

        // Fournit la durée totale du parcours sous forme d'une chaine "hh:mm:ss"
        // retourne : la durée (ou "00:00:00" si la collection est vide)
        public String getDureeTotale()
        {
            long duree = getDureeEnSecondes();
            if (duree == 0)
            {
                return "00:00:00";
            }
            else
            {
                int heures = (int)(duree / 3600);
                int reste = (int)(duree % 3600);
                int minutes = (int)(reste / 60);
                int secondes = (int)(reste % 60);
                return heures.ToString("00") + ":" + minutes.ToString("00") + ":" + secondes.ToString("00");
            }
        }

        // Fournit la distance totale du parcours (en Km)
        // retourne : la distance totale du parcours (en Km)
        //public double getDistanceTotale()
        //{
        //    double distanceTotale = 0;
        //    // parcours de tous les couples de points
        //    for (int i = 0; i < _lesPointsDeTrace.Count - 1; i++)
        //    {
        //        PointDeTrace lePoint1 = (PointDeTrace)_lesPointsDeTrace[i];
        //        PointDeTrace lePoint2 = (PointDeTrace)_lesPointsDeTrace[i + 1];
        //        distanceTotale += Point.getDistance(lePoint1, lePoint2);
        //    }
        //    return distanceTotale;
        //}
        public double getDistanceTotale()
        {
            if (_lesPointsDeTrace.Count == 0)
                return 0;
            else
            {
                int positionDernierPoint = _lesPointsDeTrace.Count - 1;
                PointDeTrace dernierPoint = (PointDeTrace)_lesPointsDeTrace[positionDernierPoint];
                return dernierPoint.getDistanceCumulee();
            }
        }

        // Fournit le dénivelé positif (en m)
        // retourne : le dénivelé positif (en m)
        public double getDenivelePositif()
        {
            double denivele = 0;
            // parcours de tous les couples de points
            for (int i = 0; i < _lesPointsDeTrace.Count - 1; i+=1)
            {
                PointDeTrace lePoint1 = (PointDeTrace)_lesPointsDeTrace[i];
                PointDeTrace lePoint2 = (PointDeTrace)_lesPointsDeTrace[i + 1];
                // on teste si ça monte
                if ( lePoint2.getAltitude() > lePoint1.getAltitude() )
                    denivele += lePoint2.getAltitude() - lePoint1.getAltitude();
            }
            return denivele;
        }

        // Fournit le dénivelé négatif (en m)
        // retourne : le dénivelé négatif (en m)
        public double getDeniveleNegatif()
        {
            double denivele = 0;
            // parcours de tous les couples de points
            for (int i = 0; i < _lesPointsDeTrace.Count - 1; i+=1)
            {
                PointDeTrace lePoint1 = (PointDeTrace)_lesPointsDeTrace[i];
                PointDeTrace lePoint2 = (PointDeTrace)_lesPointsDeTrace[i + 1];
                // on teste si ça descend
                if (lePoint2.getAltitude() < lePoint1.getAltitude())
                    denivele += lePoint1.getAltitude() - lePoint2.getAltitude();
            }
            return denivele;
        }

        // Fournit la vitesse moyenne du parcours (en km/h)
        // retourne : la vitesse moyenne (ou 0 si la distance est nulle)
        public double getVitesseMoyenne()
        {
            if (getDistanceTotale() == 0)
                return 0;
            else
            {
                double vitesseEnKmH = getDistanceTotale() / ((double)getDureeEnSecondes() / 3600);
                return vitesseEnKmH;
            }
        }

        // Fournit une chaine contenant toutes les données de l'objet
        public String toString()
        {
            String msg = "";
            msg += "Id : \t\t\t" + getId() + "\n";
            msg += "Utilisateur : \t\t" + getIdUtilisateur() + "\n";
            if (getTerminee()) msg += "Terminée : \t\tOui \n"; else msg += "Terminée : \t\tNon \n";
            msg += "Nombre de points :\t" + getNombrePoints().ToString("00000") + "\n";
            if (getNombrePoints() > 0)
            {
                msg += "Heure de début :\t\t" + getDateHeureDebut().ToString("dd/MM/yyyy HH:mm:ss") + "\n";
                msg += "Heure de fin :\t\t" + getDateHeureFin().ToString("dd/MM/yyyy HH:mm:ss") + "\n";
                msg += "Durée totale :\t\t" + getDureeTotale() + "\n";
                msg += "Distance totale en Km :\t" + getDistanceTotale().ToString("000.00") + "\n";
                msg += "Dénivelé en m :\t\t" + getDenivele().ToString("0000.00") + "\n";
                msg += "Dénivelé positif en m :\t" + getDenivelePositif().ToString("0000.00") + "\n";
                msg += "Dénivelé négatif en m :\t" + getDeniveleNegatif().ToString("0000.00") + "\n";
                msg += "Vitesse moyenne en Km/h :\t" + getVitesseMoyenne().ToString("00.00") + "\n";
                msg += "Centre du parcours :\n";
                msg += "   - Latitude :\t\t" + getCentre().getLatitude().ToString("000.000") + "\n";
                msg += "   - Longitude :\t\t" + getCentre().getLongitude().ToString("000.000") + "\n";
                msg += "   - Altitude :\t\t" + getCentre().getAltitude().ToString("000.000") + "\n";
            }
            return msg;
        }

        // ajoute un objet PointDeTrace à la collection
        // parametre nouveauPoint : le point à ajouter
        public void ajouterPoint(PointDeTrace nouveauPoint)
        {
            if (_lesPointsDeTrace.Count == 0)
            {   // si premier point de la trace, mise à zéro des données cumulées et de la vitesse
                nouveauPoint.setTempsCumule(0);
                nouveauPoint.setDistanceCumulee(0);
                nouveauPoint.setVitesse(0);
            }
            else
            {   // si d'autres points existent déjà, on cumule à partir des valeurs du dernier point
                PointDeTrace dernierPoint = (PointDeTrace) _lesPointsDeTrace[_lesPointsDeTrace.Count-1];

                // calcul de la durée entre le dernier point et le nouveau point
                TimeSpan duree = nouveauPoint.getDateHeure().Subtract(dernierPoint.getDateHeure());
                nouveauPoint.setTempsCumule(dernierPoint.getTempsCumule() + (long) duree.TotalSeconds);

                // calcul de la distance entre le dernier point et le nouveau point
                double distance = Point.getDistance(dernierPoint, nouveauPoint);
                nouveauPoint.setDistanceCumulee(dernierPoint.getDistanceCumulee() + distance);

                // calcul de la vitesse entre le dernier point et le nouveau point
                double vitesse = distance / (double) duree.TotalHours;
                nouveauPoint.setVitesse(vitesse);
            }
            // ajout du point à la collection
            _lesPointsDeTrace.Add(nouveauPoint);
        }

        // vide la collection
        public void viderListePoints()
        {
            _lesPointsDeTrace.Clear();
        }

        // lisse les données (altitude surtout)
        // fournit un nouvel objet Trace avec les données lissées
        public Trace lisserDonnees()
        {
            Trace laTraceLissee = new Trace();
            for (int i = 0 ; i < _lesPointsDeTrace.Count ; i++)
            {
                PointDeTrace lePointCourant = (PointDeTrace)_lesPointsDeTrace[i];
                // clonage du point original
                PointDeTrace lePointAvecMoyenne = new PointDeTrace(lePointCourant);
                // correction de l'altitude : calcul de l'altitude moyenne des points n-2, n-1, n, n+1, n+2
                if (i > 1 && i < _lesPointsDeTrace.Count - 2)
                {
                    PointDeTrace lePointMoins2 = (PointDeTrace)_lesPointsDeTrace[i - 2];
                    PointDeTrace lePointMoins1 = (PointDeTrace)_lesPointsDeTrace[i - 1];
                    PointDeTrace lePointPlus1 = (PointDeTrace)_lesPointsDeTrace[i + 1];
                    PointDeTrace lePointPlus2 = (PointDeTrace)_lesPointsDeTrace[i + 2];
                    double altitudeMoyenne = (lePointMoins2.getAltitude() + lePointMoins1.getAltitude() + lePointCourant.getAltitude() + lePointPlus1.getAltitude() + lePointPlus2.getAltitude()) / 5;
                    lePointAvecMoyenne.setAltitude(altitudeMoyenne);
                }
                laTraceLissee.ajouterPoint(lePointAvecMoyenne);
            }
            return laTraceLissee;
        }

    } // fin de la classe
} // fin du namespace
