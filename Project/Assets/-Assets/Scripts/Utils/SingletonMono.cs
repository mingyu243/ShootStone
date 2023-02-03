using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    static T s_instance;

    static public T Instance { get { Init(); return s_instance; } }

    static void Init()
    {
        if (s_instance == null) 
        {
            T find = GameObject.FindObjectOfType<T>();
            GameObject go = find?.gameObject;
            if (go == null)
            {
                string name = $"@{typeof(T).Name}";
                go = new GameObject(name);

                go.AddComponent<T>();
            }

            s_instance = go.GetComponent<T>();
        }
    }
}
