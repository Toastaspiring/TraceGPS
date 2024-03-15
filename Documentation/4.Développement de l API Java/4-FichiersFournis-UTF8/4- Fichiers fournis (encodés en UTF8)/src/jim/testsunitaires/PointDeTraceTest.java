package jim.testsunitaires;

import static org.junit.Assert.*;

import java.text.ParseException;
import java.util.Date;

import org.junit.Before;
import org.junit.Test;

import jim.classes.Outils;
import jim.classes.PointDeTrace;

public class PointDeTraceTest {

	private PointDeTrace point1, point2, point3, point4, point5;  
	
	@Before
	public void setUp() throws Exception {
		point1 = new PointDeTrace();
		Date uneDate = Outils.convertirEnDateHeure("21/06/2016 14:30:20");
		point2 = new PointDeTrace(1, 2, 48.5 , -1.6, 100.5, uneDate);
        point3 = new PointDeTrace(1, 3, 48.5, -1.6, 100.5, uneDate, 140);
        point4 = new PointDeTrace(1, 4, 48.5, -1.6, 100.5, uneDate, 140, 3600, 21.5, 23.5);
        point5 = new PointDeTrace(point4);
	}

	@Test
	public void testGetDateHeure() {
		assertEquals("Test getDateHeure", null, point1.getDateHeure());
		assertEquals("Test getDateHeure", "21/06/2016 14:30:20", Outils.formaterDateHeureFR(point2.getDateHeure()));
	}

	@Test
    public void testGetDistanceCumulee()
    {
        assertEquals(0, point1.getDistanceCumulee(), 0);
        assertEquals(0, point2.getDistanceCumulee(), 0);
        assertEquals(0, point3.getDistanceCumulee(), 0);
        assertEquals(21.5, point4.getDistanceCumulee(), 0);
        assertEquals(21.5, point5.getDistanceCumulee(), 0);
    }

	@Test
    public void testGetRythmeCardio()
    {
        assertEquals(0, point1.getRythmeCardio());
        assertEquals(0, point2.getRythmeCardio());
        assertEquals(140, point3.getRythmeCardio());
        assertEquals(140, point4.getRythmeCardio());
        assertEquals(140, point5.getRythmeCardio());
    }

	@Test
    public void testGetTempsCumule()
    {
        assertEquals(0, point1.getTempsCumule());
        assertEquals(0, point2.getTempsCumule());
        assertEquals(0, point3.getTempsCumule());
        assertEquals(3600, point4.getTempsCumule());
        assertEquals(3600, point5.getTempsCumule());
    }

	@Test
    public void testGetTempsCumuleEnChaine()
    {
        assertEquals("00:00:00", point1.getTempsCumuleEnChaine());
        assertEquals("00:00:00", point2.getTempsCumuleEnChaine());
        assertEquals("00:00:00", point3.getTempsCumuleEnChaine());
        assertEquals("01:00:00", point4.getTempsCumuleEnChaine());
        assertEquals("01:00:00", point5.getTempsCumuleEnChaine());
    }

	@Test
    public void testGetVitesse()
    {
        assertEquals(0, point1.getVitesse(), 0);
        assertEquals(0, point2.getVitesse(), 0);
        assertEquals(0, point3.getVitesse(), 0);
        assertEquals(23.5, point4.getVitesse(), 0);
        assertEquals(23.5, point5.getVitesse(), 0);
    }

	@Test
	public void testSetDateHeure() throws ParseException {
		Date uneDate = Outils.convertirEnDateHeure("22/07/2017 15:31:21");
		point1.setDateHeure(uneDate);
		assertEquals("Test getDateHeure", "22/07/2017 15:31:21", Outils.formaterDateHeureFR(point1.getDateHeure()));
	}
	
	@Test
    public void setDistanceCumuleeTest()
    {
        point1.setDistanceCumulee(12.75);
        assertEquals(12.75, point1.getDistanceCumulee(), 0.001);
    }

	@Test
    public void setRythmeCardioTest()
    {
        point1.setRythmeCardio(120);
        assertEquals(120, point1.getRythmeCardio());
    }

	@Test
    public void setTempsCumuleTest()
    {
        point1.setTempsCumule(4000);
        assertEquals(4000, point1.getTempsCumule());
    }

	@Test
    public void setVitesseTest()
    {
        point1.setVitesse(22.75);
        assertEquals(22.75, point1.getVitesse(), 0.001);
    }

	@Test
	public void testToString() {
		String msg;
		msg = "Id trace :\t" + "0" + "\n";
		msg += "Id point :\t" + "0" + "\n";	
        msg += "Latitude :\t" + "000,000" + "\n";
        msg += "Longitude :\t" + "000,000" + "\n";
        msg += "Altitude :\t" + "000,000" + "\n";
        msg += "Rythme cardiaque :\t" + "0" + "\n";
        msg += "Temps cumulé (s) :\t" + "0" + "\n";
        msg += "Temps cumulé (hh:mm:ss) :\t" + "00:00:00" + "\n";
        msg += "Distance cumulée (Km) :\t" + "000,000" + "\n";
        msg += "Vitesse (Km/h) :\t" + "000,000" + "\n";
		assertEquals("Test toString", msg, point1.toString());
		
		msg = "Id trace :\t" + "1" + "\n";
		msg += "Id point :\t" + "4" + "\n";	
        msg += "Latitude :\t" + "048,500" + "\n";
        msg += "Longitude :\t" + "-001,600" + "\n";
        msg += "Altitude :\t" + "100,500" + "\n";
        msg += "Heure de passage :\t" + "21/06/2016 14:30:20" + "\n";
        msg += "Rythme cardiaque :\t" + "140" + "\n";
        msg += "Temps cumulé (s) :\t" + "3600" + "\n";
        msg += "Temps cumulé (hh:mm:ss) :\t" + "01:00:00" + "\n";
        msg += "Distance cumulée (Km) :\t" + "021,500" + "\n";
        msg += "Vitesse (Km/h) :\t" + "023,500" + "\n";
		assertEquals("Test toString", msg, point5.toString());
	}	

}
