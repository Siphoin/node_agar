using UnityEngine;

public class PlayerScaleController : MonoBehaviour
{
    int frame = 0;
    float scaleValue = 0.5f;
    private NodeView view;
    private Vector3 vecAdd = new Vector3(0.02f, 0.02f, 0.02f);
    // Use this for initialization
    void Start()
    {
        view = GetComponent<NodeView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.myObject())
        {
            if (frame > 0)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale + vecAdd, Time.deltaTime * 10);
                frame--;
            }
        }
    }

    public void Scaling ()
    {
        frame += 10;
    }
}
