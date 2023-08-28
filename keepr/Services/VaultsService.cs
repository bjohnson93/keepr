using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultsRepository;

  public VaultsService(VaultsRepository vaultsRepository)
  {
    _vaultsRepository = vaultsRepository;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    int vaultId = _vaultsRepository.CreateVault(vaultData);

    Vault vault = GetVaultById(vaultId, vaultData.CreatorId);
    return vault;
  }

  internal List<Vault> GetUsersVaults(string profileId)
  {
    throw new NotImplementedException();
  }

  internal Vault GetVaultById(int vaultId, string userId = null)
  {
    Vault vault = _vaultsRepository.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception($"Bad vault id: {vaultId}");
    }
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception($"Bad vault id... {vaultId}, Nope!");
    }
    return vault;
  }

  internal void RemoveVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("NOT YOUR VAULT TOD DELETE!");
    }
    _vaultsRepository.RemoveVault(vaultId);
  }

  internal Vault UpdateVault(Vault vaultData, string userId)
  {
    Vault originalVault = GetVaultById(vaultData.Id, userId);
    if (originalVault.CreatorId != userId)
    {
      throw new Exception("NOT YOUR VAULT TO UPDATE!");
    }
    originalVault.Name = vaultData.Name ?? originalVault.Name;
    originalVault.Description = vaultData.Description ?? originalVault.Description;
    originalVault.Img = vaultData.Img ?? originalVault.Img;
    originalVault.IsPrivate = vaultData.IsPrivate ?? originalVault.IsPrivate;

    _vaultsRepository.UpdateVault(originalVault);

    return originalVault;

  }
}
