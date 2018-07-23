using System;
using Microsoft.EntityFrameworkCore;

namespace BestTestPlayers.Models
{
    public class PlayerContext: DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options)
        {

        }

        public DbSet<Player> Player { get; set; }
    }
}
