using ConsoleUI.MontyHallProblem.Interfaces;
using MontyHallProblem;

namespace TestMontyHallGame
{
	public class TestGame
	{
		[Fact]
		public void NewGame_IsInitialized()
		{
			var game = new Game();

			Assert.Equal(Game.OK, game.ErrorMessage);
			Assert.Equal(0, game.SelectedDoor);
			Assert.Equal(0, game.OpenedDoor);
			Assert.False(game.WinnerDoorIsSelected);
			Assert.True(game.WinnerDoor > 0);
			Assert.False(game.IsOver);
		}

		[Fact]
		public void SelectingDoor_BeforeOpenDoor_GameContinues()
		{
			var game = new Game();

			game.SelectedDoor = (DoorNumber)2;

			Assert.Equal(Game.OK, game.ErrorMessage);
			Assert.Equal(0, game.OpenedDoor);
			Assert.False(game.IsOver);
		}

		[Fact]
		public void GameEnds_WhenOpening_SecondDoor()
		{
			var game = new Game();

			game.SelectedDoor = (DoorNumber)1;
			game.OpenDoor((DoorNumber)2);
			game.OpenDoor((DoorNumber)3);

			Assert.Equal(Game.OK, game.ErrorMessage);
			Assert.True(game.IsOver);
		}

		[Fact]
		public void OpeningSameDoor_SecondTime_ReturnsError()
		{
			var game = new Game();

			game.SelectedDoor = (DoorNumber)1;
			game.OpenDoor((DoorNumber)2);
			game.OpenDoor((DoorNumber)2);

			Assert.NotEqual(Game.OK, game.ErrorMessage);
			Assert.False(game.IsOver);
		}

		[Fact]
		public void OpeningDoor_BeforeSelectingDoor_ResultsInError()
		{
			var game = new Game();

			game.OpenDoor((DoorNumber)2);

			Assert.NotEqual(Game.OK, game.ErrorMessage);
			Assert.False(game.IsOver);
		}

		[Fact]
		public void CanChangeSelectedDoor_AfterOpeningOne()
		{
			var game = new Game();

			var notWinnerDoor = (DoorNumber)(game.WinnerDoor == 1 ? 2 : 1);
			game.SelectedDoor = (DoorNumber)3;
			game.OpenDoor(notWinnerDoor);
			game.ChangeSelectedDoor();

			Assert.Equal(Game.OK, game.ErrorMessage);
			Assert.False(game.IsOver);
		}

		[Fact]
		public void CanNotChangeSelectedDoor_IfGameIsOver()
		{
			var game = new Game();
			
			var notWinnerDoor = (DoorNumber)(game.WinnerDoor == 1 ? 2 : 1);

			game.SelectedDoor = (DoorNumber)3;
			game.OpenDoor(notWinnerDoor);
			game.OpenDoor(game.WinnerDoor);

			var gameIsOver = game.IsOver;
			game.ChangeSelectedDoor();

			Assert.True(gameIsOver);
			Assert.NotEqual(Game.OK, game.ErrorMessage);
		}

		[Fact]
		public void CanNotChangeSelectedDoor_BeforeOneIsOpened()
		{
			var game = new Game();

			var notWinnerDoor = (DoorNumber)(game.WinnerDoor == 1 ? 2 : 1);

			game.SelectedDoor = notWinnerDoor;
			game.ChangeSelectedDoor();

			Assert.NotEqual(Game.OK, game.ErrorMessage);
		}
		[Theory]
		[InlineData(-1)]
		[InlineData(4)]
		public void CanNotSelect_DoorNumber_OutOfBonds(int number)
		{
			var game = new Game();

			Assert.Throws<ArgumentOutOfRangeException>(() => game.SelectedDoor = (DoorNumber)number);
		}
	}
}