@ECHO OFF
setlocal

set DOTNET_HOME=C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319
set MSBUILD_EXE=%DOTNET_HOME%\msbuild.exe /m /p:Prefer32Bit=false

rem set MSBUILD_EXE=
rem for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v4*) do set MSBUILD_EXE=%%D\MSBuild.exe

rem if not defined MSBUILD_EXE echo error: can't find MSBuild.exe & goto :eof
rem if not exist "%MSBUILD_EXE%" echo error: %msbuild%: not found & goto :eof

.\tools\nuget restore GillSoft.ExpressionEvaluator.sln
%MSBUILD_EXE% /m /p:Prefer32Bit=false /p:Configuration=Debug GillSoft.ExpressionEvaluator.sln /t:clean
%MSBUILD_EXE% /m /p:Prefer32Bit=false /p:Configuration=Release GillSoft.ExpressionEvaluator.sln /t:clean

%MSBUILD_EXE% /m /p:Prefer32Bit=false /p:Configuration=Release GillSoft.ExpressionEvaluator.sln /t:GillSoft_ExpressionEvaluator

:eof
endlocal
pause