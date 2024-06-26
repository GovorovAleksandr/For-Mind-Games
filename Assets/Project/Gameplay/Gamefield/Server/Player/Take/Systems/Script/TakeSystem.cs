using Leopotam.Ecs;
using Mirror;
using Project.Gameplay.Client;
using Project.Gameplay.Server;
using UnityEngine;

namespace Project.Gameplay.Servw
{
    public class TakeSystem : IEcsInitSystem
    {
        private readonly
            EcsFilter<PlayerIdComponent, ModelComponent, NearestCubeComponent> _filter;


        public void Init() => NetworkServer.RegisterHandler<TakeMessage>(TryTake);

        private void TryTake(NetworkConnection connection, TakeMessage _)
        {
            foreach(var i in _filter)
            {
                ref var idComponent = ref _filter.Get1(i);
                var id = idComponent.Id;

                if (id != connection.connectionId) continue;

                ref var modelComponent = ref _filter.Get2(i);
                ref var nearestCube = ref _filter.Get3(i);

                var transform = modelComponent.ModelTransform;
                var cubeComponent = nearestCube.CubeComponent;
                var cubeTransform = cubeComponent.Transform;

                if (cubeTransform == null) return;

                cubeTransform.parent = transform;
                cubeTransform.localPosition = Vector3.down;

                ref var entity = ref _filter.GetEntity(i);
                entity.Get<HoldingCubeComponent>().CubeComponent = cubeComponent;
            }
        }
    }
}