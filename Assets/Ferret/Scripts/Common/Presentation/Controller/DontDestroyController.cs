using UnityEngine;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class DontDestroyController : MonoBehaviour
    {
        private static DontDestroyController _instance;

        private void Awake()
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Init()
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
}