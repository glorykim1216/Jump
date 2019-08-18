using System.Collections.Generic;
using SQLite.Attribute;
using System;

public class JellyDB
{
    [PrimaryKey] public int Id { get; set; }
    public int gold { get; set; }
    public int bestScore { get; set; }
    public int openSkinList { get; set; }
    public int currSkin { get; set; }
    public int upPowerLevel { get; set; }
    public int forwardPowerLevel { get; set; }
    public int offlineGoldLevel { get; set; }
    public int openEffectList { get; set; }
    public int currEffect { get; set; }
    public string dateTime { get; set; }
    public string soundVolume { get; set; }
    public int vibration { get; set; }
    public string deviceID { get; set; }


    public override string ToString()
    {
        return string.Format("[DatabaseTable: Gold={0}, BestScore={1},  OpenSkinList={2}, CurrSkin={3}, UpPowerLevel={4},  ForwardPowerLevel={5}, OfflineGoldLevel={6}, OpenEffectList={7},  CurrEffect={8}, DateTime={9}, SoundVolume={10},  Vibration={11}, DeviceID={12}]",
            this.gold, this.bestScore, this.openSkinList, this.currSkin, this.upPowerLevel, this.forwardPowerLevel, this.offlineGoldLevel, this.openEffectList, this.currEffect, this.dateTime, this.soundVolume, this.vibration, this.deviceID);
    }
}

public enum Test_DB_Table    // 테이블 이름
{
    DatabaseTable,
    MAX
}

public class DatabaseManager : MonoSingleton<DatabaseManager>
{
    public JellyDB ItemList;  // db에 있는 아이템을 저장할 리스트

    string m_NameDB = "MyDB.db";  // db 파일 이름

    DataService ds;

    public void CreateTable()
    {
        ds = new DataService(m_NameDB);

        ds.CreateDB();
    }
    public bool Load()
    {
        ds = new DataService(m_NameDB);

        IEnumerable<JellyDB> DBs = ds.GetJellyDB();

        if (DBs != null)
        {
            foreach (JellyDB DB in DBs)
                ItemList = DB;
        }
        return true;
    }

    public void UpdateItemTable(int _gold, int _bestScore, int _openSkinList, int _currSkin, int _upPowerLevel, int _forwardPowerLevel, int _offlineGoldLevel, int _openEffectList, int _currEffect)
    {
        IEnumerable<JellyDB> DBs = ds.GetJellyDB();

        if (DBs != null)
        {
            foreach (JellyDB DB in DBs)
            {
                DB.gold = _gold;
                DB.bestScore = _bestScore;
                DB.openSkinList = _openSkinList;
                DB.currSkin = _currSkin;
                DB.upPowerLevel = _upPowerLevel;
                DB.forwardPowerLevel = _forwardPowerLevel;
                DB.offlineGoldLevel = _offlineGoldLevel;
                DB.openEffectList = _openEffectList;
                DB.currEffect = _currEffect;

                ItemList = DB;
            }
            ds._connection.UpdateAll(DBs);

        }

    }

    public void UpdateItemTable(string _dateTime)
    {
        IEnumerable<JellyDB> DBs = ds.GetJellyDB();

        if (DBs != null)
        {
            foreach (JellyDB DB in DBs)
            {
                DB.dateTime = _dateTime;

                ItemList = DB;
            }
            ds._connection.UpdateAll(DBs);
        }
    }

    public void UpdateItemTable(string _soundVolume, int _vibration)
    {
        IEnumerable<JellyDB> DBs = ds.GetJellyDB();

        if (DBs != null)
        {
            foreach (JellyDB DB in DBs)
            {
                DB.soundVolume = _soundVolume;
                DB.vibration = _vibration;

                ItemList = DB;
            }
            ds._connection.UpdateAll(DBs);
        }
    }

    public void UpdateItemTable_DeviceID(string _ID)
    {
        IEnumerable<JellyDB> DBs = ds.GetJellyDB();

        if (DBs != null)
        {
            foreach (JellyDB DB in DBs)
            {
                DB.deviceID = _ID;

                ItemList = DB;
            }
            ds._connection.UpdateAll(DBs);
        }
    }

