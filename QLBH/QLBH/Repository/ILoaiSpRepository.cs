using QLBH.Models;
namespace QLBH.Repository
{
    public interface ILoaiSpRepository
    {
        Loaimonan Add(Loaimonan loaiSp);
        Loaimonan Update(Loaimonan loaiSp);
        Loaimonan Delete(string maloaiSp);
        Loaimonan Get(string maloaiSp);
        IEnumerable<Loaimonan> GetAllLoaiSp();

    }
}
