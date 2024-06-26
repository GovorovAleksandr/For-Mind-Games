using Leopotam.Ecs;
using Project.Reusable;
using UnityEngine;

namespace Project.Gameplay.Server
{
    public class ReferenceZoneSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;

        private readonly ReferenceZoneConfig _config;

        public void Init()
        {
            var positions = _config.Positions;

            int sended = 0;

            foreach(var position in positions)
            {
                var randomInt = Random.Range(0, 100);
                if (randomInt > _config.Chance) continue;
                InvokeSpawn(_config.Prefab, position);
                sended++;
            }

            if (sended > 0) return;

            var randomIndex = Random.Range(0, positions.Count);
            var randomPosition = positions[randomIndex];
            InvokeSpawn(_config.Prefab, randomPosition);
        }

        private void InvokeSpawn(GameObject prefab, Vector3 position)
        {
            SpawnPrefabEvent spawnEvent = new()
            {
                Prefab = prefab,
                Position = position,
                Parent = ReferenceZoneTag.Transform,
                Takable = false
            };
            _world.SendMessage(spawnEvent);
        }
    }
}