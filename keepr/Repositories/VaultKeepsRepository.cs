using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
    INSERT INTO vaultKeeps (creatorId, vaultId, keepId)
    VALUES (@CreatorID, @VaultId, @KeepId);
    SELECT LAST_INSERT_ID()
    ;";

    int vaultKeepId = _db.ExecuteScalar<int>(sql, vaultKeepData);
    vaultKeepData.Id = vaultKeepId;

    return vaultKeepData;
  }

  internal List<KeepInVault> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
        SELECT 
        vk.*,
        k.*,
        acc.*
        FROM vaultKeeps vk
        JOIN keeps k ON vk.keepId = k.id
        JOIN accounts acc ON acc.id = vk.creatorId
        WHERE vk.vaultId = @vaultId
        ;";

    List<KeepInVault> keeps = _db.Query<VaultKeep, KeepInVault, Profile, KeepInVault>(
      sql,
      (vaultKeep, keep, profile) =>
      {
        keep.VaultKeepId = vaultKeep.Id;
        keep.Creator = profile;
        return keep;
      }, new { vaultId }
      ).ToList();
    return keeps;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"
        SELECT vk.*,
        acc.*
        FROM vaultKeeps vk
        JOIN accounts acc ON acc.id = vk.creatorId
        WHERE vk.id = @vaultKeepId
        ;";

    VaultKeep vaultKeep = _db.Query<VaultKeep, Profile, VaultKeep>(
      sql,
      (vaultKeep, profile) =>
      {
        vaultKeep.CreatorId = profile.Id;
        return vaultKeep;
      },
      new { vaultKeepId }
      ).FirstOrDefault();
    return vaultKeep;
  }

  internal void RemoveVaultKeep(int vaultKeepId)
  {
    string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";

    _db.Execute(sql, new { vaultKeepId });
  }
}
