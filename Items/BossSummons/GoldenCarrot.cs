using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Rajah;
using AAMod.NPCs.Bosses.AH.Haruka;
using System.Collections.Generic;
using BaseMod;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace AAMod.Items.BossSummons
{
    public class GoldenCarrot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ten Karat Carrot");
            Tooltip.SetDefault(@"Summons the Pouncing Punisher himself");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.noUseGraphic = true;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType<Rajah>());
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("Those who slaughter the innocent must be PUNISHED!", 107, 137, 179);
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), player.Center);
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("Rajah"), false, 0, 0, "");
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddIngredient(ItemID.BunnyBanner, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}