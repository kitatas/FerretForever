using UnityEngine;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class DontDestroyController : MonoBehaviour
    {
        public static DontDestroyController Instance { get; private set; }

        private void Start()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Init()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
}