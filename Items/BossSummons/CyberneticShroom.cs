using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.Truffle;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    public class CyberneticShroom : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cybernetic Shroom");
            Tooltip.SetDefault(@"Summons the Techno Truffle
Can only be used at night");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 20;
            item.value = 1000;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("TechnoTruffle"), true, 0, 0, "The Techno Truffle", false);
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Stop waving that metal mushroom around like a psychopath.", new Color(216, 110, 40), false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType<TechnoTruffle>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Techno Truffle exists.", new Color(216, 110, 40), false);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ConfusingMushroom");
                recipe.AddIngredient(ItemID.IronBar, 6);
                recipe.AddIngredient(null, "SoulOfSmite", 3);
                recipe.AddIngredient(null, "SoulOfSpite", 3);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "IntimidatingMushroom");
                recipe.AddIngredient(ItemID.IronBar, 6);
                recipe.AddIngredient(null, "SoulOfSmite", 3);
                recipe.AddIngredient(null, "SoulOfSpite", 3);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
    }
}