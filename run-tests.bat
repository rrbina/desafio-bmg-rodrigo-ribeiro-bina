@echo off
echo Rodando testes unitarios do projeto...

REM Caminho base do projeto .NET
cd Backend\tests\BigEcommerce.Sales.UnitTests


REM Executa os testes
dotnet test

pause
