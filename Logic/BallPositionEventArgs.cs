namespace Logic
{
    public abstract class BallPositionEventArgs : EventArgs
    {
        public abstract int Index { get; }
        public abstract ImmutableVector2 Position { get; }
    }
}
