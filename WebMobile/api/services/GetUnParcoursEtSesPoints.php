<?php
 

use DOMDocument;
// Projet TraceGPS - services web
// fichier : api/services/GetUnParcoursEtSesPoints.php
// Dernière mise à jour : 28/11/2023 par Noah GRASLAND
// connexion du serveur web à la base MySQL
$dao = new DAO();

// Récupération des données transmises
$pseudo = ( empty($this->request['pseudo'])) ? "" : $this->request['pseudo'];
$mdpSha1 = ( empty($this->request['mdp'])) ? "" : $this->request['mdp'];
$idTrace = ( empty($this->request['idTrace'])) ? "" : $this->request['idTrace'];
$lang = ( empty($this->request['lang'])) ? "" : $this->request['lang'];

// "xml" par défaut si le paramètre lang est absent ou incorrect
if ($lang != "json") $lang = "xml";

$laTrace = null;

// La méthode HTTP utilisée doit être GET
if ($this->getMethodeRequete() != "GET")
{	$msg = "Erreur : méthode HTTP incorrecte.";
    $code_reponse = 406;
}
else {
    // Les paramètres doivent être présents
    if ( $pseudo == "" || $mdpSha1 == "" || $idTrace == "")
    {	$msg = "Erreur : données incomplètes.";
        $code_reponse = 400;
    }
    else
    {	if ( $dao->getNiveauConnexion($pseudo, $mdpSha1) != 1 ) {
        $msg = "Erreur : authentification incorrecte.";
        $code_reponse = 401;
        }
        else
        {// récupération de la trace
            $laTrace = $dao->getLesTracesAutorisees($dao->getUnUtilisateur($pseudo)->getId());
        }
    }
}

// ferme la connexion à MySQL :
unset($dao);

// création du flux en sortie
if ($lang == "xml") {
    $content_type = "application/xml; charset=utf-8";      // indique le format XML pour la réponse
    $donnees = creerFluxXML($msg, $laTrace);
}
else {
    $content_type = "application/json; charset=utf-8";      // indique le format Json pour la réponse
    $donnees = creerFluxJSON($msg, $laTrace);
}

// envoi de la réponse HTTP
$this->envoyerReponse($code_reponse, $content_type, $donnees);

// fin du programme (pour ne pas enchainer sur les 2 fonction qui suivent)
exit;

// création du flux XML en sortie
function creerFluxXML($msg, $laTrace)
{
    $doc = new DOMDocument();
    // specifie la version et le type d'encodage
    $doc->version = '1.0';
    $doc->encoding = 'UTF-8';

    // crée un commentaire et l'encodde en UTF-8
    $elt_commentaire = $doc->createComment('Service web GetUnParcoursEtSesPoints - BTS SIO - Lycée De La Salle - Rennes');
    // place ce commentaire à la recine du documment XML
    $doc->appendChild($elt_commentaire);

    // crée l'élément 'data' à la racine du document XML
	$elt_data = $doc->createElement('data');
	$doc->appendChild($elt_data);
	
	// place l'élément 'reponse' dans l'élément 'data'
	$elt_reponse = $doc->createElement('reponse', $msg);
	$elt_data->appendChild($elt_reponse);

    if ($laTrace){
        if (sizeof($laTrace->getLesPointsDeTrace()) > 0) {

            // place l'élément 'donnees' dans l'élément 'data'
            $elt_donnees = $doc->createElement('donnees');
            $elt_data->appendChild($elt_donnees);
    
            // place l'élément 'trace' dans l'élément 'donnees'
            $elt_trace = $doc->createElement('trace');
            $elt_donnees->appendChild($elt_trace);
    
            // place l'élément 'id' dans l'élément 'trace'
            $elt_idTrace = $doc->createElement('id', $laTrace->getId());
            $elt_trace->appendChild($elt_idTrace);
    
            // palce l'élément 'dateHeureDebut' dans l'élément 'trace'
            $elt_dateHeureDebut = $doc->createElement('dateHeureDebut', $laTrace->getDateHeureDebut());
            $elt_trace->appendChild($elt_dateHeureDebut);
    
            // place l'élément 'terminee' dans l'élément 'trace'
            $elt_terminee = $doc->createElement('terminee', $laTrace->getTerminee());
            $elt_trace->appendChild($elt_terminee);
    
            // place l'élément 'detaHeureFin' dans l'élément 'trace'
            $elt_dateHeureFin = $doc->createElement('dateHeureFin', $laTrace->getDateHeureDebut());
            $elt_trace->appendChild($elt_dateHeureFin);
    
            // place l'élément 'idUtilisateur' dans l'élément 'trace'
            $elt_idUtilisateur = $doc->createElement('idUtilisateur', $laTrace->getIdUtilisateur());
            $elt_trace->appendChild($elt_idUtilisateur);
    
            // place l'élément 'lesPoints' dans l'élément 'donnees'
            $elt_lesPoints = $doc->createElement('lesPoints');
            $elt_donnees->appendChild($elt_lesPoints);
    
            foreach ($laTrace->getLesPointsDeTrace() as $Point)
            {
                // place l'élément 'point' dans l'élément 'lesPoints'
                $elt_point = $doc->createElement('point');
                $elt_lesPoints->appendChild($elt_point);
    
                // place l'élément 'id' dans l'élément 'point'
                $elt_idPoint = $doc->createElement('id', $Point->getId());
                $elt_point->appendChild($elt_point);
    
                // place l'élément 'latitude' dans l'élément 'point'
                $elt_latitude = $doc->createElement('latitude',$Point->getLatitude());
                $elt_point->appendChild($elt_latitude);
    
                // place l'élément 'longitude' dans l'élement 'point'
                $elt_longitude = $doc->createElement('longitude', $Point->getLongitude());
                $elt_point->appendChild($elt_longitude);
    
                //place l'élément 'longitude' dans l'élément 'point'
                $elt_altitude = $doc->createElement('altitude', $Point->getAltitude());
                $elt_point->appendChild($elt_altitude);
    
                // place l'élément 'dateHeure' dans l'élément 'point'
                $elt_dateHeur = $doc->createElement('dateHeure', $Point->getDateHeur());
                $elt_point->appendChild($elt_dateHeur);
    
                //place l'élément 'dateHeure' dans l'élément 'point'
                $elt_rythmeCardio = $doc->createElement('rythmeCardio', $Point->getRythmeCardio());
                $elt_point->appendChild($elt_rythmeCardio);
    
            }
        }
    }
    
    // Mise en forme final
    $doc->formatOutput = true;

    // renvoie le contenue XML
    return $doc->saveXML();
}

