using Claro.Data;
using Claro.SIACU.Data.Configuration;
using Claro.SIACU.Entity.Cases;
using System.Collections.Generic;
using System.Data;

namespace Claro.SIACU.Data.Cases
{
    public static class Common
    {
        public static List<WipBin> GetWipBinListByUser(string strIdSession, string strTransaction, string strUser)
        {
            DbParameter[] parameters = new DbParameter[] {
                  new DbParameter("p_usuario", DbType.String, 100, ParameterDirection.Input, strUser), 
                  new DbParameter("p_flag_consulta", DbType.String,100,  ParameterDirection.Output),
                  new DbParameter("p_msg_text", DbType.String, 100, ParameterDirection.Output),
                  new DbParameter("out_cursor", DbType.Object, ParameterDirection.Output),
            };

            return DbFactory.ExecuteReader<List<WipBin>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SP_MY_WIPBIN, parameters);
        }

        public static List<Queue> GetQueuesByUser(string strIdSession, string strTransaction, string strUser)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_usuario", DbType.String, 100, ParameterDirection.Input, strUser),
                new DbParameter("p_flag_consulta", DbType.String, 100, ParameterDirection.Output),
                new DbParameter("p_msg_text", DbType.String, 100, ParameterDirection.Output),
                new DbParameter("out_cursor", DbType.Object, ParameterDirection.Output),
            };

            return DbFactory.ExecuteReader<List<Queue>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SP_MY_QUEUES, parameters);
        }

        public static List<Queue> GetQueuesAll(string strIdSession, string strTransaction, string strUser, string strFlagWorkgroup)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_usuario", DbType.String, 100, ParameterDirection.Input, strUser),
                new DbParameter("p_flag_workgroup", DbType.String, 100, ParameterDirection.Input, strFlagWorkgroup),
                new DbParameter("p_flag_consulta", DbType.String, 100, ParameterDirection.Output),
                new DbParameter("p_msg_text", DbType.String, 100, ParameterDirection.Output),
                new DbParameter("out_cursor", DbType.Object, ParameterDirection.Output),
            };

            return DbFactory.ExecuteReader<List<Queue>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SP_QUEUES, parameters);
        }
        
        public static List<Case> GetCasesByWipBin(string strIdSession, string strTransaction, string strUser, string strWipBin)
        {
            DbParameter[] parameters = new DbParameter[] {
                  new DbParameter("p_usuario", DbType.String, 100, ParameterDirection.Input, strUser),
                  new DbParameter("p_bandeja", DbType.String, 100, ParameterDirection.Input, strWipBin),
                  new DbParameter("p_flag_consulta", DbType.String, 100, ParameterDirection.Output),
                  new DbParameter("p_msg_text", DbType.String, 100, ParameterDirection.Output),
                  new DbParameter("out_cursor", DbType.Object, ParameterDirection.Output),
            };

            return DbFactory.ExecuteReader<List<Case>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SP_MY_CASES_WIPBIN, parameters);
        }

        public static List<Case> GetCasesByQueueUser(string strIdSession, string strTransaction, string strUser, string strQueue)
        {
            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("P_COLA", DbType.String,24,ParameterDirection.Input, strQueue),	
                new DbParameter("P_USUARIO", DbType.String,30,ParameterDirection.Input, strUser),													   
                new DbParameter("P_FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output),
                new DbParameter("OUT_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Case>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SP_CASES_QUEUE_USER, parameters);
        }

        public static CaseNotes GetCaseNotes(string strIdSession, string strTransaction, string strCaseId)
        {
            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("P_IDCASO", DbType.String,255,ParameterDirection.Input, strCaseId),	
                new DbParameter("P_FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output),													  
                new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output),
                new DbParameter("OUT_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<CaseNotes>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SP_NOTES_CASE, parameters);
        }

        public static List<Case> GetCasesById(string strIdSession, string strTransaction, string strCaseId, string strReportId, string strComplaintId)
        {
            DbParameter[] parameters = new DbParameter[]{
                                new DbParameter("VCASE_ID", DbType.String,10,ParameterDirection.Input, strCaseId),	
                                new DbParameter("VREPORTE", DbType.String,10,ParameterDirection.Input, strReportId),	
                                new DbParameter("VRECLAMO", DbType.String,10,ParameterDirection.Input, strComplaintId),	
                                new DbParameter("P_FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output),													  
                                new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output),
                                new DbParameter("OUT_CURSOR", DbType.Object, ParameterDirection.Output)
            };
            List<Case> listCases = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SP_QUERY_CASE_BY_ID, parameters, (IDataReader reader) =>
            {
                listCases = new List<Case>();
                while(reader.Read())
                {
                    listCases.Add(new Case()
                    {
                        IdCase = Convert.ToString(reader["ID_Caso"]),
                        CreationDate = Convert.ToDate(reader["Fecha_Creacion"]),
                        Telephone = Convert.ToString(reader["Telefono"]),
                        Typification = Convert.ToString(reader["Tipificacion"]),
                        Origin  = Convert.ToString(reader["Origen"]), 
                        State  = Convert.ToString(reader["Estado"]), 
                        Priority = Convert.ToString(reader["Prioridad"]), 
                        // Severidad
                        Phase = Convert.ToString(reader["Fase"]), 
                        Report = Convert.ToString(reader["Nro_Reporte"]), 
                        Complaint = Convert.ToString(reader["Nro_Reclamo"]), 
                        Condition = Convert.ToString(reader["Condicion"]), 
                        Queue = Convert.ToString(reader["Cola"]), 
                        Owner = Convert.ToString(reader["Propietario"]), 
                        AgentName = Convert.ToString(reader["Nombre_Agente"]), 
                        AgentLastName = Convert.ToString(reader["Apellido_Agente"]), 
                        Result = Convert.ToString(reader["Resultado"]), 
                        Resolution = Convert.ToString(reader["Resolucion"]), 
                        // objid_contacto                                    
                    });
                }
            });

            return listCases;      
        }
        
    }
}