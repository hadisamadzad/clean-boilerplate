namespace Common.Extensions;

public static class EnumExtension
{
    public static int GetMaxLength(this Enum value)
    {
        var type = value.GetType();
        var names = Enum.GetNames(type);
        return names.Select(name => name.Length).Concat([0]).Max();
    }
}