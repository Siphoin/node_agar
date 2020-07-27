using System;
using UnityEngine;
[Serializable]
 public   class PlayerColor
    {
  public  string r = "255";
    public string g = "255";
    public string b = "255";

    public Color32 GetColor ()
    {
        return new Color32(byte.Parse(r), byte.Parse(g), byte.Parse(b), 255);
    }

    public PlayerColor(string R, string G, string B)
    {
        r = R;
        g = G;
        b = B;
    }

    public PlayerColor()
    {

    }
}
