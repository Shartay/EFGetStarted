using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class BloggingContext : DbContext // DbContext is the primary class that is responsible for interacting with the database.
{
    public DbSet<Blog> Blogs { get; set; }// DbSet represents the collection of all entities in the context, or that can be queried from the database, of a given type. DbSet objects are created from a DbContext using the DbContext.Set method.
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; } // The path to the database file.

    public BloggingContext() // The default constructor specifies the database to use, and creates a DbSet<Blog> property for the Blogs table in the database, and a DbSet<Post> property for the Posts table.
    {
        var folder = Environment.SpecialFolder.LocalApplicationData; // SpecialFolder provides enumerated values that are used to reference common folders. This is the local application data folder for the current non-roaming user.
        var path = Environment.GetFolderPath(folder); // Gets the path to the system special folder that is identified by the specified enumeration.
        DbPath = System.IO.Path.Join(path, "blogging.db"); // Combines strings into a path.
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Blog 
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}