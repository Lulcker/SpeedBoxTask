using TaskApiCdek.Data;
using TaskApiCdek.ViewModels;

namespace TaskApiCdek.Repositories
{
    public interface ICdekClient
    {
        List<City> GetCity();

        double CalculateTariff(IndexViewModel model);
    }
}
