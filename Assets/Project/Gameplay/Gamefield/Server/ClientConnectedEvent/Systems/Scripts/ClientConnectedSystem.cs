using Leopotam.Ecs;
using Mirror;
using Project.Reusable;

namespace Project.Gameplay.Server
{
    public class ClientConnectedSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsWorld _world;

        public void Init() => NetworkServer.OnConnectedEvent += NotifyWorld;
        public void Destroy() => NetworkServer.OnConnectedEvent -= NotifyWorld;
        private void NotifyWorld(NetworkConnection connection) =>
            _world.SendMessage(new ClientConnectedEvent(connection.connectionId));
    }
}