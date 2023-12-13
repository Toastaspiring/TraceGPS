<?php
namespace modele;

use Exception;
// Projet TraceGPS
// fichier : modele/DAO.php   (DAO : Data Access Object)
// Rôle : fournit des méthodes d'accès à la bdd tracegps (projet TraceGPS) au moyen de l'objet \PDO
// modifié par dP le 12/8/2021

// liste des méthodes déjà développées (dans l'ordre d'apparition dans le fichier) :

// __construct() : le constructeur crée la connexion $cnx à la base de données
// __destruct() : le destructeur ferme la connexion $cnx à la base de données
// getNiveauConnexion($login, $mdp) : fournit le niveau (0, 1 ou 2) d'un utilisateur identifié par $login et $mdp
// existePseudoUtilisateur($pseudo) : fournit true si le pseudo $pseudo existe dans la table tracegps_utilisateurs, false sinon
// getUnUtilisateur($login) : fournit un objet Utilisateur à partir de $login (son pseudo ou son adresse mail)
// getTousLesUtilisateurs() : fournit la collection de tous les utilisateurs (de niveau 1)
// creerUnUtilisateur($unUtilisateur) : enregistre l'utilisateur $unUtilisateur dans la bdd
// modifierMdpUtilisateur($login, $nouveauMdp) : enregistre le nouveau mot de passe $nouveauMdp de l'utilisateur $login daprès l'avoir hashé en SHA1
// supprimerUnUtilisateur($login) : supprime l'utilisateur $login (son pseudo ou son adresse mail) dans la bdd, ainsi que ses traces et ses autorisations
// envoyerMdp($login, $nouveauMdp) : envoie un mail à l'utilisateur $login avec son nouveau mot de passe $nouveauMdp

// liste des méthodes restant à développer :

// existeAdrMailUtilisateur($adrmail) : fournit true si l'adresse mail $adrMail existe dans la table tracegps_utilisateurs, false sinon
// getLesUtilisateursAutorises($idUtilisateur) : fournit la collection  des utilisateurs (de niveau 1) autorisés à suivre l'utilisateur $idUtilisateur
// getLesUtilisateursAutorisant($idUtilisateur) : fournit la collection  des utilisateurs (de niveau 1) autorisant l'utilisateur $idUtilisateur à voir leurs parcours
// autoriseAConsulter($idAutorisant, $idAutorise) : vérifie que l'utilisateur $idAutorisant) autorise l'utilisateur $idAutorise à consulter ses traces
// creerUneAutorisation($idAutorisant, $idAutorise) : enregistre l'autorisation ($idAutorisant, $idAutorise) dans la bdd
// supprimerUneAutorisation($idAutorisant, $idAutorise) : supprime l'autorisation ($idAutorisant, $idAutorise) dans la bdd
// getLesPointsDeTrace($idTrace) : fournit la collection des points de la trace $idTrace
// getUneTrace($idTrace) : fournit un objet Trace à partir de identifiant $idTrace
// getToutesLesTraces() : fournit la collection de toutes les traces
// getLesTraces($idUtilisateur) : fournit la collection des traces de l'utilisateur $idUtilisateur
// getLesTracesAutorisees($idUtilisateur) : fournit la collection des traces que l'utilisateur $idUtilisateur a le droit de consulter
// creerUneTrace(Trace $uneTrace) : enregistre la trace $uneTrace dans la bdd
// terminerUneTrace($idTrace) : enregistre la fin de la trace d'identifiant $idTrace dans la bdd ainsi que la date de fin
// supprimerUneTrace($idTrace) : supprime la trace d'identifiant $idTrace dans la bdd, ainsi que tous ses points
// creerUnPointDeTrace(PointDeTrace $unPointDeTrace) : enregistre le point $unPointDeTrace dans la bdd

// certaines méthodes nécessitent les classes suivantes :
include_once ('Utilisateur.php');
include_once ('Trace.php');
include_once ('PointDeTrace.php');
include_once ('Point.php');
include_once ('Outils.php');

// inclusion des paramètres de l'application
include_once ('parametres.php');
// début de la classe DAO (Data Access Object)
class DAO
{
    // ------------------------------------------------------------------------------------------------------
    // ---------------------------------- Membres privés de la classe ---------------------------------------
    // ------------------------------------------------------------------------------------------------------
    
    private $cnx;				// la connexion à la base de données
    
    // ------------------------------------------------------------------------------------------------------
    // ---------------------------------- Constructeur et destructeur ---------------------------------------
    // ------------------------------------------------------------------------------------------------------
    public function __construct() {
        global $PARAM_HOTE, $PARAM_PORT, $PARAM_BDD, $PARAM_USER, $PARAM_PWD;
        try
        {	$this->cnx = new \PDO("mysql:host=" . $PARAM_HOTE . ";port=" . $PARAM_PORT . ";dbname=" . $PARAM_BDD,
            $PARAM_USER,
            $PARAM_PWD);
        return true;
        }
        catch (Exception $ex)
        {	echo ("Echec de la connexion a la base de donnees <br>");
        echo ("Erreur numero : " . $ex->getCode() . "<br />" . "Description : " . $ex->getMessage() . "<br>");
        echo ("PARAM_HOTE = " . $PARAM_HOTE);
        return false;
        }
    }
    
    public function __destruct() {
        // ferme la connexion à MySQL :
        unset($this->cnx);
    }
    
    // ------------------------------------------------------------------------------------------------------
    // -------------------------------------- Méthodes d'instances ------------------------------------------
    // ------------------------------------------------------------------------------------------------------
    
