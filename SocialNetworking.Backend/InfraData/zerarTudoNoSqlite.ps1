Remove-Item ..\APIRest\database.db
Remove-Item Migrations -Recurse
dotnet ef migrations add Initial -c ApplicationDbContext -s ..\APIRest\ -v
dotnet ef database update -c ApplicationDbContext -s ..\APIRest\ -v