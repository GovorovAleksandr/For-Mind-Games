using Leopotam.Ecs;
using UnityEngine;

namespace Project.Gameplay.Server
{
    public class PlayerEntityDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ClientDisconnectEvent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var clientDisconnectedEvent = ref _filter.Get1(i);

                entity.Destroy();
                Debug.Log("Deasgd");
            }
        }
    }
}