using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Models;

public class VaultKeep : RepoItem<int>
{
  public int VaultId { get; set; }
  public int KeepId { get; set; }
  public string CreatorId { get; set; }
}

public class KeepInVault : Keep
{
  public int VaultId { get; set; }
  public int KeepId { get; set; }
  public int VaultKeepId { get; set; }
}