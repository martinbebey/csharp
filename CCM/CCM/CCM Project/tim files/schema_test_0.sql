DROP TABLE Works_on;
DROP TABLE Employee;
DROP TABLE Assignment;

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
