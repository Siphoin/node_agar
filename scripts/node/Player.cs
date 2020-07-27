using System;
using UnityEngine;
[Serializable]
  public  class Player
    {
    public string name = "";
    public NodePosition position = new NodePosition();
    public NodeScale scale = new NodeScale();
    public PlayerColor color = new PlayerColor();
    public string id = "";
    public string score = "0";


    public Player()
    {

    }

    public Player (string ID, string Name, NodePosition pos, NodeScale s)
    {
        id = ID;
        name = Name;
        position = pos;
        scale = s;
    }

    public void AddScore (int v)
    {
        score = v.ToString();
    }
    }
