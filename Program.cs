using System.Text;
using Newtonsoft.Json;

public class Program
{
    private const string ApiUrl = "";
    private const string ApiKey = "";
    private static readonly HttpClient httpClient = new(){
        BaseAddress = new Uri(ApiUrl),
        Timeout = TimeSpan.FromSeconds(3000)
    };

    // ■本ツールで実際に行ったテストについて
    // Test1. bulkdelete [使い方] https://pleasanter.org/ja/manual/api-table-bulk-delete
    private static readonly List<string> TEST_1_1_SELECTED =
    [
        "9998",
        "9999"
    ];
    private static readonly View TEST_1_2_VIEW = new()
    {
        ColumnFilterHash = new ColumnFilterHash
        {
            Incomplete = true
        }
    };
    private static readonly bool TEST_1_3_ALL = true;

    static async Task Main()
    {
        try {
            await PostAsync(httpClient);
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        } finally {
            httpClient.Dispose();
            Console.WriteLine("Complete.");
        }
    }

    // ■WHAT'S TEST?
    // Test1-1. Selected = TEST_1_1_SELECTED
    // Test1-2. View = TEST_1_2_VIEW
    // Test1-3. All = TEST_1_3_ALL

    static async Task PostAsync(HttpClient httpClient)
    {
        using StringContent jsonContent = new(
            content: JsonConvert.SerializeObject(new PleasanterApiFieldes {
                ApiKey = ApiKey,
                ApiVersion = "1.0",
                {WHAT'S TEST?} }),
            encoding: Encoding.UTF8,
            mediaType: "application/json");
        using HttpResponseMessage response = await httpClient.PostAsync(
            requestUri: httpClient.BaseAddress,
            content: jsonContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }
}

public class PleasanterApiFieldes
{
    [JsonProperty("ApiVersion")]
    public required string ApiVersion { get; set; }
    [JsonProperty("ApiKey")]
    public required string ApiKey { get; set; }
    [JsonProperty("Selected")]
    public List<string>? Selected { get; set; } = null;
    [JsonProperty("All")]
    public bool? All { get; set; } = false;
    [JsonProperty("View")]
    public View? View { get; set; } = null;
}

public class View{
    [JsonProperty("ColumnFilterHash")]
    public ColumnFilterHash? ColumnFilterHash { get; set; }
}

public class ColumnFilterHash{
    [JsonProperty("Incomplete")]
    public bool? Incomplete { get; set; }
}