    private void OnApplicationQuit()
    {
        UpdateItemTable(DateTime.Now.ToString("yyyyMMddHHmmss"));
    }



    //public class DataTable
    //{
    //    public int gold;
    //    public int bestScore;
    //    public int openSkinList;
    //    public int currSkin;
    //    public int upPowerLevel;
    //    public int forwardPowerLevel;
    //    public int offlineGoldLevel;
    //    public int openEffectList;
    //    public int currEffect;
    //    public string dateTime;
    //    public string soundVolume;
    //    public int vibration;
    //    public string deviceID;
    //    public DataTable(int _gold, int _bestScore, int _openSkinList, int _currSkin, int _upPowerLevel, int _forwardPower, int _offlineGoldLevel
    //        , int _openEffectList, int _currEffect, string _dateTime, string _soundVolume, int _vibration, string _deviceID)
    //    {
    //        gold = _gold;
    //        bestScore = _bestScore;
    //        openSkinList = _openSkinList;
    //        currSkin = _currSkin;
    //        upPowerLevel = _upPowerLevel;
    //        forwardPowerLevel = _forwardPower;
    //        offlineGoldLevel = _offlineGoldLevel;
    //        openEffectList = _openEffectList;
    //        currEffect = _currEffect;
    //        dateTime = _dateTime;
    //        soundVolume = _soundVolume;
    //        vibration = _vibration;
    //        deviceID = _deviceID;
    //    }
    //}

    //// DB파일이 있는지 검사하고 없으면 생성
    //public bool LoadDB()
    //{
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        Filepath = Application.persistentDataPath + "/" + m_NameDB;

    //        if (!File.Exists(Filepath))
    //        {
    //            Debug.LogWarning("File \"" + Filepath + "\" does not exist. Attempting to create from \"" +
    //                             Application.dataPath + "!/assets/" + m_NameDB);

    //            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + m_NameDB);
    //            loadDB.bytesDownloaded.ToString();
    //            while (!loadDB.isDone) { }
    //            File.WriteAllBytes(Filepath, loadDB.bytes);
    //        }
    //    }
    //    else
    //    {
    //        Filepath = Application.streamingAssetsPath + "/" + m_NameDB;

    //        if (!File.Exists(Filepath))
    //        {
    //            WWW loadDB = new WWW("file://" + Filepath);
    //            while (!loadDB.isDone) { }
    //        }
    //    }
    //    return true;
    //}
    //private static byte[] GetNullTerminatedUtf8(string s)
    //{
    //    int utf8Length = Encoding.UTF8.GetByteCount(s);
    //    byte[] bytes = new byte[utf8Length + 1];
    //    utf8Length = Encoding.UTF8.GetBytes(s, 0, s.Length, bytes, 0);
    //    return bytes;
    //}
    //// 테이블이 있는지 검사하고 없으면 생성
    //public bool LoadTable()
    //{
    //    bool isTable = true;
    //    string connectionString = "URI=file:" + Filepath;

    //    string dbPath = string.Format(@"Assets/StreamingAssets/{0}", m_NameDB);
    //    _connection = new SQLiteConnection(dbPath, "123123");

    //    _connection.CreateTable<JellyDB>();

    //    // using을 사용함으로써 비정상적인 예외가 발생할 경우 using 블록을 빠져나갈 때 자동적으로 Dispose 메소드를 호출한다.
    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();

    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
    //        {
    //            string sqlQuery = "SELECT count(*) FROM sqlite_master WHERE type = 'table'";
    //            dbCmd.CommandText = sqlQuery;

    //            using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
    //            {
    //                reader.Read();

    //                // 테이블 갯수 : 여기서 1개(TestTable)
    //                if (reader.GetInt32(0) < 1)
    //                {
    //                    isTable = false;
    //                }

    //                reader.Close();
    //                dbConnection.Close();
    //            }
    //        }
    //    }

    //    if (isTable == false)
    //    {
    //        CreateTable_Item();
    //        Debug.Log("테이블 생성!");
    //        InsertItemTable();

    //    }
    //    SelectItemTable();

    //    return true;
    //}

