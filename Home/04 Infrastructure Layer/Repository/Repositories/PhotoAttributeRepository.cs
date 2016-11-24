using System;
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{

    public class PhotoAttributeRepository : Repository<PhotoAttribute>, IPhotoAttributeRepository
    {
        public PhotoAttributeRepository(EFContext context) : base(context)
        {
        }

        public int GetCount(string key)
        {
            return Set.AsNoTracking().Count(nn => nn.AttKey == key);
        }
        public IDictionary<string, int> GetModelCountByMake()
        {
            StringBuilder builder = new StringBuilder(@" select   * from  equipmentview");


            var list = EfContext.Database.SqlQuery<MyClass>(builder.ToString());
            return list.ToDictionary(n => string.Format("{0} {1}", n.make, n.model), n => n.count);
        }
        class MyClass
        {
            public string make { get; set; }
            public string model { get; set; }
            public int count { get; set; }
        }
        public IDictionary<string, int> GetCountByValue(string key, string filter)
        {
            IQueryable<PhotoAttribute> quer = Set.AsNoTracking().Where(n => n.AttKey == key);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                quer = quer.Where(n => n.AttValue.Contains(filter));
            }

            return quer.GroupBy(n => n.AttValue).ToDictionary(n => n.Key, n => n.Count()); ;
        }
        public IDictionary<string, int> GetTimeLineByformat(TimeFormat format, string filtertime = null)
        {
            var list = Set.AsNoTracking().Where(n => n.AttKey == "DateTimeDigitized");
            IQueryable<string> groulist = null;
            switch (format)
            {
                case TimeFormat.YYYY:
                    groulist = list.Select(n => n.AttValue.Substring(0, 4));
                    break;
                case TimeFormat.YYYYMM:
                    groulist = list.Select(n => n.AttValue.Substring(0, 7));
                    break;
                case TimeFormat.YYYYMMDD:
                    groulist = list.Select(n => n.AttValue.Substring(0, 10));
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrWhiteSpace(filtertime)) groulist = groulist.Where(n => n.Contains(filtertime));
            return groulist.GroupBy(n => n).ToDictionary(n => n.Key, n => n.Count());
        }
        public IDictionary<string, int> GetTimeLineMonthByYear(string year)
        {
            var list = Set.AsNoTracking().Where(n => n.AttKey == "DateTimeDigitized").Select(n => n.AttValue.Substring(0, 4)).Where(n => n == year).GroupBy(n => n).ToList();
            return list.ToDictionary(n => n.Key, n => n.Count());
        }
    }
}