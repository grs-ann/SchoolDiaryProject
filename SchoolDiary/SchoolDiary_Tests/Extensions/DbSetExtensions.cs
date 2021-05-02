using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SchoolDiary_Tests.Extensions
{
    /// <summary>
    /// Extension to Db Set. Clears db set completly.
    /// </summary>
    internal static class DbSetExtensions
    {
        /// <summary>
        /// Clears db set completly.
        /// </summary>
        /// <typeparam name="T">Type of entities in db set.</typeparam>
        /// <param name="dbSet">Db set to be cleared.</param>
        /// <returns></returns>
        public static async Task ClearIfAny<T>(this DbSet<T> dbSet) where T : class
        {
            if (await dbSet.AnyAsync())
            {
                dbSet.RemoveRange(dbSet);
            }
        }
    }
}
