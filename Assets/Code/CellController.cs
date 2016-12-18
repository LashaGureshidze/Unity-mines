using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CellController : MonoBehaviour, IPointerClickHandler
{

    public GameObject tile;
    public GameObject flag;
    public GameObject mine1;
    public GameObject mine2;
    public GameObject mine3;
    public GameObject text;

    public bool isBomb;
    public int x,y;
    private Action<CellController> onClick;
    public int clickType;

    // Use this for initialization
    public void Init (int x, int y, Action<CellController> click) {
        this.onClick = click;
        this.isBomb = false;
        this.x = x;
        this.y = y;

    }


    // Update is called once per frame
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update () {
        
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            this.clickType = GameController.LEFT_CLICK;
        else if (eventData.button == PointerEventData.InputButton.Right)
            this.clickType = GameController.RIGHT_CLICK;
        else
            this.clickType = 0;

        if (this.clickType != 0 && this.onClick != null)
        {
            this.onClick(this);
        }

    }

    


    public void click()
    {
        if (onClick != null)
            onClick(this);
    }

    
}
