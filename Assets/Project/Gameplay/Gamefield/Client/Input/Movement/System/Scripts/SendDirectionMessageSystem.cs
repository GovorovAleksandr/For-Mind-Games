using Leopotam.Ecs;
using Mirror;
using UnityEngine;

namespace Project.Gameplay.Client
{
    public sealed class SendDirectionMessageSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        private Gameplay _input;

        public void Init()
        {
            _input = new();
            _input.Enable();
        }

        public void Run()
        {
            var direction = GetDirection();
            SendDirection(direction);
        }

        public void Destroy() => _input.Disable();

        private void SendDirection(Vector3 direction)
        {
            DirectionMessage message = new()
            {
                Direction = direction
            };

            NetworkClient.Send(message);
        }

        private Vector3 GetDirection() => _input.Player.Movement.ReadValue<Vector3>();
    }
}