using Leopotam.Ecs;
using Mirror;
using Project.Gameplay.Server;
using Voody.UniLeo;

namespace Project.Gameplay.Client
{
    public class ClientEcsStartup : NetworkBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        public override void OnStartClient()
        {
            _world = new();
            _systems = new(_world);

            _systems.ConvertScene();

            AddInjections();
            AddOneFrames();

            AddSystems();

            _systems.Init();
        }

        private void AddInjections()
        {
            
        }

        private void AddSystems()
        {
            _systems.
                Add(new SendDirectionMessageSystem()).
                Add(new DestroyCubesSystem()).
                Add(new TakeMessageSystem()).
                Add(new PutMessageSystem());
        }

        private void AddOneFrames()
        {

        }
        public override void OnStopClient() => TryDestroy();
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