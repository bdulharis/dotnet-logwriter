using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;


namespace LogHandler
{
    /// <summary>
    /// AHK Product
    /// General class used for writing log to local file
    /// the log filename structure are:
    /// 1. <Application_name>_YYYMMDD.log
    /// 2. <Application_name>.<ThreadID>_YYYMMDD.log
    /// </summary>
    public class LogWriter
    {
        private string appName = "";
        private bool isWritingLog = false;

        public bool IsWritingLog
        {
            get { return isWritingLog; }
        }

        public LogWriter() { }
        public LogWriter(string appname)
        {
            appName = appname;
        }

       private FileStream WaitForFile(string fullPath, FileMode mode, FileAccess access, FileShare share)
        {
            for (int numTries = 0; numTries < 30; numTries++)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fullPath, mode, access, share);
                    return fs;
                }
                catch (IOException)
                {
                    if (fs != null)
                    {
                        fs.Dispose();
                    }
                    Thread.Sleep(30);
                }
            }

            return null;
        }

        public void writeLog(string logmessage)
        {
            string LogFolder = "";
            string LogFileName = "";

            if (appName == "") { appName = "APPDEFAULT"; }
            LogFolder = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
            if (!Directory.Exists(LogFolder)) { Directory.CreateDirectory(LogFolder); }

            LogFileName = LogFolder + appName + string.Format("{0:_yyyyMMdd}", DateTime.Now) + ".log";

            isWritingLog = true;
           // FileStream fs = new FileStream(LogFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            FileStream fs = WaitForFile(LogFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);

            string logmsg = string.Format("{0:yyyy/MM/dd;HH:mm:ss;}", DateTime.Now) + logmessage;
            m_streamWriter.WriteLine(logmsg);
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Dispose();
            m_streamWriter.Dispose();
            isWritingLog = false;
        }

        public void writeLog(string appname, string logmessage)
        {
            appName = appname;
            writeLog(logmessage);
        }

        public void writeLog(int threadid, string logmessage)
        {


            string LogFolder = "";
            string LogFileName = "";

            if (appName == "") { appName = "APPDEFAULT"; }
            LogFolder = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
            if (!Directory.Exists(LogFolder)) { Directory.CreateDirectory(LogFolder); }

            LogFileName = LogFolder + appName + string.Format("{0:_yyyyMMdd}", DateTime.Now) + "." + threadid.ToString() + ".log";

            isWritingLog = true;
            FileStream fs = new FileStream(LogFileName, FileMode.Append, FileAccess.Write,FileShare.ReadWrite );

            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);

            string logmsg = string.Format("{0:yyyy/MM/dd;HH:mm:ss;}", DateTime.Now) + logmessage;
            m_streamWriter.WriteLine(logmsg);
            m_streamWriter.Flush();
            m_streamWriter.Close();

            m_streamWriter.Dispose();
            fs.Dispose();
            isWritingLog = false;



        }
        public void writeLog(string appname, int threadid, string logmessage)
        {
            appName = appname;
            writeLog(threadid, logmessage);
        }

        public string logFilename()
        {
            if (appName == "") { appName = "APPDEFAULT"; }
            string LogFolder = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
            string LogFileName = LogFolder + appName + string.Format("{0:_yyyyMMdd}", DateTime.Now) + ".log";
            return LogFileName;
        }
        public string logFilename(string appname)
        {
            string LogFolder = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
            string LogFileName = LogFolder + appname + string.Format("{0:_yyyyMMdd}", DateTime.Now) + ".log";
            return LogFileName;
        }
        public string logFilename(int threadid)
        {
            if (appName == "") { appName = "APPDEFAULT"; }
            string LogFolder = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
            string LogFileName = LogFolder + appName + string.Format("{0:_yyyyMMdd}", DateTime.Now) + "." + threadid.ToString() + ".log";
            return LogFileName;
        }
    }
}
