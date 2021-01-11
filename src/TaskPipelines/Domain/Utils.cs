using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using TaskPipelines.Domain.DataAccess;

namespace TaskPipelines.Domain
{
    public static class Utils
    {
        public static void ThrowIfNull<T>(this T @object, string paramName)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(paramName: paramName);
            }
        }

        public static Task<T> ByIdOrNullAsync<T>(this IMongoCollection<T> set, string id)
            where T : BaseModel
        {
            return set.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}