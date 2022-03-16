using UnityEngine;
using UnityEngine.Assertions;


public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this.GetComponent<T>();
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
}
