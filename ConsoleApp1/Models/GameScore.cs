using System;

namespace ConsoleApp1.Models
{
    public class GameScore
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string GameName { get; set; }//遊戲名稱
        public int Score { get; set; }
        public DateTime PlayDate { get; set; }//日期
        public GameScore(string username, string gameName, int score, DateTime playDate)
        {
            Username = username;
            GameName = gameName;
            Score = score;
            PlayDate = playDate;
        }
    }
}
//設定遊戲分數類別，包含Id、Username、GameName、Score和PlayDate