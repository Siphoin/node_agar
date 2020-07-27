using UnityEngine;
using UnityEngine.UI;
public class UIScore : MonoBehaviour
{
    [SerializeField] Text text_scroll;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text_scroll.text = "";
        foreach (var v in NodeNetwork.PlayersList)
        {
            if (v.Value.name != "")
            {
                if (v.Value.score != "")
                {
text_scroll.text += v.Value.name + " (Score " + v.Value.score + ")" + "\n";
                }
 
            }
           
        }
    }
}
