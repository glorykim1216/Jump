using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class SkillInfo
{
    string strKey = string.Empty;
    string gold = string.Empty;

    public string KEY { get { return strKey; } }
    public string GOLD { get { return gold; } }

    public SkillInfo(string _strKey, JSONNode _nodeData)
    {
        strKey = _strKey;
        gold = _nodeData["gold"];
    }
}
