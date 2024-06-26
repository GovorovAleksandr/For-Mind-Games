using Project.Reusable.Server;
using UnityEngine;

namespace Project.Gameplay.Server
{
    public class ReferenceZoneTag : ExecuteInServer
    {
        public static Transform Transform { get; private set; }

        private void Awake() => Transform = transform;
    }
}