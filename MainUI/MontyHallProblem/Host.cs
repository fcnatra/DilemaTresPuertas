namespace ConsoleUI.MontyHallProblem
{
	public class Host
	{
		private Game _game;

		public Host(Game game)
		{
			this._game = game;
		}

		public bool HasPlayerWon { get => _game.IsOver && _game.SelectedDoor == GetWinnerDoor(); }

		public int GetWinnerDoor()
		{
			_game.HostOfTheGame = this;
			return _game.GetWinnerDoor(this);
		}

		public int OpenDoorNonWinnerAvailable()
		{
			var doorToOpen = _game.GetDoorNonWinnerToOffer();
			_game.OpenedDoor = doorToOpen;

			return doorToOpen;
		}
	}
}