using Neo4j.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingCom.model
{
    public class CHoatDong
    {
        public int Id {get; set;}
        public string _name { get; set; }
        public float _ratings { get; set; }
        public string _gt { get; set; }
        public string _lang { get; set; }
        public string _tcdk { get; set; }

        public CHoatDong() { }
        public CHoatDong(int id, string name, float ratings, string gt, string lang, string tcdk)
        {
            Id = id;
            _name = name;
            _ratings = ratings;
            _gt = gt;
            _lang = lang;
            _tcdk = tcdk;
        }

        public static async Task<List<CHoatDong>> FindByLocation(string location, string _ngaybd, string _ngaykt)
        {
            IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "tungpham134"));
            IAsyncSession session = driver.AsyncSession();
            string query = @"
                        MATCH (hd:HoatDong) -[BAO_GOM_GOI]->(gv:GoiVe) -[BAO_GOM_LOAI]->(p:PhanLoaiVe) WHERE  p.SoluongTon > 0 AND hd.DiaDiemToChuc = $diaDiem AND gv.NgayCuaVe <= date($ngaykt) AND gv.NgayCuaVe >= date($ngaybd) RETURN DISTINCT hd.ID_HoatDong AS ID_HD, hd.TenHoatDong AS TenHD, hd.GioiThieu AS GT, hd.Ratings as Ratings";

            return  await session.ExecuteReadAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync(query, new { diaDiem = location, ngaykt = _ngaykt, ngaybd = _ngaybd }
                );

                return await cursor.ToListAsync(record => new CHoatDong(
                    record["ID_HD"].As<int>(), record["TenHD"].As<string>(), record["Ratings"].As<float>(), record["GT"].As<string>(), "", ""));
            });
        }

        public static async Task<CHoatDong> FindByID(int _id)
        {
            IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "tungpham134"));
            IAsyncSession session = driver.AsyncSession();
            string query = @"MATCH (hoatDong:HoatDong {ID_HoatDong: $idHD}) RETURN hoatDong;";

            return await session.ExecuteReadAsync(async transaction =>
            {
                var cursor = await transaction.RunAsync(query, new {idHD = _id }
                );

                return await cursor.SingleAsync(record => new CHoatDong(
                    record["ID_HD"].As<int>(), record["TenHD"].As<string>(), record["Ratings"].As<float>(), record["GT"].As<string>(), "", ""));
            });
        }

    }
}
