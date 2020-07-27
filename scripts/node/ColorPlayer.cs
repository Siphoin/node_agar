using UnityEngine;

public class ColorPlayer : MonoBehaviour
{
    private float still;
    [SerializeField] SpriteRenderer colorObject;
    [NodeElementGUI]
    NodeView view;
    private Player player;
    private PlayerColor old_color;
    // Use this for initialization
    void Start()
    {
        view = GetComponent<NodeView>();
        player = new Player();
        old_color = new PlayerColor();
        player.id = view.Get_id();
        if (view.myObject())
        {
            player.color = new PlayerColor(Random.Range(100, 256).ToString(), Random.Range(100, 256).ToString(), Random.Range(100, 256).ToString());
            colorObject.color = player.color.GetColor();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(view.myObject())
        {
            if (old_color != player.color)
            {
                old_color = player.color;
                still = 0;
                SerializeColor();
            }

            else
            {
                still += Time.deltaTime;
                if (still >= 1)
                {
                    still = 0;
                    SerializeColor();
                }
            }
        }
    }

    void SerializeColor ()
    {
        view.GetSocket().Emit("SetColorPlayer", new JSONObject(JsonUtility.ToJson(player)));
    }
}
