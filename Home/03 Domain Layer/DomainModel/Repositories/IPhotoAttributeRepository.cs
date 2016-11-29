using Home.DomainModel.Aggregates.GalleryAgg;
using Library.Domain.Data;
using System.Collections.Generic;

namespace Home.DomainModel.Repositories
{

    public interface IPhotoAttributeRepository : IRepository<PhotoAttribute>
    {
        int GetCount(string key);

        IDictionary<string, int> GetTimeLineByformat(TimeFormat format, string filtertime = null);

        IDictionary<string, int> GetCountByValue(string key, string filter = null);
        IDictionary<string, int> GetTimeLineMonthByYear(string year);

        IDictionary<string, int> GetModelCountByMake();
    }

    public enum TimeFormat
    {
        YYYY,
        YYYYMM,
        YYYYMMDD

    }
}