using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  public GameObject background;

  public void OnPointerEnter(PointerEventData eventData)
  {
    background.SetActive(true);
    ColorBlock color = transform.GetComponent<Button>().colors;
    color.selectedColor = Color.black;
    transform.GetComponent<Button>().colors = color;
    transform.Find("Text").GetComponent<Text>().color = Color.white;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    background.SetActive(false);
    ColorBlock color = transform.GetComponent<Button>().colors;
    color.selectedColor = Color.white;
    transform.GetComponent<Button>().colors = color;
    transform.Find("Text").GetComponent<Text>().color = Color.black;
  }
}
