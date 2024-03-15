package testsunitaires;

import static org.junit.Assert.*;

import java.util.ArrayList;
import java.util.Date;

import org.junit.BeforeClass;
import org.junit.Test;

import classes.Outils;
import classes.PointDeTrace;
import classes.Trace;

public class TraceTest {

	private static Trace trace1;		// cette trace restera vide
	private static Trace trace2;		// cette trace va comporter des points
	
	@BeforeClass
	public static void setUpBeforeClass() throws Exception {
		trace1 = new Trace();		// cette trace restera vide
		trace2 = new Trace(1, null, null, true, 3);		// cette trace va comporter des points
		
		Date uneDate1 = Outils.convertirEnDateHeure("21/06/2016 14:00:00");
		PointDeTrace point1 = new PointDeTrace(1, 1, 48.500, -1.600, 50, uneDate1);
		trace2.ajouterPoint(point1);
		trace2.setDateHeureDebut(uneDate1);
		
		Date uneDate2 = Outils.convertirEnDateHeure("21/06/2016 14:03:40");
		PointDeTrace point2 = new PointDeTrace(1, 2, 48.510, -1.600, 200, uneDate2);
		trace2.ajouterPoint(point2);
		
		Date uneDate3 = Outils.convertirEnDateHeure("21/06/2016 14:05:20");
		PointDeTrace point3 = new PointDeTrace(1, 3, 48.510, -1.610, 150, uneDate3);
		trace2.ajouterPoint(point3);
		
		Date uneDate4 = Outils.convertirEnDateHeure("21/06/2016 14:08:40");
		PointDeTrace point4 = new PointDeTrace(1, 4, 48.500, -1.610, 200, uneDate4);
		trace2.ajouterPoint(point4);
		
		Date uneDate5 = Outils.convertirEnDateHeure("21/06/2016 14:10:00");
		PointDeTrace point5 = new PointDeTrace(1, 5, 48.500, -1.600, 50, uneDate5);
		trace2.ajouterPoint(point5);
		trace2.setDateHeureFin(uneDate5);
	}

	@Test
	public void testGetLesPointsDeTrace() {
		ArrayList<PointDeTrace> lesPoints1 = trace1.getLesPointsDeTrace();
		assertEquals("Test getLesPointsDeTrace", 0, lesPoints1.size());
		
		ArrayList<PointDeTrace> lesPoints2 = trace2.getLesPointsDeTrace();
		assertEquals("Test getLesPointsDeTrace", 5, lesPoints2.size());	
		PointDeTrace premierPoint = lesPoints2.get(0);
		assertEquals("Test getLesPointsDeTrace", 48.5, premierPoint.getLatitude(), 0.001);	
		PointDeTrace dernierPoint = lesPoints2.get(4);
		assertEquals("Test getLesPointsDeTrace", 48.5, dernierPoint.getLatitude(), 0.001);	
	}

	@Test
	public void testGetDateHeureDebut() {
		Date debut1 = trace1.getDateHeureDebut();
		assertNull("Test getDateHeureDebut", debut1);
		
		String debut2 = Outils.formaterDateHeureFR(trace2.getDateHeureDebut());
		assertEquals("Test getDateHeureDebut", "21/06/2016 14:00:00", debut2);		
	}

	@Test
	public void testGetDateHeureFin() {
		Date fin1 = trace1.getDateHeureFin();
		assertNull("Test getDateHeureFin", fin1);
		
		String fin2 = Outils.formaterDateHeureFR(trace2.getDateHeureFin());
		assertEquals("Test getDateHeureFin", "21/06/2016 14:10:00", fin2);			
	}

	@Test
	public void testGetNombrePoints() {
		int nb1 = trace1.getNombrePoints();
		assertEquals("Test getNombrePoints", 0, nb1);
		
		int nb2 = trace2.getNombrePoints();
		assertEquals("Test getNombrePoints", 5, nb2);	
	}

	@Test
	public void testGetCentre() {
		assertEquals("Test getCentre", null, trace1.getCentre());

		assertEquals("Test getCentre", 48.505, trace2.getCentre().getLatitude(), 0.001);
		assertEquals("Test getCentre", -1.605, trace2.getCentre().getLongitude(), 0.001);
		assertEquals("Test getCentre", 0, trace2.getCentre().getAltitude(), 0);
	}

	@Test
	public void testGetDenivele() {
		double denivele1 = trace1.getDenivele();
		assertEquals("Test getDenivelle", 0, denivele1, 0);
		
		double denivele2 = trace2.getDenivele();
		assertEquals("Test getDenivelle", 150, denivele2, 0.1);
	}

	@Test
	public void testGetDenivelePositif() {
		double denivele1 = trace1.getDenivelePositif();
		assertEquals("Test getDenivelePositif", 0, denivele1, 0);
		
		double denivele2 = trace2.getDenivelePositif();
		assertEquals("Test getDenivelePositif", 200, denivele2, 0.1);
	}

