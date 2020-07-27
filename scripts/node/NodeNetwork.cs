using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Linq;
using System.Globalization;
using UnityEngine.UI;

public class NodeNetwork : SocketIOComponent
{
    private static string idClient = "";
    private static NodeLobby lobby;
    private static bool startConnect = false;
    Dictionary<string, NodeView> NetworkGameObjects = new Dictionary<string, NodeView>();
    static Dictionary<string, Message> messages = new Dictionary<string, Message>();
    static  SortedDictionary<string, Player> Players = new SortedDictionary<string, Player>();
    static int countPlayers = -1;

    [SerializeField] GameObject PrefabPlayer;
    [SerializeField] GameObject PrefabFood;
    [SerializeField] ScrollRect scrollRectChat;


    static Player localPlayer = new Player();

    static string playerName = "";

    public static string IdClient { get => idClient; }
    public static Player LocalPlayer { get => localPlayer; }
    public static NodeLobby Lobby { get => lobby; set => lobby = value; }
    public static string PlayerName { get => playerName; set => playerName = value; }
    public static bool StartConnect { get => startConnect; set => startConnect = value; }
    public static int CountPlayers { get => countPlayers; }
    public static SortedDictionary<string, Player> PlayersList { get => Players; }
    public static Dictionary<string, Message> Messages { get => messages; }

    private void Start()
    {
        base.Start();
        idClient = "";
        countPlayers = -1;
        localPlayer = new Player();
        messages.Clear();
        Players.Clear();
        NetworkGameObjects.Clear();
        ConnecToNode();
    }



