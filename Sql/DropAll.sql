USE Ustinov;
--Drop tables if exist
drop table if exists Parcel;
drop table if exists Inventory;
drop table if exists UserData;
drop table if exists Client;
drop table if exists MailPost;
drop table if exists UserGroup;
drop table if exists Roles;
drop table if exists Groups;

--Drop procedures, functions and trigger if exist
drop procedure if exists dbo.P_Client_Create
drop procedure if exists dbo.P_Client_Update
drop procedure if exists dbo.P_Client_Delete
drop procedure if exists dbo.P_Groups_Create
drop procedure if exists dbo.P_Groups_Update
drop procedure if exists dbo.P_Groups_Delete
drop procedure if exists dbo.P_Inventory_Create
drop procedure if exists dbo.P_Inventory_Update
drop procedure if exists dbo.P_Inventory_Delete
drop procedure if exists dbo.P_MailPost_Create
drop procedure if exists dbo.P_MailPost_Update
drop procedure if exists dbo.P_MailPost_Delete
drop procedure if exists dbo.P_Parcel_Create
drop procedure if exists dbo.P_Parcel_Update
drop procedure if exists dbo.P_Parcel_Delete
drop procedure if exists dbo.P_Roles_Create
drop procedure if exists dbo.P_Roles_Update
drop procedure if exists dbo.P_Roles_Delete
drop procedure if exists dbo.P_UserData_Create
drop procedure if exists dbo.P_UserData_Update
drop procedure if exists dbo.P_UserData_Delete
drop procedure if exists dbo.P_UserGroup_Create
drop procedure if exists dbo.P_UserGroup_Update
drop procedure if exists dbo.P_UserGroup_Delete
drop procedure if exists dbo.P_UpdateTimeById
drop procedure if exists dbo.P_UpdateTimeWithCursor

drop function if exists dbo.ParcelsTable
drop function if exists dbo.ValidUser

drop trigger if exists T_MailPost_Insert_Update
--Drop Database
drop database Ustinov;