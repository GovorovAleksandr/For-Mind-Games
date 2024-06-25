using Leopotam.Ecs;
using UnityEngine;

namespace Project.Gameplay
{
    internal sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, MovableComponent, DirectionComponent> _movableFilter = null;

        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var modelComponent = ref _movableFilter.Get1(i);

                if (!modelComponent.ModelNetworkTransform.isLocalPlayer) continue;

                ref var movableComponent = ref _movableFilter.Get2(i);
                ref var directionComponent = ref _movableFilter.Get3(i);

                ref var direction = ref directionComponent.Direction;
                ref var transform = ref modelComponent.ModelNetworkTransform.target;

                if (transform == null) continue;

                ref var characterController = ref movableComponent.CharacterController;
                ref var speed = ref movableComponent.Speed;

                var rawDirection = (transform.right * direction.x) + (transform.forward * direction.z);
                characterController.Move(rawDirection * speed * Time.deltaTime);
            }
        }
    }
}