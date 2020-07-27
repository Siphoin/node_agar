[System.Serializable]
public class Food 
{

    public NodePosition position = new NodePosition();
    public NodeScale scale = new NodeScale();
    public PlayerColor color = new PlayerColor();
    public string id = "";

    public Food(string ID, NodePosition pos, NodeScale s, PlayerColor c)
    {
        id = ID;
        position = pos;
        scale = s;
        color = c;
    }
}
