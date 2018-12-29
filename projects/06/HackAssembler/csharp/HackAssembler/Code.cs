using System;

namespace HackAssembler
{
    public class Code
    {
        public static string Dest(string dest)
        {
            char[] retChars = {'0', '0', '0'};
            if (dest != null)
            {
                if (dest.Contains('M'))
                    retChars[2] = '1';
                if (dest.Contains('D'))
                    retChars[1] = '1';
                if (dest.Contains('A'))
                    retChars[0] = '1';
            }            
            return new string(retChars);
        }

        public static string Comp(string comp)
        {
            string a = "0";
            if (comp.Contains('M'))
            {
                a = "1";
                comp = comp.Replace('M', 'A');
            }                        
            string cs = "";
            switch (comp)
            {
                case "0":   cs = "101010"; break;
                case "1":   cs = "111111"; break;
                case "-1":  cs = "111010"; break;
                case "D":   cs = "001100"; break;
                case "A":   cs = "110000"; break;
                case "!D":  cs = "001101"; break;
                case "!A":  cs = "110001"; break;
                case "-D":  cs = "001111"; break;
                case "-A":  cs = "110011"; break;
                case "D+1": cs = "011111"; break;
                case "A+1": cs = "110111"; break;
                case "D-1": cs = "001110"; break;
                case "A-1": cs = "110010"; break;
                case "D+A": cs = "000010"; break;
                case "D-A": cs = "010011"; break;
                case "A-D": cs = "000111"; break;
                case "D&A": cs = "000000"; break;
                case "D|A": cs = "010101"; break;
                default: break; // you should never get here
            }
            return string.Format($"{a}{cs}");
        }

        public static string Jump(string jump)
        {
            char[] retChars = {'0', '0', '0'};
            if (jump != null)
            {
                if (jump.Contains('G') || jump.Contains('N') || jump.Contains('P'))
                    retChars[2] = '1';
                if ((jump.Contains('E') && !jump.Contains('N')) || jump.Contains('P'))
                    retChars[1] = '1';
                if (jump.Contains('L') || jump.Contains('N') || jump.Contains('P'))
                    retChars[0] = '1';
            }
            return new string(retChars);
        }
    }
}