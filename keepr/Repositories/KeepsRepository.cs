using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateKeep(Keep keepData)
  {
    string sql = @"
        INSERT INTO keeps(name, description, img, kept, creatorId)
        VALUES(@Name, @Description, @Img, @Kept, @CreatorId);
        SELECT LAST_INSERT_ID()
        ;";

    int keepId = _db.ExecuteScalar<int>(sql, keepData);
    return keepId;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
        SELECT
        k.*,
        acc.*
        FROM keeps k
        JOIN accounts acc ON acc.id = k.creatorId
        WHERE k.id = @keepId
        ;";

    Keep keep = _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      },
      new { keepId }
      ).FirstOrDefault();
    return keep;
  }

  internal List<Keep> GetKeeps()
  {
    string sql = @"
        SELECT
        k.*,
        acc.*
        FROM keeps k
        JOIN accounts acc ON acc.id = k.creatorId
        ;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }
      ).ToList();

    return keeps;
  }

  // internal List<Keep> GetMyKeeps(string userId)
  // {
  //   string sql = @"
  //       SELECT 
  //       k.*,
  //       acc.*
  //       FROM keeps k
  //       JOIN accounts acc ON acc.id = k.creatorId
  //       WHERE k.creatorId = @userID
  //       ;";

  //   List<Keep> keeps = _db.Query<Keep, Profile, Keep>(
  //     sql,
  //     (keep, profile) =>
  //     {
  //       keep.Creator = profile;
  //       return keep;
  //     }, new { userId }
  //     ).ToList();
  //   return keeps;
  // }

  internal List<Keep> GetUsersKeeps(string profileId)
  {
    string sql = @"
        SELECT 
        k.*,
        acc.*
        FROM keeps k
        JOIN accounts acc ON acc.id = k.creatorId
        WHERE k.creatorId = @profileId
        ;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { profileId }
    ).ToList();
    return keeps;
  }



  internal void RemoveKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";

    _db.Execute(sql, new { keepId });
  }

  internal Keep UpdateKeep(Keep originalKeep)
  {
    string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views,
        kept = @Kept
        WHERE id = @Id
        LIMIT 1
        ;";

    Keep updatedKeep = _db.QueryFirstOrDefault<Keep>(sql, originalKeep);

    return updatedKeep;
  }
}
