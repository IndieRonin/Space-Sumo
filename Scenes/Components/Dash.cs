using EventCallback;
using Godot;
using System;
using System.Runtime.ExceptionServices;

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

		public override void _Ready()
		{
			dashTimer.Timeout += () => OnDashTimeout();
		}
		public void GetInput()
		{
			if (!canDash) return; //If the get input has changed the value of candash then run the code
			Vector2 mousePosition = body2D.GetGlobalMousePosition();
			Vector2 dashDir = mousePosition - body2D.GlobalPosition;
			move?.ModAccel(dashDir.Normalized() * dashSpeed);
			dashTimer.Start();
			canDash = false;
			StartDashBarEvent sdbe = new();

			sdbe.DashTimer = dashTimer;
			sdbe.FireEvent();
		}

		private void OnDashTimeout()
		{
			canDash = true;
		}
	}
}

