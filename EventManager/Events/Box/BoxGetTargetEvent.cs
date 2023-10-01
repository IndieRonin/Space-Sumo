using Godot;
using System;
namespace EventCallback
{
    public class BoxGetTargetEvent : Event<BoxGetTargetEvent>
    {
        //The actor doing the attacking
        public ulong TargetID; //The target for the box to aim for
    }
}