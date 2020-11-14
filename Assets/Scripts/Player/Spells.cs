using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Mana))]
public class Spells : MonoBehaviour
{
    public Button spell1Button;
    public Button spell2Button;
    public Button spell3Button;

    private Mana _mana;

    private void Awake()
    {
        _mana = GetComponent<Mana>();
    }

    public void OnManaChange()
    {
        if (_mana.currentFraction >= 0.33f)
        {
            spell1Button.interactable = true;
        }
        if (_mana.currentFraction >= 0.66f)
        {
            spell2Button.interactable = true;
        }
        if (_mana.currentFraction == 1f)
        {
            spell3Button.interactable = true;
        }        
    }

    public void CastSpell()
    {
        _mana.Zero();

        spell1Button.interactable = false;
        spell2Button.interactable = false;
        spell3Button.interactable = false;
    }


    private IEnumerator CastSpellCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
    }
}
