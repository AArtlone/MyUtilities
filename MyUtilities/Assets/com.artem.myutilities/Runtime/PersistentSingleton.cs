using UnityEngine;

namespace MyUtilities
{
    public abstract class PersistentSingleton<T> : MonoBehaviour where T : PersistentSingleton<T>
    {
        public static T Instance;

        public static bool Exists { get; private set; }

        protected abstract void Awake();

        protected void SetInstance(T instance)
        {
            if (Instance !=  null)
            {
                Destroy(this);
                return;
            }

            Instance = instance;
            Exists = true;
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
                Exists = false;
            }
        }
    }
}
