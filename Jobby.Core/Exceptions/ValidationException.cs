﻿using FluentValidation.Results;
using Jobby.Core.Exceptions.Common;
using System.Net;

namespace Jobby.Core.Exceptions;
public class ValidationException : Exception, IServiceException
{
    private string _message;
    private List<string> _errors = new();

    public ValidationException(ValidationResult result)
    {
        foreach (var error in result.Errors)
        {
            _errors.Add(error.ErrorMessage);
        }
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => _message;
}
