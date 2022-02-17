--- 11/02/2022
ALTER TABLE `t_dp` ADD `TypAssociation` VARCHAR(100) NULL AFTER `AssoListeRest`;
 ALTER TABLE `t_dp` DROP `ConfPrea`;
 CREATE TABLE `bdpdmoldv1`.`t_dp_donneparticuliere` ( `RefDP` BIGINT(19) NOT NULL AUTO_INCREMENT , `NumeroDp` VARCHAR(100) NULL , `CheckRemisePropo` VARCHAR(3) NULL , `DateReglement` VARCHAR(19) NULL , `ChkConference` VARCHAR(3) NULL , `DateConference` VARCHAR(19) NULL , `AdresseConfere` VARCHAR(50) NULL , `TitreConference` VARCHAR(100) NULL , `TelConferen` VARCHAR(19) NULL , `CourrielConferen` VARCHAR(100) NULL , `MontantImpot` VARCHAR(19) NULL , `RespectLoi` VARCHAR(3) NULL , `Soustraitant` VARCHAR(3) NULL , `RevisionPrix` VARCHAR(3) NULL , `Inflation` VARCHAR(3) NULL , `ProcedurProTechLine` TEXT NULL , `ProcedurOvrPropoFinLine` TEXT NULL , `AdresslieuOuvr` VARCHAR(100) NULL , `VillelieuOuvr` VARCHAR(100) NULL , `BuroOuver` VARCHAR(100) NULL , `PaysOuvertur` VARCHAR(100) NULL , `DateNego` VARCHAR(19) NULL , `AdresseNego` VARCHAR(100) NULL , `DateService` VARCHAR(19) NULL , `LieuService` VARCHAR(100) NULL , `NomReclama` VARCHAR(200) NULL , `TitreReclam` VARCHAR(100) NULL , `AdresseReclam` VARCHAR(100) NULL , `Agence` VARCHAR(100) NULL , `TelecopociRecla` VARCHAR(19) NULL , `ConsulRetenu` VARCHAR(3) NULL , `CodeProjet` VARCHAR(50) NULL , PRIMARY KEY (`RefDP`)) ENGINE = InnoDB;
ALTER TABLE `t_dp_donneparticuliere` ADD `NomConferenc` VARCHAR(100) NULL AFTER `AdresseConfere`;

----- 13/02/2022 ---
ALTER TABLE `t_marchesigne` ADD `TypeMarche1` VARCHAR(20) NULL DEFAULT 'NULL' AFTER `Attributaire`
ALTER TABLE `t_marchesigne` CHANGE `TypeMarche1` `TypeMarche1` VARCHAR(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '';

--- 14/02/2022 ---
ALTER TABLE `t_ami` ADD `DateOuvertureEffective` DATETIME NULL AFTER `DateOuverture`;
ALTER TABLE `t_dp` ADD `DateOuvertureEffective` DATETIME NULL AFTER `DateOuverture`;

------ 15/02/2022 
ALTER TABLE `t_dp` ADD `DateFinOuverturEvalFin` DATETIME NULL AFTER `DateOuvertureEvalFinance`;
--------- 16/02/2022
RENAME TABLE `bdpdmoldv1`.`t_marche_repartition` TO `bdpdmoldv1`.`t_dp_marche_repartition`;
ALTER TABLE `t_dp_marche_repartition` ADD `RefId` BIGINT(19) NOT NULL AUTO_INCREMENT FIRST, ADD PRIMARY KEY (`RefId`);
ALTER TABLE `t_dp_articlecontrat` CHANGE `RefContrat` `NumeroContrat` VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL;
ALTER TABLE `t_dp_annexepj` CHANGE `RefContrat` `NumeroContrat` VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL;
----- 16/02/2022
ALTER TABLE `t_dp` ADD `DateAviObjection` VARCHAR(10) NULL AFTER `DateSoumRapTechBail`;
ALTER TABLE `t_soumissionconsultant` ADD `DateOuvertureEvalFin` DATETIME NULL AFTER `EvalFinOk`, ADD `DateFinOuvertEvalFin` DATETIME NULL AFTER `DateOuvertureEvalFin`, ADD `EtatRapportCombine` VARCHAR(15) NULL AFTER `DateFinOuvertEvalFin`;
ALTER TABLE `t_soumissionconsultant` ADD `CheminRapportCombine` VARCHAR(200) NULL AFTER `EtatRapportCombine`, ADD `DateEnvoiRapComb` VARCHAR(10) NULL AFTER `CheminRapportCombine`, ADD `DateRepoRapComb` VARCHAR(10) NULL AFTER `DateEnvoiRapComb`;
ALTER TABLE `t_soumissionconsultant` ADD `RefOuverture` VARCHAR(50) NULL AFTER `EvalFinOk`, ADD `RefRapportCombine` VARCHAR(50) NULL AFTER `RefOuverture`;
ALTER TABLE `t_dp` DROP `EvalFinanciere`, DROP `DateOuvertureEvalFinance`, DROP `DateFinOuverturEvalFin`;
ALTER TABLE `t_dp` CHANGE `DateAviObjection` `DateAviObjectionRapTech` VARCHAR(10) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL;
ALTER TABLE `t_soumissionconsultant` ADD `FinEvalFinanciere` DATETIME NULL AFTER `DateFinOuvertEvalFin`;
ALTER TABLE `t_dp` CHANGE `DateSoumRapTechBail` `DateSoumRapTechBail` DATE NULL DEFAULT NULL, CHANGE `DateAviObjectionRapTech` `DateAviObjectionRapTech` DATE NULL DEFAULT NULL;
ALTER TABLE `t_soumissionconsultant` CHANGE `DateEnvoiRapComb` `DateEnvoiRapComb` DATE NULL DEFAULT NULL, CHANGE `DateRepoRapComb` `DateRepoRapComb` DATE NULL DEFAULT NULL;
ALTER TABLE `t_soumissionconsultant` CHANGE `DateEnvoiRapComb` `DateEnvoiRapComb` VARCHAR(10) NULL DEFAULT NULL;
ALTER TABLE `t_soumissionconsultant` CHANGE `DateRepoRapComb` `DateRepoRapComb` VARCHAR(10) NULL DEFAULT NULL;
