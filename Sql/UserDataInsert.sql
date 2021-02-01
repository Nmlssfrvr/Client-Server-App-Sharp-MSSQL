USE Ustinov;

SET IDENTITY_INSERT UserData ON;

INSERT INTO UserData (u_id, u_info, u_login, u_pass_hash, u_role) VALUES (1,1,'user1','user1',1);
INSERT INTO UserData (u_id, u_info, u_login, u_pass_hash, u_role) VALUES (2,2,'user2','user2',1);
INSERT INTO UserData (u_id, u_info, u_login, u_pass_hash, u_role) VALUES (3,3,'user3','user3',1);
INSERT INTO UserData (u_id, u_info, u_login, u_pass_hash, u_role) VALUES (4,4,'user4','user4',1);
INSERT INTO UserData (u_id, u_info, u_login, u_pass_hash, u_role) VALUES (5,5,'user5','user5',1);
INSERT INTO UserData (u_id, u_info, u_login, u_pass_hash, u_role) VALUES (6,6,'user6','user6',1);
INSERT INTO UserData (u_id, u_info, u_login, u_pass_hash, u_role) VALUES (7,7,'worker1','worker1',2);
INSERT INTO UserData (u_id, u_info, u_login, u_pass_hash, u_role) VALUES (8,8,'worker2','worker2',2);
INSERT INTO UserData (u_id, u_info, u_login, u_pass_hash, u_role) VALUES (9,9,'admin','admin',3);

SET IDENTITY_INSERT UserData OFF;