    public  void ConnecToNode()
    {
        
        On("open", (E) =>
        {
            Debug.Log("Event 110");
            
        });

        On("GetID", (E) =>
        {
            Debug.Log("Event 115");
            idClient = E.data["id"].str;
            localPlayer = new Player(idClient, playerName, new NodePosition(), new NodeScale());
        });

        On("create", (E) =>
        {
            Debug.Log("Event 127");
            string id = E.data["id"].str;
            string xStringServer = E.data["position"]["x"].str;
            string yStringServer = E.data["position"]["y"].str;
            string namePlayer = E.data["name"].str;
            GameObject objNET = Instantiate(PrefabPlayer);
            NodeView view = objNET.GetComponent<NodeView>();
            view.INIID(id);
            view.ININame(namePlayer);
            view.SetSocketData(this);
            NetworkGameObjects.Add(id, view);
            objNET.name = namePlayer;
            Player playerNew = new Player(id, namePlayer, new NodePosition(), new NodeScale());
            Players.Add(id, playerNew);
            if (id == idClient)
            {
                lobby.gameObject.SetActive(false);
                if (Camera.main.transform.parent == null)
                {
                    Camera.main.transform.SetParent(objNET.transform);
                }
            }

        });

        On("SendMessage", (E) =>
        {
            string id = E.data["id"].str;
            string data = E.data["dataMessage"].str;
            messages.Add(id, new Message(data, id));
            scrollRectChat.verticalNormalizedPosition = 0;
            Debug.Log("Event 800");
        });


        On("spawnFood", (E) =>
        {
        //    Debug.Log("Event 46");
            string id = E.data["id"].str;
            GameObject objNET = Instantiate(PrefabFood);
            NodeView view = objNET.GetComponent<NodeView>();
            view.INIID(id);
            view.SetSocketData(this);
            string xStringServer = E.data["position"]["x"].str;
            string yStringServer = E.data["position"]["y"].str;
            string xStringServerScale = E.data["scale"]["x"].str;
            string yStringServerScale = E.data["scale"]["y"].str;
            string zStringServerScale = E.data["scale"]["z"].str;
            string rStringServer = E.data["color"]["r"].str;
            string gStringServer = E.data["color"]["g"].str;
            string bStringServer = E.data["color"]["b"].str;
            view.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new PlayerColor(rStringServer, gStringServer, bStringServer).GetColor();
            view.gameObject.transform.position = new Vector3(float.Parse(xStringServer), float.Parse(yStringServer));
         float   sx = NodeNormalizeData.NormalizeFloat(xStringServerScale);
            float sy = NodeNormalizeData.NormalizeFloat(yStringServerScale);
            float sz = NodeNormalizeData.NormalizeFloat(zStringServerScale);
            view.gameObject.transform.localScale = new Vector3(sx, sy, sz);
            FoodObject foodObject = objNET.GetComponent<FoodObject>();
            NodePosition posNode = new NodePosition();
            posNode.x = objNET.transform.position.x.ToString();
            posNode.y = objNET.transform.position.y.ToString();
            NodeScale scaleNode = new NodeScale();
            scaleNode.x = sx.ToString();
            scaleNode.y = sy.ToString();
            scaleNode.z = sz.ToString();
            Food d = new Food(id, posNode, scaleNode, new PlayerColor(rStringServer, gStringServer, bStringServer));
            foodObject.SetFoodData(d);
            NetworkGameObjects.Add(id, view);
        });

        On("destroy", (E) =>
        {
            Debug.Log("Event 130");
            string id = E.data["id"].str;
            if (id == idClient)
            {
                
                Close();
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
            GameObject obj = NetworkGameObjects[id].gameObject;
            Destroy(obj);
            NetworkGameObjects.Remove(id);
        });

        On("destroyFood", (E) =>
        {
         //   Debug.Log("Event 621");          
            string id = E.data["id"].str;
            GameObject obj = NetworkGameObjects[id].gameObject;
            Destroy(obj);
            NetworkGameObjects.Remove(id);
        });

        On("updatePosition", (E) =>
        {
          //  Debug.Log("Event 220");
            string id = E.data["id"].str;
            string xStringServer = E.data["position"]["x"].str;
            string yStringServer = E.data["position"]["y"].str;
            string xStringServerScale = E.data["scale"]["x"].str;
            string yStringServerScale = E.data["scale"]["y"].str;
            string zStringServerScale = E.data["scale"]["z"].str;
            float vx = NodeNormalizeData.NormalizeFloat(xStringServer);
            float vy = NodeNormalizeData.NormalizeFloat(yStringServer);
            float sx = NodeNormalizeData.NormalizeFloat(xStringServerScale);
            float sy = NodeNormalizeData.NormalizeFloat(yStringServerScale);
            float sz = NodeNormalizeData.NormalizeFloat(zStringServerScale);
            NodeView view = NetworkGameObjects[id];
            view.gameObject.transform.position = new Vector3(vx, vy);
            view.gameObject.transform.localScale = new Vector3(sx, sy, sz);
        });

        

        On("PlayerDisconnected", (E) =>
        {
            Debug.Log("Event 237");
            string id = E.data["id"].str;
            Players.Remove(id);
        });


        On("SetNickName", (E) =>
        {
            Debug.Log("Event 219");
            string id = E.data["id"].str;
            string name = E.data["name"].str;
            Players[id].name = name;


        });

        On("SetColorPlayer", (E) =>
        {
          
            string id = E.data["id"].str;
            string rStringServer = E.data["color"]["r"].str;
            string gStringServer = E.data["color"]["g"].str;
            string bStringServer = E.data["color"]["b"].str;
            NodeView view = NetworkGameObjects[id];
            view.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new PlayerColor(rStringServer, gStringServer, bStringServer).GetColor();
        });



        On("GetCountPlayersFromServer", (E) =>
        {
               Debug.Log("Event 710");
            string count = E.data["count"].str;
            countPlayers = int.Parse(count);
        });

        On("disconnect", (E) =>
        {
            string id = E.data["id"].ToString();
            if (id == idClient)
            {
                Debug.Log("you disconnected: Node.js message");
                foreach (var v in NetworkGameObjects)
                {
                    Destroy(v.Value);
                }
            }
        });

        On("SetScorePlayer", (E) =>
        {
                    
            string id = E.data["id"].str;
            string score = E.data["score"].str;
            Players[id].score = score;
        });
    }

    public void CreateObjectPlayer()
    {
        Emit("createEvent2", new JSONObject(JsonUtility.ToJson(localPlayer)));
    }

    public void SendNickName()
    {
        localPlayer.name = playerName;
        Emit("SetNickName", new JSONObject(JsonUtility.ToJson(localPlayer)));
    }

    

}
