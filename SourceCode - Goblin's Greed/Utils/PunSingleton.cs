using UnityEngine;
using Photon.Pun;

namespace Utils
{
    public class PunSingleton<T> : MonoBehaviourPunCallbacks where T : MonoBehaviourPunCallbacks
    {
        private static T _instance;
        
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] allSingletonsInScene = FindObjectsOfType<T>();

                    if (allSingletonsInScene != null && allSingletonsInScene.Length > 0)
                    {
                        if (allSingletonsInScene.Length > 1)
                        {
                            Debug.LogWarning("You have more than one " + typeof(T) + " in the scene!");

                            for (int i = 1; i < allSingletonsInScene.Length; i++)
                            {
                                Destroy(allSingletonsInScene[i].gameObject);
                            }
                        }

                        _instance = allSingletonsInScene[0];
                    }
                    else
                    {
                        Debug.LogError("You need to add the script " + typeof(T) + " to a GameObject in the scene!");
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null) return;
            Destroy(this.gameObject);
            Debug.LogWarning("Duplicated instance of " + typeof(T) + " has been destroyed");
        }
    }
}