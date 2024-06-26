using UnityEngine;

namespace Project.Gameplay.Server
{
    public struct SpawnPrefabEvent
    {
        public GameObject Prefab;
        public Transform Parent;
        public Vector3 Position;
        public bool Takable;
    }
}