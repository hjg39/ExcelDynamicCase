using System.Collections.Generic;
using System.Diagnostics;

namespace ExcelDynamicCase.Domain.CaseQuestions
{
    public static class CaseQuestionRepo
    {
        private static readonly object[,] _snakeGrid = new object[,]
        {
            {"",1d,2d,3d,4d,5d,6d,7d,8d,9d,10d,11d,12d,13d,14d,15d,16d,17d,18d,19d,20d,},
            {"A","","","","","","","","","","","","","","","","","","","","",},
            {"B","","","","","","","","","S","S","S","S","S","","","","","","","",},
            {"C","","","","","","","","","","","","","S","","","","","","","",},
            {"D","","","","","","","","","","","","","S","","","","","","","",},
            {"E","","","","","","","","","","","","","","","","","","","","",},
            {"F","","","","","","","","","","","","","","","","","","","","",},
            {"G","","","","","","","","","","","","","","","","","","","","",},
            {"H","","","","","","","","","","","","","","","","","","","","",},
            {"I","","","","","","","","","","","","","","","","","","","","",},
            {"J","","","","","","","","","","","","","","","","","","","","",},
            {"K","","","","","","","","","","","","","","","","","","","","",},
            {"L","","","","","","","","","","","","","","","","","","","","",},
        };

        private static readonly object[,] _socialNetwork = new object[,]
        {
            {"#","","Aaron","Anna","Betty","Boris","Charlie","Charlotte","Delia","Derek","Edward","Elena","Fiona","Fred","Gary","Gertrude","Harley","Henry","India","Ivan","Joanna","Johnny","Kara","Kieran","Lara","Leon","Mickey","Moana","Niall","Nicki","Olivia","Owen","Patrick","Penny","Rebecca","Rufus","Sarah","Stuart","Tamsin","Theo","Ulysses","Uma","Valerie","Viktor","Willem","Wynn","Xavier","Ximena","Yasmin","Yavin","Zack","Zoey",},
            {"1","Aaron","","","👥","","","","★","","","👥","👥","","","","","👥","","","","","","★","","","","","","","","👥","","","","","👥","","","👥","👥","","","","","👥","","","","","","",},
            {"2","Anna","","","👥","","","","👥","","","","👥","","","","","","","👥","👥","","★","","","","","","","","","","","","","","","","","","","","","","","★","","","","👥","","",},
            {"3","Betty","👥","👥","","","👥","","","","","","","","👥","","👥","★","","","","","👥","","","","","","","","★","👥","","","","","","","","","","","","","","","","","","","","",},
            {"4","Boris","","","","","","","","","","","","","","👥","","","","👥","","","","","","👥","","","","👥","","","","","","","","","👥","👥","","","👥","👥","","","","","★","","","",},
            {"5","Charlie","","","👥","","","","★","","👥","👥","","","","👥","","","","","","","","","","","👥","👥","","","","","","👥","","","👥","","","","","","","★","","👥","","👥","","","","",},
            {"6","Charlotte","","","","","","","","","","","","👥","👥","👥","","","","","","","👥","","","","","","👥","","👥","","","👥","","","","","","","","","","","","","","","","","","",},
            {"7","Delia","★","👥","","","★","","","","","👥","","","","","","","👥","","★","","👥","","","👥","","","","","","","","","👥","👥","👥","","","","","","","","","","👥","","","","👥","",},
            {"8","Derek","","","","","","","","","","","","","","","","","👥","👥","","","👥","","","","👥","","","","","👥","","","","","","","","","","👥","","","👥","👥","","","👥","👥","👥","",},
            {"9","Edward","","","","","👥","","","","","","","","","","","","","","","★","","","","★","","","","","👥","","","","","","👥","","","","","","","","","👥","","","★","★","","",},
            {"10","Elena","👥","","","","👥","","👥","","","","","","👥","","★","","","👥","","👥","","👥","👥","","","","","","","","","👥","","👥","","","","","","","","","👥","","","👥","","","","",},
            {"11","Fiona","👥","👥","","","","","","","","","","","👥","","","★","","","","","","","","","","👥","👥","","★","","","","","","","","","","","","","","","👥","","","","","","",},
            {"12","Fred","","","","","","👥","","","","","","","","","","👥","","👥","👥","","","","","","","","","","","★","","👥","","","★","👥","","","","","👥","","","","","","","","","",},
            {"13","Gary","","","👥","","","👥","","","","👥","👥","","","","","","","","","","★","","","","","","","👥","","","","★","","","","","","","","","","","","","","","","","👥","",},
            {"14","Gertrude","","","","👥","👥","👥","","","","","","","","","","👥","","","👥","","","","","","","","","","","","👥","👥","👥","👥","","","","","","","","","","👥","","","","","👥","",},
            {"15","Harley","","","👥","","","","","","","★","","","","","","","","","","","","","👥","","","","","","","","","","","","","","","","","","","","👥","👥","","","👥","👥","","",},
            {"16","Henry","👥","","★","","","","","","","","★","👥","","👥","","","","","","","👥","","","","","","","","","","","","","","","","","👥","","","","","👥","","","","","","","★",},
            {"17","India","","","","","","","👥","👥","","","","","","","","","","","","","","","","","","","","","","","","","","","","👥","","","👥","","","👥","","","","","👥","","","",},
            {"18","Ivan","","👥","","👥","","","","👥","","👥","","👥","","","","","","","","","","","👥","","","★","","","","","","","","👥","","","","","","","","","","","","👥","","","","",},
            {"19","Joanna","","👥","","","","","★","","","","","👥","","👥","","","","","","","","","","","","","👥","👥","","","","","👥","","","","","","","👥","","","","","","","","👥","","",},
            {"20","Johnny","","","","","","","","","★","👥","","","","","","","","","","","","","★","","","","","","","","","","","","","","","","","","","","","👥","","","","👥","","",},
            {"21","Kara","","★","👥","","","👥","👥","👥","","","","","★","","","👥","","","","","","","","","","","","","","","","","","","","","","","","","👥","","★","★","","","★","","★","",},
            {"22","Kieran","★","","","","","","","","","👥","","","","","","","","","","","","","","","","","","👥","","👥","","","","","","","★","👥","","","","★","","","","","","","","",},
            {"23","Lara","","","","","","","","","","👥","","","","","👥","","","👥","","★","","","","","","","","👥","","","","","","","","","","","","","👥","","👥","","","","","","👥","",},
            {"24","Leon","","","","👥","","","👥","","★","","","","","","","","","","","","","","","","","","👥","👥","👥","","","","","","","","","","👥","","","","","","","","👥","","","",},
            {"25","Mickey","","","","","👥","","","👥","","","","","","","","","","","","","","","","","","","","","","","","👥","👥","","","","","👥","","👥","","","","","","","","","","",},
            {"26","Moana","","","","","👥","","","","","","👥","","","","","","","★","","","","","","","","","","","","","","👥","","","","","","★","","","","","","","","","","","","",},
            {"27","Niall","","","","","","👥","","","","","👥","","","","","","","","👥","","","","","👥","","","","","","","","","","","","","","","","","","👥","","","","","","","","",},
            {"28","Nicki","","","","👥","","","","","","","","","👥","","","","","","👥","","","👥","👥","👥","","","","","","","👥","","","","","","👥","","","","","","","★","","","👥","","","",},
            {"29","Olivia","","","★","","","👥","","","👥","","★","","","","","","","","","","","","","👥","","","","","","","","","","","","","★","","","","","","👥","","","","","","","",},
            {"30","Owen","👥","","👥","","","","","👥","","","","★","","","","","","","","","","👥","","","","","","","","","","","","","","👥","","","","","","","👥","","","","","","★","",},
            {"31","Patrick","","","","","","","","","","","","","","👥","","","","","","","","","","","","","","👥","","","","","","👥","","","","","👥","👥","👥","","","","","","","","","",},
            {"32","Penny","","","","","👥","👥","","","","👥","","👥","★","👥","","","","","","","","","","","👥","👥","","","","","","","","","👥","","","","","","👥","","","👥","","👥","","","","",},
            {"33","Rebecca","","","","","","","👥","","","","","","","👥","","","","","👥","","","","","","👥","","","","","","","","","","","","","","","","","","","","","","","","","",},
            {"34","Rufus","","","","","","","👥","","","👥","","","","👥","","","","👥","","","","","","","","","","","","","👥","","","","★","👥","","","👥","","","👥","","","","","👥","★","","",},
            {"35","Sarah","👥","","","","👥","","👥","","👥","","","★","","","","","","","","","","","","","","","","","","","","👥","","★","","","👥","👥","","★","👥","👥","","","","★","","👥","👥","",},
            {"36","Stuart","","","","","","","","","","","","👥","","","","","👥","","","","","","","","","","","","","👥","","","","👥","","","👥","👥","","","","","","","","★","","","","",},
            {"37","Tamsin","","","","👥","","","","","","","","","","","","","","","","","","★","","","","","","👥","★","","","","","","👥","👥","","","","","","","","","★","★","","","","",},
            {"38","Theo","👥","","","👥","","","","","","","","","","","","👥","","","","","","👥","","","👥","★","","","","","","","","","👥","👥","","","","","👥","","","","","★","","","","",},
            {"39","Ulysses","👥","","","","","","","","","","","","","","","","👥","","","","","","","👥","","","","","","","👥","","","👥","","","","","","👥","","","","","","","","👥","","",},
            {"40","Uma","","","","","","","","👥","","","","","","","","","","","👥","","","","","","👥","","","","","","👥","","","","★","","","","👥","","","","","","","","","","","",},
            {"41","Valerie","","","","👥","","","","","","","","👥","","","","","","","","","👥","","👥","","","","","","","","👥","👥","","","👥","","","👥","","","","","","","👥","","👥","","","",},
            {"42","Viktor","","","","👥","★","","","","","","","","","","","","👥","","","","","★","","","","","👥","","","","","","","👥","👥","","","","","","","","","","","","","","","",},
            {"43","Willem","","","","","","","","👥","","👥","","","","","👥","👥","","","","","★","","👥","","","","","","👥","👥","","","","","","","","","","","","","","👥","","","","","","",},
            {"44","Wynn","👥","★","","","👥","","","👥","👥","","👥","","","👥","👥","","","","","👥","★","","","","","","","★","","","","👥","","","","","","","","","","","👥","","","★","👥","","","",},
            {"45","Xavier","","","","","","","👥","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","★","","","","👥","","","","","👥","","","","",},
            {"46","Ximena","","","","","👥","","","","","👥","","","","","","","","👥","","","","","","","","","","","","","","👥","","","★","★","★","★","","","","","","★","👥","","","","","",},
            {"47","Yasmin","","","","★","","","","👥","★","","","","","","👥","","👥","","","","★","","","👥","","","","👥","","","","","","👥","","","","","","","👥","","","👥","","","","","","",},
            {"48","Yavin","","👥","","","","","","👥","★","","","","","","👥","","","","👥","👥","","","","","","","","","","","","","","★","👥","","","","👥","","","","","","","","","","","",},
            {"49","Zack","","","","","","","👥","👥","","","","","👥","👥","","","","","","","★","","👥","","","","","","","★","","","","","👥","","","","","","","","","","","","","","","",},
            {"50","Zoey","","","","","","","","","","","","","","","","★","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","",},
        };

        public static readonly object[,] _lanaBanana = new object[,]
        {
            {null,"B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","AA","AB","AC","AD","AE","AF","AG","AH","AI","AJ","AK","AL","AM","AN",},
            {2d,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,},
            {3d,null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,"🍌","🍌",null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,"🍌",null,},
            {4d,"🍌",null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,"🍌",null,null,null,"🍌",null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,null,"🍌","🍌",},
            {5d,null,null,"🍌",null,"🍌",null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,"🍌","🍌",null,null,null,null,"🍌",null,null,null,null,null,null,"🍌","🍌",null,null,null,null,"🍌",},
            {6d,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,"🍌","🍌",null,null,null,null,null,},
            {7d,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,"🍌","🍌","🍌","🍌","🍌",null,null,null,"🍌",null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,null,"🍌",null,null,null,},
            {8d,null,"🍌","🍌","🍌",null,null,"🍌",null,"🍌",null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,},
            {9d,"🍌",null,"🍌",null,"🍌",null,"🍌",null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,"🍌",null,null,"🍌",null,null,null,"🍌",null,null,null,null,null,null,"🍌",null,null,},
            {10d,null,null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,"🍌","🍌",null,"🍌",null,null,null,null,"🍌",},
            {11d,null,null,null,null,null,"🍌",null,null,null,"🍌",null,null,null,"🍌",null,null,null,"🍌",null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,},
            {12d,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,"🍌",},
            {13d,null,null,null,null,null,"🍌",null,"🍌",null,null,null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,"🍌",null,null,null,},
            {14d,null,null,"🍌","🍌",null,null,"🍌",null,null,null,null,"🍌",null,"🍌",null,null,null,"🍌","🍌",null,"🍌","🍌",null,null,null,null,null,null,null,"🍌",null,null,null,null,"🍌",null,null,null,null,},
            {15d,null,null,null,"🍌",null,null,null,"🍌",null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,"🍌",null,null,null,"🍌",null,null,},
            {16d,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,"🍌",null,null,null,"🍌","🍌",null,"🍌",null,null,null,null,null,null,},
            {17d,null,null,null,"🍌",null,"🍌",null,null,null,null,null,null,"🍌","🍌",null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,"🍌","🍌",null,null,null,"🍌","🍌",null,null,},
            {18d,null,null,null,null,"🍌",null,null,"🍌",null,"🍌",null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,"🍌","🍌",null,null,},
            {19d,null,null,null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,null,"🍌",null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,"🍌",null,null,},
            {20d,"🍌",null,"🍌","🍌",null,null,null,null,null,"🍌","🍌","🍌",null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,"🍌",null,null,"🍌",null,null,},
            {21d,null,null,null,null,null,null,null,null,null,"🍌",null,null,"🍌",null,"🍌",null,"🍌",null,"🍌",null,null,null,null,null,null,null,"🍌",null,"🍌","🍌",null,null,null,null,null,null,null,null,null,},
            {22d,null,"🍌","🍌",null,"🍌","🍌","🍌",null,null,"🍌","🍌",null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,},
            {23d,null,null,null,null,null,"🍌","🍌","🍌",null,null,null,null,null,null,"🍌",null,null,null,null,null,"🍌",null,null,null,"🍌",null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,null,},
            {24d,null,null,null,"🍌",null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,"🍌","🍌","🍌",null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,},
            {25d,null,null,null,null,null,null,null,"🍌",null,"🍌",null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,"🍌","🍌",null,null,null,null,null,null,null,null,null,"🍌",null,},
            {26d,null,null,null,null,null,null,null,null,null,"🍌",null,"🍌",null,null,"🍌",null,null,null,null,"🍌",null,null,null,null,null,null,null,"🍌","🍌",null,null,null,null,null,null,null,null,null,null,},
            {27d,null,"🍌",null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,"🍌",null,"🍌",null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,"🍌",null,null,null,},
            {28d,null,null,null,null,null,null,"🍌",null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,"🍌",null,"🍌",},
            {29d,"🍌",null,null,null,null,null,"🍌",null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,"🍌",null,null,null,"🍌",null,null,null,null,"🍌",null,null,},
            {30d,null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,"🍌","🍌",null,null,null,null,null,"🍌",null,"🍌",null,null,null,null,"🍌","🍌",null,null,"🍌",null,null,null,null,null,null,null,null,},
            {31d,null,null,null,"🍌","🍌","🍌",null,null,"🍌",null,null,null,null,"🍌",null,null,null,null,null,null,null,null,"🍌","🍌",null,null,null,null,null,null,"🍌",null,null,null,null,"🍌",null,"🍌",null,},
            {32d,null,null,null,null,null,null,null,null,null,null,"🍌","🍌",null,"🍌",null,null,null,null,"🍌",null,null,null,null,"🍌",null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,},
            {33d,null,null,null,"🍌",null,"🍌",null,null,null,"🍌",null,null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,"🍌",null,null,"🍌",null,null,null,"🍌",null,null,null,null,null,null,"🍌",null,},
            {34d,"🍌","🍌",null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,"🍌","🍌",null,null,"🍌",null,null,"🍌",null,null,null,"🍌",null,null,},
            {35d,null,null,null,"🍌",null,"🍌",null,null,null,null,"🍌","🍌","🍌",null,null,null,"🍌",null,"🍌",null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,},
            {36d,null,null,null,null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,},
            {37d,null,null,"🍌","🍌",null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,null,null,},
            {38d,null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,"🍌","🍌",null,null,null,null,null,null,"🍌",null,null,null,null,"🍌",null,null,},
            {39d,"🍌",null,null,null,"🍌",null,null,null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,null,null,null,null,"🍌",null,null,null,null,null,null,"🍌",},
            {40d,null,"🍌",null,null,"🍌",null,null,null,"🍌","🍌",null,null,null,null,null,null,"🍌",null,null,null,null,"🍌",null,null,"🍌",null,null,null,null,"🍌",null,null,null,"🍌",null,null,null,null,null,},
        }

