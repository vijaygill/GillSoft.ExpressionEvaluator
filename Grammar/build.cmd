@echo off

setlocal

set src_path=%~dp0
set dest_path=%~dp0..\GillSoft.ExpressionEvaluator\AntlrGenerated

if not exist %dest_path% mkdir %dest_path%

set JAR=..\tools\antlr-4.9-complete.jar
set NAMESPACE=GillSoft.ExpressionEvaluator

java -jar %JAR% ..\Grammar\Expression.g4 -no-listener -Dlanguage=CSharp -visitor -o ./ -package %NAMESPACE%
java -jar %JAR% ..\Grammar\xpath.g4 -no-listener -Dlanguage=CSharp -visitor -o ./ -package %NAMESPACE%
java -jar %JAR% ..\Grammar\JsonPath.g4 -no-listener -Dlanguage=CSharp -visitor -o ./ -package %NAMESPACE%

if exist "%src_path%\.antlr" rd /s /q "%src_path%\.antlr"

move "%src_path%*.cs" "%dest_path%"
del "%src_path%*.interp"
del "%src_path%*.tokens"

endlocal

