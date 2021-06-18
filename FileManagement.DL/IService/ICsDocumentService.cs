using eTMS.API.Common.Globals;
using FileManagement.DL.Models;
using FileManagementAPI.Service.Models;
using ITL.NetCore.Common;
using ITL.NetCore.Common.Items;
using ITL.NetCore.Connection.BL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement.DL.IService
{
 public interface ICsDocumentService : IRepositoryBase<CsDocument, CsDocumentModel>
    {
        List<CsDocumentModel> LoadDocument(string referenceObject, DocumentType docType);

        IQueryable<CsDocumentModel> LoadDocument(string referenceObject);
        //IQueryable<CsDocumentModel> GetDTBTransportDocument(Guid gTransportID);
    }
}