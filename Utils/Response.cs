using Microsoft.AspNetCore.Mvc;

namespace App.Utils;

public class ResponseUtils
{
    public static object SuccessResponse(object data, string message)
    {
        return new {
            ok = true,
            result = data,
            message = message
        };
    }

    public static object ErrorResponse(object data, string message)
    {
        return new {
            ok = false,
            result = data,
            message = message
        };
    }
}