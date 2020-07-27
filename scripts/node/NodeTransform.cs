using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class NodeTransform : MonoBehaviour
{
    [NodeElementGUI]
    private Vector3 old_pos;

    private NodeView view;
    private Player player;

    private float still;

    // Use this for initialization
    void Start()
    {
        view = GetComponent<NodeView>();
        old_pos = transform.position;
        player = new Player();
        player.position = new NodePosition();
        player.position.x = "0";
        player.position.y = "0";
        player.scale = new NodeScale();
        player.scale.x = "0";
        player.scale.y = "0";
        player.scale.z = "0";
        player.id = view.Get_id();

        if (!view.myObject())
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.myObject())
        {
            if (old_pos != transform.position)
            {
                old_pos = transform.position;
                still = 0;
                SerializeTransform();
            }

            else
            {
                still += Time.deltaTime;
                if (still >= 1)
                {
                    still = 0;
                    SerializeTransform();
                }
            }
        }
    }

    void SerializeTransform ()
    {
        player.position.x = transform.position.x.ToString();
        player.position.y = transform.position.y.ToString();

        // scale
        player.scale.x = transform.localScale.x.ToString();
        player.scale.y = transform.localScale.y.ToString();
        player.scale.z = transform.localScale.z.ToString();
        view.GetSocket().Emit("updatePosition", new JSONObject(JsonUtility.ToJson(player)));
    }
}
