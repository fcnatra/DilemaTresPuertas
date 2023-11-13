// See https://aka.ms/new-console-template for more information

namespace MontyHallProblem
{
	public  class MontyHall
	{
		private Game game;

		public MontyHall(Game game)
		{
			this.game = game;
		}

		public void ChangeSelectedDoor()
		{
			throw new NotImplementedException();
		}

		public int OpensADoorWithAGoatBehind()
		{
			var availableOptions = DoorNumber.All();
			availableOptions.Remove(game.WinnerDoor);
			availableOptions.Remove(game.SelectedDoor);

			int randomRemainingDoor = new Random().Next(0, availableOptions.Count);
			var doorToOpen = availableOptions[randomRemainingDoor];

			game.OpenDoor(doorToOpen);

			return doorToOpen;
		}

		public int OpensRemainingUnselectedDoor()
		{
			var availableOptions = DoorNumber.All();
			availableOptions.Remove(game.SelectedDoor);
			availableOptions.Remove(game.OpenedDoor);

			var doorToOpen = availableOptions.First();
			game.OpenDoor(doorToOpen);

			return doorToOpen;
		}
	}
}