using Mirror;
using System;
using UnityEngine;

namespace Project.Reusable.Server
{
    public abstract class ExecuteInServer : MonoBehaviour
    {
        [SerializeField] private DestroyMode _destroyMode;

        private enum DestroyMode
        {
            Component,
            GameObject
        }

        private void Awake() => NetworkClient.OnConnectedEvent += HandleClientStart;

        private void HandleClientStart()
        {
            if (NetworkServer.active) return;

            switch (_destroyMode)
            {
                case DestroyMode.Component:
                    Destroy(this);
                    break;
                case DestroyMode.GameObject:
                    Destroy(gameObject);
                    break;
                default:
                    throw new Exception("Select Destroy mode");
            }
        }

        private void OnDestroy() => NetworkClient.OnConnectedEvent -= HandleClientStart;
    }
}