        public static Dictionary<CaseQuestionEnum, CaseQuestion> CaseQuestions = new Dictionary<CaseQuestionEnum, CaseQuestion>()
        {
            {
                CaseQuestionEnum.EulerMultiplesThreeFive,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerMultiplesThreeFive,
                    QuestionText = "If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.\r\n\r\nFind the sum of all the multiples of 3 or 5 below 1000.",
                    QuestionLink = "https://projecteuler.net/problem=1",
                    Data = null,
                    Answer = "233168",
                    ExampleAnswer = 10000d,
                    Minutes = 5,
                }
            },
            {
                CaseQuestionEnum.EulerEvenFibonacci,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerEvenFibonacci,
                    QuestionText = "Each new term in the Fibonacci sequence is generated by adding the previous two terms. By starting with 1 and 2, the first 10 terms will be:\r\n\r\n1,2, 3, 5, 8, 13, 21, 34, 55, 89...\r\n\r\nBy considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.",
                    QuestionLink = "https://projecteuler.net/problem=2",
                    Data = null,
                    Answer = "4613732",
                    ExampleAnswer = 231231d,
                    Minutes = 5,
                }
            },
            {
                CaseQuestionEnum.EulerPrimeFactorisation,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerPrimeFactorisation,
                    QuestionText = "The prime factors of 13195 are 5, 7, 13 and 29.\r\nWhat is the largest prime factor of the number 600851475143?",
                    QuestionLink = "https://projecteuler.net/problem=3",
                    Data = null,
                    Answer = "6857",
                    ExampleAnswer = 17d,
                    Minutes = 3,
                }
            },
            {
                CaseQuestionEnum.EulerLargestPalindromeProduct,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerLargestPalindromeProduct,
                    QuestionText = "A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 x 99.\r\n\r\nFind the largest palindrome made from the product of two 3-digit numbers.",
                    QuestionLink = "https://projecteuler.net/problem=4",
                    Data = null,
                    Answer = "906609",
                    ExampleAnswer = 999999d,
                    Minutes = 3,
                }
            },
            {
                CaseQuestionEnum.EulerEvenlyDivisibleByManyNumbers,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerEvenlyDivisibleByManyNumbers,
                    QuestionText = "2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.\r\n\r\nWhat is the smallest positive number that is dividible by all numbers from 1 to 20 with no remainder?",
                    QuestionLink = "https://projecteuler.net/problem=5",
                    Data = null,
                    Answer = "232792560",
                    ExampleAnswer = 321312d,
                    Minutes = 3,
                }
            },
            {
                CaseQuestionEnum.EulerSumOfSquaresVsSquareOfSum,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerSumOfSquaresVsSquareOfSum,
                    QuestionText = "The sum of the squares of the first 10 natural numbers is 1^2 + 2^2 + ... + 10^2 = 385\r\n\r\nThe square of the sum of the first 10 natural numbers is (1 + 2 + ... + 10)^2 = 55^2 = 3025.\r\nHence the difference is 3025 - 385 = 2640.\r\n\r\nFind the difference between the sum of the squares of the first 100 natural numbers and the square of the sum.",
                    QuestionLink = "https://projecteuler.net/problem=6",
                    Data = null,
                    Answer = "25164150",
                    ExampleAnswer = 3213129d,
                    Minutes = 3,
                }
            },
            {
                CaseQuestionEnum.Euler10001stPrime,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.Euler10001stPrime,
                    QuestionText = "By listing the first 6 prime numbers 2, 3, 5, 7, 11 and 13 we can see that the 6th prime is 13.\r\n\r\nWhat is the 10,001st prime number?",
                    QuestionLink = "https://projecteuler.net/problem=7",
                    Data = null,
                    Answer = "104743",
                    ExampleAnswer = 31237911d,
                    Minutes = 10,
                }
            },
            {
                CaseQuestionEnum.EulerDigitProduct,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerDigitProduct,
                    QuestionText = "The 4 adjacent digits in this 1000-digit number that have the greatest product are 9x9x8x9 = 5832.\r\nFind the 13 adjacent digits that have the greatest product, what is their product?\r\n\r\n7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450",
                    QuestionLink = "https://projecteuler.net/problem=8",
                    Data = null,
                    Answer = "23514624000",
                    ExampleAnswer = 2310310d,
                    Minutes = 5,
                }
            },
            {
                CaseQuestionEnum.EulerSpecialPythagoreanTriplet,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerSpecialPythagoreanTriplet,
                    QuestionText = "A Pythagorean triplet is a set of 3 natural numbers a < b < c for which a^2 + b^2 = c^2.\r\n\r\nFor example, 3^2+4^2=9+16=25=5^2.\r\n\r\nThere exists exactly one Pythagorean triplet for which a+b+c=1000.\r\nFind the product abc.",
                    QuestionLink = "https://projecteuler.net/problem=9",
                    Data = null,
                    Answer = "31875000",
                    ExampleAnswer = 3217390127d,
                    Minutes = 5,
                }
            },
            {
                CaseQuestionEnum.EulerLargestProductInAGrid,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerLargestProductInAGrid,
                    QuestionText = "What is the greatest product of four adjacent numbers in the same direction (up, down, left, right, or diagonally) in the 20x20 grid?",
                    QuestionLink = "https://projecteuler.net/problem=11",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Grid",
                            new object[,]
                            {
                                {8d, 2d, 22d, 97d, 38d, 15d, 0d, 40d, 0d, 75d, 4d, 5d, 7d, 78d, 52d, 12d, 50d, 77d, 91d, 8d},
                                {49d, 49d, 99d, 40d, 17d, 81d, 18d, 57d, 60d, 87d, 17d, 40d, 98d, 43d, 69d, 48d, 4d, 56d, 62d, 0d},
                                {81d, 49d, 31d, 73d, 55d, 79d, 14d, 29d, 93d, 71d, 40d, 67d, 53d, 88d, 30d, 3d, 49d, 13d, 36d, 65d},
                                {52d, 70d, 95d, 23d, 4d, 60d, 11d, 42d, 69d, 24d, 68d, 56d, 1d, 32d, 56d, 71d, 37d, 2d, 36d, 91d},
                                {22d, 31d, 16d, 71d, 51d, 67d, 63d, 89d, 41d, 92d, 36d, 54d, 22d, 40d, 40d, 28d, 66d, 33d, 13d, 80d},
                                {24d, 47d, 32d, 60d, 99d, 3d, 45d, 2d, 44d, 75d, 33d, 53d, 78d, 36d, 84d, 20d, 35d, 17d, 12d, 50d},
                                {32d, 98d, 81d, 28d, 64d, 23d, 67d, 10d, 26d, 38d, 40d, 67d, 59d, 54d, 70d, 66d, 18d, 38d, 64d, 70d},
                                {67d, 26d, 20d, 68d, 2d, 62d, 12d, 20d, 95d, 63d, 94d, 39d, 63d, 8d, 40d, 91d, 66d, 49d, 94d, 21d},
                                {24d, 55d, 58d, 5d, 66d, 73d, 99d, 26d, 97d, 17d, 78d, 78d, 96d, 83d, 14d, 88d, 34d, 89d, 63d, 72d},
                                {21d, 36d, 23d, 9d, 75d, 0d, 76d, 44d, 20d, 45d, 35d, 14d, 0d, 61d, 33d, 97d, 34d, 31d, 33d, 95d},
                                {78d, 17d, 53d, 28d, 22d, 75d, 31d, 67d, 15d, 94d, 3d, 80d, 4d, 62d, 16d, 14d, 9d, 53d, 56d, 92d},
                                {16d, 39d, 5d, 42d, 96d, 35d, 31d, 47d, 55d, 58d, 88d, 24d, 0d, 17d, 54d, 24d, 36d, 29d, 85d, 57d},
                                {86d, 56d, 0d, 48d, 35d, 71d, 89d, 7d, 5d, 44d, 44d, 37d, 44d, 60d, 21d, 58d, 51d, 54d, 17d, 58d},
                                {19d, 80d, 81d, 68d, 5d, 94d, 47d, 69d, 28d, 73d, 92d, 13d, 86d, 52d, 17d, 77d, 4d, 89d, 55d, 40d},
                                {4d, 52d, 8d, 83d, 97d, 35d, 99d, 16d, 7d, 97d, 57d, 32d, 16d, 26d, 26d, 79d, 33d, 27d, 98d, 66d},
                                {88d, 36d, 68d, 87d, 57d, 62d, 20d, 72d, 3d, 46d, 33d, 67d, 46d, 55d, 12d, 32d, 63d, 93d, 53d, 69d},
                                {4d, 42d, 16d, 73d, 38d, 25d, 39d, 11d, 24d, 94d, 72d, 18d, 8d, 46d, 29d, 32d, 40d, 62d, 76d, 36d},
                                {20d, 69d, 36d, 41d, 72d, 30d, 23d, 88d, 34d, 62d, 99d, 69d, 82d, 67d, 59d, 85d, 74d, 4d, 36d, 16d},
                                {20d, 73d, 35d, 29d, 78d, 31d, 90d, 1d, 74d, 31d, 49d, 71d, 48d, 86d, 81d, 16d, 23d, 57d, 5d, 54d},
                                {1d, 70d, 54d, 71d, 83d, 51d, 54d, 69d, 16d, 92d, 33d, 48d, 61d, 43d, 52d, 1d, 89d, 19d, 67d, 48d},
                            }
                        }
                    },
                    Answer = "70600674",
                    ExampleAnswer = 3219037d,
                    Minutes = 5,
                }
            },
            {
                CaseQuestionEnum.EulerHighlyDivisibleTriangleNumber,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerHighlyDivisibleTriangleNumber,
                    QuestionText = "The sequence of triangle numbers if generated by adding consecutive natural numbers starting at 1.  For example, the 7th triangle number is 1+2+...+7=28.\r\n\r\nThe first 7 triangle numbers are 1, 3, 6, 10, 15, 21, 28, ...\r\n\r\nThe factors of the first 7 triangle numbers are:\r\n1: 1\r\n3: 1,3\r\n6: 1,2,3,6\r\n10: 1,2,5,10\r\n15: 1,3,5,15\r\n21: 1,3,7,21\r\n28: 1,2,4,7,14,28\r\n\r\nWe see that 28 is the first triangle number to have over 5 divisors.\r\n\r\nWhat is the value of the first triangle number to have over 500 divisors?",
                    QuestionLink = "https://projecteuler.net/problem=12",
                    Data = null,
                    Answer = "76576500",
                    ExampleAnswer = 321031d,
                    Minutes = 10,
                }
            },
            {
                CaseQuestionEnum.EulerLargeSum,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerLargeSum,
                    QuestionText = "Work out the first ten digits of the sum of the following 100 50-digit numbers.",
                    QuestionLink = "https://projecteuler.net/problem=13",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "ItemsToSum",
                            new object[,]
                            {
                                {"'37107287533902102798797998220837590246510135740250"},
                                {"'46376937677490009712648124896970078050417018260538"},
                                {"'74324986199524741059474233309513058123726617309629"},
                                {"'91942213363574161572522430563301811072406154908250"},
                                {"'23067588207539346171171980310421047513778063246676"},
                                {"'89261670696623633820136378418383684178734361726757"},
                                {"'28112879812849979408065481931592621691275889832738"},
                                {"'44274228917432520321923589422876796487670272189318"},
                                {"'47451445736001306439091167216856844588711603153276"},
                                {"'70386486105843025439939619828917593665686757934951"},
                                {"'62176457141856560629502157223196586755079324193331"},
                                {"'64906352462741904929101432445813822663347944758178"},
                                {"'92575867718337217661963751590579239728245598838407"},
                                {"'58203565325359399008402633568948830189458628227828"},
                                {"'80181199384826282014278194139940567587151170094390"},
                                {"'35398664372827112653829987240784473053190104293586"},
                                {"'86515506006295864861532075273371959191420517255829"},
                                {"'71693888707715466499115593487603532921714970056938"},
                                {"'54370070576826684624621495650076471787294438377604"},
                                {"'53282654108756828443191190634694037855217779295145"},
                                {"'36123272525000296071075082563815656710885258350721"},
                                {"'45876576172410976447339110607218265236877223636045"},
                                {"'17423706905851860660448207621209813287860733969412"},
                                {"'81142660418086830619328460811191061556940512689692"},
                                {"'51934325451728388641918047049293215058642563049483"},
                                {"'62467221648435076201727918039944693004732956340691"},
                                {"'15732444386908125794514089057706229429197107928209"},
                                {"'55037687525678773091862540744969844508330393682126"},
                                {"'18336384825330154686196124348767681297534375946515"},
                                {"'80386287592878490201521685554828717201219257766954"},
                                {"'78182833757993103614740356856449095527097864797581"},
                                {"'16726320100436897842553539920931837441497806860984"},
                                {"'48403098129077791799088218795327364475675590848030"},
                                {"'87086987551392711854517078544161852424320693150332"},
                                {"'59959406895756536782107074926966537676326235447210"},
                                {"'69793950679652694742597709739166693763042633987085"},
                                {"'41052684708299085211399427365734116182760315001271"},
                                {"'65378607361501080857009149939512557028198746004375"},
                                {"'35829035317434717326932123578154982629742552737307"},
                                {"'94953759765105305946966067683156574377167401875275"},
                                {"'88902802571733229619176668713819931811048770190271"},
                                {"'25267680276078003013678680992525463401061632866526"},
                                {"'36270218540497705585629946580636237993140746255962"},
                                {"'24074486908231174977792365466257246923322810917141"},
                                {"'91430288197103288597806669760892938638285025333403"},
                                {"'34413065578016127815921815005561868836468420090470"},
                                {"'23053081172816430487623791969842487255036638784583"},
                                {"'11487696932154902810424020138335124462181441773470"},
                                {"'63783299490636259666498587618221225225512486764533"},
                                {"'67720186971698544312419572409913959008952310058822"},
                                {"'95548255300263520781532296796249481641953868218774"},
                                {"'76085327132285723110424803456124867697064507995236"},
                                {"'37774242535411291684276865538926205024910326572967"},
                                {"'23701913275725675285653248258265463092207058596522"},
                                {"'29798860272258331913126375147341994889534765745501"},
                                {"'18495701454879288984856827726077713721403798879715"},
                                {"'38298203783031473527721580348144513491373226651381"},
                                {"'34829543829199918180278916522431027392251122869539"},
                                {"'40957953066405232632538044100059654939159879593635"},
                                {"'29746152185502371307642255121183693803580388584903"},
                                {"'41698116222072977186158236678424689157993532961922"},
                                {"'62467957194401269043877107275048102390895523597457"},
                                {"'23189706772547915061505504953922979530901129967519"},
                                {"'86188088225875314529584099251203829009407770775672"},
                                {"'11306739708304724483816533873502340845647058077308"},
                                {"'82959174767140363198008187129011875491310547126581"},
                                {"'97623331044818386269515456334926366572897563400500"},
                                {"'42846280183517070527831839425882145521227251250327"},
                                {"'55121603546981200581762165212827652751691296897789"},
                                {"'32238195734329339946437501907836945765883352399886"},
                                {"'75506164965184775180738168837861091527357929701337"},
                                {"'62177842752192623401942399639168044983993173312731"},
                                {"'32924185707147349566916674687634660915035914677504"},
                                {"'99518671430235219628894890102423325116913619626622"},
                                {"'73267460800591547471830798392868535206946944540724"},
                                {"'76841822524674417161514036427982273348055556214818"},
                                {"'97142617910342598647204516893989422179826088076852"},
                                {"'87783646182799346313767754307809363333018982642090"},
                                {"'10848802521674670883215120185883543223812876952786"},
                                {"'71329612474782464538636993009049310363619763878039"},
                                {"'62184073572399794223406235393808339651327408011116"},
                                {"'66627891981488087797941876876144230030984490851411"},
                                {"'60661826293682836764744779239180335110989069790714"},
                                {"'85786944089552990653640447425576083659976645795096"},
                                {"'66024396409905389607120198219976047599490197230297"},
                                {"'64913982680032973156037120041377903785566085089252"},
                                {"'16730939319872750275468906903707539413042652315011"},
                                {"'94809377245048795150954100921645863754710598436791"},
                                {"'78639167021187492431995700641917969777599028300699"},
                                {"'15368713711936614952811305876380278410754449733078"},
                                {"'40789923115535562561142322423255033685442488917353"},
                                {"'44889911501440648020369068063960672322193204149535"},
                                {"'41503128880339536053299340368006977710650566631954"},
                                {"'81234880673210146739058568557934581403627822703280"},
                                {"'82616570773948327592232845941706525094512325230608"},
                                {"'22918802058777319719839450180888072429661980811197"},
                                {"'77158542502016545090413245809786882778948721859617"},
                                {"'72107838435069186155435662884062257473692284509516"},
                                {"'20849603980134001723930671666823555245252804609722"},
                                {"'53503534226472524250874054075591789781264330331690"},
                            }
                        }
                    },
                    Answer = "5537376230",
                    ExampleAnswer = "'3213912291",
                    Minutes = 15,
                }
            },
            {
                CaseQuestionEnum.EulerLatticePaths,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerLatticePaths,
                    QuestionText = "Starting in the top left corner of a 2x2 grid, and only being able to move right and down, there are exactly 6 routes to the bottom right corner.\r\n\r\nHow many such routes are there through a 20x20 grid? ",
                    QuestionLink = "https://projecteuler.net/problem=15",
                    Answer = "137846528820",
                    ExampleAnswer = "'321231912291",
                    Minutes = 10,
                }
            },
            {
                CaseQuestionEnum.EulerPowerDigitSum,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerPowerDigitSum,
                    QuestionText = "2^15=32768 and the sum of digits is 3+2+7+6+8=26.\r\n\r\nWhat is the sum of the digits of the number 2^1000?",
                    QuestionLink = "https://projecteuler.net/problem=16",
                    Answer = "1366",
                    ExampleAnswer = 9372d,
                    Minutes = 15,
                }
            },
            {
                CaseQuestionEnum.EulerNumberLetterCounts,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerNumberLetterCounts,
                    QuestionText = "If the numbers 1 to 5 are written out in words: one, two, three, four, five, then there are 3+3+5+4+4=19 letters used in total.\r\n\r\nIf all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used?\r\n\r\nNote: Do not count spaces or hyphens.  For example, 342 (three hundred and forty-two) contains 23 letters and 115 (one hundred and fifteen) contains 20 letters.  The use of 'and' when writing out numbers is in compliance with British usage.",
                    QuestionLink = "https://projecteuler.net/problem=17",
                    Answer = "21124",
                    ExampleAnswer = 32189d,
                    Minutes = 10f,
                }
            },
            {
                CaseQuestionEnum.EulerLexicographicPermutations,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerLexicographicPermutations,
                    QuestionText = "A permutation is an ordered arrangement of objects. For example, 3124 is one possible permutation of the digits 1, 2, 3 and 4. If all of the permutations are listed numerically or alphabetically, we call it lexicographic order. The lexicographic permutations of 0, 1 and 2 are:\r\n012 021 102 120 201 210\r\n\r\nWhat is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, ..., 9?",
                    QuestionLink = "https://projecteuler.net/problem=24",
                    Answer = "2783915460",
                    ExampleAnswer = 37219137d,
                    Minutes = 15f,
                }
            },
            {
                CaseQuestionEnum.EulerDigitFifthPowers,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerDigitFifthPowers,
                    QuestionText = "Surprisingly there are only 3 numbers that can be written as the sum of fourth powers of their digits:\r\n1634=1^4+6^4+3^4+4^4\r\n8208=8^4+2^4+0^4+8^4\r\n9474=9^4+4^4+7^4+4^4\r\n\r\nAs 1=1^4 is not a sum it is not included.\r\nThe sum of these numbers is 1634+8208+9474=19316.\r\nFind the sum of all the numbers that can be written as the sum of fifth powers of their digits.",
                    QuestionLink = "https://projecteuler.net/problem=30",
                    Answer = "443839",
                    ExampleAnswer = 231321d,
                    Minutes = 8f,
                }
            },
            {
                CaseQuestionEnum.EulerCoinSums,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerCoinSums,
                    QuestionText = "In the United Kingdom the currency is made up of pound (£) and pence (p).  There are 8 coins in general circulation:\r\n\r\n1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200)p.\r\n\r\nIt is possible to make £2 in the following way: 1x£1 + 1x50p + 2x20p + 1x5p + 1x2p + 3x1p\r\n\r\nHow many different ways can £2 be made using any number of coins?",
                    QuestionLink = "https://projecteuler.net/problem=31",
                    Answer = "73682",
                    ExampleAnswer = 2312d,
                    Minutes = 15f,
                }
            },
            {
                CaseQuestionEnum.EulerDigitCancellingFractions,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerDigitCancellingFractions,
                    QuestionText = "The fraction 49/98 is a curious fraction, as an inexperienced mathematician in attempting to simplify it may incorrectly believe that 49/98=4/8, which is correct, is obtained by cancelling the 9s.\r\n\r\nWe shall consider fractions like, 30/50=3/5, to be trivial examples.\r\n\r\nThere are exactly four non-trivial examples of this type of fraction, less than one in value, and containing two digits in the numerator and denominator.\r\n\r\nIf the product of these four fractions is given in its lowest common terms, find the value of the denominator (number at the bottom of the reduced fraction).",
                    QuestionLink = "https://projecteuler.net/problem=33",
                    Answer = "100",
                    ExampleAnswer = 3213d,
                    Minutes = 10f,
                }
            },
            {
                CaseQuestionEnum.EulerDigitFactorials,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerDigitFactorials,
                    QuestionText = "145 is a curious number, as 1!+4!+5!=1+24+120=145.\r\n\r\nFind the sum of all numbers which are equal to the sum of the factorial of their digits.\r\n\r\nNote: As 1! = 1 and 2! = 2 are not sums they are not included.",
                    QuestionLink = "https://projecteuler.net/problem=34",
                    Answer = "40730",
                    ExampleAnswer = 23123d,
                    Minutes = 5f,
                }
            },
            {
                CaseQuestionEnum.EulerDoubleBasePalindromes,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerDoubleBasePalindromes,
                    QuestionText = "The decimal number 585 equals 1001001001 in binary, and is palindromic in both bases (i.e. reads the same forward and backwards).\r\n\r\nFind the sum of all numbers less than 1 million which are palindromic in base 10 and in base 2.\r\n\r\n(For this question do not count numbers where the first digit is 0.)",
                    QuestionLink = "https://projecteuler.net/problem=36",
                    Answer = "872187",
                    ExampleAnswer = 231321d,
                    Minutes = 10f,
                }
            },
            {
                CaseQuestionEnum.EulerChampernownesConstant,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerChampernownesConstant,
                    QuestionText = "An irrational decimal fraction is created by concatenating the positive integers:\r\n\r\n0.123456789101112131415161718192021...\r\n\r\nIt can be seen that the 12th digit of the fractional part is 1.\r\n\r\nIf d(n) represents the nth digit of the fractional part, find d(1) x d(10) x d(100) x d(1000) x d(10000) x d(100000) x d(1000000).",
                    QuestionLink = "https://projecteuler.net/problem=40",
                    Answer = "210",
                    ExampleAnswer = 3600d,
                    Minutes = 15f,
                }
            },
            {
                CaseQuestionEnum.EulerSubStringDivisibility,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerSubStringDivisibility,
                    QuestionText = "The number, 1406357289, is a 0-9 pandigital number because it is made up of each of the digits 0 to 9 in some order, but it also has a rather interesting sub-string divisibility property.\r\n\r\nLet d(1) be the first digit, d(2) the second digit and so on.  We note the following:\r\nd(2)d(3)d(4)=406 is divisible by 2\r\nd(3)d(4)d(5)=063 is divisible by 3\r\nd(4)d(5)d(6)=635 is divisible by 5\r\nd(5)d(6)d(7)=357 is divisible by 7\r\nd(6)d(7)d(8)=572 is divisible by 11\r\nd(7)d(8)d(9)=728 is divisible by 13\r\nd(8)d(9)d(10)=289 is divisible by 17\r\n\r\nFind the sum of all 0-9 pandigital numbers with this property.",
                    QuestionLink = "https://projecteuler.net/problem=43",
                    Answer = "16695334890",
                    ExampleAnswer = 321321790d,
                    Minutes = 20f,
                }
            },
            {
                CaseQuestionEnum.EulerCountingSummations,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerCountingSummations,
                    QuestionText = "It is possible to write five as a sum in exactly six different ways:\r\n\r\n4+1\r\n3+2\r\n3+1+1\r\n3+1+1\r\n2+2+1\r\n2+1+1+1\r\n1+1+1+1+1\r\n\r\nHow many different ways can one hundred be written as a sum of at least two positive integers?",
                    QuestionLink = "https://projecteuler.net/problem=76",
                    Answer = "190569291",
                    ExampleAnswer = 3213290d,
                    Minutes = 20f,
                }
            },
            {
                CaseQuestionEnum.DPMMSNumbersSetsEx1Q15,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.DPMMSNumbersSetsEx1Q15,
                    QuestionText = "All integers greater than one but less than 100 are put into a hat and two are drawn.\r\n\r\nKylie is given their sum and Tim their product.\r\n\r\nKylie says, “I can tell you don’t know the numbers.” Tim replies, “Now I do.”\r\n\r\nKylie exclaims, “Now I do too!” What is the sum of the numbers?",
                    QuestionLink = "http://www.dpmms.cam.ac.uk/study/IA/Numbers%2BSets/2024-2025/NS_1.pdf",
                    Answer = "17",
                    ExampleAnswer = 13d,
                    Minutes = 10d,
                }
            },
            {
                CaseQuestionEnum.CodeGolf9x9Queens,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.CodeGolf9x9Queens,
                    QuestionText = "This a puzzle involving a 9x9 grid.  Your goal is to exactly one queen (Q) in each row, column, diagonal and region [regions represented by the numbers in the grid below].\r\n\r\nAnswer in a format where you concatenate the positions in the grid, with '.' representing no queen and Q representing a Queen.",
                    QuestionLink = "https://codegolf.stackexchange.com/questions/282183/solve-queens-puzzle",
                    Data = new Dictionary<string, object[,]>
                    {
                        {
                            "Regions",
                            new object[,]
                            {
                                {1d,1d,2d,2d,2d,2d,2d,2d,2d},
                                {1d,3d,3d,4d,4d,4d,5d,5d,2d},
                                {3d,3d,4d,4d,6d,4d,4d,5d,5d},
                                {3d,4d,4d,4d,6d,4d,4d,4d,5d},
                                {3d,4d,7d,7d,7d,4d,4d,4d,5d},
                                {3d,4d,4d,4d,4d,4d,4d,4d,5d},
                                {3d,8d,4d,4d,4d,4d,4d,9d,5d},
                                {3d,8d,8d,4d,4d,4d,9d,9d,5d},
                                {3d,3d,8d,8d,8d,8d,5d,5d,5d},
                            }
                        }
                    },
                    Answer = ".Q...............Q....Q....Q...........Q..........Q..........Q...Q............Q..",
                    ExampleAnswer = "..Q.............Q......Q........Q.......Q...........Q.Q..............Q...Q.......",
                    Minutes = 20d,
                }
            },
            {
                CaseQuestionEnum.CodeGolfReverseNATOPhoneticSpelling,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.CodeGolfReverseNATOPhoneticSpelling,
                    QuestionText = "NATO Phonetic Spelling is a way to spell out letters so that they're distinguishable over a noisy connection. The mapping of letters is as follows:\r\n\r\nA -> ALFA\r\nB -> BRAVO\r\nC -> CHARLIE\r\nD -> DELTA\r\nE -> ECHO\r\nF -> FOXTROT\r\nG -> GOLF\r\nH -> HOTEL\r\nI -> INDIA\r\nJ -> JULIETT\r\nK -> KILO\r\nL -> LIMA\r\nM -> MIKE\r\nN -> NOVEMBER\r\nO -> OSCAR\r\nP -> PAPA\r\nQ -> QUEBEC\r\nR -> ROMEO\r\nS -> SIERRA\r\nT -> TANGO\r\nU -> UNIFORM\r\nV -> VICTOR\r\nW -> WHISKEY\r\nX -> XRAY\r\nY -> YANKEE\r\nZ -> ZULU\r\n\r\nFor example, CAT would map to CHARLIEALFATANGO.\r\n\r\nYour challenge is to do the reverse.\r\n\r\nTranslate this string into the corresponding sequence of (capital) letters:\r\n\r\nLIMAUNIFORMHOTELWHISKEYCHARLIEOSCARJULIETTGOLFROMEOBRAVOPAPAECHOSIERRAINDIADELTAZULUNOVEMBERKILOALFAXRAYMIKEQUEBECTANGOVICTORFOXTROTYANKEE",
                    QuestionLink = "https://codegolf.stackexchange.com/questions/282191/reverse-nato-phonetic-spelling",
                    Data = null,
                    Answer = "LUHWCOJGRBPESIDZNKAXMQTVFY",
                    ExampleAnswer = "DSOIJPJAOPAD",
                    Minutes = 5f,
                }
            },
            {
                CaseQuestionEnum.CodeGolfAlbuquerque,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.CodeGolfAlbuquerque,
                    QuestionText = "The goal is to create an output string from 'albuquerque' that follows these rules.\r\n\r\nAs you read the string, each time a character is repeated, you repeat the substring from where that character was last used back to itself (in the original string).\r\n\r\nFor example, here is a step-by-step on 'success' which becomes 'succcesuccesss'.\r\n\r\ns\r\nsu\r\nsuc\r\nsuccc (loop cc)\r\nsuccce\r\nsucccesucces (loop succes)\r\nsucccesuccesss (loop ss).\r\n\r\nWhat would the final result be if the input was 'albuquerque'?",
                    QuestionLink = "https://codegolf.stackexchange.com/questions/279619/albuququerquerquerquerque-challlengenge",
                    Data = null,
                    Answer = "albuququerquerquerquerque",
                    ExampleAnswer = "dosiahdoa",
                    Minutes = 8f,
                }
            },
            {
                CaseQuestionEnum.DailyProgrammerMatrixSum,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.DailyProgrammerMatrixSum,
                    QuestionText = "What is the smallest possible sum you can select from this 5x5 grid such that no two elements come from the same row or column?",
                    QuestionLink = "https://www.reddit.com/r/dailyprogrammer/comments/oirb5v/20210712_challenge_398_difficult_matrix_sum/",
                    Data = null,
                    Answer = "1099762961",
                    ExampleAnswer = 2319013d,
                    Minutes = 10f,
                }
            },
            {
                CaseQuestionEnum.DailyProgrammerLetterValueSum,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.DailyProgrammerLetterValueSum,
                    QuestionText = "Assign every lowercase letter a value, from 1 for 'a' to 26 for 'z'.\r\nWhat is the sum of the values for \"microspectrophotometries\"?",
                    QuestionLink = "https://www.reddit.com/r/dailyprogrammer/comments/onfehl/20210719_challenge_399_easy_letter_value_sum/",
                    Data = null,
                    Answer = "317",
                    ExampleAnswer = 317d,
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.DailyProgrammerNumberOfOnes,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.DailyProgrammerNumberOfOnes,
                    QuestionText = "If you write out the numbers 1, 2, ... up to 123321, how many 1s are there in total?",
                    QuestionLink = "https://www.reddit.com/r/dailyprogrammer/comments/neg49j/20210517_challenge_390_difficult_number_of_1s/",
                    Data = null,
                    Answer = "93395",
                    ExampleAnswer = 21791d,
                    Minutes = 20f,
                }
            },
            {
                CaseQuestionEnum.DailyProgrammerSmooshedMorseCode1,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.DailyProgrammerNumberOfOnes,
                    QuestionText = "The morse code for the letters a-z in order are given below.  Give the full concatenated morse code of the word 'programmer'.",
                    QuestionLink = "https://www.reddit.com/r/dailyprogrammer/comments/cmd1hb/20190805_challenge_380_easy_smooshed_morse_code_1/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Conversions",
                            new object[,]
                            {
                                {"a",".-"},
                                {"b","-..."},
                                {"c","-.-."},
                                {"d","-.."},
                                {"e","."},
                                {"f","..-."},
                                {"g","--."},
                                {"h","...."},
                                {"i",".."},
                                {"j",".---"},
                                {"k","-.-"},
                                {"l",".-.."},
                                {"m","--"},
                                {"n","-."},
                                {"o","---"},
                                {"p",".--."},
                                {"q","--.-"},
                                {"r",".-."},
                                {"s","..."},
                                {"t","-"},
                                {"u","..-"},
                                {"v","...-"},
                                {"w",".--"},
                                {"x","-..-"},
                                {"y","-.--"},
                                {"z","--.."},
                            }
                        }
                    },
                    Answer = ".--..-.-----..-..-----..-.",
                    ExampleAnswer = "-.....-...",
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.DailyProgrammerEasyProgressiveTaxation,
                new CaseQuestion
                {
                    Id = CaseQuestionEnum.DailyProgrammerEasyProgressiveTaxation,
                    QuestionText = "In Excelopolis, income up to 10,000 is taxed at 0, from 10,000 to 30,000 taxed at 10%, from 30,000 to 100,000 taxed at 25% and over 100,000 taxed at 40%.\r\n\r\nHow much tax would someone who earns 1234567 pay?",
                    QuestionLink = "https://www.reddit.com/r/dailyprogrammer/comments/cdieag/20190715_challenge_379_easy_progressive_taxation/",
                    Answer = "473326",
                    ExampleAnswer = 321739d,
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.DailyProgrammerHavelHakimi,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.DailyProgrammerHavelHakimi,
                    QuestionText = "It was a dark and stormy night. Detective Havel and Detective Hakimi arrived at the scene of the crime.\r\n\r\nOther than the detectives, there were 10 people present. They asked the first person, \"out of the 9 other people here, how many had you already met before tonight?\" The person answered \"5\". They asked the same question of the second person, who answered \"3\". And so on. The 10 answers they got from the 10 people were:\r\n\r\n5 3 0 2 6 2 0 7 2 5\r\n\r\nThe detectives deduced that someone must have been lying.\r\n\r\nIf a group of people had instead resulted in '16, 9, 9, 15, 9, 7, 9, 11, 17, 11, 4, 9, 12, 14, 14, 12, 17, 0, 3, 16', answer 1 if the answers are consistent, and 0 otherwise.",
                    QuestionLink = "https://www.reddit.com/r/dailyprogrammer/comments/bqy1cf/20190520_challenge_378_easy_the_havelhakimi/",
                    Answer = "1",
                    ExampleAnswer = 1,
                    Minutes = 20f,
                }
            },
            {
                CaseQuestionEnum.DailyProgrammerHexConversion,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.DailyProgrammerHexConversion,
                    QuestionText = "Convert the 'r;g;b' colour '189;183;107' into its hex code (base 16) - use A to represent 10, B to represent 11, ..., F to represent 15.\r\n\r\nFor example, '184;134;11' would convert into '#BDB76B'.\r\nDon't forget to add a hash (#) at the start.",
                    QuestionLink = "https://www.reddit.com/r/dailyprogrammer/comments/a0lhxx/20181126_challenge_369_easy_hex_colors/",
                    Answer = "#BDB76B",
                    ExampleAnswer = "#B8860B",
                    Minutes =  8f,

                }
            },
            {
                CaseQuestionEnum.DailyProgrammerIntegerComplexity,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.DailyProgrammerIntegerComplexity,
                    QuestionText = "Consider all the different ways of representing a positive integer using nothing but positive integers, addition, multiplication, and parentheses. For 5678, a few such ways are:\r\n\r\n5678 = 2*17*167\r\n5678 = 5678\r\n5678 = 23*59+29*149\r\n5678 = (1+4*4)*(1+3*3*(1+3*3*4))\r\n5678 = 2*(1+2*(1+2*(1+2*2*(1+2*2*2*2*(1+2*(1+2*2))))))\r\nFor each such representation, consider the sum of the integers involved:\r\n\r\n2*17*167 => 2+17+167 = 186\r\n5678 => 5678 = 5678\r\n23*59+29*149 => 23+59+29+149 = 260\r\n(1+4*4)*(1+3*3*(1+3*3*4)) => 1+4+4+1+3+3+1+3+3+4 = 27\r\n2*(1+2*(1+2*(1+2*2*(1+2*2*2*2*(1+2*(1+2*2)))))) =>\r\n    2+1+2+1+2+1+2+2+1+2+2+2+2+1+2+1+2+2 = 30\r\nFor 5678, the minimum possible sum for any such representation is 27. The minimum possible sum for a given integer is known as its integer complexity, so the integer complexity of 5678 is 27. The integer complexity of the numbers 1, 2, 3, ... is:\r\n\r\n1 2 3 4 5 5 6 6 6 7 8 7 8 8 8 8 9 8 9 9 ...\r\nThe sum of the integer complexities for all numbers from 1 to 100 inclusive is 1113.\r\n\r\nFind the sum of the integer complexities for all numbers from 1 to 1000 inclusive.",
                    QuestionLink = "https://www.reddit.com/r/dailyprogrammer/comments/84f35x/20180314_challenge_354_intermediate_integer/",
                    Answer = "18274",
                    ExampleAnswer = 43214d,
                    Minutes = 30f,
                }
            },
            {
                CaseQuestionEnum.MEWC2021CardGameLevel1,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2021CardGameLevel1,
                    QuestionText = "How many points in total will there be on first 10 cards that players draw (i.e. from the left) before the game?",
                    QuestionLink = "https://fmworldcup.com/product/fmwc-open-dec-4-2021-card-game/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Points",
                            new object[,]
                            {
                                {"Card","Pts"},
                                {"6",6d},
                                {"7",7d},
                                {"8",8d},
                                {"9",9d},
                                {"10",10d},
                                {"J",12d},
                                {"Q",15d},
                                {"K",25d}
                            }
                        },
                        {
                            "Cards",
                            new object[,]
                            {
                                {"10♠"},
                                {"9♣"},
                                {"Q♣"},
                                {"8♥"},
                                {"A♣"},
                                {"A♥"},
                                {"J♦"},
                                {"8♣"},
                                {"10♥"},
                                {"10♣"},
                                {"J♣"},
                                {"Q♥"},
                                {"K♠"},
                                {"9♠"},
                                {"6♣"},
                                {"9♦"},
                                {"A♠"},
                                {"6♥"},
                                {"7♠"},
                                {"9♥"},
                                {"J♥"},
                                {"7♣"},
                                {"K♣"},
                                {"6♦"},
                                {"8♠"},
                                {"A♦"},
                                {"K♦"},
                                {"J♠"},
                                {"6♠"},
                                {"8♦"},
                                {"K♥"},
                                {"Q♦"},
                                {"10♦"},
                                {"Q♠"},
                                {"7♥"},
                                {"7♦"},
                            }
                        }
                    },
                    Answer = "182",
                    ExampleAnswer = 174d,
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.MEWC2021DominateTheDominoesLevel1,
                new CaseQuestion()
                {
                    QuestionText = "How many pips in total will there be on the first 14 tiles?",
                    QuestionLink = "https://fmworldcup.com/product/fmwc-open-dec-4-2021-dominate-the-dominoes/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "DominoLookup",
                            new object[,]
                            {
                                {"Tile","Side 1","Side 2"},
                                {"🀱","",""},
                                {"🀲","",1d},
                                {"🀳","",2d},
                                {"🀴","",3d},
                                {"🀵","",4d},
                                {"🀶","",5d},
                                {"🀷","",6d},
                                {"🀹",1d,1d},
                                {"🀺",1d,2d},
                                {"🀻",1d,3d},
                                {"🀼",1d,4d},
                                {"🀽",1d,5d},
                                {"🀾",1d,6d},
                                {"🁁",2d,2d},
                                {"🁂",2d,3d},
                                {"🁃",2d,4d},
                                {"🁄",2d,5d},
                                {"🁅",2d,6d},
                                {"🁉",3d,3d},
                                {"🁊",3d,4d},
                                {"🁋",3d,5d},
                                {"🁌",3d,6d},
                                {"🁑",4d,4d},
                                {"🁒",4d,5d},
                                {"🁓",4d,6d},
                                {"🁙",5d,5d},
                                {"🁚",5d,6d},
                                {"🁡",6d,6d},
                            }
                        },
                        {
                            "DominoOrder (Get first 14 from top to bottom here)",
                            new object[,]
                            {
                                {"🁉"},
                                {"🀱"},
                                {"🀲"},
                                {"🀾"},
                                {"🁑"},
                                {"🀼"},
                                {"🀷"},
                                {"🁚"},
                                {"🁄"},
                                {"🁡"},
                                {"🀽"},
                                {"🁌"},
                                {"🀵"},
                                {"🀻"},
                                {"🀶"},
                                {"🀳"},
                                {"🁊"},
                                {"🁂"},
                                {"🁅"},
                                {"🁓"},
                                {"🁁"},
                                {"🁋"},
                                {"🀹"},
                                {"🁃"},
                                {"🀴"},
                                {"🁙"},
                                {"🀺"},
                                {"🁒"},
                            }
                        }
                    },
                    Answer = "86",
                    ExampleAnswer = 77d,
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.MEWC2021LumberjackLevel1A,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2021LumberjackLevel1A,
                    QuestionText = "How many units of the 🌲 tree are there in the forest?",
                    QuestionLink = "https://fmworldcup.com/product/fmwc-open-dec-4-2021-lumberjack/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Map",
                            new object[,]
                            {
                                {"🎋","🎄","🌲","🌳","🎄","🎄","🎄","🎄","","🌳","🌴","🌲","🌲","🌲","🌴","🌲","🌳","🎄","🌳","🌲","🎄","🌳","🎄","🌳","🌲",},
                                {"🌲","🌲","🌳","🌲","🎄","🌲","🌲","🎋","","🌲","🌲","🌲","🌲","🌲","🎄","🌲","🌲","🌳","🌲","🎋","🌴","🌲","🌲","🌲","🎄",},
                                {"🌴","🌲","🌲","🌲","🎄","🎄","🌲","🌲","","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌴","🌲","🌲","🌲","🌴","🌳","🌳","🌲",},
                                {"🌲","🎄","🌳","🌳","🌳","🎄","🎋","🎄","","🌳","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌳","🎄","🌲","🌴","🌲","🌴","🌲",},
                                {"🌲","🌳","🌲","🌲","🌲","🌳","🌲","🌳","","🌲","🌴","🌲","🌲","🌲","🌳","🌲","🌴","🌲","🌲","🎄","🌲","🌴","🌲","🎄","🎄",},
                                {"🌳","🌲","🌳","🌳","🌲","🌲","🌲","🌲","","🌲","🌲","🌳","🌲","🌳","🌲","🎄","🌲","🌲","🌲","🌳","🌲","🌴","🌲","🌲","🎋",},
                                {"🌳","🌳","🌳","🎄","🌳","🌲","🎄","🌲","","🌲","🌳","🌳","🌳","🌲","🎄","🌲","🌲","🌴","🌲","🌲","🎄","🎄","🌳","🌲","🌲",},
                                {"🎋","🌳","🌲","🎄","🌲","🌳","🌲","🎄","","","","","","","🌲","🌲","🌳","🌴","🌳","🌲","🌲","🌴","🌳","🌳","🌳",},
                                {"🌲","🌴","🌲","🌴","🌴","🌲","🌳","🌲","🌲","🌳","🌲","🌳","🌳","","🌲","🌳","🌴","🌳","🌴","🌳","🌴","🌲","🌴","🌳","🌴",},
                                {"🌴","🌲","🎄","🌲","🎄","🌴","🌲","🌲","🌳","🌲","🎄","🌲","🌲","","🌲","🌲","🌳","🎄","🌴","🌲","🎄","🌲","🌴","🌲","🌲",},
                                {"🌳","🌲","🌲","🌲","🌳","🌲","🎄","🌲","🌲","🌳","🌲","🌲","🌲","","🌲","🌲","🌲","🌳","🌳","🌲","🎄","🌴","🌳","🌲","🌴",},
                                {"🌲","🌴","🌲","🌲","🌴","🎄","🌲","🎄","🌲","🌲","🌳","🌴","🌴","","🌲","🎄","🌴","🌴","🌲","🌲","🎄","🌲","🌲","🎄","🌴",},
                                {"🎄","🌲","🌴","🎄","🎄","🌲","🌲","🎋","🌲","🌲","🌲","🌳","","","","🌳","🌳","🌲","🌲","🌳","🌲","🌲","🌲","🎄","🌴",},
                                {"🎄","🌳","🌲","🌲","🌳","🌲","🌲","🌳","🌳","🌳","🌴","🌲","","🏭","","","","","","","","","","","",},
                                {"🌳","🌲","🌳","🌲","🌲","🌲","🌳","🎋","🌴","🌲","🌲","🌲","","","","🌳","🌲","🌴","🌴","🌲","🎋","🌳","🌲","🌲","🌲",},
                                {"🌴","🎄","🌳","🌳","🌳","🌴","🌲","🌲","🌲","🎄","🎄","🌳","","🌲","🌲","🎄","🌳","🌳","🌲","🌳","🌳","🌳","🌳","🌳","🌲",},
                                {"🎄","🎄","🌳","🎄","🌴","🌳","🌳","🌳","🌲","🌳","🌲","🎄","","🌲","🌳","🌴","🌲","🎄","🌳","🌲","🌳","🎄","🎄","🌳","🌲",},
                                {"🎄","🌴","🌳","🌴","","","","","","","","","","🌴","🌲","🌲","🌳","🌴","🌲","🌴","🌴","🌴","🌴","🌴","🌲",},
                                {"🌳","🌲","🌲","🎋","","🎋","🌴","🌲","🎄","🎋","🌴","🌳","🎋","🎋","🎋","🌴","🎄","🎄","🎋","🌲","🎄","🌴","🌳","🌴","🌲",},
                                {"🌴","🌲","🎄","🌲","","🎋","🎄","🌲","🌲","🎄","🌲","🌲","🌲","🎄","🎋","🎄","🌳","🌴","🌲","🌴","🎋","🌴","🌳","🌴","🌲",},
                                {"🌲","🌴","🌳","🌳","","🎄","🌲","🌲","🌳","🌴","🌳","🌲","🌳","🌳","🌲","🌳","🌳","🌴","🌳","🌲","🌲","🌲","🌲","🌲","🌳",},
                                {"🌲","🌲","🎄","🎄","","🌳","🌲","🌲","🌳","🌴","🌴","🌳","🌲","🌳","🌳","🌲","🌲","🌲","🌲","🎄","🌲","🌲","🌲","🌳","🌲",},
                                {"🌳","🌴","🌳","🌲","","🌲","🌲","🌴","🌲","🌲","🌲","🌲","🌳","🌲","🌳","🌲","🌲","🌲","🌲","🌳","🌲","🌲","🌳","🌲","🌲",},
                                {"🌲","🌳","🌳","🌳","","🌳","🌲","🎄","🎄","🌴","🌲","🎄","🌳","🌲","🌲","🌴","🌳","🌲","🎄","🌲","🌲","🌲","🌳","🌴","🎄",},
                                {"🌲","🌴","🌳","🌲","","🌳","🌳","🎋","🌳","🎄","🌳","🌲","🌳","🌳","🌳","🌳","🌲","🌳","🌲","🌳","🌳","🌴","🌲","🌲","🌲",},
                            }
                        }
                    },
                    Answer = "259",
                    ExampleAnswer = 210d,
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.MEWC2021LumberjackLevel1B,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2021LumberjackLevel1B,
                    QuestionText = "How is the total available volume of the 🌳 tree in the forest?",
                    QuestionLink = "https://fmworldcup.com/product/fmwc-open-dec-4-2021-lumberjack/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Table",
                            new object[,]
                            {
                                {"Symbol","Number","Tree Name","Volume, m3 per Tree","Cutting Cost, $ per tree","Transportation Cost, $ per Tree per Cell","Sale Price, Compared to Pine","Processing Cost, $/m3",},
                                {"🌲",1d,"Pine",10d,10d,2d,1d,10d,},
                                {"🌳",2d,"Birch",8d,12d,2d,1d,10d,},
                                {"🎄",3d,"Christmas tree",6d,12d,1d,2d,10d,},
                                {"🌴",4d,"Palm",5d,15d,3d,2d,10d,},
                                {"🎋",5d,"Tanabata",3d,20d,8d,3d,10d,},
                            }
                        },
                        {
                            "Map",
                            new object[,]
                            {
                                {"🎋","🎄","🌲","🌳","🎄","🎄","🎄","🎄","","🌳","🌴","🌲","🌲","🌲","🌴","🌲","🌳","🎄","🌳","🌲","🎄","🌳","🎄","🌳","🌲",},
                                {"🌲","🌲","🌳","🌲","🎄","🌲","🌲","🎋","","🌲","🌲","🌲","🌲","🌲","🎄","🌲","🌲","🌳","🌲","🎋","🌴","🌲","🌲","🌲","🎄",},
                                {"🌴","🌲","🌲","🌲","🎄","🎄","🌲","🌲","","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌴","🌲","🌲","🌲","🌴","🌳","🌳","🌲",},
                                {"🌲","🎄","🌳","🌳","🌳","🎄","🎋","🎄","","🌳","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌳","🎄","🌲","🌴","🌲","🌴","🌲",},
                                {"🌲","🌳","🌲","🌲","🌲","🌳","🌲","🌳","","🌲","🌴","🌲","🌲","🌲","🌳","🌲","🌴","🌲","🌲","🎄","🌲","🌴","🌲","🎄","🎄",},
                                {"🌳","🌲","🌳","🌳","🌲","🌲","🌲","🌲","","🌲","🌲","🌳","🌲","🌳","🌲","🎄","🌲","🌲","🌲","🌳","🌲","🌴","🌲","🌲","🎋",},
                                {"🌳","🌳","🌳","🎄","🌳","🌲","🎄","🌲","","🌲","🌳","🌳","🌳","🌲","🎄","🌲","🌲","🌴","🌲","🌲","🎄","🎄","🌳","🌲","🌲",},
                                {"🎋","🌳","🌲","🎄","🌲","🌳","🌲","🎄","","","","","","","🌲","🌲","🌳","🌴","🌳","🌲","🌲","🌴","🌳","🌳","🌳",},
                                {"🌲","🌴","🌲","🌴","🌴","🌲","🌳","🌲","🌲","🌳","🌲","🌳","🌳","","🌲","🌳","🌴","🌳","🌴","🌳","🌴","🌲","🌴","🌳","🌴",},
                                {"🌴","🌲","🎄","🌲","🎄","🌴","🌲","🌲","🌳","🌲","🎄","🌲","🌲","","🌲","🌲","🌳","🎄","🌴","🌲","🎄","🌲","🌴","🌲","🌲",},
                                {"🌳","🌲","🌲","🌲","🌳","🌲","🎄","🌲","🌲","🌳","🌲","🌲","🌲","","🌲","🌲","🌲","🌳","🌳","🌲","🎄","🌴","🌳","🌲","🌴",},
                                {"🌲","🌴","🌲","🌲","🌴","🎄","🌲","🎄","🌲","🌲","🌳","🌴","🌴","","🌲","🎄","🌴","🌴","🌲","🌲","🎄","🌲","🌲","🎄","🌴",},
                                {"🎄","🌲","🌴","🎄","🎄","🌲","🌲","🎋","🌲","🌲","🌲","🌳","","","","🌳","🌳","🌲","🌲","🌳","🌲","🌲","🌲","🎄","🌴",},
                                {"🎄","🌳","🌲","🌲","🌳","🌲","🌲","🌳","🌳","🌳","🌴","🌲","","🏭","","","","","","","","","","","",},
                                {"🌳","🌲","🌳","🌲","🌲","🌲","🌳","🎋","🌴","🌲","🌲","🌲","","","","🌳","🌲","🌴","🌴","🌲","🎋","🌳","🌲","🌲","🌲",},
                                {"🌴","🎄","🌳","🌳","🌳","🌴","🌲","🌲","🌲","🎄","🎄","🌳","","🌲","🌲","🎄","🌳","🌳","🌲","🌳","🌳","🌳","🌳","🌳","🌲",},
                                {"🎄","🎄","🌳","🎄","🌴","🌳","🌳","🌳","🌲","🌳","🌲","🎄","","🌲","🌳","🌴","🌲","🎄","🌳","🌲","🌳","🎄","🎄","🌳","🌲",},
                                {"🎄","🌴","🌳","🌴","","","","","","","","","","🌴","🌲","🌲","🌳","🌴","🌲","🌴","🌴","🌴","🌴","🌴","🌲",},
                                {"🌳","🌲","🌲","🎋","","🎋","🌴","🌲","🎄","🎋","🌴","🌳","🎋","🎋","🎋","🌴","🎄","🎄","🎋","🌲","🎄","🌴","🌳","🌴","🌲",},
                                {"🌴","🌲","🎄","🌲","","🎋","🎄","🌲","🌲","🎄","🌲","🌲","🌲","🎄","🎋","🎄","🌳","🌴","🌲","🌴","🎋","🌴","🌳","🌴","🌲",},
                                {"🌲","🌴","🌳","🌳","","🎄","🌲","🌲","🌳","🌴","🌳","🌲","🌳","🌳","🌲","🌳","🌳","🌴","🌳","🌲","🌲","🌲","🌲","🌲","🌳",},
                                {"🌲","🌲","🎄","🎄","","🌳","🌲","🌲","🌳","🌴","🌴","🌳","🌲","🌳","🌳","🌲","🌲","🌲","🌲","🎄","🌲","🌲","🌲","🌳","🌲",},
                                {"🌳","🌴","🌳","🌲","","🌲","🌲","🌴","🌲","🌲","🌲","🌲","🌳","🌲","🌳","🌲","🌲","🌲","🌲","🌳","🌲","🌲","🌳","🌲","🌲",},
                                {"🌲","🌳","🌳","🌳","","🌳","🌲","🎄","🎄","🌴","🌲","🎄","🌳","🌲","🌲","🌴","🌳","🌲","🎄","🌲","🌲","🌲","🌳","🌴","🎄",},
                                {"🌲","🌴","🌳","🌲","","🌳","🌳","🎋","🌳","🎄","🌳","🌲","🌳","🌳","🌳","🌳","🌲","🌳","🌲","🌳","🌳","🌴","🌲","🌲","🌲",},
                            }
                        }
                    },
                    Answer = "1112",
                    ExampleAnswer = 210d,
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.MEWC2021LumberjackLevel2,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2021LumberjackLevel2,
                    QuestionText = "If the top left of the map started in A1, how far away from the road (non-tree cells) are these cells?\r\n\r\nH9;J9;B2;T17;G20;O2;G15;C5;B18;A18;K15;X13;U5;C18;G3;A18;E12;V19;V5;D2;V17\r\n\r\nAnswer as a semi-colon joined list.",
                    QuestionLink = "https://fmworldcup.com/product/fmwc-open-dec-4-2021-lumberjack/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Map",
                            new object[,]
                            {
                                {"🎋","🎄","🌲","🌳","🎄","🎄","🎄","🎄","","🌳","🌴","🌲","🌲","🌲","🌴","🌲","🌳","🎄","🌳","🌲","🎄","🌳","🎄","🌳","🌲",},
                                {"🌲","🌲","🌳","🌲","🎄","🌲","🌲","🎋","","🌲","🌲","🌲","🌲","🌲","🎄","🌲","🌲","🌳","🌲","🎋","🌴","🌲","🌲","🌲","🎄",},
                                {"🌴","🌲","🌲","🌲","🎄","🎄","🌲","🌲","","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌴","🌲","🌲","🌲","🌴","🌳","🌳","🌲",},
                                {"🌲","🎄","🌳","🌳","🌳","🎄","🎋","🎄","","🌳","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌲","🌳","🎄","🌲","🌴","🌲","🌴","🌲",},
                                {"🌲","🌳","🌲","🌲","🌲","🌳","🌲","🌳","","🌲","🌴","🌲","🌲","🌲","🌳","🌲","🌴","🌲","🌲","🎄","🌲","🌴","🌲","🎄","🎄",},
                                {"🌳","🌲","🌳","🌳","🌲","🌲","🌲","🌲","","🌲","🌲","🌳","🌲","🌳","🌲","🎄","🌲","🌲","🌲","🌳","🌲","🌴","🌲","🌲","🎋",},
                                {"🌳","🌳","🌳","🎄","🌳","🌲","🎄","🌲","","🌲","🌳","🌳","🌳","🌲","🎄","🌲","🌲","🌴","🌲","🌲","🎄","🎄","🌳","🌲","🌲",},
                                {"🎋","🌳","🌲","🎄","🌲","🌳","🌲","🎄","","","","","","","🌲","🌲","🌳","🌴","🌳","🌲","🌲","🌴","🌳","🌳","🌳",},
                                {"🌲","🌴","🌲","🌴","🌴","🌲","🌳","🌲","🌲","🌳","🌲","🌳","🌳","","🌲","🌳","🌴","🌳","🌴","🌳","🌴","🌲","🌴","🌳","🌴",},
                                {"🌴","🌲","🎄","🌲","🎄","🌴","🌲","🌲","🌳","🌲","🎄","🌲","🌲","","🌲","🌲","🌳","🎄","🌴","🌲","🎄","🌲","🌴","🌲","🌲",},
                                {"🌳","🌲","🌲","🌲","🌳","🌲","🎄","🌲","🌲","🌳","🌲","🌲","🌲","","🌲","🌲","🌲","🌳","🌳","🌲","🎄","🌴","🌳","🌲","🌴",},
                                {"🌲","🌴","🌲","🌲","🌴","🎄","🌲","🎄","🌲","🌲","🌳","🌴","🌴","","🌲","🎄","🌴","🌴","🌲","🌲","🎄","🌲","🌲","🎄","🌴",},
                                {"🎄","🌲","🌴","🎄","🎄","🌲","🌲","🎋","🌲","🌲","🌲","🌳","","","","🌳","🌳","🌲","🌲","🌳","🌲","🌲","🌲","🎄","🌴",},
                                {"🎄","🌳","🌲","🌲","🌳","🌲","🌲","🌳","🌳","🌳","🌴","🌲","","🏭","","","","","","","","","","","",},
                                {"🌳","🌲","🌳","🌲","🌲","🌲","🌳","🎋","🌴","🌲","🌲","🌲","","","","🌳","🌲","🌴","🌴","🌲","🎋","🌳","🌲","🌲","🌲",},
                                {"🌴","🎄","🌳","🌳","🌳","🌴","🌲","🌲","🌲","🎄","🎄","🌳","","🌲","🌲","🎄","🌳","🌳","🌲","🌳","🌳","🌳","🌳","🌳","🌲",},
                                {"🎄","🎄","🌳","🎄","🌴","🌳","🌳","🌳","🌲","🌳","🌲","🎄","","🌲","🌳","🌴","🌲","🎄","🌳","🌲","🌳","🎄","🎄","🌳","🌲",},
                                {"🎄","🌴","🌳","🌴","","","","","","","","","","🌴","🌲","🌲","🌳","🌴","🌲","🌴","🌴","🌴","🌴","🌴","🌲",},
                                {"🌳","🌲","🌲","🎋","","🎋","🌴","🌲","🎄","🎋","🌴","🌳","🎋","🎋","🎋","🌴","🎄","🎄","🎋","🌲","🎄","🌴","🌳","🌴","🌲",},
                                {"🌴","🌲","🎄","🌲","","🎋","🎄","🌲","🌲","🎄","🌲","🌲","🌲","🎄","🎋","🎄","🌳","🌴","🌲","🌴","🎋","🌴","🌳","🌴","🌲",},
                                {"🌲","🌴","🌳","🌳","","🎄","🌲","🌲","🌳","🌴","🌳","🌲","🌳","🌳","🌲","🌳","🌳","🌴","🌳","🌲","🌲","🌲","🌲","🌲","🌳",},
                                {"🌲","🌲","🎄","🎄","","🌳","🌲","🌲","🌳","🌴","🌴","🌳","🌲","🌳","🌳","🌲","🌲","🌲","🌲","🎄","🌲","🌲","🌲","🌳","🌲",},
                                {"🌳","🌴","🌳","🌲","","🌲","🌲","🌴","🌲","🌲","🌲","🌲","🌳","🌲","🌳","🌲","🌲","🌲","🌲","🌳","🌲","🌲","🌳","🌲","🌲",},
                                {"🌲","🌳","🌳","🌳","","🌳","🌲","🎄","🎄","🌴","🌲","🎄","🌳","🌲","🌲","🌴","🌳","🌲","🎄","🌲","🌲","🌲","🌳","🌴","🎄",},
                                {"🌲","🌴","🌳","🌲","","🌳","🌳","🎋","🌳","🎄","🌳","🌲","🌳","🌳","🌳","🌳","🌲","🌳","🌲","🌳","🌳","🌴","🌲","🌲","🌲",},
                            }
                        }
                    },
                    Answer = "2;1;7;3;2;6;3;6;3;4;2;1;9;2;2;4;6;5;9;5;3",
                    ExampleAnswer = "4;5;6;76;2;1;4;6;7;89;0;23;3;4",
                    Minutes = 5f,
                }
            },
            {
                CaseQuestionEnum.MEWC2021ItIsASnakeLevel1,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2021ItIsASnakeLevel1,
                    QuestionText = "The snake initially appears in the top left of its grid (A1).  It will not hit a wall.\r\n\r\nWhat will be the location id (based on the labels, not the cell address) at which the snake will find itself after the 5th move?\r\n\r\nAnswer as a semi-colon joined list of one answer for each row.",
                    QuestionLink = "https://fmworldcup.com/product/fmwc-open-dec-8-2021-its-a-snake/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Snake Grid",
                            _snakeGrid
                        },
                        {
                            "Games (One row per game)",
                            new object[,]
                            {
                                {"3→","3↓","9→","8↓","4→",},
                                {"4→","2↓","3→","9↓","4→",},
                                {"8→","2↓","6←","1↓","3→",},
                                {"5↓","5→","5↑","5→","5↓",},
                                {"6↓","4→","6↑","4→","4↓",},
                                {"4↓","7→","7↓","3←","4↑",},
                                {"3↓","8→","8↓","3←","9↑",},
                                {"2↓","5→","2↓","1→","3↑",},
                                {"5→","4↓","5→","3↑","9←",},
                                {"6→","2↓","5←","5↓","1←",},
                                {"7→","2↓","3→","6↓","4→",},
                                {"8→","5↓","1←","5↑","4←",},
                                {"13→","10↓","4←","7↑","6←",},
                            }
                        }
                    },
                    Answer = "L17;L12;D6;F11;E9;H5;C6;B7;B2;H1;I15;A4;D4;I15;C12;J4",
                    ExampleAnswer = "A1,A2,A3,A4,A5,A6,A7",
                    Minutes = 10f,
                }
            },
            {
                CaseQuestionEnum.MEWC2021ItIsASnakeLevel2,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2021ItIsASnakeLevel2,
                    QuestionText = "The snake initially appears in the top left of its grid (A1).  The game will be over if it hits a wall.\r\n\r\nHow many moves will the game last before the snake hits a wall?\r\n\r\nAnswer as a semi-colon joined list of one answer for each row.",
                    QuestionLink = "https://fmworldcup.com/product/fmwc-open-dec-8-2021-its-a-snake/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Snake Grid",
                            _snakeGrid
                        },
                        {
                            "Games (One row per game)",
                            new object[,]
                            {
                                {"4↓","1→","2↓","4←","2↑","4←","5↑","5←","5↑","2→","3↑","5←","1↑","4→","2↑","1←","4↓","5→","1↓","1←","3↓","3→","2↓","4←","1↑","2→","4↑","1→","3↓","4→","3↑","2←","4↓","4→","4↓","1←","2↑","2←","2↓","1→","1↑","1→","3↑","5←","2↓","2←","4↓","2←","5↑","4←",},
                                {"2↓","4→","2↑","3←","4↑","4→","1↑","4→","2↓","5←","5↑","2←","1↓","4→","5↓","3→","2↓","3→","1↑","4←","2↑","3→","4↓","2→","5↓","3←","2↑","3→","4↓","4→","1↓","3→","5↓","4←","4↓","2→","3↑","2→","1↑","3→","1↑","4←","1↓","2→","2↓","4←","2↓","3→","3↓","2→",},
                                {"4↓","1→","3↓","3←","4↑","4→","5↓","2←","2↓","4←","4↑","3→","2↓","4→","2↓","1←","2↓","1←","3↑","3→","5↓","3→","4↓","2→","2↓","2→","2↓","4→","3↓","4←","2↑","1←","2↓","2→","2↓","1←","4↓","2←","2↓","3←","5↑","1→","4↑","5→","4↓","2←","2↑","2→","5↓","2←",},
                                {"3→","4↓","1→","3↓","2→","2↓","2←","2↓","3→","3↑","5→","2↓","5←","3↑","2→","1↑","1←","4↑","1→","4↓","1→","4↓","2←","4↓","4←","2↑","2←","4↓","2←","4↓","2→","4↓","2←","4↑","4→","1↑","2←","2↑","4←","2↑","2←","2↓","5←","5↑","2→","4↓","3←","4↓","4→","2↑",},
                                {"2→","1↓","2→","2↓","2→","3↑","2←","4↓","2→","2↓","5←","5↑","4→","3↑","2→","3↑","3→","3↑","5→","4↓","4→","4↓","2←","5↑","3←","2↑","2→","3↓","2→","1↓","2→","4↓","4→","2↓","4→","5↓","2→","3↑","2←","5↓","2→","2↓","1←","3↓","2←","5↓","4→","1↓","2→","2↓",},
                                {"2↓","1→","1↑","4→","4↓","2←","3↑","5→","2↑","3←","3↓","2←","2↑","4→","4↑","2→","1↑","4←","2↑","4←","1↓","3→","5↓","4→","5↑","4→","4↑","5←","2↓","1→","5↓","2←","2↓","3←","4↑","4←","1↓","2←","2↑","1→","2↓","2→","2↑","2→","4↑","4→","2↑","2←","1↑","4←",},
                                {"2→","3↓","3→","3↑","2→","2↑","1←","4↓","5→","3↓","4←","2↓","5→","4↑","4→","2↓","4→","3↑","1→","5↓","2←","2↓","1←","5↑","1→","1↓","2→","1↓","1→","3↓","2←","3↓","5←","2↓","1←","5↑","3←","4↑","3←","4↓","1←","5↓","4→","3↑","3←","1↑","1←","2↑","4←","2↓",},
                                {"2↓","2→","3↓","1←","5↑","2←","2↓","4←","2↓","2←","1↓","2→","1↓","1←","4↑","4→","3↑","3→","5↓","1→","3↓","1←","2↓","2←","2↑","3→","1↑","2←","1↑","3←","4↓","3→","2↓","1←","2↓","4←","1↑","2→","5↑","1→","2↓","1→","5↓","4←","4↓","2←","4↑","2→","2↓","2←",},
                                {"2→","2↓","5←","2↓","4→","5↑","2→","3↓","2→","3↑","3→","1↑","5→","4↑","2→","2↑","1→","4↓","4←","2↓","2←","4↑","4←","5↓","4←","2↓","2→","3↓","4←","3↓","2→","4↑","3←","5↑","1→","1↓","2←","2↑","1→","4↓","3←","3↑","1←","4↑","3→","4↓","3→","4↑","4→","2↑",},
                                {"2→","3↓","2→","2↑","2←","3↑","2→","5↑","1←","5↓","4→","5↓","2→","2↑","3→","3↓","3←","1↓","3→","2↓","2←","4↓","3←","1↓","5→","1↓","3→","3↑","1←","1↑","4→","4↑","4←","2↑","3→","2↓","1→","3↑","3→","2↓","4←","3↓","4→","1↑","2←","3↓","3←","2↑","1←","3↑",},
                                {"2→","3↓","1←","3↓","1←","4↑","2→","4↑","2→","1↑","1→","1↑","3←","3↓","1←","2↑","2→","2↓","2→","3↑","5→","2↓","2←","2↑","2←","3↑","2←","4↓","3←","3↑","3→","3↓","4→","4↑","3←","2↑","2←","1↑","1→","2↑","2→","2↓","5→","2↓","2→","3↓","5←","3↑","3→","2↑",},
                                {"3↓","2→","5↓","3←","2↑","3→","2↑","2←","4↑","3→","5↓","3←","2↑","3←","2↓","5→","2↑","2→","3↓","3→","5↓","2←","5↑","3←","2↓","2→","2↓","1→","2↑","1→","1↓","2→","3↓","4→","4↑","2→","4↑","2←","1↑","1→","4↓","1←","4↓","2→","4↑","2←","4↓","4←","1↓","1→",},
                                {"2↓","3→","2↓","2←","1↑","4→","5↓","2→","2↑","1←","3↓","1→","3↓","5→","2↓","3←","5↓","2→","1↑","3→","4↑","2→","1↑","2→","1↑","3→","3↑","2→","4↓","2←","4↓","3←","2↑","2→","4↑","3←","4↓","2→","2↑","4→","3↑","5←","4↓","1←","2↑","2←","1↓","2→","5↑","2→",},
                                {"2↓","2→","2↑","3→","4↓","4←","4↓","2→","4↓","2←","5↓","2→","3↓","2←","4↓","4→","1↓","2→","4↓","2→","3↓","3→","3↓","3→","4↓","2←","5↓","1←","3↑","1←","2↓","3←","1↑","3→","2↑","4←","1↓","4←","2↑","3→","1↓","2←","2↑","2→","2↑","3→","1↓","2→","1↑","4→",},
                                {"2↓","1→","4↑","3←","4↓","2←","3↓","2←","2↓","3←","2↑","4←","5↑","5→","4↑","2←","4↑","3←","4↑","5→","1↓","1→","3↓","1→","2↓","3→","3↓","5→","2↓","2→","1↓","3←","3↑","3→","2↓","2→","2↑","3→","4↓","4←","5↑","4←","5↓","4←","5↑","5←","3↑","2←","2↑","2←",},
                                {"4↓","5→","5↓","3→","4↓","1←","2↑","1→","5↑","2→","4↓","2→","3↓","2←","2↑","2←","2↓","3→","2↓","4→","2↑","3→","3↑","2→","3↓","5→","4↑","2←","3↑","4→","3↑","4←","3↓","2→","2↑","4→","4↑","4←","2↑","4→","1↓","1←","3↓","3→","3↑","2←","3↑","3←","1↑","3→",},
                            }
                        }
                    },
                    Answer = "4;5;4;24;14;15;6;6;3;6;8;4;13;9;3;5",
                    ExampleAnswer = "5;8;9;12;43;43;3;7;1;4",
                    Minutes = 12f,
                }
            },
            {
                CaseQuestionEnum.MEWC2021ItIsASnakeLevel3,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2021ItIsASnakeLevel3,
                    QuestionText = "The snake initially appears in the top left of its grid (A1).  The game will be over if it hits a wall.\r\n\r\nApples appear in sequence, the first appearing immediately and the next appearing when the previous one is eaten.\r\n\r\nHow many apples will the snake eat before it hits a wall or reaches 50 moves?\r\nAnswer as a semi-colon joined list of one answer for each row.",
                    QuestionLink = "https://fmworldcup.com/product/fmwc-open-dec-8-2021-its-a-snake/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "AppleOrder",
                            new object[,]
                            {
                                {"I4","L19","F7","I10","J12","D8","F16","E16","A7","E2","C6","D19","E9","J16","I16","L1","E13","B15","A9","G5" }
                            }
                        },
                        {
                            "Snake Grid",
                            _snakeGrid
                        },
                        {
                            "Games (One row per game)",
                            new object[,]
                            {
                                {"3↓","7→","1↓","4←","4↓","10→","1↓","12←","7↑","9→","2↑","1←","6↓","1←","3↑","5←","4↓","3→","7↑","1→","5↓","2←","5↓","2←","1↓","8→","1↑","7←","4↑","4←","1↓","5→","2↓","1→","1↑","3→","8↑","6←","9↓","5→","9↑","5←","7↓","4→","6↑","4←","5↓","16→","2↓","1←",},
                                {"6↓","3→","3↓","15→","5↑","1←","1↓","2→","1↑","5←","1↓","7←","3↓","4→","1↑","8←","7↑","1→","7↓","3→","4↓","5←","9↑","14→","3↓","4←","3↑","11←","9↓","1→","11↑","8→","8↓","2←","2↓","5→","2↑","4←","6↑","5→","2↑","4←","3↓","6→","4↓","3→","1↑","1←","3↑","13←",},
                                {"8↓","1←","2↑","4→","2↓","15→","4↑","1←","1↑","7←","8↓","3→","2↑","6→","3↑","10←","1↓","8→","2↑","12←","2↑","1←","3↓","7→","1↑","5←","5↓","3→","1↑","6→","6↑","1→","5↓","2→","5↑","6←","2↓","2←","1↑","9←","3↑","5→","1↑","13→","1↑","12←","7↓","2→","2↑","1←",},
                                {"8↓","3→","1↑","15→","4↓","12←","1↑","3→","1↑","4→","3↑","5→","5↓","13←","1↑","5←","3↑","13→","2↓","4→","5↑","6←","2↑","8←","1↑","1←","7↓","15→","8↑","7←","4↓","1←","6↓","8→","8↑","1←","1↑","15←","4↓","5→","3↓","2←","1↓","1←","5↑","5→","2↑","1←","8↓","2→",},
                                {"4↓","3→","4↓","15→","3↓","12←","8↑","5←","1↑","1→","1↑","7→","9↓","1←","4↑","9→","4↓","1←","1↑","3→","2↑","11←","4↑","4←","3↓","9→","3↑","5→","7↓","13←","5↑","6→","1↑","5←","1↑","8→","7↓","5→","6↑","18←","3↑","14→","1↑","4←","3↓","2→","5↓","3←","3↓","2←",},
                                {"8↓","3→","3↓","15→","6↑","5←","1↑","3←","1↑","2←","6↓","1←","1↑","1←","1↑","8→","5↑","1←","1↓","4←","2↑","1←","2↓","4←","1↑","6→","6↓","9→","3↓","1←","1↑","16←","9↑","2←","8↓","1←","3↑","15→","6↑","10←","1↓","19→","1↑","4←","7↓","5←","2↑","5→","5↓","5→",},
                                {"8↓","3→","1↑","15→","4↓","16←","3↑","17→","5↑","7←","3↑","1←","4↓","3←","4↓","1←","1↑","1←","2↑","10→","2↑","15←","3↓","2→","5↑","10→","5↓","10←","2↑","9→","2↓","1→","2↓","1←","1↑","4←","5↑","1←","1↑","1←","10↓","8→","9↑","1→","8↓","7←","7↑","6→","2↑","5→",},
                                {"8↓","3→","3↓","15→","6↑","12←","5↓","1←","8↑","8→","2↑","3←","1↓","5→","5↓","6←","1↓","4→","4↑","11←","6↓","17→","6↑","17←","2↑","4→","6↓","5→","1↓","5→","4↑","11←","1↑","9→","1↑","6←","2↑","10→","3↓","1→","4↓","8←","4↑","1←","1↑","9→","3↓","14←","2↑","8→",},
                                {"8↓","3→","3↓","15→","6↑","12←","2↓","3→","1↓","8←","5↑","12→","1↑","8←","7↓","9→","2↓","14←","9↑","1←","2↓","3→","2↑","11→","1↑","8←","4↓","5←","3↑","12→","5↓","2←","4↑","10←","3↓","4→","5↑","3→","5↓","5→","3↓","6→","3↑","15←","2↑","14→","1↑","14←","3↑","8→",},
                                {"8↓","3→","3↓","15→","6↑","12←","6↓","6→","1↑","6→","10↑","14←","9↓","7→","1↓","3←","5↑","1←","2↓","12→","4↑","17←","7↓","15→","6↑","2←","6↓","15←","10↑","17→","2↓","2←","7↓","10←","2↑","7→","1↑","7←","6↑","1←","11↓","10→","4↑","11←","2↑","8→","3↓","3←","1↓","9→",},
                                {"8↓","3→","3↓","15→","7↑","14←","1↑","2→","1↓","1←","2↓","2←","2↓","16→","1↓","18←","7↑","1←","1↓","15→","4↓","4→","3↑","17←","7↓","2←","8↑","18→","6↓","14←","2↑","2→","7↑","11→","9↓","9←","1↑","1←","7↑","4→","10↓","2→","7↑","12←","1↑","19→","1↑","1←","6↓","9←",},
                                {"6↓","8→","2↓","5←","3↓","15→","11↑","18←","11↓","18→","11↑","5←","11↓","6→","11↑","9←","7↓","4→","1↑","2←","1↓","1←","2↓","4→","6↑","5←","8↓","3→","2↑","3←","8↑","6→","7↓","3→","7↑","19←","1↑","5→","9↓","4←","7↑","15→","1↑","11←","2↓","9→","1↑","13←","9↓","1→",},
                                {"8↓","3→","4↑","4→","7↓","4←","11↑","6→","5↓","2→","4↑","1→","4↓","3←","6↓","7→","11↑","4←","1↓","9←","9↓","4→","4↑","4→","1↓","1←","2↑","1←","5↓","8→","7↑","13←","2↓","8→","6↓","1←","2↑","2→","8↑","4→","1↓","6←","1↑","5→","2↓","1←","2↑","3←","7↓","6→",},
                                {"10↓","12→","2↑","2←","5↑","4←","6↓","6→","4↑","5←","6↓","2→","5↑","2→","6↑","9←","6↓","1←","6↑","2→","6↓","3←","5↓","1→","5↑","9→","2↑","8←","2↑","1→","9↓","9→","11↑","2←","3↓","1→","7↓","7←","10↑","8→","5↓","12←","4↓","2→","9↑","6→","10↓","8→","8↑","1←",},
                                {"2↓","3→","2↓","1←","4↓","1→","3↑","12→","3↑","3←","5↓","5←","2↑","2→","4↓","8→","2↑","5←","3↓","8←","6↑","3→","4↓","6→","1↑","8←","1↑","11→","2↓","12←","8↑","15→","5↓","15←","3↑","3→","1↑","2→","1↑","8→","5↓","16←","1↓","18→","5↑","9←","1↓","9←","2↑","12→",},
                                {"8↓","3→","8↑","15→","4↓","11←","6↓","8→","3↑","1→","1↑","14←","1↑","2→","3↑","15→","6↓","11←","3↑","5←","4↓","1→","1↑","1→","1↑","3→","3↑","6→","1↓","2→","6↓","3←","6↑","1←","3↑","5→","1↑","5←","9↓","8←","5↑","4←","1↑","4→","1↑","4→","7↓","1←","3↑","3←",},
                            }
                        }
                    },
                    Answer = "1;1;1;3;4;2;3;3;6;5;3;2;1;1;1;1;1;5;3;1;1",
                    ExampleAnswer = "5;8;9;12;43;43;3;7;1;4",
                    Minutes = 15f,
                }
            },
            {
                CaseQuestionEnum.MEWC2022TheSocialNetworkL1,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2022TheSocialNetworkL1,
                    QuestionText = "In thie grid the 👥 icon indicates a friendship and the ★ a close friendship.\r\n\r\nHow many friends do the people below have?\r\n\r\nAnswer as a semi-colon joined list of the answers.",
                    QuestionLink = "https://fmworldcup.com/product/the-social-network-microsoft-excel-world-championship-2022",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "People",
                            new object[,]
                            {
                                {"Aaron"},
                                {"Niall"},
                                {"Olivia"},
                                {"Gary"},
                                {"Rufus"},
                                {"Uma"},
                                {"Derek"},
                                {"Harley"},
                                {"Olivia"},
                                {"Yasmin"},
                                {"Valerie"}
                            }
                        },
                        {
                            "Social Network",
                            _socialNetwork
                        }
                    },
                    Answer = "11;5;7;8;11;6;11;7;7;11;10",
                    ExampleAnswer = "5;8;9;12;43;43;3;7;1;4",
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.MEWC2022TheSocialNetworkL2,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2022TheSocialNetworkL2,
                    QuestionText = "In the grid the 👥 icon indicates a friendship and the ★ a close friendship.\r\n\r\nOut of these people's friends, who comes earliest in the alphabet?\r\n\r\nAnswer as a semi-colon joined list of the answers.",
                    QuestionLink = "https://fmworldcup.com/product/the-social-network-microsoft-excel-world-championship-2022",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "People",
                            new object[,]
                            {
                                {"Aaron"},
                                {"Wynn"},
                                {"Yasmin"},
                                {"Xavier"},
                                {"Anna"},
                                {"Viktor"},
                                {"Zack"},
                                {"Mickey"},
                                {"Fred"},
                                {"Rebecca"},
                                {"Gary"},
                                {"Charlotte"},
                                {"Henry"},
                                {"Kara"},
                                {"Betty"},
                                {"Elena"},
                                {"India"},
                                {"Sarah"},
                                {"Nicki"},
                                {"Zoey"},
                                {"Niall"},
                            }
                        },
                        {
                            "Social Network",
                            _socialNetwork
                        }
                    },
                    Answer = "Betty;Aaron;Boris;Delia;Betty;Boris;Delia;Charlie;Charlotte;Delia;Betty;Fred;Aaron;Anna;Aaron;Aaron;Delia;Aaron;Boris;Henry;Charlotte",
                    ExampleAnswer = "Aaron;Aaron;Aaron;Aaron;Aaron;Aaron;Aaron;Aaron;Aaron;Aaron;Aaron;Aaron;Aaron;Aaron;Betty",
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.MEWC2022TheSocialNetworkL3,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2022TheSocialNetworkL3,
                    QuestionText = "In the grid the 👥 icon indicates a friendship and the ★ a close friendship.\r\n\r\nOut of these pairs of people, how many friends do they have in common?\r\n\r\nAnswer as a semi-colon joined list of the answers.",
                    QuestionLink = "https://fmworldcup.com/product/the-social-network-microsoft-excel-world-championship-2022",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Pairs of friends",
                            new object[,]
                            {
                                {"Aaron","Betty",},
                                {"Harley","Johnny",},
                                {"Viktor","Fred",},
                                {"Kieran","Betty",},
                                {"Elena","Viktor",},
                                {"Wynn","Anna",},
                                {"Henry","Rebecca",},
                                {"Ulysses","Zoey",},
                                {"Patrick","Ximena",},
                                {"Nicki","Lara",},
                                {"India","Gertrude",},
                                {"Johnny","Charlie",},
                                {"Yavin","Leon",},
                                {"Lara","Harley",},
                                {"Aaron","Willem",},
                                {"Willem","Mickey",},
                                {"Derek","Valerie",},
                                {"Joanna","Henry",},
                                {"Gertrude","Gary",},
                                {"Boris","Kara",},
                                {"Charlotte","Sarah",},
                            }
                        },
                        {
                            "Social Network",
                            _socialNetwork
                        }
                    },
                    Answer = "2;4;1;2;3;2;1;0;0;0;0;3;2;2;4;1;2;2;3;2;2",
                    ExampleAnswer = "2;3;4;1;4;5;2;1;4;5;6;2",
                    Minutes = 8f,
                }
            },
            {
                CaseQuestionEnum.MEWC2022TheSocialNetworkL4,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2022TheSocialNetworkL4,
                    QuestionText = "In the grid the 👥 icon indicates a friendship and the ★ a close friendship.\r\n\r\nOut of these pairs of people, how many friends do they have if you combine them?  (Note that if they are friends with each other they will both appear.)\r\n\r\nAnswer as a semi-colon joined list of the answers.",
                    QuestionLink = "https://fmworldcup.com/product/the-social-network-microsoft-excel-world-championship-2022",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Pairs of friends",
                            new object[,]
                            {
                                {"Aaron","Betty",},
                                {"Ivan","Charlie",},
                                {"Viktor","Theo",},
                                {"Derek","Kieran",},
                                {"Delia","Nicki",},
                                {"Ulysses","Leon",},
                                {"Yasmin","Fiona",},
                                {"Joanna","Derek",},
                                {"Owen","Moana",},
                                {"Uma","Stuart",},
                                {"Tamsin","Gertrude",},
                                {"India","Aaron",},
                                {"Boris","Kara",},
                                {"Charlie","Betty",},
                                {"Johnny","Yasmin",},
                                {"Valerie","Joanna",},
                                {"Anna","Rebecca",},
                                {"Xavier","Ximena",},
                                {"Rufus","Penny",},
                                {"Fiona","Xavier",},
                                {"Penny","Ivan",},
                            }
                        },
                        {
                            "Social Network",
                            _socialNetwork
                        }
                    },
                    Answer = "18;18;14;17;21;15;18;18;13;13;18;15;19;21;14;18;10;13;20;12;17",
                    ExampleAnswer = "2;3;4;1;4;5;2;1;4;5;6;2",
                    Minutes = 8f,
                }
            },
            {
                CaseQuestionEnum.MEWC2022TheSocialNetworkL5,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2022TheSocialNetworkL5,
                    QuestionText = "In the grid the 👥 icon indicates a friendship and the ★ a close friendship.\r\n\r\nWhen someone makes a post, their friends and close friends will see it, and their close friends will also share it such that their friends will also see it.  Do not count the poster themselves.\r\n\r\nHow many people would see a post by these people?\r\n\r\nAnswer as a semi-colon joined list of the answers.",
                    QuestionLink = "https://fmworldcup.com/product/the-social-network-microsoft-excel-world-championship-2022",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "People",
                            new object[,]
                            {
                                {"Aaron",},
                                {"Patrick",},
                                {"Viktor",},
                                {"Edward",},
                                {"Ulysses",},
                                {"Charlotte",},
                                {"Johnny",},
                                {"Leon",},
                                {"Kara",},
                                {"Gertrude",},
                                {"Lara",},
                                {"Ximena",},
                                {"Theo",},
                                {"Stuart",},
                                {"Moana",},
                                {"Anna",},
                                {"Zoey",},
                                {"Yasmin",},
                                {"Olivia",},
                                {"Yavin",},
                                {"Boris",},
                                {"Penny",},
                                {"Fred",},
                                {"Gary",},
                                {"Joanna",},
                                {"Valerie",},
                                {"Derek",},
                                {"Willem",},
                                {"Betty",},
                                {"Sarah",},
                                {"Derek",},
                            }
                        },
                        {
                            "Social Network",
                            _socialNetwork
                        }
                    },
                    Answer = "24;6;22;23;7;7;16;13;34;11;11;38;18;14;20;25;9;29;25;21;16;17;25;23;19;10;11;17;19;30;11",
                    ExampleAnswer = "2;3;4;1;4;5;2;1;4;5;6;2",
                    Minutes = 12f,
                }
            },
            {
                CaseQuestionEnum.MEWC2022TheSocialNetworkB1,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2022TheSocialNetworkB1,
                    QuestionText = "In the grid the 👥 icon indicates a friendship and the ★ a close friendship.\r\n\r\nIf each close friendship counts as 3 normal friendships, who has the most friendships?",
                    QuestionLink = "https://fmworldcup.com/product/the-social-network-microsoft-excel-world-championship-2022",
                    Data = new Dictionary<string, object[,]>()
                    {
                        { "Social Network", _socialNetwork }
                    },
                    Answer = "Kara",
                    ExampleAnswer = "Aaron",
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.MEWC2022TheSocialNetworkB2,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2022TheSocialNetworkB2,
                    QuestionText = "In the grid the 👥 icon indicates a friendship and the ★ a close friendship.\r\n\r\nWhen someone shares a post their friends & close friends see it, and their close friends share it such that their friends also see it.  Do not count the poster themselves.\r\n\r\nWhich person would have their post seen by the fewest people?",
                    QuestionLink = "https://fmworldcup.com/product/the-social-network-microsoft-excel-world-championship-2022",
                    Data = new Dictionary<string, object[,]>()
                    {
                        { "Social Network", _socialNetwork }
                    },
                    Answer = "Rebecca",
                    ExampleAnswer = "Aaron",
                    Minutes = 5f,
                }
            },
            {
                CaseQuestionEnum.MEWC2022TheSocialNetworkB2,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2022TheSocialNetworkB2,
                    QuestionText = "In the grid the 👥 icon indicates a friendship and the ★ a close friendship.\r\n\r\nHow many triangles of friends (including close friends) are there in the network?\r\n3 different friends are in a triangle if they are all friends with each other.",
                    QuestionLink = "https://fmworldcup.com/product/the-social-network-microsoft-excel-world-championship-2022",
                    Data = new Dictionary<string, object[,]>()
                    {
                        { "Social Network", _socialNetwork }
                    },
                    Answer = "107",
                    ExampleAnswer = 100d,
                    Minutes = 15f,
                }
            },
            {
                CaseQuestionEnum.MEWC2023LanaBananaL1,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2023LanaBananaL1,
                    QuestionText = "If Lana starts in these cells and moves straight down without using diagonals, how many bananas will she collect?  (Use the key rather than the actual Excel cell addresses.)\r\n\r\nIf she starts on a cell with a banana, she can grab it immediately.\r\n\r\nWrite the sum of the answers for the different starting cells.",
                    QuestionLink = "https://fmworldcup.com/product/lana-banana-mewc-2023/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Starting cells",
                            new object[,]
                            {
                                {"D31"},
                                {"AE36"},
                                {"J25"},
                                {"AG19"},
                                {"AI10"},
                                {"D24"},
                                {"Y18"},
                                {"X24"},
                                {"Z29"},
                                {"AH27"},
                                {"C18"},
                            }
                        },
                        {
                            "Lana Banana", 
                            _lanaBanana
                        },
                    },
                    Answer = "32",
                    ExampleAnswer = 30,
                    Minutes = 7f,
                }
            },
            {
                CaseQuestionEnum.MEWC2023LanaBananaL2,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2023LanaBananaL2,
                    QuestionText = "If Lana starts in these cells and moves in these directions, how many bananas will she collect?  (Use the key rather than the actual Excel cell addresses.)\r\n\r\nIf she starts on a cell with a banana, she can grab it immediately.\r\n\r\nWrite the sum of the answers for the different starting cells.",
                    QuestionLink = "https://fmworldcup.com/product/lana-banana-mewc-2023/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Starting cells and directions",
                            new object[,]
                            {
                                {"D31","↙",},
                                {"AE36","↘",},
                                {"J25","↘",},
                                {"AG19","↙",},
                                {"AI10","↙",},
                                {"D24","↘",},
                                {"Y18","↘",},
                                {"X24","↙",},
                                {"Z29","↘",},
                                {"AH27","↘",},
                                {"C18","↙",},
                                {"AN25","↙",},
                                {"H2","↙",},
                                {"AN40","↙",},
                                {"B30","↙",},
                                {"Q30","↘",},
                                {"K29","↘",},
                                {"M10","↙",},
                                {"V38","↙",},
                                {"AI6","↘",},
                                {"N16","↘",},
                            }
                        },
                        {
                            "Lana Banana",
                            _lanaBanana
                        },
                    },
                    Answer = "61",
                    ExampleAnswer = 30,
                    Minutes = 12f,
                }
            },
            {
                CaseQuestionEnum.MEWC2023LanaBananaL3,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2023LanaBananaL3,
                    QuestionText = "Lana can move down, down-left or down-right on each move.  If she starts on these cells on the map, what is the highest number of bananas she can collect?  (Use the key rather than the actual Excel cell addresses.)\r\n\r\nIf she starts on a cell with a banana, she can grab it immediately.\r\n\r\nWrite the sum of the answers for the different starting cells.",
                    QuestionLink = "https://fmworldcup.com/product/lana-banana-mewc-2023/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Starting cells",
                            new object[,]
                            {
                                {"AD38",},
                                {"E38",},
                                {"F38",},
                                {"AG38",},
                                {"V38",},
                                {"P38",},
                                {"O38",},
                                {"X38",},
                                {"AB38",},
                                {"AH38",},
                                {"I38",},
                                {"AA38",},
                                {"R38",},
                                {"AN38",},
                                {"G38",},
                                {"Q38",},
                                {"M38",},
                                {"J38",},
                                {"AE38",},
                                {"Z38",},
                                {"L38",},
                            }
                        },
                        {
                            "Lana Banana",
                            _lanaBanana
                        },
                    },
                    Answer = "29",
                    ExampleAnswer = 30,
                    Minutes = 8f,
                }
            },
            {
                CaseQuestionEnum.MEWC2023LanaBananaL4,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2023LanaBananaL4,
                    QuestionText = "Lana can move down, down-left or down-right on each move.  If she starts on these cells on the map, what is the highest number of bananas she can collect?  (Use the key rather than the actual Excel cell addresses.)\r\n\r\nIf she starts on a cell with a banana, she can grab it immediately.\r\n\r\nWrite the sum of the answers for the different starting cells.",
                    QuestionLink = "https://fmworldcup.com/product/lana-banana-mewc-2023/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Starting cells",
                            new object[,]
                            {
                                {"AF4",},
                                {"J35",},
                                {"AH33",},
                                {"N2",},
                                {"V34",},
                                {"AE25",},
                                {"J5",},
                                {"C8",},
                                {"D7",},
                                {"AF7",},
                                {"P21",},
                                {"X7",},
                                {"P30",},
                                {"AJ14",},
                                {"AG23",},
                                {"AC24",},
                                {"O39",},
                                {"Y36",},
                                {"D8",},
                                {"AN9",},
                                {"S2",},
                            }
                        },
                        {
                            "Lana Banana",
                            _lanaBanana
                        },
                    },
                    Answer = "332",
                    ExampleAnswer = 310,
                    Minutes = 8f,
                }
            },
            {
                CaseQuestionEnum.MEWC2023LanaBananaL5,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2023LanaBananaL5,
                    QuestionText = "Lana can move down, down-left or down-right on each move.  She cannot eat bananas on consecutive turns.  If she starts on these cells on the map, what is the highest number of bananas she can collect?  (Use the key rather than the actual Excel cell addresses.)\r\n\r\nIf she starts on a cell with a banana, she can grab it immediately.\r\n\r\nWrite the sum of the answers for the different starting cells.",
                    QuestionLink = "https://fmworldcup.com/product/lana-banana-mewc-2023/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Starting cells",
                            new object[,]
                            {
                                {"AM28",},
                                {"AK10",},
                                {"AD4",},
                                {"AN10",},
                                {"G16",},
                                {"AF9",},
                                {"O35",},
                                {"S16",},
                                {"AM4",},
                                {"G27",},
                                {"K29",},
                                {"R4",},
                                {"W16",},
                                {"AC23",},
                                {"AH20",},
                                {"H21",},
                                {"S26",},
                                {"F31",},
                                {"AK8",},
                                {"AE22",},
                                {"M34",},
                                {"X25",},
                                {"W16",},
                                {"Z17",},
                                {"Z11",},
                                {"Y19",},
                                {"H5",},
                                {"AM27",},
                                {"B16",},
                                {"AB21",},
                                {"AB6",},
                            }
                        },
                        {
                            "Lana Banana",
                            _lanaBanana
                        },
                    },
                    Answer = "303",
                    ExampleAnswer = 310,
                    Minutes = 10f,
                }
            },
            {
                CaseQuestionEnum.MEWC2023LanaBananaB1,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2023LanaBananaB1,
                    QuestionText = "How many 2x2 grids which contain at least 3 bananas are on the map?",
                    QuestionLink = "https://fmworldcup.com/product/lana-banana-mewc-2023/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Lana Banana",
                            _lanaBanana
                        },
                    },
                    Answer = "37",
                    ExampleAnswer = 35,
                    Minutes = 3f,
                }
            },
            {
                CaseQuestionEnum.MEWC2023LanaBananaB2,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.MEWC2023LanaBananaB2,
                    QuestionText = "How many distinct pairs of neighbouring bananas are there?  Two bananas are neighbouring if they are in adjacent cells horizontally, vertically or diagonally.",
                    QuestionLink = "https://fmworldcup.com/product/lana-banana-mewc-2023/",
                    Data = new Dictionary<string, object[,]>()
                    {
                        {
                            "Lana Banana",
                            _lanaBanana
                        },
                    },
                    Answer = "204",
                    ExampleAnswer = 200,
                    Minutes = 3f,
                }
            },
        };
    }
}
