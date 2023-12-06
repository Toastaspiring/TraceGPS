package testsunitaires;

import static org.junit.Assert.*;

import java.text.ParseException;
import java.util.Date;

import org.junit.Before;
import org.junit.Test;

import classes.Outils;
import classes.Utilisateur;

public class UtilisateurTest {
	
	private Utilisateur utilisateur1;
	private Utilisateur utilisateur2;
	
	@Before
	public void setUp() throws Exception {
		utilisateur1 = new Utilisateur();
		
		int unId = 111;
		String unPseudo = "toto";
		String unMdpSha1 = "abcdef";
		String uneAdrMail = "toto@free.fr";
		String unNumTel = "1122334455";
		int unNiveau = 1;
		Date uneDateCreation = Outils.convertirEnDateHeure("21/06/2016 14:00:00");
		int unNbTraces = 3;
		Date uneDateDerniereTrace = Outils.convertirEnDateHeure("28/06/2016 14:00:00");
		utilisateur2 = new Utilisateur(unId, unPseudo, unMdpSha1, uneAdrMail, unNumTel, unNiveau, uneDateCreation, unNbTraces, uneDateDerniereTrace);
	}

	@Test
	public void testGetId() {
		assertEquals(0, utilisateur1.getId());
	    assertEquals(111, utilisateur2.getId());
	}

	@Test
	public void testSetId() {
		utilisateur1.setId(99);
	    assertEquals(99, utilisateur1.getId());
	}

	@Test
	public void testGetPseudo() {
		assertEquals("", utilisateur1.getPseudo());
	    assertEquals("toto", utilisateur2.getPseudo());
	}

	@Test
	public void testSetPseudo() {
		utilisateur1.setPseudo("newPseudo");
	    assertEquals("newPseudo", utilisateur1.getPseudo());
	}

	@Test
	public void testGetMdpSha1() {
		assertEquals("", utilisateur1.getMdpSha1());
		assertEquals("abcdef", utilisateur2.getMdpSha1());
	}

	@Test
	public void testSetMdpSha1() {
		utilisateur1.setMdpSha1("xyz123");
		assertEquals("xyz123", utilisateur1.getMdpSha1());
	}

	@Test
	public void testGetAdrMail() {
		assertEquals("", utilisateur1.getAdrMail());
		assertEquals("toto@free.fr", utilisateur2.getAdrMail());
	}

	@Test
	public void testSetAdrMail() {
		utilisateur1.setAdrMail("tata@gmail.com");
		assertEquals("tata@gmail.com", utilisateur1.getAdrMail());
	}

	@Test
	public void testGetNumTel() {
		assertEquals("", utilisateur1.getNumTel()); // Vérifie si le numéro est vide par défaut
	    assertEquals("11.22.33.44.55", utilisateur2.getNumTel());
	}

	@Test
	public void testSetNumTel() {
		utilisateur1.setNumTel("9988776655");
	    assertEquals("99.88.77.66.55", utilisateur1.getNumTel());
	}

	@Test
	public void testGetNiveau() {
		assertEquals(0, utilisateur1.getNiveau());
		assertEquals(1, utilisateur2.getNiveau());
	}

	@Test
	public void testSetNiveau() {
		utilisateur1.setNiveau(2);
		assertEquals(2, utilisateur1.getNiveau());
	}

	@Test
	public void testGetDateCreation() throws ParseException {
		assertEquals(null, utilisateur1.getDateCreation());
		assertEquals(Outils.convertirEnDateHeure("21/06/2016 14:00:00"), utilisateur2.getDateCreation());
	}

	@Test
	public void testSetDateCreation() throws ParseException {
		Date newDate = Outils.convertirEnDateHeure("25/12/2020 12:00:00");
		utilisateur1.setDateCreation(newDate);
		assertEquals(newDate, utilisateur1.getDateCreation());
	}

	@Test
	public void testGetNbTraces() {
		assertEquals(0, utilisateur1.getNbTraces());
		assertEquals(3, utilisateur2.getNbTraces());
	}

	@Test
	public void testSetNbTraces() {
		utilisateur1.setNbTraces(5);
		assertEquals(5, utilisateur1.getNbTraces());
	}

	@Test
	public void testGetDateDerniereTrace() throws ParseException {
		assertEquals(null, utilisateur1.getDateDerniereTrace());
		assertEquals(Outils.convertirEnDateHeure("28/06/2016 14:00:00"), utilisateur2.getDateDerniereTrace());
	}

	@Test
	public void testSetDateDerniereTrace() throws ParseException {
		Date newDate = Outils.convertirEnDateHeure("01/01/2023 00:00:00");
		utilisateur1.setDateDerniereTrace(newDate);
		assertEquals(newDate, utilisateur1.getDateDerniereTrace());
	}

	@Test
	public void testToString() {
		String msg = "";
	    msg += "id : " + "0" + "\n";
	    msg += "pseudo : " + "" + "\n";
	    msg += "mdpSha1 : " + "" + "\n";
	    msg += "adrMail : " + "" + "\n";
	    msg += "numTel : " + "" + "\n";
	    msg += "niveau : " + "0" + "\n";
	    msg += "nbTraces : " + "0" + "\n";
	    assertEquals("Test toString", msg, utilisateur1.toString());
	    
		msg = "";
	    msg += "id : " + "111" + "\n";
	    msg += "pseudo : " + "toto" + "\n";
	    msg += "mdpSha1 : " + "abcdef" + "\n";
	    msg += "adrMail : " + "toto@free.fr" + "\n";
	    msg += "numTel : " + "11.22.33.44.55" + "\n";
	    msg += "niveau : " + "1" + "\n";
	    msg += "dateCreation : " + "21/06/2016 14:00:00" + "\n";
	    msg += "nbTraces : " + "3" + "\n";
	    msg += "dateDerniereTrace : " + "28/06/2016 14:00:00" + "\n";
	    assertEquals("Test toString", msg, utilisateur2.toString());
	}

}
