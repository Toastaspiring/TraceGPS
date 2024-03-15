package classes;

import java.util.ArrayList;
import java.util.Date;

// Cette classe représente une trace au moyen d'une collection d'objets PointDeTrace<br>
// Dernière mise à jour : 29/4/2018 par Jim

public class Trace {

	// attributs privés ---------------------------------------------------------------------------

	private int _id;				// l'identifiant de la trace
	private Date _dateHeureDebut;	// date et heure de début (utilise un objet java.util.Date)
	private Date _dateHeureFin;		// date et heure de fin (utilise un objet java.util.Date)
	private boolean _terminee;		// true si la trace est terminée, false sinon
	private int _idUtilisateur;		// l'identifiant de l'utilisateur ayant créé la trace
	private double _distance;		// la distance totale
	private ArrayList<PointDeTrace> _lesPointsDeTrace;	// la collection d'objets PointDeTrace
	
	// Constructeurs ------------------------------------------------------------------------------

	// Constructeur sans paramètre
	public Trace() {
		_id = 0;
		_dateHeureDebut = null;
		_dateHeureFin = null;
		_terminee = false;
		_idUtilisateur = 0;
		_distance = 0;
		_lesPointsDeTrace = new ArrayList<PointDeTrace>();
	}

	// Constructeur avec 5 paramètres
	public Trace(int unId, Date uneDateHeureDebut, Date uneDateHeureFin, boolean terminee, int unIdUtilisateur) {
		_id = unId;
		_dateHeureDebut = uneDateHeureDebut;
		_dateHeureFin = uneDateHeureFin;
		_terminee = terminee;
		_idUtilisateur = unIdUtilisateur;
		_distance = 0;
		_lesPointsDeTrace = new ArrayList<PointDeTrace>();
	}

	// Constructeur avec 6 paramètres
	public Trace(int unId, Date uneDateHeureDebut, Date uneDateHeureFin, boolean terminee, int unIdUtilisateur, double uneDistance) {
		_id = unId;
		_dateHeureDebut = uneDateHeureDebut;
		_dateHeureFin = uneDateHeureFin;
		_terminee = terminee;
		_idUtilisateur = unIdUtilisateur;
		_distance = uneDistance;
		_lesPointsDeTrace = new ArrayList<PointDeTrace>();
	}

	// Accesseurs ---------------------------------------------------------------------------------

    public int getId() {return _id;}
    public void setId(int unId) {this._id = unId;}

	public Date getDateHeureDebut() {return _dateHeureDebut;}
	public void setDateHeureDebut(Date uneDateHeureDebut) {this._dateHeureDebut = uneDateHeureDebut;}    

	public Date getDateHeureFin() {return _dateHeureFin;}
	public void setDateHeureFin(Date uneDateHeureFin) {this._dateHeureFin = uneDateHeureFin;} 

    public boolean getTerminee() {return _terminee;}
    public void setTerminee(boolean terminee) {this._terminee = terminee;}

    public int getIdUtilisateur() {return _idUtilisateur;}
    public void setIdUtilisateur(int unIdUtilisateur) {this._idUtilisateur = unIdUtilisateur;}	
	
	public ArrayList<PointDeTrace> getLesPointsDeTrace() {return _lesPointsDeTrace;}

	// Méthodes publiques -------------------------------------------------------------------------

	// Fournit le nombre de points de passage
	public int getNombrePoints() {
		return _lesPointsDeTrace.size();
	}

