using Leopotam.Ecs;
using Mirror;

namespace Project.Gameplay.Client
{
    public class PutMessageSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private Gameplay _input;

        public void Init()
        {
            _input = new();
            _input.Enable();

            _input.Player.Put.performed += context => Send();
        }

        public void Destroy()
        {
            _input.Player.Put.performed -= context => Send();
            _input.Disable();
        }

        private void Send() => NetworkClient.Send(new PutMessage());
    }
}