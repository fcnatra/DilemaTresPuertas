using Xunit;
using ConsoleUI.MontyHallProblem;
using System.Collections.Generic;
using Xunit.Sdk;

namespace TestMontyHallGame
{
	public class TestGame
	{
		[Fact]
		public void GivenNewGame_ThreeDoorsAreClosed()
		{
			var game = new Game();

			// ACT
			List<int> availableDoors = game.GetAvailableDoors();

			var expectedAvailableDoors = new List<int> { 1, 2, 3 };
			Assert.Equal(3, availableDoors.Count);
			Assert.All(availableDoors, (x) => expectedAvailableDoors.Contains(x));
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void GivenNewGame_Player_CanSelectDoors_1_to_3(int doorNumber)
		{
			var game = new Game();
			var player = new Player(game);

			// ACT
			var selectedDoor = player.SelectDoor(() => doorNumber);

			Assert.Equal(selectedDoor, game.SelectedDoor);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(9)]
		[InlineData(-6)]
		public void GivenNewGame_Player_CanNotSelectDoors_OutOf1_to_3(int incorrectDoorNumber)
		{
			var game = new Game();
			var player = new Player(game);

			// ACT & ASSERT
			Assert.Throws<ArgumentOutOfRangeException>(() => player.SelectDoor(() => incorrectDoorNumber));
		}

		[Fact]
		public void GivenPlayerSelectedADoor_HostCanOpenDoor_NonWinnerAvailable()
		{
			var game = new Game();
			var host = new Host(game);
			var player = new Player(game);

			player.SelectDoor(() => 1);

			// ACT
			var openedDoor = host.OpenDoorNonWinnerAvailable();

			Assert.NotEqual(host.GetWinnerDoor(), openedDoor);
			Assert.NotEqual(game.SelectedDoor, openedDoor);
			Assert.Equal(game.OpenedDoor, openedDoor);
		}

		[Fact]
		public void GiveNewGame_HostCanNotOpenDoor()
		{
			var game = new Game();
			var host = new Host(game);

			// ACT & ASSERT
			Assert.Throws<InvalidOperationException>(() => host.OpenDoorNonWinnerAvailable());
		}

		[Fact]
		public void GivenDoorOpened_PlayerCanChangeItsSelection()
		{
			var game = new Game();
			var host = new Host(game);
			var player = new Player(game);

			player.SelectDoor(() => 1);
			var openedDoor = host.OpenDoorNonWinnerAvailable();
			var doorToChange = openedDoor == 2 ? 3 : 2;

			// ACT
			player.ChangeSelection();
			var newSelection = game.SelectedDoor;

			Assert.Equal(doorToChange, newSelection);
		}

		[Fact]
		public void GivenDoorOpened_PlayerCanChangeItsSelectionMoreThanOnce()
		{
			var game = new Game();
			var host = new Host(game);
			var player = new Player(game);

			player.SelectDoor(() => 1);
			var openedDoor = host.OpenDoorNonWinnerAvailable();

			// ACT
			player.ChangeSelection();
			var doorToChange = game.GetAvailableDoors().First();
			player.ChangeSelection();
			
			var newSelection = game.SelectedDoor;
			Assert.Equal(doorToChange, newSelection);
		}

		[Fact]
		public void GivenDoorOpened_PlayerDecidesToOpenItsSelection()
		{
			var game = new Game();
			var host = new Host(game);
			var player = new Player(game);

			int selectedDoor = player.SelectDoor(() => 1);
			host.OpenDoorNonWinnerAvailable();

			// ACT
			var openedDoor = player.OpenSelection();

			Assert.Equal(selectedDoor, openedDoor);
		}

		[Fact]
		public void GivenSelectedDoorOpened_GameEnds()
		{
			var game = new Game();
			var host = new Host(game);
			var player = new Player(game);

			int selectedDoor = player.SelectDoor(() => 1);
			host.OpenDoorNonWinnerAvailable();

			// ACT
			var openedDoor = player.OpenSelection();

			Assert.True(game.IsOver);
		}

		[Fact]
		public void GivenGameOver_IfPlayerSelectedWinnerDoor_PlayerWins()
		{
			var game = new Game();
			var host = new Host(game);
			var player = new Player(game);

			var winnerDoor = host.GetWinnerDoor();

			player.SelectDoor(() => winnerDoor);
			host.OpenDoorNonWinnerAvailable();

			// ACT & ASSERT
			player.OpenSelection();

			Assert.True(host.HasPlayerWon);
		}

		[Fact]
		public void GivenGameOver_PlayerCanChangeSelection()
		{
			var game = new Game();
			var host = new Host(game);
			var player = new Player(game);

			player.SelectDoor(() => 1);
			host.OpenDoorNonWinnerAvailable();
			player.OpenSelection();

			// ACT & ASSERT
			Assert.Throws<InvalidOperationException>(() => player.ChangeSelection());
		}
	}
}