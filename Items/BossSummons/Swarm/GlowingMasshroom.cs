using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.MushroomMonarch;
using Terraria.ModLoader;
using Terraria.Localization;
using BaseMod;

namespace AAMod.Items.BossSummons.Swarm
{
    public class GlowingMasshroom : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Masshroom");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"Summons a lot of Feudal Fungi
Can only be used in glowing mushroom biomes");
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
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat("You are being assaulted by a bunch of fungis", Color.SkyBlue, false);

            for (int i = 0; i < 10; i++)
            {
                int boss = NPC.NewNPC((int)player.position.X + Main.rand.Next(-1000, 1000), (int)player.position.Y + Main.rand.Next(-1000, -400), mod.NPCType("Feudal Fungus"));
            }

            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneGlowshroom)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat("Stop waving a bunch of shrooms around in the middle of nowhere like a nutcase.",  Color.SkyBlue, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<MushroomMonarch>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ConfusingMushroomFalse2"), Color.SkyBlue, false);
                return false;
            }
            return true;
        }

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("Fargowiltas") != null;
        }

        private readonly Mod fargos = ModLoader.GetMod("Fargowiltas");

        public override void AddRecipes()
        {
            if (fargos != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ModContent.ItemType<ConfusingMushroom>(), 10);
                recipe.AddIngredient(fargos, "Overloader", 1);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
    }
}