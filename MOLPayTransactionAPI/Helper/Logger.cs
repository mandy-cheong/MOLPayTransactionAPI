﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Logger
/// </summary>
public class Logger
{
    public Logger()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void AddLog(string strSql, string exStr)
    {
        try
        {
            StreamWriter sw;
            DateTime Date = DateTime.Now;
            var path = System.IO.Path.GetFullPath("log");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            var filePath = path + "/" + fileName;
            FileInfo fi = new FileInfo(filePath);

            if (fi.Exists)
            {
                sw = File.AppendText(filePath);
            }
            else
            {
                File.Create(filePath).Close();
                sw = File.AppendText(filePath);
            }
            sw.WriteLine("*----------------------------------------------------------");
            sw.WriteLine("err_Time:" + Date.ToString("yyyy-MM-dd HH:mm:ss") + "");
            sw.WriteLine("err_SqlStr:" + strSql + "");
            sw.WriteLine("err_ExStr:" + exStr);
            sw.WriteLine("----------------------------------------------------------*");
            sw.Flush();
            sw.Close();
        }
        finally
        {
        }
    }
}