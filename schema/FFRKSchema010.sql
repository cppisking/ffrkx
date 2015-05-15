
# Change `dungeons`.`series` to be non-NULL
ALTER TABLE `dungeons` DROP FOREIGN KEY `FK_dungeon_series`;
ALTER TABLE `dungeons` CHANGE COLUMN `series` `series` INT(10) UNSIGNED NOT NULL ;
ALTER TABLE `dungeons` 
ADD CONSTRAINT `FK_dungeon_series`
  FOREIGN KEY (`series`)
  REFERENCES `series` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

INSERT INTO `schema_version` VALUES (10);
