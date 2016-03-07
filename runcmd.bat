@echo off
pushd  R:\JEROM\GR08_Berardi\KillAll Game\ConsoleGame
title Command Executor
color 07
:execute
echo Please Type A Command Here:
set /p cmd=Command:
%cmd%
GOTO:execute
