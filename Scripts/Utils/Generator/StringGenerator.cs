using System.Linq;
using UnityEngine;

public static class StringGenerator
{
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public static string RandomString(int length)
    {
        return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Range(0, s.Length)]).ToArray());
    }
}