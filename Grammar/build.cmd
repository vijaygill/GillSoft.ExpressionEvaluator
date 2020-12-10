@echo off

setlocal

set src_path=%~dp0
set dest_path=%~dp0..\GillSoft.ExpressionEvaluator\AntlrGenerated

java -jar ..\tools\antlr-4.8-complete.jar ..\Grammar\Expression.g4 -no-listener -Dlanguage=CSharp -visitor -o ./ -package GillSoft.ExpressionEvaluator
java -jar ..\tools\antlr-4.8-complete.jar ..\Grammar\xpath.g4 -no-listener -Dlanguage=CSharp -visitor -o ./ -package GillSoft.ExpressionEvaluator

move "%src_path%*.cs" "%dest_path%"
del "%src_path%*.interp"
del "%src_path%*.tokens"

endlocal

pause