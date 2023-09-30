using Godot;
using System;

namespace Components
{
	[GlobalClass]
	public partial class Movement : Node
	{
		[Export] CharacterBody2D body2D;
		[Export] public float speed = 500f;
		[Export] public float friction = .8f; // 0.97f
		[Export] public float move_radius = 25f;
		private Vector2 AccelMod = Vector2.One;
		private Vector2 velocity = Vector2.Zero;
		private Vector2 acceleration = Vector2.Zero;
		float x = 0, y = 0;
		Vector2 target;
		public override void _PhysicsProcess(double delta)
		{
			body2D.LookAt(target);
			acceleration = new Vector2(x, y) * (float)delta;
			velocity += acceleration * speed + AccelMod;
			MoveAndBounceOffTheAsteroids(velocity, (float)delta);
			velocity = velocity.Clamp(new Vector2(-900, -900), new Vector2(900, 900));
			velocity *= friction;
			acceleration = Vector2.One;
			AccelMod = Vector2.One;
			// x = 0; y = 0;
		}

		private void MoveAndBounceOffTheAsteroids(Vector2 velocity, float delta)
		{
			KinematicCollision2D collision = body2D.MoveAndCollide(velocity * delta, false);
			if (collision != null)
			{
				this.velocity = this.velocity.Bounce(collision.GetNormal()) * 0.5f;
				Vector2 reflect = collision.GetRemainder().Bounce(collision.GetNormal());
				body2D.MoveAndCollide(reflect);

				if (collision.GetCollider().HasMethod("ApplyCentralImpulse"))
				{
					collision.GetCollider().Call("ApplyCentralImpulse", -collision.GetNormal() * 200);
				}
			}
		}

		public void ModAccel(Vector2 _accellMod)
		{
			AccelMod = _accellMod;
		}
		public void GetInput(float _x, float _y, Vector2 _target)
		{
			x = _x;
			y = _y;
			target = _target;
		}
	}
}
