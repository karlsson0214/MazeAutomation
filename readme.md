different setup to make random maze
=======================================
Tiles
-------------
wall takes up 25% of width

floor takes up 50% of width

### 4 way crossing
```
x  x

x  x
```
### 3 way crossing 
4 rotations
```
xxxx

x  x
```

### straight
2 rotations
```
  xxxx

  xxxx
```
### curve
4 rotations
```
  xxxx
  x
  x  x
```

Cells
------------
x - wall
o - floor
  - not decided yet
Start:

```
 01234567890
0xooo   ooox
1xox x x xox
2xo       ox
3x x x x x x
4x         x
5x x x x x x
6 symmetric
7
8
9
0
```
