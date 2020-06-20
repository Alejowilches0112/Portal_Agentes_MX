using DAO;
using Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace Models
{
    public class ManageReadCSV
    {
        public string readCSV()
        {
            var reader = new StreamReader(File.OpenRead("C:/Users/Usuario/Downloads/archive/CtestFINANCIERA FORTALEZA05_09_2019Pendientes.csv"));
            var datos = new List<String[]>();
            try
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    string[] rows = line.Split(',');
                    datos.Add(rows);
                    System.Diagnostics.Debug.WriteLine(line);
                }
                foreach (var i in datos)
                {
                    System.Diagnostics.Debug.WriteLine(i[0]);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageParams", "updDocumentos" + "", ex, "");
            }
            return datos.ToString();
        }
    }
}
