using UnityEngine;

namespace Project.Gameplay.Server
{
    [CreateAssetMenu(fileName = "Server Config", menuName = "Project/Server/Config")]
    public class ServerConfig : ScriptableObject
    {
        public int PlayersSpeed;
        public float MaxTakeDistance;
    }
}