using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StageControl : MonoBehaviour
{
    public static StageControl _stageControl;
    public int _stage = 1;
    public int _width = 10;
    public int _height = 10;
    public int _layer = 7;

    public TMP_InputField _inputStage;
    public TMP_InputField _inputWidth;
    public TMP_InputField _inputHeight;
    public TMP_InputField _inputLayer;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (_stageControl == null) _stageControl = this;
        else if (GameObject.Find(name).gameObject != null) {
            Destroy(GameObject.Find(name).gameObject);
        }
    }

    private void Start()
    {
        _inputStage.onValueChanged.AddListener(delegate { _stage = int.Parse(_inputStage.text); });
        _inputWidth.onValueChanged.AddListener(delegate{
            if (int.Parse(_inputWidth.text) > 12)
            {
                _inputWidth.text = _width.ToString();
                return;
            }
            _width = int.Parse(_inputWidth.text); });
        _inputHeight.onValueChanged.AddListener(delegate {
            if (int.Parse(_inputHeight.text) > 12)
            {
                _inputHeight.text = _height.ToString();
                return;
            }
            _height = int.Parse(_inputHeight.text); });
        _inputLayer.onValueChanged.AddListener(delegate { _layer = int.Parse(_inputLayer.text); });
    }


    public void ClickCreate()
    {
        SceneManager.LoadScene("BoardScene");
    }
}