    // fournit le niveau (0, 1 ou 2) d'un utilisateur identifié par $pseudo et $mdpSha1
    // cette fonction renvoie un entier :
    //     0 : authentification incorrecte
    //     1 : authentification correcte d'un utilisateur (pratiquant ou personne autorisée)
    //     2 : authentification correcte d'un administrateur
    // modifié par dP le 11/1/2018
    public function getNiveauConnexion($pseudo, $mdpSha1) {
        // préparation de la requête de recherche
        $txt_req = "Select niveau from tracegps_utilisateurs";
        $txt_req .= " where pseudo = :pseudo";
        $txt_req .= " and mdpSha1 = :mdpSha1";
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et de ses paramètres
        $req->bindValue("pseudo", $pseudo, \PDO::PARAM_STR);
        $req->bindValue("mdpSha1", $mdpSha1, \PDO::PARAM_STR);
        // extraction des données
        $req->execute();
        $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        // traitement de la réponse
        $reponse = 0;
        if ($uneLigne) {
            $reponse = $uneLigne->niveau;
        }
        // libère les ressources du jeu de données
        $req->closeCursor();
        // fourniture de la réponse
        return $reponse;
    }
    
    // fournit true si le pseudo $pseudo existe dans la table tracegps_utilisateurs, false sinon
    // modifié par dP le 27/12/2017
    public function existePseudoUtilisateur($pseudo) {
        // préparation de la requête de recherche
        $txt_req = "Select count(*) from tracegps_utilisateurs where pseudo = :pseudo";
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et de ses paramètres
        $req->bindValue("pseudo", $pseudo, \PDO::PARAM_STR);
        // exécution de la requête
        $req->execute();
        $nbReponses = $req->fetchColumn(0);
        // libère les ressources du jeu de données
        $req->closeCursor();
        
        // fourniture de la réponse
        if ($nbReponses == 0) {
            return false;
        }
        else {
            return true;
        }
    }
    
    // fournit un objet Utilisateur à partir de son pseudo $pseudo
    // fournit la valeur null si le pseudo n'existe pas
    // modifié par dP le 9/1/2018
    public function getUnUtilisateur($pseudo) {
        // préparation de la requête de recherche
        $txt_req = "Select id, pseudo, mdpSha1, adrMail, numTel, niveau, dateCreation, nbTraces, dateDerniereTrace";
        $txt_req .= " from tracegps_vue_utilisateurs";
        $txt_req .= " where pseudo = :pseudo";
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et de ses paramètres
        $req->bindValue("pseudo", $pseudo, \PDO::PARAM_STR);
        // extraction des données
        $req->execute();
        $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        // libère les ressources du jeu de données
        $req->closeCursor();
        
        // traitement de la réponse
        if ( ! $uneLigne) {
            return null;
        }
        else {
            // création d'un objet Utilisateur
            $unId = mb_convert_encoding($uneLigne->id, "UTF-8");
            $unPseudo = mb_convert_encoding($uneLigne->pseudo, "UTF-8");
            $unMdpSha1 = mb_convert_encoding($uneLigne->mdpSha1, "UTF-8");
            $uneAdrMail = mb_convert_encoding($uneLigne->adrMail, "UTF-8");
            $unNumTel = mb_convert_encoding($uneLigne->numTel, "UTF-8");
            $unNiveau = mb_convert_encoding($uneLigne->niveau, "UTF-8");
            $uneDateCreation = mb_convert_encoding($uneLigne->dateCreation, "UTF-8");
            $unNbTraces = mb_convert_encoding($uneLigne->nbTraces, "UTF-8");
            if (isset($uneLigne->dateDerniereTrace)) {
                $uneDateDerniereTrace = mb_convert_encoding($uneLigne->dateDerniereTrace, "UTF-8");
            } else {
                $uneDateDerniereTrace ="";
            }
            $unUtilisateur = new Utilisateur($unId, $unPseudo, $unMdpSha1, $uneAdrMail, $unNumTel, $unNiveau, $uneDateCreation, $unNbTraces, $uneDateDerniereTrace);
            return $unUtilisateur;
        }
    }
    
    // fournit la collection  de tous les utilisateurs (de niveau 1)
    // le résultat est fourni sous forme d'une collection d'objets Utilisateur
    // modifié par dP le 27/12/2017
    public function getTousLesUtilisateurs() {
        // préparation de la requête de recherche
        $txt_req = "Select id, pseudo, mdpSha1, adrMail, numTel, niveau, dateCreation, nbTraces, dateDerniereTrace";
        $txt_req .= " from tracegps_vue_utilisateurs";
        $txt_req .= " where niveau = 1";
        $txt_req .= " order by pseudo";
        
        $req = $this->cnx->prepare($txt_req);
        // extraction des données
        $req->execute();
        $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        
        // construction d'une collection d'objets Utilisateur
        $lesUtilisateurs = array();
        // tant qu'une ligne est trouvée :
        while ($uneLigne) {
            // création d'un objet Utilisateur
            $unId = mb_convert_encoding($uneLigne->id, "UTF-8");
            $unPseudo = mb_convert_encoding($uneLigne->pseudo, "UTF-8");
            $unMdpSha1 = mb_convert_encoding($uneLigne->mdpSha1, "UTF-8");
            $uneAdrMail = mb_convert_encoding($uneLigne->adrMail, "UTF-8");
            $unNumTel = mb_convert_encoding($uneLigne->numTel, "UTF-8");
            $unNiveau = mb_convert_encoding($uneLigne->niveau, "UTF-8");
            $uneDateCreation = mb_convert_encoding($uneLigne->dateCreation, "UTF-8");
            $unNbTraces = mb_convert_encoding($uneLigne->nbTraces, "UTF-8");
            if (isset($uneLigne->dateDerniereTrace)) {
                $uneDateDerniereTrace = mb_convert_encoding($uneLigne->dateDerniereTrace, "UTF-8");
            } else {
                $uneDateDerniereTrace ="";
            }
            
            $unUtilisateur = new Utilisateur($unId, $unPseudo, $unMdpSha1, $uneAdrMail, $unNumTel, $unNiveau, $uneDateCreation, $unNbTraces, $uneDateDerniereTrace);
            // ajout de l'utilisateur à la collection
            $lesUtilisateurs[] = $unUtilisateur;
            // extrait la ligne suivante
            $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        }
        // libère les ressources du jeu de données
        $req->closeCursor();
        // fourniture de la collection
        return $lesUtilisateurs;
    }
    
