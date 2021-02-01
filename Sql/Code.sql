IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Ustinov')
BEGIN
	CREATE DATABASE	Ustinov;
END;
GO

USE Ustinov;

CREATE TABLE  MailPost (
	m_id INT IDENTITY PRIMARY KEY not null,
	m_index DECIMAL(9,0) UNIQUE not null CHECK (m_index > 0),
	m_address NVARCHAR(70) not null CHECK (m_address != ''),
	m_phone DECIMAL(11,0) not null,
	m_postamat_count TINYINT not null,
	m_last_update_time DATETIME,
	
	CONSTRAINT phone_number CHECK ((CAST(m_phone as bigint)/CAST(10000000000 as bigint) = 7) AND (m_phone > 0)),
);

CREATE TABLE Client (
	c_id INT IDENTITY PRIMARY KEY not null,
	c_index DECIMAL(9,0),
	c_FN NVARCHAR(70) not null CHECK (c_FN != ''),
	c_passport_series SMALLINT not null CHECK (c_passport_series > 0),
	c_passport_number INT not null CHECK (c_passport_number > 0),
	
	FOREIGN KEY (c_index) REFERENCES MailPost (m_index) ON DELETE SET NULL ON UPDATE CASCADE 
);

CREATE TABLE Inventory (
	i_id INT IDENTITY PRIMARY KEY not null,
	i_product_name NVARCHAR(50) not null ,
	i_amount INT not null CHECK (i_amount > 0),
	i_price MONEY not null CHECK (i_price >= 0),
	i_fragile NVARCHAR(3) not null CHECK (i_fragile IN('yes','no'))
);

CREATE TABLE Parcel (
	p_id INT IDENTITY PRIMARY KEY not null,
	p_tax MONEY not null CHECK (p_tax >= 0),
	p_reciever INT,
	p_inventory INT,
	p_office INT,
	p_tariff NVARCHAR(10) not null CHECK (p_tariff != ''),

	CONSTRAINT tariffs CHECK (p_tariff IN('normal','speed','precious')),
	FOREIGN KEY (p_office) REFERENCES MailPost (m_id) ON DELETE SET NULL ON UPDATE CASCADE,
	FOREIGN KEY (p_reciever) REFERENCES Client (c_id) ON DELETE CASCADE,
	FOREIGN KEY (p_inventory) REFERENCES Inventory (i_id) ON DELETE SET NULL ON UPDATE CASCADE,
);

--reciever,worker, admin
CREATE TABLE Roles (
	r_id INT IDENTITY PRIMARY KEY not null,
	r_role NVARCHAR(10) UNIQUE not null
);

--recievers,workers,administrators
CREATE TABLE Groups (
	g_id INT IDENTITY PRIMARY KEY not null,
	g_group NVARCHAR(20) UNIQUE not null
);

