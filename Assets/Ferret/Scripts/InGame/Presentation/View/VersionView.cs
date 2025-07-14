using Ferret.Common;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class VersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI version = default;

        private void Awake()
        {
            version.text =
                $"{VersionConfig.MAJOR_VERSION}.{VersionConfig.MIDDLE_VERSION}.{VersionConfig.MINOR_VERSION}";
        }
    }
}