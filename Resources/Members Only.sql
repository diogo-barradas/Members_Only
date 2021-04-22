create database members_only;
CREATE SCHEMA `members_only`;
use members_only;


CREATE TABLE `members_only`.`registo` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Username` VARCHAR(50) NOT NULL,
  `PIN` VARCHAR(4) NOT NULL,
  `Idade` INT NOT NULL,
  `Email` VARCHAR(50) NOT NULL,
  `Morada` VARCHAR(70) NOT NULL,
  PRIMARY KEY (`ID`));
  ALTER TABLE registo ADD UNIQUE (Email);
  ALTER TABLE registo ADD UNIQUE (Username);
  ALTER TABLE `members_only`.`registo` 
  ADD COLUMN `Saldo` DOUBLE NULL AFTER `Morada`;
  ALTER TABLE `members_only`.`registo` 
  CHANGE COLUMN `PIN` `PIN` VARCHAR(50) NOT NULL ;



INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('1', 'Admin', '1292201552198220877194054219216496220885', '1995', 'admin@gmail.com', 'Matosinhos');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '1');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('2', 'Diogo Barradas', '1037960442613811114470301381022518580186', '2002', 'diogobarra@gmail.com', 'Santo António dos Cavaleiros');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '2');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('3', 'André Costa', '217531451892471341430782262521671531451821', '2001', 'kpop@gmail.com', 'Arroja Odivelas');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '3');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('4', 'António Varandas', '243143239761473136121393519415411211202152', '1970', 'antonio@yahoo.com', 'Alameda');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '4');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('5', 'Bruno Carvalho', '1282012391518499105205372491024212624558158', '1950', 'brunocarva@sapo.pt', 'Belas Sintra');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '5');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('6', 'Igor Fransicsco', '1031671171817321423516138512178178112120126', '2000', 'Monkey@hotmail.com', 'Quinta do Conde');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '6');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('7', 'Daniel Adrião', '22625211376713923814714924336205461275131', '2003', 'danieladriao@gmail.com', 'Benfica Lisboa');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '7');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('8', 'Afonso Macedo', '5681177174115202121662271947428616810206', '1999', 'afonso@bing.com', 'Margem Sul');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '8');


  CREATE TABLE `members_only`.`depositos` (
  `idDepositos` INT NOT NULL AUTO_INCREMENT,
  `Descriçao` VARCHAR(60) NOT NULL,
  `Hora` VARCHAR(5) NOT NULL,
  `Valor` DOUBLE NOT NULL,
  `ID` INT,
  PRIMARY KEY (`idDepositos`),
  foreign key (ID) references registo (ID)
  );

  
  CREATE TABLE `members_only`.`levantamentos` (
  `idLevantamentos` INT NOT NULL AUTO_INCREMENT,
  `Descriçao` VARCHAR(60) NOT NULL,
  `Hora` VARCHAR(5) NOT NULL,
  `Valor` DOUBLE NOT NULL,
  `ID` INT,
  PRIMARY KEY (`idLevantamentos`),
  foreign key (ID) references registo (ID)
  );


CREATE TABLE `members_only`.`transferencias` (
  `idTransferencias` INT NOT NULL AUTO_INCREMENT,
  `Descriçao` VARCHAR(60) NOT NULL,
  `Hora` VARCHAR(5) NOT NULL,
  `Valor` DOUBLE NOT NULL,
  `idDestinatario` INT NOT NULL,
  `idRemetente` INT NOT NULL,
  `ID` INT,
  PRIMARY KEY (`idTransferencias`),
  foreign key (ID) references registo (ID)
  );
  ALTER TABLE `members_only`.`transferencias` 
  DROP COLUMN `idRemetente`;
  
  
  SELECT * FROM registo;
  SELECT * FROM depositos;
  SELECT * FROM levantamentos;
  SELECT * FROM transferencias;
  
 /*
 drop table registo; 
 drop table depositos; 
 drop table levantamentos; 
 drop table transferencias; 
 */  