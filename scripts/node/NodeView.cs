using UnityEngine;
using System.Collections;
using SocketIO;
public class NodeView : MonoBehaviour
{
    [Header("View Data")]
    [NodeElementGUI]
    [SerializeField] string id;
    [NodeElementGUI]
    [SerializeField] string ownerName;
    [NodeElementGUI]
    [SerializeField] bool isMyObject;


    private SocketIOComponent socket;

    public string OwnerName { get => ownerName; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        isMyObject = false;
    }

 public   void INIID (string ID)
    {
        id = ID;
        isMyObject = (NodeNetwork.IdClient == ID) ? true : false;
    }

    public void ININame(string NAME)
    {
        ownerName = NAME;
    }

    public void SetSocketData (SocketIOComponent Socket)
    {
        socket = Socket;
    }

    public string Get_id()
    {
        return id;
    }

    public SocketIOComponent GetSocket ()
    {
        return socket;
    }

    public bool myObject ()
    {
        return isMyObject;
    }
}
