using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZomatoFoodAPI_BusinessEntities.Dtos;
using ZomatoFoodAPI_BusinessEntities.StoredProcedureResponseModels;

namespace ZomatoFoodAPI_BusinessEntities.Interfaces
{
    public  interface IFilesUploadService
    {
        Task<FileUploadResponse> AddFileUpload(FileUploadDTO fileUploadDTO);
        Task<List<FileUploadDTO>> GetFileUploadList();
        Task<FileUploadDTO> GetFileUploadDetailsById(int Id);
    }
}
