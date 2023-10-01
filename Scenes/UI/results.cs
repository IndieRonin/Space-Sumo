using Godot;
using System;
using EventCallback;

public partial class results : Label
{

	[Export] Button exit = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		exit.ButtonUp += () => OnExitButtonUp();

		ShowResultsEvent.RegisterListener(OnShowResultsEvent);
	}

	private void OnExitButtonUp()
	{
		GetTree().Quit();
	}

	private void OnShowResultsEvent(ShowResultsEvent sre)
	{
		if (sre.win)
		{
			exit.Text = "Get out ->";
			Text = "Congrats! Now you just have to pay back the debt of the ship you totaled.";
		}
		else
		{
			if (sre.wentOutOfRift)
			{
				exit.Text = "Now get out ->";
				Text = "Uhg! We regrew you from what we could scrape off the walls and told you to stay in the rift. Now you just have to pay back the debt of the ship you totaled and your expensive resurrection!";
			}
			else
			{
				exit.Text = "Go to heaven ->";
				Text = "The damage you brought to the company was crippling. As the rift closed, all the unstable boxes started to explode. The rift exit was inside the main headquarters of the company.";
			}
		}
	}
}
