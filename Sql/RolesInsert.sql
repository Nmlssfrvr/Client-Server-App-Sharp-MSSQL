USE Ustinov;

SET IDENTITY_INSERT Roles ON;

INSERT INTO Roles (r_id,r_role) VALUES (1, 'reciever');
INSERT INTO Roles (r_id, r_role) VALUES (2, 'worker');
INSERT INTO Roles (r_id, r_role) VALUES (3, 'admin');

SET IDENTITY_INSERT Roles OFF;