USE Ustinov;

SET IDENTITY_INSERT MailPost ON;

INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (1,101000,'������, ��������� ��, 26�, ���.1', 78001000000,3);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (2,119002,'������, ����� ��, 47/23', 78001000000,2);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (3,123001,'������, ������������ ��, 27/24', 78001000000,1);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (4,109004,'������, ���������� ����������� ��, 24', 78001000000,0);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (5,105005,'������, ���������� ��, 38, ���.2', 78001000000,1);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (6,127006,'������, �������� ��� ��, 5/10, ���.2', 78001000000,2);

SET IDENTITY_INSERT MailPost OFF;