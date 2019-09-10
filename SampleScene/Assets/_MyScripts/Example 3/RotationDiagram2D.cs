using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Example3
{
    public class RotationDiagram2D : MonoBehaviour
    {
        // Start is called before the first frame update
        public Vector2 ItemSize;
        public Sprite[] ItemSprites;
        private List<RotationDiagramItem> itemsList;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        /// <summary>
        /// 创建模板Template物体
        /// </summary>
        /// <returns></returns>
        private GameObject CreateTemplate()
        {
            GameObject item = new GameObject("Template");
            item.AddComponent<RectTransform>().sizeDelta = ItemSize;
            item.AddComponent<RotationDiagramItem>();
            item.AddComponent<Image>();
            return item;
        }
        private void CreateItem()
        {
            GameObject template = CreateTemplate();
            RotationDiagramItem itemTemp = null;
            //resources->prefab->->GameObject->init
            foreach (Sprite sprite in ItemSprites)
            {
                itemTemp=Instantiate(template).GetComponent<RotationDiagramItem>();
                itemTemp.SetParent(transform);
                itemTemp.SetSprite(sprite);
                itemsList.Add(itemTemp);
            }
        }

    }
}
