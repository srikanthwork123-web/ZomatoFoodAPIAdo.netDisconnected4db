using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using ZomatoFoodAPI_BusinessEntities.Dtos;
using ZomatoFoodAPI_BusinessEntities.Interfaces;
using ZomatoFoodAPI_BusinessEntities.StoredProcedureResponseModels;

namespace ZomatoFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesUploadController : ControllerBase
    {
        private readonly IFilesUploadService _filesUploadService;//constructor injection.
        public FilesUploadController(IFilesUploadService filesUploadService)
        {
            this._filesUploadService = filesUploadService;
        }
        [HttpGet]
        [Route("GetAllFileUploadList")]
        public async Task<IActionResult> GetAllFileUploadList()
        {
            try
            {   //1.Constructor Injection to access the fileuploadservice Method.
                var fileUploadList = await this._filesUploadService.GetFileUploadList();
                if (fileUploadList != null)
                {
                    return StatusCode(StatusCodes.Status200OK, fileUploadList);
                    // return Ok(new { response = fileUploadList });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "FileUpload Data not  found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System or Server Error");
            }
        }

        [HttpPost]
        [Route("UploadFile")]
//to catch the file from the client side(angular/react/postman we need to use IFormFile interface and we need to use [FromForm] attribute to bind the data from the form data which is sent from the client side to the server side
//in the form of key value pair and then we are using that data in the method of the controller to perform the required operations.
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string ImageCaption)
        {//IFormFile  is predefined interface,it will catch/take the file from http request
         //[FromForm]  is one attribute,used for binding the data from the form data which is sent from the client side to the server side
         //in the form of key value pair and then we are using that data in the method of the controller to perform the required operations.
            var obj = ImageCaption;
            var result = await WriteFile(file);
            return Ok(new { response = result });
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile([FromQuery] int Id)
        {
            var data = await this._filesUploadService.GetFileUploadDetailsById(Id);
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", data.ModifiedFilename);
            // var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }

        [NonAction]//this attribute is used to indicate that this method is not an action method and it will not be accessible from the client side and it will not be treated as an action method by the framework.]
        private async Task<FileUploadResponse> WriteFile(IFormFile file)
        {
            var dateformat = DateTime.Now.Ticks.ToString();
            FileUploadResponse fileUploadResponse = new();
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = DateTime.Now.Ticks.ToString() + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                FileUploadDTO fileUploadDTO = new();
                fileUploadDTO.FilePath = exactpath;
                fileUploadDTO.FileName = file.FileName;
                fileUploadDTO.ModifiedFilename = filename;
                fileUploadDTO.Createdby = "srikanth";//Read this one from your token in realtime.

                //Service Logic
                fileUploadResponse = await this._filesUploadService.AddFileUpload(fileUploadDTO);
            }

            catch (Exception ex)
            {
            }
            return fileUploadResponse;
        }
    }
}
