using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZomatoFoodAPI_ServiceLayer.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void InitializeMap(IServiceCollection services)
        {
            //Initialize all AutoMapper profiles
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<AutoMapperProfile>();


            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
