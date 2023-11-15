namespace ConsoleUI.MontyHallProblem
{
	public class Player
	{
		private Game _game;

		public Player(Game game)
		{
			this._game = game;
		}

		public int ChangeSelection()
		{
			var openedDoor = _game.GetAvailableDoors();
			_game.SelectedDoor = openedDoor.First();
			return _game.SelectedDoor;
		}

		public int OpenSelection()
		{
			_game.OpenSelection();
			return _game.SelectedDoor;
		}

		public int SelectRandomDoor()
		{
			return SelectDoor(() => new Random().Next(1, 4));
		}

		public int SelectDoor(Func<int> selectionMethod)
		{
			_game.SelectedDoor = selectionMethod();
			return _game.SelectedDoor;
		}
	}
}