using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ExcelDynamicCase.Questions
{
    /// <summary>
    /// Downloads and extracts the plain‑text description of a Project Euler problem.
    /// Provides both asynchronous and synchronous APIs.
    /// </summary>
    public sealed class EulerProblemParser
    {
        private static readonly HttpClient _client;

        static EulerProblemParser()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            _client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = true,
                CookieContainer = new CookieContainer()
            })
            {
                BaseAddress = new Uri("https://projecteuler.net/"),
                Timeout = TimeSpan.FromSeconds(10)
            };

            _client.DefaultRequestHeaders.UserAgent.ParseAdd("EulerProblemParser/1.0 (+github.com/your-repo)");
            _client.DefaultRequestHeaders.Accept.ParseAdd("text/html");
        }

        /// <summary>
        /// Downloads the specified Project Euler problem page and returns a clean UTF‑16 string.
        /// Multiplication signs (&times;) and other HTML entities are decoded automatically.
        /// </summary>
        /// <param name="problemNumber">Problem number (1 – n).</param>
        /// <returns>Multiline plain‑text problem statement.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the parser cannot locate the &lt;div class="problem_content"&gt; element.
        /// </exception>
        public async Task<string> GetProblemTextAsync(int problemNumber)
        {
            if (problemNumber <= 0) throw new ArgumentOutOfRangeException(nameof(problemNumber));

            var response = await _client.GetAsync($"problem={problemNumber}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var html = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Problem statement is wrapped in <div class="problem_content" role="problem"> … </div>
            var node = doc.DocumentNode.SelectSingleNode("//div[@class='problem_content']") ?? throw new InvalidOperationException("Problem content element not found – the site layout may have changed or login may be required.");

            // HtmlAgilityPack keeps the raw markup. Decode entities like &times; → ×.
            var raw = WebUtility.HtmlDecode(node.InnerText);

            // Normalise whitespace for a clean, readable string.
            raw = raw.Replace('\u00A0', ' ');                        // non‑breaking space → space
            raw = Regex.Replace(raw, @"[ \t]+\n", "\n");             // trim end‑of-line spaces
            raw = Regex.Replace(raw, @"\r?\n\s*\r?\n", "\n\n");      // collapse blank lines
            raw = Regex.Replace(raw, @"[ \t]{2,}", " ");             // collapse runs of spaces

            raw = DecodeLatex(raw);

            return raw.Trim();
        }

        /// <summary>
        /// Synchronous wrapper around <see cref="GetProblemTextAsync"/> for environments that prefer blocking calls.
        /// </summary>
        /// <param name="problemNumber">Problem number (1 – n).</param>
        /// <returns>Multiline plain‑text problem statement.</returns>
        public string GetProblemText(int problemNumber)
        {
            // Blocking wait; ConfigureAwait(false) above avoids deadlock on UI contexts.
            return GetProblemTextAsync(problemNumber).GetAwaiter().GetResult();
        }

        private static readonly (string pattern, string replacement)[] _latexRules = new (string, string)[]
        {
            // Simple symbol replacements – patterns use doubled backslashes so the regex engine sees a single \.
            (@"\\times", "×"),
            (@"\\cdot", "·"),
            (@"\\pm",    "±"),
            (@"\\leq",   "≤"),
            (@"\\geq",   "≥"),
            (@"\\neq",   "≠"),

            // Fractions – keeps it readable for plain-text editors
            (@"\\frac{([^{}]+)}{([^{}]+)}", "$1/$2"),

            // Square roots
            (@"\\sqrt{([^{}]+)}", "√($1)"),

            // Remove size/spacing commands we don’t want in plain text
            (@"\\left",  ""),
            (@"\\right", ""),

            // \text{…} → …
            (@"\\text{([^}]+)}", "$1")
        };

        private static string DecodeLatex(string text)
        {
            // 1. Strip inline math delimiters
            var cleaned = text.Replace("$", string.Empty);

            // 2. Apply regex‑based replacements
            foreach (var (pattern, replacement) in _latexRules)
            {
                cleaned = Regex.Replace(cleaned, pattern, replacement);
            }

            return cleaned;
        }
    }
}
