using Leopotam.Ecs;
using Mirror;
using UnityEngine;

namespace Project.Gameplay.Server
{
    public class FactorySystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnPrefabEvent> _filter;

        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                var position = component.Position;
                var parent = component.Parent;
                var prefab = component.Prefab;

                var spawnedObject = Object.Instantiate(prefab);
                spawnedObject.transform.parent = parent;
                spawnedObject.transform.localPosition = position;

                ref var entity = ref _filter.GetEntity(i);

                if (component.Takable) entity.Get<TakableComponent>();
                else spawnedObject.isStatic = true;

                NetworkServer.Spawn(spawnedObject);

                entity.Get<CubeTag>() = new()
                {
                    Transform = spawnedObject.transform,
                    DefaultParent = parent
                };
                entity.Del<SpawnPrefabEvent>();
            }
        }
    }
}