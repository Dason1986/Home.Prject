using System;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Home.Repository.Repositories
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

        public EequipmentItem[] GetModelCountByMake()
        {
            StringBuilder builder = new StringBuilder(@" select   * from  equipmentview");

            var list = EfContext.Database.SqlQuery<EequipmentItem>(builder.ToString());
            return list.ToArray();
        }

        public IDictionary<string, int> GetCountByValue(string key, string filter)
        {
            IQueryable<PhotoAttribute> quer = Set.AsNoTracking().Where(n => n.AttKey == key);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                quer = quer.Where(n => n.AttValue.Contains(filter));
            }
            return quer.GroupBy(n => n.AttValue).ToDictionary(n => n.Key, n => n.Count());
        }

        public TimeLineItem[] GetTimeLineByformat(TimeFormat format, string filtertime = null)
        {
            var list = EfContext.Database.SqlQuery<TimeLineItem>("select * from TimeLineBy" + format).ToArray();
            return string.IsNullOrEmpty(filtertime) ? list : list.Where(n => n.TimeLine.Contains(filtertime)).ToArray();
        }
    }
}