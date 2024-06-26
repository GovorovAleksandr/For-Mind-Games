using Leopotam.Ecs;
using Project.Gameplay.Client;
using UnityEngine;

namespace Project.Gameplay.Server
{
    public class NearestCubeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, PlayerIdComponent> _playerFilter;
        private readonly EcsFilter<CubeTag, TakableComponent> _takableFilter;

        private readonly ServerConfig _config;

        public void Run()
        {
            foreach(var i in _playerFilter)
            {
                ref var playerEntity = ref _playerFilter.GetEntity(i);
                ref var modelComponent = ref _playerFilter.Get1(i);
                var position = modelComponent.ModelTransform.position;

                if (modelComponent.ModelTransform == null) continue;

                float minDistance = _config.MaxTakeDistance;
                CubeTag nearestCube = default;

                foreach(var j in _takableFilter)
                {
                    ref var cubeTag = ref _takableFilter.Get1(j);
                    var cubeTransform = cubeTag.Transform;
                    var cubePosition = cubeTransform.position;

                    var distance = Vector3.Distance(cubePosition, position);

                    if (distance < minDistance)
                    {
                        nearestCube = cubeTag;
                        minDistance = distance;
                    }
                }

                playerEntity.Get<NearestCubeComponent>().CubeComponent = nearestCube;
            }
        }
    }
}