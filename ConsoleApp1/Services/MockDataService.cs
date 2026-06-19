using System;
using ConsoleApp1.Models;

namespace ConsoleApp1.Services
{
    public class MockDataService
    {

        private readonly GameService _gameService;

        public MockDataService(GameService gameService)
        {
            _gameService = gameService;
        }// 模擬用戶登入和成績輸入

        public void MockScoreInput(string username, string gameName, int score, int daysAgo = 0) //daysAgo表示幾天前的成績，預設為0表示今天
        {
            _gameService.SavePlayerScore(username, gameName, score);//保存成績到資料庫
            Console.WriteLine($"[後台回報]手動輸入成功：用戶 {username} 在 {gameName} 中獲得了 {score} 分，日期為 {DateTime.Now.AddDays(-daysAgo):yyyy-MM-dd}");
            //輸出模擬輸入的成績信息
        }

    }
}// 成績後台輸入