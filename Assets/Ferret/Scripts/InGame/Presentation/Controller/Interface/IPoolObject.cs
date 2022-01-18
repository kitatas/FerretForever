using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public interface IPoolObject
    {
        GameObject self { get; }
        void Release();
    }
}