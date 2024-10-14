using System.Text;

namespace OAuth2Server.Helpers;

public static class FileHelpers
{
    public static async Task<string> ReadFileAsync(string path)
    {
        if (!string.IsNullOrEmpty(path) && File.Exists(path))
        {
            using (var sr = new StreamReader(path))
            {
                string text = await sr.ReadToEndAsync();

                return text;
            }
        }
        else
            return string.Empty;
    }

    public static async Task SaveFileAsync(string path, string text, bool isAppend = false)
    {
        using (var sw = new StreamWriter(path, isAppend, Encoding.UTF8))
            await sw.WriteLineAsync(text);
    }
}