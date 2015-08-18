using AutoMapper;
using Investors2015.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Inestors2015.Models
{
    public class FinancialDocCM : IEquatable<FinancialDocCM>
    {
        public int? PartnerId
        {
            get;
            set;
        }

        public int FinancialDocID
        {
            get;
            set;
        }

        public string Datubaze
        {
            get;
            set;
        }

        public ICollection<FinancialDocLineCM> FinancialDocLineCMs
        {
            get;
            set;
        }

        bool IEquatable<FinancialDocCM>.Equals(FinancialDocCM other)
        {
            bool result;
            if (object.ReferenceEquals(other, null))
            {
                result = false;
            }
            else if (object.ReferenceEquals(this, other))
            {
                result = true;
            }
            else
            {
                bool areAllValuesEqual = this.PartnerId.Equals(other.PartnerId) && this.PartnerId.Equals(other.PartnerId);
                result = areAllValuesEqual;
            }
            return result;
        }

        public override int GetHashCode()
        {
            int hashDebetID = (!this.PartnerId.HasValue) ? 0 : this.PartnerId.GetHashCode();
            int hashCreditID = (!this.PartnerId.HasValue) ? 0 : this.PartnerId.GetHashCode();
            return hashDebetID ^ hashCreditID;
        }

        public static List<FinancialDocCM> GetFinancialDocsForPartner(string datubaze, int partnerId)
        {
            List<FinancialDocCM> financialDocs = new List<FinancialDocCM>();
            using (TildesJumisFinancialDBContext dbContext = new TildesJumisFinancialDBContext())
            {
                dbContext.ChangeDatabase(datubaze, "", "admin", "is ri itldes ajuna aprole", false, "");
                if (partnerId != 0)
                {
                    List<FinancialDoc> originalFormatFinancialDocs = (from x in dbContext.FinancialDocs
                                                                      where x.PartnerID == (int?)partnerId
                                                                      select x into y
                                                                      select y).ToList<FinancialDoc>();
                    if (originalFormatFinancialDocs.Any<FinancialDoc>())
                    {
                        Mapper.CreateMap<FinancialDoc, FinancialDocCM>();
                        financialDocs = Mapper.Map<List<FinancialDoc>, List<FinancialDocCM>>(originalFormatFinancialDocs);
                        financialDocs.ForEach(delegate(FinancialDocCM x)
                        {
                            x.Datubaze = datubaze;
                        });
                    }
                }
            }
            return financialDocs;
        }

        public static List<FinancialDocLineCM> GetTestTemplates(string Database, int PartnerId)
        {
            List<FinancialDocCM> financialDocsCMdistinct = new List<FinancialDocCM>();
            List<FinancialDocCM> financialDocsCM = new List<FinancialDocCM>();
            Mapper.CreateMap<FinancialDocLine, FinancialDocLineCM>().ForMember((FinancialDocLineCM y) => (object)y.FinancialDocID, delegate(IMemberConfigurationExpression<FinancialDocLine> opt)
            {
                opt.Ignore();
            }).ForMember((FinancialDocLineCM u) => (object)u.FinancialDocLineID, delegate(IMemberConfigurationExpression<FinancialDocLine> opt)
            {
                opt.Ignore();
            });
            List<FinancialDocLineCM> financialDocLinesCM = new List<FinancialDocLineCM>();
            using (TildesJumisFinancialDBContext dbContext = new TildesJumisFinancialDBContext())
            {
                dbContext.ChangeDatabase(Database, "", "admin", "is ri itldes ajuna aprole", false, "");
                if (PartnerId != 0)
                {
                    List<FinancialDoc> financialDocs = (from x in dbContext.FinancialDocs
                                                        where x.PartnerID == (int?)PartnerId
                                                        select x).Include((FinancialDoc i) => i.FinancialDocLines1).ToList<FinancialDoc>();
                    if (financialDocs.Any<FinancialDoc>())
                    {
                        Mapper.CreateMap<FinancialDoc, FinancialDocCM>().ForMember((FinancialDocCM d) => d.FinancialDocLineCMs, delegate(IMemberConfigurationExpression<FinancialDoc> src)
                        {
                            src.MapFrom<ICollection<FinancialDocLine>>((FinancialDoc t) => t.FinancialDocLines1);
                        }).ForMember((FinancialDocCM r) => (object)r.FinancialDocID, delegate(IMemberConfigurationExpression<FinancialDoc> src)
                        {
                            src.Ignore();
                        });
                        financialDocsCMdistinct = Mapper.Map<List<FinancialDoc>, List<FinancialDocCM>>(financialDocs).Distinct<FinancialDocCM>().ToList<FinancialDocCM>();
                        financialDocsCM = Mapper.Map<List<FinancialDoc>, List<FinancialDocCM>>(financialDocs).ToList<FinancialDocCM>();
                    }
                }
            }
            return financialDocLinesCM;
        }
    }
}
