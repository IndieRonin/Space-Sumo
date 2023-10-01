using Godot;
using System;
namespace EventCallback
{
    public class BounceBackEvent : Event<BounceBackEvent>
    {
        //The actor being bounced
        public ulong TargetID;
        //The actor being bounced
        public ulong AttackerID;
        //The force of the bounce back
        public Vector2 BounceForce;
    }
}