using UnityEngine;
using UnityEngine.UI;
public class Chat : MonoBehaviour
{
    [SerializeField] NodeNetwork nodeNetwork;

    [SerializeField] InputField inputField;

    [SerializeField] Text textChat;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        textChat.text = "";
        foreach (var v in NodeNetwork.Messages)
        {
            textChat.text += v.Value.dataMessage + "\n";

        }

        if (inputField.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Send();
            }
        }
    }

    public void Send ()
    {
        inputField.text = inputField.text.Trim();
        if (inputField.text != "")
        {
            nodeNetwork.Emit("SendMessage", new JSONObject(JsonUtility.ToJson(new Message(NodeNetwork.PlayerName + ": " + inputField.text, ""))));
            inputField.text = "";
        }
    }
}
