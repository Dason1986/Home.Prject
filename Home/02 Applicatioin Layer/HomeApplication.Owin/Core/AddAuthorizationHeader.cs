using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;

namespace HomeApplication.Owin.API
{

    public class AddPagination : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null) operation.parameters = new List<Parameter>();

            var falg = apiDescription.HttpMethod.Method == "GET";
            if (!falg) return;
            var parameterBindings = apiDescription.ActionDescriptor.ActionBinding.ParameterBindings;

            foreach (var item in parameterBindings)
            {
                if (item.Descriptor.ParameterType == typeof(Pagination))
                {
                    var indexparameter = operation.parameters.FirstOrDefault(n => n.name == "pagination.index");

                    indexparameter.description = "page index";

                    indexparameter.name = "index";




                    var sizeparameter = operation.parameters.FirstOrDefault(n => n.name == "pagination.size");

                    sizeparameter.description = "page size";

                    sizeparameter.name = "size";
                }
            }

        }
    }

    public class AddAuthorizationHeader : IOperationFilter
    {
        /// <summary>
        /// Adds an authorization header to the given operation in Swagger.
        /// </summary>
        /// <param name="operation">The Swashbuckle operation.</param>
        /// <param name="schemaRegistry">The Swashbuckle schema registry.</param>
        /// <param name="apiDescription">The Swashbuckle api description.</param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null) operation.parameters = new List<Parameter>();
            if (operation == null) return;

            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            var parameter = new Parameter
            {
                description = "The authorization token",
                @in = "header",
                name = "Authorization",
                required = true,
                type = "string"
            };

            if (apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            operation.responses.Add("401", new Response { description = "The request did not have the correct authorization header credientials." });

            operation.parameters.Add(parameter);
        }
    }
}
