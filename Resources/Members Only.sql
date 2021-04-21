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


INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('1', 'Admin', '1234', '1995', 'admin@gmail.com', 'Santo Antonio dos Cavaleiros');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '1');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('2', 'Diogo Barradas', '1234', '2002', 'diogobarradas1@gmail.com', 'Santo Antonio dos Cavaleiros');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '2');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('3', 'Andre Costa', '1234', '2001', 'kpop@gmail.com', 'Arroja Odivelas');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '3');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('4', 'Antonio Varandas', '1234', '1970', 'antonio@yahoo.com', 'Alameda');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '4');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('5', 'Bruno Carvalho', '1a2b', '1950', 'brunocarva@sapo.pt', 'Belas Sintra');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '5');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('6', 'Igor Franscisco', 'abcd', '2000', 'Monkey@hotmail.com', 'Quinta do Conde');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '6');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('7', 'Daniel Adrião', '1234', '2003', 'danieladriao@gmail.com', 'Benfica Lisboa');
UPDATE `members_only`.`registo` SET `Saldo` = '0' WHERE (`ID` = '7');
INSERT INTO `members_only`.`registo` (`ID`, `Username`, `PIN`, `Idade`, `Email`, `Morada`) VALUES ('8', 'Afonso Macedo', '1234', '1999', 'afonso@bing.com', 'Margem Sul');
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