USE Ustinov;

SET IDENTITY_INSERT Groups ON;

INSERT INTO Groups (g_id, g_group) VALUES (1,'recievers');
INSERT INTO Groups (g_id, g_group) VALUES (2,'workers');
INSERT INTO Groups (g_id, g_group) VALUES (3,'administrators');

SET IDENTITY_INSERT Groups OFF;