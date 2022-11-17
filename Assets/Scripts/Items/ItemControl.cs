using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public ItemData itemData;

    private void Start()
    {
        //LoadItem()


    }

    private void LoadItem(ItemData itemData)
    {
        GameObject visuals = Instantiate(itemData.model);
        visuals.transform.SetParent(transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;

    }

    //quand le player touche l'item, la valeur 'stat' est augmentée de 'value'
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            //player.stat = player.stat + value;
            //destroy the item;
        }
    }
}





