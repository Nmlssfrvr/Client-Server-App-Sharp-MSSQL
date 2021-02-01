USE Ustinov;

SET IDENTITY_INSERT Client ON;

INSERT INTO Client (c_id, c_index, c_FN, c_passport_series, c_passport_number) VALUES (1, 101000, 'Иванов Иван Иванович', 1111, 111111);
INSERT INTO Client (c_id, c_index, c_FN, c_passport_series, c_passport_number) VALUES (2, 101000, 'Петров Петр Петрович', 2222, 222222);
INSERT INTO Client (c_id, c_index, c_FN, c_passport_series, c_passport_number) VALUES (3, 119002, 'Иванов Александр Александрович', 3333, 333333);
INSERT INTO Client (c_id, c_index, c_FN, c_passport_series, c_passport_number) VALUES (4, 123001, 'Дудник Андрей Романович', 4444, 444444);
INSERT INTO Client (c_id, c_index, c_FN, c_passport_series, c_passport_number) VALUES (5, 123001, 'Тринкер Борис Давидович', 5555, 555555);
INSERT INTO Client (c_id, c_index, c_FN, c_passport_series, c_passport_number) VALUES (6, 109004, 'Васильев Михаил Петрович', 6666, 666666);
INSERT INTO Client (c_id, c_index, c_FN, c_passport_series, c_passport_number) VALUES (7, 105005, 'Краев Алексей Анатольевич', 7777, 777777);
INSERT INTO Client (c_id, c_index, c_FN, c_passport_series, c_passport_number) VALUES (8, 105005, 'Васильев Андрей Владимирович', 8888, 888888);
INSERT INTO Client (c_id, c_index, c_FN, c_passport_series, c_passport_number) VALUES (9, 127006, 'Полежайкин Илья Сергеевич', 9999, 999999);

SET IDENTITY_INSERT Client OFF;