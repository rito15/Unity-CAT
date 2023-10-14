using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{

    public class Demo2_EditorDIGameObject : MonoBehaviour
    {
        [EditorDI(ComponentType = typeof(Collider))]
        public GameObject go1;

        [EditorDI(ComponentType = typeof(Collider), NameIncludes = "2")]
        public GameObject go2;

        [EditorDI(ComponentType = typeof(Collider), NameEquals = "col9")]
        public GameObject go3;

        [EditorDI(ComponentType = typeof(Collider), NameEquals = "col99")]
        public GameObject go4;

        [EditorDI(ComponentType = typeof(Collider), NameEquals = "col8", IncludeDisabledObject = false)]
        public GameObject go5_fail;


        [EditorDI(DiMethod.GetComponentInChildren, 
            ComponentType = typeof(Collider), NameEquals = "col99")]
        public GameObject childGo1;

        [EditorDI(DiMethod.GetComponentInChildren,
            ComponentType = typeof(Collider), NameEquals = "col99", IncludeDisabledObject = false)]
        public GameObject childGo2_fail;

        [EditorDI(NameEquals = "main camera")]
        public GameObject mainCam1;

        [EditorDI(NameIncludes = "main")]
        public GameObject mainCam2;

        [EditorDI(NameEquals = "main camera", IgnoreCase = false)]
        public GameObject mainCam_fail;
    }
}