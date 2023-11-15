using System.Xml.Linq;

namespace ConsoleUI.MontyHallProblem
{
	public class Game
	{
		private int _selectedDoor;
		private readonly List<int> _gameDoors = new List<int>() { 1, 2, 3 };
		private readonly int _winnerDoor;

		public int OpenedDoor { get; set; }

		public bool IsOver { get; private set; }

		public Game()
        {
			_winnerDoor = new Random().Next(1, 4);
        }

        public int SelectedDoor
		{ 
			get => _selectedDoor; 
			set
			{ 
				if (!_gameDoors.Contains(value))
					throw new ArgumentOutOfRangeException(nameof(value), $"{value} Is not a valid door. Door must be in ({_gameDoors.ToString()}).");

				_selectedDoor = value;
			}
		}

		public Host HostOfTheGame { get; internal set; }

		public List<int> GetAvailableDoors()
		{
			List<int> availableDoors;
			if (this.IsOver)
				availableDoors = new List<int>();
			else
			{
				availableDoors = _gameDoors.ToList();
				availableDoors.Remove(SelectedDoor);
				availableDoors.Remove(OpenedDoor);
			}
			return availableDoors;
		}

		public int GetDoorNonWinnerToOffer()
		{
			if (SelectedDoor == 0)
				throw new InvalidOperationException("Player must select a door before opening a \"discard\" door");

			List<int> availableDoors;
			if (this.IsOver)
				availableDoors = new List<int>();
			else
			{
				availableDoors = _gameDoors.ToList();
				availableDoors.Remove(SelectedDoor);
				availableDoors.Remove(OpenedDoor);
			}

			availableDoors.Remove(_winnerDoor);

			return availableDoors.First();
		}

		internal void OpenSelection()
		{
			this.IsOver = true;
		}

		internal int GetWinnerDoor(Host host)
		{
			if (host == HostOfTheGame)
				return _winnerDoor;
			else
				throw new UnauthorizedAccessException();
		}
	}
}