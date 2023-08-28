using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Models;

public class Profile : RepoItem<string>
{
    public string Name { get; set; }
    public string Picture { get; set; }
    public string CoverImg { get; set; }
}
