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

    [Rito.CAT.EditorDI(Rito.CAT.DiMethod.FindObjectOfType)]
    [SerializeField]
    private UnityEngine.UI.Image _bgImage3;

    public Rigidbody noDI3;

    [Space(12f)]
    [Rito.CAT.EditorDI]
    public Canvas anyCanvas;
    [Rito.CAT.EditorDI("A")]
    public Canvas uiCanvasA;
    [Rito.CAT.EditorDI("B")]
    public Canvas uiCanvasB;
}
