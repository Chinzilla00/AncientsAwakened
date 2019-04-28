using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.Orthrus;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class ScrapHeap : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scrap Heap");
            Tooltip.SetDefault(@"A bunch of metal garbage
Summons the Orthrus X
Can only be used at night");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 20;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 500;
            item.consumable = true;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(mod, player, "Orthrus");
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("You feel a static shock from using this. Maybe it's trying to send a signal?", Color.Purple.R, Color.Purple.G, Color.Purple.B, false);
                if (Main.netMode == 0)
                {
                    player.statLife -= 1;
                }
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType("Orthrus")))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Orthrus wants to eat that AND you", Color.Purple.R, Color.Purple.G, Color.Purple.B, false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "AbyssiumBar", 6);
                recipe.AddRecipeGroup("AAMod:Iron", 6);
                recipe.AddIngredient(null, "SoulOfSpite", 6);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
    }
}