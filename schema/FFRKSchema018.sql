CREATE TABLE `characters` (
  `id` INT UNSIGNED NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));

CREATE TABLE `equip_usage` (
  `character_id` INT UNSIGNED NOT NULL,
  `equip_category` TINYINT(4) UNSIGNED NOT NULL,
  PRIMARY KEY (`character_id`, `equip_category`),
  CONSTRAINT `FK_Equip_Character`
    FOREIGN KEY (`character_id`)
    REFERENCES `characters` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
