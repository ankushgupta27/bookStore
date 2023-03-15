using System;
using System.Collections.Generic;

namespace bookStore.Entities;

public partial class BookTable
{
    public int Id { get; set; }

    public string? BookName { get; set; }

    public string? Category { get; set; }

    public string? Image { get; set; }

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public string? Description { get; set; }
}
