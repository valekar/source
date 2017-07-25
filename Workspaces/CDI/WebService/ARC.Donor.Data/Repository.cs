using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Teradata.Client.Provider;
using NLog;
using System.Collections;

namespace ARC.Donor.Data
{

    public class Repository
    {
        private readonly DBContext _context;
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _msg = "";
        private int _TimeOut;
        public string GetMessage()
        {
            return _msg;
        }

        public Repository()
        {
            this._context = new DBContext();
            _TimeOut = 280;
        }

        public Repository(string Conn)
        {
            this._context = new DBContext(Conn);
            _TimeOut = 280;
        }
        public Repository(DBContext context)
        {
            this._context = context;
            _TimeOut = 280;
        }

        public int SetTimeOut
        {
            set { _TimeOut = value; }
        }
        private static Tuple<string, object[]> prepareArguments(string SP, object Params)
        {
            try
            {
                IList<string> paramNames = new List<string>();
                var ParamObjects = new List<object>();
                if (Params != null)
                {
                    foreach (PropertyInfo propInfo in Params.GetType().GetProperties())
                    {
                        string name = "@" + propInfo.Name;
                        object value = propInfo.GetValue(Params, null);

                        paramNames.Add(name);

                        ParamObjects.Add(new TdParameter(name, value ?? DBNull.Value));
                    }
                }
                if (paramNames.Count > 0)
                {
                    SP += " " + string.Join(", ", paramNames);
                }
                return new Tuple<string, object[]>(SP, ParamObjects.ToArray());
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public int ExecuteSqlCommand(string StoredProc, object parameters = null)
        {
            _msg = "";
            try
            {
                var arguments = prepareArguments(StoredProc, parameters);
                return this._context.Database.ExecuteSqlCommand(arguments.Item1, arguments.Item2);
            }
            catch (Exception ex)
            {
                _msg = "ERROR(" + StoredProc + "): " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
            }
            return -1;
        }

        public IEnumerable<T> ExecuteSqlQuery<T>(string StoredProc, object parameters = null)
        {
            _msg = "";
            try
            {
                var arguments = prepareArguments(StoredProc, parameters);
                this._context.Database.CommandTimeout = _TimeOut;
                return this._context.Database.SqlQuery<T>(arguments.Item1, arguments.Item2);
            }
            catch (Exception ex)
            {
                _msg = "ERROR(" + StoredProc + "): " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
            }
            return null;
        }

        /* Method to execute a stored procedure . This method accepts two parameters:
         * Parameter 1(strSPQuery): The call statement for the Stored procedure. The call statement should have '?' in place of 
         *                      each input paramater followed by output parameters. 
         *                      For Ex: "call dbname1.spname1(?,?,message); " should be the call statement where the 
         *                      procedure name is "spname1" present inside the schema "dbname1". This procedure call means that the procedure accepts 
         *                      two input parameters(as noted by two occurences of '?') and one output paramater(as noted by the last paramater)                      
         * Parameter 2(parameters): The list of Teradata parameters which needs to be passed.
         * */
        public IEnumerable<T> ExecuteStoredProcedure<T>(string strSPQuery, List<object> parameters)
        {
            _msg = "";
            try
            {
                this._context.Database.CommandTimeout = _TimeOut;;

                return this._context.Database.SqlQuery<T>(strSPQuery, parameters.ToArray());

            }
            catch (Exception ex)
            {
                _msg = "ERROR(" + strSPQuery + "): " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
            }
            return null;
        }

        //Added by Chiranjib - 12/04/2016 for async operations
        public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string strSPQuery, List<object> parameters)
        {
            _msg = "";
            try
            {
                this._context.Database.CommandTimeout = _TimeOut;

                return await this._context.Database.SqlQuery<T>(strSPQuery, parameters.ToArray()).ToListAsync();

            }
            catch (Exception ex)
            {
                _msg = "ERROR(" + strSPQuery + "): " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
            }
            return null;
        }

        //Keerthana - 22-Apr-2016 - Asyn call to the DB returning results as the passed in type
        public async Task<IEnumerable> ExecuteStoredProcedureAsync(Type element, string strSPQuery, List<object> parameters)
        {
            _msg = "";
            try
            {
                this._context.Database.CommandTimeout = _TimeOut;

                return await this._context.Database.SqlQuery(element, strSPQuery, parameters.ToArray()).ToListAsync();

            }
            catch (Exception ex)
            {
                _msg = "ERROR(" + strSPQuery + "): " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
            }
            return null;
        }

        public async Task ExecuteStoredProcedureAsync(string strSPQuery, List<object> parameters)
        {
            _msg = "";
            try
            {
                this._context.Database.CommandTimeout = _TimeOut;

                await this._context.Database.ExecuteSqlCommandAsync(strSPQuery, parameters.ToArray());

            }
            catch (Exception ex)
            {
                _msg = "ERROR (" + strSPQuery +"): " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
            }
            
        }


        public IEnumerable ExecuteSqlQuery(Type element, string StoredProc, object parameters = null)
        {
            _msg = "";
            try
            {
                var arguments = prepareArguments(StoredProc, parameters);
                return this._context.Database.SqlQuery(element, arguments.Item1, arguments.Item2);
            }
            catch (Exception ex)
            {
                _msg = "ERROR(" + StoredProc + "): " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
            }
            return null;
        }

        public async Task<IList<T>> ExecuteSqlQueryAsync<T>(string strSPQuery)
        {
            _msg = "";
            try
            {
                this._context.Database.CommandTimeout = _TimeOut;    
                return await this._context.Database.SqlQuery<T>(strSPQuery).ToListAsync();

            }
            catch (Exception ex)
            {
                _msg = "ERROR(" + strSPQuery + "): " + ex.Message;
                if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
                log.Info(_msg);
            }
            return null;
        }

        /*
       private static Tuple<string, object[]> prepareArgumentsName(string SP, object Params)
       {
           try
           {
               IList<string> paramNames = new List<string>();
               var ParamObjects = new List<object>();
               if (Params != null)
               {
                   foreach (PropertyInfo propInfo in Params.GetType().GetProperties())
                   {
                       string name = "@" + propInfo.Name;
                       //object value = propInfo.GetValue(Params, null);

                       paramNames.Add(name);

                       //TdParameter tdp = new TdParameter(name, value ?? DBNull.Value);
                       //if(propInfo.Name == "o_outputMessage" || propInfo.Name == "o_transaction_key") 
                       //{
                       //    tdp.Direction = ParameterDirection.Output;
                       //}
                       //else { tdp.Direction = ParameterDirection.Input; tdp.Size = 250; }

                        
                       //ParamObjects.Add(tdp);

                       //ParamObjects.Add(new TdParameter(name, value ?? DBNull.Value));
                       
                   }

                   ParamObjects = prepareParamObjectsForName(Params);


                   //paramNames.RemoveAt(23);
                   //paramNames.RemoveAt(22);
                   //paramNames.Add("o_outputMessage");
                   //paramNames.Add("o_transaction_key");
               }
               if (paramNames.Count > 0)
               {
                   SP = "CALL " + SP + "(" + string.Join(", ", paramNames) + ")";
               }

              // ParamObjects.RemoveAt(23);
              // ParamObjects.RemoveAt(22);
              // SP = "CALL dw_stuart_macs.strx_loc_dtl_prsn_nm('@i_req_typ', '@i_mstr_id', '@i_usr_nm', '@i_cnst_typ', '@i_notes', '@i_case_seq_num', '@i_bk_arc_srcsys_cd', '@i_bk_prsn_nm_typ_cd', '@i_bk_prsn_nm_best_los_ind', '@i_new_prsn_first_nm', '@i_new_prsn_middle_nm', '@i_new_prsn_last_nm', '@i_new_prsn_prefix_nm', '@i_new_prsn_suffix_nm', '@i_new_prsn_nick_nm', '@i_new_prsn_mom_maiden_nm', '@i_new_prsn_full_nm', '@i_new_alias_in_saltn_nm', '@i_new_alias_out_saltn_nm', '@i_new_arc_srcsys_cd', '@i_new_prsn_nm_typ_cd', '@i_new_prsn_nm_best_los_ind', 'o_outputMessage', 'o_transaction_key')";
                
               return new Tuple<string, object[]>(SP, ParamObjects.ToArray());
           }
           catch (Exception ex)
           {

           }
           return null;
       }
       */

        /*
       public IEnumerable<T> ExecuteSqlQuerySP<T>(string StoredProc, object parameters)
       {
           _msg = "";
           try
           {
               var arguments = prepareArgumentsName(StoredProc, parameters);
               this._context.Database.CommandTimeout = 280;
              //return this._context.Database.SqlQuery<T>(arguments.Item1, arguments.Item2);
               TdParameter tdp;
               TdParameter tdp2;
               tdp = new TdParameter("i_case_nm", 10);
               tdp.Size = 200;
               tdp.Direction = ParameterDirection.Input;
               tdp2 = new TdParameter("o_outputMessage", TdType.VarChar);
               tdp2.Direction = ParameterDirection.Output;
               tdp2.Size = 200;


               //return this._context.Database.SqlQuery<T>("CALL dw_stuart_macs.strx_del_case (?,a)", tdp);

               return this._context.Database.SqlQuery<T>("CALL dw_stuart_macs.strx_loc_dtl_prsn_nm (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,a,b)", arguments.Item2);
                
           }
           catch (Exception ex)
           {
               _msg = "ERROR: " + ex.Message;
               if (ex.InnerException != null) { _msg += "INNER EXCEPTION: " + ex.InnerException; }
               log.Info(_msg);
           }
           return null;
       } */

        /*
        public static List<object> prepareParamObjectsForName(object Params)
        {
            var ParamObjects = new List<object>();

            TdParameter tdp ;
            tdp = new TdParameter("@i_req_typ",  "insert");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_mstr_id","101");
            tdp.Direction = ParameterDirection.Input;
            tdp.TdType = TdType.BigInt;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_usr_nm", "dixit.jain");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_cnst_typ", "IN");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_notes", "Testing");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_case_seq_num", 1);
            tdp.Direction = ParameterDirection.Input;
            tdp.TdType = TdType.BigInt;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_bk_arc_srcsys_cd", string.Empty);
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_bk_prsn_nm_typ_cd", string.Empty);
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_bk_prsn_nm_best_los_ind", 1);
            tdp.TdType = TdType.ByteInt;
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_first_nm", "Dixit");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_middle_nm","A");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_last_nm", "Jain");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_prefix_nm", "Mr.");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_suffix_nm", "Sr.");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_nick_nm", "Abc");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_mom_maiden_nm", "Abc");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_full_nm", "Dixit A Jain");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_alias_in_saltn_nm", "Dixit A Jain");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_alias_out_saltn_nm", "Dixit A Jain");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_arc_srcsys_cd", "STRX");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_nm_typ_cd", "PN");
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 250;
            ParamObjects.Add(tdp);
            tdp = new TdParameter("@i_new_prsn_nm_best_los_ind", 1);
            tdp.TdType = TdType.ByteInt;
            tdp.Direction = ParameterDirection.Input;
            tdp.Size = 10;
            ParamObjects.Add(tdp);
            //tdp = new TdParameter("@o_outputMessage", TdType.VarChar);
            //tdp.Size = 100;
            //tdp.Direction = ParameterDirection.Output;
            //ParamObjects.Add(tdp);
            //tdp = new TdParameter("@o_transaction_key", TdType.VarChar);
            //tdp.Size = 100;
            //tdp.Direction = ParameterDirection.Output;
            //ParamObjects.Add(tdp);


            //ConstNameINp2.o_outputMessage = "a";
            //ConstNameINp2.o_transaction_key = 10;


            return ParamObjects;
        }
          */
    }

