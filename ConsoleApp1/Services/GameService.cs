using System;
using System.Collections.Generic;
using ConsoleApp1.Data;
using ConsoleApp1.Models;

namespace ConsoleApp1.Services
{
    public class GameService
    {
        private readonly DatabaseContext _db = new DatabaseContext();

        public GameService()
        {
            _db.ResetDatabase();//重置資料庫

            _db.InitializeDatabase();//初始化資料庫
        }

        public User Login(string username)
        {
            if (username == null)
            {
                throw new ArgumentException("請輸入名稱");
            }
            return new User(username);
        }//用戶登入，檢查用戶名稱是否為空，若為空則拋出異常

        public void SavePlayerScore(string username, string gameName, int score)
        {
            var newScore = new GameScore(username, gameName, score, DateTime.Now);//DateTime.Now表示當下時間
            _db.InsertScore(newScore);//將新的成績插入資料庫
        }

        public List<GameScore> GetTopScores(string gameName)
        {
            return _db.GetTopScores(gameName);//從資料庫獲取指定遊戲的前5名成績
        }

        
    }
}//包含用戶登入、保存遊戲成績和獲取前5名成績的方法，使用DatabaseContext與資料庫進行交互