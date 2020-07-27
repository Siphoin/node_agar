using UnityEngine;

public class FoodOtherPlayers : MonoBehaviour
{
    NodeView view;
    PlayerScaleController scaleController;
    // Use this for initialization
    void Start()
    {
        view = GetComponent<NodeView>();
        scaleController = GetComponent<PlayerScaleController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<NodeView>().myObject() == false)
            {
                Vector3 scale_col = collision.transform.localScale;
                NodeView view2 = collision.GetComponent<NodeView>();
                if (transform.localScale.x > scale_col.x)
                {
                    if (transform.localScale.y > scale_col.y)
                    {
                        if (transform.localScale.z > scale_col.z)
                        {
                            view.GetSocket().Emit("FoodPlayer", new JSONObject(JsonUtility.ToJson(new Player(view2.Get_id(), view2.OwnerName, new NodePosition(), new NodeScale()))));
                            for (int i = 0; i < 5 + collision.gameObject.transform.position.x / 2; i++)
                            {
                                scaleController.Scaling();
                            }
                            int sc = int.Parse(NodeNetwork.LocalPlayer.score);
                            sc += 10;
                            NodeNetwork.LocalPlayer.score = sc.ToString();
                            
                            view.GetSocket().Emit("SetScorePlayer", new JSONObject(JsonUtility.ToJson(NodeNetwork.LocalPlayer)));
                            
                            Destroy(collision.gameObject);
                        }
                    }
                }
                
            }
        }
    }
}
