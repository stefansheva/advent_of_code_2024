namespace Utils;

public static class Helpers
{
    /// <summary>
    /// This util function get next element from Enum
    /// example:
    /// enum SampleEnum { LevelOne, LevelTwo }
    /// if we are on LevelOne, Next() will return LevelTwo
    /// </summary>
    /// <param name="src"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf(Arr, src) + 1;
        return (Arr.Length==j) ? Arr[0] : Arr[j];            
    }
}