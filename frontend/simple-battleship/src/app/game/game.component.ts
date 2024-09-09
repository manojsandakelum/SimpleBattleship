import { Component, OnInit } from '@angular/core';
import { BattleshipService } from '../services/battleship.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  grid: string[][] = [];
  gameStatus: string = '';

  constructor(private battleshipService: BattleshipService) {}

  ngOnInit(): void {
    this.loadGameGrid();
  }

  // Load the initial grid
  loadGameGrid(): void {
    this.battleshipService.getGrid().subscribe((data: string[][]) => {
      this.grid = data;
    });
  }

  // Fire a shot at the given coordinates
  fireShot(x: number, y: number): void {
    this.battleshipService.fireShot(x, y).subscribe(response => {
      this.loadGameGrid(); // Refresh grid after firing
      this.gameStatus = response.gameStatus;
    });
  }

  // Reset the game
  resetGame(): void {
    this.battleshipService.resetGame().subscribe(res => {
      this.loadGameGrid();
      this.gameStatus = 'Game reset. Start firing!';
    });
  }
}
