using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
  {
    _vaultKeepsRepository = vaultKeepsRepository;
    _vaultsService = vaultsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
    if (vaultKeepData.CreatorId != vault.CreatorId)
    {
      throw new Exception("YOU CAN'T CREATE A KEEP ON A VAULT THAT ISN'T YOURS!");
    }
    VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
    // KeepInVault keepInVault = GetVaultKeepById(vaultKeepData.Id);
    // keepInVault.Kept++;
    // _keep
    return vaultKeep;
  }

  internal List<KeepInVault> GetKeepsByVaultId(int vaultId, string userId)
  {
    _vaultsService.GetVaultById(vaultId, userId);
    List<KeepInVault> keeps = _vaultKeepsRepository.GetKeepsByVaultId(vaultId);
    return keeps;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = _vaultKeepsRepository.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception($"Bad vaultKeep id: {vaultKeepId}");
    }
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception("Not your vault keep to delete!");
    }
    return vaultKeep;


  }
  internal void RemoveVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId, userId);
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception("NOT YOUR VAULT KEEP!");
    }
    _vaultKeepsRepository.RemoveVaultKeep(vaultKeepId);
  }
}
