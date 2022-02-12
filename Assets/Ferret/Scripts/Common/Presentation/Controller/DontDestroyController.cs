using Ferret.Common.Presentation.View;
using UnityEngine;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class DontDestroyController : MonoBehaviour
    {
        public static DontDestroyController Instance { get; private set; }

        public CriBgmController bgmController { get; private set; }
        public CriSeController seController { get; private set; }
        public LoadingView loadingView { get; private set; }
        public TransitionMaskView maskView { get; private set; }

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
            bgmController = FindObjectOfType<CriBgmController>();
            seController = FindObjectOfType<CriSeController>();
            loadingView = GetComponentInChildren<LoadingView>();
            maskView = GetComponentInChildren<TransitionMaskView>();

            DontDestroyOnLoad(gameObject);
        }
    }
}