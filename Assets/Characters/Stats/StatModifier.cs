using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModifierType
{
    Base = 0,
    Flat = 1,
    PercentAdd = 2,
    PercentMult = 3,
    FlatAtEnd = 4,
}

public class StatModifier
{
    public float value;
    public StatModifierType type;
    public int order;
    public object source;

    public StatModifier(float value, StatModifierType type, int order, object source)
    {
        this.value = value;
        this.type = type;
        this.order = order;
        this.source = source;
    }

    public StatModifier(float value, StatModifierType type) : this(value, type, (int)type, null) { }

    public StatModifier(float value, StatModifierType type, int order) : this(value, type, order, null) { }

    public StatModifier(float value, StatModifierType type, object source) : this(value, type, (int)type, source) { }
}
