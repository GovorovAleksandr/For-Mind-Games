using Leopotam.Ecs;
using Mirror;
using Project.Gameplay.Client;
using UnityEngine;

namespace Project.Gameplay.Server
{
    public class PutSystem : IEcsInitSystem
    {
        private readonly
            EcsFilter<PlayerIdComponent, ModelComponent, HoldingCubeComponent> _filter;


        public void Init()
        {
            NetworkServer.RegisterHandler<PutMessage>(TryPut);
        }

        private void TryPut(NetworkConnection connection, PutMessage _)
        {
            foreach (var i in _filter)
            {
                ref var idComponent = ref _filter.Get1(i);
                var id = idComponent.Id;

                if (id != connection.connectionId) continue;

                ref var holdingCubeComponent = ref _filter.Get3(i);
                ref var cubeComponent = ref holdingCubeComponent.CubeComponent;
                var cube = cubeComponent.Transform;

                if (cube == null) return;

                cube.parent = cubeComponent.DefaultParent;

                var cubePosition = cube.position;

                var x = Mathf.RoundToInt(cubePosition.x);
                var y = 0;
                var z = Mathf.RoundToInt(cubePosition.z);

                cube.transform.position = new(x, y, z);

                ref var entity = ref _filter.GetEntity(i);
                entity.Del<HoldingCubeComponent>();
            }
        }
    }
}