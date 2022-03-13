using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// script attached to all status and action menu buttons
public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  // button background object
  // may want to retrieve programmatically in the future
  public GameObject background;

  // called when mouse enters button
  public void OnPointerEnter(PointerEventData eventData)
  {
    // invert background and text colors 
    background.SetActive(true);
    ColorBlock color = transform.GetComponent<Button>().colors;
    color.selectedColor = Color.black;
    transform.GetComponent<Button>().colors = color;
    transform.Find("Text").GetComponent<Text>().color = Color.white;
  }

  // called when mouse exits button
  public void OnPointerExit(PointerEventData eventData)
  {
    // invert background and text colors
    background.SetActive(false);
    ColorBlock color = transform.GetComponent<Button>().colors;
    color.selectedColor = Color.white;
    transform.GetComponent<Button>().colors = color;
    transform.Find("Text").GetComponent<Text>().color = Color.black;
  }

  // not sure if this is used
  public void OnDisable()
  {
    transform.Find("Text").GetComponent<Text>().color = Color.black;
  }
}
