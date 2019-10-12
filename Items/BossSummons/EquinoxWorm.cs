using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Equinox;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace AAMod.Items.BossSummons
{
    public class EquinoxWorm : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equinox Worm");
            Tooltip.SetDefault(@"A worm created using celestial materials
Summons the Equinox Worms
Non-Consumable");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.rare = 11;
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<NightcrawlerHead>()) && !NPC.AnyNPCs(ModContent.NPCType<DaybringerHead>());
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != 1) BaseMod.BaseUtility.Chat("The Equinox Worms have awoken!", 175, 75, 255, false); }
            else if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("The Equinox Worms have awoken!"), new Color(175, 75, 255), -1);
            }
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("DaybringerHead"), false, 0, 0);
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("NightcrawlerHead"), false, 0, 0);			
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MechanicalWorm, 2);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}