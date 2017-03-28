@echo off

setlocal

set src_path=%~dp0
set dest_path=%~dp0..\GillSoft.ExpressionEvaluator

java -jar c:\Dev\000-DevTools\antlr\antlr-4.6-complete.jar ..\Grammar\Expression.g4 -no-listener -Dlanguage=CSharp -visitor -o "%dest_path%" -package GillSoft.ExpressionEvaluator

move "%src_path%*.cs" "%dest_path%"
del "%src_path%*.tokens"

endlocal