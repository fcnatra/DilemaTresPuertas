// See https://aka.ms/new-console-template for more information
using MontyHallProblem;

Console.WriteLine("MontyHall problem - or Three doors dilemma");

const int REPETITIONS = 500;


int _playerWins = 0;
Console.WriteLine($"{REPETITIONS} Repetitions with player keeping the selected door: ");
var gameSummary = Play(false);
Console.WriteLine($"Player wins {_playerWins}/{REPETITIONS} times ({_playerWins * 100 / REPETITIONS}%)");



_playerWins = 0;
Console.WriteLine($"\n{REPETITIONS} Repetitions with player CHANGIND doors: ");
gameSummary = Play(true);
Console.WriteLine($"Player wins {_playerWins}/{REPETITIONS} times ({_playerWins * 100 / REPETITIONS}%)");


Console.WriteLine("END.");



GameSummary Play(bool changeDoors)
{
	var gameSummary = new GameSummary();
	for (int i = 0; i < REPETITIONS; i++)
	{
		gameSummary = Game.Play(changeDoors);

		//PrintGameSummary(gameSummary);

		if (gameSummary.PlayerWins) _playerWins++;
	}

	return gameSummary;
}

void PrintGameSummary(GameSummary gameSummary)
{
	Console.WriteLine("\tW{0} P{1} M{2} M{3} {4}",
		gameSummary.WinnerDoor,
		gameSummary.SelectedDoor,
		gameSummary.FirstDoorOpened,
		gameSummary.SecondDoorOpened,
		gameSummary.PlayerWins ? "PLAYER WINS ···················" : "");
}
