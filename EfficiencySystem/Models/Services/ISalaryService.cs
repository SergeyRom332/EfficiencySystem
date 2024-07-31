namespace EfficiencySystem.Models.Services
{
    public interface ISalaryService
    {
        public Task<List<Salary>> CalculateCashiersSalaries(DateTime dateFirst, DateTime dateSecond);
        public Task<List<Salary>> CalculateCooksSalaries(DateTime dateFirst, DateTime dateSecond);
        public Task<List<Salary>> CalculateCouriersSalaries(DateTime dateFirst, DateTime dateSecond);
        public byte[] CreateSalaryFile(List<Salary> data);
    }
}
