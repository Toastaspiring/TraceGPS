package testsunitaires;

import static org.junit.Assert.*;

import org.junit.Before;
import org.junit.Test;

import classes.Point;

public class PointTest {

	private Point point1;
	private Point point2;
	
	@Before
	public void setUp() throws Exception {
		point1 = new Point();
		point2 = new Point(48.5 , -1.6, 100.5);
	}

	@Test
	public void testGetLatitude() {
		assertEquals("Test getLatitude", 0, point1.getLatitude(), 0.001);
		assertEquals("Test getLatitude", 48.5, point2.getLatitude(), 0.001);
	}

	@Test
	public void testSetLatitude() {
		point1.setLatitude(10.5);
		assertEquals("Test getLatitude", 10.5, point1.getLatitude(), 0.001);
	}

	@Test
	public void testGetLongitude() {
		assertEquals("Test getLongitude", 0, point1.getLongitude(), 0.001);
		assertEquals("Test getLongitude", -1.6, point2.getLongitude(), 0.001);
	}

	@Test
	public void testSetLongitude() {
		point1.setLongitude(12.5);
		assertEquals("Test getLongitude", 12.5, point1.getLongitude(), 0.001);
	}

	@Test
	public void testGetAltitude() {
		assertEquals("Test getAltitude", 0, point1.getAltitude(), 0.001);
		assertEquals("Test getAltitude", 100.5, point2.getAltitude(), 0.001);
	}

	@Test
	public void testSetAltitude() {
		point1.setAltitude(200.5);
		assertEquals("Test getAltitude", 200.5, point1.getAltitude(), 0.001);
	}
	
	@Test
	public void testGetDistancePoint() {
		double dist = point1.getDistance(point2);
		assertEquals("Test getDistance", 5395, dist, 5);		
	}

	@Test
	public void testGetDistancePointPoint() {
		double dist = Point.getDistance(point1, point2);
		assertEquals("Test getDistance", 5395, dist, 5);	
	}

	@Test
	public void testToString() {
		String msg;
		msg = "Latitude :\t" + "000,000" + "\n";
		msg += "Longitude :\t" + "000,000" + "\n";
		msg += "Altitude :\t" + "000,000" + "\n";
		assertEquals("Test toString", msg, point1.toString());
		
		msg = "Latitude :\t" + "048,500" + "\n";
		msg += "Longitude :\t" + "-001,600" + "\n";
		msg += "Altitude :\t" + "100,500" + "\n";
		assertEquals("Test toString", msg, point2.toString());
	}

}
