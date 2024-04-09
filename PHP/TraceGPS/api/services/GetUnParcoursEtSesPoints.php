<?php




namespace api\services;
use modele\DAO;
use DOMDocument;
// connexion du serveur web a la base MySQL
$dao = new DAO();
// Recuperation des donnees transmises
$pseudo = ( empty($this->request['pseudo'])) ? "" : $this->request['pseudo'];
$mdpSha1 = ( empty($this->request['mdp'])) ? "" : $this->request['mdp'];
$idTrace =( empty($this->request['idTrace'])) ? "" : $this->request['idTrace'];
$lang = ( empty($this->request['lang'])) ? "" : $this->request['lang'];

if ($lang != "json") $lang = "xml";

if ($this->getMethodeRequete() != "GET")
{	$msg = "Erreur : methode HTTP incorrecte.";
$code_reponse = 406;
}
else {
    // Les parametres doivent etre presents
    if ( $pseudo == "" || $mdpSha1 == "" || $idTrace == "")
    {	$msg = "Erreur : donnees incompletes.";
        $uneTrace=null;
        $desPointsDeTraces=null;
        $code_reponse = 400;
    }
    else if ($dao->getUneTrace($idTrace)==false)
    {	$msg = "Erreur : parcours inexistant.";
        $uneTrace=null;
        $desPointsDeTraces=null;
        $code_reponse = 400;
    }
    else
    {	$niveauConnexion = $dao->getNiveauConnexion($pseudo, $mdpSha1);
    
    switch ($niveauConnexion)
    {   case 0 :
        $msg = "Erreur : authentification incorrecte.";
        $uneTrace=null;
        $desPointsDeTraces=null;
        $code_reponse = 401; break;
    case 1 :
        $unUtilisateur=$dao->getUnUtilisateur($pseudo);
        $lesTracesAutorise=$dao->getLesTracesAutorisees($unUtilisateur->getId());
        foreach ($lesTracesAutorise as $element)
        {
            if ($element->getId()==$idTrace)
            {
                $msg = "Donnees de la trace demandee.";
                $uneTrace=$dao->getUneTrace($idTrace);
                $desPointsDeTraces=$dao->getLesPointsDeTrace($idTrace);
                $code_reponse = 200; break;
            }
        }
        if (!isset($msg))
        {
            $msg="Erreur : vous n'etes pas autorise par le proprietaire du parcours.";
            $uneTrace=null;
            $desPointsDeTraces=null;
            $code_reponse = 400;break;
        }
    }
    }
}
// ferme la connexion a MySQL :
unset($dao);

// creation du flux en sortie
if ($lang == "xml") {
    $content_type = "application/xml; charset=utf-8";      // indique le format XML pour la reponse
    $donnees = creerFluxXML($msg,$uneTrace,$desPointsDeTraces);
}
else {
    $content_type = "application/json; charset=utf-8";      // indique le format Json pour la reponse
    $donnees = creerFluxJSON($msg,$uneTrace,$desPointsDeTraces);
}

// envoi de la reponse HTTP
$this->envoyerReponse($code_reponse, $content_type, $donnees);

// fin du programme (pour ne pas enchainer sur les 2 fonctions qui suivent)
exit;


//-------------------------------------------------------------------

