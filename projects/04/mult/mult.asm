// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Mult.asm

// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[2], respectively.)

// Put your code here.
// Pseudo Code
//  let n = R0, r1 = R1, R2 = 0, i = 1
//  if n < 0
//      n = -n
//      r1 = -r1
//  LOOP
//      if i <= n
//          R2 = R2 + r1
//          goto LOOP
//      else
//          goto END
//  END

    @R0
    D=M
    @n
    M=D // n = R0
    @R1
    D=M
    @r1
    M=D
    @R2
    M=0 // R2 = 0
    @i
    M=1 // i = 1
    @n
    D=M
    @LOOP
    0;JGE // if n >=0 goto LOOP
// else
    @n
    M=-M // n = -n
    @r1
    M=-M // r1 = -r1
(LOOP)
    @i
    D=M
    @n
    D=D-M
    @END
    D;JGT // if i > n goto END
    @R2
    D=M
    @r1
    D=D+M
    @R2
    M=D // R2 = R2 + r1
    @i
    M=M+1 // i = i + 1
    @LOOP
    0;JMP
(END)
    @END
    0;JMP