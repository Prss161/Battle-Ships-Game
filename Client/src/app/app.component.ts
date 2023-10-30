import { Component, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'BattleShipsGame';
  playerOneShips: any[] = [];
  playerTwoShips: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
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
    console.log(`x: ${x}, y: ${y}`);
    for (const ship of this.playerOneShips) {
      console.log(`Ship: ${ship.name}, LocationX: ${ship.locationX}, LocationY: ${ship.locationY}, Size: ${ship.size}, Direction: ${ship.direction}`);
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
  getShipsPlayerTwo(x: number, y: number): string {
    console.log(`x: ${x}, y: ${y}`);
    for (const ship of this.playerTwoShips) {
      console.log(`Ship: ${ship.name}, LocationX: ${ship.locationX}, LocationY: ${ship.locationY}, Size: ${ship.size}, Direction: ${ship.direction}`);
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
