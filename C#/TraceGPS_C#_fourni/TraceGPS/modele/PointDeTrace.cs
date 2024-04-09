// Projet TraceGPS
// fichier : modele/PointDeTrace.cs
// Rôle : la classe PointDeTrace représente un point de passage de la trace
// Elle hérite de la classe Point et y ajoute :
// - l'id de la trace
// - l'id du point (relatif à la trace)
// - l'heure de passage au point
// - le rythme cardiaque
// - le temps cumulé depuis le départ (en secondes)
// - la distance cumulée depuis le départ (en Km)
// - la vitesse instantanée, calculée entre le point précédent et le point actuel (en Km/h)
// Dernière mise à jour : 1/11/2021 par dp

using System;

namespace TraceGPS
{
    public class PointDeTrace : Point
    {

        // attributs privés -----------------------------------------------------------------------------	

        private int _idTrace;				// l'identifiant de la trace
        private int _id;					// l'identifiant relatif du point dans la trace
        private DateTime _dateHeure;        // l'heure de passage au point
        private int _rythmeCardio;          // le rythme cardiaque au point (en bpm)
        private long _tempsCumule;          // le temps cumulé depuis le départ (en secondes)
        private double _distanceCumulee;    // la distance cumulée depuis le départ (en Km)
        private double _vitesse;            // vitesse instantanée entre le point précédent et le point actuel (en Km/h)

        // Constructeurs ------------------------------------------------------------------------------

        // Constructeur sans paramètre
        public PointDeTrace()
            // appelle le constructeur de la classe mère
            : base()
        {
            // initialise les nouveaux champs
            _idTrace = 0;
            _id = 0;
            _dateHeure = DateTime.MinValue;
            _rythmeCardio = 0;
            _tempsCumule = 0;
            _distanceCumulee = 0;
            _vitesse = 0;
        }

        // Constructeur avec 4 paramètres
        // paramètre uneLatitude  : latitude du point (en degrés décimaux)
        // paramètre uneLongitude : longitude du point (en degrés décimaux)
        // paramètre uneAltitude  : altitude du point (en mètres)
        // paramètre uneDateHeure : heure de passage au point
        public PointDeTrace(double uneLatitude, double uneLongitude, double uneAltitude, DateTime uneDateHeure)
            // appelle le constructeur de la classe mère
            : base(uneLatitude, uneLongitude, uneAltitude)
        {
            // initialise les nouveaux champs
            _idTrace = 0;
            _id = 0;
            _dateHeure = uneDateHeure;
            _rythmeCardio = 0;
            _tempsCumule = 0;
            _distanceCumulee = 0;
            _vitesse = 0;
        }

        // Constructeur avec 5 paramètres
        // paramètre uneLatitude    : latitude du point (en degrés décimaux)
        // paramètre uneLongitude   : longitude du point (en degrés décimaux)
        // paramètre uneAltitude    : altitude du point (en mètres)
        // paramètre uneDateHeure   : heure de passage au point
        // paramètre unRythmeCardio : rythme cardiaque au passage au point
        public PointDeTrace(double uneLatitude, double uneLongitude, double uneAltitude, DateTime uneDateHeure, int unRythmeCardio)
            // appelle le constructeur de la classe mère
            : base(uneLatitude, uneLongitude, uneAltitude)
        {
            // initialise les nouveaux champs
            _idTrace = 0;
            _id = 0;
            _dateHeure = uneDateHeure;
            _rythmeCardio = unRythmeCardio;
            _tempsCumule = 0;
            _distanceCumulee = 0;
            _vitesse = 0;
        }

        // Constructeur avec 6 paramètres
        // paramètre idTrace      : l'identifiant de la trace
        // paramètre id           : l'identifiant relatif du point dans la trace
        // paramètre uneLatitude  : latitude du point (en degrés décimaux)
        // paramètre uneLongitude : longitude du point (en degrés décimaux)
        // paramètre uneAltitude  : altitude du point (en mètres)
        // paramètre uneDateHeure : heure de passage au point
        public PointDeTrace(int idTrace, int id, double uneLatitude, double uneLongitude, double uneAltitude, DateTime uneDateHeure)
            // appelle le constructeur de la classe mère
            : base(uneLatitude, uneLongitude, uneAltitude)
        {
            // initialise les nouveaux champs
            _idTrace = idTrace;
            _id = id;
            _dateHeure = uneDateHeure;
            _rythmeCardio = 0;
            _tempsCumule = 0;
            _distanceCumulee = 0;
            _vitesse = 0;
        }

