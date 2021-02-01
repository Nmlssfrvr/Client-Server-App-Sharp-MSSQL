USE Ustinov;

SET IDENTITY_INSERT UserGroup ON;

INSERT INTO UserGroup (ug_id, ug_group, ug_role) VALUES (1,1,1);
INSERT INTO UserGroup (ug_id, ug_group, ug_role) VALUES (2,2,1);
INSERT INTO UserGroup (ug_id, ug_group, ug_role) VALUES (3,2,2);
INSERT INTO UserGroup (ug_id, ug_group, ug_role) VALUES (4,3,1);
INSERT INTO UserGroup (ug_id, ug_group, ug_role) VALUES (5,3,2);
INSERT INTO UserGroup (ug_id, ug_group, ug_role) VALUES (6,3,3);

SET IDENTITY_INSERT UserGroup OFF;
