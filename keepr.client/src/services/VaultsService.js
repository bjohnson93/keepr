import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log('getting vault by id...', res.data)
    AppState.activeVault = new Vault(res.data)
  }

  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    logger.log('[CREATING A VAULT]', res.data)
    const vault = new Vault(res.data)
    AppState.myVaults.push(vault)
  }

  async getUsersVaults(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log('[GETTING THE USERs VAULTS]', res.data)
    AppState.usersVaults = res.data.map(v => new Vault(v))
  }


  async deleteMyVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    // AppState.usersVaults = AppState.usersVaults.filter(v => v.id != vaultId)
    AppState.myVaults = AppState.myVaults.filter(v => v.id != vaultId)
  }

  async getMyVaults() {
    const res = await api.get('account/vaults')
    logger.log('am i getting my vaults?', res.data)
    AppState.myVaults = res.data.map(v => new Vault(v))
  }
}

export const vaultsService = new VaultsService()