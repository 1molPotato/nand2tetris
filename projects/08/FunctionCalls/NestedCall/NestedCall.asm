	@256
	D=A
	@SP
	M=D
// call Sys.init 0
	@$ret.1
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@LCL
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@ARG
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@THIS
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@THAT
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@5
	D=A
	@SP
	D=M-D
	@ARG
	M=D
	@SP
	D=M
	@LCL
	M=D
	@Sys.init
	0;JMP
($ret.1)
// funtion Sys.init 0
(Sys.init)
// push constant 4000
	@4000
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop pointer 0
	@SP
	AM=M-1
	D=M
	@R3
	M=D
// push constant 5000
	@5000
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop pointer 1
	@SP
	AM=M-1
	D=M
	@R4
	M=D
// call Sys.main 0
	@Sys.init$ret.1
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@LCL
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@ARG
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@THIS
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@THAT
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@5
	D=A
	@SP
	D=M-D
	@ARG
	M=D
	@SP
	D=M
	@LCL
	M=D
	@Sys.main
	0;JMP
(Sys.init$ret.1)
// pop temp 1
	@SP
	AM=M-1
	D=M
	@R6
	M=D
// label Sys.init$LOOP declaration
(Sys.init$LOOP)
// jump to Sys.init$LOOP
	@Sys.init$LOOP
	0;JMP
// funtion Sys.main 5
(Sys.main)
// push constant 0
	@0
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push constant 0
	@0
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push constant 0
	@0
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push constant 0
	@0
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push constant 0
	@0
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push constant 4001
	@4001
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop pointer 0
	@SP
	AM=M-1
	D=M
	@R3
	M=D
// push constant 5001
	@5001
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop pointer 1
	@SP
	AM=M-1
	D=M
	@R4
	M=D
// push constant 200
	@200
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop local 1
	@1
	D=A
	@LCL
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// push constant 40
	@40
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop local 2
	@2
	D=A
	@LCL
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// push constant 6
	@6
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop local 3
	@3
	D=A
	@LCL
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// push constant 123
	@123
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// call Sys.add12 1
	@Sys.main$ret.1
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@LCL
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@ARG
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@THIS
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@THAT
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
	@6
	D=A
	@SP
	D=M-D
	@ARG
	M=D
	@SP
	D=M
	@LCL
	M=D
	@Sys.add12
	0;JMP
(Sys.main$ret.1)
// pop temp 0
	@SP
	AM=M-1
	D=M
	@R5
	M=D
// push local 0
	@0
	D=A
	@LCL
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push local 1
	@1
	D=A
	@LCL
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push local 2
	@2
	D=A
	@LCL
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push local 3
	@3
	D=A
	@LCL
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push local 4
	@4
	D=A
	@LCL
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// add
	@SP
	AM=M-1
	D=M
	A=A-1
	M=D+M
// add
	@SP
	AM=M-1
	D=M
	A=A-1
	M=D+M
// add
	@SP
	AM=M-1
	D=M
	A=A-1
	M=D+M
// add
	@SP
	AM=M-1
	D=M
	A=A-1
	M=D+M
// function Sys.main return
	@LCL
	D=M
	@R13
	M=D
	@5
	D=A
	@R13
	A=M-D
	D=M
	@R14
	M=D
	@SP
	A=M-1
	D=M
	@ARG
	A=M
	M=D
	D=A
	@SP
	M=D+1
	@1
	D=A
	@R13
	A=M-D
	D=M
	@THAT
	M=D
	@2
	D=A
	@R13
	A=M-D
	D=M
	@THIS
	M=D
	@3
	D=A
	@R13
	A=M-D
	D=M
	@ARG
	M=D
	@4
	D=A
	@R13
	A=M-D
	D=M
	@LCL
	M=D
	@R14
	A=M
	0;JMP
// funtion Sys.add12 0
(Sys.add12)
// push constant 4002
	@4002
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop pointer 0
	@SP
	AM=M-1
	D=M
	@R3
	M=D
// push constant 5002
	@5002
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop pointer 1
	@SP
	AM=M-1
	D=M
	@R4
	M=D
// push argument 0
	@0
	D=A
	@ARG
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push constant 12
	@12
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// add
	@SP
	AM=M-1
	D=M
	A=A-1
	M=D+M
// function Sys.add12 return
	@LCL
	D=M
	@R13
	M=D
	@5
	D=A
	@R13
	A=M-D
	D=M
	@R14
	M=D
	@SP
	A=M-1
	D=M
	@ARG
	A=M
	M=D
	D=A
	@SP
	M=D+1
	@1
	D=A
	@R13
	A=M-D
	D=M
	@THAT
	M=D
	@2
	D=A
	@R13
	A=M-D
	D=M
	@THIS
	M=D
	@3
	D=A
	@R13
	A=M-D
	D=M
	@ARG
	M=D
	@4
	D=A
	@R13
	A=M-D
	D=M
	@LCL
	M=D
	@R14
	A=M
	0;JMP
