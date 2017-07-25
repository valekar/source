using Newtonsoft.Json;
using Orgler.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Configuration;
using System.Web.Configuration;

namespace Orgler.Models
{
    public class Utility
    {
        public static string ObjectToString<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static T StringToObject<T>(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return (T)new BinaryFormatter().Deserialize(ms);
            }
        }

        public static string writeJSONToFile<T>(T Obj, string path)
        {
            string Result = "";
            StreamWriter aFile = null;
            try
            {
                //aFile = new StreamWriter(new FileStream(path, FileMode.Append));
                aFile = new StreamWriter(path);
                aFile.WriteLine(JsonConvert.SerializeObject(Obj, Formatting.Indented));
                Result = "SUCCESS";
            }
            catch (Exception ex)
            {
                Result = "FAIL: " + ex.Message;
            }
            finally
            {
                if (aFile != null) aFile.Dispose();
            }

            //using (StreamWriter aFile = new StreamWriter(path))
            //{
            //    aFile.WriteLine(JsonConvert.SerializeObject(myObj));
            //}
            return Result;
        }

        public static T readJSONTFromFile<T>(string path)
        {
            T result = default(T); ;
            StreamReader aFile = null;
            try
            {
                aFile = new StreamReader(path);
                result = JsonConvert.DeserializeObject<T>(aFile.ReadToEnd());
                aFile.Close();
            }
            catch
            {

            }
            finally
            {
                if (aFile != null) aFile.Dispose();
            }

            //using (StreamReader file = new StreamReader(path))
            //{
            //    result = JsonConvert.DeserializeObject<T>(file.ReadToEnd());
            //    file.Close();

            //}
            return result;
        }

        //Checks if an object has all its properties as null or not
        public static bool IsAnyPropertyNotNull(object o)
        {
            return o.GetType().GetProperties().Any(c => c.GetValue(o) != null);
        }


        /*written by srini for tab level securty hide feature*/
        public static List<TabLevelSecurityParams> getTabLevelSecurityList()
        {
            //Tab level security configuration entries read from JSON file
            string tabLevelSecureFilePath = ConfigurationManager.AppSettings["TabLevelSecurityConfigPath"];
            List<TabLevelSecurityParams> listTabLevelSecurity = new List<TabLevelSecurityParams>();
            string JsonDeserializeTabLevelUsers = File.ReadAllText(tabLevelSecureFilePath);
            listTabLevelSecurity = JsonConvert.DeserializeObject(JsonDeserializeTabLevelUsers, (typeof(List<TabLevelSecurityParams>))) as List<TabLevelSecurityParams>;
            return listTabLevelSecurity;

        }


        /*written by srini for tab level securty hide feature*/
        public static TabLevelSecurityParams getUserPermissions(string userName)
        {
            List<TabLevelSecurityParams> tabLevelSecurityList = Utility.getTabLevelSecurityList();
            TabLevelSecurityParams tabLevelSecurityCurrentUser = new TabLevelSecurityParams();
            if (tabLevelSecurityList.Exists(x => x.usr_nm.ToString().ToLower() == userName.ToLower()))
            {
                tabLevelSecurityCurrentUser = tabLevelSecurityList.AsEnumerable().Where(x => (x.usr_nm.ToString().ToLower() == userName.ToLower())).Select(x => x).FirstOrDefault();
            }
            return tabLevelSecurityCurrentUser;               
        }


    }


    public class Constant
    {
        public static Constant instance = null;
        public static Dictionary<string, string> CONSTANTS = new Dictionary<string, string>(){
            {"CONST_NAME","ConstName"},
            {"CONST_ORG_NAME","ConstOrgName"},
            {"BEST_SMRY", "BestSmry"},
            {"CONST_ADDRESS", "ConstAddress"},
            {"CONST_EMAIL", "ConstEmail"},
            {"CONST_PHONE", "ConstPhone"},
            {"CONST_EXT_BRIDGE", "ConstExtBridge"},
            {"CONST_BIRTH", "ConstBirth"},
            {"CONST_DEATH", "ConstDeath"},
            {"CONST_PREF", "ConstPref"},
            {"CHARACTERISTICS", "Character"},
            {"GRP_MEMBERSHIP", "GrpMembership"},
            {"TRANS_HISTORY", "TransHistory"},
            {"ANON_INFO", "AnonInfo"},
            {"MASTER_ATTEMPT", "MasterAttempt"},
            {"INTERNAL_BRIDGE", "InternalBridge"},
            {"MERGE_HISTORY", "MergeHistory"},
            {"AFFILIATOR", "Affiliator"} ,
            {"MASTER_DETAIL", "MasterDetail"} 
        };


    


        private Constant()
        {            
        }

        public static Constant getInstance()
        {
            if (instance == null)
                return new Constant();
            else
                return instance;
        }

    }

}