using AutoMapper;
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
        private readonly IMapper _mapper;
        public FilesUploadService(IFilesUploadRepository filesUploadRepository, IMapper mapper)
        {
            //this is current class object,by using this keyword you can access the members of the current class in the constructor of the class and then you can assign the value to the private readonly field of the interface type
            this._filesUploadRepository = filesUploadRepository;//this process called constructor injection process (we are inject the dependency in the constructor of the service class and then we are assigning that dependency to the private readonly field of the interface type and then we can use that private readonly field to access the members of the repository class in the service class.)
            this._mapper = mapper;
        }
        //for example one class having so many dependencies we need to inject those dependencies into constructor of that class and we can use those dependencies in the method of that class.
        public async Task<List<FileUploadDTO>> GetFileUploadList()
        {
            var fileUploadList = await _filesUploadRepository.GetFileUploadList();

  #region before Automapper we are mapping the entiy class object to dto class object process
  //          List<FileUploadDTO> lstFileUploadDTO = new List<FileUploadDTO>();
  //          foreach (var fileUpload in fileUploadList)
  //          {
  //              FileUploadDTO fileUploadDTO = new FileUploadDTO();
             

  //              #region before Automapper we are mapping the entiy class object to dto class object process
  //              //fileUploadDTO.Id = fileUpload.Id;
  //              //fileUploadDTO.FileName = fileUpload.FileName;
  //              //fileUploadDTO.ModifiedFilename = fileUpload.ModifiedFilename;
  //              //fileUploadDTO.FilePath = fileUpload.FilePath;
  //              //fileUploadDTO.Createdby = fileUpload.Createdby;
  //              //fileUploadDTO.CreatedDatetTime = fileUpload.CreatedDatetTime;
  //              #endregion region

  //              lstFileUploadDTO.Add(fileUploadDTO);
  //          }
  //          return lstFileUploadDTO;
            #endregion


     //The above enire code was replaced by below single of code.
     //var result= _mapper.Map<List<FileUploadDTO>>(fileUploadList);(for debugging purpose assign into one variable and you can check)
            return _mapper.Map<List<FileUploadDTO>>(fileUploadList);//entity to dto mapping
        }
        public async Task<FileUploadDTO> GetFileUploadDetailsById(int Id)
        {//if you are using any class as a return type of method,we must return the value of that class object.this is rule
            var result = await _filesUploadRepository.GetFileUploadDetailsById(Id);
            // 1)Auto mapper is used to create a mapping between  to source model object to destination model object        
            return _mapper.Map<FileUploadDTO>(result);//entity to dto mapping
//in Service layer we are using Dto(Data transfer object) classes and return the data of Dto class object data.
            #region before Automapper we are mapping the entiy class object to dto class object process
            //FileUploadDTO fileUploadDTO = new FileUploadDTO();
            //fileUploadDTO.ModifiedFilename = result.ModifiedFilename;
            //fileUploadDTO.Id = result.Id;
            //fileUploadDTO.FileName = result.FileName;
            //fileUploadDTO.ModifiedFilename = result.ModifiedFilename;
            //fileUploadDTO.FilePath = result.FilePath;
            //fileUploadDTO.Createdby = result.Createdby;
            //fileUploadDTO.CreatedDatetTime = result.CreatedDatetTime;
            //return fileUploadDTO;
            #endregion

        }

        public async Task<FileUploadResponse> AddFileUpload(FileUploadDTO fileUploadDTO)
        {
            FileUpload obj = new FileUpload();//destinationmodelclass object
                                              //This Code was replaced by above Automapper concept.
// 1)Auto mapper is used to create a mapping between  to source model object to destination model object
            _mapper.Map(fileUploadDTO, obj);//sourceobject,destinationobject
//Converting source modelobject to destination modelobject
//Syntax:   _mapper.Map(SourceModelObject,DestinationModelObject)
//once mapping is created, source model object can be converted to destination model object with less code and easy way.
            #region before Automapper we are mapping the entiy class object to dto class object process
            //This Code was replaced by above Automapper concept.
            //obj.FileName = fileUploadDTO.FileName;
            //obj.FilePath = fileUploadDTO.FilePath;
            //obj.CreatedDatetTime = fileUploadDTO.CreatedDatetTime;
            //obj.Createdby = fileUploadDTO.Createdby;
            //obj.ModifiedFilename = fileUploadDTO.ModifiedFilename;
            //obj.Id = fileUploadDTO.Id;
            #endregion


            var result = await _filesUploadRepository.AddFileUpload(obj);
            return result;

        }
    }

    }
/* 1.what is Automapper?
 1)Auto mapper is used to create a mapping between  to source model object to destination model object

 2)once mapping is created, source model object can be converted to destination model object with less code and easy way.
 
 3)Auto mapper can be instaled by using NUEGet Manage packager.
 
 4) This required two steps:

        1) creating mapping
         
      syntax: mapper.createmap < sourcemodel object,destination modelobject >();
            
           Here <> Means We called as a Placeholder .

         2) Converting source modelobject to destination modelobject

          destination modelclass Reference variable= Mapper.Map<destination modelclassname>(source modelclasspbject)

//===================================================

//Converting source modelobject to destination modelobject
//Syntax:   _mapper.Map(SourceModelObject,DestinationModelObject)
//once mapping is created, source model object can be converted to destination model object with less code and easy way.
================



*/