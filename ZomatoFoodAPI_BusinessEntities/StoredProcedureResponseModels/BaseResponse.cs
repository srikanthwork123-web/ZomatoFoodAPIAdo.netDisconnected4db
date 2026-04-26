using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZomatoFoodAPI_BusinessEntities.StoredProcedureResponseModels
{
    public class BaseResponse
    {
        public string ERROR_CODE { get; set; }
        public string ERROR_MESSAGE { get; set; }
        //we can also set the default value to property like this
        //public string ERROR_MESSAGE { get; set; } = "No error";
        //public string ERROR_MESSAGE { get; set; } ="";(empty value also you can assign)
    }
}
