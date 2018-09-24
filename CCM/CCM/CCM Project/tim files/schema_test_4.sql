DROP TABLE Ans_by;
DROP TABLE Answer;
DROP VIEW Discussion_Board;
DROP TABLE Asks;
DROP TABLE Question;
DROP VIEW Vehicle_Log;
DROP TABLE Checks_out;
DROP TABLE Vehicle;
DROP VIEW Current_Assignments;
DROP TABLE Works_on;
DROP TABLE Assignment;
DROP TABLE Employee;

CREATE TABLE IF NOT EXISTS `ccm_test`.`Answer` (
  `aid` INT NOT NULL,
  `a_time` DATETIME NOT NULL,
  `content` VARCHAR(400) NOT NULL,
  PRIMARY KEY (`aid`))
ENGINE = InnoDB

CREATE SCHEMA IF NOT EXISTS `ccm_test` DEFAULT CHARACTER SET utf8; 

CREATE TABLE IF NOT EXISTS `ccm_test`.`Employee` (
  `id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `pw` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

INSERT INTO `ccm_test`.`Employee` (`id`, `name`, `email`, `pw`) VALUES (0000000, 'Mar\'n', 'mm@test.com', '47bce5c74f589f4867dbd57e9ca9f808');
INSERT INTO `ccm_test`.`Employee` (`id`, `name`, `email`, `pw`) VALUES (1111111, 'Bim', 'lilt@test.com', '08f8e0260c64418510cefb2b06eee5cd');
INSERT INTO `ccm_test`.`Employee` (`id`, `name`, `email`, `pw`) VALUES (2222222, 'Stephan', 'bigs@test.com', '9df62e693988eb4e1e1444ece0578579');



CREATE TABLE IF NOT EXISTS `ccm_test`.`Assignment` (
  `aid` INT NOT NULL,
  `oem` VARCHAR(45) NOT NULL,
  `model` VARCHAR(45) NOT NULL,
  `platform` VARCHAR(45) NOT NULL,
  `region` VARCHAR(45) NOT NULL,
  `version` VARCHAR(45) NOT NULL,
  `task` VARCHAR(45) NOT NULL,
  `due` DATE NOT NULL,
  `status` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`aid`))
ENGINE = InnoDB;

INSERT INTO `ccm_test`.`Assignment` (`aid`, `oem`, `model`, `platform`, `region`, `version`, `task`, `due`, `status`) VALUES (0, 'HMC', 'PD', 'GEN5', 'USA', 'PD.USA.0000.V000.000000.STD_H', 'Monitoring', '2017-01-01', 'complete');
INSERT INTO `ccm_test`.`Assignment` (`aid`, `oem`, `model`, `platform`, `region`, `version`, `task`, `due`, `status`) VALUES (1, 'HMC', 'OS', 'D.Audio 1.0', 'CAN', 'OS.CAN.0000.V111.111111', 'Tele BFC (EO)', '2017-02-02', 'ongoing');
INSERT INTO `ccm_test`.`Assignment` (`aid`, `oem`, `model`, `platform`, `region`, `version`, `task`, `due`, `status`) VALUES (2, 'KMC', 'QL', 'D.Audio 1.0', 'USA', 'QL.USA.0000.V222.222222', 'MM BFC', '2017-03-03', 'ongoing');



CREATE TABLE IF NOT EXISTS `ccm_test`.`Works_on` (
  `id` INT NOT NULL,
  `aid` INT NOT NULL,
  PRIMARY KEY (`id`, `aid`),
  INDEX `aid_idx` (`aid` ASC),
  CONSTRAINT `id`
    FOREIGN KEY (`id`)
    REFERENCES `ccm_test`.`Employee` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `aid`
    FOREIGN KEY (`aid`)
    REFERENCES `ccm_test`.`Assignment` (`aid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

INSERT INTO `ccm_test`.`Works_on` (`id`, `aid`) VALUES (0000000, 0);
INSERT INTO `ccm_test`.`Works_on` (`id`, `aid`) VALUES (1111111, 1);
INSERT INTO `ccm_test`.`Works_on` (`id`, `aid`) VALUES (2222222, 2);



SELECT * FROM Employee;
SELECT * FROM Assignment;
SELECT * FROM Works_on;

CREATE VIEW Current_Assignments(name, version, task, due, status) 
AS SELECT E.name, A.version, A.task, A.due, A.status
FROM Employee E, Assignment A, Works_on W
WHERE E.id = W.id AND W.aid = A.aid;

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

SELECT * FROM Vehicle;



CREATE TABLE IF NOT EXISTS `ccm_test`.`Checks_out` (
  `out` DATE NOT NULL,
  `exp_in` DATE NOT NULL,
  `in` VARCHAR(10) NULL,
  `emp_id` INT NOT NULL,
  `vin` VARCHAR(17) NOT NULL,
  `status` VARCHAR(45) NULL,
  `note` VARCHAR(100) NULL,
  INDEX `vin_idx` (`vin` ASC),
  PRIMARY KEY (`emp_id`, `vin`),
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

INSERT INTO `ccm_test`.`Checks_out` (`out`, `exp_in`, `in`, `emp_id`, `vin`, `status`, `note`) VALUES ('2018-01-01', '2018-02-03', '2018-02-02', 0000000, 'AAAAAAAAAAAAAAAAA', 'returned', 'Magic Martin');
INSERT INTO `ccm_test`.`Checks_out` (`out`, `exp_in`, `in`, `emp_id`, `vin`, `status`, `note`) VALUES ('2018-03-03', '2018-04-01', NULL, 1111111, 'BBBBBBBBBBBBBBBBB', 'overdue', NULL);
INSERT INTO `ccm_test`.`Checks_out` (`out`, `exp_in`, `in`, `emp_id`, `vin`, `status`, `note`) VALUES ('2018-05-05', '2018-06-06', NULL, 2222222, 'CCCCCCCCCCCCCCCCC', 'not returned', NULL);

CREATE VIEW Vehicle_Log
AS SELECT E.name, V.color, V.model, C.out, C.exp_in, C.in, C.status, C.note
FROM Employee E, Vehicle V, Checks_out C
WHERE E.id = C.emp_id AND C.vin = V.vin;

SELECT * FROM Vehicle_Log;



CREATE TABLE IF NOT EXISTS `ccm_test`.`Question` (
  `qid` INT NOT NULL,
  `q_time` DATETIME NOT NULL,
  `title` VARCHAR(45) NOT NULL,
  `content` VARCHAR(200) NOT NULL,
  `to_0` INT NOT NULL,
  `to_1` INT NULL,
  `to_2` INT NULL,
  `status` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`qid`))
ENGINE = InnoDB;

INSERT INTO `ccm_test`.`Question` (`qid`, `q_time`, `title`, `content`, `to_0`, `to_1`, `to_2`, `status`) VALUES (0, '2017-05-02', 'Time', 'What time is it?', 1111111, 2222222, NULL, 'closed');
INSERT INTO `ccm_test`.`Question` (`qid`, `q_time`, `title`, `content`, `to_0`, `to_1`, `to_2`, `status`) VALUES (1, '2017-05-03', 'Lunch', 'Where do you want to eat?', 0, 2222222, NULL, 'addressed');
INSERT INTO `ccm_test`.`Question` (`qid`, `q_time`, `title`, `content`, `to_0`, `to_1`, `to_2`, `status`) VALUES (2, '2017-05-04', 'Reserve table', 'Can you reserve a table? Be there real soon.', 1111111, NULL, NULL, 'open');


SELECT * FROM Question;



CREATE TABLE IF NOT EXISTS `ccm_test`.`Asks` (
  `ask_id` INT NOT NULL,
  `q_id` INT NOT NULL,
  PRIMARY KEY (`ask_id`, `q_id`),
  CONSTRAINT `ask_id`
    FOREIGN KEY (`ask_id`)
    REFERENCES `ccm_test`.`Employee` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `q_id`
    FOREIGN KEY (`q_id`)
    REFERENCES `ccm_test`.`Question` (`qid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

INSERT INTO `ccm_test`.`Asks` (`ask_id`, `q_id`) VALUES (0000000, 0);
INSERT INTO `ccm_test`.`Asks` (`ask_id`, `q_id`) VALUES (1111111, 1);
INSERT INTO `ccm_test`.`Asks` (`ask_id`, `q_id`) VALUES (2222222, 2);

SELECT * FROM Asks;



CREATE VIEW Discussion_Board 
AS SELECT Q.qid, Q.q_time, E.name, Q.title, Q.content, Q.status
FROM Asks A, Employee E, Question Q
WHERE A.ask_id = E.id AND A.q_id = Q.qid; 

SELECT * FROM Discussion_Board;



CREATE TABLE IF NOT EXISTS `ccm_test`.`Answer` (
  `aid` INT NOT NULL,
  `a_time` DATETIME NOT NULL,
  `content` VARCHAR(400) NOT NULL,
  PRIMARY KEY (`aid`))
ENGINE = InnoDB;

INSERT INTO `ccm_test`.`Answer` (`aid`, `a_time`, `content`) VALUES (0, '2017-05-02 15:30:00', 'Snack time.');
INSERT INTO `ccm_test`.`Answer` (`aid`, `a_time`, `content`) VALUES (1, '2017-05-03', 'Asian Buffet.');

SELECT * FROM Answer;



CREATE TABLE IF NOT EXISTS `ccm_test`.`Ans_by` (
  `question_id` INT NOT NULL,
  `answer_id` INT NOT NULL,
  `e_id` INT NOT NULL,
  PRIMARY KEY (`question_id`, `answer_id`, `e_id`),
  INDEX `a_id_idx` (`answer_id` ASC),
  INDEX `e_id_idx` (`e_id` ASC),
  CONSTRAINT `question_id`
    FOREIGN KEY (`question_id`)
    REFERENCES `ccm_test`.`Question` (`qid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `answer_id`
    FOREIGN KEY (`answer_id`)
    REFERENCES `ccm_test`.`Answer` (`aid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `e_id`
    FOREIGN KEY (`e_id`)
    REFERENCES `ccm_test`.`Employee` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

INSERT INTO `ccm_test`.`Ans_by` (`question_id`, `answer_id`, `e_id`) VALUES (0, 0, 2222222);
INSERT INTO `ccm_test`.`Ans_by` (`question_id`, `answer_id`, `e_id`) VALUES (1, 1, 1111111);

SELECT * FROM Ans_by;
