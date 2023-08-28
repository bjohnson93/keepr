import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {

  async setActiveKeep(keep) {
    AppState.activeKeep = keep
  }
  async getKeeps() {
    const res = await api.get('api/keeps')
    // logger.log(res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)
    logger.log('[NEW KEEP...]', res.data)
    const keep = new Keep(res.data)
    AppState.keeps.push(keep)
  }
}

export const keepsService = new KeepsService()