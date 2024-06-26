namespace Project.Gameplay.Server
{
    public struct ClientConnectedEvent
    {
        public int ConnectionId;

        public ClientConnectedEvent(int connectionId) => ConnectionId = connectionId;
    }
}