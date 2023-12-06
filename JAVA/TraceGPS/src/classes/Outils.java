/**
 * Ce package contient 2 classes : Adresse et Individu.
 * @version 2.3
 * @author JM CARTRON
 */
package classes;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.text.DecimalFormat; 

/**
 * <b>Cette classe offre différents services courants sous forme de méthodes à portée classe.</b>
 * <p>
 * Ces services sont de différents types :
 * <ul>
 * <li>des outils de saisie et d'affichage sous forme de boites de dialogue</li>
 * <li>des outils concernant le traitement des nombres</li>
 * <li>des outils concernant le traitement des dates</li>
 * <li>des outils concernant le traitement des chaines</li>
 * </ul>
 * @version 2.3
 * @author JM CARTRON
 */
@SuppressWarnings("unused")
public class Outils {

	// --------------------------------------------------------------------------------------------------------------------------
	// --------------------------------------outils concernant le traitement des nombres-----------------------------------------
	// --------------------------------------------------------------------------------------------------------------------------
	
	/**
	 * teste si une chaine est bien numérique
	 * @param laChaine : la chaine à tester
	 * @return         : booléen - true si la chaine représente un nombre correct
	 *                             false dans les autres cas
	 */
	public static boolean isNumeric (String laChaine) {
		if (laChaine == null) return false;
		try {
			laChaine = laChaine.replace(",", ".");
			double Nombre = Double.parseDouble(laChaine);
			// new java.math.BigDecimal(laChaine);
			return true;
		}
		catch (Exception e) {
			return false;
		}
	}
	
	/**
	 * convertit un nombre en chaine formatée
	 * @param unNombre : le nombre à formater
	 * @param unFormat : le format de conversion (exemples : "00", "0.00", "###,###,##0.00", ...)
	 * @return une chaine numérique formatée
	 */
	public static String formaterNombre(double unNombre, String unFormat) {
	    DecimalFormat df = new DecimalFormat(unFormat);	
		return df.format(unNombre);
	}	
	
	
	// --------------------------------------------------------------------------------------------------------------------------
	// --------------------------------------outils concernant le traitement des dates-------------------------------------------
	// --------------------------------------------------------------------------------------------------------------------------	

	/**
	 * teste si une chaine est bien une date
	 * @param laChaine : la chaine à tester
	 * @return         : booléen - true si la chaine représente une date correcte
	 *                             false dans les autres cas
	 */
	public static boolean isDate (String laChaine) {
		if (laChaine == null) return false;
		
// 		version 1 (ne marche pas correctement)
//		try {
//			@SuppressWarnings("unused")
//			Date uneDate;
//			uneDate = Outils.convertirEnDate(laChaine);
//			return true;
//		}
//		catch (Exception e) {
//			return false;
//		}

		// version 2 du 10/10/2012
		// remplacement des autres séparateurs par des /
		laChaine = laChaine.replace(" ", "/");
		laChaine = laChaine.replace("-", "/");
		// éclatement de la chaine pour obtenir un tableau de 3 sous-chaines
		String[] tableau = laChaine.split("/");
		if (tableau.length != 3) return false;

		// conversion des 3 sous-chaines en type int
        int j = Integer.parseInt(tableau[0]);		// le jour
        int m = Integer.parseInt(tableau[1]);		// le mois		
        int a = Integer.parseInt(tableau[2]);		// l'année
        
        // test général
        if ( m < 0 || m > 12 || j < 0 || j > 31 )  return false;
        // test des mois de 30 jours
        if ( ( m == 4 || m == 6 || m == 9 || m == 11 ) && ( j > 30 ) ) return false;
        // les années bissextiles sont multiples de 4 mais pas de 100, ou bien elles sont multiples de 400 :
        boolean bissextile = ((a % 4) == 0 && (a % 100) != 0) || (a % 400) == 0;
        // février des années normales (28 jours)
        if ( m == 2 && bissextile == false && j > 28 ) return false;
        // février des années bissextiles (29 jours)
        if ( m == 2 && bissextile == true && j > 29 ) return false;
        // si on est encore là, cela signifie que la date est correcte
        return true;
	}  
	
