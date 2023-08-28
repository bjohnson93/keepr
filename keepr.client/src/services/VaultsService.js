import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log('getting vault by id...', res.data)
  }

  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    logger.log('[CREATING A VAULT]', res.data)
    const vault = new Vault(res.data)
    AppState.vaults.push(vault)
  }
}

export const vaultsService = new VaultsService()