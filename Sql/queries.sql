--roles of all persons
SELECT distinct Roles.r_role, Client.c_FN 
	FROM UserData
	JOIN UserGroup ON UserData.u_role = UserGroup.ug_group
	JOIN Roles ON UserGroup.ug_role = Roles.r_id
	JOIN Client ON UserData.u_info = Client.c_id

--2 a 
select Client.c_FN, MailPost.m_index, MailPost.m_address,
	(case when Inventory.i_product_name is null then 'нет посылок' else cast(Inventory.i_product_name as nvarchar(50)) end) as Product
	from Inventory
	join Parcel on Inventory.i_id = Parcel.p_inventory
	full join Client on Parcel.p_reciever = Client.c_id
	full join MailPost on Client.c_index = MailPost.m_index

--2 b
--в code.sql create or alter view MoscowPosts...

--2 c

--select кор.
select MailPost.m_index, MailPost.m_address, 
	(select count(*) from Parcel where Parcel.p_office = MailPost.m_id) as ParcelsCount 
		from MailPost

--select некор.
select (select AVG(p_tax) from Parcel) as AVG_Tax,(select AVG(i_price) from Inventory) as AVG_Price

--from кор.
select fClient.c_FN, i_product_name,i_amount,i_price,p_tax
	from Parcel outer apply (select c_FN from Client where Client.c_id = Parcel.p_reciever) as fClient, Inventory 
		where Parcel.p_inventory = Inventory.i_id

--from некор.
select max(tb.somemoney) as MaximumTaxAndPrice 
	from (select (Parcel.p_tax + Inventory.i_price) as somemoney 
		from Parcel, Inventory, MailPost 
			where Parcel.p_inventory = Inventory.i_id and Parcel.p_office = MailPost.m_id and MailPost.m_index = 101000) as tb

--where кор.
select distinct MailPost.m_address 
	from MailPost, Inventory, Parcel 
		where 'Пользовательский параметр' in (select Inventory.i_product_name from Inventory where Inventory.i_id = Parcel.p_inventory) and Parcel.p_office = MailPost.m_id

--where некор.
select * 
	from Inventory
		where i_amount >= (select AVG(i_amount) from Inventory)

--2 d
select MailPost.m_address ,(select count (*) from Parcel, Inventory where Parcel.p_office = MailPost.m_id and Inventory.i_fragile = 'yes' and Parcel.p_inventory = Inventory.i_id) as FragileCount
	from MailPost
		group by MailPost.m_address, MailPost.m_id
		having (select count (*) from Parcel, Inventory where Parcel.p_office = MailPost.m_id and Inventory.i_fragile = 'yes' and Parcel.p_inventory = Inventory.i_id) > 0

--2 e

select MailPost.m_index, MailPost.m_address from MailPost where MailPost.m_index != all (select Client.c_index from Client)