    // enregistre l'utilisateur $unUtilisateur dans la bdd
    // fournit true si l'enregistrement s'est bien effectué, false sinon
    // met à jour l'objet $unUtilisateur avec l'id (auto_increment) attribué par le SGBD
    // modifié par dP le 9/1/2018
    public function creerUnUtilisateur($unUtilisateur) {
        // on teste si l'utilisateur existe déjà
        if ($this->existePseudoUtilisateur($unUtilisateur->getPseudo())) return false;
        
        // préparation de la requête
        $txt_req1 = "insert into tracegps_utilisateurs (pseudo, mdpSha1, adrMail, numTel, niveau, dateCreation)";
        $txt_req1 .= " values (:pseudo, :mdpSha1, :adrMail, :numTel, :niveau, :dateCreation)";
        $req1 = $this->cnx->prepare($txt_req1);
        // liaison de la requête et de ses paramètres
        $req1->bindValue("pseudo", mb_convert_encoding($unUtilisateur->getPseudo(), "ISO-8859-1"), \PDO::PARAM_STR);
        $req1->bindValue("mdpSha1", sha1($unUtilisateur->getMdpsha1()), \PDO::PARAM_STR);
        $req1->bindValue("adrMail", mb_convert_encoding($unUtilisateur->getAdrmail(), "ISO-8859-1"), \PDO::PARAM_STR);
        $req1->bindValue("numTel", mb_convert_encoding($unUtilisateur->getNumTel(), "ISO-8859-1"), \PDO::PARAM_STR);
        $req1->bindValue("niveau", mb_convert_encoding($unUtilisateur->getNiveau(), "ISO-8859-1"), \PDO::PARAM_INT);
        $req1->bindValue("dateCreation", mb_convert_encoding($unUtilisateur->getDateCreation(), "ISO-8859-1"), \PDO::PARAM_STR);
        // exécution de la requête
        $ok = $req1->execute();
        // sortir en cas d'échec
        if ( ! $ok) { return false; }
        
        // recherche de l'identifiant (auto_increment) qui a été attribué à la trace
        $unId = $this->cnx->lastInsertId();
        $unUtilisateur->setId($unId);
        return true;
    }
    
    // enregistre le nouveau mot de passe $nouveauMdp de l'utilisateur $pseudo daprès l'avoir hashé en SHA1
    // fournit true si la modification s'est bien effectuée, false sinon
    // modifié par dP le 9/1/2018
    public function modifierMdpUtilisateur($pseudo, $nouveauMdp) {
        // préparation de la requête
        $txt_req = "update tracegps_utilisateurs set mdpSha1 = :nouveauMdp";
        $txt_req .= " where pseudo = :pseudo";
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et de ses paramètres
        $req->bindValue("nouveauMdp", sha1($nouveauMdp), \PDO::PARAM_STR);
        $req->bindValue("pseudo", $pseudo, \PDO::PARAM_STR);
        // exécution de la requête
        $ok = $req->execute();
        return $ok;
    }
    
    // supprime l'utilisateur $pseudo dans la bdd, ainsi que ses traces et ses autorisations
    // fournit true si l'effacement s'est bien effectué, false sinon
    // modifié par dP le 9/1/2018
    public function supprimerUnUtilisateur($pseudo) {
        $unUtilisateur = $this->getUnUtilisateur($pseudo);
        if ($unUtilisateur == null) {
            return false;
        }
        else {
            $idUtilisateur = $unUtilisateur->getId();
            
            // suppression des traces de l'utilisateur (et des points correspondants)
            $lesTraces = $this->getLesTraces($idUtilisateur);
            if($lesTraces != null)
            {
                foreach ($lesTraces as $uneTrace) {
                    $this->supprimerUneTrace($uneTrace->getId());
                }
            }
            // préparation de la requête de suppression des autorisations
            $txt_req1 = "delete from tracegps_autorisations" ;
            $txt_req1 .= " where idAutorisant = :idUtilisateur or idAutorise = :idUtilisateur";
            $req1 = $this->cnx->prepare($txt_req1);
            // liaison de la requête et de ses paramètres
            $req1->bindValue("idUtilisateur", mb_convert_encoding($idUtilisateur, "ISO-8859-1"), \PDO::PARAM_INT);
            // exécution de la requête
            $ok = $req1->execute();
            
            // préparation de la requête de suppression de l'utilisateur
            $txt_req2 = "delete from tracegps_utilisateurs" ;
            $txt_req2 .= " where pseudo = :pseudo";
            $req2 = $this->cnx->prepare($txt_req2);
            // liaison de la requête et de ses paramètres
            $req2->bindValue("pseudo", mb_convert_encoding($pseudo, "ISO-8859-1"), \PDO::PARAM_STR);
            // exécution de la requête
            $ok = $req2->execute();
            return $ok;
        }
    }
    
