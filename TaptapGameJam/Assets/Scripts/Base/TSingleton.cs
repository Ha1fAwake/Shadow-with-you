using System;
using System.Reflection;

public class TSingleton<T> where T : class
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Console.WriteLine(_instance);
                var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                if (ctor == null)
                {
                    throw new Exception("[Base]: Non-Public Constructor() not found! in " + typeof(T));
                }
                _instance = ctor.Invoke(null) as T;
            }
            return _instance;
        }
    }
}