// See https://aka.ms/new-console-template for more information

using ConsoleUI.MontyHallProblem;

Console.WriteLine("MontyHall problem - or Three doors dilemma");

const int REPETITIONS = 10;

int playerWins = PlayGame(false);
Console.WriteLine($"NOT CHANGING - Player wins: {playerWins}/{REPETITIONS} = {((double)playerWins / REPETITIONS) * 100}%");

playerWins = PlayGame(true);
Console.WriteLine($"    CHANGING - Player wins: {playerWins}/{REPETITIONS} = {((double)playerWins / REPETITIONS) * 100}%");




static int PlayGame(bool changeSelection)
{
	var playerWins = 0;

	for (int i = 0; i < REPETITIONS; i++)
	{
		var game = new Game();
		var player = new Player(game);
		var host = new Host(game);

		player.SelectRandomDoor();
		host.OpenDoorNonWinnerAvailable();

		if (changeSelection) player.ChangeSelection();

		player.OpenSelection();
		if (host.HasPlayerWon) playerWins++;
	}

	return playerWins;
}