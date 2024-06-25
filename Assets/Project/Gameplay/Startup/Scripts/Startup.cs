using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Project.Gameplay
{
    internal class Startup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
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
                Add(new PlayerInputSystem()).
                Add(new MovementSystem());
        }

        private void AddOneFrames()
        {

        }

        private void Update() => _systems.Run();

        private void OnDestroy()
        {
            if (_systems == null) return;

            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}