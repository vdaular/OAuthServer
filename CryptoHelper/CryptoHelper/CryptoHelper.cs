using System.Text.Json;

namespace CryptoHelper;

public class CryptoHelper
{
    public static async Task<string> DecryptAsync(string encryptedText)
    {
        string key = Uri.EscapeDataString(Environment.GetEnvironmentVariable("CKModule")!);
        encryptedText = Uri.EscapeDataString(encryptedText);
        string apiUrl = $"https://idmadmin.gire.com:4430/Criptografia/v1/Decrypt?key={key}&toCiph={encryptedText}&api-key=ArAilHVOoL3upX78Cohq";

        using HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseData);
            string keyValue = jsonDocument.RootElement.GetProperty("key_value").GetString()!;

            return keyValue;
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            return string.Empty;
        }
    }
}
