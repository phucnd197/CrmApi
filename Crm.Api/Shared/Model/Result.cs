using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Crm_Api.Shared.Model;

public class Result<TRes>
{
    public bool IsSuccess { get; init; }
    public int StatusCode { get; init; }
    public TRes? Data { get; init; }
    public IList<string>? Errors { get; init; }
}

public static class Result
{
    public static Result<T1> Success<T1>(T1 data, int statusCode = 200)
    {
        return new Result<T1>
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccess = true,
        };
    }
    public static Result<T1> Fail<T1>(IList<string> errors, int statusCode = 500)
    {
        return new Result<T1>
        {
            Errors = errors,
            StatusCode = statusCode,
            IsSuccess = false,
        };
    }
    public static Result<T1> Fail<T1>(string error, int statusCode = 500)
    {
        return new Result<T1>
        {
            Errors = [error],
            StatusCode = statusCode,
            IsSuccess = false,
        };
    }
    public static Result<T1> Fail<T1>(ValidationResult validationResult)
    {
        return new Result<T1>
        {
            Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray(),
            StatusCode = 400,
            IsSuccess = false,
        };
    }
    public static ObjectResult ToActionResult<T>(this Result<T> result)
    {
        return new ObjectResult(result)
        {
            StatusCode = result.StatusCode,
        };
    }
}
