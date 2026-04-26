using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZomatoFoodAPI_BusinessEntities.Interfaces;
using ZomatoFoodAPI_BusinessEntities.Models;
using ZomatoFoodAPI_BusinessEntities.StoredProcedureResponseModels;
using ZomatoFoodAPI_BusinessEntities.Utils;

namespace ZomatoFoodAPI_RepositoryLayer
{
    public class FilesUploadRepository: IFilesUploadRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public FilesUploadRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        //IConnectionFactory is used to fetch the connection string information here we are using dependency injection to inject the connection factory in the constructor of the repository class and then we are using that connection factory to get the connection string information in the methods of the repository class
        public async Task<List<FileUpload>> GetFileUploadList()
        {
            List<FileUpload> lstfiles = new List<FileUpload>();//it will store list of file upload details which we are fetching from the database
            //Don't hardcode the connection string below way, instead use the connection factory to get the connection string information from the appsettings.json file or from the environment variables or from the user secrets or from any other secure place where you are storing the connection string information
            // string conectonstring = "Server=DESKTOP-AAO14OC;Database=hotelmanagement;integrated security=yes;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;";
            using (SqlConnection con = _connectionFactory.HotelmanagementsqlConnectionString())//here we are getting the conection string
            {
                SqlCommand cmd = new SqlCommand(Storedprocedures.GetFileUpload_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "FileUpload");
                DataTable dt = new DataTable();
                dt = ds.Tables["FileUpload"];
                foreach (DataRow dr in dt.Rows)
                {//every time we will stroe one record in object of file upload and then we will add that object in the list of file upload details
                    FileUpload fileUpload = new FileUpload();
                    fileUpload.Id = Convert.ToInt32(dr["Id"]);
                    fileUpload.FileName = Convert.ToString(dr["FileName"]);
                    fileUpload.ModifiedFilename = Convert.ToString(dr["ModifiedFilename"]);
                    fileUpload.FilePath = Convert.ToString(dr["FilePath"]);
                    fileUpload.Createdby = Convert.ToString(dr["Createdby"]);
                    fileUpload.CreatedDatetTime = Convert.ToDateTime(dr["CreatedDateTime"]);
                    lstfiles.Add(fileUpload);
                }
            }
            return lstfiles;
        }

        public async Task<FileUpload> GetFileUploadDetailsById(int Id)
        {//here based on id we are featching the data from the database and then we are storing that data in the object of file upload and then we are returning that object to the service layer
            FileUpload fileUpload = new FileUpload();
            using (SqlConnection con = _connectionFactory.HotelmanagementsqlConnectionString())//here we are getting the conection string
            {
                SqlCommand cmd = new SqlCommand(Storedprocedures.GetFileUploadDetailsById_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "FileUpload");
                DataTable dt = new DataTable();
                dt = ds.Tables["FileUpload"];
                foreach (DataRow dr in dt.Rows)
                {
                    fileUpload.Id = Convert.ToInt32(dr["Id"]);
                    fileUpload.FileName = Convert.ToString(dr["FileName"]);
                    fileUpload.ModifiedFilename = Convert.ToString(dr["ModifiedFilename"]);
                    fileUpload.FilePath = Convert.ToString(dr["FilePath"]);
                    fileUpload.Createdby = Convert.ToString(dr["Createdby"]);
                    fileUpload.CreatedDatetTime = Convert.ToDateTime(dr["CreatedDateTime"]);
                }
            }
            return fileUpload;
        }

        public async Task<FileUploadResponse> AddFileUpload(FileUpload fileUpload)
        {
            FileUploadResponse fileUploadResponse = new FileUploadResponse();
            using (SqlConnection con = _connectionFactory.HotelmanagementsqlConnectionString())//here we are getting the conection string
            {
                SqlCommand cmd = new SqlCommand(Storedprocedures.AddFileUpload_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileName", fileUpload.FileName);
                cmd.Parameters.AddWithValue("@FilePath", fileUpload.FilePath);
                cmd.Parameters.AddWithValue("@ModifiedFilename", fileUpload.ModifiedFilename);
                cmd.Parameters.AddWithValue("@Createdby", fileUpload.Createdby);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "FileUpload");
                DataTable dt = new DataTable();
                dt = ds.Tables["FileUpload"];
                foreach (DataRow dr in dt.Rows)
                {
                    fileUploadResponse.ERROR_CODE = Convert.ToString(dr["ERROR_CODE"]);
                    fileUploadResponse.ERROR_MESSAGE = Convert.ToString(dr["ERROR_MESSAGE"]);
                }
            }
            return fileUploadResponse;
        }
    }
}
/*
=============HotelMangment database we have file upload related stored procedures are available========
1.Usp_AddFileUpload  (this store procedure is used to add the file information into database)
2.GetFileUpload  (this stored procedure is used to getthefileupload information)
3.GetFileUploadDetailsById(this stored procedure is used to get the file upload information using id based)
=========================================================================================================
*/
