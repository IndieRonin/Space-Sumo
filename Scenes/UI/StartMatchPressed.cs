using EventCallback;
using Godot;
using System;

public partial class StartMatchPressed : Button
{
	[Export] Control menu;
	bool loadedGame = false; //Keeps track if the game has already been loaded for in case something else uses the blackout screen to make sure we do not load the game again
							 // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ButtonUp += () => OnButtonUp();
		WaitforBlackoutEvent.RegisterListener(OnWaitforBlackoutEvent);//Listen for the blackout screen to be done hiding the screeen
	}

	private void OnButtonUp()
	{
		//Call the start of the blackout screen
		ShowBlackoutEvent sbe = new();
		sbe.FireEvent();
	}

	private void OnWaitforBlackoutEvent(WaitforBlackoutEvent wfbe)
	{
		if (loadedGame) return;//If hte game has already been loaded we return out of the function
							   //Load all the assest for a new game ==============================================================================================
		StartGameEvent sme = new();
		sme.FireEvent();
		menu.Visible = false;
		Disabled = true;
		//=================================================================================================================================
		ContinueBlackoutEvent cbe = new();//After everything is loaded behind the black screen continue blackout so blackout screen goes away
		cbe.FireEvent();
		loadedGame = true; //Set the loaded game tu true as not to load everything again if the blacout screen is used again
	}
}
