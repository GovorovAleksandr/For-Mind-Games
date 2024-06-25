using System;
using UnityEngine;

namespace Project.Gameplay
{
    [Serializable]
    internal struct MovableComponent
    {
        public CharacterController CharacterController;
        public float Speed;
    }
}