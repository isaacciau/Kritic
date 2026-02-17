using System.Net.Http.Json;
using Kritik.Shared.Models;


namespace Kritik.App.Services;

public class EvaluationService
{
    private readonly HttpClient _httpClient;

    public EvaluationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> SubmitEvaluationAsync(Evaluation evaluation)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/evaluations", evaluation);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error submitting evaluation: {ex.Message}");
            return false;
        }
    }
}
