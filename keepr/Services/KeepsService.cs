using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _keepsRepository;

  public KeepsService(KeepsRepository keepsRepository)
  {
    _keepsRepository = keepsRepository;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    int keepId = _keepsRepository.CreateKeep(keepData);

    Keep keep = GetKeepById(keepId, keepData.CreatorId);
    return keep;
  }

  internal Keep GetKeepById(int keepId, string userId = null)
  {
    Keep keep = _keepsRepository.GetKeepById(keepId);
    if (keep == null)
    {
      throw new Exception($"Bad keep id: {keepId}.");
    }
    return keep;
  }

  internal List<Keep> GetKeeps()
  {
    List<Keep> keeps = _keepsRepository.GetKeeps();
    return keeps;
  }

  internal List<Keep> GetUsersKeeps(string profileId)
  {
    List<Keep> keeps = _keepsRepository.GetUsersKeeps(profileId);

    // keeps = keeps.FindAll(keep => keep.CreatorId == profileId);
    return keeps;
  }

  internal void RemoveKeep(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("NOT YOUR KEEP TO DELETE!");
    }
    _keepsRepository.RemoveKeep(keepId);
  }

  internal Keep UpdateKeep(Keep keepData, string userId)
  {
    Keep originalKeep = GetKeepById(keepData.Id, userId);
    if (originalKeep.CreatorId != userId)
    {
      throw new Exception("NOT YOUR KEEP TO UPDATE!");
    }

    originalKeep.Name = keepData.Name ?? originalKeep.Name;
    originalKeep.Description = keepData.Description ?? originalKeep.Description;
    originalKeep.Img = keepData.Img ?? originalKeep.Img;

    _keepsRepository.UpdateKeep(originalKeep);

    // Keep updatedKeep = GetKeepById(originalKeep.Id);
    return originalKeep;
  }
}
