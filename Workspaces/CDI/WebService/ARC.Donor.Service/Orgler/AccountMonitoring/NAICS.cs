using System;
using AutoMapper;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Orgler.AccountMonitoring
{
    public class NAICS
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        /* Method name: naicsStatusChange
         * Input Parameters: An object of NAICSStatusChangeInput class
         * Output Parameters: A list of TransactionResult class  
         * Purpose: This method updates the status of naics codes(approve,reject or add) for the input master */
        public IList<ARC.Donor.Business.Orgler.AccountMonitoring.TransactionResult> naicsStatusChange(ARC.Donor.Business.Orgler.AccountMonitoring.NAICSStatusChangeInput NAICSStatusChangeInput)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.NAICSStatusChangeInput, Data.Entities.Orgler.AccountMonitoring.NAICSStatusChangeInput>();
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.TransactionResult, Business.Orgler.AccountMonitoring.TransactionResult>();

            //map the input from buisness object to the data layer object
            var Input = Mapper.Map<Business.Orgler.AccountMonitoring.NAICSStatusChangeInput, Data.Entities.Orgler.AccountMonitoring.NAICSStatusChangeInput>(NAICSStatusChangeInput);

            //Instantiate the data layer object for confirm functionality
            Data.Orgler.AccountMonitoring.NAICS naics = new Data.Orgler.AccountMonitoring.NAICS();

            //call the data layer method to update the status for naics codes in the database
            var AcctLst = naics.naicsStatusChange(Input);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.AccountMonitoring.TransactionResult>, IList<Business.Orgler.AccountMonitoring.TransactionResult>>(AcctLst);

            //return the results back to the controller
            return result;
        }

        /* Method name: getOrgNAICSDetails
         * Input Parameters: An object of GetMasterNAICSDetailsInput class
         * Output Parameters: A list of GetMasterNAICSDetailsOutput class  
         * Purpose: This method gets the naics details for a single master */
        public IList<ARC.Donor.Business.Orgler.AccountMonitoring.GetMasterNAICSDetailsOutput> getOrgNAICSDetails(int NoOfRecs, int PageNum, ARC.Donor.Business.Orgler.AccountMonitoring.GetMasterNAICSDetailsInput NAICSInput)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.GetMasterNAICSDetailsInput, Data.Entities.Orgler.AccountMonitoring.GetMasterNAICSDetailsInput>();
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.GetMasterNAICSDetailsOutput, Business.Orgler.AccountMonitoring.GetMasterNAICSDetailsOutput>();

            //map the input from buisness object to the data layer object
            var Input = Mapper.Map<Business.Orgler.AccountMonitoring.GetMasterNAICSDetailsInput, Data.Entities.Orgler.AccountMonitoring.GetMasterNAICSDetailsInput>(NAICSInput);

            //Instantiate the data layer object for confirm functionality
            Data.Orgler.AccountMonitoring.NAICS naics = new Data.Orgler.AccountMonitoring.NAICS();

            //call the data layer method to get NAICS details from the database
            var NAICSDetails = naics.getOrgNAICSDetails(NoOfRecs, PageNum, Input);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.AccountMonitoring.GetMasterNAICSDetailsOutput>, IList<Business.Orgler.AccountMonitoring.GetMasterNAICSDetailsOutput>>(NAICSDetails);

            //return the results back to the controller
            return result;
        }

        /* Method name: getALLNAICSCodes
         * Input Parameters: NA
         * Output Parameters: A list of NAICSCode class  
         * Purpose: This method gets all the naics codes in tree data structure */
        public IList<ARC.Donor.Business.Orgler.AccountMonitoring.NAICSCode> getALLNAICSCodes()
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.NAICSCode, Business.Orgler.AccountMonitoring.NAICSCode>();

            //Instantiate the data layer object for confirm functionality
            Data.Orgler.AccountMonitoring.NAICS naics = new Data.Orgler.AccountMonitoring.NAICS();

            //call the data layer method to get All NAICS details from the database 
            var NAICSDBCodes = naics.getAllNAICSCodes();

            //invoke the method to form tree structure view for all the naics codes
            var NAICSCodes = formTreeDataStructureNAICSCode(NAICSDBCodes);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.AccountMonitoring.NAICSCode>, IList<Business.Orgler.AccountMonitoring.NAICSCode>>(NAICSCodes);

            //return the results back to the controller
            return result;
        }

        /* Method name: formTreeDataStructureNAICSCode
         * Input Parameters: An object of NAICSCodeMapper class
         * Output Parameters: A list of NewAccountsOutputModel class  
         * Purpose: This method uses all the naics codes and forms a tree hierachy data structure */
        public IList<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode> formTreeDataStructureNAICSCode(IList<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCodeMapper> listDBNAICSCodes)
        {
            //create a list of naicscode class
            List<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode> result = new List<ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode>();
            
            //create a dictionary of naics code and the object. This is used to store all the naics codes from the db
            Dictionary<string, ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode> alreadyRead = new Dictionary<string, ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode>();
            
            //for each record from db
            foreach (ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCodeMapper naics in listDBNAICSCodes)
            {
                if (!naics.naics_cd.ToString().Contains("-"))
                {
                    //populate the dictionary for this naics code
                    alreadyRead[naics.naics_cd] = createNAICSCode(naics);
                }
                else
                {
                    string[] rangeNaicsCodes = naics.naics_cd.Split(new char[] { '-' });
                    for(int i=int.Parse(rangeNaicsCodes[0]); i<=int.Parse(rangeNaicsCodes[1]);i++)
                    {
                        ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCodeMapper naicsCodeRange = naics;
                        naicsCodeRange.naics_cd = i.ToString();

                        //populate the dictionary for each naics code inside the range
                        alreadyRead[naics.naics_cd] = createNAICSCode(naicsCodeRange);
                    }
                }
            }

            //for each naics code in the dictionary
            foreach (ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode naicsCode in alreadyRead.Values)
            {
                //get the parent id
                ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode aParent;
                string strParentId = naicsCode.parent_naics_cd;

                //if parent is present , then append the current naics code as the parent's child
                if (alreadyRead.TryGetValue(strParentId, out aParent))
                    aParent.AddSubNAICSCode(naicsCode);
                else
                {
                    //else add it to the result 
                    if (naicsCode.naics_lvl != "3")
                        result.Add(naicsCode);
                }
            }

            //return back the result
            return result;
        }

        public  ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode createNAICSCode(ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCodeMapper naicsMapper)
        {
            //create one naicscode object
            ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode naicsCode = new ARC.Donor.Data.Entities.Orgler.AccountMonitoring.NAICSCode();

            //populate the columns 
            naicsCode.naics_cd = naicsMapper.naics_cd;
            naicsCode.naics_key = naicsMapper.naics_key;
            naicsCode.naics_lvl = naicsMapper.naics_lvl;
            naicsCode.naics_indus_title = naicsMapper.naics_indus_title;

            //populate the parent id for higher levels where parent id is the same as naics code exceot the last character
            if (naicsMapper.naics_lvl == "2")
                naicsCode.parent_naics_cd = "0";
            else
            {
                int intNAICSCodelength = naicsMapper.naics_cd.Length;
                naicsCode.parent_naics_cd = naicsMapper.naics_cd.Substring(0, intNAICSCodelength - 1);
            }

            return naicsCode;
        }

    }
}
