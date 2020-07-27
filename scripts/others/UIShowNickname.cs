using UnityEngine;
using UnityEngine.UI;
public class UIShowNickname : MonoBehaviour
{
    [SerializeField] Text text;
    NodeView view;
    // Use this for initialization
    void Start()
    {
        view = GetComponent<NodeView>();
    }

    private void Update()
    {
        text.text = view.name;    
    }

}
