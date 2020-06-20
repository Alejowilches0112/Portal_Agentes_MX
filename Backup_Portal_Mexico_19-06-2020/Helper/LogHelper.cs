using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace Helper
{
	public class LogHelper
	{
		/// <summary>
		/// Metodo para escribir log de excepciones en disco.
		/// </summary>
		/// <param name="sNameSpace"></param>
		/// <param name="sClass"></param>
		/// <param name="sOperation"></param>
		/// <param name="sFile"></param>
		/// <param name="ex1"></param>
		/// <param name="Identificador"></param>
		public static void WriteLog(string sNameSpace, string sClass, string sOperation,  Exception ex1, string Identificador)
		{
			StreamWriter log;
			try
			{
				string Rute = ConfigurationManager.AppSettings["PathLogFile"];
				if (!System.IO.Directory.Exists(Rute))
				{
					System.IO.Directory.CreateDirectory(Rute);
				   
				}
				string Archive = ConfigurationManager.AppSettings["NameLogFile"] + DateTime.Now.ToString("yyyyMMdd") + ".txt";
				if (!File.Exists(Rute + Archive))
				{
					log = new StreamWriter(Rute + Archive);
				}
				else
				{
					log = File.AppendText(Rute + Archive);
				}
				// Write to the file:
				log.WriteLine("Identificador:" + Identificador);
				log.WriteLine("Data Time:" + DateTime.Now);
				log.WriteLine("NameSpace: " + sNameSpace);
				log.WriteLine("Class: " + sClass);
				log.WriteLine("Operation: " + sOperation);
				log.WriteLine("Error Message: " + ex1.Message);
				log.WriteLine("Error Source: " + ex1.Source);
				log.WriteLine("Error StackTrace: " + ex1.StackTrace);
				log.WriteLine("Error InnerException: " + ex1.InnerException);
				log.WriteLine("Error TargetSite: " + ex1.TargetSite);
				log.WriteLine("****************************************************************************************************************************************************************");
				log.WriteLine("\n");
				// Close the stream:
				log.Close();
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
                //log.Close();
            }

		}

		/// <summary>
		/// Metodo para escribir log de transacciones en disco.
		/// </summary>
		/// <param name="sNameSpace"></param>
		/// <param name="sClass"></param>
		/// <param name="sOperation"></param>
		/// <param name="sFile"></param>
		/// <param name="MensajeError"></param>
		/// <param name="Identificador"></param>
		/// <param name="EsRequest"></param>
		public static void WriteLog(string sNameSpace, string sClass, string sOperation, string sFile, string MensajeError, string Identificador, bool? EsRequest = false)
		{
			StreamWriter log;
			try
			{
				string Ruta = ConfigurationManager.AppSettings["PathLogFile"];
				string Archivo = ConfigurationManager.AppSettings["NameLogFile"] + DateTime.Now.ToString("yyyyMMdd") + ".txt";
				string tmp = "Error Message:";
				if (EsRequest == true)
				{
					Archivo = ConfigurationManager.AppSettings["NameLogFileRequest"] + DateTime.Now.ToString("yyyyMMdd") + ".txt";
					tmp = "XML:";
				}
				if (!File.Exists(Ruta + Archivo))
				{
					log = new StreamWriter(Ruta + Archivo);
				}
				else
				{
					log = File.AppendText(Ruta + Archivo);
				}
				// Write to the file:
				log.WriteLine("Data Time:" + DateTime.Now);
				log.WriteLine("Identificador:" + Identificador);
				log.WriteLine("NameSpace:" + sNameSpace);
				log.WriteLine("Class:" + sClass);
				log.WriteLine("Operation:" + sOperation);
				log.WriteLine("File:" + sFile);
				log.WriteLine(tmp + MensajeError);
				log.WriteLine("****************************************************************************************************************************************************************");
				log.WriteLine("\n");
				// Close the stream:
				log.Close();
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
			}
		}

		/// <summary>
		/// Metodo para registrar errores en Base de Datos
		/// </summary>
		/// <param name="clase"></param>
		/// <param name="operacion"></param>
		/// <param name="numeroDocumento"></param>
		/// <param name="MsgError"></param>
		/// <param name="detalleExcepcion"></param>
		public static void InsertLogBD(string clase, string operacion, string numeroDocumento, string MsgError, string detalleExcepcion)
		{
			
		}

	}
}
