using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZomatoFoodAPI_BusinessEntities.Models;
using ZomatoFoodAPI_BusinessEntities.StoredProcedureResponseModels;

namespace ZomatoFoodAPI_BusinessEntities.Interfaces
{
    public interface IFilesUploadRepository
    {
        Task<FileUploadResponse> AddFileUpload(FileUpload fileUpload);//this is used to add the file information
        Task<List<FileUpload>> GetFileUploadList();//this is used fetch ttal files data
        Task<FileUpload> GetFileUploadDetailsById(int Id);//this is used to featch files based on id based
    }
}
