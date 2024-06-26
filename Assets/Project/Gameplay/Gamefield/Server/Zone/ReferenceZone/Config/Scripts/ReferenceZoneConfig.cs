using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Server
{
    [CreateAssetMenu(fileName = "Reference Zone Config", menuName = "Project/ReferenceZone/Config")]
    public class ReferenceZoneConfig : ScriptableObject
    {
        public GameObject Prefab;
        public List<Vector3> Positions;
        [Range(1, 100)] public int Chance;
    }
}