using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSticker : Sticker
{
	void Start ()
    {
        GetComponentInChildren<TextMesh>().text = Color.ToString();
	}
}
