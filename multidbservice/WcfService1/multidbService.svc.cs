﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.SqlClient;
using System.Collections;

namespace nsMultiDBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "multiDBService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select multiDBService.svc or multiDBService.svc.cs at the Solution Explorer and start debugging.
    public class multiDBService : IMultiDBService
    {
        //[Serializable]
        [WebInvoke(Method = "POST", 
                    ResponseFormat = WebMessageFormat.Json, 
                    RequestFormat = WebMessageFormat.Json,
                    UriTemplate = "addDatabase")]
        public string JSONparametrosAddDatabase(parametrosAddDatabase addDatabase)
        {
            try {
                MariaConnect db = new MariaConnect("localhost", "TEST", "prueba", "prueba");
                db.NonQuery("CALL registrar_conexion('" + addDatabase.database_type + "','"
                                                   + addDatabase.user + "','"
                                                   + addDatabase.pass + "','"
                                                   + addDatabase.server + "','"
                                                   + addDatabase.protocol + "','"
                                                   + addDatabase.port + "','"
                                                   + addDatabase.alias + "');");

                /*ArrayList filas = new ArrayList();

                string prueba = "";

                filas = db.Select2("db_connection");

                for (int i = 0; i < filas.Count; i++) prueba += filas[i];


                return prueba;*/
                return "Conexion exitosa";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json,
                   UriTemplate = "getDBConnections")]
        public string getDBConnections()
        {
            MariaConnect db = new MariaConnect("localhost", "TEST", "prueba", "prueba");
            ArrayList result = new ArrayList();
            result = db.Select("db_connection");
            string resultados = result.ToJson();
            return resultados;
        }

        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json,
                   UriTemplate = "getDatabases")]
        public string getDatabases()
        {
            MariaConnect db = new MariaConnect("localhost", "TEST", "prueba", "prueba");
            //ArrayList result = new ArrayList();
            string result;
            result = db.Select2("data_bases");
            string resultados = result.ToJson();
            return resultados;
        }

        public string connection_mongo()
        {
            IMongoClient _client;
            IMongoDatabase _database;
            
            _client = new MongoClient();
            _database = _client.GetDatabase("test");

            string state = _client.Cluster.Description.State.ToString();

            return state;
        }
    }
    
    public class parametrosAddDatabase
    {
        public string database_type { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string server { get; set; }
        public string protocol { get; set; }
        public string port { get; set; }
        public string alias { get; set; }
        public string estadoConexion_maria { get; set; }
        public string estadoConexion_mongo { get; set; }
        public string estadoConexion_sqlserver { get; set; }
    }

}
