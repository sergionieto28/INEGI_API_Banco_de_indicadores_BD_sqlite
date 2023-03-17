using System;
using carga.Model;
using Newtonsoft.Json;

using Microsoft.Data.Sqlite;
using System.Net;
using System.Text;

namespace carga
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();
            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
            wc.Headers[HttpRequestHeader.UserAgent] = "Desarrolladores_Inegi";
            wc.Encoding = Encoding.UTF8;
            //string[] parametros = parameters.Split('*');
            string token = "970a2694-e217-5311-12b9-b9c1b9403d4c";
            string ruta = "http://www.inegi.org.mx/app/api/indicadores/desarrolladores/jsonxml/INDICATOR/1002000002,1002000003,1002000001/es/0700/false/BISE/2.0/"+ token + "?type=json";
 
            var response = wc.UploadString(ruta, "GET");

            SqliteConnection cadenaconexion = new SqliteConnection("Data source= ../../../../../mydatabase.db");
            cadenaconexion.Open();

            string queryBU="";
            SqliteCommand comandoBU;
            SqliteDataReader resultBU;
            Info dato = JsonConvert.DeserializeObject<Info>(response);
              foreach (var Ser in dato.Series)
              {
                //Buscar unidades iguales
               queryBU = "SELECT * FROM Unit_catalog WHERE Unit=" + Ser.Unit + ";";
                comandoBU = new SqliteCommand(queryBU, cadenaconexion);
                resultBU = comandoBU.ExecuteReader();
                if (!resultBU.HasRows)
                {
                    //insertar unidad
                    string queryU = "insert into Unit_catalog(Unit) values ('" + Ser.Unit + "')";
                    SqliteCommand comandoU = new SqliteCommand(queryU, cadenaconexion);
                    comandoU.ExecuteNonQuery();
                }
                //Buscar notas iguales
                queryBU = "SELECT * FROM Note_catalog WHERE Note=" + Ser.Note + ";";
                comandoBU = new SqliteCommand(queryBU, cadenaconexion);
                resultBU = comandoBU.ExecuteReader();
                if (!resultBU.HasRows)
                {
                    //insertar nota
                    string queryN = "insert into Note_catalog(Note) values ('" + Ser.Note + "')";
                    SqliteCommand comandoN = new SqliteCommand(queryN, cadenaconexion);
                    comandoN.ExecuteNonQuery();
                }
                string queryS = "insert into Series(Indicador,Freq,Topic,Unit,Unit_mult,Note,Source,Lastupdate,Status) values ('" + Ser.Indicador + "','" + Ser.Freq + "','" + Ser.Topic + "','" + Ser.Unit + "','" + Ser.Unit_mult + "','" + Ser.Note + "','" + Ser.Lastupdate + "','" + Ser.Source + "','" + Ser.Status + "')";
                SqliteCommand comandoS = new SqliteCommand(queryS, cadenaconexion);
                comandoS.ExecuteNonQuery();

                //recorrer observations
                foreach(var obs in Ser.Observations)
                {
                    string queryO = "insert into Observations(Indicador,Time_period,Obs_value,Obs_exception,Obs_status,Obs_source,Obs_note,Cober_geo) values ('" + Ser.Indicador + "','" + obs.Time_period + "','" + obs.Obs_value + "','" + obs.Obs_exception + "','" + obs.Obs_status + "','" + obs.Obs_source + "','" + obs.Obs_note + "','" + obs.Cober_geo +"')";
                    SqliteCommand comandoO = new SqliteCommand(queryO, cadenaconexion);
                    comandoO.ExecuteNonQuery();
                }
             }
            cadenaconexion.Close();

        }
    }
}
