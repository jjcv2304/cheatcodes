cd /d D:\GIT\CheatCodes\Api
start "" dotnet publish -o c:\temp\deployment\cheatCodes
start "" robocopy D:\GIT\CheatCodes\CheatCodes.UI C:\temp\deployment\CheatCodes.UI /s /e /XD D:\GIT\CheatCodes\CheatCodes.UI\node_modules

