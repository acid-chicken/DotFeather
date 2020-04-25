using System;

namespace DotFeather.Demo
{
[System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
sealed class DescriptionAttribute : Attribute
{
    public string Language {
        get;
        set;
    }
    public string Text {
        get;
        set;
    }

    // This is a positional argument
    public DescriptionAttribute(string lang, string text) => (Language, Text) = (lang, text);
}
}
