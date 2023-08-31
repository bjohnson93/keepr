import { Keep } from "./Keep.js"

export class VaultKeep extends Keep {
  constructor(data) {
    super(data)
    this.vaultId = data.vaultId
    this.keepId = data.keepId
    this.vaultKeepId = data.vaultKeepId
  }
}

// let data = {
//   "vaultId": 78,
//   "keepId": 101,
//   "vaultKeepId": 39,
  
 
//   "id": 101,
//   "createdAt": "2023-08-29T18:31:25",
//   "updatedAt": "2023-08-29T18:31:25"
// }