# Minesweeper Console Game

## Overview
This is a console-based Minesweeper game with multiple board sizes, seeded mine placement, and persistent high scores.  

## Board Sizes & Mines
- 8x8 → 10 mines  
- 12x12 → 25 mines  
- 16x16 → 40 mines  

## Input Commands
- Reveal a tile: `r row col`  
- Flag/unflag a tile: `f row col`  
- Quit: `q`  

Coordinates are **0-indexed**.

## Seed
- Seed is prompted at the start.  
- Leave blank to generate from current time.  
- Seed ensures reproducible mine placement.  

## Board Symbols
- Hidden: `#`  
- Flagged: `f`  
- Bomb (hit): `b`  
- Empty revealed: `.`  
- Numbers: `1` through `8`  

## High Scores
- Stored in `data/highscores.csv`  
- Top 5 per board size  
- Format: `size,seconds,moves,seed,timestamp`  

## Build & Run
```bash
dotnet build
dotnet run --project src/Minesweeper.Console/Minesweeper.Console.csproj
