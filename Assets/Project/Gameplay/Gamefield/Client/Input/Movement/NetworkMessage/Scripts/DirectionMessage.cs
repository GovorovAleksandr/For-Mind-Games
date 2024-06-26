using Leopotam.Ecs;
using Mirror;
using UnityEngine;

namespace Project.Gameplay
{
    public struct DirectionMessage : NetworkMessage
    {
        public Vector3 Direction;
    }
}