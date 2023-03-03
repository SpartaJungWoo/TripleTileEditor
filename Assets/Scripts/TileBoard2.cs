using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Security.Cryptography;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TileBoard2 : MonoBehaviour
{
    [Header("SaveData")]
    public Tile _tilePrefab;

    public bool[,] _tiles;
    //public List<bool[,]> _layers = new List<bool[,]>();
    public List<List<bool[,]>> _stages = new List<List<bool[,]>>();

    public int _width = 9;
    public int _height = 10;
    public int _stage = 200;
    public int _currentStage = 1;
    public int _layer = 31;
    public int _currentLayer = 15;
    public int _type = 3;
    public int _count = 0;
    public int _tileSize = 30;


    bool _onLayerChange = false;
    bool _onStageChange = false;

    [Header("InputFieldData")]
    public TMP_InputField _inputFieldStage;
    public TMP_InputField _inputFieldLayer;
    public TMP_InputField _inputFieldType;
    public TMP_InputField _inputFieldCount;

    [Header("EditableBoard")]
    public Transform[] _editableBoard;
    public GameObject[,] _editalbeTile;
    public List<GameObject[,]> _editableTiles = new List<GameObject[,]>();

    [Header("DisplayBoard")]
    public Transform _displayBoard;
    public GameObject[,] _displayTiles;
    public List<GameObject[,]> _displayLayers = new List<GameObject[,]>();


    private void Start()
    {
        InitEditableBoard();
        InitDisplayBoard();
        InitStage();
        print(_stages[1][0].Length);
    }

    void InitStage()
    {
        for (int stage = 0; stage < _stage; stage++)
        {
            List<bool[,]> _layers = new List<bool[,]>();
            for (int layer = 0; layer < _layer; layer++)
            {
                int width = _width;
                int height = _height;

                if (layer % 2 == 0)
                {
                    width--;
                }
                _layers.Add(new bool[width, height]);
            }
            _stages.Add(_layers);
                }
    }

    void InitEditableBoard()
    {
        for (int layer = 0; layer < _editableBoard.Length; layer++)
        {
            int width = _width;
            int height = _height;

            if (layer %2 == 0)
            {
                width--;
            }

            _editableTiles.Add(new GameObject[width, height]);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++) 
                {
                    GameObject _editableTile = Instantiate(_tilePrefab.gameObject, _editableBoard[layer]);
                    Vector3 _tilePos = new Vector3(x * _tileSize - 350 + ((15 - width) * 10), y * _tileSize - 130 + ((4 - height) * 10), 0);
                    _editableTile.transform.localPosition = _tilePos;
                    _editableTile.transform.localScale = Vector3.one;
                    _editableTile.name = $"EditableTile {x} {y}";

                    Tile _editableTileConponent = _editableTile.GetComponent<Tile>();
                    _editableTileConponent._x = x;
                    _editableTileConponent._y = y;

                    int _current_x = x;
                    int _current_y = y;

                    Toggle _toggle = _editableTile.GetComponent<Toggle>();
                    _toggle.interactable = true;
                    _toggle.onValueChanged.AddListener(delegate { UpdateTile(_toggle, _current_x, _current_y); });

                    _editableTiles[layer][x, y] = _editableTile;
                }
            }
        }
        _editableBoard[(_currentLayer + 1) % 2].gameObject.SetActive(false);
    }


    void InitDisplayBoard()
    {
        for (int layer = 0; layer < _layer; layer++)
        {
            int width = _width;
            int height = _height;
            float _layerHeight = -50;

            if (layer %2 == 0)
            {
                width--;
                _layerHeight = _layerHeight + 5;
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
                    _displayTile.transform.localPosition = displayTilePos - new Vector3(width * _tileSize * 0.25f, height * _tileSize * 0.25f, 0) + new Vector3(200, _layerHeight, 0);

                    _displayLayers[layer][x, y] = _displayTile;
                }
            }
        }
    }

    void UpdateTile(Toggle toggle, int x, int y)
    {
        if (_onStageChange)
        {
            _displayBoard.GetChild(_currentLayer).GetChild(x * _height + y).GetComponent<Toggle>().isOn = toggle.isOn;
            SetCount(toggle.isOn);
        }

        else
        {
            _stages[_currentStage][_currentLayer][x, y] = toggle.isOn;
            _displayBoard.GetChild(_currentLayer).GetChild(x * _height + y).GetComponent<Toggle>().isOn = toggle.isOn;
            SetCount(toggle.isOn);
        }
    }

    public void ShowStage()
    {
        _onStageChange = true;
        _currentLayer = 15;

        for (int layer = 0; layer < _layer; layer++)
        {
            int width = _width;
            int heght = _height;

            if (layer %2 == 0)
            {
                width--;
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heght; y++) 
                {
                    _editableTiles[layer%2][x, y].GetComponent<Toggle>().isOn = _stages[_currentStage][layer][x, y];
                    
                }
            }
        }
        _inputFieldStage.text = _currentStage.ToString();

        _onStageChange = false;
    }

    public void StageUp()
    {
        if (_currentStage >= _stage - 1) return;
        _currentStage++;
        ShowStage();
    }

    public void StageDown()
    {
        if (_currentStage <= 0) return;
        _currentStage--;
        ShowStage();
    }



    public void ShowLayer()
    {
        _onLayerChange = true;
        int width = _width;
        int height = _height;

        if (_currentLayer % 2 == 0)
        {
            width--;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _editableTiles[_currentLayer % 2][x, y].GetComponent<Toggle>().isOn = _stages[_currentStage][_currentLayer][x, y];

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
        if (_currentLayer >= _layer - 1) return;
        _currentLayer++;
        ShowLayer();
    }

    public void LayerDown()
    {
        if (_currentLayer <= 0) return;
        _currentLayer--;
        ShowLayer();
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
                    _stages[_currentStage][layer][x, y] = false;
                    _displayLayers[layer][x, y].GetComponent<Toggle>().isOn = false;
                    _editableTiles[layer % 2][x, y].GetComponent<Toggle>().isOn = false;
                }
            }
        }
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

}

