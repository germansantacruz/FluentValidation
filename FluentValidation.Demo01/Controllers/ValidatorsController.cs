using FluentValidation.Demo01.Models;
using FluentValidation.Demo01.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.Demo01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidatorsController : ControllerBase
    {
        [HttpGet("validate-product")]
        public async Task<IActionResult> ValidateProduct(string name)
        {
            IActionResult result;
            Product p = new Product { Name = name };
            ProductValidator validator = new ProductValidator();

            ValidationResult validationResult = await validator.ValidateAsync(p);

            if (validationResult.IsValid)
            {
                result = Ok("Producto válido");
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                foreach (var failure in validationResult.Errors)
                {
                    builder.AppendLine(string.Format("Propiedad: {0}. Error: {1}"
                        , failure.PropertyName, failure.ErrorMessage));
                }

                result = BadRequest(builder.ToString());
            }

            return result;
        }
    }
}
