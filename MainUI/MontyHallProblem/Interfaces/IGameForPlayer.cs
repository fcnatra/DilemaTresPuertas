using MontyHallProblem;

namespace ConsoleUI.MontyHallProblem.Interfaces
{
    public interface IGameForPlayer
    {
		DoorNumber SelectedDoor { get; set; }
        bool IsOver { get; }
        string ErrorMessage { get; }
		void ChangeSelectedDoor();
	}
}