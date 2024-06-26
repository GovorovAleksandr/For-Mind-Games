using Leopotam.Ecs;
using Project.Reusable;
using UnityEngine;

namespace Project.Gameplay.Server
{
    public class CubeZoneSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly CubeZoneConfig _config;

        public void Init()
        {
            var positions = _config.Positions;

            foreach(var position in positions) InvokeSpawn(_config.Prefab, position);
        }

        private void InvokeSpawn(GameObject prefab, Vector3 position)
        {
            SpawnPrefabEvent spawnEvent = new()
            {
                Prefab = prefab,
                Position = position,
                Parent = CubeZoneTag.Transform,
                Takable = true
            };
            _world.SendMessage(spawnEvent);
        }
    }
}