    public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        private readonly DBContext _context;
        // private IDbSet<T> _entities;        
        private int _TimeOut;
        public Repository(DBContext context)
            : base(context)
        {
            this._context = context;
            _TimeOut = 280;
            //this._context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public IList<T> ExecuteSQL(string SQL)
        {
            _context.Database.CommandTimeout = _TimeOut;
            return _context.Database.SqlQuery<T>(SQL).ToList();
        }

        public IList<T> ExecuteSQL(string SQL, string[] Params)
        {
            if(Params == null) return _context.Database.SqlQuery<T>(SQL).ToList();

            SqlParameter[] ParsmLst = new SqlParameter[Params.Count()];

            int i = 0;
            foreach (string param in Params)
            {
                var p = new SqlParameter("p" + i, param);
                ParsmLst[i] = p;
                i++;
            }

            if (ParsmLst.Count() == 0) return _context.Database.SqlQuery<T>(SQL).ToList();

            return _context.Database.SqlQuery<T>(SQL, ParsmLst).ToList();
        }

        public IList<T> ExecuteSQL(string SQL, object[] Params)
        {
            if (Params == null) return _context.Database.SqlQuery<T>(SQL).ToList();
            return _context.Database.SqlQuery<T>(SQL, Params).ToList();
        }

        public async Task<IList<T>> ExecuteSQLAsync(string SQL)
        {
            return await Task.Run(() => _context.Database.SqlQuery<T>(SQL).ToList());
        }

        public async Task<IList<T>> ExecuteSQLAsync(string SQL, object[] Params)
        {
            return await Task.Run(() => _context.Database.SqlQuery<T>(SQL, Params).ToList());
        }

        public  List<T> ExecToList(IQueryable<T> query)
        {
            //return query.ToList();
            return _context.Database.SqlQuery<T>(query.ToString()).ToList();
        }

        public int ExecuteCommand(string SQL)
        {
            return ExecuteCommand(SQL, null);
        }

        public int ExecuteCommand(string SQL, string[] Params)
        {
            if (Params == null) return _context.Database.ExecuteSqlCommand(SQL);

            SqlParameter[] ParsmLst = new SqlParameter[Params.Count()];

            int i = 0;
            foreach (string param in Params)
            {
                var p = new SqlParameter("p" + i, param);
                ParsmLst[i] = p;
                i++;
            }
            if (ParsmLst.Count() == 0) return _context.Database.ExecuteSqlCommand(SQL);

            return _context.Database.ExecuteSqlCommand(SQL,ParsmLst);
        }

        public async Task<int> ExecuteCommandAsync(string SQL)
        {
            return await ExecuteCommandAsync(SQL, null);
        }

        public async Task<int> ExecuteCommandAsync(string SQL, string[] Params)
        {
            if (Params == null) return await _context.Database.ExecuteSqlCommandAsync(SQL);

            SqlParameter[] ParsmLst = new SqlParameter[Params.Count()];

            int i = 0;
            foreach (string param in Params)
            {
                var p = new SqlParameter("p" + i, param);
                ParsmLst[i] = p;
                i++;
            }
            if (ParsmLst.Count() == 0) return await _context.Database.ExecuteSqlCommandAsync(SQL);

            return await _context.Database.ExecuteSqlCommandAsync(SQL, ParsmLst);
        }


    }
}