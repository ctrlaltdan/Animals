using System;
using System.Collections.Generic;
using Animals.Domain.Animals;
using Animals.Domain.Users;

namespace Animals.Infrastructure
{
    public static class Db
    {
        private static readonly Lazy<List<User>> LazyUsers = new Lazy<List<User>>(ConstructInMemoryStorage<User>, false);
        public static IList<User> Users { get { return LazyUsers.Value; } }

        private static readonly Lazy<List<Animal>> LazyAnimals = new Lazy<List<Animal>>(ConstructInMemoryStorage<Animal>, false);
        public static IList<Animal> Animals { get { return LazyAnimals.Value; } }

        private static List<T> ConstructInMemoryStorage<T>()
        {
            return new List<T>();
        }
    }
}
