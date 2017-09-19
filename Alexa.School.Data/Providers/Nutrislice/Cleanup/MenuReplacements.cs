using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Alexa.School.Data.Providers.Nutrislice.Cleanup
{
    /// <summary>
    /// For cleaning up the text we get back from Nutrislice that Alexa isn't a fan of. 
    /// Facilitates working with <see cref="MenuReplacement"/>.
    /// </summary>
    internal static class MenuReplacements
    {
        private static readonly List<MenuReplacement> Replacements = new List<MenuReplacement>()
                                                                     {
                                                                         new MenuReplacement(new Regex(pattern: "w/", options: RegexOptions.Compiled), " with "),
                                                                         new MenuReplacement(new Regex(pattern: "®"), string.Empty),
                                                                         new MenuReplacement(new Regex(pattern: "&", options: RegexOptions.Compiled), " and "),
                                                                         new MenuReplacement(new Regex(pattern: "\\(.*\\)", options: RegexOptions.Compiled), " "),
                                                                         new MenuReplacement(new Regex(pattern: "\\s+", options: RegexOptions.Compiled), " "),
                                                                         new MenuReplacement(, ""),
                                                                         new MenuReplacement(, ""),
                                                                         new MenuReplacement(, ""),
                                                                         new MenuReplacement(, ""),
                                                                     };

        // TODO : make an interface for all cleanup processes?
    }
}
