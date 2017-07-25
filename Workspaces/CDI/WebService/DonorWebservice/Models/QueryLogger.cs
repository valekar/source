using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DonorWebservice.ClientValidation;
using NLog;

namespace DonorWebservice.Models
{
    public class QueryLogger
    {
        private static IRepository<QueryTimeLogger> repQuery;
        private static Logger log = LogManager.GetCurrentClassLogger();
        private static readonly object padlock = new object();
        public QueryLogger()
        {
            repQuery = new ClientValidation.Repository<QueryTimeLogger>();
        }

        public static async Task InsertQuery(string UserName, string Action, string Qry, DateTime STime, DateTime ETime){
            try
            {
                var Query = new QueryTimeLogger();
                Query.UserName = UserName;
                Query.Action = Action;
                Query.Query = Qry;
                Query.StartTime = STime;
                Query.EndTime = ETime;
                await repQuery.InsertAsync(Query, true);
            }
            catch (Exception ex)
            {
                log.Info("ERROR: " + ex.Message);
            }
        }

        public static void InsertQuery(ClientValidation.QueryTimeLogger QLog)
        {
            
                if (repQuery == null)
                {
                    lock (padlock)
                    {
                        if (repQuery == null)
                        {
                            repQuery = new ClientValidation.Repository<QueryTimeLogger>();
                        }
                    }
                }
                //repQuery.Insert(QLog, true);
                Task.Run(() => {
                    try
                    {
                        repQuery.InsertAsync(QLog, true);
                    }
                    catch (Exception ex)
                    {
                        log.Info("ERROR: " + ex.Message);
                    }
                });
            
        }

        public void InsertQuery1(ClientValidation.QueryTimeLogger QLog)
        {
            Task.Run(() =>
            {
                try
                {
                    repQuery.InsertAsync(QLog, true);
                }
                catch (Exception ex)
                {
                    log.Info("ERROR: " + ex.Message);
                }
            });

        }

        public static async Task InsertQueryAsync(ClientValidation.QueryTimeLogger QLog)
        {
            try
            { 
                await repQuery.InsertAsync(QLog, true);
            }
            catch (Exception ex)
            {
                log.Info("ERROR: " + ex.Message);
            }
        }
    }
}