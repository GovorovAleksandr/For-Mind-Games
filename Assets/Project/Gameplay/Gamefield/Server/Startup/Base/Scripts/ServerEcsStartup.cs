using Leopotam.Ecs;
using Mirror;
using Project.Gameplay.Servw;
using UnityEngine;
using Voody.UniLeo;

namespace Project.Gameplay.Server
{
    public class ServerEcsStartup : NetworkBehaviour
    {
        [SerializeField] private ServerConfig _config;
        [SerializeField] private ReferenceZoneConfig _referenceZoneConfig;
        [SerializeField] private CubeZoneConfig _cubeZoneConfig;

        private EcsWorld _world;
        private EcsSystems _systems;

        public override void OnStartServer()
        {
            _world = new();
            _systems = new(_world);

            _systems.ConvertScene();

            AddInjections();
            AddOneFrames();
            AddSystems();

            _systems.Init();
        }

        public override void OnStopServer() => TryDestroy();

        private void AddInjections()
        {
            _systems.
                Inject(_config).
                Inject(_referenceZoneConfig).
                Inject(_cubeZoneConfig);
        }

        private void AddSystems()
        {
            _systems.
                Add(new FactorySystem()).
                Add(new ReferenceZoneSystem()).
                Add(new CubeZoneSystem()).
                Add(new ClientConnectedSystem()).
                Add(new CreatePlayerEntitySystem()).
                Add(new ConfigurePlayerEntitySystem()).
                Add(new PlayerEntityDestroySystem()).
                Add(new DisconnectSystem()).
                Add(new MovementSystem()).
                Add(new NearestCubeSystem()).
                Add(new TakeSystem()).
                Add(new PutSystem());
        }

        private void AddOneFrames()
        {

        }

        private void Update() => TryRun();

        private void TryRun()
        {
            if (_systems == null) return;
            _systems.Run();
        }

        private void TryDestroy()
        {
            if (_systems == null) return;

            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}