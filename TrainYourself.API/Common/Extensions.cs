using System.Collections.Generic;
using System.Linq;
using TrainYourself.API.Models;

namespace TrainYourself.API.Common
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this IEnumerable<IDocument> sources)
        {
            return sources == null || sources.Any();
        }
    }
}
