DROP procedure IF EXISTS `transfer_missing_items`;

DELIMITER $$
CREATE PROCEDURE `transfer_missing_items`()
BEGIN
	INSERT INTO equipment_stats
	SELECT equipment_id, base_atk, base_mag, base_acc, base_def, base_res, base_eva, base_mnd, max_atk, max_mag, max_acc, max_def, max_res, max_eva, max_mnd
	FROM missing_items; 
END$$

DELIMITER ;

INSERT INTO schema_version VALUES (20, false);