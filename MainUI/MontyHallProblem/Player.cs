// See https://aka.ms/new-console-template for more information
using ConsoleUI.MontyHallProblem.Interfaces;

namespace MontyHallProblem
{
	public class Player
	{
		private IGameForPlayer game;

		public Player(IGameForPlayer game)
		{
			this.game = game;
		}

		public void ChangeSelectedDoor()
		{
			game.ChangeSelectedDoor();
		}

		public int SelectsDoor()
		{
			var randomDoor = (DoorNumber)new Random().Next(1, 4);
			game.SelectedDoor = randomDoor;

			return randomDoor;
		}
	}
}