    //// 테이블 생성
    //public void CreateTable_Item()
    //{
    //    // 테이블 갯수를 비교하여 테이블 생성
    //    string connectionString = "URI=file:" + Filepath;

    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();

    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
    //        {

    //            // 테이블을 생성하는 SQL 쿼리문
    //            string sqlQuery = "CREATE TABLE `" + Test_DB_Table.DatabaseTable.ToString() +
    //                "`( `Gold` INTEGER NOT NULL, `BestScore` INTEGER NOT NULL, `OpenSkinList` INTEGER NOT NULL, `CurrSkin` INTEGER NOT NULL, `UpPowerLevel` INTEGER NOT NULL, `ForwardPowerLevel` INTEGER NOT NULL, `OfflineGoldLevel` INTEGER NOT NULL, `OpenEffectList` INTEGER NOT NULL, `CurrEffect` INTEGER NOT NULL, `DateTime` TEXT NOT NULL, `SoundVolume` TEXT NOT NULL, `Vibration` INTEGER NOT NULL, `DeviceID` TEXT NOT NULL)";
    //            dbCmd.CommandText = sqlQuery;

    //            using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
    //            {
    //                reader.Close();
    //                dbCmd.Dispose();
    //                dbConnection.Close();
    //            }
    //        }
    //    }
    //}

    //// 테이블 삭제
    //public void DropTable(string _name)
    //{
    //    string connectionString = "URI=file:" + Filepath;

    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();

    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
    //        {
    //            string sqlQuery = "DROP Table '" + _name.ToString() + "'";
    //            dbCmd.CommandText = sqlQuery;

    //            using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
    //            {
    //                reader.Close();
    //                dbCmd.Dispose();
    //                dbConnection.Close();
    //            }
    //        }
    //    }
    //}

    //// 모든 데이터 조회
    //public void SelectItemTable()
    //{
    //    string connectionString = "URI=file:" + Filepath;

    //    ItemList.Clear();

    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();
    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
    //        {
    //            string sqlQuery = "SELECT * FROM " + Test_DB_Table.DatabaseTable.ToString();
    //            dbCmd.CommandText = sqlQuery;

    //            using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
    //            {
    //                while (reader.Read())
    //                {
    //                    // Debug.Log(reader.GetString(1));  //  타입명 . (몇 열에있는것을 부를것인가)
    //                    ItemList.Add(new DataTable(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetString(9), reader.GetString(10), reader.GetInt32(11), reader.GetString(12)));
    //                }

    //                reader.Close();
    //                dbCmd.Dispose();
    //                dbConnection.Close();
    //            }
    //        }
    //    }


    //    //TEST_CODE
    //    //for (int i = 0; i < ItemList.Count; i++)
    //    //{
    //    //    Debug.Log(ItemList[i].gold + "::" + ItemList[i].bestScore + "::" + ItemList[i].openSkinList);
    //    //}
    //}

    //// 테이블에 새로운 투플 삽입
    //public void InsertItemTable()
    //{
    //    string connectionString = "URI=file:" + Filepath;

    //    ItemList.Clear();

    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();
    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
    //        {
    //            string sqlQuery = "INSERT INTO " + Test_DB_Table.DatabaseTable.ToString() +
    //                " (Gold, BestScore, OpenSkinList, CurrSkin, UpPowerLevel, ForwardPowerLevel, OfflineGoldLevel, OpenEffectList, CurrEffect, DateTime, SoundVolume, Vibration, DeviceID)" +
    //                " VALUES (0, 0, 524288, 1, 1, 1, 1, 524288, 1, 0, 1, 1, 0)";
    //            dbCmd.CommandText = sqlQuery;

    //            using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
    //            {
    //                reader.Close();
    //                dbCmd.Dispose();
    //                dbConnection.Close();
    //            }
    //        }
    //    }
    //}

    //// 속성값 수정
    //public void UpdateItemTable(int _gold, int _bestScore, int _openSkinList, int _currSkin, int _upPowerLevel, int _forwardPowerLevel, int _offlineGoldLevel, int _openEffectList, int _currEffect)
    //{
    //    string connectionString = "URI=file:" + Filepath;

