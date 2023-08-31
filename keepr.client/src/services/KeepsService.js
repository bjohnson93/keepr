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
    logger.log(res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
  }
  async getKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
  }
  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)
    logger.log('[NEW KEEP...]', res.data)
    const keep = new Keep(res.data)
    AppState.keeps.push(keep)
    AppState.myKeeps = AppState.keeps
  }

  async getUsersKeeps(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log('[GETTING USERS KEEPS...]', res.data)
    if (profileId == AppState.account.id) {
      AppState.myKeeps = res.data.map(k => new Keep(k))
    }
    AppState.usersKeeps = res.data.map(k => new Keep(k))
  }

  async deleteMyKeep(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`)
    AppState.keeps = AppState.keeps.filter(k => k.id != keepId)
  }
  // async getMyKeeps(profileId) {
  //   const res = await api.get(`api/profiles/${profileId}/keeps`)
  //   logger.log(res.data)
  //   AppState.myKeeps = res.data.map(k => new Keep(k))
  // }
}

export const keepsService = new KeepsService()