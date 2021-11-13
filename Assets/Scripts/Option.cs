using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option
{
  public string optionName { get; set; }
  public List<Option> subOptions
  {
    get { return subOptions; }
    set { subOptions = value; }
  }
  public Option parentOption { get; set; }
  public Constants.OptionActionCallback callback { get; set; }
}
