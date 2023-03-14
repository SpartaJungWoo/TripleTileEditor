using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;


[Serializable]
public class JsonStageData
{
    public List<bool[,]> stages;
    public int width;
    public int height;
    public int stageIndex;
    public int layer;
    public int type;
    public int count;
}


public class JSonStageData : MonoBehaviour
{
    string path;
    public TileBoard _tileBoard;
    public JsonStageData[] stages;


    // Initialize stage data
    public void InitStageData()
    {
        int numStage = _tileBoard._stage;
        stages = new JsonStageData[numStage];

        for (int i = 0; i < numStage; i++)
        {
            JsonStageData stageData = new JsonStageData();
            stageData.stages = _tileBoard._stages[i];
            stageData.width = _tileBoard._width;
            stageData.height = _tileBoard._height;
            stageData.layer = _tileBoard._layer;
            stageData.type = _tileBoard._type;
            stageData.count = _tileBoard._count;
            stageData.stageIndex = i;

            stages[i] = stageData;
            SaveStageData(i, stageData);
        }
    }



    public void AddStageData(int stageIndex, JsonStageData stageData)
    {
        stages[stageIndex] = stageData;
    }

    public void RemoveStageData(int stageIndex)
    {
        stages[stageIndex] = null;
    }

    public JsonStageData GetStageData(int stageIndex)
    {
        return stages[stageIndex];
    }


    // Save
    public void SaveStageData(int stageIndex, JsonStageData stageData)
    {
        string json = JsonConvert.SerializeObject(stageData, Formatting.Indented);
        //string json = JsonUtility.ToJson(stageData, true);
        string path = Application.dataPath + "/" + stageIndex + ".json";
        Debug.Log(json);
        File.WriteAllText(path, json);
    }


    // Load
    public JsonStageData LoadStageData(int stageIndex)
    {
        string path = Application.dataPath + "/" + stageIndex + ".json";
        string json = File.ReadAllText(path);
        JsonStageData stageData = JsonConvert.DeserializeObject<JsonStageData>(json);
        return stageData;
    }
}

