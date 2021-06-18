using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using eTMS.API.Common.AppSetting;
using eTMS.IdentityServer.DL.Infrastructure.ErrorHandler;
using FileManagement.DL.IService;
using FileManagementAPI.Common;
using ITL.NetCore.Common.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace FileManagementAPI.Controllers
{
    //[MiddlewareFilter(typeof(LocalizationMiddleware))]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/{lang}/[controller]")]
    [ApiController]
    //[Authorize]
    public class FileManagementController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private string Root = "D:\\File\\";
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IAWSS3Service _iAWSS3Service;
        public FileManagementController(IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            IStringLocalizer<LanguageSub> stringLocalizer,IServiceOptions options,
            IAWSS3Service iAWSS3Service)
        {
            _hostingEnvironment = hostingEnvironment;
            _stringLocalizer = stringLocalizer;
            Root = options.FileManagementOptions.URL_SAVE_FILE;
            _iAWSS3Service = iAWSS3Service;
        }
        /// <summary>
        /// Add IFormFileCollection
        /// </summary>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Add(string DocType,string FileName)
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file != null && file.Length > 0)
                {
                    string fullPath =string.Format("{0}{1}\\{2}", Root,DocType,FileName);
                    if (!Directory.Exists(string.Format("{0}{1}", Root, DocType)))
                    {
                        Directory.CreateDirectory(string.Format("{0}{1}", Root, DocType));
                    }
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new ResponseResult(HttpStatusCode.OK, _stringLocalizer[LanguageSub.MSG_INSERT_SUCCESS]));
                }
                return Ok(new ResponseResult(HttpStatusCode.BadRequest, _stringLocalizer[LanguageSub.MSG_OBJECT_EMPTY]));
            }
            catch (Exception ex)
            {
                return Ok(new ResponseResult(HttpStatusCode.BadRequest, _stringLocalizer[LanguageSub.MSG_INSERT_FAIL_SYSTEM_ERROR]));
            }
        }
        /// <summary>
        /// Delete File
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(string fileName)
        {
            try
            {
                string fullPath = string.Format("{0}{1}", Root,fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    return Ok(new ResponseResult(HttpStatusCode.OK, _stringLocalizer[LanguageSub.MSG_DELETE_SUCCESS]));
                }
                return Ok(new ResponseResult(_stringLocalizer[LanguageSub.MSG_OBJECT_NOT_EXISTS]));
            }
            catch (Exception ex)
            {
                return Ok(new ResponseResult(HttpStatusCode.BadRequest, _stringLocalizer[LanguageSub.MSG_DELETE_FAIL_SYSTEM_ERROR]));
            }
        }
        [HttpGet]
        public async Task<IActionResult> LoadDataFileAsync(string fileName)
        {
            try
            {
                string fullPath = string.Format("{0}{1}", Root, fileName);
                string test = "\\\\192.168.7.88\\inetpub\\eTMS-FileUpload\\FCLTransportRequest";
                if (System.IO.File.Exists(fullPath))
                {
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(fullPath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return Ok(memory.ToArray());
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(null);
        }
        [HttpGet("CheckFileName/{DocType}/{FileName}")]
        public IActionResult CheckFileName(string DocType,string FileName)
        {
            int count = 1;
            string fullPath = string.Format("{0}{1}\\{2}", Root, DocType, FileName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
            string extension = Path.GetExtension(fullPath);
            string path = Path.GetDirectoryName(fullPath);
            string newFullPath = fullPath;
            while (System.IO.File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }
            return Ok(new ResponseResult(HttpStatusCode.OK,newFullPath.Substring(string.Format("{0},{1}\\",Root,DocType).Length-1)));
        }
        [HttpGet("GetFullPathFile/{DocType}/{FileName}")]
        public IActionResult GetFullPathFile(string DocType, string FileName)
        {
            return Ok(new ResponseResult(HttpStatusCode.OK, string.Format("{0}{1}\\{2}", Root, DocType, FileName)));
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        [HttpGet(nameof(UserGuide))]
        //[Authorize]
        public IActionResult UserGuide()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(),
                                    "wwwroot", "UserGuide", "eTMS USER GUIDE DOCUMENT_v02.docx");

            return PhysicalFile(file, "application/msword");
        }

        [Route("getObject")]
        [HttpGet]
        public async Task<IActionResult> getObject(string fileKey, int type)
        {
            try
            {
                var ObjResult = await _iAWSS3Service.getObjectAsync(fileKey, type);
                return File(ObjResult.Result, ObjResult.Extenstion);
            }
            catch
            {
                return Ok("NoFile");
            }
        }

        [Route("postObject")]
        [HttpPost]
        public async Task<IActionResult> postObject(IFormFile file, int type)
        {
            try
            {
                return Ok(await _iAWSS3Service.postObjectAsync(file, type));
            }
            catch
            {
                throw;
            }
        }
    }

}