    // envoie un mail à l'utilisateur $pseudo avec son nouveau mot de passe $nouveauMdp
    // retourne true si envoi correct, false en cas de problème d'envoi
    // modifié par dP le 9/1/2018
    public function envoyerMdp($pseudo, $nouveauMdp) {
        $ADR_MAIL_EMETTEUR = "delasalle.sio.eleves@gmail.com";
        // si le pseudo n'est pas dans la table tracegps_utilisateurs :
        if ( $this->existePseudoUtilisateur($pseudo) == false ) return false;
        
        // recherche de l'adresse mail
        $adrMail = $this->getUnUtilisateur($pseudo)->getAdrMail();
        
        // envoie un mail à l'utilisateur avec son nouveau mot de passe
        $sujet = "Modification de votre mot de passe d'accès au service TraceGPS";
        $message = "Cher(chère) " . $pseudo . "\n\n";
        $message .= "Votre mot de passe d'accès au service service TraceGPS a été modifié.\n\n";
        $message .= "Votre nouveau mot de passe est : " . $nouveauMdp ;
        $ok = Outils::envoyerMail ($adrMail, $sujet, $message, $ADR_MAIL_EMETTEUR);
        return $ok;
    }
    
    // Le code restant à développer va être réparti entre les membres de l'équipe de développement.
    // Afin de limiter les conflits avec GitHub, il est décidé d'attribuer une zone de ce fichier à chaque développeur.
    // Développeur 1 : lignes 350 à 549
    // Développeur 2 : lignes 550 à 749
    // Développeur 3 : lignes 750 à 949
    // Développeur 4 : lignes 950 à 1150
    
    // Quelques conseils pour le travail collaboratif :
    // avant d'attaquer un cycle de développement (début de séance, nouvelle méthode, ...), faites un Pull pour récupérer
    // la dernière version du fichier.
    // Après avoir testé et validé une méthode, faites un commit et un push pour transmettre cette version aux autres développeurs.
    
    
    
    
    
    
    
    // --------------------------------------------------------------------------------------
    // début de la zone attribuée au développeur 1 (Dupon de ligones) : lignes 350 à 549
    // --------------------------------------------------------------------------------------
    
    public function existeAdrMailUtilisateur($adrMail) {
        // préparation de la requête de recherche
        $txt_req = "Select count(*) from tracegps_utilisateurs where adrMail = :adrMail";
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et de ses paramètres
        $req->bindValue("adrMail", $adrMail, \PDO::PARAM_STR);
        // exécution de la requête
        $req->execute();
        $nbReponses = $req->fetchColumn(0);
        // libère les ressources du jeu de données
        $req->closeCursor();
        
        // fourniture de la réponse
        if ($nbReponses == 0) {
            return false;
        }
        else {
            return true;
        }
    }
    
