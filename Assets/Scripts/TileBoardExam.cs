using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TileBoardExam : MonoBehaviour
{
    [Header("SaveData")]

    public Tile _tilePrefab;

    public bool[,] _tiles;
    public List<bool[,]> _layers = new List<bool[,]>();
    public List<List<bool[,]>> _stages = new List<List<bool[,]>>();

    public int _width = 10;
    public int _height = 10;
    public int _stage = 200;
    public int _currentStage = 1;
    public int _type = 3;
    public int _count = 0;
    public int _layer = 7;
    public int _currentLayer = 0;
    public float _tileSize = 25f;

    public bool _onStageChange = false;
    public bool _onLayerChange = false;

    [Header("InputFieldData")]
    
    public TMP_InputField _inputFieldStage;
    public TMP_InputField _inputFieldLayer;
    public TMP_InputField _inputFieldType;
    public TMP_InputField _inputFieldCount;

    [Header("EditableBoard")]

    public Transform[] _editableBoard;
    public List<GameObject[,]> _editableTiles = new List<GameObject[,]>();

    [Header("DisplayBoard")]

    public Transform _displayBoard;
    public List<GameObject[,]> _displayLayers = new List<GameObject[,]>();


    private void Start()
    {

        InitDisplayBoard();
        InitEditableBoard();
        CreateBoard();
        InitStage();
        
    }

    void InitStage()
    {
        for (int i=0; i<200; i++)
        {
            _stages.Add(_layers);
        }
    }

    void CreateBoard()
    {
        for (int layer = 0; layer < _layer; layer++) 
        {

            int width = _width;
            int height = _height;

            if (layer % 2 == 0)
            {
                width--;
            }

            _layers.Add(_tiles);
            _layers[layer] = new bool[width, height];
        }
    }

    void ShowLayer()
    {
        _onLayerChange = true;
        int width = _width;
        int height = _height;

        if (_currentLayer % 2 == 0)
        {
            width--;
        }

        for (int x= 0; x<width; x++)
        {
            for (int y=0; y<height; y ++)
            {
                _editableTiles[_currentLayer%2][x, y].GetComponent<Toggle>().isOn = _stages[_currentStage][_currentLayer][x, y];
            }
        }

        if (_currentLayer % 2 == 0)
        {
            _editableBoard[0].gameObject.SetActive(true);
            _editableBoard[1].gameObject.SetActive(false);
        }
        else
        {
            _editableBoard[0].gameObject.SetActive(false);
            _editableBoard[1].gameObject.SetActive(true);
        }
        _inputFieldLayer.text = (_currentLayer).ToString();
        _onLayerChange = false;
    }

    public void LayerUp()
    {
        if (_currentLayer >= _layer-1) return;
        _currentLayer++;
        ShowLayer();
    }

    public void LayerDown()
    {
        if (_currentLayer <= 0) return;
        _currentLayer--;
        ShowLayer();
    }



    public void StageUp()
    {
        if (_currentStage >= _stage - 1) return;
        _currentStage++;
        ShowStage();
    }

    public void StageDown()
    {
        if (_currentStage <= 1) return;
        _currentStage--;
        ShowStage();
    }


    public void ShowStage()
    {
        _onStageChange = true;
        _currentLayer = 15;

        for (int layer = 0; layer < _layer; layer++)
        {
            int width = _width;
            int height = _height;

            if (layer % 2 == 0)
            {
                width--;
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (_stages[_currentStage][layer][x, y])
                    {
                        _editableTiles[layer][x, y].GetComponent<Toggle>().isOn = _stages[_currentStage][layer][x, y];
                    }
                    //print("!!!" + _stages[_currentStage][layer][x, y]);
                    //_editableTiles[_currentLayer % 2][x, y].GetComponent<Toggle>().isOn = _stages[_currentStage][layer][x, y];
                }
            }
        }
        _inputFieldStage.text = _currentStage.ToString();

        ShowLayer();
        _onStageChange = false;

    }


    void InitEditableBoard()
    {
        for (int layer = 0; layer < _editableBoard.Length; layer++)
        {

            int width = _width;
            int height = _height;

            if (layer % 2 == 0)
            {
                width--;
            }

            _editableTiles.Add(new GameObject[width, height]);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GameObject _editableTile = Instantiate(_tilePrefab.gameObject, _editableBoard[layer]);
                    Vector3 tilePos = new Vector3(x * _tileSize - 350 + ((15 - width) * 10), y * _tileSize - 130 + ((4 - height) * 10), 0);
                    _editableTile.transform.localPosition = tilePos;
                    _editableTile.transform.localScale = Vector3.one;
                    _editableTile.name = $"EditableTile {x} {y}";

                    Tile _editableTileComponent = _editableTile.GetComponent<Tile>();
                    _editableTileComponent._x = x;
                    _editableTileComponent._y = y;

                    int _current_x = x;
                    int _current_y = y;

                    Toggle _toggle = _editableTile.GetComponent<Toggle>();
                    _toggle.interactable = true;
                    _toggle.onValueChanged.AddListener(delegate { UpdateTile(_toggle, _current_x, _current_y); });

                    _editableTiles[layer][x, y] = _editableTile;
                }
            }
        }
        _editableBoard[(_currentLayer+1)%2].gameObject.SetActive(false);
    }


    // DisplayBoard 생성 함수
    void InitDisplayBoard()
    {
        for (int layer = 0; layer < _layer; layer++)
        {
            int width = _width;
            int height = _height;
            float layerHeight = -50;


            if (layer % 2 == 0)
            {
                width--;
                layerHeight = layerHeight + 5;
            }

            GameObject[,] _displayTiles = new GameObject[width, height];
            _displayLayers.Add(_displayTiles);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GameObject _displayTile = Instantiate(_tilePrefab.gameObject, _displayBoard.GetChild(layer));
                    _displayTile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    _displayTile.GetComponent<Toggle>().interactable = false;
                    _displayTile.transform.name = $"DisplayTile {layer} {x} {y}";

                    Tile _displayTileComponent = _displayTile.GetComponent<Tile>();

                    _displayTile.GetComponent<Tile>()._x = x;
                    _displayTile.GetComponent<Tile>()._y = y;

                    Vector3 displayTilePos = new Vector3((x * _tileSize + 100) * 0.5f, (y * _tileSize - 130 + ((height) * 10)) * 0.5f, 0);
                    _displayTile.transform.localPosition = displayTilePos - new Vector3(width * _tileSize * 0.25f, height * _tileSize * 0.25f, 0) + new Vector3(200, layerHeight, 0);


                    _displayLayers[layer][x,y] = _displayTile;
                }
            }
        }
    }

    // EditableTile이 눌렸을 때 불려지는 함수
    void UpdateTile(Toggle toggle, int x, int y)
    {
        int width = _width;
        int height = _height;

        if (_currentLayer % 2 == 0)
        {
            width--;
        }


        //_layers[_currentLayer][x, y] = toggle.isOn;
        
        if (_onStageChange)
        {
        }
        else 
        {
            _stages[_currentStage][_currentLayer][x, y] = toggle.isOn;
            _displayBoard.GetChild(_currentLayer).GetChild(x * _height + y).GetComponent<Toggle>().isOn = toggle.isOn;
            SetCount(toggle.isOn);
        }
        Debug.Log($"@@ {_currentStage} {_currentLayer} {_stages[1][15][x, y]}");
    }

    void SetCount(bool isOn)
    {
        if (isOn)
        {
            _count++;
        }
        else
        {
            _count--;
        }

        if (!(_count % 3 == 0))
        {
            _inputFieldCount.image.color = Color.red;
        }
        else
        {
            _inputFieldCount.image.color = Color.white;
        }
        _inputFieldCount.text = _count.ToString();
    }

    public void LayerClear()
    {
        int width = _width;

        if (_currentLayer % 2 == 0)
        {
            width--;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _editableTiles[_currentLayer % 2][x, y].GetComponent<Toggle>().isOn = _stages[_currentStage][_currentLayer][x, y] = false;

            }
        }
    }

    public void LayerAllClear()
    {
        for (int layer =0; layer<_layer; layer++)
        {
            int width = _width;
            int height = _height;

            if (layer % 2 == 0)
            {
                width--;
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _stages[_currentStage][layer][x, y] = false;
                    _displayLayers[layer][x, y].GetComponent<Toggle>().isOn = false;
                    _editableTiles[layer%2][x, y].GetComponent<Toggle>().isOn = false;
                }
            }
        }
    }
}
