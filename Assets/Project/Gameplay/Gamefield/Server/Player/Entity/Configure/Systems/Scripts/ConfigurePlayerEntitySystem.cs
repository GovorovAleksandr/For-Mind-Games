using Leopotam.Ecs;
using Mirror;
using Project.Gameplay.Client;
using System.Linq;
using UnityEngine;

namespace Project.Gameplay.Server
{
    public class ConfigurePlayerEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<NotConfiguredPlayerEntityComponent, PlayerIdComponent> _filter;

        private readonly ServerConfig _config;

        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var idComponent = ref _filter.Get2(i);
                var id = idComponent.Id;

                var connections = NetworkServer.connections;
                var connectionKeys = NetworkServer.connections.Keys;

                if (!connectionKeys.Contains(id)) continue;
                if (connections[id].identity == null) continue;

                ref var entity = ref _filter.GetEntity(i);

                ConfigurePlayerEntity(entity, id);
            }
        }

        private void ConfigurePlayerEntity(EcsEntity entity, int id)
        {
            var playerConnection = NetworkServer.connections[id];
            var player = playerConnection.identity.gameObject;

            ref var movableComponent = ref entity.Get<MovableComponent>();
            int speed = _config.PlayersSpeed;
            movableComponent.Speed = speed;
            var playerRigidbody = player.GetComponent<Rigidbody>();
            movableComponent.Rigidbody = playerRigidbody;

            var playerTransform = player.transform;
            entity.Get<ModelComponent>().ModelTransform = playerTransform;

            entity.Get<DirectionComponent>();

            entity.Del<NotConfiguredPlayerEntityComponent>();
        }
    }
}