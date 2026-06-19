using System;
using ConsoleApp1.Services;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GameService gameService = new GameService();
            MockDataService mockDataService = new MockDataService(gameService);

            Console.WriteLine("測試輸入功能區");

            Console.WriteLine("1.後台注入資料:");
            mockDataService.MockScoreInput("吳先生", "翻牌遊戲", 100 ,3);
            mockDataService.MockScoreInput("吳先生", "翻牌遊戲", 90, 2);
            mockDataService.MockScoreInput("陳小姐", "翻牌遊戲", 60, 5);
            mockDataService.MockScoreInput("王先生", "翻牌遊戲", 70, 7);
            mockDataService.MockScoreInput("MR.KUO", "翻牌遊戲", 120, 2);
            mockDataService.MockScoreInput("黃小姐", "翻牌遊戲", 30, 0);//不會出現在排行榜

            mockDataService.MockScoreInput("王先生", "水果忍者", 300, 0);
            mockDataService.MockScoreInput("陳小姐", "水果忍者", 100,3);

            mockDataService.MockScoreInput("吳小姐", "方塊消除", 1000,4);

            Console.WriteLine("2.登入測試");
            Console.Write("請輸入用戶名稱:");
            string NAME = Console.ReadLine();
            //Console.WriteLine(NAME);

            User currentUser = gameService.Login(NAME);
            Console.WriteLine("登入成功，歡迎 " + currentUser.Username);
            string gameName = "";//初始為空字符串(除錯""不能空格，會判定為空也能選擇)
            while (string.IsNullOrEmpty(gameName))//IsNullOrEmpty方法檢查gameName是否為空或null
            {
                Console.WriteLine("3.選擇遊戲並寫入成績");
                Console.WriteLine("請選擇遊戲: 1.翻牌遊戲 2.水果忍者 3.方塊消除");
                Console.Write("請輸入遊戲編號:");

                string gameChoice = Console.ReadLine();

               
                switch (gameChoice)//根據用戶輸入的遊戲編號選擇遊戲名稱
                {
                    case "1": gameName = "翻牌遊戲"; break;
                    case "2": gameName = "水果忍者"; break;
                    case "3": gameName = "方塊消除"; break;
                    default:
                        Console.WriteLine("無效的遊戲編號，請重新選擇!");
                        continue;
                }
            }//根據用戶輸入的遊戲編號選擇遊戲名稱
            int Mockgamescore = 1500;
            gameService.SavePlayerScore(currentUser.Username, gameName, Mockgamescore);
            Console.WriteLine($"選擇{gameName}遊玩,並獲得{Mockgamescore}的分數!");

            Console.WriteLine("4.查看排行榜");
            string[] games = new string[] { "翻牌遊戲", "水果忍者", "方塊消除" };
            foreach (var game in games)
            {
                Console.WriteLine($"排行榜 | {game}:");
                var topScores = gameService.GetTopScores(game);
                if (topScores.Count == 0)
                {
                    Console.WriteLine("目前沒有成績紀錄。");
                }
                else
                {
                    int rank = 1;
                    foreach (var score in topScores)
                    {
                        Console.WriteLine($"第{rank}名 | {score.Username} | {score.Score} 分 ");
                        rank++;
                    }
                }
                Console.WriteLine();
            }

        }
    }

}