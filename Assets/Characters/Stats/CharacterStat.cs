using System;
using System.Collections.Generic;

[Serializable]
public class CharacterStat
{
    public float baseValue;
    public float maxValue;

    private float _value;

    public virtual float Value
    {
        get {
            _value = CalculateFinalValue();
            return _value;
        }
        set { _value = value; }
    }

    private List<StatModifier> _statModifiers;

    public CharacterStat()
    {
        _statModifiers = new List<StatModifier>();
    }

    public CharacterStat(float baseValue, float maxValue) : this()
    {
        this.baseValue = baseValue;
        this.maxValue = maxValue;
    }

    public virtual void AddModifier(StatModifier modifier)
    {
        _statModifiers.Add(modifier);
    }

    public virtual void RemoveModifier(StatModifier modifier)
    {
        _statModifiers.Remove(modifier);
    }

    public virtual void RemoveModifiersFromSource(object source)
    {
        _statModifiers.RemoveAll(mod => mod.source == source);
    }

    // T‰‰ vois olla ehk‰ jopa jossain helpereiss‰
    // Sorttauksen tueks
    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        return 0;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue; // TODO: T‰n vois tutkia et ei vahingossakaan muokkaa C#:ssa tuota basevaluee?!
        float sumPercentAdd = 0;

        _statModifiers.Sort(CompareModifierOrder);

        for (int i = 0; i < _statModifiers.Count; i++)
        {
            StatModifier mod = _statModifiers[i];
            
            // Pystyt‰‰n tiputtamaan esim pohjahp 50% tms.
            if (mod.type == StatModifierType.Base)
            {
                finalValue *= mod.value;
            }
            // Ammutaan sen p‰‰lle flatti value
            else if (mod.type == StatModifierType.Flat)
            {
                finalValue += mod.value;
            }
            // Poesta tuttu %increased
            else if (mod.type == StatModifierType.PercentAdd)
            {
                sumPercentAdd += mod.value;

                if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].type != StatModifierType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            // Poesta tuttu %more
            else if (mod.type == StatModifierType.PercentMult)
            {
                finalValue *= 1 + mod.value;
            }
            // T‰ll‰ voidaan poistaa kˆnts‰ lopusta, esim ottaa dmg ... TODO: Ehk‰.
            else if (mod.type == StatModifierType.FlatAtEnd)
            {
                finalValue += mod.value;
            }
        }

        float roundedValue = (float)Math.Round(finalValue, 4);
        // Kurkataan meneekˆ maksimin yli
        return roundedValue > maxValue ? maxValue : roundedValue;
    }
}
