using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZomatoFoodAPI_BusinessEntities.Dtos;
using ZomatoFoodAPI_BusinessEntities.Interfaces;
using ZomatoFoodAPI_BusinessEntities.Models;
using ZomatoFoodAPI_BusinessEntities.StoredProcedureResponseModels;
using ZomatoFoodAPI_RepositoryLayer;

namespace ZomatoFoodAPI_ServiceLayer
{
    public class FilesUploadService : IFilesUploadService
    {
        public readonly IFilesUploadRepository _filesUploadRepository;
        public FilesUploadService(IFilesUploadRepository filesUploadRepository)
        {
            _filesUploadRepository = filesUploadRepository;
        }
        public async Task<List<FileUploadDTO>> GetFileUploadList()
        {
            var fileUploadList = await _filesUploadRepository.GetFileUploadList();
            List<FileUploadDTO> lstFileUploadDTO = new List<FileUploadDTO>();
            foreach (var fileUpload in fileUploadList)
            {
                FileUploadDTO fileUploadDTO = new FileUploadDTO();
                fileUploadDTO.Id = fileUpload.Id;
                fileUploadDTO.FileName = fileUpload.FileName;
                fileUploadDTO.ModifiedFilename = fileUpload.ModifiedFilename;
                fileUploadDTO.FilePath = fileUpload.FilePath;
                fileUploadDTO.Createdby = fileUpload.Createdby;
                fileUploadDTO.CreatedDatetTime = fileUpload.CreatedDatetTime;
                lstFileUploadDTO.Add(fileUploadDTO);
            }
            return lstFileUploadDTO;
        }
        public async Task<FileUploadDTO> GetFileUploadDetailsById(int Id)
        {
            var result = await _filesUploadRepository.GetFileUploadDetailsById(Id);
            FileUploadDTO fileUploadDTO = new FileUploadDTO();
            fileUploadDTO.ModifiedFilename = result.ModifiedFilename;
            fileUploadDTO.Id = result.Id;
            fileUploadDTO.FileName = result.FileName;
            fileUploadDTO.ModifiedFilename = result.ModifiedFilename;
            fileUploadDTO.FilePath = result.FilePath;
            fileUploadDTO.Createdby = result.Createdby;
            fileUploadDTO.CreatedDatetTime = result.CreatedDatetTime;
            return fileUploadDTO;
        }

        public async Task<FileUploadResponse> AddFileUpload(FileUploadDTO fileUploadDTO)
        {
            FileUpload obj = new FileUpload();//destinationmodelclass object
          //This Code was replaced by above Automapper concept.
            obj.FileName = fileUploadDTO.FileName;
            obj.FilePath = fileUploadDTO.FilePath;
            obj.CreatedDatetTime = fileUploadDTO.CreatedDatetTime;
            obj.Createdby = fileUploadDTO.Createdby;
            obj.ModifiedFilename = fileUploadDTO.ModifiedFilename;
            obj.Id = fileUploadDTO.Id;
            var result = await _filesUploadRepository.AddFileUpload(obj);
            return result;

        }
    }

    }
