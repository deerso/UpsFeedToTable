using System.Data;
using Deerso.Data.OrmLite.Contracts;
using UpsFeedToTable.Models;

namespace UpsFeedToTable
{
    public class Repos
    {
        protected ILiteRepoProvider Provider { get; set; }
        public Repos(ILiteRepoProvider repoProvider)
        {
            Provider = repoProvider;
        } 

        public ILiteRepository<EDI_Data> EdiData {get { return Provider.Repo<EDI_Data>(); }} 
        public ILiteRepository<EDI_Processed_Files> ProcessedFiles {get { return Provider.Repo<EDI_Processed_Files>(); }} 
        public ILiteRepository<InvalidData> InvalidShippingCosts {get { return Provider.Repo<InvalidData>(); }} 
        public ILiteRepository<Tracking> Tracking {get { return Provider.Repo<Tracking>(); }} 
        public ILiteRepository<ShippingCosts> ShippingCosts {get { return Provider.Repo<ShippingCosts>(); }} 

    }
}