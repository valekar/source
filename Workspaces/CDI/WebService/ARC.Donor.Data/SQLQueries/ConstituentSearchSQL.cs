using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data.SQL
{
    public class ConstituentSearchSQL
    {
        public static string getConstituentSearchSQL(string Master_id)
        {
            return string.Format(Qry,
                     string.Join(",", Master_id));
        }

        static readonly string Qry = @"select top 50  query.* 
                                    from    ( 
                                    select x.*  ,coalesce(trans.trans_key,-1) as request_transaction_key  
                                    from    ( (   
                                    select data_stwrd.constituent_id as constituent_id, data_stwrd.cnst_dsp_id as cnst_dsp_id,
                                            data_stwrd.name as name, data_stwrd.first_name as first_name,
                                            data_stwrd.last_name as last_name, data_stwrd.constituent_type as constituent_type ,
                                            data_stwrd.phone_number as phone_number ,data_stwrd.email_address as email_address ,data_stwrd.ent_org_name as ent_org_name
                                         ( COALESCE(data_stwrd.addr_line_1,'') || ' '  ||COALESCE(data_stwrd.addr_line_2,'')  || ' '  || COALESCE(data_stwrd.city, '')  || ' '  || COALESCE(data_stwrd.state_cd, '')  || ' '  || COALESCE(data_stwrd.zip, '')) AS addr_line_1
                                    from    dw_stuart_vws.stwrd_dnr_prfle data_stwrd  
                                    where 1=1 
                                        and data_stwrd.appl_src_cd = 'CDIM'  
                                        and constituent_type = 'IN'  
                                        AND   data_stwrd.row_stat_cd <> 'L'  
                                        and (  data_stwrd.constituent_id in ( 
                                    sel   data_stwrd.constituent_id 
                                    from    dw_stuart_vws.stwrd_dnr_prfle data_stwrd  
                                    where data_stwrd.constituent_id = '{0}'  
                                    union
                                     all 
                                    select data_stwrd.constituent_id 
                                    from    dw_stuart_vws.stwrd_dnr_prfle data_stwrd inner join dw_stuart_vws.cnst_mstr_id_map cnst_mstr_id_map 
                                        ON  data_stwrd.constituent_id = cnst_mstr_id_map.new_cnst_mstr_id  
                                        and ( cnst_mstr_id_map.constituent_id = '{0}'  )) ))) x  left outer join ( 
                                    SELECT trans_cnst.cnst_id,trans_cnst.trans_key 
                                    FROM dw_stuart_vws.trans_cnst trans_cnst inner join  dw_stuart_vws.trans trans 
                                        ON   trans_cnst.trans_key=trans.trans_key 
                                    WHERE trans.trans_typ_id <> 4 ) trans_constituent 
                                        on   x.constituent_id = trans_constituent.cnst_id left outer join dw_stuart_vws.trans trans 
                                        on   trans_constituent.trans_key = trans.trans_key 
                                        and trans.trans_stat in ('In Progress','Waiting for approval') 
                                        and trans.sub_trans_typ_id in ( 
                                    select distinct sub_trans_typ_id 
                                    from    dw_stuart_vws.sub_trans_typ sub_trans_typ 
                                    where sub_trans_typ.sub_trans_typ_dsc in ('merge','do not merge')) 
                                    qualify    row_number() over (partition by x.constituent_id 
                                    order  by x.constituent_id  , trans.trans_key desc  ) <=1 ) query 
                                    order  by name DESC, last_name ASC, first_name ASC, constituent_id ASC  ;
                                     ";
    }
    
}