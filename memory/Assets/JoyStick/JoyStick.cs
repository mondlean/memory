using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler

{
    [SerializeField] private RectTransform rect_JoyStickBackground;
    [SerializeField] private RectTransform rect_JoyStick;

    
    private float Radius;           // 조이스틱 배경의 반 지름.

    [SerializeField] private GameObject go_Player;
    [SerializeField] private float moveSpeed ;

    private bool isTouch = false;
    private Vector3 movePosition;       

    void Start()
    {
        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true);
        
       Radius = rect_JoyStickBackground.rect.width * 0.5f;
    }


    void Update()
    {
        if (isTouch)
        {
            if (rect_JoyStick.localPosition != Vector3.zero)
            {
                go_Player.transform.position += movePosition;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_JoyStickBackground.position;

        value = Vector2.ClampMagnitude(value, Radius);
          
        rect_JoyStick.localPosition = value;

        value = value.normalized;
        movePosition = new Vector3(value.x * moveSpeed * Time.deltaTime, 0f, value.y * moveSpeed* Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_JoyStick.localPosition = Vector3.zero; 
    }
}
 
