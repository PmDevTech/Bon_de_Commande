------------------25/05/2022-----------------------
CREATE TABLE `bdpdmtest2`.`t_bc_listebesoins` ( `Id_listebesoins` BIGINT(19) NOT NULL AUTO_INCREMENT , `RefBonCommande` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `RefListeBesoins` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `Designation` VARCHAR(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `Quantite` VARCHAR(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `PrixUnitaire` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , `PrixTotal` VARCHAR(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL , PRIMARY KEY (`Id_listebesoins`)) ENGINE = MyISAM;
ALTER TABLE `t_bc_listebesoins` CHANGE `RefListeBesoins` `RefListeBesoins` VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

----26/05/2022------


--27/05/2022---
ALTER TABLE `t_bc_listebesoins` CHANGE `PrixTotal` `PrixTotal` DOUBLE(19,5) NOT NULL;
ALTER TABLE `t_bc_listebesoins` CHANGE `PrixUnitaire` `PrixUnitaire` DOUBLE(19,5) NOT NULL;

----28/05/2022---
ALTER TABLE `t_bc_listebesoins` CHANGE `Quantite` `Quantite` VARCHAR(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL, CHANGE `PrixUnitaire` `PrixUnitaire` DOUBLE(19,5) NULL;
ALTER TABLE `t_bc_listebesoins` CHANGE `PrixUnitaire` `PrixUnitaire` VARCHAR(20) NULL DEFAULT NULL;

---30/05/2022----


---01/06/2022----
ALTER TABLE `t_bc_listebesoins` CHANGE `PrixTotal` `PrixTotal` DOUBLE(19,2) NOT NULL;

---03/06/2022---


