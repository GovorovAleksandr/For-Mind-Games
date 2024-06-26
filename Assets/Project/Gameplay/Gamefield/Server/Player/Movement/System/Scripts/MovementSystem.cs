using Leopotam.Ecs;
using Mirror;
using Project.Gameplay.Client;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Gameplay.Server
{
    internal sealed class MovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly
            EcsFilter<
                PlayerIdComponent,
                DirectionComponent,
                MovableComponent,
                ModelComponent> _filter;

        private readonly EcsWorld _world;

        public void Init() => NetworkServer.RegisterHandler<DirectionMessage>(HandlePlayerInput);

        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var idComponent = ref _filter.Get1(i);
                ref var directionComponent = ref _filter.Get2(i);
                ref var movableComponent = ref _filter.Get3(i);
                ref var modelComponent = ref _filter.Get4(i);

                ref var rawdirection = ref directionComponent.Direction;
                ref var transform = ref modelComponent.ModelTransform;

                ref var rigidbody = ref movableComponent.Rigidbody;
                ref var speed = ref movableComponent.Speed;

                if (transform == null) continue;
                if (rigidbody == null) continue;

                var x = rawdirection.x;
                var y = rigidbody.velocity.y;
                var z = rawdirection.z;

                Vector3 direction = new(x, y, z);
                rigidbody.velocity = direction * speed;
            }
        }

        private void HandlePlayerInput(NetworkConnection connection, DirectionMessage message)
        {
            foreach (var i in _filter)
            {
                ref var idComponent = ref _filter.Get1(i);

                if (idComponent.Id != connection.connectionId) continue;

                ref var directionComponent = ref _filter.Get2(i);
                directionComponent.Direction = message.Direction;
            }
        }
    }
}