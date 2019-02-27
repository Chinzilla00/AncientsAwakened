using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
	//imported from my tAPI mod because I'm lazy
	public class SubzeroCrystal : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero Crystal");
            Tooltip.SetDefault(@"Summons the Subzero Serpent
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

        public override bool UseItem(Player player)
        {
            if (player.ZoneCrimson)
            {
                SpawnBoss(player, "SerpentHeadCr", "The Subzero Serpent");
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                return true;
            }
            if (player.ZoneCorrupt)
            {
                SpawnBoss(player, "SerpentHeadCo", "The Subzero Serpent");
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                return true;
            }
            if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
            {
                SpawnBoss(player, "SerpentHeadI", "The Subzero Serpent");
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                return true;
            }
            if (player.GetModPlayer<AAPlayer>(mod).ZoneMire)
            {
                SpawnBoss(player, "SerpentHeadM", "The Subzero Serpent");
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                return true;
            }
            SpawnBoss(player, "SerpentHead", "The Subzero Serpent");
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime )
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The crystal just sits there, melting in the sun", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
                return false;
            }
            if (!player.ZoneSnow)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The crystal shows an image of the nearby ice biome inside of it", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType("Serpent")))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Subzero Serpent continues to attack", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
                return false;
            }
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), -300f);
                Main.npc[npcID].netUpdate2 = true;
                string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName);
                if (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                else
                if (Main.netMode == 2)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                    {
                        NetworkText.FromLiteral(npcName)
                    }), new Color(175, 75, 255), -1);
                }
            }
        }

        public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "SnowMana", 3);
                recipe.AddIngredient(ItemID.IceBlock, 30);
                recipe.AddTile(TileID.IceMachine);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
	}
}