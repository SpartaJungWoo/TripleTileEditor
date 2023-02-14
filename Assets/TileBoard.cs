using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TileBoard : MonoBehaviour
{
    public bool[,] _tiles;
    public List<bool[,]> _boards = new List<bool[,]>();
    public Tile _tilePrefab;
    public int _stage;
    public int _width;
    public int _height;
    public int _layer;
    public int _showLayer = 0;
    public float _tileSize = 25f;
    public Transform _editableBoard;
    public Transform _displayBoard;

    public GameObject[,] _displayBoards;
    public List<GameObject[,]> _displayLayer = new List<GameObject[,]>();

    public GameObject[,] _boardTile;

    StageControl _stageControl;

    public TMP_InputField _inputLayer;

    private void Start()
    {

        if (GameObject.Find("StageControl") != null)
        {
            _stageControl = GameObject.Find("StageControl").GetComponent<StageControl>();
            _stage = _stageControl._stage;
            _width = _stageControl._width;
            _height = _stageControl._height;
            _layer = _stageControl._layer;
        }

        _inputLayer.onValueChanged.AddListener(delegate{
            print(_inputLayer.text);
            if (_layer <= int.Parse(_inputLayer.text) && _inputLayer.text == "") return;
            _showLayer = int.Parse(_inputLayer.text);
            ShowLayer(); });
        CreateDisplayBoard();
        InitBoard();
        CreateBoard();
    }


    void CreateBoard()
    {
        for (int layer = 0; layer < _layer; layer++) 
        {
            _boards.Add(_tiles);
            _boards[layer] = new bool[_width, _height];
        }
    }

    void ShowLayer()
    {
        for (int x= 0; x<_width; x++)
        {
            for (int y=0; y<_height; y ++)
            {
                _boardTile[x, y].GetComponent<Toggle>().isOn = _boards[_showLayer][x, y];
            }
        }
        _inputLayer.text = (_showLayer).ToString();
    }

    public void LayerUp()
    {
        if (_showLayer >= _layer-1) return;
        _showLayer++;
        ShowLayer();
    }

    public void LayerDown()
    {
        if (_showLayer <= 0) return;
        _showLayer--;
        ShowLayer();
    }


    void DeleteLayer()
    {

    }

    void InitBoard()
    {
        _boardTile = new GameObject[_width, _height];
        for (int x = 0; x < _width; x++) 
        {
            for (int y = 0; y < _height; y++)
            {
                GameObject _editableTile = Instantiate(_tilePrefab.gameObject);
                _editableTile.transform.SetParent(_editableBoard);
                _editableTile.transform.localPosition = new Vector3(x * _tileSize - 350 + ((10 - _width) * 10), y * _tileSize - 130 + ((10 - _height) * 10), 0);
                _editableTile.transform.localScale = Vector3.one;
                _editableTile.transform.name = "EditableTile " + x + " " + y;

                _editableTile.GetComponent<Tile>()._x = x;
                _editableTile.GetComponent<Tile>()._y = y;

                _boardTile[x, y] = _editableTile;

                int this_x = x;
                int this_y = y;

                Toggle toggle = _editableTile.GetComponent<Toggle>();
                toggle.onValueChanged.AddListener(delegate { UpdateTile(toggle, this_x, this_y); });
            }
        }
    }


    void CreateDisplayBoard()
    {
        for (int layer  = 0; layer < _layer; layer++)
        {
           _displayBoards = new GameObject[_width, _height];
            for (int x=0; x<_width; x++)
            {
                for (int y=0; y<_height; y++)
                {
                    GameObject _displayTile = Instantiate(_tilePrefab.gameObject);
                    _displayTile.transform.SetParent(_displayBoard.GetChild(layer));
                    _displayTile.transform.localScale = Vector3.one;
                    _displayTile.transform.localPosition = new Vector3(x * _tileSize + 100, y * _tileSize - 130 + ((10 - _height) * 10), 0);
                    _displayTile.GetComponent<Toggle>().interactable = false;
                    _displayTile.transform.name = "DisplayTile " + x + " " + y;

                    if (layer % 2 == 0){
                        _displayTile.transform.localPosition = new Vector3(x * _tileSize + 100+13, y * _tileSize - 130 + ((10 - _height) * 10)+13, 0);
                    }
                    _displayTile.GetComponent<Tile>()._x = x;
                    _displayTile.GetComponent<Tile>()._y = y;

                    _displayBoards[x, y] = _displayTile;
                    _displayLayer.Add(_displayBoards);
                }
            }
        }
    }

    void UpdateTile(Toggle toggle, int x, int y)
    {
        _boards[_showLayer][x, y] = toggle.isOn;
        _displayBoard.GetChild(_showLayer).GetChild(x*_height + y).GetComponent<Toggle>().isOn = toggle.isOn;
    }



    public void ClickBack()
    {
        SceneManager.LoadScene("StageScene");
    }
};