CREATE TABLE UserGroup (
	ug_id INT IDENTITY PRIMARY KEY not null,
	ug_group INT,
	ug_role INT,

	FOREIGN KEY (ug_group) REFERENCES Groups (g_id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (ug_role) REFERENCES Roles (r_id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE UserData (
	u_id INT IDENTITY PRIMARY KEY not null,
	u_info INT UNIQUE,
	u_login NVARCHAR(40) UNIQUE not null CHECK (u_login != ''),
	u_pass_hash NVARCHAR(100) not null CHECK (u_pass_hash != ''),
	u_role INT DEFAULT 1,

	FOREIGN KEY (u_info) REFERENCES Client (c_id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (u_role) REFERENCES Groups (g_id) ON DELETE SET DEFAULT ON UPDATE CASCADE
);

GO

CREATE OR ALTER TRIGGER T_MailPost_Insert_Update
ON MailPost
AFTER INSERT, UPDATE
AS
UPDATE MailPost SET m_last_update_time = SYSDATETIME() 
WHERE m_id = (SELECT m_id FROM inserted)

GO

CREATE OR ALTER PROCEDURE P_MailPost_Create
	@new_index decimal(9,0),
	@new_address nvarchar(70),
	@new_phone decimal(11,0),
	@new_postamat_count tinyint
AS
	BEGIN TRAN
		INSERT INTO MailPost (m_index, m_address, m_phone, m_postamat_count) VALUES (@new_index, @new_address, @new_phone, @new_postamat_count);
	IF (@new_index < 100000) ROLLBACK TRAN
	ELSE COMMIT

	SELECT SCOPE_IDENTITY()
GO

CREATE OR ALTER PROCEDURE P_MailPost_Update
	@id int,
	@new_index decimal(9,0),
	@new_address nvarchar(70),
	@new_phone decimal(11,0),
	@new_postamat_count tinyint
AS
	UPDATE MailPost SET m_index = @new_index,  m_address = @new_address, m_phone = @new_phone, m_postamat_count = @new_postamat_count WHERE m_id = @id

GO

CREATE OR ALTER PROCEDURE P_MailPost_Delete
	@id int
AS
	DELETE FROM MailPost WHERE m_id = @id
GO

CREATE OR ALTER PROCEDURE P_Client_Create
	@new_index decimal(9,0),
	@new_FN nvarchar(70),
	@new_passport_series smallint,
	@new_passport_number int
AS
	INSERT INTO Client (c_index,c_FN,c_passport_series,c_passport_number) VALUES (@new_index, @new_FN, @new_passport_series, @new_passport_number)

	SELECT SCOPE_IDENTITY()
GO

CREATE OR ALTER PROCEDURE P_Client_Update
	@id int,
	@new_index decimal(9,0),
	@new_FN nvarchar(70),
	@new_passport_series smallint,
	@new_passport_number int
AS
	UPDATE Client SET c_index = @new_index,  c_FN = @new_FN, c_passport_series = @new_passport_series, c_passport_number = @new_passport_number WHERE c_id = @id
GO

CREATE OR ALTER PROCEDURE P_Client_Delete
	@id int
AS
	DELETE FROM Client WHERE c_id = @id
GO	

CREATE OR ALTER PROCEDURE P_Inventory_Create
	@new_product_name nvarchar(50),
	@new_amount int,
	@new_price money,
	@new_fragile nvarchar(3)
AS
	INSERT INTO Inventory(i_product_name,i_amount,i_price,i_fragile) VALUES (@new_product_name,@new_amount,@new_price,@new_fragile)

	SELECT SCOPE_IDENTITY()
GO

CREATE OR ALTER PROCEDURE P_Inventory_Update
	@id int,
	@new_product_name nvarchar(50),
	@new_amount int,
	@new_price money,
	@new_fragile nvarchar(3)
AS
	UPDATE Inventory SET i_product_name = @new_product_name,  i_amount = @new_amount, i_price = @new_price, i_fragile = @new_fragile WHERE i_id = @id
GO

CREATE OR ALTER PROCEDURE P_Inventory_Delete
	@id int
AS
	DELETE FROM Inventory WHERE i_id = @id
GO

CREATE OR ALTER PROCEDURE P_Parcel_Create
	@new_tax money,
	@new_reciever int,
	@new_inventory int,
	@new_office int,
	@new_tariff nvarchar(10)
AS
	INSERT INTO Parcel(p_tax,p_reciever,p_inventory,p_office,p_tariff) VALUES (@new_tax,@new_reciever,@new_inventory,@new_office,@new_tariff)

	SELECT SCOPE_IDENTITY()
GO

CREATE OR ALTER PROCEDURE P_Parcel_Update
	@id int,
	@new_tax money,
	@new_reciever int,
	@new_inventory int,
	@new_office int,
	@new_tariff nvarchar(10)
AS
	UPDATE Parcel SET p_tax = @new_tax, p_reciever = @new_reciever, p_inventory = @new_inventory, p_office = @new_office, p_tariff = @new_tariff WHERE p_id = @id
GO

CREATE OR ALTER PROCEDURE P_Parcel_Delete
	@id int
AS
	DELETE FROM Parcel WHERE p_id = @id
GO

CREATE OR ALTER PROCEDURE P_Roles_Create
	@new_role nvarchar(10)
AS
	INSERT INTO Roles(r_role) VALUES (@new_role)

	SELECT SCOPE_IDENTITY()
GO

CREATE OR ALTER PROCEDURE P_Roles_Update
	@id int,
	@new_role nvarchar(10)
AS
	UPDATE Roles SET r_role = @new_role WHERE r_id = @id
GO

CREATE OR ALTER PROCEDURE P_Roles_Delete
	@id int
AS
	DELETE FROM Roles WHERE r_id = @id
GO

CREATE OR ALTER PROCEDURE P_Groups_Create
	@new_group nvarchar(20)
AS
	INSERT INTO Groups(g_group) VALUES (@new_group)

	SELECT SCOPE_IDENTITY()
GO

CREATE OR ALTER PROCEDURE P_Groups_Update
	@id int,
	@new_group nvarchar(20)
AS
	UPDATE Groups SET g_group = @new_group WHERE g_id = @id
GO

CREATE OR ALTER PROCEDURE P_Groups_Delete
	@id int
AS
	DELETE FROM Groups WHERE g_id = @id
GO

CREATE OR ALTER PROCEDURE P_UserGroup_Create
	@new_group int,
	@new_role int
AS
	INSERT INTO UserGroup(ug_group,ug_role) VALUES (@new_group,@new_role)

	SELECT SCOPE_IDENTITY()
GO

CREATE OR ALTER PROCEDURE P_UserGroup_Update
	@id int,
	@new_group int,
	@new_role int
AS
	UPDATE UserGroup SET ug_group = @new_group, ug_role = @new_role WHERE ug_id = @id
GO

CREATE OR ALTER PROCEDURE P_UserGroup_Delete
	@id int
AS
	DELETE FROM UserGroup WHERE ug_id = @id
GO

CREATE OR ALTER PROCEDURE P_UserData_Create
	@new_info int,
	@new_login nvarchar(40),
	@new_pass nvarchar(100),
	@new_role int
AS
	INSERT INTO UserData(u_info,u_login,u_pass_hash,u_role) VALUES (@new_info,@new_login,@new_pass,@new_role)

	SELECT SCOPE_IDENTITY()
GO

CREATE OR ALTER PROCEDURE P_UserData_Update
	@id int,
	@new_info int,
	@new_login nvarchar(40),
	@new_pass nvarchar(100),
	@new_role int
AS
	UPDATE UserData SET u_info = @new_info, u_login = @new_login, u_pass_hash = @new_pass, u_role = @new_role WHERE u_id = @id
GO

CREATE OR ALTER PROCEDURE P_UserData_Delete
	@id int
AS
	DELETE FROM UserData WHERE u_id = @id
GO

CREATE OR ALTER FUNCTION ValidUser(@login nvarchar(40), @pass nvarchar(100))
RETURNS INT
AS
BEGIN
	DECLARE @out int;
	select @out = UserData.u_id from UserData where UserData.u_login = @login and UserData.u_pass_hash = @pass
	IF (@out IS NULL)   
        SET @out = 0
	ELSE SET @out = 1
	RETURN @out
END
GO

CREATE OR ALTER FUNCTION ParcelsTable(@id int)
RETURNS TABLE
AS
	RETURN select (case when Inventory.i_product_name is null then 'нет посылок' else cast(Inventory.i_product_name as nvarchar(50)) end) as Name,
					(case when Inventory.i_amount is null then 0 else Inventory.i_amount end) as Amount, 
					(case when Parcel.p_tax is null then 0 else Parcel.p_tax end) as Tax,
					MailPost.m_address as Address
		from Inventory
		join Parcel on Inventory.i_id = Parcel.p_inventory
		full join Client on Parcel.p_reciever = Client.c_id
		full join MailPost on Client.c_index = MailPost.m_index
			where Client.c_id = @id
GO

CREATE OR ALTER PROCEDURE P_UpdateTimeById
	@id int
AS
	 UPDATE MailPost SET m_last_update_time = SYSDATETIME() WHERE m_last_update_time IS NULL and m_id = @id
GO

CREATE OR ALTER PROCEDURE P_UpdateTimeWithCursor
AS
 DECLARE @id int
 DECLARE cur CURSOR
  FOR SELECT m_id FROM MailPost

	OPEN cur
	FETCH NEXT FROM cur INTO @id
	WHILE @@FETCH_STATUS = 0
	 BEGIN   
        EXECUTE dbo.P_UpdateTimeById @id
        FETCH NEXT FROM cur INTO @id
	 END
	CLOSE cur
    DEALLOCATE cur
GO

CREATE INDEX MailIndex ON MailPost (m_index)
CREATE INDEX ClientIndex ON Client (c_FN)
CREATE INDEX InventoryIndex ON Inventory (i_product_name)

GO

CREATE OR ALTER VIEW MoscowPosts
	AS SELECT *, (select count(*) from Parcel where Parcel.p_office = MailPost.m_id) as ParcelsCount, (select count(*) from Client where Client.c_index = MailPost.m_index) as ClientsCount
		FROM MailPost 
			WHERE m_address LIKE '%Москва%'
