using System;
using PlayFab;

namespace Ferret.Common
{
    public sealed class CustomPlayFabException : Exception
    {
        public CustomPlayFabException(PlayFabError error) : base(error.GenerateErrorReport())
        {
            
        }
    }
}