using CriWare;
using UnityEngine;

namespace Ferret.Common.Presentation.Controller
{
    [RequireComponent(typeof(CriAtomSource))]
    public abstract class BaseCriAtomSource : MonoBehaviour
    {
        private CriAtomSource _atomSource;
        private CriAtomSource _criAtomSource => _atomSource ??= GetComponent<CriAtomSource>();

        protected void SetCueSheet(string sheetName)
        {
            _criAtomSource.cueSheet = sheetName;
        }

        protected void SetLoop(bool value)
        {
            _criAtomSource.loop = value;
        }

        protected void PlayCue(string cueName)
        {
            _criAtomSource.cueName = cueName;
            _criAtomSource.Play();
        }

        protected void StopSource()
        {
            _criAtomSource.Stop();
        }

        protected bool IsSourceStatus(CriAtomSource.Status status)
        {
            return _criAtomSource.status == status;
        }

        protected void SetVolumeSource(float value)
        {
            if (IsSourceStatus(CriAtomSource.Status.PlayEnd))
            {
                _criAtomSource.Play();
            }

            _criAtomSource.volume = value;
        }
    }
}