
require term.fs
require lcd.fs
require time.fs
require input.fs
require random.fs
require gbhw.fs
require sfx.fs

require flappy-bird-tilemap.fs
require save.fs

\ TODO
\ + start screen
\ + display title
\ + score system
\ + save top-score
\ + display score in-game via window-map
\ + flap animation
\ + death animation (faceplant bird)
\ - draw backround as it scrolls (or change tiles so they tile `mod 32`
\   - I feel I dont fit into vblank :(
\ + BUG: if you hit the pipe just right you will get 60 points while falling during gameover
\   = fixed this by checking if player dead or not before increasing score

[host]

: <-row
  \ depth dup dup
  \ .S
  \ 20 <>
  \ .S
  \ swap
  \ .S
  \ 32 <>
  \ .S
  \ and
  \ if .S true abort" Each row of a map should have exactly 20 numbers (tiles)!" then

  depth 0 do
    31 I - pick
    [target] c, [host]
  loop
  depth 0 do
    drop
  loop ;

[target]


ROM

create background-map

 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1 <-row
 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1 <-row
 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1 <-row
 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1 <-row
 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1 <-row
 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1 <-row
 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1 <-row
 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1 <-row
 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1 <-row
 4  5  6  1 10  11 12 14 15 16  17 18 19 20 21   4  5  6  1 10  11 12 14 15 16  17 18 19 20 21   4  5 <-row
 0  0  7  8  9   0 13  0  0  0   0  0  0  0 22   0  0  7  8  9   0 13  0  0  0   0  0  0  0 22   0  0 <-row
 0 30 31  0  0  30 31  0  0 30  31  0  0 30 31   0  0 30 31  0   0 30 31  0  0  30 31  0  0 30  31  0 <-row
32 33 34 35 32  33 34 35 32 33  34 35 32 33 34  35 32 33 34 35  32 33 34 35 32  33 34 35 32 33  34 35 <-row
36 37 38 39 36  37 38 39 36 37  38 39 36 37 38  39 36 37 38 39  36 37 38 39 36  37 38 39 36 37  38 39 <-row
40 41 42 43 40  41 42 43 40 41  42 43 40 41 42  43 40 41 42 43  40 41 42 43 40  41 42 43 40 41  42 43 <-row
 2  2  2  2  2   2  2  2  2  2   2  2  2  2  2   2  2  2  2  2   2  2  2  2  2   2  2  2  2  2   2  2 <-row
23 24 25 26 27  28 29 23 24 25  26 27 28 29 23  24 25 26 27 28  29 23 24 25  26 27 28 29 23  24 25 26 <-row
 1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1   1  1  1  1  1   1  1  1 <-row

\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row
\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row
\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row
\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row
\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row
\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row
\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row
\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row
\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row
\  4  5  6  1 10  11 12 14 15 16  17 18 19 20 21   4  5  6  1 10 <-row
\  0  0  7  8  9   0 13  0  0  0   0  0  0  0 22   0  0  7  8  9 <-row
\  0 30 31  0  0  30 31  0  0 30  31  0  0 30 31   0  0 30 31  0 <-row
\ 32 33 34 35 32  33 34 35 32 33  34 35 32 33 34  35 32 33 34 35 <-row
\ 36 37 38 39 36  37 38 39 36 37  38 39 36 37 38  39 36 37 38 39 <-row
\ 40 41 42 43 40  41 42 43 40 41  42 43 40 41 42  43 40 41 42 43 <-row
\  2  2  2  2  2   2  2  2  2  2   2  2  2  2  2   2  2  2  2  2 <-row
\ 23 24 25 26 27  28 29 23 24 25  26 27 28 29 23  24 25 26 27 28 <-row
\  1  1  1  1  1   1  1  1  1  1   1  1  1  1  1   1  1  1  1  1 <-row

RAM

: set-sprite-attr ( x y tile attr -- )
  ( a ) $fe03 c!
  ( t ) $fe02 c!
  ( y ) $fe00 c!
  ( x ) $fe01 c! ;

: move-sprite ( x y -- )
  ( y ) $fe00 c!
  ( x ) $fe01 c! ;

: set-sprite-attr2 ( x y tile attr -- )
  ( a ) $fe07 c!
  ( t ) $fe06 c!
  ( y ) $fe04 c!
  ( x ) $fe05 c! ;

: move-sprite2 ( x y -- )
  ( y ) $fe04 c!
  ( x ) $fe05 c! ;

: set-sprite-attr3 ( x y tile attr -- )
  ( a ) $fe0a c!
  ( t ) $fe10 c!
  ( y ) $fe08 c!
  ( x ) $fe09 c! ;

: move-sprite3 ( x y -- )
  ( y ) $fe08 c!
  ( x ) $fe09 c! ;

: change-sprite ( t -- )
  $fe02 c! ;


: set-sprite ( t attr idx -- )
  4 *
  dup $fe03 +
  rot swap c!
  $fe02 + c! ;

: move-sprite-i ( x y idx -- )
  4 *
  dup $fe00 +
  rot swap c!
  $fe01 + c! ;

: move-player-sprites-x ( x x x -- )
       $fe01 c!
   8 + $fe05 c!
  16 + $fe09 c! ;

: move-player-sprites-y ( y y y -- )
  $fe00 c!
  $fe04 c!
  $fe08 c! ;

: player-anim-flap0
  56 $fe02 c!
  58 $fe06 c! ;

: player-anim-flap1
  62 $fe02 c!
  64 $fe06 c! ;

: player-anim-flap2
  66 $fe02 c!
  134 $fe06 c! ;

: flip-player-sprite ( bool -- )
  if
    %01000000 $fe03 c!
    %01000000 $fe07 c!
    %01000000 $fe0b c!
  else
    %00000000 $fe03 c!
    %00000000 $fe07 c!
    %00000000 $fe0b c!
  then ;


48 constant WALL-GAP
 8 constant WALL-DISTANCE \ doesn't work
 4 constant WALL-NUM
134 constant FLOOR-HEIGHT
24 constant PIPE-COLLISION-LEFT
42 constant PIPE-COLLISION-RIGHT

variable player-x
variable player-y
variable jump-velocity
variable is-player-grounded-var
variable can-jump

variable redraw-wall
variable next-wall-x-tile
variable next-wall-y-tile
variable current-wall-idx
variable wall1-x \ pending delete
variable wall1-y
variable is-even-frame
variable time
variable skip-walls
variable score
variable best-score
variable score-win-offset
variable is-game-over
variable anim-fade-state
CREATE rSCXbuf 1 cells allot

CREATE wall-ys 4 allot

: move-wall1-sprites-x ( x -- )
  16 -
  dup $fe19 c!
  24 +
  dup $fe1d c!
  40 +
  dup $fe21 c!
  24 +
  dup $fe25 c!
  40 +
  dup $fe29 c!
  24 +
  ( ) $fe2d c! ;

: move-wall1-sprites-y ( y1 y2 y3 -- )
  $fe18 c!
  $fe1c c!
  $fe20 c!
  $fe24 c!
  $fe28 c!
  $fe2c c! ;

: disable-sprites ( -- )
  %11100111 $ff40 c! ;

: enable-sprites ( - )
  %11110111 $ff40 c! ;

: player-jump ( -- )
  thud
  -3 jump-velocity c!
  false can-jump c! ;

: apply-gravity-to-player
  player-y c@ jump-velocity c@ + player-y c!

  jump-velocity c@ 3 <>
  \ rDIV c@ 4 mod 0 =
  \ is-even-frame c@
  time c@ 5 mod 0 =
  and if
    jump-velocity c@ 1+ jump-velocity c!
  then ;

: animate-flap ( -- )
  \ comparision with negative numbers is messed up :(
  jump-velocity c@ 253 = if
    player-anim-flap2
  then
  jump-velocity c@ 255 = if
    player-anim-flap1
  then
  jump-velocity c@ 1 = if
    player-anim-flap0
  then ;

: draw-pipe-top ( height -- )
  0 do
    47 cursor-addr v!
    1 cursor-x +!
    48 cursor-addr v!
    1 cursor-y +!
    -1 cursor-x +!
  loop ;

: draw-pipe-top-cap
  44 cursor-addr v!
  1 cursor-x +!
  45 cursor-addr v! ;

: draw-pipe-bot-cap
  45 cursor-addr v!
  -1 cursor-x +!
  44 cursor-addr v! ;

: draw-pipe-bot ( height -- )
  0 do
    1 cursor-y +!
    47 cursor-addr v!
    1 cursor-x +!
    48 cursor-addr v!
    -1 cursor-x +!
  loop ;

: redraw-gap
  \ 6 is `WALL-GAP` in tiles
  6 0 do
    -1 cursor-x +!
    1 cursor-y +!
    background-map
    cursor-y @ 32 * cursor-x @ +
    + c@
    cursor-addr v!
    1 cursor-x +!
    background-map
    cursor-y @ 32 * cursor-x @ +
    + c@
    cursor-addr v!
  loop ;

: draw-pipe ( gap-y x -- )
  32 mod
  0 at-xy

  dup draw-pipe-top
  draw-pipe-top-cap

  redraw-gap
  WALL-GAP 8 /

  draw-pipe-bot-cap
  \ 15 is total pipe height
  + 15 swap - draw-pipe-bot ;


: generate-wall
  \ 2..9
  7 random 2 + ;


: collision-player-hit-pipe? ( -- bool )
  wall1-x c@ PIPE-COLLISION-LEFT >
  wall1-x c@ PIPE-COLLISION-RIGHT <
  and
  \ @optimize: `wall1-y c@ 8 *` can be done once
  player-y c@ wall1-y c@ 8 * 20 + <
  player-y c@ wall1-y c@ 8 * WALL-GAP + 8 + >
  or
  and ;


: pipe-went-offscreen ( -- )
  wall1-x c@ 0<> if
    exit
  then

  \ reset wall position
  WALL-DISTANCE 8 * wall1-x c!
  1 redraw-wall c!

  \ regenerate wall player just passed
  generate-wall dup wall-ys current-wall-idx c@ + c!

  \ draw next pipe at this x coord
  current-wall-idx c@ WALL-DISTANCE * 8 + next-wall-x-tile c!
  next-wall-y-tile c!

  \ change pointer to next wall
  current-wall-idx c@ 1+ WALL-NUM mod
  dup
  current-wall-idx c!
  \ switch collision var to next wall
  wall-ys + c@ wall1-y c!

  skip-walls c@ 0 > if
    skip-walls c@ 1 - skip-walls c!
    exit
  then ;


: player-passed-pipe ( -- )
  skip-walls c@ 0 > if
    exit
  then

  wall1-x c@ 40 <> if
    exit
  then

  \ Increase score
  is-game-over c@ false = if
    score c@ 1+ score c!
  then
  \ recalc offset
  score c@
  dup 9 > if
    12 score-win-offset c!
  then

  99 > if
    20 score-win-offset c!
  then ;


: draw-small-number ( n x y -- )
  ( y ) SCRN_VX_B * + $9c00 +
  swap
  124 +
  swap
  c! ;


: draw-current-score ( -- )
  score c@
  3 0 do
   dup
   10 mod
   9 I - 0 draw-small-number
   10 /
   dup 0= if
     leave
   then
  loop
  drop
  7 score-win-offset c@ + rWX c!  ;


: game-over ( -- )
  true is-game-over c!
  \ player-jump
  lcd-wait-vblank
  true flip-player-sprite ;


: game-loop ( -- n )
  apply-gravity-to-player

  time c@ 1+ time c!
  is-even-frame c@ true xor is-even-frame c!

  is-game-over c@ if
    player-y c@ FLOOR-HEIGHT > if
      FLOOR-HEIGHT 3 - player-y c!
      500 ms
      1 exit
    then
  else
    key-state k-a and if
      can-jump c@ if
        player-jump
      then
    \ prevent jumping when A button is held
    else
      true can-jump c!
    then

    \ collision with floor
    player-y c@ FLOOR-HEIGHT > if
      \ DEBUG auto-jump
      \ player-jump

      \ game over
      game-over
      \ 1 exit
    then

    \ collision with ceiling
    player-y c@ 200 >
    player-y c@ 6 <
    or if
      game-over
      6 player-y c!
      0 jump-velocity c!
    then


    \ move walls collision forward
    wall1-x c@ 1 - wall1-x c!

    collision-player-hit-pipe? skip-walls c@ 0 = and if
      game-over
      \ 1 exit
    then
  then

  pipe-went-offscreen
  player-passed-pipe

  redraw-wall c@

  \ TODO wall sprites wont match with actual walls :(
  \ \ 32 32 32 dup dup dup
  \ \ wall-ys current-wall-idx 1 + 4 mod + c@ 8 * 16 + dup
  \ \ wall-ys current-wall-idx 2 + 4 mod + c@ 8 * 16 + dup
  \ 5 5 5 5 50 50
  \ \ wall-ys current-wall-idx 4 mod + c@ 2 + 8 * dup
  \ wall1-x c@

  \ OpTiMaZaTiOn
  player-y c@ dup dup


  \ \ DEBUG collision
  \ wall1-x c@
  \ wall1-x c@ 16 >
  \ wall1-x c@ 43 <
  \ and
  \ if
  \   0
  \ else
  \   8
  \ then
  \ wall1-y c@ 8 * + 20
  \ \ END DEBUG

  ( wall1-x bool redraw-wall player-y )
  lcd-wait-vblank
  is-game-over c@ false = if
    1 rSCX +!
  then

  \ ( DEBUG ) move-sprite-i

  animate-flap
  ( player-y ) move-player-sprites-y
  draw-current-score

  \ TODO see above about wall sprites
  \ move-wall1-sprites-x
  \ move-wall1-sprites-y

  ( redraw-wall ) 1 = if
    next-wall-y-tile c@
    next-wall-x-tile c@

\    IEF_LCDC rIE c@ xor rIE c!
\    rSCXbuf @ rSCX !

    draw-pipe

\    IEF_LCDC enable-interrupt-flags

    0 redraw-wall c!
  then

  \ put on 0 on stack, which means don't exit external game loop
  0 ;


: animate-fade-in ( n -- )
    case
      0 of %00000000 %00000000 endof
      1 of %01000000 %01000010 endof
      2 of %10010000 %10000010 endof
      3 of %11100100 %11000110 endof
    endcase
    rOBP0 c!
    rBGP c!
    ;


: set-fully-fade-in-palettes
    %11000110 rOBP0 c!
    %11100100 rBGP c! ;


: menu-loop ( - n )
  key-state k-start and if
    \ start game
    \ ----------

    \ update player position
    15 player-x c!
    lcd-wait-vblank
    player-x c@ dup dup move-player-sprites-x
    1 exit
  then

  apply-gravity-to-player

  \ auto jump
  \ prevent loop-falling and allows you to test jump height / gravity strength
  player-y c@ 90 > if
    player-jump
  then

  time c@ 1+ time c!
  is-even-frame c@ true xor is-even-frame c!

  \ OpTiMaZaTiOn
  player-y c@ dup dup

  1 rSCXbuf +!

  \ fade-in
  time c@ 20 mod 0 = anim-fade-state c@ 3 < and if
    anim-fade-state c@ 1 + anim-fade-state c!
  then
  anim-fade-state c@
  lcd-wait-vblank
  animate-fade-in

  animate-flap

  ( player-y ) move-player-sprites-y


  \ put on 0 on stack, which means don't exit external game loop
  0 ;


: reset-variables ( - )
  72 player-x c!
  70 player-y c!
  0 jump-velocity c!
  false is-player-grounded-var c!
  true can-jump c!
  false is-even-frame c!
  0 time c!
  0 redraw-wall c!
  0 next-wall-x-tile c!
  0 current-wall-idx c!
  2 skip-walls c! \ skips first two invisible walls
  0 score c!
  0 score-win-offset c!
  false is-game-over c!
  0 rSCXbuf !

  \ load save file
  enable-external-ram
  $a000 c@ 255 = if
    0 best-score c!
    0 $a000 c!
  else
    $a000 c@ best-score c!
  then
  disable-external-ram

  \ setup wall
  \ WALL-DISTANCE 8 * \ 80 when WALL-DISTANCE == 8
  48 wall1-x c!
  5 2 + wall1-y c! ;


: setup-sprites ( - )
  \ Set sprite palettes
\  %11000110 $ff48 c!
\  %00100111 $ff49 c!
  \      ^^ transparent

  \ Player
  56 %00000000 0 set-sprite
  58 %00000000 1 set-sprite
  60 %00000000 2 set-sprite

  player-x c@      player-y c@ 0 move-sprite-i
  player-x c@  8 + player-y c@ 1 move-sprite-i
  player-x c@ 16 + player-y c@ 2 move-sprite-i

  \ TODO remove: pipe's side panels (broken)
  \ 46 %00100000 6 set-sprite
  \ 46 %00000000 7 set-sprite
  \ 46 %00100000 8 set-sprite
  \ 46 %00000000 9 set-sprite
  \ 46 %00100000 10 set-sprite
  \ 46 %00000000 11 set-sprite
  \ 0 16 6 move-sprite-i
  \ 0 16 7 move-sprite-i
  \ 0 16 8 move-sprite-i
  \ 0 16 9 move-sprite-i
  \ 0 16 10 move-sprite-i
  \ 0 16 11 move-sprite-i

  2 %00000000 20 set-sprite ;


: init-pipes ( - )
\  WALL-NUM 0 do
\    generate-wall dup wall-ys I + c!
\    I 1 > if
\      I WALL-DISTANCE * 8 + draw-pipe
\    then
\  loop

  \ Above version had an error found by forthchk.py
  \ I did a rewrite, but its need a closer check. See [1] below
  WALL-NUM 0 do
    generate-wall wall-ys I + c!
  loop
  \ [1] I'm not sure 2 is the right number (this replace `I 1 > if` in above version)
  WALL-NUM 2 do
    wall-ys I + c@
    I WALL-DISTANCE * 8 + draw-pipe
  loop
  wall-ys c@ wall1-y c! ;


: init-background ( - )
  18 0 do
    0 I at-xy
    background-map I 32 * + 32 type
  loop ;


: int-handler-fs
  rLYC c@ 132 = if
    0 rSCX !
    48 rLYC c!
  else
    rSCXbuf @ rSCX !
    132 rLYC c!
  then
  enable-interrupts
  ;


: enable-SCX-interrupt
  0 rLYC c!
  IEF_LCDC enable-interrupt-flags
  rSTAT c@ %01000000 or rSTAT c! ;


: draw-tile-line ( cursor1 t t n - cursor2 )
  ( t n ) + ( t ) swap do
    dup
    I swap c!
    1+
  loop ;


: draw-title ( -- )
  \ flappy
  4 1 at-xy
  cursor-addr
  68 68 7 draw-tile-line
  SCRN_VX_B + 7 -
  75 75 7 draw-tile-line
  SCRN_VX_B + 7 -
  82 82 7 draw-tile-line
  drop

  \ bird
  11 1 at-xy
  cursor-addr
  89 89 5 draw-tile-line
  SCRN_VX_B + 5 -
  94 94 5 draw-tile-line
  SCRN_VX_B + 5 -
  99 99 5 draw-tile-line
  drop ;


: draw-big-number ( x y n - )
  case
  0 of
    2dup
    1+ at-xy 114 cursor-addr c!
       at-xy 104 cursor-addr c!
  endof
  1 of
    2dup
    1+ at-xy 115 cursor-addr c!
       at-xy 105 cursor-addr c!
  endof
  2 of
    2dup
    1+ at-xy 116 cursor-addr c!
       at-xy 106 cursor-addr c!
  endof
  3 of
    2dup
    1+ at-xy 117 cursor-addr c!
       at-xy 107 cursor-addr c!
  endof
  4 of
    2dup
    1+ at-xy 118 cursor-addr c!
       at-xy 108 cursor-addr c!
  endof
  5 of
    2dup
    1+ at-xy 119 cursor-addr c!
       at-xy 109 cursor-addr c!
  endof
  6 of
    2dup
    1+ at-xy 120 cursor-addr c!
       at-xy 110 cursor-addr c!
  endof
  7 of
    2dup
    1+ at-xy 121 cursor-addr c!
       at-xy 111 cursor-addr c!
  endof
  8 of
    2dup
    1+ at-xy 122 cursor-addr c!
       at-xy 112 cursor-addr c!
  endof
  9 of
    2dup
    1+ at-xy 123 cursor-addr c!
       at-xy 113 cursor-addr c!
  endof
  endcase ;


: draw-best-score ( -- )
  best-score c@
  3 0 do
   dup
   10 mod
   10 I - 4 rot draw-big-number
   10 /
   dup 0= if
     leave
   then
  loop
  drop
  \ 10 4 2 draw-big-number
  ;


: reset-game ( - )
  disable-sprites
  disable-interrupts
  disable-lcd

  1234 seed !

  reset-variables

  setup-sprites

  init-background
  draw-title
  draw-best-score

  \ window setup
  7 12 + rWX c!
  136 rWY c!

  \ clear window (only top row is visible)
  20 0 do
    1 $9c00 I + c!
  loop

  enable-lcd
  enable-sprites
  enable-interrupts
  enable-SCX-interrupt ;


\ Special ROM setup
\ -----------------

\ https://gbdev.io/pandocs/#_0149-ram-size
$02 $0147 c! \ set cart type to ROM+RAM
$01 $0149 c! \ enable cartridge ram (2kb)

\ set jump to our interrupt handler
$c3 $0048 c!
' int-handler-fs $0049 !


: fade-in ( n -- )
    lcd-wait-vblank
    %00000000 rBGP c!
    dup ms

    lcd-wait-vblank
    %01000000 rBGP c!
    dup ms

    lcd-wait-vblank
    %10010000 rBGP c!
    dup ms

    lcd-wait-vblank
    %11100100 rBGP c!
    ms
    ;


: fade-out ( n -- )
    lcd-wait-vblank
    %11100100 rBGP c!
    dup ms

    lcd-wait-vblank
    %10010000 rBGP c!
    dup ms

    lcd-wait-vblank
    %01000000 rBGP c!
    dup ms

    lcd-wait-vblank
    %00000000 rBGP c!
    ms
    ;


: main
  install-flappy-bird-tileset
  \ init-term

  disable-lcd
  \ set this only once
  0 anim-fade-state c!
  $00 rBGP c!
  %00000000 rOBP0 c!
  %11111111 rOBP1 c!
\  %11000110 $ff48 c!
\  %00100111 $ff49 c!
  reset-game
  init-sfx

  begin
\ FIXME why this doesn't crash the game? Did I left something on the stack??
\     drop
\     drop
\     drop
\    \ drop \ super glitchy scrolling
    \ 2050 fade-in
    begin menu-loop 1 = until

    disable-lcd
    init-background
    IEF_LCDC rIE c@ xor rIE c!
    32 rSCX !
    init-pipes
    set-fully-fade-in-palettes
    player-jump
    enable-lcd

    begin game-loop 1 = until

    score c@ best-score c@ > if
      score c@ best-score c!
      enable-external-ram
      best-score c@ $a000 c!
      disable-external-ram
    then

    2000 ms

    reset-game

  false until ;

