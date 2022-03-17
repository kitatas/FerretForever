using UnityEngine;

namespace Ferret.Common.Presentation.Controller
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseAudioSource : MonoBehaviour
    {
        private AudioSource _source;
        protected AudioSource _audioSource => _source ??= GetComponent<AudioSource>();
    }
}