        // Constructeur avec 7 paramètres
        // paramètre idTrace        : l'identifiant de la trace
        // paramètre id             : l'identifiant relatif du point dans la trace
        // paramètre uneLatitude    : latitude du point (en degrés décimaux)
        // paramètre uneLongitude   : longitude du point (en degrés décimaux)
        // paramètre uneAltitude    : altitude du point (en mètres)
        // paramètre uneDateHeure   : heure de passage au point
        // paramètre unRythmeCardio : rythme cardiaque au passage au point
        public PointDeTrace(int idTrace, int id, double uneLatitude, double uneLongitude, double uneAltitude, 
            DateTime uneDateHeure, int unRythmeCardio)
            // appelle le constructeur de la classe mère
            : base(uneLatitude, uneLongitude, uneAltitude)
        {
            // initialise les nouveaux champs
            _idTrace = idTrace;
            _id = id;
            _dateHeure = uneDateHeure;
            _rythmeCardio = unRythmeCardio;
            _tempsCumule = 0;
            _distanceCumulee = 0;
            _vitesse = 0;
        }

        // Constructeur avec 8 paramètres
        // paramètre uneLatitude  : latitude du point (en degrés décimaux)
        // paramètre uneLongitude : longitude du point (en degrés décimaux)
        // paramètre uneAltitude  : altitude du point (en mètres)
        // paramètre uneDateHeure : heure de passage au point
        // paramètre unRythmeCardio : rythme cardiaque au passage au point
        // paramètre unTempsCumule : temps cumulé depuis le départ(en secondes)
        // paramètre uneDistanceCumulee : distance cumulée depuis le départ (en Km)
        // paramètre uneVitesse : vitesse instantanée, calculée entre le point précédent et le point suivant (en Km/h)
        public PointDeTrace(double uneLatitude, double uneLongitude, double uneAltitude,
            DateTime uneDateHeure, int unRythmeCardio, long unTempsCumule, double uneDistanceCumulee, double uneVitesse)
            // appelle le constructeur de la classe mère
            : base(uneLatitude, uneLongitude, uneAltitude)
        {
            // initialise les nouveaux champs
            _idTrace = 0;
            _id = 0;
            _dateHeure = uneDateHeure;
            _rythmeCardio = unRythmeCardio;
            _tempsCumule = unTempsCumule;
            _distanceCumulee = uneDistanceCumulee;
            _vitesse = uneVitesse;
        }

        // Constructeur avec 10 paramètres
        // paramètre idTrace      : l'identifiant de la trace
        // paramètre id           : l'identifiant relatif du point dans la trace
        // paramètre uneLatitude  : latitude du point (en degrés décimaux)
        // paramètre uneLongitude : longitude du point (en degrés décimaux)
        // paramètre uneAltitude  : altitude du point (en mètres)
        // paramètre uneDateHeure : heure de passage au point
        // paramètre unRythmeCardio : rythme cardiaque au passage au point
        // paramètre unTempsCumule : temps cumulé depuis le départ(en secondes)
        // paramètre uneDistanceCumulee : distance cumulée depuis le départ (en Km)
        // paramètre uneVitesse : vitesse instantanée, calculée entre le point précédent et le point suivant (en Km/h)
        public PointDeTrace(int idTrace, int id, double uneLatitude, double uneLongitude, double uneAltitude,
            DateTime uneDateHeure, int unRythmeCardio, long unTempsCumule, double uneDistanceCumulee, double uneVitesse)
            // appelle le constructeur de la classe mère
            : base(uneLatitude, uneLongitude, uneAltitude)
        {
            // initialise les nouveaux champs
            _idTrace = idTrace;
            _id = id;
            _dateHeure = uneDateHeure;
            _rythmeCardio = unRythmeCardio;
            _tempsCumule = unTempsCumule;
            _distanceCumulee = uneDistanceCumulee;
            _vitesse = uneVitesse;
        }

        // Constructeur par recopie (ou clonage) d'un point existant
        // paramètre unPointExistant  : le point à cloner
        public PointDeTrace(PointDeTrace unPointExistant)
            // appelle le constructeur de la classe mère
            : base(unPointExistant.getLatitude(), unPointExistant.getLongitude(), unPointExistant.getAltitude())    
        {
            // initialise les nouveaux champs
            _idTrace = unPointExistant._idTrace;
            _id = unPointExistant._id;
            _dateHeure = unPointExistant.getDateHeure();
            _rythmeCardio = unPointExistant.getRythmeCardio();
            _tempsCumule = unPointExistant.getTempsCumule();
            _distanceCumulee = unPointExistant.getDistanceCumulee();
            _vitesse = unPointExistant.getVitesse();
        }

        // Accesseurs ---------------------------------------------------------------------------------

