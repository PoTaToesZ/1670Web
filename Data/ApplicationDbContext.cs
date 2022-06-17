using FPTBookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPTBookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }

        protected void onModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedCategory(modelBuilder);
            SeedAuthor(modelBuilder);
            SeedBook(modelBuilder);
        }
        private void SeedCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fantasy", Description = "Fantasy is a genre of speculative fiction involving magical elements, typically set in a fictional universe and sometimes inspired by mythology and folklore. Its roots are in oral traditions, which then became fantasy literature and drama. From the twentieth century, it has expanded further into various media, including film, television, graphic novels, manga, animations and video games.", Image = "https://i.pinimg.com/originals/cb/85/4e/cb854e493d5915abd39158e5b23a1476.jpg" },
                new Category { Id = 2, Name = "Science Fiction", Description = "Science fiction, often called “sci-fi,” is a genre of fiction literature whose content is imaginative, but based in science. It relies heavily on scientific facts, theories, and principles as support for its settings, characters, themes, and plot-lines, which is what makes it different from fantasy.", Image = "https://s26162.pcdn.co/wp-content/uploads/2018/08/EZJa80S.jpg" },
                new Category { Id = 3, Name = "Mystery", Description = "The mystery genre is a genre of fiction that follows a crime (like a murder or a disappearance) from the moment it is committed to the moment it is solved. Mystery novels are often called “whodunnits” because they turn the reader into a detective trying to figure out the who, what, when, and how of a particular crime.", Image = "https://m.media-amazon.com/images/G/01/seo/siege-lists/best-mystery-audiobooks-social.jpg" }
                );
        }
        private void SeedAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, LName = "Joanne", FName = "Rowling" },
                new Author { Id = 2, LName = "Isaac", FName = "Asimov " },
                new Author { Id = 3, LName = "Agatha", FName = "Christie " }
                );
        }

        private void SeedBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Harry Potter", Description = "Harry Potter is a series of seven fantasy novels written by British author J. K. Rowling. The novels chronicle the lives of a young wizard, Harry Potter, and his friends Hermione Granger and Ron Weasley, all of whom are students at Hogwarts School of Witchcraft and Wizardry. The main story arc concerns Harry's struggle against Lord Voldemort, a dark wizard who intends to become immortal, overthrow the wizard governing body known as the Ministry of Magic and subjugate all wizards and Muggles", Image = "https://images-na.ssl-images-amazon.com/images/I/71NFcRl66bL.jpg", Price = 10.99, PublishedDate = "21/06/2003", CategoryId = 1, AuthorId = 1 },
                new Book { Id = 2, Title = "Foundation", Description = "The Foundation series is a science fiction book series written by American author Isaac Asimov. First published as a series of short stories in 1942–50, and subsequently in three collections in 1951–53, for thirty years the series was a trilogy: Foundation; Foundation and Empire; and Second Foundation. It won the one-time Hugo Award for (Best All - Time Series) in 1966.", Image = "https://upload.wikimedia.org/wikipedia/en/2/25/Foundation_gnome.jpg", Price = 4.99, PublishedDate = "12/05/1942", CategoryId = 2, AuthorId = 2 },
                new Book { Id = 3, Title = "The Murder of Roger Ackroyd", Description = "Hercule Poirot has retired to the village of King’s Abbot to cultivate marrows. But when wealthy Roger Ackroyd is found stabbed in his study, he agrees to investigate. A typical village murder mystery; or so it seems until the last chapter with its stunning revelation.", Image = "https://m.media-amazon.com/images/I/41n5CwqOv2L.jpg", Price = 3.49, PublishedDate = "15/06/1926", CategoryId = 3, AuthorId = 3 }
                );
        }
    }
}
