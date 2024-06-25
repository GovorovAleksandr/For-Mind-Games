using Leopotam.Ecs;
using UnityEngine;

namespace Project.Gameplay
{
    internal sealed class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<DirectionComponent> _directionFilter = null;

        private Gameplay _input;

        private Vector3 _direction;

        public void Init()
        {
            _input = new();
            _input.Enable();
        }

        public void Run()
        {
            SetDirection();

            foreach (var i in _directionFilter)
            {
                ref var directionComponent = ref _directionFilter.Get1(i);
                ref var direction = ref directionComponent.Direction;

                direction = _direction;
            }
        }

        private void SetDirection() => _direction = _input.Player.Movement.ReadValue<Vector3>();
        public void Destroy() => _input.Disable();
    }
}