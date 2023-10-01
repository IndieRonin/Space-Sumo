using Godot;
using System;
namespace EventCallback
{
    public class StartDashBarEvent : Event<StartDashBarEvent>
    {
        //The timer for the dash
        public Timer DashTimer;

    }
}