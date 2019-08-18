using System.Collections.Generic;
using SqlCipher4Unity3D;
using UnityEngine;
using System.Linq;
using System.IO;
#if !UNITY_EDITOR
#endif

public class DataService
{
    public readonly SQLiteConnection _connection;

    public DataService(string DatabaseName)
    {
#if UNITY_EDITOR
        string dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
            // check if file exists in Application.persistentDataPath
            string filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

            if (!File.Exists(filepath))
            {
                Debug.Log("Database not in Persistent path");
                // if it doesn't ->
                // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
                WWW loadDb =
     new WWW ("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName); // this is the path to your StreamingAssets in android
                while (!loadDb.isDone) { } // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                // then save to Application.persistentDataPath
                File.WriteAllBytes (filepath, loadDb.bytes);
#elif UNITY_IOS
                string loadDb =
     Application.dataPath + "/Raw/" + DatabaseName; // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy (loadDb, filepath);
#elif UNITY_WP8
                string loadDb =
     Application.dataPath + "/StreamingAssets/" + DatabaseName; // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy (loadDb, filepath);
    
#elif UNITY_WINRT
                string loadDb =
     Application.dataPath + "/StreamingAssets/" + DatabaseName; // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy (loadDb, filepath);
#elif UNITY_STANDALONE_OSX
                string loadDb =
     Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName; // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#else
                string loadDb =
     Application.dataPath + "/StreamingAssets/" + DatabaseName; // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#endif

                Debug.Log("Database written");
            }
            
            var dbPath = filepath;

#endif

        if (!File.Exists(dbPath))
        {
            _connection = new SQLiteConnection(dbPath, "Jelly123123");
            CreateDB();
        }
        else
            _connection = new SQLiteConnection(dbPath, "Jelly123123");

        Debug.Log("Final PATH: " + dbPath);
    }

    public void CreateDB()
    {
        _connection.DropTable<JellyDB>();
        _connection.CreateTable<JellyDB>();

        _connection.InsertAll(new[]
        {
                new JellyDB
                {
                    Id= 1,
                    gold =0,
                    bestScore =0,
                    openSkinList =524288,
                    currSkin =1,
                    upPowerLevel =1,
                    forwardPowerLevel=1,
                    offlineGoldLevel=1,
                    openEffectList =524288,
                    currEffect =1,
                    dateTime ="0",
                    soundVolume ="1",
                    vibration =1,
                    deviceID ="0"
                }
            });
    }

    public IEnumerable<JellyDB> GetJellyDB()
    {
        return _connection.Table<JellyDB>().ToList();
    }

}