	/**
	 * convertit une chaine date en un objet Date
	 * @param uneChaineDate   : la chaine à convertir
	 * @return                : l'objet Date obtenu par la conversion de la chaine (ou null si la chaine est incorrecte)
	 * @throws ParseException la chaine ne peut pas être convertie en date
	 */
	public static Date convertirEnDate(String uneChaineDate) throws ParseException {
		SimpleDateFormat leFormat = new SimpleDateFormat("dd/MM/yyyy");
		try
		{	return leFormat.parse(uneChaineDate);
		}
		catch (Exception ex) {
			return null;
		}
	}

	/**
	 * convertit une chaine date en un objet Date
	 * @param uneChaineDate   : la chaine à convertir
	 * @param unFormat        : le format de la chaine fournie (ex : "yyyy-MM-dd HH:mm:ss" pour format US)
	 * @return                : l'objet Date obtenu par la conversion de la chaine
	 * @throws ParseException
	 */
	public static Date convertirEnDate(String uneChaineDate, String unFormat) throws ParseException {
		SimpleDateFormat leFormat = new SimpleDateFormat(unFormat);
		return leFormat.parse(uneChaineDate);
	}
	
	/**
	 * convertit une chaine dateHeure en un objet Date
	 * @param uneChaineDateHeure : la chaine à convertir
	 * @return                   : l'objet Date obtenu par la conversion de la chaine (ou null si la chaine est incorrecte)
	 * @throws ParseException la chaine ne peut pas être convertie en date
	 */
	public static Date convertirEnDateHeure(String uneChaineDateHeure) throws ParseException {
		SimpleDateFormat leFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
		try
		{	return leFormat.parse(uneChaineDateHeure);
		}
		catch (Exception ex) {
			return null;
		}
	}
	
	/**
	 * convertit une date en une chaine formatée
	 * @param uneDate : la date à formater
	 * @return        : la chaine formatée
	 */
	public static String formaterDate(Date uneDate) {
		SimpleDateFormat leFormat = new SimpleDateFormat("dd/MM/yyyy");
		return leFormat.format(uneDate);
	}

	/**
	 * convertit une date en une chaine formatée comprenant également l'heure (format FR)
	 * @param uneDate : la date et l'heure à formater
	 * @return        : la chaine formatée
	 */
	public static String formaterDateHeureFR(Date uneDate) {
		SimpleDateFormat leFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
		return leFormat.format(uneDate);
	}

	/**
	 * convertit une date en une chaine formatée contenant uniquement l'heure (format FR)
	 * @param uneDate : la date et l'heure à formater
	 * @return        : la chaine formatée
	 */
	public static String formaterHeureFR(Date uneDate) {
		SimpleDateFormat leFormat = new SimpleDateFormat("HH:mm:ss");
		return leFormat.format(uneDate);
	}	
	
	/**
	 * convertit une date en une chaine formatée comprenant également l'heure (format US)
	 * @param uneDate : la date et l'heure à formater
	 * @return        : la chaine formatée
	 */
	public static String formaterDateHeureUS(Date uneDate) {
		SimpleDateFormat leFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		return leFormat.format(uneDate);
	}
	
	/**
	 * fournit le jour de la semaine à partir d'une date
	 * @param uneDate : la date étudiée
	 * @return        : le jour de la semaine (exemples : "dimanche", "lundi", ...)
	 */
	public static String getJourDeLaSemaine(Date uneDate)	{
		SimpleDateFormat leFormat = new SimpleDateFormat("EEEE");	// "EEEE" : jour de la semaine
		return leFormat.format(uneDate);
	}
	
	/**
	 * fournit l'année à partir d'une date
	 * @param uneDate : la date étudiée
	 * @return        : l'année sur 4 chiffres
	 */
	public static int getAnnee(Date uneDate) {
		SimpleDateFormat leFormat = new SimpleDateFormat("yyyy");	// "yyyy" : année sur 4 chiffres
		return Integer.parseInt(leFormat.format(uneDate));
	}
	
	/**
	 * fournit une date en ajoutant des jours à une autre date
	 * @param uneDate   : la date de départ
	 * @param nbDeJours : le nombre de jours à ajouter (ce nombre peut être négatif)
	 * @return          : la nouvelle date obtenue
	 */
	public static Date ajouterDesJours(Date uneDate, int nbDeJours) {
		Calendar calendrier = Calendar.getInstance();
		calendrier.setTime(uneDate);
		calendrier.add(Calendar.DATE, nbDeJours);
		return calendrier.getTime();
	}
	
