package classes;

import java.util.Date;

// Cette classe représente un point de passage de la trace.
// Elle hérite de la classe Point.
// Elle y ajoute :
// - l'id de la trace
// - l'id du point (relatif à la trace)
// - l'heure de passage au point
// - le rythme cardiaque
// - le temps cumulé depuis le départ (en secondes)
// - la distance cumulée depuis le départ (en Km)
// - la vitesse instantanée, calculée entre le point précédent et le point actuel (en Km/h)
// Dernière mise à jour : 26/3/2018 par Jim

public class PointDeTrace extends Point {

	// attributs privés ---------------------------------------------------------------------------

	private int _idTrace;				// l'identifiant de la trace
    private int _id;					// l'identifiant relatif du point dans la trace
	private Date _dateHeure;			// l'heure de passage au point (utilise un objet java.util.Date)
    private int _rythmeCardio;			// le rythme cardiaque au point
    private long _tempsCumule;			// le temps cumulé depuis le départ (en secondes)
    private double _distanceCumulee;	// la distance cumulée depuis le départ (en Km)
    private double _vitesse;			// la vitesse instantanée, calculée entre le point précédent et le point actuel (en Km/h)	
	
	// Constructeurs ------------------------------------------------------------------------------

    // Constructeur sans paramètre
	public PointDeTrace() {
		// appelle le constructeur de la classe mère
		super();
		// initialise les nouveaux champs
		_idTrace = 0;
		_id = 0;
		_dateHeure = null;
        _rythmeCardio = 0;
        _tempsCumule = 0;
        _distanceCumulee = 0;
        _vitesse = 0;		
	}
	
	// Constructeur avec 6 paramètres
	public PointDeTrace(int idTrace, int id, double uneLatitude, double uneLongitude, double uneAltitude, Date uneDateHeure) {
		// appelle le constructeur de la classe mère
		super(uneLatitude, uneLongitude, uneAltitude);
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
    public PointDeTrace(int idTrace, int id, double uneLatitude, double uneLongitude, double uneAltitude, Date uneDateHeure, int unRythmeCardio) {
		// appelle le constructeur de la classe mère
		super(uneLatitude, uneLongitude, uneAltitude);
		// initialise les nouveaux champs
		_idTrace = idTrace;
		_id = id;
        _dateHeure = uneDateHeure;
        _rythmeCardio = unRythmeCardio;
        _tempsCumule = 0;
        _distanceCumulee = 0;
        _vitesse = 0;
    }
    
	// Constructeur avec 10 paramètres
    public PointDeTrace(int idTrace, int id, double uneLatitude, double uneLongitude, double uneAltitude, Date uneDateHeure, int unRythmeCardio,
        long unTempsCumule, double uneDistanceCumulee, double uneVitesse) {
		// appelle le constructeur de la classe mère
		super(uneLatitude, uneLongitude, uneAltitude);
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
    public PointDeTrace(PointDeTrace unPointExistant) {
        // appelle le constructeur de la classe mère
        super(unPointExistant.getLatitude(), unPointExistant.getLongitude(), unPointExistant.getAltitude());
        // initialise les nouveaux champs
		_idTrace = unPointExistant._idTrace;
		_id = unPointExistant._id;
        _dateHeure = unPointExistant._dateHeure;
        _rythmeCardio = unPointExistant._rythmeCardio;
        _tempsCumule = unPointExistant._tempsCumule;
        _distanceCumulee = unPointExistant._distanceCumulee;
        _vitesse = unPointExistant._vitesse;
    }
    
	// Accesseurs ---------------------------------------------------------------------------------

    public int getIdTrace() {return _idTrace;}
    public void setIdTrace(int unIdTrace) {this._idTrace = unIdTrace;}
    
    public int getId() {return _id;}
    public void setId(int unId) {this._id = unId;}

	public Date getDateHeure() {return _dateHeure;}
	public void setDateHeure(Date uneDateHeure) {this._dateHeure = uneDateHeure;}

    public int getRythmeCardio() {return _rythmeCardio;}
    public void setRythmeCardio(int unRythmeCardio) {this._rythmeCardio = unRythmeCardio;}

    public long getTempsCumule() {return _tempsCumule;}
    public void setTempsCumule(long unTempsCumule) {this._tempsCumule = unTempsCumule;}

    public double getDistanceCumulee() {return _distanceCumulee;}
    public void setDistanceCumulee(double uneDistanceCumulee) {this._distanceCumulee = uneDistanceCumulee;}

    public double getVitesse() {return _vitesse;}
    public void setVitesse(double uneVitesse) {this._vitesse = uneVitesse;}
    
	// Méthodes publiques -------------------------------------------------------------------------

    // Méthode fournissant le temps cumulé depuis le départ (sous la forme d'une chaine "00:00:00")
    public String getTempsCumuleEnChaine()
    {
        long duree = _tempsCumule;
        int heures = (int)(duree / 3600);
        duree = duree - heures * 3600;
        int minutes = (int)(duree / 60);
        int secondes = (int)(duree % 60);

        return Outils.formaterNombre(heures, "00") + ":" + Outils.formaterNombre(minutes, "00") + ":" + Outils.formaterNombre(secondes, "00");
    }
    
    // Fournit une chaine contenant toutes les données de l'objet
	public String toString()
	{	String msg = "";
		msg += "Id trace :\t" + Outils.formaterNombre(_idTrace, "0") + "\n";
		msg += "Id point :\t" + Outils.formaterNombre(_id, "0") + "\n";	
		msg += super.toString();
		if (this._dateHeure != null)
			msg += "Heure de passage :\t" + Outils.formaterDateHeureFR(this._dateHeure) + "\n";
        msg += "Rythme cardiaque :\t" + Outils.formaterNombre(_rythmeCardio, "0") + "\n";
        msg += "Temps cumulé (s) :\t" + Outils.formaterNombre(_tempsCumule, "0") + "\n";
        msg += "Temps cumulé (hh:mm:ss) :\t" + getTempsCumuleEnChaine() + "\n";
        msg += "Distance cumulée (Km) :\t" + Outils.formaterNombre(_distanceCumulee, "000.000") + "\n";
        msg += "Vitesse (Km/h) :\t" + Outils.formaterNombre(_vitesse, "000.000") + "\n";		
		return msg;
	}	
	
}
