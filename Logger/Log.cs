using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public static class Log
    {
        public static void write(Exception ex, string fullPath)
        {
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.WriteLine($"\n------\nData:{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} \n Mensagem:{ex.Message} \n StackTrace:{ex.StackTrace} \n InnerException:{ex.InnerException} \n Tipo do erro: {ex.GetType()} \n Source: {ex.Source} \n TargetSite: {ex.TargetSite}");
            }
        }
    }
}
