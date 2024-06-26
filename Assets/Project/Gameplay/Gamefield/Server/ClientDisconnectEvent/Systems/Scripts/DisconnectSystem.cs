using Leopotam.Ecs;
using Mirror;

namespace Project.Gameplay.Server
{
    public class DisconnectSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<PlayerIdComponent> _filter;

        public void Init() => NetworkServer.OnDisconnectedEvent += NotifyWorld;
        public void Destroy() => NetworkServer.OnDisconnectedEvent -= NotifyWorld;

        private void NotifyWorld(NetworkConnection connection)
        {
            foreach(var i in _filter)
            {
                ref var idComponent = ref _filter.Get1(i);
                var id = idComponent.Id;

                if (connection.connectionId != id) continue;

                ref var entity = ref _filter.GetEntity(i);
                entity.Get<ClientDisconnectEvent>();
            }
        }
    }
}