	@Test
	public void testGetDeniveleNegatif() {
		double denivele1 = trace1.getDeniveleNegatif();
		assertEquals("Test getDenivelleNegatif", 0, denivele1, 0);
		
		double denivele2 = trace2.getDeniveleNegatif();
		assertEquals("Test getDenivelleNegatif", 200, denivele2, 0.1);
	}
	
	@Test
	public void testGetDureeEnSecondes() {
		long duree1 = trace1.getDureeEnSecondes();
		assertEquals("Test getDureeEnSecondes", 0, duree1);
		
		long duree2 = trace2.getDureeEnSecondes();
		assertEquals("Test getDureeEnSecondes", 600, duree2);		
	}

	@Test
	public void testGetDureeTotale() {
		String duree1 = trace1.getDureeTotale();
		assertEquals("Test getDureeTotale", "00:00:00", duree1);	
		
		String duree2 = trace2.getDureeTotale();
		assertEquals("Test getDureeTotale", "00:10:00", duree2);	
	}

	@Test
	public void testGetDistanceTotale() {
		double dist1 = trace1.getDistanceTotale();
		assertEquals("Test getDistanceTotale", 0, dist1, 0);	
		
		double dist2 = trace2.getDistanceTotale();
		assertEquals("Test getDistanceTotale", 3.698, dist2, 0.004);	
	}

	@Test
	public void testGetVitesseMoyenne() {
		double vitesse1 = trace1.getVitesseMoyenne();
		assertEquals("Test getVitesseMoyenne", 0, vitesse1, 0);
		
		double vitesse2 = trace2.getVitesseMoyenne();
		assertEquals("Test getVitesseMoyenne", 22.188, vitesse2, 0.02);
	}

	@Test
	public void testToString() {
		String msg1 = "";
        msg1 += "Id : \t\t\t\t" + "0" + "\n";
        msg1 += "Utilisateur : \t\t\t" + "0" + "\n";
        msg1 += "Terminée : \t\t\tNon \n";
		msg1 += "Nombre de points :\t\t" + "00000" + "\n";
		assertEquals("Test toString", msg1, trace1.toString());
		
		String msg2 = "";
        msg2 += "Id : \t\t\t\t" + "1" + "\n";
        msg2 += "Utilisateur : \t\t\t" + "3" + "\n";
        msg2 += "Heure de début :\t\t" + "21/06/2016 14:00:00" + "\n";
		msg2 += "Heure de fin :\t\t\t" + "21/06/2016 14:10:00" + "\n";
        msg2 += "Terminée : \t\t\tOui \n";		
		msg2 += "Nombre de points :\t\t" + "00005" + "\n";
		msg2 += "Durée en secondes :\t\t" + "600" + "\n";
		msg2 += "Durée totale :\t\t\t" + "00:10:00" + "\n";
		msg2 += "Distance totale en Km :\t\t" + "003,69" + "\n";
		msg2 += "Dénivelé en m :\t\t\t" + "0150,00" + "\n";
		msg2 += "Dénivelé positif en m :\t\t" + "0200,00" + "\n";
		msg2 += "Dénivelé négatif en m :\t\t" + "0200,00" + "\n";
		msg2 += "Vitesse moyenne en Km/h :\t" + "22,17" + "\n";
		msg2 += "Centre du parcours :\n";
		msg2 += "   - Latitude :\t\t\t" + "048,505" + "\n";
		msg2 += "   - Longitude :\t\t" + "-001,605" + "\n";
		msg2 += "   - Altitude :\t\t\t" + "000,000" + "\n";
		assertEquals("Test toString", msg2, trace2.toString());
	}

	@Test
    public void testAjouterPoint()
    {
        PointDeTrace point1 = (PointDeTrace) trace2.getLesPointsDeTrace().get(0);
        assertEquals(0, point1.getTempsCumule());
        assertEquals(0, point1.getDistanceCumulee(), 0.01);
        assertEquals(0, point1.getVitesse(), 0.01);

        PointDeTrace point2 = (PointDeTrace)trace2.getLesPointsDeTrace().get(1);
        assertEquals(220, point2.getTempsCumule());
        assertEquals(1.112, point2.getDistanceCumulee(), 0.002);
        assertEquals(18.196, point2.getVitesse(), 0.04);

        PointDeTrace point3 = (PointDeTrace)trace2.getLesPointsDeTrace().get(2);
        assertEquals(320, point3.getTempsCumule());
        assertEquals(1.849, point3.getDistanceCumulee(), 0.003);
        assertEquals(26.532, point3.getVitesse(), 0.04);

        PointDeTrace point4 = (PointDeTrace)trace2.getLesPointsDeTrace().get(3);
        assertEquals(520, point4.getTempsCumule());
        assertEquals(2.961, point4.getDistanceCumulee(), 0.004);
        assertEquals(20.016, point4.getVitesse(), 0.04);

        PointDeTrace point5 = (PointDeTrace)trace2.getLesPointsDeTrace().get(4);
        assertEquals(600, point5.getTempsCumule());
        assertEquals(3.698, point5.getDistanceCumulee(), 0.004);
        assertEquals(33.165, point5.getVitesse(), 0.04);
    }
    
}
