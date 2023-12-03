using System;
using System.Collections.Generic;

namespace BigBrother.Models;

public partial class SoftwareUsage
{
    public long Id { get; set; }

    public string WindowTitle { get; set; } = null!;

    public string ApplicationName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public int Time { get; set; }
}