// creation du flux XML en sortie
function creerFluxXML($msg,$uneTrace,$desPointsDeTraces)
{
    /* Exemple de code XML
     <?xml version="1.0" encoding="UTF-8"?>
     <!--Service web Connecter - BTS SIO - Lycee De La Salle - Rennes-->
     <data>
     <reponse>Erreur : donnees incompletes.</reponse>
     </data>
     */
    
    // cree une instance de DOMdocument (DOM : Document Object Model)
    $doc = new DOMDocument();
    
    // specifie la version et le type d'encodage
    $doc->version = '1.0';
    $doc->encoding = 'UTF-8';
    
    // cree un commentaire et l'encode en UTF-8
    $elt_commentaire = $doc->createComment('Service web Connecter - BTS SIO - Lycee De La Salle - Rennes');
    // place ce commentaire a la racine du document XML
    $doc->appendChild($elt_commentaire);
    
    // cree l'element 'data' a la racine du document XML
    $elt_data = $doc->createElement('data');
    $doc->appendChild($elt_data);
    
    // place l'element 'reponse' juste apres l'element 'data'
    $elt_reponse = $doc->createElement('reponse', $msg);
    $elt_data->appendChild($elt_reponse);
    
    if($uneTrace!=null and $desPointsDeTraces!=null)
    {
        // place l'element 'donnees' juste apres l'element 'data'
        $elt_donnees = $doc->createElement('donnees');
        $elt_data->appendChild($elt_donnees);
        
        // place l'element 'trace' juste apres l'element 'donnees'
        $elt_trace = $doc->createElement('trace');
        $elt_donnees->appendChild($elt_trace);
        

        $elt_id = $doc->createElement('id',$uneTrace->getId());
        $elt_trace->appendChild($elt_id);
        

        $elt_dateHeureDebut = $doc->createElement('dateHeureDebut',$uneTrace->getDateHeureDebut());
        $elt_trace->appendChild($elt_dateHeureDebut);
        

        $elt_terminee = $doc->createElement('terminee',$uneTrace->getTerminee());
        $elt_trace->appendChild($elt_terminee);
        

        if ($uneTrace->getDateHeureFin()!=null)
        {
            $elt_dateHeureFin = $doc->createElement('dateHeureFin',$uneTrace->getDateHeureFin());
            $elt_trace->appendChild($elt_dateHeureFin);
        }
        

        $elt_idUtilisateur = $doc->createElement('idUtilisateur',$uneTrace->getIdUtilisateur());
        $elt_trace->appendChild($elt_idUtilisateur);
        
        $elt_lesPoints = $doc->createElement('lesPoints');
        $elt_donnees->appendChild($elt_lesPoints);
        
        foreach ($desPointsDeTraces as $unPointsDeTrace)
        {
            
            $elt_point = $doc->createElement('point');
            $elt_lesPoints->appendChild($elt_point);
            
            $elt_id = $doc->createElement('id',$unPointsDeTrace->getId());
            $elt_point->appendChild($elt_id);
            
            $elt_latitude = $doc->createElement('latitude',$unPointsDeTrace->getLatitude());
            $elt_point->appendChild($elt_latitude);
            
            $elt_longitude = $doc->createElement('longitude',$unPointsDeTrace->getLongitude());
            $elt_point->appendChild($elt_longitude);
            
            $elt_altitude = $doc->createElement('altitude',$unPointsDeTrace->getAltitude());
            $elt_point->appendChild($elt_altitude);
            
            $elt_dateHeure = $doc->createElement('dateHeure',$unPointsDeTrace->getDateHeure());
            $elt_point->appendChild($elt_dateHeure);
            
            $elt_rythmeCardio = $doc->createElement('rythmeCardio',$unPointsDeTrace->getRythmeCardio());
            $elt_point->appendChild($elt_rythmeCardio);
        }
    }

    // Mise en forme finale
    $doc->formatOutput = true;
    
    // renvoie le contenu XML
    return $doc->saveXML();
}

// ================================================================================================

// creation du flux JSON en sortie
function creerFluxJSON($msg,$uneTrace,$desPointsDeTraces)
{
    /* Exemple de code JSON
     {
     "data":{
     "reponse": "authentification incorrecte."
     }
     }
     */
    if($uneTrace!=null and $desPointsDeTraces!=null)
    {
        $elt_lesPoints=[];
        foreach ($desPointsDeTraces as $unPointsDeTrace)
        {
            
            $elt_point = ['id'=>$unPointsDeTrace->getId(),'latitude'=>$unPointsDeTrace->getLatitude(),'longitude'=>$unPointsDeTrace->getLongitude(),'altitude'=>$unPointsDeTrace->getAltitude(),'dateHeure'=>$unPointsDeTrace->getDateHeure(),'rythmeCardio'=>$unPointsDeTrace->getRythmeCardio()];
            $elt_lesPoints[]=$elt_point;

        }
        
        $elt_trace=["id"=>$uneTrace->getId(),"dateHeureDebut"=>$uneTrace->getDAteHeureDebut(),"terminee"=>$uneTrace->getTerminee(),"dateHeureFin"=>$uneTrace->getDateHeureFin(),"idUtilisateur"=>$uneTrace->getIdUtilisateur()];
    // 2 notations possibles pour creer des tableaux associatifs (la deuxieme est en commentaire)
        $elt_donnees=["trace"=>$elt_trace,"lesPoints"=>$elt_lesPoints];
        $elt_data = ["reponse" => $msg,"donnees"=>$elt_donnees];
    }
    else{$elt_data = ["reponse" => $msg];}
    // construction de l'element "data"
    
    //     $elt_data = array("reponse" => $msg);
    
    // construction de la racine
    $elt_racine = ["data" => $elt_data];
    //     $elt_racine = array("data" => $elt_data);
    
    // retourne le contenu JSON (l'option JSON_PRETTY_PRINT gere les sauts de ligne et l'indentation)
    return json_encode($elt_racine, JSON_PRETTY_PRINT);
}

// ================================================================================================