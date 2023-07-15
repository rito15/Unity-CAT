using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_DITester : MonoBehaviour
{
    [Rito.CAT.EditorDI(Rito.CAT.DiMethod.FindObjectOfType, false, "Image BG")]
    [SerializeField]
    private UnityEngine.UI.Image _bgImage;

    public GameObject noDI;

    [Rito.CAT.EditorDI(Rito.CAT.DiMethod.FindObjectOfType, "Image BG")]
    [SerializeField]
    private UnityEngine.UI.Image _bgImage2;

    public Light noDI2;
}
