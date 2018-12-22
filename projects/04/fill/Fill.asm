// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Fill.asm

// Runs an infinite loop that listens to the keyboard input.
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel;
// the screen should remain fully black as long as the key is pressed. 
// When no key is pressed, the program clears the screen, i.e. writes
// "white" in every pixel;
// the screen should remain fully clear as long as no key is pressed.

// Put your code here.
// Pseudo Code
//  BLACKEN
//  INFLOOP
//  if KBD = 0
//      if SCREEN[0] = 1
//          write white in every pixel
//      goto INFLOOP
//  else
//      write black in every pixel
//      goto INFLOOP

    @8192
    D=A
    @n
    M=D // n = 8192
    @current
    M=0 // current color in screen is white

(INFLOOP)
    // reset i and addr
    @i
    M=0 // i = 0
    @SCREEN
    D=A
    @addr
    M=D // addr = SCREEN
    @KBD
    D=M
    @SETWHITE
    D;JEQ // if RAM[KBD] = 0 goto SETWHITEN

    @current
    M=-1 // set black
    @WRITECOLOR
    0;JMP

(SETWHITE)
    @current
    M=0

(WRITECOLOR)
    @current
    D=M
    @SCREEN
    D=D-M
    @INFLOOP
    D;JEQ // if RAM[SCREEN] = current goto INFLOOP (no need to change color)

(LOOP)
    @i
    D=M
    @n
    D=D-M
    @INFLOOP
    D;JGE // if i >= n goto INFLOOP
    @current
    D=M
    @addr
    A=M
    M=D // write color to 16 pixels in addr
    @addr
    M=M+1 // addr = addr + 1
    @i
    M=M+1
    @LOOP
    0;JMP // goto WHITENLOOP








