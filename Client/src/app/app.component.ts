import { Component, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'BattleShipsGame';
  
  playerOneGameBoard: string[][] = [];
  playerTwoGameBoard: string[][] = [];

  playerOneShips: any[] = [];
  playerTwoShips: any[] = [];

  playerOneHits: number[] = [];
  playerOneMisses: number[] = [];
  playerTwoHits: number[] = [];
  playerTwoMisses: number[] = [];

  isPlayerOneTurn: boolean = true;
  currentPlayer: string = 'playerOne';
  clickedCells: Set<string> = new Set();
  
  constructor(private http: HttpClient) {}

  ngOnInit() {
    // Initialize the game boards for PlayerOne and PlayerTwo with empty cells
    for (let i = 0; i < 10; i++) {
      this.playerOneGameBoard[i] = [];
      this.playerTwoGameBoard[i] = [];
      for (let j = 0; j < 10; j++) {
        this.playerOneGameBoard[i][j] = 'empty';
        this.playerTwoGameBoard[i][j] = 'empty';
      }
    }

    // Make an HTTP request to fetch ship placement data for PlayerOne
    this.http.get('http://localhost:5000/api/Battle/PlayerOne').subscribe((data: any) => {
      this.playerOneShips = data;
    });

    // Make an HTTP request to fetch ship placement data for PlayerTwo
    this.http.get('http://localhost:5000/api/Battle/PlayerTwo').subscribe((data: any) => {
      this.playerTwoShips = data;
    });
  }

  getShipsPlayerOne(x: number, y: number): string {
    if (this.playerOneHits.includes(x * 10 + y)) {
      return 'hit';
    } else if (this.playerOneMisses.includes(x * 10 + y)) {
      return 'miss';
    } else {
      for (const ship of this.playerTwoShips) {
        console.log(`LocationX ${ship.locationX}, LocationY ${ship.locationY}, Ship: ${ship.name}`);
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
        console.log(`LocationX ${ship.locationX}, LocationY ${ship.locationY}, Ship: ${ship.name}`);
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
    // Check if it's the player's turn
    if (
      (player === 'playerOne' && this.isPlayerOneTurn) ||
      (player === 'playerTwo' && !this.isPlayerOneTurn)
    ) {
      const cellIndex = x * 10 + y;
  
      // Check if the cell has already been clicked
      if (
        this.playerOneHits.includes(cellIndex) ||
        this.playerOneMisses.includes(cellIndex) ||
        this.playerTwoHits.includes(cellIndex) ||
        this.playerTwoMisses.includes(cellIndex)
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
      } else {
        // Add the cell to misses
        if (player === 'playerOne') {
          this.playerOneMisses.push(cellIndex);
        } else {
          this.playerTwoMisses.push(cellIndex);
        }
      }
  
      // Toggle the turn to the other player
      this.isPlayerOneTurn = !this.isPlayerOneTurn;
      this.currentPlayer = this.isPlayerOneTurn ? 'playerOne' : 'playerTwo';
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
          return true; // It's a hit
        }
      } else if (ship.direction === 'West') {
        if (
          x <= ship.locationX &&
          x > ship.locationX - ship.size &&
          y === ship.locationY
        ) {
          return true; // It's a hit
        }
      } else if (ship.direction === 'South') {
        if (
          y <= ship.locationY &&
          y > ship.locationY - ship.size &&
          x === ship.locationX
        ) {
          return true; // It's a hit
        }
      } else if (ship.direction === 'North') {
        if (
          y >= ship.locationY &&
          y < ship.locationY + ship.size &&
          x === ship.locationX
        ) {
          return true; // It's a hit
        }
      }
    }
    return false; // It's a miss
  }
  switchTurn() {
    this.isPlayerOneTurn = !this.isPlayerOneTurn;
  }
}