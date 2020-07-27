using UnityEngine;

public class FoodObject : MonoBehaviour
{
    Food foodData = null;
    NodeView view;
    // Use this for initialization
    void Start()
    {
        view = GetComponent<NodeView>();
    }

    public void SetFoodData (Food f)
    {
        foodData = f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<NodeView>().myObject())
            {
                    collision.gameObject.GetComponent<PlayerScaleController>().Scaling();

                view.GetSocket().Emit("destroyFood", new JSONObject(JsonUtility.ToJson(foodData)));
                int sc = int.Parse(NodeNetwork.LocalPlayer.score);
                sc += 1;
                NodeNetwork.LocalPlayer.score = sc.ToString();
                view.GetSocket().Emit("SetScorePlayer", new JSONObject(JsonUtility.ToJson(NodeNetwork.LocalPlayer)));

            }
            Destroy(gameObject);
        }
    }
}
