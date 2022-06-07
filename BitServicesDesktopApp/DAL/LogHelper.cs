﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitServicesDesktopApp.Views;

namespace BitServicesDesktopApp.DAL
{
    public enum LogType
    {
        Info, Debug, Error
    }

    public class LogHelper
    {
        public void Log(string message, LogType type)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
            
            using (StreamWriter writer = File.AppendText(@"logs/" + date + "-log.txt"))
            {
                writer.WriteLine($"[{DateTime.Now}] [{type}] ${message}");
                writer.Close();
            }

            switch (type)
            {
                case LogType.Info:
                    MainWindow.Logger.Info(message);
                    break;
                case LogType.Debug:
                    MainWindow.Logger.Debug(message);
                    break;
                case LogType.Error:
                    MainWindow.Logger.Error(message);
                    break;

            }
        }
    }
}