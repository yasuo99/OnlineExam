using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LuyenThiOnline.Helpers
{
    public static class Extension
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Alow-Origin", "*");
        }
        public static void AddPaginationHeader(this HttpResponse response, int currentPage,
        int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
        public static int CalculateAge(this DateTime birthDate)
        {
            var age = DateTime.Now.Year - birthDate.Year;
            if (birthDate.AddYears(age) > DateTime.Now)
            {
                age--;
            }
            return age;
        }
        //
        // Summary:
        //     Encapsulates a method that has one parameter and returns a value of the type
        //     specified by the TResult parameter.
        //
        public static bool SameValue<T>(this T subject, Dictionary<dynamic, dynamic> propertiesValue) where T : class
        {
            var properties = subject.GetType().GetProperties();
            int sameCount = 0;
            foreach (var item in propertiesValue)
            {
                foreach (var property in properties)
                {
                    if (item.Key.Equals(property.Name.ToString()))
                    {
                        if (item.Value.Equals(property.GetValue(subject)))
                        {
                            sameCount++;
                        }
                    }
                }
            }
            if (sameCount == propertiesValue.Count)
            {
                return true;
            }
            return false;
        }
    }
}