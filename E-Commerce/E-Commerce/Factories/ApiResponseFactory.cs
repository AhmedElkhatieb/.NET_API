﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrors(ActionContext context)
        {
            //Get All Errors In ModelState
            var errors = context.ModelState.Where(error => error.Value.Errors.Any())
                .Select(error => new ValidationErrors
                {
                    Field = error.Key,
                    Errors = error.Value.Errors.Select(e => e.ErrorMessage)
                });
            // Create Custom Response
            var response = new ValidationErrorsResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "There is a problem with validation",
                Errors = errors
            };
            //Return
            return new BadRequestObjectResult(response);
        }
    }
}
