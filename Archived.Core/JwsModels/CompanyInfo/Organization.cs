using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Archived.Core.JwsModels.CompanyInfo
{
    public class Organization
    {
        private List<BaseTier> _tiers;

        public List<BaseTier> Tiers
        {
            get { return _tiers ?? (_tiers = new List<BaseTier>()); }
            set { _tiers = value; }
        }

        public string CompanyId { get; set; }

        public Organization(string companyId)
        {
            CompanyId = companyId;
            ReadTiers();
        }

        private void ReadTiers()
        {
            const string sql = @"
                select
                       t1.company_id as t1_id,
                          t1.company_name as t1_name,
                          t1.cpcpnum as t1_cpcpnum,
                          t1.wc_lob_yn as t1_wc_lob_yn,
                       t2.company_id as t2_id,
                          t2.company_name as t2_name,
                          t2.cpcpnum as t2_cpcpnum,
                          t2.wc_lob_yn as t2_wc_lob_yn,
                       t3.company_id as t3_id,
                          t3.company_name as t3_name,
                          t3.cpcpnum as t3_cpcpnum,
                          t3.wc_lob_yn as t3_wc_lob_yn,
                       t4.company_id as t4_id,
                          t4.company_name as t4_name,
                          t4.cpcpnum as t4_cpcpnum,
                          t4.wc_lob_yn as t4_wc_lob_yn,
                       t5.company_id as t5_id,
                          t5.company_name as t5_name,
                          t5.cpcpnum as t5_cpcpnum,
                          t5.wc_lob_yn as t5_wc_lob_yn, 
                       t6.company_id as t6_id,
                          t6.company_name as t6_name,
                          t6.cpcpnum as t6_cpcpnum,
                          t6.wc_lob_yn as t6_wc_lob_yn  
                from
                       fh_company co left outer join fh_company t1 on co.tier1_company_id = t1.company_id
                                        left outer join fh_company t2 on co.tier2_company_id = t2.company_id
                                                   left outer join fh_company t3 on co.tier3_company_id = t3.company_id
                                                   left outer join fh_company t4 on co.tier4_company_id = t4.company_id
                                                   left outer join fh_company t5 on co.tier5_company_id = t5.company_id
                                                   left outer join fh_company t6 on co.tier6_company_id = t6.company_id
                where 1=1
                and t2.wc_lob_yn  = 'W'
                and t1.company_id = @companyId";


            using (var cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["database:jws"].ConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@companyId", CompanyId);
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var tier1 = new Tier1(dr);
                        var tier2 = new Tier2(tier1,dr);
                        var tier3 = new Tier3(tier2 ,dr);
                        var tier4 = new Tier4(tier3 ,dr);
                        var tier5 = new Tier5(tier4 ,dr);
                        var tier6 = new Tier6(tier5 ,dr);
                        //var tier7 = new Tier7(dr);

                        if(!string.IsNullOrEmpty(tier1.Id) )
                        {
                            Tiers.Add(tier1);
                        }
                        if (!string.IsNullOrEmpty(tier2.Id))
                        {
                            Tiers.Add(tier2);
                        }
                        if (!string.IsNullOrEmpty(tier3.Id))
                        {
                            Tiers.Add(tier3);
                        }
                        if (!string.IsNullOrEmpty(tier4.Id))
                        {
                            Tiers.Add(tier4);
                        }
                        if (!string.IsNullOrEmpty(tier5.Id))
                        {
                            Tiers.Add(tier5);
                        }
                        if (!string.IsNullOrEmpty(tier6.Id))
                        {
                            Tiers.Add(tier6);
                        }

                        
                        //Tiers.Add(tier7);

                    }

                }
            }
            if (Tiers.Any())
            {
                Tiers = Tiers.GroupBy(c => new {c.Id,c.Name,c.Prefix, c.TierLevel}, g => g, (g,res)=> res.FirstOrDefault()).ToList();
              
            }

        }

    }
}
