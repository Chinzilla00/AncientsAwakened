using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.MushroomMonarch;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    public class ConfusingMushroom : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Confusing Looking Mushroom");
            Tooltip.SetDefault(@"Summons the Feudal Fungus
Can only be used in a glowing mushroom biome or at night");
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
            SpawnBoss(player, NPC.downedPlantBoss ? "FeudalFungusA" : "FeudalFungus", "The Feudal Fungus");
            if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Feudal Fungus appears!", Color.SkyBlue, false);
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if ((!Main.dayTime && player.position.Y < Main.worldSurface) || player.ZoneGlowshroom)
            {
                return true;
            }
            if ((Main.dayTime && player.position.Y > Main.worldSurface) || !player.ZoneGlowshroom)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The mushroom glows, and the smell of it makes you feel loopy.", Color.SkyBlue, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType<FeudalFungus>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Feudal Fungus keeps trying to attack you", Color.SkyBlue, false);
                return false;
            }
            return false;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlowingMushroom, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}