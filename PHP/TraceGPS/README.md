# Traitement fichier gps

## Coordonnées géographiques d’un lieu
Les coordonnées géographiques d’un lieu sur la Terre forment un système de trois coordonnées qui sont le plus souvent :

la latitude
la longitude
l’altitude (ou l’élévation) par rapport au niveau de la mer en un point précis.

![image](https://github.com/DailyScreen/TraceGPS/assets/113687926/768fa72d-fa9c-4a8e-8df4-644425d31cdd)

La latitude exprime le positionnement nord-sud d’un point sur Terre :

Les points de même latitude constituent un parallèle.
La latitude s’étend de 0° à l’équateur à +90° au pôle Nord.
La latitude s’étend de 0° à l’équateur à -90° au pôle Sud.
La longitude exprime le positionnement est-ouest d’un point sur Terre :

Les points de même longitude constituent un méridien.
Le méridien de référence est le méridien de Greenwich et correspond à la longitude 0.
La longitude s’étend de -180 à +180 , ou respectivement de 180 ouest à 180 est.

![image](https://github.com/DailyScreen/TraceGPS/assets/113687926/1fc78856-07d2-445d-944b-607c8d490f0c)

## L'application

Beaucoup de sportifs de plein-air (marcheurs, coureurs, cyclistes, kayakistes, ...) ont pris l'habitude
d'enregistrer leur parcours avec un système de géolocalisation GPS tel que :


 - montre GPS
 - téléphone mobile équipé d'un GPS et d'une application d'enregistrement de parcours
 
Ces systèmes enregistrent régulièrement (toutes les secondes dans certains cas) la position du sportif
avec les données suivantes :
- latitude et longitude (en degrés décimaux)
- altitude (en mètres)
- heure de passage (format courant : "yyyy-MM-ddThh:mm:ssZ")
- rythme cardiaque (en battements par minute) si l'appareil dispose d'un capteur

Les données sont enregistrées dans la mémoire de l'appareil.
Il existe différents formats de fichiers, souvent basés sur le langage XML.
C'est le cas des formats GPX, PWX et TCX (présentés plus loin dans ce document).
A son retour, après une bonne douche, une sieste, ou une bière (!!!), le sportif peut transférer le fichier
sur son ordinateur (avec ou sans fil) pour visualiser et analyser son parcours.

![image](https://github.com/DailyScreen/TraceGPS/assets/113687926/c90cb2e7-4e9b-4efe-989d-2d02825d1fc2)