	// Fournit le point central du parcours
	public Point getCentre() {
		if (_lesPointsDeTrace.size() == 0)
			return null;
		else {
			// au départ, les valeurs extrêmes sont celles du premier point
			PointDeTrace premierPoint = _lesPointsDeTrace.get(0);
			double latMini = premierPoint.getLatitude();
			double latMaxi = premierPoint.getLatitude();
			double longMini = premierPoint.getLongitude();
			double longMaxi = premierPoint.getLongitude();
			// parcours des autres points (à partir de la position 1)
			for (int i = 1 ; i < _lesPointsDeTrace.size(); i++) {
				PointDeTrace lePoint = _lesPointsDeTrace.get(i);
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
	public double getDenivele() {
		if (_lesPointsDeTrace.size() == 0)
			return 0;
		else {
			// au départ, les valeurs extrêmes sont celles du premier point
			PointDeTrace premierPoint = _lesPointsDeTrace.get(0);
			double altMini = premierPoint.getAltitude();
			double altMaxi = premierPoint.getAltitude();
			// parcours des autres points (àpartir de la position 1)
			for (int i = 1 ; i < _lesPointsDeTrace.size(); i++) {
				PointDeTrace lePoint = _lesPointsDeTrace.get(i);
				if (lePoint.getAltitude() < altMini) altMini = lePoint.getAltitude();
				if (lePoint.getAltitude() > altMaxi) altMaxi = lePoint.getAltitude();
			}
			double denivele = altMaxi - altMini;
			return denivele;
		}
	}
	
	// Fournit la durée totale du parcours (en secondes)
	public long getDureeEnSecondes() {
		if (_lesPointsDeTrace.size() == 0)
			return 0;
		else {
			int positionDernierPoint = _lesPointsDeTrace.size() -1;
			PointDeTrace dernierPoint = _lesPointsDeTrace.get(positionDernierPoint);
            return dernierPoint.getTempsCumule();
		}
	}
	
	// Fournit la durée totale du parcours sous forme d'une chaine "hh:mm:ss"
	public String getDureeTotale() {
		long duree = getDureeEnSecondes();
		if (duree == 0) {
			return "00:00:00";
		}
		else {
			int heures = (int) (duree / 3600);
			duree = duree - heures * 3600;
			int minutes = (int) (duree / 60);
			int secondes = (int) (duree % 60);
			return Outils.formaterNombre(heures, "00") + ":" + Outils.formaterNombre(minutes, "00") + ":" + Outils.formaterNombre(secondes, "00");
		}
	}
	
	// Fournit la distance totale du parcours (en Km)
	public double getDistanceTotale() {
		if (_distance > 0) return _distance;

		if (_lesPointsDeTrace.size() == 0)
			return 0;
		else {
			int positionDernierPoint = _lesPointsDeTrace.size() -1;
			PointDeTrace dernierPoint = _lesPointsDeTrace.get(positionDernierPoint);
            return dernierPoint.getDistanceCumulee();
		}
	}
	
	// Fournit le dénivelé positif (en m)
    public double getDenivelePositif()
    {
        double denivele = 0;
        // parcours de tous les couples de points
        for (int i = 0; i < _lesPointsDeTrace.size() - 1; i+=1)
        {
            PointDeTrace lePoint1 = (PointDeTrace)_lesPointsDeTrace.get(i);
            PointDeTrace lePoint2 = (PointDeTrace)_lesPointsDeTrace.get(i + 1);
            // on teste si ça monte
            if ( lePoint2.getAltitude() > lePoint1.getAltitude() )
                denivele += lePoint2.getAltitude() - lePoint1.getAltitude();
        }
        return denivele;
    }

    // Fournit le dénivelé négatif (en m)
    public double getDeniveleNegatif()
    {
        double denivele = 0;
        // parcours de tous les couples de points
        for (int i = 0; i < _lesPointsDeTrace.size() - 1; i+=1)
        {
            PointDeTrace lePoint1 = (PointDeTrace)_lesPointsDeTrace.get(i);
            PointDeTrace lePoint2 = (PointDeTrace)_lesPointsDeTrace.get(i + 1);
            // on teste si ça descend
            if (lePoint2.getAltitude() < lePoint1.getAltitude())
                denivele += lePoint1.getAltitude() - lePoint2.getAltitude();
        }
        return denivele;
    }	
	
    // Fournit la vitesse moyenne du parcours (en km/h)
	public double getVitesseMoyenne() {
		if (getDistanceTotale() == 0)
			return 0;
		else {
			double vitesseEnKmH = getDistanceTotale() / (double) getDureeEnSecondes() * 3600;
			return vitesseEnKmH;
		}
	}
	
	// Fournit une chaine contenant toutes les données de l'objet
	public String toString() {
		String msg = "";
        msg += "Id : \t\t\t\t" + getId() + "\n";
        msg += "Utilisateur : \t\t\t" + getIdUtilisateur() + "\n";
        if (getDateHeureDebut() != null) msg += "Heure de début :\t\t" + Outils.formaterDateHeureFR(getDateHeureDebut()) + "\n";
        if (getDateHeureFin() != null) msg += "Heure de fin :\t\t\t" + Outils.formaterDateHeureFR(getDateHeureFin()) + "\n";
        if (getTerminee()) msg += "Terminée : \t\t\tOui \n"; else msg += "Terminée : \t\t\tNon \n";
		msg += "Nombre de points :\t\t" + Outils.formaterNombre(getNombrePoints(), "00000") + "\n";
		if (getNombrePoints() > 0) {
			msg += "Durée en secondes :\t\t" + getDureeEnSecondes() + "\n";
			msg += "Durée totale :\t\t\t" + getDureeTotale() + "\n";
			msg += "Distance totale en Km :\t\t" + Outils.formaterNombre(getDistanceTotale(), "000.00") + "\n";
			msg += "Dénivelé en m :\t\t\t" + Outils.formaterNombre(getDenivele(), "0000.00") + "\n";
            msg += "Dénivelé positif en m :\t\t" + Outils.formaterNombre(getDenivelePositif(), "0000.00") + "\n";
            msg += "Dénivelé négatif en m :\t\t" + Outils.formaterNombre(getDeniveleNegatif(), "0000.00") + "\n";
			msg += "Vitesse moyenne en Km/h :\t" + Outils.formaterNombre(getVitesseMoyenne() , "00.00") + "\n";
			msg += "Centre du parcours :\n";
            msg += "   - Latitude :\t\t\t" + Outils.formaterNombre(getCentre().getLatitude(), "000.000") + "\n";
            msg += "   - Longitude :\t\t" + Outils.formaterNombre(getCentre().getLongitude(), "000.000") + "\n";
            msg += "   - Altitude :\t\t\t" + Outils.formaterNombre(getCentre().getAltitude(), "000.000") + "\n";
		}
		return msg;
	}
	
	// ajoute un objet PointDeTrace à la collection
	public void ajouterPoint(PointDeTrace unPoint) {
        if (_lesPointsDeTrace.size() == 0)
        {   // si premier point de la trace, mise à zéro des données cumulées et de la vitesse
            unPoint.setTempsCumule(0);
            unPoint.setDistanceCumulee(0);
            unPoint.setVitesse(0);
        }
        else
        {   // si déjà d'autres points dans la trace, on cumule la durée et la distance avec celle du dernier point stocké
            PointDeTrace dernierPoint = (PointDeTrace) _lesPointsDeTrace.get(_lesPointsDeTrace.size()-1);

            long duree = (unPoint.getDateHeure().getTime() - dernierPoint.getDateHeure().getTime()) / 1000;		// en sec
            unPoint.setTempsCumule(dernierPoint.getTempsCumule() + duree);

            double distance = Point.getDistance(dernierPoint, unPoint);
            unPoint.setDistanceCumulee(dernierPoint.getDistanceCumulee() + distance);

            // calcul de la vitesse entre l'avant-dernier point et le point à ajouter
            if (_lesPointsDeTrace.size() >= 1)   // il faut au moins 1 point précédent
            {
                // distance et durée entre le dernier point et le nouveau point à ajouter
                distance = Point.getDistance(dernierPoint, unPoint);
                duree = (unPoint.getDateHeure().getTime() - dernierPoint.getDateHeure().getTime()) / 1000;	// en sec
                double vitesse = distance / (double) duree * 3600;
                // on affecte la vitesse calculée au nouveau point
                unPoint.setVitesse(vitesse);
            }
        }
		_lesPointsDeTrace.add(unPoint);
	}
	
	// vide la collection
	public void viderListePoints() {
		_lesPointsDeTrace.clear();
	}

	// lisse les données (altitude surtout)
    public Trace lisserDonnees()
    {
        Trace laTraceLissee = new Trace();
        for (int i = 0 ; i < _lesPointsDeTrace.size() ; i++)
        {
            PointDeTrace lePointCourant = (PointDeTrace)_lesPointsDeTrace.get(i);
            // clonage du point original
            PointDeTrace lePointAvecMoyenne = new PointDeTrace(lePointCourant);
            // correction de l'altitude : calcul de l'altitude moyenne des points n-2, n-1, n, n+1, n+2
            if (i > 1 && i < _lesPointsDeTrace.size() - 2)
            {
                PointDeTrace lePointMoins2 = (PointDeTrace)_lesPointsDeTrace.get(i-2);
                PointDeTrace lePointMoins1 = (PointDeTrace)_lesPointsDeTrace.get(i-1);
                PointDeTrace lePointPlus1 = (PointDeTrace)_lesPointsDeTrace.get(i+1);
                PointDeTrace lePointPlus2 = (PointDeTrace)_lesPointsDeTrace.get(i+2);
                double altitudeMoyenne = (lePointMoins2.getAltitude() + lePointMoins1.getAltitude() + lePointCourant.getAltitude() + lePointPlus1.getAltitude() + lePointPlus2.getAltitude()) / 5;
                lePointAvecMoyenne.setAltitude(altitudeMoyenne);
            }
            laTraceLissee.ajouterPoint(lePointAvecMoyenne);
        }
        return laTraceLissee;
    }
}
