using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace Orgler.Services
{
    public static class Utilities
    {
        public static string getProcessedCSV(string csvIds)
        {
            List<string> listIds = new List<string>();
            csvIds = RemoveWhitespace(csvIds);
            listIds = csvIds.Split(',').ToList();
            listIds.ForEach(x => x = x.Replace("'", "''"));
            return string.Join("','", listIds.ToArray());
        }

        //Checks if an object has all its properties as null or not
        public static bool IsAnyPropertyNotNull(object o)
        {
            return o.GetType().GetProperties().Any(c => c.GetValue(o) != null);
        }

        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public static System.Xml.XmlElement getField(System.Xml.XmlDocument doc, string fieldName, string fieldValue)
        {
            System.Xml.XmlElement field = doc.CreateElement(fieldName);
            field.InnerText = fieldValue;
            return field;
        }

        public static string getFieldValue(System.Xml.XmlElement[] any, string fieldName)
        {
            if (any.Where(x => x.Name.Contains(fieldName)).Count() == 1)
            {
                return any.Single(x => x.Name.Contains(fieldName)).InnerText;
            }
            return string.Empty;
        }

        public static List<List<string>> splitList(List<string> listString, int nSize = 30)
        {
            List<List<string>> list = new List<List<string>>();

            for (int i = 0; i < listString.Count; i += nSize)
            {
                list.Add(listString.GetRange(i, Math.Min(nSize, listString.Count - i)));
            }

            return list;
        }
        
        public static string writeJSONIntoFile<T>(T objs, string strLocation)
        {
            string strResult = string.Empty;
            StreamWriter file = new StreamWriter(strLocation);
            try
            {
                file.WriteLine(JsonConvert.SerializeObject(objs, Formatting.Indented));
                strResult = "Success";
            }
            catch (Exception e)
            {
                strResult = "Failed";
            }
            finally
            {
                if (file != null)
                {
                    file.Dispose();
                }
            }
            return strResult;
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
            return result;
        }
    }
}