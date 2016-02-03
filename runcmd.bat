pushd  \\centre-fsrv02\ESHome$\073620775\Not-Work\ConsoleGame
title Command Executor
color 07
:execute
echo Please Type A Command Here:
set /p cmd=Command:
%cmd%
GOTO:execute
