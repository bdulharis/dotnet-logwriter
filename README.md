# dotnet-logwriter
this is a super simple library for writing log to a local file

usage :

LogWriter myLogwriter = new LogWriter(processName);
myLogwriter.writeLog(myLogData);

the log will be written in this format:
yyyy/MM/dd;HH:mm:ss;myLogData

features:
1. daily rolling
2. autocheck for filelock
