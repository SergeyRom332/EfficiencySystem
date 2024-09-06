using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;

namespace EfficiencySystem.Models
{
    public class WorkShiftHttpClient
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public WorkShiftHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _httpClient.BaseAddress = new Uri(_configuration["ServiceUrls:WorkShiftService"] ?? "http://127.0.0.1:5232");
        }

        public async Task<List<WorkShift>> GetWorkShifts(DateTime dateFirst, DateTime dateSecond, int restaurantId)
        {
            var response = await _httpClient.GetAsync(
                $"/workshifts?dateFirst={dateFirst.ToString("yyyy-MM-dd")}&dateSecond={dateSecond.ToString("yyyy-MM-dd")}&restaurantId={restaurantId}");
            if (!response.IsSuccessStatusCode) throw new Exception($"Can't get workshifts list. Workshift service error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");

            return await response.Content.ReadFromJsonAsync<List<WorkShift>>() ?? new List<WorkShift>();
        }

        public async Task<WorkShift> GetWorkShift(int id)
        {
            var response = await _httpClient.GetAsync($"/workshifts/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception($"Can't get workshift, id = {id}. Workshift service error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");

            return await response.Content.ReadFromJsonAsync<WorkShift>() ?? new WorkShift();
        }

        public async Task AddWorkShiftAsync(WorkShift workShift)
        {
            var response = await _httpClient.PostAsJsonAsync("/workshifts", workShift);
            if (!response.IsSuccessStatusCode) throw new Exception($"Can't post workshift, id = {workShift.Id}. Workshift service error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
        }
    }
}
