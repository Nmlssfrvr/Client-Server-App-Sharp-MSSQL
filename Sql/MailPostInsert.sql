USE Ustinov;

SET IDENTITY_INSERT MailPost ON;

INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (1,101000,'Москва, Мясницкая ул, 26А, стр.1', 78001000000,3);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (2,119002,'Москва, Арбат ул, 47/23', 78001000000,2);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (3,123001,'Москва, Спиридоновка ул, 27/24', 78001000000,1);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (4,109004,'Москва, Александра Солженицына ул, 24', 78001000000,0);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (5,105005,'Москва, Бауманская ул, 38, стр.2', 78001000000,1);
INSERT INTO MailPost (m_id, m_index, m_address, m_phone, m_postamat_count) VALUES (6,127006,'Москва, Каретный Ряд ул, 5/10, стр.2', 78001000000,2);

SET IDENTITY_INSERT MailPost OFF;