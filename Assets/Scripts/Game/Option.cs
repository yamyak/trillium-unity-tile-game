using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// button selection option object
public class Option
{
  // name associated with option
  public string optionName { get; set; }
  // parent button/action
  // optional
  public Option parentOption { get; set; }
  // child options/buttons 
  // used to set up buttons when option selected
  // optional
  public List<Option> subOptions;
  // callback function called when option selected
  public Constants.OptionActionCallback actionCallback { get; set; }
}
