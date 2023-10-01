using Godot;
using System;
using EventCallback;

public partial class BadBox : RigidBody2D
{
	[Export] int deathHit = 2;
	[Export] float speed = 20;
	Node2D target = null; //The target the 
						  // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BounceBackEvent.RegisterListener(OnBounceBackEvent);

		//Get the target when th box is spawned in
		BoxGetTargetEvent bgte = new(); //Create message to send
		bgte.FireEvent();//Send Message
		target = (Node2D)InstanceFromId(bgte.TargetID); //Set the target to the instanced id object

		if (target != null)
		{
			Vector2 dir = target.GlobalPosition - GlobalPosition; //Get the direction to move in
			dir.Normalized();//Normalize the direction
			ApplyImpulse(dir * speed * .005f);
		}
	}

	private void OnBounceBackEvent(BounceBackEvent bbe)
	{
		if (bbe.TargetID != GetInstanceId()) return;
		ApplyImpulse(bbe.BounceForce / Mass);

		deathHit--;//Decrease the hits it can take by one

		if (deathHit <= 0)
		{
			DeathEvent de = new()
			{
				AttackerID = bbe.AttackerID,
				TargetID = bbe.TargetID
			};
			de.FireEvent();

			PlaySFXEvent psfxe = new();
			psfxe.sfx = SFXList.explosion;
			psfxe.FireEvent();
			BounceBackEvent.UnregisterListener(OnBounceBackEvent);
			CallDeferred("queue_free");
		}
	}

	public override void _ExitTree()
	{

	}

}
