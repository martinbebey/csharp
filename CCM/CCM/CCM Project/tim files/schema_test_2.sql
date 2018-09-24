DROP VIEW Vehicle_Log;
DROP TABLE Checks_out;
DROP TABLE Vehicle;
DROP VIEW Current_Assignments;
DROP TABLE Works_on;
DROP TABLE Assignment;
DROP TABLE Employee;



CREATE SCHEMA IF NOT EXISTS `ccm_test` DEFAULT CHARACTER SET utf8; 

CREATE TABLE IF NOT EXISTS `Employee` (
  `id` INT(11) NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `pw` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

INSERT INTO `ccm_test`.`Employee` (`id`, `name`, `pw`) VALUES (0000000, 'Mar\'n', 'aaa');
INSERT INTO `ccm_test`.`Employee` (`id`, `name`, `pw`) VALUES (1111111, 'Bim', 'bbb');
INSERT INTO `ccm_test`.`Employee` (`id`, `name`, `pw`) VALUES (2222222, 'Stephan', 'ccc');



CREATE TABLE IF NOT EXISTS `Assignment` (
  `oem` VARCHAR(45) NOT NULL,
  `model` VARCHAR(45) NOT NULL,
  `platform` VARCHAR(45) NOT NULL,
  `region` VARCHAR(45) NOT NULL,
  `version` VARCHAR(45) NOT NULL,
  `task` VARCHAR(45) NOT NULL,
  `due` DATETIME NOT NULL,
  `status` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`version`))
ENGINE = InnoDB;

INSERT INTO `ccm_test`.`Assignment` (`oem`, `model`, `platform`, `region`, `version`, `task`, `due`, `status`) VALUES ('HMC', 'PD', 'GEN5', 'USA', 'PD.USA.0000.V000.000000.STD_H', 'Monitoring', '2017-01-01', 'complete');
INSERT INTO `ccm_test`.`Assignment` (`oem`, `model`, `platform`, `region`, `version`, `task`, `due`, `status`) VALUES ('HMC', 'OS', 'D.Audio 1.0', 'CAN', 'OS.CAN.0000.V111.111111', 'Tele BFC (EO)', '2017-02-02', 'ongoing');
INSERT INTO `ccm_test`.`Assignment` (`oem`, `model`, `platform`, `region`, `version`, `task`, `due`, `status`) VALUES ('KMC', 'QL', 'D.Audio 1.0', 'USA', 'QL.USA.0000.V222.222222', 'MM BFC', '2017-03-03', 'ongoing');



CREATE TABLE IF NOT EXISTS `Works_on` (
  `id` INT NOT NULL,
  `version` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`, `version`),
  INDEX `version_idx` (`version` ASC),
  CONSTRAINT `id`
    FOREIGN KEY (`id`)
    REFERENCES `ccm_test`.`Employee` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `version`
    FOREIGN KEY (`version`)
    REFERENCES `ccm_test`.`Assignment` (`version`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

INSERT INTO `Works_on` (`id`, `version`) VALUES (0000000, 'PD.USA.0000.V000.000000.STD_H');
INSERT INTO `Works_on` (`id`, `version`) VALUES (1111111, 'OS.CAN.0000.V111.111111');
INSERT INTO `Works_on` (`id`, `version`) VALUES (2222222, 'QL.USA.0000.V222.222222');



SELECT * FROM Employee;
SELECT * FROM Assignment;
SELECT * FROM Works_on;

CREATE VIEW Current_Assignments(name, version, due, status) 
AS SELECT E.name, A.version, A.due, A.status
FROM Employee E, Assignment A, Works_on W
WHERE E.id = W.id AND W.version = A.version;

SELECT * FROM Current_Assignments;



CREATE TABLE IF NOT EXISTS `Vehicle` (
  `vin` VARCHAR(17) NOT NULL,
  `color` VARCHAR(45) NOT NULL,
  `model` VARCHAR(45) NOT NULL,
  `mileage` INT NULL,
  `mtn_status` VARCHAR(45) NULL,
  PRIMARY KEY (`vin`))
ENGINE = InnoDB;

INSERT INTO `Vehicle` (`vin`, `color`, `model`, `mileage`, `mtn_status`) VALUES ('AAAAAAAAAAAAAAAAA', 'white', 'LF', 11111, 'good');
INSERT INTO `Vehicle` (`vin`, `color`, `model`, `mileage`, `mtn_status`) VALUES ('BBBBBBBBBBBBBBBBB', 'blue', 'BB', 88888, 'Broken');
INSERT INTO `Vehicle` (`vin`, `color`, `model`, `mileage`, `mtn_status`) VALUES ('CCCCCCCCCCCCCCCCC', 'silver', 'UM', 77777, 'low fuel');



CREATE TABLE IF NOT EXISTS `ccm_test`.`Checks_out` (
  `out` DATETIME NOT NULL,
  `in` DATETIME NOT NULL,
  `emp_id` INT NOT NULL,
  `vin` VARCHAR(17) NOT NULL,
  `status` VARCHAR(45) NULL,
  INDEX `vin_idx` (`vin` ASC),
  PRIMARY KEY (`vin`, `emp_id`),
  CONSTRAINT `emp_id`
    FOREIGN KEY (`emp_id`)
    REFERENCES `ccm_test`.`Employee` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `vin`
    FOREIGN KEY (`vin`)
    REFERENCES `ccm_test`.`Vehicle` (`vin`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE VIEW Vehicle_Log
AS SELECT E.name, V.color, V.model, C.out, C.in, C.status
FROM Employee E, Vehicle V, Checks_out C
WHERE E.id = C.emp_id AND C.vin = V.vin;

INSERT INTO `ccm_test`.`Checks_out` (`out`, `in`, `emp_id`, `vin`, `status`) VALUES ('2018-01-01', '2018-02-02', 0000000, 'AAAAAAAAAAAAAAAAA', 'returned');
INSERT INTO `ccm_test`.`Checks_out` (`out`, `in`, `emp_id`, `vin`, `status`) VALUES ('2018-03-03', '2018-04-04', 1111111, 'BBBBBBBBBBBBBBBBB', 'overdue');
INSERT INTO `ccm_test`.`Checks_out` (`out`, `in`, `emp_id`, `vin`, `status`) VALUES ('2018-05-05', '2018-06-06', 2222222, 'CCCCCCCCCCCCCCCCC', 'not returned');

SELECT * FROM Vehicle_Log;