using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateShop : MonoBehaviour
{
    public List<string> ShopItems = new List<string>();

    public GameObject ContentGameobject;
    public GameObject ButtonPrefab;

    void Start()
    {
        for(int i = 0; i < ShopItems.Count; i++)
        {
            GameObject TempButton = Instantiate(ButtonPrefab, ContentGameobject.transform);
            TempButton.GetComponent<RectTransform>().localPosition = new Vector3(0, -i * 18, 0);

            RectTransform MyRect = TempButton.GetComponent<RectTransform>();
            MyRect.offsetMax = new Vector2(-150,0);
            MyRect.offsetMin = new Vector2(150,0);
        }
    }
}
