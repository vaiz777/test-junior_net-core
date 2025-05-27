using ExportApp.Models;

namespace ExportApp.Data
{
    public interface ISiswaRepository
    {
        List<Siswa> GetRanking();
    }
}