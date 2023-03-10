using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class StageCreater : MonoBehaviour
{
    public Transform _displayBoard;
    public Tile _tilePrefab;
    int _tileSize = 65;
    JsonStageData jsonStageData;
    List<Tile> tiles = new List<Tile>();

    List<Tile> _clickTile = new List<Tile>(); 
    public List<GameObject> _clickTileSlot = new List<GameObject>();
    int clickTileCount = 0;
    int tileCount;

    bool _initBoard = false;

    public List<Sprite> _typeImage;

    private void Start()
    {
        jsonStageData = new JsonStageData()
        {
            stages = StageData.stage,
            width = StageData.width,
            height = StageData.height,
            stageIndex = StageData.stageIndex,
            layer = StageData.layer,
            type = StageData.type,
            count = StageData.count
        };
        
        InitDisplayBoard(jsonStageData);
    }

    void InitDisplayBoard(JsonStageData JsonstageData)
    {
        _initBoard = true;
        for (int layer = 0; layer < JsonstageData.layer; layer++)
        {
            int width = JsonstageData.width;
            int height = JsonstageData.height;
            float _layerHeight = -50;
            float _tileScale = 1f;

            if (layer % 2 == 0)
            {
                width--;
                _layerHeight = _layerHeight + 20f;
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (JsonstageData.stages[layer][x, y])
                    {
                        GameObject _displayTile = Instantiate(_tilePrefab.gameObject, _displayBoard.GetChild(layer));
                        _displayTile.GetComponent<Toggle>().interactable = false;
                        _displayTile.transform.name = $"DisplayTile {layer} {x} {y}";

                         Tile displayTileCompo = _displayTile.GetComponent<Tile>();

                        displayTileCompo._x = x;
                        displayTileCompo._y = y;
                        displayTileCompo._z = layer;
                        displayTileCompo._boardSize = width * height;
                        displayTileCompo._playingGame = true;

                        tileCount++;

                        Toggle _toggle = _displayTile.GetComponent<Toggle>();
                        _toggle.interactable = true;
                        _toggle.onValueChanged.AddListener(delegate { UpdateTile(_toggle); });


                        tiles.Add(_displayTile.GetComponent<Tile>());

                        Vector3 displayTilePos = new Vector3((x * _tileSize) * _tileScale, (y * _tileSize + ((height) * 10)) * _tileScale, layer);
                        _displayTile.transform.localPosition = displayTilePos - new Vector3(width * _tileSize * (_tileScale/2), height * _tileSize * (_tileScale / 2), 0) + new Vector3(0, _layerHeight, 0);

                        _displayTile.GetComponent<Toggle>().isOn = JsonstageData.stages[layer][x, y];
                    }
                }
            }
        }
        ShuffleType();
        _initBoard = false;
    }

    void ShuffleType()
    {
        int numTypes = StageData.type;

        int type = 0;
        for (int i=0; i<tileCount / 3; i++)
        {
            if (type >= numTypes){ type = 0; }
            for (int r = 0; r < 3; r++)
            {
                int rand = Random.Range(0, tiles.Count);
                tiles[rand]._type = type;
                tiles[rand].SetType(type);
                tiles.RemoveAt(rand);
            }
            type++; 
        }
    }

    public void Home()
    {
        SceneManager.LoadScene("BoardScene");
    }

    public void UpdateTile(Toggle _toggle)
    {
        if (!_initBoard)
        {
            print("Update! " + clickTileCount);
            _clickTile.Add(_toggle.gameObject.GetComponent<Tile>());
            _clickTileSlot[clickTileCount].transform.GetChild(0).gameObject.SetActive(true);
            _toggle.transform.parent = _clickTileSlot[clickTileCount].transform;
            _toggle.transform.localPosition = Vector3.zero;
            _toggle.interactable = false;
            clickTileCount++;

            TileSort(_toggle.GetComponent<Tile>());
        }
    }



    public void TileSort(Tile tile)
    {
        Tile[] DestroyTile = new Tile[3];
        int clickTileCountTemp = _clickTile.Count-1;

        for (int clickTileCountNum = clickTileCountTemp- 1; clickTileCountNum >= 0; clickTileCountNum--)
        {
            if (_clickTile[clickTileCountNum]._type == tile._type)
            {
                for (int swapCount = clickTileCountNum; swapCount < clickTileCountTemp; swapCount++)
                {
                    Tile temp;
                    temp = _clickTile[swapCount];
                    _clickTile[swapCount] = _clickTile[clickTileCountTemp];
                    _clickTile[clickTileCountTemp] = temp;
                }
                break;
            }
        }

        //_clickTile.Sort((a, b) => a._type.CompareTo(b._type));


        for (int i = 0; i < _clickTile.Count; i++)
        {
            _clickTile[i].transform.parent = _clickTileSlot[i].transform;
            _clickTile[i].transform.localPosition = Vector3.zero;
        }


        int[] Type = new int[StageData.type];
        List<Tile[]> tileType = new List<Tile[]>();


        for (int i=0; i < StageData.type; i++)
        {
            tileType.Add(new Tile[3]);
        }


        for (int i=0; i< _clickTile.Count; i++)
        {
            Type[_clickTile[i]._type]++;

            tileType[_clickTile[i]._type][Type[_clickTile[i]._type]-1] = _clickTile[i];
            if (Type[_clickTile[i]._type] >= 3)
            {
                for (int j=0; j<3; j++)
                {
                    DestroyTile[j] = tileType[_clickTile[i]._type][j];
                }
                Type[_clickTile[i]._type] = 0;


                for (int destroyCount = 0; destroyCount < 3; destroyCount++)
                {
                    _clickTile.Remove(DestroyTile[destroyCount]);
                    Destroy(DestroyTile[destroyCount].gameObject);
                    _clickTileSlot[clickTileCountTemp-destroyCount].transform.GetChild(0).gameObject.SetActive(false);
                }

                clickTileCount = clickTileCount - 3;
            }
        }


        for (int i = 0; i < _clickTile.Count; i++)
        {
            _clickTile[i].transform.parent = _clickTileSlot[i].transform;
            _clickTile[i].transform.localPosition = Vector3.zero;
        }
        return;
    }

}
