using Leopotam.Ecs;

namespace Project.Gameplay.Server
{
    public class CreatePlayerEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ClientConnectedEvent> _filter;
        private readonly EcsWorld _world;

        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var clientConnectedEvent = ref _filter.Get1(i);
                var id = clientConnectedEvent.ConnectionId;

                SendToWorldPlayerEntity(id);

                entity.Del<ClientConnectedEvent>();
            }
        }

        private void SendToWorldPlayerEntity(int id)
        {
            var entity = _world.NewEntity();
            entity.Get<PlayerIdComponent>().Id = id;
            entity.Get<NotConfiguredPlayerEntityComponent>();
        }
    }
}