// création du flux JSON en sortie
function creerFluxJSON($msg,$laTrace)
{
    $elt_data = ["reponse" => $msg];
    if ($laTrace){
        // construction d'un tableur conteant les utilisateurs
        $lesObjetsDuTableau = array();

        // ajout id de la trace
        $idTrace = $laTrace->getId();
        $lesObjetsDuTableau['id'] = $idTrace;

        // ajout 'dateHeureDebut' de la trace
        $dateHeureDebutTrace = $laTrace->getDateHeureDebut();
        $lesObjetsDuTableau['dateHeureDebut'] = $dateHeureDebutTrace;

        // ajout 'terminee' de la trace
        $termineeTrace = $laTrace->getTerminee();
        $lesObjetsDuTableau['terminee'] = $termineeTrace;

        // ajout 'dateHeureFin' de la trace
        $dateHeureFinTrace = $laTrace->getDateHeureFin();
        $lesObjetsDuTableau['dateHeureFin'] = $dateHeureDebutTrace;

        // ajour 'distance' de la trace
        $distanceTrace = $laTrace->getDistance();
        $lesObjetsDuTableau['distance'] = $distanceTrace;

        // ajout 'idUtilisateur' de la trace
        $idUtilisateurTrace = $laTrace->getIdUtilisateur();
        $lesObjetsDuTableau['idUtilisateur'] = $idUtilisateurTrace;

        $lesPoints = array();
        foreach($laTrace->getLesPointsDeTrace() as $unPoint){
            // création du point de trace
            $ObjetPoint = array();

            // ajout de l'id du point
            $ObjetPoint['id'] = $unPoint->getId();
            $ObjetPoint['latitude'] = $unPoint->getLatitude();
            $ObjetPoint['longitude'] = $unPoint->getLongitude();
            $ObjetPoint['altitude'] = $unPoint->getAltitude();
            $ObjetPoint['dateHeure'] = $unPoint->getDateHeure();
            $ObjetPoint['rythmeCardio'] = $unPoint->getRythmeCardio();

            $lesPoints[] = $ObjetPoint;
        }
        
        // ajour des points de trace
        $lesObjetsDuTableau['lesPoints'] = $lesPoints;

        // création de m'élément Trace
        $elt_trace = ['trace' => $lesObjetsDuTableau];

        // construction de l'élément 'data'
        $elt_data = ['response' => $msg, 'donnees' => $elt_trace];
    }
    // construction de la racine
    $elt_racine = ['data' => $elt_data];

    return json_encode($elt_racine, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
}
?>