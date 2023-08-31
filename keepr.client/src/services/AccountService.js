import { AppState } from '../AppState'
import { Account, Profile } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async editAccount(accountData) {
    try {
      const res = await api.put('/account', accountData)
      logger.log('[EDIT ACCT?]', res.data)
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error(err.message)
    }
  }

}

export const accountService = new AccountService()
