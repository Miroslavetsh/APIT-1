# APIT
> version 1.0.0 alpha

Get started in back-end
-------------------------

### Add new migration

Open terminal (cmd.exe / PowerShell / Terminal.app / ...)

1. `dotnet tool install --global dotnet-ef` => install EntityFramework globally
2. `dotnet ef migrations add [migration-name]` => creating migration (add flag `-v` or `--verbose` to enable logs)
3. `dotnet ef database update` => apply changes to database
-------------------------
