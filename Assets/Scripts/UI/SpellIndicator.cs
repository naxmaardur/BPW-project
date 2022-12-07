using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIndicator : MonoBehaviour
{
    [SerializeField]
    Image _gemImage;

    [SerializeField]
    Image[] _spellNotches;

    public void SetSpellNotches(int count)
    {
        int i = 0;
        foreach(Image image in _spellNotches)
        {
            image.color = i++ < count ? Color.white : Color.black;
        }
        
    }

    public void SetGemColor(Color color)
    {
        _gemImage.color = color;
    }

}