	/**
	 * La fonction DateUS convertit une date française (j/m/a) au format US (a-m-j)
	 * par exemple, le paramètre '16/05/2007' donnera '2007-05-16'
	 * @param laDate : la date à transformer
	 * @return       : la date transformée
	 */
	public static String getDateUS (String laDate)
	{	String[] tableau = laDate.split ("/");		// on extrait les segments de la chaine laDate séparés par des "/"
		String J = tableau[0];
		String M = tableau[1];
		String A = tableau[2];
		return (A + "-" + M + "-" + J);				// on les reconcatène dans un ordre différent
	}

	/**
	 * La fonction DateFR convertit une date US (a-m-j) au format Français (j/m/a)
	 * par exemple, le paramètre '2007-05-16' donnera '16/05/2007'
	 * @param laDate : la date à transformer
	 * @return       : la date transformée
	 */
	public static String getDateFR (String laDate)
	{	String[] tableau = laDate.split ("-");		// on extrait les segments de la chaine laDate séparés par des "-"
		String A = tableau[0];
		String M = tableau[1];
		String J = tableau[2];
		return (J + "/" + M + "/" + A);				// on les reconcatène dans un ordre différent
	}

	
	// --------------------------------------------------------------------------------------------------------------------------
	// --------------------------------------outils concernant le traitement des chaines ----------------------------------------
	// --------------------------------------------------------------------------------------------------------------------------	

	/**
	 * complète la chaine fournie par des espaces jusqu'à la longueur désirée
	 * @param laChaine : la chaine à compléter
	 * @param longueur : la longueur à obtenir
	 * @return         : la chaine complétée
	 */
	public static String completerChaine(String laChaine, int longueur) {
		while ( laChaine.length() < longueur ) {
			laChaine = laChaine + " ";
		}
		return laChaine;
	}
	
	/**
	 * complète la chaine fournie par un caractère choisi jusqu'à la longueur désirée
	 * @param laChaine    : la chaine à compléter
	 * @param longueur    : la longueur à obtenir
	 * @param leCaractere : le caractère utilisé pour compléter la chaine
	 * @return            : la chaine complétée
	 */
	public static String completerChaine(String laChaine, int longueur, char leCaractere) {
		while ( laChaine.length() < longueur ) {
			laChaine = laChaine + leCaractere;
		}
		return laChaine;
	}	
	
    /**
     * méthode de classe pour reformater un numéro de téléphone en 5 groupes de 2 chiffres séparés par des points.
     * @param laChaine le numéro à transformer
     * @return         le numéro transformé
     */
	public static String corrigerTelephone(String laChaine)
	{	String temp;
		String resultat;
		temp = laChaine;
		temp = temp.replace(" ", "");		// supprime les espaces
		temp = temp.replace(".", "");		// supprime les points
		temp = temp.replace(",", "");		// supprime les virgules
		temp = temp.replace("-", "");		// supprime les tirets
		temp = temp.replace("_", "");		// supprime les underscore
		temp = temp.replace("/", "");		// supprime les slash
		if (temp.length() == 10 && Outils.isNumeric(temp))
		{
			resultat = temp.substring(0, 2) + ".";
			resultat += temp.substring(2, 4) + ".";
			resultat += temp.substring(4, 6) + ".";
			resultat += temp.substring(6, 8) + ".";
			resultat += temp.substring(8, 10);
			return resultat;
		}
		else
		{
			return laChaine;
		}
	}
	
	public static String sha1(String laChaineAcoder)
	{
		byte[] laChaineCodee = null;
		try {
			MessageDigest codeur = MessageDigest.getInstance("sha-1");
			codeur.update(laChaineAcoder.getBytes());
			laChaineCodee = codeur.digest();
			
		} catch (NoSuchAlgorithmException e) {
		}
		return bytesToHex(laChaineCodee);
	}
	
	public static String bytesToHex(byte[] tableau) {
		char hexDigits[] = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
		StringBuffer buffer = new StringBuffer();
		for (int i = 0; i < tableau.length; i++) {
			buffer.append(hexDigits[(tableau[i] >> 4) & 0x0f]);
			buffer.append(hexDigits[tableau[i] & 0x0f]);
		}
		return buffer.toString();
	}
	
}
