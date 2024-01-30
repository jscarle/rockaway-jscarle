using NodaTime;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Data.Sample;

public static partial class SampleData
{
    public static Show WithTicketType(this Show show, Guid id, string name, decimal price, int? limit = null)
    {
        show.TicketTypes.Add(new TicketType(id, show, name, price, limit));
        return show;
    }

    public static Show WithSupportActs(this Show show, params Artist[] artists)
    {
        show.SupportSlots.AddRange(artists.Select(artist => new SupportSlot
        {
            Show = show,
            Artist = artist,
            SlotNumber = show.NextSupportSlotNumber
        }));
        return show;
    }

    public static class Shows
    {
        private static int seed = 1;

        public static readonly Show Coda_Barracuda_20240517 = Venues.Barracuda
            .BookShow(Artists.Coda, new LocalDate(2024, 5, 17))
            .WithTicketType(NextId, "Upstairs unallocated seating", 25, 100)
            .WithTicketType(NextId, "Downstairs standing", 25, 120)
            .WithTicketType(NextId, "Cabaret table (4 people)", 120, 10)
            .WithSupportActs(Artists.KillerBite, Artists.Overflow);

        public static readonly Show Coda_Columbia_20240518 = Venues.Columbia
            .BookShow(Artists.Coda, new LocalDate(2024, 5, 18))
            .WithTicketType(NextId, "General Admission", 35)
            .WithTicketType(NextId, "VIP Meet & Greet", 75, 20)
            .WithSupportActs(Artists.KillerBite, Artists.Overflow);

        public static readonly Show Coda_Bataclan_20240519 = Venues.Bataclan
            .BookShow(Artists.Coda, new LocalDate(2024, 5, 19))
            .WithTicketType(NextId, "General Admission", 35)
            .WithTicketType(NextId, "VIP Meet & Greet", 75)
            .WithSupportActs(Artists.KillerBite, Artists.Overflow, Artists.JavasCrypt);

        public static readonly Show Coda_NewCrossInn_20240520 = Venues.NewCrossInn
            .BookShow(Artists.Coda, new LocalDate(2024, 5, 20))
            .WithTicketType(NextId, "General Admission", 25)
            .WithTicketType(NextId, "VIP Meet & Greet", 55, 20)
            .WithSupportActs(Artists.JavasCrypt);

        public static readonly Show Coda_JohnDee_20240522 = Venues.JohnDee
            .BookShow(Artists.Coda, new LocalDate(2024, 5, 22))
            .WithTicketType(NextId, "General Admission", 350)
            .WithTicketType(NextId, "VIP Meet & Greet", 750, 25)
            .WithSupportActs(Artists.JavasCrypt);

        public static readonly Show Coda_PubAnchor_20240523 = Venues.PubAnchor
            .BookShow(Artists.Coda, new LocalDate(2024, 5, 23))
            .WithTicketType(NextId, "General Admission", 300)
            .WithTicketType(NextId, "VIP Meet & Greet", 720, 10)
            .WithSupportActs(Artists.JavasCrypt);

        public static readonly Show Coda_Gagarin_20240525 =
            Venues.Gagarin.BookShow(Artists.Coda, new LocalDate(2024, 5, 25))
                .WithTicketType(NextId, "General Admission", 25)
                .WithSupportActs(Artists.JavasCrypt, Artists.ScriptKiddies);

        public static IEnumerable<Show> AllShows =
        [
            Coda_Barracuda_20240517,
            Coda_Columbia_20240518,
            Coda_Bataclan_20240519,
            Coda_NewCrossInn_20240520,
            Coda_JohnDee_20240522,
            Coda_PubAnchor_20240523,
            Coda_Gagarin_20240525
        ];

        private static Guid NextId => TestGuid(seed++, 'C');

        public static IEnumerable<TicketType> AllTicketTypes
            => AllShows.SelectMany(show => show.TicketTypes);

        public static IEnumerable<SupportSlot> AllSupportSlots
            => AllShows.SelectMany(show => show.SupportSlots);
    }
}