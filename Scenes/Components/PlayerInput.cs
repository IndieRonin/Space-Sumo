using Godot;
using System;

namespace Components
{
	[GlobalClass]
	public partial class PlayerInput : ExternalInput
	{
		[Export] Movement move;
		[Export] Dash dash;
		float x, y;
		Vector2 target;
		bool canDash = true;

		public override void _Input(InputEvent @event)
		{
			x = 0; y = 0;
			target = (GetParent() as Node2D).GetGlobalMousePosition();

			if (Input.IsKeyPressed(Key.W))
			{
				y = -1;
			}
			if (Input.IsKeyPressed(Key.S))
			{
				y = 1;
			}
			if (Input.IsKeyPressed(Key.A))
			{
				x = -1;
			}
			if (Input.IsKeyPressed(Key.D))
			{
				x = 1;
			}
			if (Input.IsMouseButtonPressed(MouseButton.Left))
			{
				canDash = true;
			}
		}
		public override void _Process(double delta)
		{
			move?.GetInput(x, y, target);
			dash?.GetInput(canDash);
			canDash = false;
		}
	}
}
