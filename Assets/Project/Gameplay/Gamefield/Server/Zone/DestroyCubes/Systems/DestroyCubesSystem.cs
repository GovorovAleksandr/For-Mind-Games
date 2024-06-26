using Leopotam.Ecs;
using UnityEngine;

namespace Project.Gameplay.Server
{
    public class DestroyCubesSystem : IEcsDestroySystem
    {
        private readonly EcsFilter<CubeTag> _filter;

        public void Destroy()
        {
            foreach(var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                var gameObject = component.Transform.gameObject;
                Object.Destroy(gameObject);
            }
        }
    }
}