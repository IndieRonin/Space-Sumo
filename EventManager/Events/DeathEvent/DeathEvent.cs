using Godot;
using System;
namespace EventCallback
{
    public class DeathEvent : Event<DeathEvent>
    {
        //The actor doing the attacking
        public ulong AttackerID; //To keep trackof thte units kill streak maybe
        //The target that is hit
        public ulong TargetID;
    }
}