using System.Collections.Generic;

internal static class ListPool<T>
{
    // Object pool to avoid allocations.
    private static readonly Pool<List<T>> listPool = new Pool<List<T>>(
        () => new List<T>(),
        null,
        l => l.Clear(), 1);

    public static List<T> Get()
    {
        return listPool.Get();
    }

    public static void Release(List<T> toRelease)
    {
        listPool.Put(toRelease);
    }
}
