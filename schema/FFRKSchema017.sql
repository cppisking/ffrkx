CREATE TABLE `ffrktest`.`equipment_stats` (
  `equipment_id` INT UNSIGNED NOT NULL,
  `base_atk` SMALLINT UNSIGNED NOT NULL,
  `base_mag` SMALLINT UNSIGNED NOT NULL,
  `base_acc` SMALLINT UNSIGNED NOT NULL,
  `base_def` SMALLINT UNSIGNED NOT NULL,
  `base_res` SMALLINT UNSIGNED NOT NULL,
  `base_eva` SMALLINT UNSIGNED NOT NULL,
  `base_mnd` SMALLINT UNSIGNED NOT NULL,
  `max_atk` SMALLINT UNSIGNED NOT NULL,
  `max_mag` SMALLINT UNSIGNED NOT NULL,
  `max_acc` SMALLINT UNSIGNED NOT NULL,
  `max_def` SMALLINT UNSIGNED NOT NULL,
  `max_res` SMALLINT UNSIGNED NOT NULL,
  `max_eva` SMALLINT UNSIGNED NOT NULL,
  `max_mnd` SMALLINT UNSIGNED NOT NULL,
  PRIMARY KEY (`equipment_id`),
  UNIQUE INDEX `equipment_id_UNIQUE` (`equipment_id` ASC),
  CONSTRAINT `FK_equipment_stat_id`
    FOREIGN KEY (`equipment_id`)
    REFERENCES `ffrktest`.`items` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


# Set the correct type for armors
UPDATE items SET type=2 WHERE type=1 AND subtype>30 AND subtype<80;

# Set the correct type for accessories
UPDATE items SET type=3 WHERE type=1 AND subtype=80;

# Set the correct type for upgrade materials.
UPDATE items SET series=1, type=4 WHERE subtype=98 OR subtype=99;
UPDATE items SET series=1, type=4, subtype=99 WHERE id=24099001;

# Set the correct type for orbs.
UPDATE items SET type=5 WHERE id>=40000001 AND id<=40000065;

# A couple of items that were incorrectly listed as having 0 rarity.
UPDATE items SET rarity=2 WHERE id IN (23080002,23080003);
UPDATE items SET rarity=3 WHERE id IN (23080010,23080011,23080019,23080020);

INSERT INTO schema_version VALUES (17, false);
