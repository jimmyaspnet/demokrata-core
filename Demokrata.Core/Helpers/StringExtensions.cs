// <copyright file="StringExtensions.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Helpers;

using System;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// The string etensions
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    /// Gets the name of the friendly.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <param name="search">The search.</param>
    /// <returns></returns>
    public static async Task<string> GetFriendlyName(this string text, int maxLength, Func<string, Task<bool>>? search = null)
    {
        string result = text.NormalizeText();

        if (result.Length > maxLength)
        {
            result = result[..(text.Length - 4)];
        }

        if (search is not null)
        {
            int counter = 1;

            while (await search(result))
            {
                result = $"{counter}-{result}";
                counter++;
            }
        }

        return result;
    }

    /// <summary>
    /// Normalizes the text.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns></returns>
    public static string NormalizeText(this string text)
    {
        Regex reg = RegexCleaner();
        string textNormalize = text.Trim().ToLower().Normalize(NormalizationForm.FormD);
        textNormalize = reg.Replace(textNormalize, string.Empty);
        textNormalize = Regex.Replace(textNormalize, @"\s+", " ").Replace(' ', '-').TrimEnd('-');

        return textNormalize;
    }

    /// <summary>
    /// Determines whether this instance is image.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>
    ///   <c>true</c> if the specified file name is image; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsImage(this string fileName)
    {
        var extension = Path.GetExtension(fileName);

        return !string.IsNullOrEmpty(extension) && extension switch
        {
            ".gif" or ".jpeg" or ".jpg" or ".png" or ".webp" => true,
            _ => false,
        };
    }

    /// <summary>
    /// Regexes the cleaner.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("[^a-zA-Z0-9 ]")]
    private static partial Regex RegexCleaner();
}