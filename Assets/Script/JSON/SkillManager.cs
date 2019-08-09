using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class SkillManager : MonoSingleton<SkillManager>
{
    Dictionary<int, SkillInfo> Dic_SkillInfo = new Dictionary<int, SkillInfo>();

    public void LoadJson()
    {
        TextAsset Json = Resources.Load<TextAsset>("JSON/SKILL_INFO");

        JSONNode rootNode = JSON.Parse(Json.text);

        foreach (KeyValuePair<string, JSONNode> pair in rootNode["SKILL_INFO"] as JSONObject)
        {
            SkillInfo Info = new SkillInfo(pair.Key, pair.Value);
            Dic_SkillInfo.Add(int.Parse(Info.KEY), Info);
        }

    }

    public SkillInfo GetValue(int _key)
    {
        SkillInfo Info = null;
        Dic_SkillInfo.TryGetValue(_key, out Info);

        if (Info == null)
        {
            Debug.LogError("1. JSON 로드 확인, 2. JSON KEY 값 확인, 3. info클래스 MonoBehaviour 상속 확인");
            return null;
        }

        return Info;
    }
}
