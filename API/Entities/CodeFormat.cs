using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class CodeFormat
{
    public int Id { get; set; }

    public string Format { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public bool IsActive { get; set; }
}
