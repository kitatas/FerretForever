using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class InputView : MonoBehaviour
    {
        [SerializeField] private Button backButton = default;

        public UniTask OnClickAsync(CancellationToken token) => backButton.OnClickAsync(token);
    }
}