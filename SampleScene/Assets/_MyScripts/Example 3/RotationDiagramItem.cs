using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Example3 {
    public class RotationDiagramItem : MonoBehaviour
    {
        // Start is called before the first frame update
        private Image myImage;
        private Image MyImage
        {
            get {
                if (myImage == null)
                    myImage = GetComponent<Image>();
                return myImage;      
               }
        }
        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }
        public void SetSprite(Sprite sprite)
        {
            MyImage.sprite = sprite;
        }
    }
}
