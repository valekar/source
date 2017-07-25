using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Teradata.Client.Provider;

namespace ARC.Donor.Data
{
    //helper class for Stored procedure
    public static class Extensions
    {
        public static object CheckDBNull<T> (this T Value, T optionalValue = default(T)){


            return (Value == null) ? (object)DBNull.Value : Value;
        }
    }
    public class SPHelper
    {
        /* Method to create the SP query. It accepts three parameters
         * Parameter 1 (strSPName) : String to accept the name of the Stored procedure that needs to be invoked
         * Parameter 2 (intNoOfInputParameters) : The count of the number of input only parameters (i.e., the number of parameters that are of type IN)
         * Parameter 3 (listOutputParameters) : List of strings i.e. output variables that are part of the procedure call
         * */
        public static string createSPQuery(string strSPName, int intNoOfInputParameters, List<string> listOutputParameters)
        {
            //build the call statement with the name of the procedure
            string strSPQuery = string.Empty;
            strSPQuery = "Call " + strSPName + " ( ";
            //add 'n' no of question marks where n=number of input parameters. In teradata '?' will be replaced by the parameter value.
            if (intNoOfInputParameters != 0)
            {
                for (int i = 0; i < intNoOfInputParameters; i++)
                {
                    if (i == 0) strSPQuery += "?";
                    else strSPQuery += ",?";
                }
            }
            //add the output variables in the procedure call if there are any output variables
            if (listOutputParameters.Count != 0)
            {
                foreach (string s in listOutputParameters)
                    strSPQuery += " , " + s;
            }
            //close the call statement 
            strSPQuery += " );";

            //return the procedure call statement to the calling function
            return strSPQuery;
        }

        /* Method to create a Teradata Parameter. It accepts five parameters
         * Parameter 1 (strParameterName) :The name of the parameter
         * Parameter 2 (ParameterValue) : The value asssociated with the parameter
         * Parameter 3 (strParameterDirection) : The direction of the parameter. "IN" - for input parameter and "OUT" for output parameter.
         * Parameter 4 (tdParameterType) : The Teradata type associated with the parameter. Example values: TdType.BigInt, TdType.VarChar, TdType.ByteInt etc..
         * Parameter 5 (intSize) : The size of the parameter
         * */
        public static TdParameter createTdParameter(string strParameterName, object ParameterValue, string strParameterDirection, TdType tdParameterType, int intSize)
        {
            //create a Teradata Parameter with the given name and value
            TdParameter tdp;
            string strParameterInputName = "@" + strParameterName;
            if (ParameterValue == null)
                tdp = new TdParameter(strParameterInputName, DBNull.Value);
            else
                tdp = new TdParameter(strParameterInputName, ParameterValue);

            //associate the type of parameter
            tdp.TdType = tdParameterType;

            //associate the direction of the parameter
            if (strParameterDirection == "IN")
                tdp.Direction = ParameterDirection.Input;
            else
                tdp.Direction = ParameterDirection.Output;

            //provide the size of the parameter
            if (intSize != 0)
                tdp.Size = intSize;

            //return the teradata parameter
            return tdp;
        }
    }
}