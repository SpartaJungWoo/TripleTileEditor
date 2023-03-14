using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public TileBoard tileBoard;
    public JSonStageData jSonStageData;
    public AWS_S3 AWS_S3;


    private void OnJsonInit()
    {
        if (Application.dataPath + "/" + 1 + ".json" == null)
        {
            jSonStageData.InitStageData();
        }
    }

    void Start()
    {
        tileBoard.StartBoard();
        OnJsonInit();
        tileBoard.LoadStageData();
        tileBoard.ShowStage();
    }


    public void Save()
    {
        jSonStageData.SaveStageData(tileBoard._currentStage, tileBoard.CurrentStageData());
        AWS_S3.PutObject(tileBoard._currentStage);
    }


    public void Play()
    {
        tileBoard.StageDataSave();
        SceneManager.LoadScene("GameplayScene");
    }
}
