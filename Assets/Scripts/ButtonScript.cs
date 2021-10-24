using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  public GameObject background;

  public void OnPointerEnter(PointerEventData eventData)
  {
    background.SetActive(true);
    transform.Find("Text").GetComponent<Text>().color = Color.white;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    background.SetActive(false);
    transform.Find("Text").GetComponent<Text>().color = Color.black;
  }
}
