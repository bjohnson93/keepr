import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /** @type {import('./models/Account.js').Account | null } */
  profile: null,

  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  /** @type {import('./models/Keep.js').Keep[] | null} */
  usersKeeps: [] | null,
  myKeeps: [] | null,
  /** @type {import('./models/Keep.js').Keep | null} */
  activeKeep: null,

  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [],
  /** @type {import('./models/Vault.js').Vault[] | null} */
  usersVaults: [] | null,
  /** @type {import('./models/Vault.js').Vault[] | null} */
  myVaults: [] | null,
  /** @type {import('./models/Vault.js').Vault | null} */
  activeVault: null,

  /** @type {import('./models/VaultKeep.js').VaultKeep[]} */
  vaultKeeps: [],
  /** @type {import('./models/VaultKeep.js').VaultKeep[]} */
  activeVaultKeep: null,

})
