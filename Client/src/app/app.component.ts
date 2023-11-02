import { Component, OnInit, Input, Renderer2  } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  logEntries: string[] = [];
  gameIsOver: boolean = false;

  playerOneShips: { name: string, direction: string, size: number, locationX: number, locationY: number, health: number }[] = [];
  playerTwoShips: { name: string, direction: string, size: number, locationX: number, locationY: number, health: number }[] = [];


  playerOneHits: number[] = [];
  playerOneMisses: number[] = [];
  playerTwoHits: number[] = [];
  playerTwoMisses: number[] = [];

  isPlayerOneTurn: boolean = true;
  currentPlayer: string = 'playerOne';

  constructor(private http: HttpClient, private renderer: Renderer2) { }

  ngOnInit() {
    // Make an HTTP request to fetch ship placement data for PlayerOne
    this.http.get('http://localhost:5000/api/battle_controller/player_one').subscribe((data: any) => {
      // Initialize ships for Player One with health
      this.playerOneShips = data.map((ship: any) => ({ ...ship, health: ship.size }));
    });

    this.http.get('http://localhost:5000/api/battle_controller/player_two').subscribe((data: any) => {
      // Initialize ships for Player Two with health
      this.playerTwoShips = data.map((ship: any) => ({ ...ship, health: ship.size }));
    });
  }

  getShipsPlayerOne(x: number, y: number): string {
    if (this.playerOneHits.includes(x * 10 + y)) {
      return 'hit';
    } else if (this.playerOneMisses.includes(x * 10 + y)) {
      return 'miss';
    } else {
      for (const ship of this.playerTwoShips) {
        // console.log(`LocationX ${ship.locationX}, LocationY ${ship.locationY}, Ship: ${ship.name}`);
        if (ship.direction === 'East') {
          if (
            x >= ship.locationX &&
            x < ship.locationX + ship.size &&
            y === ship.locationY
          ) {
            return ship.name.toLowerCase();
          }
        } else if (ship.direction === 'West') {
          if (
            x <= ship.locationX &&
            x > ship.locationX - ship.size &&
            y === ship.locationY
          ) {
            return ship.name.toLowerCase();
          }
        } else if (ship.direction === 'South') {
          if (
            y <= ship.locationY &&
            y > ship.locationY - ship.size &&
            x === ship.locationX
          ) {
            return ship.name.toLowerCase();
          }
        } else if (ship.direction === 'North') {
          if (
            y >= ship.locationY &&
            y < ship.locationY + ship.size &&
            x === ship.locationX
          ) {
            return ship.name.toLowerCase();
          }
        }
      }
      return 'empty';
    }
  }
  getShipsPlayerTwo(x: number, y: number): string {
    if (this.playerTwoHits.includes(x * 10 + y)) {
      return 'hit';
    } else if (this.playerTwoMisses.includes(x * 10 + y)) {
      return 'miss';
    } else {
      for (const ship of this.playerOneShips) {
        // console.log(`LocationX ${ship.locationX}, LocationY ${ship.locationY}, Ship: ${ship.name}`);
        if (ship.direction === 'East') {
          if (
            x >= ship.locationX &&
            x < ship.locationX + ship.size &&
            y === ship.locationY
          ) {
            return ship.name.toLowerCase();
          }
        } else if (ship.direction === 'West') {
          if (
            x <= ship.locationX &&
            x > ship.locationX - ship.size &&
            y === ship.locationY
          ) {
            return ship.name.toLowerCase();
          }
        } else if (ship.direction === 'South') {
          if (
            y <= ship.locationY &&
            y > ship.locationY - ship.size &&
            x === ship.locationX
          ) {
            return ship.name.toLowerCase();
          }
        } else if (ship.direction === 'North') {
          if (
            y >= ship.locationY &&
            y < ship.locationY + ship.size &&
            x === ship.locationX
          ) {
            return ship.name.toLowerCase();
          }
        }
      }
      return 'empty';
    }
  }

  
  handleCellClick(player: string, x: number, y: number) {
    console.log(`Clicked cell at X: ${x}, Y: ${y}`);
    // Check if it's the player's turn
    if (
      (player === 'playerOne' && this.isPlayerOneTurn) ||
      (player === 'playerTwo' && !this.isPlayerOneTurn)
    ) {
      const cellIndex = x * 10 + y;

      // Check if the cell has already been clicked
      if (
        (player === 'playerOne' && this.playerOneHits.includes(cellIndex)) ||
        (player === 'playerOne' && this.playerOneMisses.includes(cellIndex)) ||
        (player === 'playerTwo' && this.playerTwoHits.includes(cellIndex)) ||
        (player === 'playerTwo' && this.playerTwoMisses.includes(cellIndex))
      ) {
        return; // Cell has already been clicked
      }

      const isHit = this.checkForHit(
        player === 'playerOne' ? this.playerTwoShips : this.playerOneShips,
        x,
        y
      );

      if (isHit) {
        // Add the cell to hits
        if (player === 'playerOne') {
          this.playerOneHits.push(cellIndex);
        } else {
          this.playerTwoHits.push(cellIndex);
        }

        // Switch the turn to the other player
        this.isPlayerOneTurn = !this.isPlayerOneTurn;
        this.currentPlayer = this.isPlayerOneTurn ? 'playerOne' : 'playerTwo';
      } else {
        // Add the cell to misses
        if (player === 'playerOne') {
          this.playerOneMisses.push(cellIndex);
        } else {
          this.playerTwoMisses.push(cellIndex);
        }

        // Switch the turn to the other player (when it's a miss)
        this.isPlayerOneTurn = !this.isPlayerOneTurn;
        this.currentPlayer = this.isPlayerOneTurn ? 'playerOne' : 'playerTwo';
      }
    }
  }

  // Helper function to check if it's a hit or miss
  checkForHit(playerShips: any[], x: number, y: number): boolean {
    for (const ship of playerShips) {
      if (ship.direction === 'East') {
        if (
          x >= ship.locationX &&
          x < ship.locationX + ship.size &&
          y === ship.locationY
        ) {
          ship.health--;
          if (ship.health === 0) {
            // Handle the destruction of the ship here
            this.appendToCombatLog(`Ship ${ship.name} has been destroyed!`);
            this.checkForDestroyedShips(this.currentPlayer);
          }
          return true; // It's a hit
        }
      } else if (ship.direction === 'West') {
        if (
          x <= ship.locationX &&
          x > ship.locationX - ship.size &&
          y === ship.locationY
        ) {
          ship.health--;
          if (ship.health === 0) {
            // Handle the destruction of the ship here
            this.appendToCombatLog(`Ship ${ship.name} has been destroyed!`);
            this.checkForDestroyedShips(this.currentPlayer);
          }
          return true; // It's a hit
        }
      } else if (ship.direction === 'South') {
        if (
          y <= ship.locationY &&
          y > ship.locationY - ship.size &&
          x === ship.locationX
        ) {
          ship.health--;
          if (ship.health === 0) {
            // Handle the destruction of the ship here
            this.appendToCombatLog(`Ship ${ship.name} has been destroyed!`);
            this.checkForDestroyedShips(this.currentPlayer);
          }
          return true; // It's a hit
        }
      } else if (ship.direction === 'North') {
        if (
          y >= ship.locationY &&
          y < ship.locationY + ship.size &&
          x === ship.locationX
        ) {
          ship.health--;
          if (ship.health === 0) {
            // Handle the destruction of the ship here
            this.appendToCombatLog(`Ship ${ship.name} has been destroyed!`);
            this.checkForDestroyedShips(this.currentPlayer);
          }
          return true; // It's a hit
        }
      }
    }
    return false; // It's a miss
  }
  checkForDestroyedShips(player: string) {
    const ships = player === 'playerOne' ? this.playerOneShips : this.playerTwoShips;
    const playerTwo = player === 'playerOne' ? 'playerTwo' : 'playerOne';
    const remainingShips = ships.filter((ship) => ship.health > 0);
    const remainingShipsPlayerTwo = player === 'playerOne' ? this.playerTwoShips : this.playerOneShips;
  
    if (player === 'playerOne') {
      this.appendToCombatLog(`Player One has ${remainingShips.length} ships remaining.`);
      this.appendToCombatLog(`Player Two has ${this.playerTwoShips.filter((ship) => ship.health > 0).length} ships remaining.`);
    } else {
      this.appendToCombatLog(`Player One has ${remainingShips.length} ships remaining.`);
      this.appendToCombatLog(`Player Two has ${this.playerOneShips.filter((ship) => ship.health > 0).length} ships remaining.`);
    }
  
    if (remainingShips.length === 0) {
      this.appendToCombatLog(`Player One has no remaining ships. Player Two wins!`);
      this.gameIsOver = true; // Set the game over flag
      // You can handle the game-ending logic here, such as displaying a message or ending the game.
    } else if (remainingShipsPlayerTwo.filter((ship) => ship.health > 0).length === 0) {
      this.appendToCombatLog(`Player Two has no remaining ships. Player One wins!`);
      this.gameIsOver = true; // Set the game over flag
      // Handle the case where the opponent wins
    }
  }
  
  checkForGameStatus() {

    if (this.gameIsOver == true) {
      this.blurGameBoard(); // Add this to blur the game board
    }
  }

  blurGameBoard() {
    if (this.gameIsOver) {
      this.renderer.addClass(document.body, 'blur-background');

      // Add an event listener for the "Play Again" button click
      const playAgainButton = document.querySelector('#play-again-button');
      if (playAgainButton) {
        playAgainButton.addEventListener('click', () => {
          window.location.reload(); // Reload the page to restart the game
        });
      }
    }
  }
  appendToCombatLog(message: string) {
    this.logEntries.push(message);
  }
  switchTurn() {
    this.isPlayerOneTurn = !this.isPlayerOneTurn;
  }
  restartGame() {
    window.location.reload();
  }
}
