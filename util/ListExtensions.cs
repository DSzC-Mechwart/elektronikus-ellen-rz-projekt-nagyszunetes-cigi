namespace KretaKlon.util;

public static class ListExtensions {
    public static bool IsEmpty<T>(this List<T>? list) => list == null || list.Count == 0;
}