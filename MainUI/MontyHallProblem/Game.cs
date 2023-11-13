using ConsoleUI.MontyHallProblem.Interfaces;

namespace MontyHallProblem
{

	public class Game : IGameForPlayer
	{
		public string ErrorMessage { get; private set; }
		public const string OK = "OK";

		public DoorNumber OpenedDoor { get; private set; }

		public DoorNumber SelectedDoor { get; set; }

		public bool IsOver { get; private set; }

		public Game()
        {
            WinnerDoor = (DoorNumber)new Random().Next(1, 4);
			OpenedDoor = (DoorNumber)0;
			IsOver = false;
			SelectedDoor = (DoorNumber)0;
			ErrorMessage = OK;
		}

		public DoorNumber WinnerDoor { get; private set; }

		public bool WinnerDoorIsSelected => SelectedDoor == WinnerDoor;

		public void OpenDoor(DoorNumber doorToOpen)
		{
			if (OpenedDoor == doorToOpen)
				ErrorMessage = "Can not open a door that is already opened";
			else if (SelectedDoor == 0)
				ErrorMessage = "To open a door, first select a door. Opening a door can be done from the second step of the game";
			else
			{
				ErrorMessage = OK;
				IsOver = (OpenedDoor != doorToOpen && OpenedDoor != 0);
				OpenedDoor = doorToOpen;
			}
		}

		public void ChangeSelectedDoor()
		{
			if (IsOver || OpenedDoor == 0)
			{
				ErrorMessage = "Can not change doors before opening a door or after game is over";
			}
			else
			{
				ErrorMessage = OK;
				var availableOptions = DoorNumber.All();
				availableOptions.Remove(OpenedDoor);
				availableOptions.Remove(SelectedDoor);
				SelectedDoor = availableOptions.First();
			}
		}

		public static GameSummary Play(bool playerChoosesToChangeDoor)
		{
			GameSummary gameSummary;
			var game = new Game();

			var player = new Player(game);
			var monty = new MontyHall(game);

			gameSummary.WinnerDoor = game.WinnerDoor;
			gameSummary.SelectedDoor = player.SelectsDoor();
			gameSummary.FirstDoorOpened = monty.OpensADoorWithAGoatBehind();

			if (playerChoosesToChangeDoor)
			{
				player.ChangeSelectedDoor();
			}

			gameSummary.SecondDoorOpened = monty.OpensRemainingUnselectedDoor();
			gameSummary.PlayerWins = game.WinnerDoorIsSelected;
			return gameSummary;
		}

	}
}