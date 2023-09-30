using Godot;
using System;

namespace Components
{
	[GlobalClass]
	public partial class Dash : Node
	{
		[Export] public float dashSpeed = 400f;
		[Export] CharacterBody2D body2D;
		[Export] Movement move;
		[Export] Timer dashTimer = null;
		bool canDash = true;
		public override void _PhysicsProcess(double delta)
		{
			if (!canDash) return; //If the get input has changed the value of candash then run the code
			Vector2 mousePosition = body2D.GetGlobalMousePosition();
			if (!dashTimer.IsStopped()) return;
			Vector2 dashDir = mousePosition - body2D.GlobalPosition;
			move?.ModAccel(dashDir.Normalized() * dashSpeed);
			dashTimer.Start();
			canDash = false;
		}

		public void GetInput(bool _canDash)
		{
			canDash = _canDash;
		}
	}
}

