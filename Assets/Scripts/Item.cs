using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Apple,
    Pear,
    Banana,
    Bacon,
    Beer,
    Beet,
    Olive,
    Beans,
    Bone,
    Borsch,
    Chiken,
    Bread,
    Broccoli,
    Cheese,
    Cabbage,
    Pie,
    Chocolatte,
    ChupaChups,
    Candy,
    Carrot
}


[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField] private ItemType _type;
    [SerializeField] private Sprite _sprite;

    public ItemType Type => _type;
    public Sprite Sprite => _sprite;
}
