using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SharedDataManager
{
    private static Dictionary<string, object> sharedData = new Dictionary<string, object>();

    /// <summary>
    /// Añade un valor al diccionario, si ya existe lo actualiza
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    public static void SetDataByKey(string key, object data)
    {
        if (sharedData.ContainsKey(key))
        {
            sharedData[key] = data;
        }
        else 
        {
            sharedData.Add(key, data);
        }
    }

    /// <summary>
    /// Intenta conseguir un valor por string, si no existe devuelve null
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static object GetDataByKey(string key)
    {
        if (sharedData.ContainsKey(key))
        {
            return sharedData[key];
        }
        else
        {
            return null;
        }
    }
}
