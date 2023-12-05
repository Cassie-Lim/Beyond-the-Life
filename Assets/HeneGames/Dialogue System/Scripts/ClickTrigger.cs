using UnityEngine;
using UnityEngine.EventSystems;

// public class EventClick : MonoBehaviour,IPointerClickHandler
// {
//     public void OnPointerClick(PointerEventData eventData)
//     {
//        //Output to console the clicked GameObject's name and the following message.
//         Debug.Log("Clicked: "+name);
//     }
// }
public class EventClick : MonoBehaviour
{
    public void OnMouseDown()
    {
       //Output to console the clicked GameObject's name and the following message.
        Debug.Log("Clicked: "+name);
    }
}

