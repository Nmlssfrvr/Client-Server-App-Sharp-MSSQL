USE Ustinov;

SET IDENTITY_INSERT Parcel ON;

INSERT INTO Parcel (p_id, p_tax, p_reciever, p_inventory, p_office, p_tariff) VALUES (1, 100, 1, 1, 1, 'normal');
INSERT INTO Parcel (p_id, p_tax, p_reciever, p_inventory, p_office, p_tariff) VALUES (2, 200, 2, 4, 1, 'speed');
INSERT INTO Parcel (p_id, p_tax, p_reciever, p_inventory, p_office, p_tariff) VALUES (3, 100, 3, 2, 2, 'normal');
INSERT INTO Parcel (p_id, p_tax, p_reciever, p_inventory, p_office, p_tariff) VALUES (4, 400, 4, 3, 3, 'precious');
INSERT INTO Parcel (p_id, p_tax, p_reciever, p_inventory, p_office, p_tariff) VALUES (5, 200, 5, 5, 3, 'speed');
INSERT INTO Parcel (p_id, p_tax, p_reciever, p_inventory, p_office, p_tariff) VALUES (6, 400, 7, 3, 5, 'precious');
INSERT INTO Parcel (p_id, p_tax, p_reciever, p_inventory, p_office, p_tariff) VALUES (7, 200, 9, 4, 6, 'speed');

SET IDENTITY_INSERT Parcel OFF;
