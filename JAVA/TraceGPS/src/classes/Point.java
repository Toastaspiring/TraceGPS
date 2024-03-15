package classes;

// Cette classe représente un point géographique avec une latitude, une longitude et une altitude
// Dernière mise à jour : 26/3/2018 par Jim

public class Point {
	
	// attributs privés ---------------------------------------------------------------------------

	private double _latitude;		// la latitude du point (en degrés décimaux)
	private double _longitude;		// la longitude du point (en degrés décimaux)
	private double _altitude;		// l'altitude du point (en mètres)
	
	// Constructeurs ------------------------------------------------------------------------------

	// Constructeur sans paramètre
	public Point() {
		_latitude = 0;
		_longitude = 0;
		_altitude = 0;
	}

	// Constructeur avec paramètres
	public Point(double uneLatitude, double uneLongitude, double uneAltitude) {
		_latitude = uneLatitude;
		_longitude = uneLongitude;
		_altitude = uneAltitude;
	}
	
	// Accesseurs ---------------------------------------------------------------------------------

	public double getLatitude() {return _latitude;}
	public void setLatitude(double uneLatitude) {this._latitude = uneLatitude;}

	public double getLongitude() {return _longitude;}
	public void setLongitude(double uneLongitude) {this._longitude = uneLongitude;}

	public double getAltitude() {return _altitude;}
	public void setAltitude(double uneAltitude) {this._altitude = uneAltitude;}	
	
	// Méthodes publiques -------------------------------------------------------------------------
	
	// Calcul de la distance (en Km) entre 2 points géographiques.<br>
	// Ce code est transposé du forum JavaScript suivant :<br>
	// www.clubic.com/forum/programmation/calcul-de-distance-entre-deux-coordonnees-gps-id178494-page1.html<br>
	// CETTE FONCTION EST A TESTER ABSOLUMENT<br>
	// (on pourra par exemple utiliser le site http://www.lexilogos.com/calcul_distances.htm)
	// 
	// latitude1  : latitude point 1 (en degrés décimaux)
	// longitude1 : longitude point 1 (en degrés décimaux)
	// latitude2  : latitude point 2 (en degrés décimaux)
	// longitude2 : longitude point 2 (en degrés décimaux)
	// retourne   : la distance (en Km) entre les 2 points
	private static double getDistanceBetween (double latitude1, double longitude1, double latitude2, double longitude2)
	{
		if (latitude1 == latitude2 && longitude1 == longitude2) return 0;
		
		double a = Math.PI / 180; 
		latitude1 = latitude1 * a; 
		latitude2 = latitude2 * a; 
		longitude1 = longitude1 * a; 
		longitude2 = longitude2 * a;  
		double t1 = Math.sin(latitude1) * Math.sin(latitude2); 
		double t2 = Math.cos(latitude1) * Math.cos(latitude2); 
		double t3 = Math.cos(longitude1 - longitude2); 
		double t4 = t2 * t3; 
		double t5 = t1 + t4; 
		double rad_dist = Math.atan(-t5 / Math.sqrt(-t5 * t5 + 1)) + 2 * Math.atan(1);  
		return (rad_dist * 3437.74677 * 1.1508) * 1.6093470878864446;		
	}
	
	// Calcule et retourne la distance (en Km) entre l'instance (this) et le point géographique autrePoint
	public double getDistance (Point autrePoint) {
		double lat1 = this._latitude;
		double long1 = this._longitude;
		double lat2 = autrePoint._latitude;
		double long2 = autrePoint._longitude;
		double dist = getDistanceBetween(lat1, long1, lat2, long2);
		return dist;
	}
	
	// Calcule et retourne la distance (en Km) entre les 2 points géographiques point1 et point2
	public static double getDistance (Point point1, Point point2) {
		double lat1 = point1._latitude;
		double long1 = point1._longitude;
		double lat2 = point2._latitude;
		double long2 = point2._longitude;
		double dist = getDistanceBetween(lat1, long1, lat2, long2);
		return dist;
	}
	
	// Fournit une chaine contenant toutes les données de l'objet
	public String toString()
	{	String msg = "";
		msg += "Latitude :\t" + Outils.formaterNombre(this._latitude, "000.000") + "\n";
		msg += "Longitude :\t" + Outils.formaterNombre(this._longitude, "000.000") + "\n";
		msg += "Altitude :\t" + Outils.formaterNombre(this._altitude, "000.000") + "\n";
		return msg;
	}
	
} // fin de la classe
