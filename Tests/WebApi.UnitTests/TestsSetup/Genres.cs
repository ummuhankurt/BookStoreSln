﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entity;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                    new Genre { Name = "Personel Growth" },
                    new Genre { Name = "Science Fiction" },
                    new Genre { Name = "Romance" });
        }
    }
}
