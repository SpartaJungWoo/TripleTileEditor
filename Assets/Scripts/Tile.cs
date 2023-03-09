 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int _x;
    public int _y;
    public int _z;

    public int _type;

    public int _boardSize = 0;
    public bool _playingGame = false;

    public List<Sprite> _typeImage;

    public List<bool> _topTile = new List<bool>();

    public List<bool[,]> _stages = new List<bool[,]>();

    private void Start()
    {
        _stages = StageData.stage;

        //if (_playingGame)
        //{
        //    if (_boardSize == 90)
        //    {
        //        if (_y > 0)
        //        {
        //            _topTile.Add(_stages[_z][_x, _y - 1]);
        //            _topTile.Add(_stages[_z][_x + 1, _y - 1]);
        //        }
        //        if (_y < 9)
        //        {
        //            _topTile.Add(_stages[_z][_x, _y]);
        //            _topTile.Add(_stages[_z][_x + 1, _y]);
        //        }
        //    }

        //    else
        //    {
        //        if (_x > 0)
        //        {
        //            _topTile.Add(_stages[_z][_x - 1, _y]);
        //            _topTile.Add(_stages[_z][_x - 1, _y + 1]);
        //        }
        //        _topTile.Add(_stages[_z][_x, _y]);
        //        _topTile.Add(_stages[_z][_x, _y + 1]);
        //    }
        //}
    }

    public void SetType(int Type)
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameObject.Find("StageCreater").GetComponent<StageCreater>()._typeImage[Type];
    }


    public void TopTile()
    {
        

        
    }
}
