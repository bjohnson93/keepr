import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {

  async setActiveVaultKeep(vaultKeep) {
    AppState.activeVaultKeep = vaultKeep
  }

  // async getVaultKeepById(vaultKeepId){
  //   const res = await api.get(`api/`)
  // }
  async getKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('[Getting the keeps for this vault?]', res.data)
    AppState.vaultKeeps = res.data.map(vk => new VaultKeep(vk))

  }

  async createVaultKeep(vaultKeepData) {
    const res = await api.post('api/vaultKeeps', vaultKeepData)
    // FIXME look at your response data, it might not fit your model... try not doing anything with the response data maybe **fixed?**
    logger.log('[ADDING A VAULT KEEP?]', res.data)
    // AppState.vaultKeeps.push(new VaultKeep(res.data))

  }
}

export const vaultKeepsService = new VaultKeepsService()