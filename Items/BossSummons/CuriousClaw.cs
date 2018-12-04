using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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

        public override bool UseItem(Player player)
        {
            SpawnBoss(player, "GripOfChaosBlue", "GripOfChaosRed");
            Main.NewText("The Grips of Chaos have awoken", new Color(175, 75, 255));
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The claw just sits there, limp in your hand.", Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType("GripOfChaosBlue")) || NPC.AnyNPCs(mod.NPCType("GripOfChaosRed")))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Grips are already here", Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                return false;
            }
            return true;
        }

        public void SpawnBoss(Player player, string name1, string name2)
        {
            if (Main.netMode != 1)
            {
                int bossType1 = mod.NPCType(name1);
                int bossType2 = mod.NPCType(name2);
                if (NPC.AnyNPCs(bossType1)) { return; } //don't spawn if there's already a boss!
                if (NPC.AnyNPCs(bossType2)) { return; }
                int npcID1 = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType1, 0);
                int npcID2 = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType2, 0);
                Main.npc[npcID1].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
                Main.npc[npcID1].netUpdate2 = true;
                Main.npc[npcID2].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
                Main.npc[npcID2].netUpdate2 = true;
            }
        }

        public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }
    }
}