@echo off
set MIGRATION_NAME=initial_migration

echo === ADICIONANDO MIGRATION "%MIGRATION_NAME%" ===
dotnet ef migrations add %MIGRATION_NAME% --startup-project ../BigEcommerce.Producer.Sales.Presentation

echo === APLICANDO UPDATE NO BANCO ===
dotnet ef database update --startup-project ../BigEcommerce.Producer.Sales.Presentation

pause