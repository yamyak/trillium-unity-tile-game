using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option
{
  public string optionName { get; set; }
  public Option parentOption { get; set; }
  public List<Option> subOptions;
  public Constants.OptionActionCallback actionCallback { get; set; }
}