        // Accesseur fournissant l'identifiant de la trace
        // retourne l'identifiant de la trace
        public int getIdTrace()
        {   return _idTrace;
        }
        // Mutateur pour modifier l'identifiant de la trace
        // paramètre unIdTrace : le nouvel identifiant de la trace
        public void setIdTrace(int unIdTrace)
        {   this._idTrace = unIdTrace;
        }

        // Accesseur fournissant l'identifiant relatif du point dans la trace
        // retourne l'identifiant relatif du point dans la trace
        public int getId()
        {   return _id;
        }
        // Mutateur pour modifier l'identifiant relatif du point dans la trace
        // paramètre unId : le nouvel identifiant relatif du point dans la trace
        public void setId(int unId)
        {   this._id = unId;
        }

        // Accesseur fournissant l'heure de passage au point
        // retourne l'heure de passage au point
        public DateTime getDateHeure()
        {   return _dateHeure;
        }
        // Mutateur pour modifier l'heure de passage au point
        // paramètre uneLatitude : la nouvelle heure de passage au point
        public void setDateHeure(DateTime uneDateHeure)
        {   this._dateHeure = uneDateHeure;
        }

        // Accesseur fournissant le rythme cardiaque au point
        // retourne le rythme cardiaque au point
        public int getRythmeCardio()
        {   return _rythmeCardio;
        }
        // Mutateur pour modifier le rythme cardiaque au point
        // paramètre unRythmeCardio : le nouveau rythme cardiaque au point
        public void setRythmeCardio(int unRythmeCardio)
        {  this._rythmeCardio = unRythmeCardio;
        }

        // Accesseur fournissant le temps cumulé depuis le départ (en secondes)
        // retourne le temps cumulé depuis le départ (en secondes)
        public long getTempsCumule()
        {  return _tempsCumule;
        }
        // Mutateur pour modifier le temps cumulé depuis le départ (en secondes)
        // paramètre unTempsCumule : le temps cumulé depuis le départ (en secondes)
        public void setTempsCumule(long unTempsCumule)
        {  this._tempsCumule = unTempsCumule;
        }

        // Accesseur fournissant la distance cumulée depuis le départ (en Km)
        // retourne la distance cumulée depuis le départ (en Km)
        public double getDistanceCumulee()
        {   return _distanceCumulee;
        }
        // Mutateur pour modifier la distance cumulée depuis le départ (en Km)
        // paramètre uneDistanceCumulee : la distance cumulée depuis le départ (en Km)
        public void setDistanceCumulee(double uneDistanceCumulee)
        {   this._distanceCumulee = uneDistanceCumulee;
        }

        // Accesseur fournissant la vitesse instantanée, calculée entre le point précédent et le point suivant (en Km/h)
        // retourne la vitesse instantanée, calculée entre le point précédent et le point suivant (en Km/h)
        public double getVitesse()
        {   return _vitesse;
        }
        // Mutateur pour modifier la vitesse instantanée, calculée entre le point précédent et le point suivant (en Km/h)
        // paramètre uneVitesse : la vitesse instantanée, calculée entre le point précédent et le point suivant (en Km/h)
        public void setVitesse(double uneVitesse)
        {   this._vitesse = uneVitesse;
        }

        // Méthodes publiques -------------------------------------------------------------------------

        // Méthode fournissant le temps cumulé depuis le départ (sous la forme d'une chaine "00:00:00")
        // retourne le temps cumulé depuis le départ (sous la forme d'une chaine "00:00:00")
        public String getTempsCumuleEnChaine()
        {
            long duree = _tempsCumule;
            int heures = (int)(duree / 3600);
            int reste = (int)(duree % 3600);
            int minutes = (int)(reste / 60);
            int secondes = (int)(reste % 60);

            return heures.ToString("00") + ":" + minutes.ToString("00") + ":" + secondes.ToString("00");
        }

        // Fournit une chaine contenant toutes les données de l'objet
        public override String toString()
        {
            String msg = "";
            msg += "Id trace :\t" + _idTrace.ToString("0") + "\n";
            msg += "Id point :\t" + _id.ToString("0") + "\n";	
            msg += base.toString();
            msg += "Heure de passage :\t" + this._dateHeure.ToString("dd/MM/yyyy HH:mm:ss") + "\n";
            msg += "Rythme cardiaque :\t" + this._rythmeCardio.ToString("0") + "\n";
            msg += "Temps cumule (s) :\t" + this._tempsCumule.ToString("0") + "\n";
            msg += "Temps cumule (hh:mm:ss) :\t" + this.getTempsCumuleEnChaine() + "\n";
            msg += "Distance cumulée (Km) :\t" + this._distanceCumulee.ToString("000.000") + "\n";
            msg += "Vitesse (Km/h) :\t" + this._vitesse.ToString("000.000") + "\n";
            return msg;
        }

    } // fin de la classe
} // fin du namespace
