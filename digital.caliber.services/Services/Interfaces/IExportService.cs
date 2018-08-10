using System.IO;
using System.Threading.Tasks;

namespace digital.caliber.services.Services
{
    public interface IExportService
    {
        Task<(byte[], string)> ExportToPdf(string userId, int id);

        Task<(byte[], string)> ExportToExcel(string userId, int id);
    }
}
