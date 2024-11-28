using System.Text.RegularExpressions;

namespace Procademy_ASPNETCOREMVC_2.CustomConstrains;

public class AlphaNumericConstraint : IRouteConstraint
{
    public bool Match(
        HttpContext? httpContext,
        IRouter? route,
        string routeKey,
        RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        if (!values.ContainsKey(routeKey))
            return false;

        Regex regex = new("^[a-zA-Z][a-zA-Z0-9]*$");
        string? userNameValue = Convert.ToString(values[routeKey]);
        if (userNameValue != null && regex.IsMatch(userNameValue))
            return true;

        return false;
    }
}