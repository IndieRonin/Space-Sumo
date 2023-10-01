using Godot;
using System;
namespace EventCallback
{
    public class GetRiftTimeEvent : Event<GetRiftTimeEvent>
    {//The total time for hte round
        public float WaitTime;
        //Gets the time of the rift timer
        public float timeLeft;
    }
}