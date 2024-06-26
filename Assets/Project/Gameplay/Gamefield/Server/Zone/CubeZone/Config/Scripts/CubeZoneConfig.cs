using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Server
{
    [CreateAssetMenu(fileName = "Cube Zone Config", menuName = "Project/CubeZone/Config")]
    public class CubeZoneConfig : ScriptableObject
    {
        public GameObject Prefab;
        public List<Vector3> Positions;
    }
}