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
* x - wall

* o - floor

* empty - not decided yet

Start:

```
 01234567890
0xxxxxxxxxxx
1xooo   ooox
2xox x x xox
3xo       ox
4x x x x x x
5x         x
6x x x x x x
7 symmetric
8
9
0
```

###algorithm
```
Traverse all odd rows where the column also is odd. One cell at a time. 
if cell is floor
    // check neighbours East, N, W, S
    // and set UNset cells
    if 2 walls => add 2 floors
    if 1 walls => randomize rest (wall or floor) but at least 2 floors
    if 0 walls => randomize rest (wall or floor) but at least 2 floors (Will never happend with setup above)
```
