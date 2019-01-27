// push argument 1
	@1
	D=A
	@ARG
	A=D+M
	D=M
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
// push constant 0
	@0
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop that 0
	@0
	D=A
	@THAT
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// push constant 1
	@1
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// pop that 1
	@1
	D=A
	@THAT
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
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
// push constant 2
	@2
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// sub
	@SP
	A=M-1
	M=-M
	@SP
	AM=M-1
	D=M
	A=A-1
	M=D+M
// pop argument 0
	@0
	D=A
	@ARG
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// label MAIN_LOOP_START declaration
(MAIN_LOOP_START)
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
// if pop is true, jump to COMPUTE_ELEMENT
	@SP
	AM=M-1
	D=M
	@COMPUTE_ELEMENT
	D;JNE
// jump to END_PROGRAM
	@END_PROGRAM
	0;JMP
// label COMPUTE_ELEMENT declaration
(COMPUTE_ELEMENT)
// push that 0
	@0
	D=A
	@THAT
	A=D+M
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push that 1
	@1
	D=A
	@THAT
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
// pop that 2
	@2
	D=A
	@THAT
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// push pointer 1
	@R4
	D=M
	@SP
	A=M
	M=D
	@SP
	M=M+1
// push constant 1
	@1
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
// push constant 1
	@1
	D=A
	@SP
	A=M
	M=D
	@SP
	M=M+1
// sub
	@SP
	A=M-1
	M=-M
	@SP
	AM=M-1
	D=M
	A=A-1
	M=D+M
// pop argument 0
	@0
	D=A
	@ARG
	D=D+M
	@R13
	M=D
	@SP
	AM=M-1
	D=M
	@R13
	A=M
	M=D
// jump to MAIN_LOOP_START
	@MAIN_LOOP_START
	0;JMP
// label END_PROGRAM declaration
(END_PROGRAM)
