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

    public bool _thisTopTile = false;

    public int _boardSize = 0;
    public bool _playingGame = false;

    public List<Sprite> _typeImage;

    public List<bool> _topTile;




    public void SetType(int Type)
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameObject.Find("StageCreater").GetComponent<StageCreater>()._typeImage[Type];
    }



    public void TopTile()
    {
        bool ThisTopTile = false;
        for (int toptileCount=0; toptileCount < _topTile.Count; toptileCount++)
        {
            ThisTopTile = ThisTopTile | _topTile[toptileCount];
            print("Name : " + this.name + "\n Top!! " + _topTile.Count + "\n OntopTile" + _topTile[toptileCount] + "\nthisTopTile " + ThisTopTile) ;
        }
        _thisTopTile = ThisTopTile;

        transform.GetChild(1).gameObject.SetActive(ThisTopTile);
        GetComponent<Toggle>().interactable = !ThisTopTile;

    }

    public void InitTopTile(List<bool[,]> stage)
    {
        _topTile = new List<bool>();

        int _zplus = _z + 1;
        if (_playingGame)
        {
            if (_boardSize == 90)
            {
                Debug.Log(_x + " " + _y);
                switch (_x)
                {
                    case 0:
                        switch (_y)
                        {
                            case 0:
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                            default:
                                _topTile.Add(stage[_zplus][_x, _y - 1]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                        }
                    case 1:
                        switch (_y)
                        {
                            case 0:
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                            default:
                                _topTile.Add(stage[_zplus][_x - 1, _y - 1]);
                                _topTile.Add(stage[_zplus][_x, _y - 1]);
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                        }

                    case 2:
                        switch (_y)
                        {
                            case 0:
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                            default:
                                _topTile.Add(stage[_zplus][_x - 1, _y - 1]);
                                _topTile.Add(stage[_zplus][_x, _y - 1]);
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                        }

                    case 3:
                        switch (_y)
                        {
                            case 0:
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                            default:
                                _topTile.Add(stage[_zplus][_x - 1, _y - 1]);
                                _topTile.Add(stage[_zplus][_x, _y - 1]);
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                        }

                    case 4:
                        switch (_y)
                        {
                            case 0:
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                            default:
                                _topTile.Add(stage[_zplus][_x - 1, _y - 1]);
                                _topTile.Add(stage[_zplus][_x, _y - 1]);
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                        }

                    case 5:
                        switch (_y)
                        {
                            case 0:
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                            default:
                                _topTile.Add(stage[_zplus][_x - 1, _y - 1]);
                                _topTile.Add(stage[_zplus][_x, _y - 1]);
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                        }

                    case 6:
                        switch (_y)
                        {
                            case 0:
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                            default:
                                _topTile.Add(stage[_zplus][_x - 1, _y - 1]);
                                _topTile.Add(stage[_zplus][_x, _y - 1]);
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                        }

                    case 7:
                        switch (_y)
                        {
                            case 0:
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                            default:
                                _topTile.Add(stage[_zplus][_x - 1, _y - 1]);
                                _topTile.Add(stage[_zplus][_x, _y - 1]);
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                _topTile.Add(stage[_zplus][_x, _y]);
                                return;
                        }

                    case 8:
                        switch (_y)
                        {
                            case 0:
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                return;
                            default:
                                _topTile.Add(stage[_zplus][_x - 1, _y - 1]);
                                _topTile.Add(stage[_zplus][_x - 1, _y]);
                                return;
                        }

                    default:
                        _topTile.Add(stage[_zplus][_x-1, _y-1]);
                        _topTile.Add(stage[_zplus][_x, _y-1]);
                        _topTile.Add(stage[_zplus][_x-1, _y]);
                        _topTile.Add(stage[_zplus][_x, _y]);
                        return;
                }
            }

            else
            {
                print(_x + " " + _y);

                switch (_y)
                {
                    case 9:
                        _topTile.Add(stage[_zplus][_x, _y]);
                        _topTile.Add(stage[_zplus][_x + 1, _y]);
                        return;

                    
                    default:

                        _topTile.Add(stage[_zplus][_x, _y]);
                        _topTile.Add(stage[_zplus][_x, _y+1]);
                        _topTile.Add(stage[_zplus][_x+1, _y]);
                        _topTile.Add(stage[_zplus][_x+1, _y+1]);
                        return;

                }
            }
        }
        }
    }