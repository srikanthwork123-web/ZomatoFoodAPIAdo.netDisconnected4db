using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZomatoFoodAPI_BusinessEntities.Dtos;
using ZomatoFoodAPI_BusinessEntities.Models;

namespace ZomatoFoodAPI_ServiceLayer.AutoMapper
{
    //create one class and inherit this class from profile class of automapper library and
    //then we will create the constructor for this class and inside the constructor we will create
    //the mapping configuration for the source and destination objects using the CreateMap method
    //of the AutoMapper library.
    public class AutoMapperProfile:Profile
    {
//create the constructor for this class and inside the constructor we will create
//the mapping configuration for the source and destination objects using the CreateMap method of the AutoMapper library.
        public AutoMapperProfile()
        {
            //=================syntax for creating the mapping configuration=================
            // CreateMap<SourceClass, DestinationClass>();
            // Example:
            // CreateMap<UserEntity, UserDTO>();
            //===========================================
CreateMap<FileUploadDTO,FileUpload>();//this is used to map the data of fileUploadDTO class object to fileUpload class object
CreateMap<FileUpload, FileUploadDTO>();//this is used to map the data of fileUpload class object to fileUploadDTO class object
        }
    }
}
/*
//in repository layer we are using model/entity classes and return the data of model/class object data.


//in Service layer we are using Dto(Data transfer object) classes and return the data of Dto class object data.
*/