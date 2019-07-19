using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;

public class DataTable
{
    public int gold;
    public int bestScore;
    public int skin;

    public DataTable(int _gold, int _bestScore, int _skin)
    {
        gold = _gold;
        bestScore = _bestScore;
        skin = _skin;
    }
}

public enum Test_DB_Table    // 테이블 이름
{
    DatabaseTable,
    MAX
}

public class DatabaseManager : MonoSingleton<DatabaseManager>
{
    public List<DataTable> ItemList = new List<DataTable>();  // db에 있는 아이템을 저장할 리스트

    string m_NameDB = "TestDB.db";  // db 파일 이름
    string Filepath = string.Empty;

    public void Load()
    {
        LoadDB();
        LoadTable();
    }

    // DB파일이 있는지 검사하고 없으면 생성
    public void LoadDB()
    {
        Filepath = Application.streamingAssetsPath + "/" + m_NameDB;

        if (Application.platform == RuntimePlatform.Android)
        {
            if (!File.Exists(Filepath))
            {
                Debug.LogWarning("File \"" + Filepath + "\" does not exist. Attempting to create from \"" +
                                 Application.dataPath + "!/assets/" + m_NameDB);

                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + m_NameDB);
                while (!loadDB.isDone) { }
                File.WriteAllBytes(Filepath, loadDB.bytes);
            }
        }
        else
        {
            if (!File.Exists(Filepath))
            {
                WWW loadDB = new WWW("file://" + Filepath);
                while (!loadDB.isDone) { }
            }
        }
    }

    // 테이블이 있는지 검사하고 없으면 생성
    public void LoadTable()
    {
        bool isTable = true;
        string connectionString = "URI=file:" + Filepath;

        // using을 사용함으로써 비정상적인 예외가 발생할 경우 using 블록을 빠져나갈 때 자동적으로 Dispose 메소드를 호출한다.
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
            {
                string sqlQuery = "SELECT count(*) FROM sqlite_master WHERE type = 'table'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
                {
                    reader.Read();

                    // 테이블 갯수 : 여기서 1개(TestTable)
                    if (reader.GetInt32(0) < 1)
                    {
                        isTable = false;
                    }

                    reader.Close();
                    dbConnection.Close();
                }
            }
        }

        if (isTable == false)
        {
            CreateTable_Item();

            Debug.Log("테이블 생성!");
        }
        else
        {
            SelectItemTable();
        }
    }

    // 테이블 생성
    public void CreateTable_Item()
    {
        // 테이블 갯수를 비교하여 테이블 생성
        string connectionString = "URI=file:" + Filepath;

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
            {

                // 테이블을 생성하는 SQL 쿼리문
                string sqlQuery = "CREATE TABLE `" + Test_DB_Table.DatabaseTable.ToString() + "`( `Gold` INTEGER NOT NULL, `BestScore` INTEGER NOT NULL, `Skin` INTEGER NOT NULL)";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
                {
                    reader.Close();
                    dbCmd.Dispose();
                    dbConnection.Close();
                }
            }
        }
    }

    // 테이블 삭제
    public void DropTable(string _name)
    {
        string connectionString = "URI=file:" + Filepath;

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
            {
                string sqlQuery = "DROP Table '" + _name.ToString() + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
                {
                    reader.Close();
                    dbCmd.Dispose();
                    dbConnection.Close();
                }
            }
        }
    }

    // 모든 데이터 조회
    public void SelectItemTable()
    {
        string connectionString = "URI=file:" + Filepath;

        ItemList.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
            {
                string sqlQuery = "SELECT * FROM " + Test_DB_Table.DatabaseTable.ToString();
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
                {
                    while (reader.Read())
                    {
                        // Debug.Log(reader.GetString(1));  //  타입명 . (몇 열에있는것을 부를것인가)
                        ItemList.Add(new DataTable(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                    }

                    reader.Close();
                    dbCmd.Dispose();
                    dbConnection.Close();
                }
            }
        }

        for (int i = 0; i < ItemList.Count; i++)
        {
            Debug.Log(ItemList[i].gold + "::" + ItemList[i].bestScore + "::" + ItemList[i].skin);
        }
    }

    // 속성값 수정
    public void UpdateItemTable(int _gold, int _bestScore, int _skin)
    {
        string connectionString = "URI=file:" + Filepath;

        ItemList.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
            {
                string sqlQuery = "UPDATE " + Test_DB_Table.DatabaseTable.ToString() + " SET Gold='" + _gold + "', BestScore='" + _bestScore + "', Skin='" + _skin + "'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
                {
                    reader.Close();
                    dbCmd.Dispose();
                    dbConnection.Close();
                }
            }
        }
    }

    // TEST_CODE
    public void test()
    {
        SelectItemTable();
        UpdateItemTable(1, 8, 2);
        SelectItemTable();


        //Skin 관련 진수변환 코드
        // 10진수 to 2진수
        int t = 8;
        List<int> test = new List<int>();
        for (int i = 0; t > 0; i++)
        {
            test.Add(t % 2);
            t = t / 2;
        }
        for (int i = 0; i < test.Count; i++)
        {
            Debug.Log(test[i]);
        }
        Debug.Log("----------------");

        // 2진수 to 10진수
        int re = 0;
        for (int i = 0; i < test.Count; i++)
        {
            re += test[i] * (int)Mathf.Pow(2, i);
        }
        Debug.Log(re);
    }
}
