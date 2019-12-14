using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class CommonUtilities
{
    public static T ConvertStringTo<T>(string str)
    {
        if (string.IsNullOrEmpty(str))
            return default(T);

        Type resultType = typeof(T);
        TypeConverter converter = TypeDescriptor.GetConverter(resultType);
        bool canConvert = converter.CanConvertFrom(typeof(string));

        object result = null;
        if (canConvert)
        {
            try
            {
                result = converter.ConvertFromString(str);
            }
            catch (Exception)
            {
                result = default(T);
            }
        }
        else
        {
            Debug.LogErrorFormat("[Common]: Can not convert type {0} from string.", resultType);
        }

        return (T)result;
    }

    public static void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
    }

    public static void SaveToFile(string stream, string path)
    {
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        FileInfo file = new FileInfo(path);

        StreamWriter sw = file.CreateText();
        sw.Write(stream);
        sw.Close();
        sw.Dispose();
    }

#if UNITY_EDITOR
    // Get a none public method from UnityEditor.dll, 
    // in order to receive all subclasses of T
    static Type Type_EditorAssemblies = Assembly.Load("UnityEditor.dll").GetType("UnityEditor.EditorAssemblies");
    static MethodInfo Method_SubclassesOf = Type_EditorAssemblies.GetMethod(
        "SubclassesOf",
        BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance,
        null,
        new Type[] { typeof(Type) },
        null
    );

    public static IEnumerable<Type> GetAllSubTypesOfTypeFromEditor<T>()
    {
        return Method_SubclassesOf.Invoke(null, new object[] { typeof(T) }) as IEnumerable<Type>;
    }

    [Obsolete("Use GetAllSubTypesOfTypeFromEditor()")]
    public static List<Type> GetAllSubTypesOfTypeFromMonoScript<T>() where T : MonoBehaviour
    {
        List<Type> result = new List<Type>();

        string[] guids = AssetDatabase.FindAssets("t:MonoScript");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
            var type = script.GetClass();

            if (type == null || !type.IsSubclassOf(typeof(T)))
                continue;

            result.Add(type);
        }

        return result;
    }
#endif
}
