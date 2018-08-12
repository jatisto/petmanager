using System;
using Microsoft.Extensions.Configuration;

namespace pet_manager.Extensions
{
    public static class ConfigurationExtensions
    {
        public static  string GetConnectionStringFromEnvironmentVariable(this IConfiguration configuration, string name=null){
            if(string.IsNullOrEmpty(name))
                return Environment.GetEnvironmentVariable("ConnectionStrings_DefaultConnection");
            else
                return Environment.GetEnvironmentVariable(name);
        }
    }
}