using UnityEngine;
using UnityEngine.UI;
public class NodeLobby : MonoBehaviour
{
    [SerializeField] InputField nickname;
    [SerializeField] Text text_state_server;
    [SerializeField] NodeNetwork nodeNetwork;
    [SerializeField] Button button_join;
    // Use this for initialization
    void Start()
    {

    }


    public void Join ()
    {
        nickname.text = nickname.text.Trim();
        if (nickname.text != "")
        {
            NodeNetwork.Lobby = this;
            NodeNetwork.PlayerName = nickname.text;
            nodeNetwork.SendNickName();
            nodeNetwork.CreateObjectPlayer();
           
        }
    }

    private void Update()
    {
        button_join.interactable = (NodeNetwork.CountPlayers > -1) ? true : false;
        if (NodeNetwork.CountPlayers < 0)
        {
            text_state_server.text = "Connect to server...";
            
        }

        else
        {
            text_state_server.text = "Players: " + NodeNetwork.CountPlayers;
        }
    }
}
