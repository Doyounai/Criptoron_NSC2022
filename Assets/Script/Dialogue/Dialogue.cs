using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue {

	public string name;
    public UnityEvent endDialogue;

    [TextArea(3, 10)]
	public string[] sentences;

}
