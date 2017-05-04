using UnityEngine;
using System.Collections;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    static object newLock;
    static bool applicationIsQuitting;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed on application quit. Won't create again - returning null.");
                return null;
            }

            object lock2 = newLock;
            lock (lock2)
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if(FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong - there should never be more than 1 singleton! Reopening the scene might fix it.");
                        return instance;
                    }

                    if(instance == null)
                    {
                        GameObject temp = new GameObject();
                        instance = temp.AddComponent<T>();
                        temp.name = "Singleton " + typeof(T).ToString();

                        DontDestroyOnLoad(temp);

                        Debug.Log(string.Concat(new object[] { "[Singleton] An instance of ", typeof(T), " is needed in the scene, so '", temp, "' was created with DontDestroyOnLoad." }));
                    }

                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " + instance.gameObject.name);
                    }
                }
            }

            return instance;
        }
    }

    public SingletonBase()
    {
        newLock = new object();
        applicationIsQuitting = false;
    }

    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}