CREATE OR REPLACE VIEW tracegps_vue_utilisateurs (id, pseudo, mdpSha1, adrMail, numTel, niveau, dateCreation, nbTraces, dateDerniereTrace) AS
SELECT tracegps_utilisateurs.id, pseudo, mdpSha1, adrMail, numTel, niveau, dateCreation, count(tracegps_traces.id), max(dateDebut)
FROM tracegps_utilisateurs left join tracegps_traces ON tracegps_utilisateurs.id = tracegps_traces.idUtilisateur
GROUP BY id, pseudo, mdpsha1, adrmail, numtel, niveau, dateCreation
ORDER BY pseudo ;

CREATE OR REPLACE VIEW tracegps_vue_traces (id, dateDebut, dateFin, terminee, idUtilisateur, pseudo, nbPoints) AS
SELECT tracegps_traces.id, dateDebut, dateFin, terminee, idUtilisateur, pseudo, count(tracegps_points.id)
FROM (tracegps_traces INNER JOIN tracegps_utilisateurs ON tracegps_traces.idUtilisateur = tracegps_utilisateurs.id)
LEFT JOIN tracegps_points ON tracegps_traces.id = tracegps_points.idTrace
GROUP BY id, dateDebut, dateFin, terminee, idUtilisateur, pseudo
ORDER BY id;