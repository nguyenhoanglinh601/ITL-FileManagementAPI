using Amazon.S3.Model;
using FileManagement.DL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement.DL.IService
{
    public interface IAWSS3Service
    {
        //Task<int> getNumberOfBucketsAsync();
        Task<PutObjectResponse> postObjectAsync(IFormFile file, int type);
        Task<AWSS3ObjectResponse> getObjectAsync(string fileKey, int type);
    }
}
