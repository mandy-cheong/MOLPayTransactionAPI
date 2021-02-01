using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ParamHelper
/// </summary>
public class ParamHelper
{
    public ParamHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string ObjToURL(object obj)
    {
        string url = "";
        var props = obj.GetType().GetProperties();
        foreach (var prop in props)
        {
            if (prop.GetValue(obj, null) != null)
            {
                url += prop.Name.ToString() + "=" + prop.GetValue(obj, null).ToString() + "&";
            }
        }
        url = url.TrimEnd('&');

        return url;
    }
}