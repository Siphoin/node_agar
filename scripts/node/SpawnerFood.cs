using UnityEngine;

public class SpawnerFood : MonoBehaviour
{
    NodeView view;
    // Use this for initialization
    void Start()
    {
        view = GetComponent<NodeView>();
        if (view.myObject())
        {
SpawnFood();
        InvokeRepeating("SpawnFood", 3, 3);
        }
        
    }

    void SpawnFood ()
    {
        for (int i = 0; i < Random.Range(1, 28); i++)
        {
           PlayerColor color = new PlayerColor(Random.Range(100, 256).ToString(), Random.Range(100, 256).ToString(), Random.Range(100, 256).ToString());
            NodeScale scale = new NodeScale();
            string scaleArgument = "1.99";
            scale.x = scaleArgument;
            scale.y = scaleArgument;
            scale.z = scaleArgument;
            float rad = 5f;;
            Vector3 vecUnity = new Vector3(transform.position.x + rad * Mathf.Cos(Random.Range(-180, 180)) + Random.Range(-5.1f, 5.1f), transform.position.y + rad * Mathf.Sin(Random.Range(-250, 250)) + Random.Range(-5.1f, 5.1f), 1);
            NodePosition posNode = new NodePosition();
            posNode.x = vecUnity.x.ToString();
            posNode.y = vecUnity.y.ToString();
            Food newFood = new Food("", posNode, scale, color);
            view.GetSocket().Emit("spawnFood", new JSONObject(JsonUtility.ToJson(newFood)));
        }
    }
}
