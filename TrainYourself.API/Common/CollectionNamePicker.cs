using System;
using System.Linq;

namespace TrainYourself.API.Common
{
    public static class CollectionNamePicker
    {
        public static string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}
