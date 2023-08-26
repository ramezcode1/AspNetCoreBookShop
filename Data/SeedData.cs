using BookStore.Models;

namespace BookStore.Data
{
    public static class SeedData
    {
        public static void SeedDB(ApplicationDbContext context)
        {
            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }

            var catFiction = new BookCategory() { Name = "Fiction" };
            var catFantasy = new BookCategory() { Name = "Fantasy" };
            var catClassics = new BookCategory() { Name = "Classics" };

            context.Books.AddRange(
                new Book() { ISBN = "ABC-111", Title = "The Hunger Games", Author = "Suzanne Collins", Price = 26.5, BookCategory = catFiction, 
                    Description = "Could you survive on your own, in the wild, with everyone out to make sure you don't live to see the morning?"
                },
                new Book() { ISBN = "ABC-222", Title = "Harry Potter and the Order of the Phoenix", Author = "J.K. Rowling", Price = 30.5, BookCategory = catFantasy, 
                    Description = "There is a door at the end of a silent corridor. And it’s haunting Harry Pottter’s dreams. Why else would he be waking in the middle of the night, screaming in terror?"
                },
                new Book() { ISBN = "ABC-333", Title = "The Chronicles of Narnia", Author = "C.S. Lewis", Price = 40.3, BookCategory = catFiction, 
                    Description = "Journeys to the end of the world, fantastic creatures, and epic battles between good and evil—what more could any reader ask for in one book? The book that has it all is"
                },
                new Book() { ISBN = "ABC-444", Title = "The Hobbit and The Lord of the Rings", Author = "J.R.R. Tolkien", Price = 18.72, BookCategory = catClassics, 
                    Description = "This four-volume, boxed set contains J.R.R. Tolkien's epic masterworks The Hobbit and the three volumes of The Lord of the Rings (The Fellowship of the Ring, The Two Towers, and The Return of the King)."
                },
                new Book() { ISBN = "ABC-555", Title = "Divergent", Author = "Veronica Roth", Price = 19.99, BookCategory = catFiction, 
                    Description = "In Beatrice Prior's dystopian Chicago world, society is divided into five factions, each dedicated to the cultivation of a particular virtue. On an appointed day of every year, all sixteen-year-olds must select the faction to which they will devote the"
                },
                new Book() { ISBN = "ABC-666", Title = "City of Bones", Author = "City of Bones", Price = 21.3, BookCategory = catFantasy, 
                    Description = "When fifteen-year-old Clary Fray heads out to the Pandemonium Club in New York City, she hardly expects to witness a murder― much less a murder committed by three teenagers covered with strange tattoos and brandishing bizarre weapons. "
                },
                new Book() { ISBN = "ABC-777", Title = "Anne of Green Gables", Author = "L.M. Montgomery", Price = 27.55, BookCategory = catFiction, 
                    Description = "As soon as Anne Shirley arrives at the snug white farmhouse called Green Gables, she is sure she wants to stay forever. but will the Cuthberts send her back to to the orphanage? "
                },
                new Book() { ISBN = "ABC-888", Title = "The Little Prince", Author = "Antoine de Saint-Exupéry", Price = 31.3, BookCategory = catClassics, 
                    Description = "Moral allegory and spiritual autobiography, The Little Prince is the most translated book in the French language. With a timeless charm it tells the story of a little boy who leaves the safety of his own tiny planet to travel the universe" }
            );
            context.SaveChanges();
        }
    }
}
