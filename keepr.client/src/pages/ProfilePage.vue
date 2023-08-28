<template>
  <h1>Profile Page</h1>
</template>


<script>
import { onMounted } from "vue";
import { vaultsService } from "../services/VaultsService.js"
import Pop from "../utils/Pop.js";

export default {
  setup(){
    onMounted(() => {
      // getVaultById();
    })
    // Will use this function to open a vault?
    // async function getVaultById(){
    //   try 
    //   {

    //    await vaultsService.getVaultById(vaultId);
    //   }
    //   catch (error)
    //   {
    //     Pop.error(error.message)
    //   }
    // }
    // guessing I would make this sort of function to get vaults for a profile.... should probably go create in the backend first!
//     async function getVaultsByProfileId(){
//       try 
//       {
      
//       }
//       catch (error)
//       {
//         Pop.error(error.message)
//       }
//     }
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>