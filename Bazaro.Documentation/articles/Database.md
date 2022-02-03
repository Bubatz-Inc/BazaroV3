# Database
## General
We are using a Postgres Database, hosted on an V-Server at Strato. We have createt 2 Databases, 
1. Bazaro (Production) and Bazaro_dev (Development). 
## Update Database
Beucause of an Error that happen when you try to run the Command
	Update-database
you need to run the SQL Script manually on the Database. 
### Initial
You can generate the SQL Script with the command in the Powershell
	dotnet ef Database update script 
Copy Paste this script in to your Database Managment Software and run the script.
### Update
Same as before but the Command is 
	dotnet ef Database update script [Migration Name]