    public function getLesUtilisateursAutorisant($idUtilisateur) {
        // préparation de la requête de recherche
        $txt_req = "Select * from tracegps_autorisations join tracegps_vue_utilisateurs on tracegps_autorisations.idAutorisant = tracegps_vue_utilisateurs.id where idAutorise = :idUtilisateur;";
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et dde ses paramètres
        $req->bindValue("idUtilisateur", $idUtilisateur, \PDO::PARAM_STR);
        // execution de la requête
        $req->execute();
        $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        
        // construction d'une collection d'objets Utilisateur
        $lesUtilisateursAutorise = array();
        // tant qu'une ligne est trouvée :
        while ($uneLigne) {
            // création d'un objet Utilisateur
            $unId = mb_convert_encoding($uneLigne->id, "UTF-8");
            $unPseudo = mb_convert_encoding($uneLigne->pseudo, "UTF-8");
            $unMdpSha1 = mb_convert_encoding($uneLigne->mdpSha1, "UTF-8");
            $uneAdrMail = mb_convert_encoding($uneLigne->adrMail, "UTF-8");
            $unNumTel = mb_convert_encoding($uneLigne->numTel, "UTF-8");
            $unNiveau = mb_convert_encoding($uneLigne->niveau, "UTF-8");
            $uneDateCreation = mb_convert_encoding($uneLigne->dateCreation, "UTF-8");
            $unNbTraces = mb_convert_encoding($uneLigne->nbTraces, "UTF-8");
            $uneDateDerniereTrace = mb_convert_encoding($uneLigne->dateDerniereTrace, "UTF-8");
            
            $unUtilisateur = new Utilisateur($unId, $unPseudo, $unMdpSha1, $uneAdrMail, $unNumTel, $unNiveau, $uneDateCreation, $unNbTraces, $uneDateDerniereTrace);
            // ajout de l'utilisateur à la collection
            $lesUtilisateursAutorise[] = $unUtilisateur;
            // extrait la ligne suivante
            $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        }
        // libère les ressources du jeu de données
        $req->closeCursor();
        // fourniture de la collection
        return $lesUtilisateursAutorise;
    }
    
    
    
    
    public function getLesUtilisateursAutorises($idUtilisateur) {
        // préparation de la requête de recherche
        $txt_req = "Select * from tracegps_autorisations join tracegps_vue_utilisateurs on tracegps_autorisations.idAutorise = tracegps_vue_utilisateurs.id where idAutorisant = :idUtilisateur;";
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et dde ses paramètres
        $req->bindValue("idUtilisateur", $idUtilisateur, \PDO::PARAM_INT);
        // execution de la requête
        $req->execute();
        $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        
        // construction d'une collection d'objets Utilisateur
        $lesUtilisateursAutorise = array();
        // tant qu'une ligne est trouvée :
        while ($uneLigne) {
            // création d'un objet Utilisateur
            $unId = mb_convert_encoding($uneLigne->id, "UTF-8");
            $unPseudo = mb_convert_encoding($uneLigne->pseudo, "UTF-8");
            $unMdpSha1 = mb_convert_encoding($uneLigne->mdpSha1, "UTF-8");
            $uneAdrMail = mb_convert_encoding($uneLigne->adrMail, "UTF-8");
            $unNumTel = mb_convert_encoding($uneLigne->numTel, "UTF-8");
            $unNiveau = mb_convert_encoding($uneLigne->niveau, "UTF-8");
            $uneDateCreation = mb_convert_encoding($uneLigne->dateCreation, "UTF-8");
            $unNbTraces = mb_convert_encoding($uneLigne->nbTraces, "UTF-8");
            if ($uneLigne->dateDerniereTrace == null)
            {
             $uneDateDerniereTrace = null; 
            }else{
                $uneDateDerniereTrace = mb_convert_encoding($uneLigne->dateDerniereTrace, "UTF-8");
            }
            
            $unUtilisateur = new Utilisateur($unId, $unPseudo, $unMdpSha1, $uneAdrMail, $unNumTel, $unNiveau, $uneDateCreation, $unNbTraces, $uneDateDerniereTrace);
            // ajout de l'utilisateur à la collection
            $lesUtilisateursAutorise[] = $unUtilisateur;
            // extrait la ligne suivante
            $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        }
        // libère les ressources du jeu de données
        $req->closeCursor();
        // fourniture de la collection
        return $lesUtilisateursAutorise;
    }
    
    
    public function autoriseAConsulter($idAutorisant, $idAutorise) {
        // préparation de la requête de recherche
        $txt_req = "Select count(*) from tracegps_autorisations where idAutorisant = :idAutorisant and idAutorise = :idAutorise;";
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et dde ses paramètres
        $req->bindValue("idAutorisant", $idAutorisant, \PDO::PARAM_INT);
        $req->bindValue("idAutorise", $idAutorise, \PDO::PARAM_INT);
        // execution de la requête
        $req->execute();
        $nbReponses = $req->fetchColumn(0);
        // libère les ressources du jeu de données
        $req->closeCursor();
        
        // fourniture de la réponse
        if ($nbReponses == 0) {
            return false;
        }
        else {
            return true;
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    // --------------------------------------------------------------------------------------
    // début de la zone attribuée au développeur 2 (Nino) : lignes 550 à 749
    // --------------------------------------------------------------------------------------

    // Par Louis ( NINO FAUT CHECK SI L'ENTRE EXISTE AVANT D'INSERER SINON TU FAIT DES CONFLIT !!!)
    public function creerUneAutorisation($idAutorisant, $idAutorise)
    {
        // Vérifie si un enregistrement avec les mêmes valeurs pour idAutorisant et idAutorise existe déjà ( IMPORTANT )
        $txt_req2 = "SELECT COUNT(*) AS nb FROM tracegps_autorisations WHERE idAutorisant = :idAutorisant AND idAutorise = :idAutorise";
        $req2 = $this->cnx->prepare($txt_req2);
        $req2->bindValue("idAutorisant", $idAutorisant, \PDO::PARAM_INT);
        $req2->bindValue("idAutorise", $idAutorise, \PDO::PARAM_INT);
        $req2->execute();

        $nb = $req2->fetchColumn();

        if ($nb > 0) {
            // Un enregistrement avec les mêmes valeurs pour idAutorisant et idAutorise existe déjà
            return false;
        }

        // Insère le nouvel enregistrement
        $txt_req1 = "INSERT INTO tracegps_autorisations (idAutorisant, idAutorise) VALUES (:idAutorisant, :idAutorise)";
        $req1 = $this->cnx->prepare($txt_req1);
        $req1->bindValue("idAutorisant", $idAutorisant, \PDO::PARAM_INT);
        $req1->bindValue("idAutorise", $idAutorise, \PDO::PARAM_INT);
        $ok = $req1->execute();

        if (!$ok) {
            // L'insertion a échoué
            return false;
        }
        return true;
    }

    
    public function supprimerUneAutorisation($idAutorisant, $idAutorise)
    {
        $txt_req1 = "DELETE FROM tracegps_autorisations";
        $txt_req1 .= " WHERE idAutorisant=:idAutorisant AND idAutorise=:idAutorise";
        $req1 = $this->cnx->prepare($txt_req1);
        
        $req1->bindValue("idAutorisant", $idAutorisant, \PDO::PARAM_STR);
        $req1->bindValue("idAutorise", $idAutorise, \PDO::PARAM_STR);
        
        
        $ok = $req1->execute();
        // sortir en cas d'échec
        if ( ! $ok)
        {
            return false;
        }
        return true;
    }

    public function getLesPointsDeTrace($idTrace)
    {
        //$rtrace = "Select tracegps_traces.id,latitude,longitude,altitude, dateHeure, rythmecardio,(dateFin - dateDebut) as TempsCumule from tracegps_points inner join tracegps_traces on tracegps_points.idTrace = tracegps_traces.id ";
        
        
        
        $rtrace = "SELECT idTrace,id,latitude,longitude,altitude, dateHeure, rythmeCardio ";
        $rtrace .= "FROM tracegps_points";
        $rtrace .= " WHERE tracegps_points.idTrace = :idTrace";
        $rtrace .= " ORDER BY tracegps_points.id";
        
        $req = $this->cnx->prepare($rtrace);
        $req->bindValue("idTrace", $idTrace, \PDO::PARAM_INT);
        $req->execute();
        $uneligne = $req->fetch(\PDO::FETCH_OBJ);
        
        $lespointsdetrace = array();
        
        while ($uneligne) {
            
            $unID = mb_convert_encoding($uneligne -> id, "UTF-8");
            $uneLatitude = mb_convert_encoding($uneligne -> latitude, "UTF-8");
            $uneLongitude = mb_convert_encoding($uneligne -> longitude, "UTF-8");
            $uneAltitude = mb_convert_encoding($uneligne -> altitude, "UTF-8");
            $uneDateHeure = mb_convert_encoding($uneligne -> dateHeure, "UTF-8");
            $unRythmeCardio = mb_convert_encoding($uneligne -> rythmeCardio, "UTF-8");
            
            
            
            $unPointDeTrace = new PointDeTrace($idTrace, $unID, $uneLatitude, $uneLongitude, $uneAltitude, $uneDateHeure, $unRythmeCardio, 0, 0, 0);
            
            $lespointsdetrace[] = $unPointDeTrace;
            $uneligne = $req->fetch(\PDO::FETCH_OBJ);
        }
        
        $req->closeCursor();
        return $lespointsdetrace;
        
    }


    public function creerUnPointDeTrace($unPointDeTrace) {

        //verifie si un enregistrement avec les même valeur existe ( id car cle primaire ne peut être dupliquer)
        $txt_req2 = "SELECT COUNT(*) AS nb FROM tracegps_points WHERE id = :id";
        $req2 = $this->cnx->prepare($txt_req2);
        $req2->bindValue("id",$unPointDeTrace->getId());
        $req2->execute();

        $nb = $req2->fetchColumn();

        if ($nb > 0) {
            return null;
        }

        // on teste si l'utilisateur existe déjà
        // prÃ©paration de la requÃªte idTrace ,id, latitude, longitude, altitude, dateHeure, rythmeCardio, tempsCumule, distanceCumulee, vitesse
        $txt_req1 = "insert into tracegps_points (idTrace ,id, latitude, longitude, altitude, dateHeure, rythmeCardio)";
        $txt_req1 .= " values (:idTrace, :id, :latitude, :longitude, :altitude, :dateHeure, :rythmeCardio)";
        $req1 = $this->cnx->prepare($txt_req1);
        // liaison de la requÃªte et de ses paramÃ¨tres
        $req1 -> bindvalue ('id',mb_convert_encoding($unPointDeTrace ->getId(),"UTF-8"),\PDO::PARAM_STR);
        $req1 -> bindvalue ('idTrace',mb_convert_encoding($unPointDeTrace ->getIdTrace(),"UTF-8"),\PDO::PARAM_STR);
        $req1 -> bindvalue ('latitude',mb_convert_encoding($unPointDeTrace ->getLatitude(),"UTF-8"),\PDO::PARAM_STR);
        $req1 -> bindvalue ('longitude',mb_convert_encoding($unPointDeTrace ->getLongitude(),"UTF-8"),\PDO::PARAM_STR);
        $req1 -> bindvalue ('altitude',mb_convert_encoding($unPointDeTrace ->getAltitude(),"UTF-8"),\PDO::PARAM_STR);
        $req1 -> bindvalue ('dateHeure',mb_convert_encoding($unPointDeTrace ->getDateHeure(),"UTF-8"),\PDO::PARAM_STR);
        $req1 -> bindvalue ("rythmeCardio",mb_convert_encoding($unPointDeTrace ->getRythmeCardio(), "UTF-8"),\PDO::PARAM_INT);
        // exÃ©cution de la requÃªte
        $ok = $req1->execute();
        // retourne true ou false
        return $ok;
    }




    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    // --------------------------------------------------------------------------------------
    // début de la zone attribuée au développeur 3 (Ethan) : lignes 750 à 949
    // --------------------------------------------------------------------------------------
    
    // fournit un objet Trace à partir de l'identifiant $idTrace
    // fournit la valeur null si l'identifiant $idTrace n'existe pas
    function getUneTrace($idTrace) {
        // préparation de la requête de recherche
        $txt_req = "Select id, dateDebut, dateFin, terminee, idUtilisateur";
        $txt_req = $txt_req . " from tracegps_traces";
        $txt_req = $txt_req . " where id = :idTrace";
        
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et de ses paramètres
        $req->bindValue("idTrace", $idTrace, \PDO::PARAM_INT);
        // extraction des données
        $req->execute();
        $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        // libère les ressources du jeu de données
        $req->closeCursor();
        
        // traitement de la réponse
        if ( ! $uneLigne) {
            return null;
        }
        else {
            // création d'un objet Trace
            $unId = mb_convert_encoding($uneLigne->id, "UTF-8");
            $uneDateHeureDebut = mb_convert_encoding($uneLigne->dateDebut, "UTF-8");
            $uneDateHeureFin = mb_convert_encoding($uneLigne->dateFin, "UTF-8");
            $terminee = mb_convert_encoding($uneLigne->terminee, "UTF-8");
            $unIdUtilisateur = mb_convert_encoding($uneLigne->idUtilisateur, "UTF-8");
            
            $uneTrace = new Trace($unId, $uneDateHeureDebut, $uneDateHeureFin, $terminee, $unIdUtilisateur);
            
            // mise à jour de la collection de points
            $lesPointsDeTrace = $this->getLesPointsDeTrace($idTrace);

            foreach ($lesPointsDeTrace as $unPoint) {
                $uneTrace->ajouterPoint($unPoint);
            }
            return $uneTrace;
        }
    }
    
    // fournit la collection de toutes les traces
    // le résultat est fourni sous forme d'une collection d'objets Trace
    function getToutesLesTraces() {
        // préparation de la requête de recherche
        $txt_req = "Select id, dateDebut, dateFin, terminee, idUtilisateur";
        $txt_req = $txt_req . " from tracegps_traces";
        $txt_req = $txt_req . " order by id desc";
        
        $req = $this->cnx->prepare($txt_req);
        // extraction des données
        $req->execute();
        $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        
        // construction d'une collection d'objets Trace
        $lesTraces = array();
        // tant qu'une ligne est trouvée :
        while ($uneLigne) {
            // création d'un objet Trace
            $unId = mb_convert_encoding($uneLigne->id, "UTF-8");
            $uneDateHeureDebut = mb_convert_encoding($uneLigne->dateDebut, "UTF-8");
            if ($uneLigne->dateFin == null){
                $uneDateHeureFin = null;
            }else{
                $uneDateHeureFin = mb_convert_encoding($uneLigne->dateFin, "UTF-8");
            }
            $terminee = mb_convert_encoding($uneLigne->terminee, "UTF-8");
            $unIdUtilisateur = mb_convert_encoding($uneLigne->idUtilisateur, "UTF-8");
            
            $uneTrace = new Trace($unId, $uneDateHeureDebut, $uneDateHeureFin, $terminee, $unIdUtilisateur);
            
            // mise à jour de la collection de points
            $lesPointsDeTrace = $this->getLesPointsDeTrace($unId);
            foreach ($lesPointsDeTrace as $unPoint) {
                $uneTrace->ajouterPoint($unPoint);
            }
            
            // ajout de la trace à la collection
            $lesTraces[] = $uneTrace;
            // extrait la ligne suivante
            $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        }
        // libère les ressources du jeu de données
        $req->closeCursor();
        // fourniture de la collection
        return $lesTraces;
    }
    
    // fournit la collection des traces de l'utilisateur $idUtilisateur
    // le résultat est fourni sous forme d'une collection d'objets Trace
    function getLesTraces($idUtilisateur) {
        // préparation de la requête de recherche
        $txt_req = "Select id, dateDebut, dateFin, terminee, idUtilisateur";
        $txt_req = $txt_req . " from tracegps_traces";
        $txt_req = $txt_req . " where idUtilisateur = :idUtilisateur";
        $txt_req = $txt_req . " order by id desc";
        
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et de ses paramètres
        $req->bindValue("idUtilisateur", $idUtilisateur, \PDO::PARAM_INT);
        // extraction des données
        $req->execute();
        $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        
        // construction d'une collection d'objets Trace
        $lesTraces = array();
        // tant qu'une ligne est trouvée :
        while ($uneLigne) {
            // création d'un objet Trace
            $unId = mb_convert_encoding($uneLigne->id, "UTF-8");
            $uneDateHeureDebut = mb_convert_encoding($uneLigne->dateDebut, "UTF-8");
            if ($uneLigne->dateFin == null) {
                $uneDateHeureFin = null;
            }else{
                $uneDateHeureFin = mb_convert_encoding($uneLigne->dateFin, "UTF-8");
            }
            $terminee = mb_convert_encoding($uneLigne->terminee, "UTF-8");
            $unIdUtilisateur = mb_convert_encoding($uneLigne->idUtilisateur, "UTF-8");
            
            $uneTrace = new Trace($unId, $uneDateHeureDebut, $uneDateHeureFin, $terminee, $unIdUtilisateur);
            
            // mise à jour de la collection de points
            $lesPointsDeTrace = $this->getLesPointsDeTrace($unId);
            foreach ($lesPointsDeTrace as $unPoint) {
                $uneTrace->ajouterPoint($unPoint);
            }
            
            // ajout de la trace à la collection
            $lesTraces[] = $uneTrace;
            // extrait la ligne suivante
            $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        }
        // libère les ressources du jeu de données
        $req->closeCursor();
        // fourniture de la collection
        return $lesTraces;
    }
    
    // fournit la collection des traces que l'utilisateur $idUtilisateur a le droit de consulter
    // le résultat est fourni sous forme d'une collection d'objets Trace
    function getLesTracesAutorisees($idUtilisateur) {
        // préparation de la requête de recherche
        $txt_req = "Select id, dateDebut, dateFin, terminee, idUtilisateur from tracegps_traces";
        $txt_req .= " where idUtilisateur in ";
        $txt_req .= " (Select idAutorisant from tracegps_autorisations where idAutorise = :idUtilisateur)";
        $txt_req .= " order by id desc";
        
        $req = $this->cnx->prepare($txt_req);
        // liaison de la requête et de ses paramètres
        $req->bindValue("idUtilisateur", $idUtilisateur, \PDO::PARAM_INT);
        // extraction des données
        $req->execute();
        $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        
        // construction d'une collection d'objets Trace
        $lesTraces = array();
        // tant qu'une ligne est trouvée :
        while ($uneLigne) {
            // création d'un objet Trace
            $unId = mb_convert_encoding($uneLigne->id, "UTF-8");
            $uneDateHeureDebut = mb_convert_encoding($uneLigne->dateDebut, "UTF-8");
            $uneDateHeureFin = mb_convert_encoding($uneLigne->dateFin, "UTF-8");
            $terminee = mb_convert_encoding($uneLigne->terminee, "UTF-8");
            $unIdUtilisateur = mb_convert_encoding($uneLigne->idUtilisateur, "UTF-8");
            
            $uneTrace = new Trace($unId, $uneDateHeureDebut, $uneDateHeureFin, $terminee, $unIdUtilisateur);
            
            
            // mise à jour de la collection de points
            $lesPointsDeTrace = $this->getLesPointsDeTrace($unId);
            foreach ($lesPointsDeTrace as $unPoint) {
                $uneTrace->ajouterPoint($unPoint);
            }
            
            // ajout de la trace à la collection
            $lesTraces[] = $uneTrace;
            // extrait la ligne suivante
            $uneLigne = $req->fetch(\PDO::FETCH_OBJ);
        }
        // libère les ressources du jeu de données
        $req->closeCursor();
        // fourniture de la collection
        return $lesTraces;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    // --------------------------------------------------------------------------------------
    // début de la zone attribuée au développeur 4 (Noah) : lignes 950 à 1150
    // --------------------------------------------------------------------------------------
    
    // NINO FAUT CHECK SI Y'A DEJA UNE TRACE !!! 
    public function creerUneTrace($uneTrace) {
        // on teste si l'utilisateur existe déjà
        if ($this->existePseudoUtilisateur($uneTrace->getId())) return false;
        
        // préparation de la requête
        if ($uneTrace->getDateHeureFin() == null) {
            $txt_req1 = "insert into tracegps_traces (id, dateDebut, dateFin, terminee, idUtilisateur)";
            $txt_req1 .= " values (:id, :dateHeureDebut, NULL, :terminee, :idUtilisateur)";
            $req1 = $this->cnx->prepare($txt_req1);
            // liaison de la requête et de ses paramètres
            $req1->bindValue("id", mb_convert_encoding($uneTrace->getId(), "UTF-8"), \PDO::PARAM_INT);
            $req1->bindValue("dateHeureDebut", mb_convert_encoding($uneTrace->getDateHeureDebut(), "UTF-8"), \PDO::PARAM_STR);
            $req1->bindValue("terminee", mb_convert_encoding($uneTrace->getTerminee(), "UTF-8"), \PDO::PARAM_STR);
            $req1->bindValue("idUtilisateur", mb_convert_encoding($uneTrace->getIdUtilisateur(), "UTF-8"), \PDO::PARAM_INT);
        }else{
            $txt_req1 = "insert into tracegps_traces (id, dateDebut, dateFin, terminee, idUtilisateur)";
            $txt_req1 .= " values (:id, :dateHeureDebut, :dateHeureFin, :terminee, :idUtilisateur)";
            $req1 = $this->cnx->prepare($txt_req1);
            // liaison de la requête et de ses paramètres
            $req1->bindValue("id", mb_convert_encoding($uneTrace->getId(), "UTF-8"), \PDO::PARAM_INT);
            $req1->bindValue("dateHeureDebut", mb_convert_encoding($uneTrace->getDateHeureDebut(), "UTF-8"), \PDO::PARAM_STR);
            $req1->bindValue("dateHeureFin", mb_convert_encoding($uneTrace->getDateHeureFin(), "UTF-8"), \PDO::PARAM_STR);
            $req1->bindValue("terminee", mb_convert_encoding($uneTrace->getTerminee(), "UTF-8"), \PDO::PARAM_STR);
            $req1->bindValue("idUtilisateur", mb_convert_encoding($uneTrace->getIdUtilisateur(), "UTF-8"), \PDO::PARAM_INT);
        }
        // exécution de la requête
        $ok = $req1->execute();
        // sortir en cas d'échec
        if ( ! $ok) { return false; }
        
        // recherche de l'identifiant (auto_increment) qui a été attribué à la trace
        $unId = $this->cnx->lastInsertId();
        $uneTrace->setId($unId);
        return true;
    }
    
    public function supprimerUneTrace($Id) {
        $uneTrace = $this->getUneTrace($Id);
        if ($uneTrace == null) {
            return false;
        }

        else {
            $idTrace = $uneTrace->getId();
            $txt_req2 = "delete from tracegps_points where idTrace = :idTrace";
            $req2 = $this->cnx->prepare($txt_req2);
            $req2->bindValue("idTrace", mb_convert_encoding($idTrace, "UTF-8"), \PDO::PARAM_INT);
            
            // préparation de la requête de suppression des autorisations
            $txt_req1 = "delete from tracegps_traces where id = :idTrace";
            $req1 = $this->cnx->prepare($txt_req1);
            // liaison de la requête et de ses paramètres
            $req1->bindValue("idTrace", mb_convert_encoding($idTrace, "UTF-8"), \PDO::PARAM_INT);
            // exécution de la requête
            $ok2 = $req2->execute();
            $ok = $req1->execute();

            return $ok * $ok2;
        }
    }
    
    public function terminerUneTrace($idTrace) {

        $lastDate = date("Y-m-d h:i:sa", time());
        $lesPointsDeTrace = $this->getLesPointsDeTrace($idTrace);
        if(sizeof($lesPointsDeTrace) != 0) {
            $lastDate = $lesPointsDeTrace[sizeof($lesPointsDeTrace) -1]->getDateHeure();
        }
        $txt_req = "UPDATE tracegps_traces SET dateFin = :dateF, terminee = 1 WHERE id = :idTrace";
        $req = $this->cnx->prepare($txt_req);
        $req->bindValue("dateF", mb_convert_encoding($lastDate, "UTF-8"), \PDO::PARAM_STR);
        $req->bindvalue("idTrace", mb_convert_encoding($idTrace, "UTF-8"), \PDO::PARAM_INT);

        $ok = $req->execute();

        if ($ok){
            return true;
        }else{
            return false;
        }

    }
    
} // fin de la classe DAO

// ATTENTION : on ne met pas de balise de fin de script pour ne pas prendre le risque
// d'enregistrer d'espaces après la balise de fin de script !!!!!!!!!!!!