using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Grips;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class CuriousClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Curious Looking Claw");
            Tooltip.SetDefault(@"It's strangely dry
Only usable at night");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 24;
            item.maxStack = 20;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DragonClaw", 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(mod.NPCType<GripOfChaosBlue>()) || NPC.AnyNPCs(mod.NPCType<GripOfChaosRed>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Grips of Chaos are already here!", Color.DarkOrange, false);
                return false;
            }
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The claw lays limp in your hand. Nasty.", Color.DarkOrange, false);
                return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("GripOfChaosBlue"), true, 1, 0, "The Grips of Chaos", true);
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("GripOfChaosRed"), false, -1, 0);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}