    //    //ItemList.Clear();

    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();
    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
    //        {
    //            string sqlQuery = "UPDATE " + Test_DB_Table.DatabaseTable.ToString() +
    //                " SET Gold='" + _gold + "', BestScore='" + _bestScore + "', OpenSkinList='" + _openSkinList + "', CurrSkin='" + _currSkin + "', UpPowerLevel='" + _upPowerLevel + "', ForwardPowerLevel='" + _forwardPowerLevel + "',OfflineGoldLevel='" + _offlineGoldLevel + "', OpenEffectList='" + _openEffectList + "', CurrEffect='" + _currEffect + "'";

    //            dbCmd.CommandText = sqlQuery;

    //            using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
    //            {
    //                //while (reader.Read())
    //                //{
    //                //    // Debug.Log(reader.GetString(1));  //  타입명 . (몇 열에있는것을 부를것인가)
    //                //    ItemList.Add(new DataTable(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5)));
    //                //}

    //                reader.Close();
    //                dbCmd.Dispose();
    //                dbConnection.Close();
    //            }
    //        }
    //    }
    //    SelectItemTable();
    //}
    //// time값 수정
    //public void UpdateItemTable(string _dateTime)
    //{
    //    string connectionString = "URI=file:" + Filepath;

    //    //ItemList.Clear();

    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();
    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
    //        {
    //            string sqlQuery = "UPDATE " + Test_DB_Table.DatabaseTable.ToString() +
    //                " SET DateTime='" + _dateTime + "'";

    //            dbCmd.CommandText = sqlQuery;

    //            using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
    //            {
    //                //while (reader.Read())
    //                //{
    //                //    // Debug.Log(reader.GetString(1));  //  타입명 . (몇 열에있는것을 부를것인가)
    //                //    ItemList.Add(new DataTable(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5)));
    //                //}

    //                reader.Close();
    //                dbCmd.Dispose();
    //                dbConnection.Close();
    //            }
    //        }
    //    }
    //    SelectItemTable();
    //}
    //public void UpdateItemTable(string _soundVolume, int _vibration)
    //{
    //    string connectionString = "URI=file:" + Filepath;

    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();
    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
    //        {
    //            string sqlQuery = "UPDATE " + Test_DB_Table.DatabaseTable.ToString() +
    //                " SET SoundVolume='" + _soundVolume + "', Vibration='" + _vibration + "'";

    //            dbCmd.CommandText = sqlQuery;

    //            using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
    //            {
    //                reader.Close();
    //                dbCmd.Dispose();
    //                dbConnection.Close();
    //            }
    //        }
    //    }
    //    SelectItemTable();
    //}
    //public void UpdateItemTable_DeviceID(string _ID)
    //{
    //    string connectionString = "URI=file:" + Filepath;

    //    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
    //    {
    //        dbConnection.Open();
    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())  // EnterSqL에 명령 할 수 있다. 
    //        {
    //            string sqlQuery = "UPDATE " + Test_DB_Table.DatabaseTable.ToString() +
    //                " SET DeviceID='" + _ID + "'";

    //            dbCmd.CommandText = sqlQuery;

    //            using (IDataReader reader = dbCmd.ExecuteReader()) // 테이블에 있는 데이터들이 들어간다. 
    //            {
    //                reader.Close();
    //                dbCmd.Dispose();
    //                dbConnection.Close();
    //            }
    //        }
    //    }
    //    SelectItemTable();
    //}

    // TEST_CODE
    //public void test()
    //{
    //    SelectItemTable();
    //    //UpdateItemTable(0, 1, 2,1,1,1);
    //    SelectItemTable();


    //    //Skin 관련 진수변환 코드
    //    // 10진수 to 2진수
    //    int t = 8;
    //    List<int> test = new List<int>();
    //    for (int i = 0; t > 0; i++)
    //    {
    //        test.Add(t % 2);
    //        t = t / 2;
    //    }
    //    for (int i = 0; i < test.Count; i++)
    //    {
    //        Debug.Log(test[i]);
    //    }
    //    Debug.Log("----------------");

    //    // 2진수 to 10진수
    //    int re = 0;
    //    for (int i = 0; i < test.Count; i++)
    //    {
    //        re += test[i] * (int)Mathf.Pow(2, i);
    //    }
    //    Debug.Log(re);
    //}


}
