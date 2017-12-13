using System;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace Home.Repository.Repositories
{
    public class PhotoAttributeRepository : Repository<PhotoAttribute>, IPhotoAttributeRepository
    {
        public PhotoAttributeRepository(DbContext context) : base(context)
        {
        }

        public int GetCount(string key)
        {
            return Wrapper.Find().AsNoTracking().Count(nn => nn.AttKey == key);
        }

        public EequipmentItem[] GetModelCountByMake()
        {
            StringBuilder builder = new StringBuilder(@" select   * from  equipmentview");

            var list =ExecuteQuery<EequipmentItem>(builder.ToString());
            return list.ToArray();
        }
       
        public IDictionary<string, int> GetCountByValue(string key, string filter)
        {
            
          
           var sql= string.Format("select AttValue 'Name',count(0) 'count' from photoattribute where AttKey='{0}' GROUP BY AttValue", key);
           var list= this.ExecuteQuery<StatisticsItem>(sql);
            
            return list.ToDictionary(n => n.Name, n => n.Count);
        }

        public TimeLineItem[] GetTimeLineByformat(TimeFormat format, string filtertime = null)
        {
            var list = ExecuteQuery<TimeLineItem>("select * from TimeLineBy" + format).ToArray();
            return string.IsNullOrEmpty(filtertime) ? list : list.Where(n => n.TimeLine.Contains(filtertime)).ToArray();
        }
    }
}