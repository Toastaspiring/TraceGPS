package classes;

import java.util.Date;

// Cette classe représente un utilisateur de l'application
// Dernière mise à jour : 22/1/2018 par Jim

public class Utilisateur {

	// attributs privés ---------------------------------------------------------------------------
	
	private int _id;					// identifiant de l'utilisateur (numéro automatique dans la BDD)
	private String _pseudo;				// pseudo de l'utilisateur
	private String _mdpSha1;			// mot de passe de l'utilisateur (hashé en SHA1)
	private String _adrMail;			// adresse mail de l'utilisateur
	private String _numTel;				// numéro de téléphone de l'utilisateur
	private int _niveau;				// niveau d'accès : 1 = utilisateur (pratiquant ou proche)    2 = administrateur
	private Date _dateCreation;			// date de création du compte
	private int _nbTraces;				// nombre de traces stockées actuellement
	private Date _dateDerniereTrace;	// date de début de la dernière trace	

	// Constructeurs ------------------------------------------------------------------------------
	
	// Constructeur sans paramètre
	public Utilisateur() {
		_id = 0;
		_pseudo = "";
		_mdpSha1 = "";
		_adrMail = "";
		_numTel = "";
		_niveau = 0;
		_dateCreation = null;
		_nbTraces = 0;
		_dateDerniereTrace = null;
	}

	// Constructeur avec paramètres
	public Utilisateur(int unId, String unPseudo, String unMdpSha1, String uneAdrMail, String unNumTel, 
			int unNiveau, Date uneDateCreation, int unNbTraces, Date uneDateDerniereTrace) {
		_id = unId;
		_pseudo = unPseudo;
		_mdpSha1 = unMdpSha1;
		_adrMail = uneAdrMail;
		_numTel = Outils.corrigerTelephone(unNumTel);
		_niveau = unNiveau;
		_dateCreation = uneDateCreation;
		_nbTraces = unNbTraces;
		_dateDerniereTrace = uneDateDerniereTrace;
	}
	
	// Accesseurs ---------------------------------------------------------------------------------
	
    public int getId()	{return _id;}
    public void setId(int unId)  {this._id = unId;}
    
	public String getPseudo()	{return _pseudo;}
	public void setPseudo(String unPseudo) {_pseudo = unPseudo;}
	
	public String getMdpSha1()	{return _mdpSha1;}
	public void setMdpSha1(String unMdpSha1) {_mdpSha1 = unMdpSha1;}
	
	public String getAdrMail()	{return _adrMail;}
	public void setAdrMail(String uneAdrMail) {_adrMail = uneAdrMail;}
	
	public String getNumTel()	{return _numTel;}
	public void setNumTel(String unNumTel) {_numTel = Outils.corrigerTelephone(unNumTel);}

	public int getNiveau()	{return _niveau;}
	public void setNiveau(int unNiveau) {_niveau = unNiveau;}

	public Date getDateCreation()	{return _dateCreation;}
	public void setDateCreation(Date uneDateCreation) {_dateCreation = uneDateCreation;}

	public int getNbTraces()	{return _nbTraces;}
	public void setNbTraces(int unNbTraces) {_nbTraces = unNbTraces;}
	
	public Date getDateDerniereTrace()	{return _dateDerniereTrace;}
	public void setDateDerniereTrace(Date uneDateDerniereTrace) {_dateDerniereTrace = uneDateDerniereTrace;}
	
	// Méthodes publiques -------------------------------------------------------------------------
	
	// Fournit une chaine contenant toutes les données de l'objet
	public String toString() {
		String msg = "";
	    msg += "id : " + _id + "\n";
	    msg += "pseudo : " + _pseudo + "\n";
	    msg += "mdpSha1 : " + _mdpSha1 + "\n";
	    msg += "adrMail : " + _adrMail + "\n";
	    msg += "numTel : " + _numTel + "\n";
	    msg += "niveau : " + _niveau + "\n";
	    if (this._dateCreation != null)
	    	msg += "dateCreation : " + Outils.formaterDateHeureFR(_dateCreation) + "\n";
	    msg += "nbTraces : " + _nbTraces + "\n";
	    if (this._dateDerniereTrace != null)
	    	msg += "dateDerniereTrace : " + Outils.formaterDateHeureFR(_dateDerniereTrace) + "\n";
	    
	    return msg;
	}
	
}
