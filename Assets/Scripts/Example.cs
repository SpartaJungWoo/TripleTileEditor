using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ExampleStageData
{
    public List<bool[,]> stages;
    public bool[,] stage;
    public bool st;

}

public class Example : MonoBehaviour
{
    private void Start()
    {
        // Create a new instance of the StageData class
        ExampleStageData stageData = new ExampleStageData();

        // Create a list of bool[,] arrays
        List<bool[,]> stages = new List<bool[,]>();
        stages.Add(new bool[,] { { true, false }, { false, true } });
        stages.Add(new bool[,] { { false, true }, { true, false } });

        stageData.stage = stages[1];
        stageData.st = stageData.stage[0,0];


        // Assign the list of stages to the StageData object

        // Convert the StageData object to a JSON string
        //string json = JsonUtility.ToJson(stageData);
        string json = JsonConvert.SerializeObject(stageData);

        // Output the JSON string to the console
        Debug.Log(json);
    }
}
