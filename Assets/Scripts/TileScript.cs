using UnityEngine;

public class TileScript : MonoBehaviour
{
  public void SetCellColor(MapColor colorIn)
  {
    this.GetComponent<SpriteRenderer>().color = Constants.EnumToCellColor(colorIn);
  }

  public void SetHighlightColor(MapColor colorIn)
  {
    transform.Find("Highlight").GetComponent<SpriteRenderer>().color = Constants.EnumToHighlightColor(colorIn);
  }

  public void ActivateHighlight(bool active)
  {
    transform.Find("Highlight").gameObject.SetActive(active);
  }

  // Start is called before the first frame update
  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnMouseEnter()
  {
  }

  private void OnMouseExit()
  {
  }
}