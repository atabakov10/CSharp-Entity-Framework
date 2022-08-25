using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MusicHub
{
    using System;

    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context = 
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
            string result = ExportAlbumsInfo(context, 9);
            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
                StringBuilder sb = new StringBuilder();
            var albumsInfo = context
                .Albums
                .Where(a => a.ProducerId.Value == producerId)
                .Include(a=> a.Producer)
                .Include(a=> a.Songs)
                .ThenInclude(s=> s.Writer)
                .ToArray()
                
                .Select(a => new
                {
                    AlbumName = a.Name,
                    ReleaseDate = $"{a.ReleaseDate:MM/dd/yyyy}",
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs
                        .Select(s=> new
                        {
                            SongName = s.Name,
                            s.Price,
                            Writer = s.Writer.Name,
                        })
                        .OrderByDescending(s=> s.SongName)
                        .ThenBy(s => s.Writer)
                        .ToArray(),
                    TotalPrice = a.Price
                })
                .OrderByDescending(a => a.TotalPrice)
                .ToArray();

            foreach (var a in albumsInfo)
            {
                sb
                    .AppendLine($"-AlbumName: {a.AlbumName}")
                    .AppendLine($"-ReleaseDate: {a.ReleaseDate}")
                    .AppendLine($"-ProducerName: {a.ProducerName}")
                    .AppendLine($"-Songs: ");
                int songCounter = 1;
                foreach (var song in a.Songs)
                {
                    sb
                        .AppendLine($"---#{songCounter++}")
                        .AppendLine($"---SongName: {song.SongName}")
                        .AppendLine($"---Price: {song.Price:f2}")
                        .AppendLine($"---Writer: {song.Writer}");
                }

                sb
                    .AppendLine($"-AlbumPrice: {a.TotalPrice:f2}");
            }
            return sb.ToString().Trim();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            throw new NotImplementedException();
        }
    }
}
