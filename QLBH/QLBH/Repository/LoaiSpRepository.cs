using QLBH.Models;

namespace QLBH.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly QlbandoanContext _context;
        public LoaiSpRepository(QlbandoanContext context)
        {
            _context = context;
        }
        public Loaimonan Add(Loaimonan loaiSp)
        {
            _context.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        public Loaimonan Delete(string maloaiSp)
        {
            throw new NotImplementedException();
        }

        public Loaimonan Get(string maloaiSp)
        {
            return _context.Loaimonans.Find(maloaiSp);
        }

        public IEnumerable<Loaimonan> GetAllLoaiSp()
        {
            return _context.Loaimonans;
        }

        public Loaimonan Update(Loaimonan loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }
    }
}
