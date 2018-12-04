using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Daybringer;
using AAMod.NPCs.Bosses.Nightcrawler;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class EquinoxWorm : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equinox Worm");
            Tooltip.SetDefault(@"Brings forth the serpents of the celestial heavens
Not Consumable");
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
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MechanicalWorm, 2);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            SpawnBoss(player, "Daybringer", "Nightcrawler");
            Main.NewText("The Grips of Chaos have awoken", new Color(Main.DiscoR, 125, Main.DiscoB));
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(mod.NPCType("Daybringer")) || NPC.AnyNPCs(mod.NPCType("Nightcrawler")))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Equinox Worms are already here", (byte)Main.DiscoR, 125, (byte)Main.DiscoB, false);
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