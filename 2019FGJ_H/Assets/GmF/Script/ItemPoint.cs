using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    ItemBase item;
    public bool IsHaveItem
    {
        get
        {
            return item != null;
        }
    }

    void Awake()
    {
        if(ItemManager.Instance == null)
        {
            Debug.LogError("ItemPoint Awake too late");
            return;
        }
        if (!(ItemManager.Instance.ItemPoints.Contains(this)))
        {
            ItemManager.Instance.ItemPoints.Add(this);
        }
    }

    public void CreateItem(ItemBase sampleItem)
    {
        if (item != null)
        {
            return;
        }

        sampleItem.gameObject.SetActive(true);
        GameObject newItem = Instantiate(sampleItem.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        newItem.transform.position = transform.position;
        newItem.SetActive(true);
        sampleItem.gameObject.SetActive(false);
        newItem.GetComponent<ItemBase>().OnDeath += ItemOnDeath;

        newItem.gameObject.name += ItemManager.Instance.CreateItemCount;
    }

    void ItemOnDeath(ItemBase deathItem)
    {
        item = null;
        deathItem.OnDeath -= ItemOnDeath;
    }
}
