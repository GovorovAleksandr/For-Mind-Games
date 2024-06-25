using Mirror;
using System;

namespace Project.Gameplay
{
    [Serializable]
    internal struct ModelComponent
    {
        public NetworkTransformUnreliable ModelNetworkTransform;
    }
}