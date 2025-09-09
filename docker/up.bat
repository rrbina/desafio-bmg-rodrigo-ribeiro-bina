@echo off
echo Iniciando os containers em background...
docker compose up -d
echo.
echo API Producer: http://localhost:5100
echo.
echo Swagger Producer: http://localhost:5100/swagger/
echo.
echo Pressione qualquer tecla para fechar esta janela...
choice /n /c YN /m ""