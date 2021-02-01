USE Ustinov;

SET IDENTITY_INSERT Inventory ON;

INSERT INTO Inventory (i_id, i_product_name, i_amount, i_price, i_fragile) VALUES (1,'product1',100,1250,'no');
INSERT INTO Inventory (i_id, i_product_name, i_amount, i_price, i_fragile) VALUES (2,'product2',200,1500,'yes');
INSERT INTO Inventory (i_id, i_product_name, i_amount, i_price, i_fragile) VALUES (3,'product3',300,1750,'yes');
INSERT INTO Inventory (i_id, i_product_name, i_amount, i_price, i_fragile) VALUES (4,'product4',400,2000,'no');
INSERT INTO Inventory (i_id, i_product_name, i_amount, i_price, i_fragile) VALUES (5,'product5',500,2250,'no');

SET IDENTITY_INSERT Inventory OFF;