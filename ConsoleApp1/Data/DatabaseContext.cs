using ConsoleApp1.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace ConsoleApp1.Data
{
    public class DatabaseContext
    {
        private const string ConnectionString = "Data Source = DatabaseContext.db";

        public void InitializeDatabase()
        {
            using (var con = new SqliteConnection(ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Scores (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL,
                        GameName TEXT NOT NULL,
                        Score INTEGER NOT NULL,
                        PlayDate TEXT NOT NULL
                    );";
                cmd.ExecuteNonQuery();
            }
        }//資料庫初始化，建立Scores表格

        public void InsertScore(GameScore score)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                 INSERT INTO Scores (Username, GameName, Score, PlayDate)
                 VALUES ($username, $gameName, $score, $playDate);";

                cmd.Parameters.AddWithValue("$username", score.Username);
                cmd.Parameters.AddWithValue("$gameName", score.GameName);
                cmd.Parameters.AddWithValue("$score", score.Score);
                cmd.Parameters.AddWithValue("$playDate", score.PlayDate.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();
            }
        }//插入新的成績到Scores表格



            private const string DbFileName = "DatabaseContext.db";

            public void ResetDatabase()
            {
                try
                {
                    if (File.Exists(DbFileName))
                    {
                        // 先解除所有連線鎖定，否則檔案會被系統鎖住不給刪
                        SqliteConnection.ClearAllPools();

                        // 刪除實體檔案
                        File.Delete(DbFileName);
                        //Console.WriteLine("[系統後台] 偵測到程式重新啟動，已成功清理舊的資料庫檔案檔案 (VrGame.db)。");
                    }
                }
                catch (Exception ex)
                {
                   // Console.WriteLine($"[系統後台] 自動重置資料庫時發生阻礙: {ex.Message}");
                }
            }
            public List<GameScore> GetTopScores(string gameName)
        {
            var scores = new List<GameScore>();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                    SELECT Username, GameName, Score, PlayDate
                    FROM Scores
                    WHERE GameName = $gameName
                    ORDER BY Score DESC
                    LIMIT 5;";//按照由高到低順序前5名成績，由低到高的話改成ORDER BY Score ASC 
                cmd.Parameters.AddWithValue("$gameName", gameName);//指定遊戲名稱參數

                using (var reader = cmd.ExecuteReader())
                {
                    int UsernameOrdinal = reader.GetOrdinal("Username");
                    int gameNameOrdinal = reader.GetOrdinal("GameName");
                    int scoreOrdinal = reader.GetOrdinal("Score");
                    int playDateOrdinal = reader.GetOrdinal("PlayDate");
                    //讀取資料庫中的成績資料，並轉換為GameScore物件，最後返回成績列表

                    while (reader.Read())
                    {
                        scores.Add(new GameScore(
                            reader.GetString(UsernameOrdinal),
                            reader.GetString(gameNameOrdinal),
                            reader.GetInt32(scoreOrdinal),
                            DateTime.Parse(reader.GetString(playDateOrdinal))
                        ));
                    }//將讀取到的名稱 遊戲 分數 日期轉換為GameScore物件，並加入到scores列表中
                }
            }
            return scores;//返回成績列表
        }
    }
}