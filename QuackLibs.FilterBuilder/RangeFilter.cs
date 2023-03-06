namespace QuackLibs.FilterBuilder;

public class RangeFilter<T> : List<T> where T : struct
{
    public T? Min
    {
        get;
        set;
    }

    public T? Max
    {
        get;
        set;
    }

    public bool HasMin => Min.HasValue;

    public bool HasMax => Max.HasValue;

    public bool HasRange => base.Count > 0;

    public RangeFilter(T[] items) : base((IEnumerable<T>)items)
    {
    }

    public RangeFilter(T? min, T? max)
    {
        Min = min;
        Max = max;
    }
}
