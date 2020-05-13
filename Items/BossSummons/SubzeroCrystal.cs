using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class SubzeroCrystal : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero Crystal");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"Summons the Subzero Serpent");
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
            SpawnBoss(player, "SerpentHead", Language.GetTextValue("Mods.AAMod.Common.SubzeroSerpent"));
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneSnow)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.SubzeroCrystalSnowZoneFalse"), Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType("SerpentHead")))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.SubzeroCrystalFalse"), Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
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

                int type = 0;
                if (player.ZoneCorrupt)
                {
                    type = 1;
                }
                else if (player.ZoneCrimson)
                {
                    type = 2;
                }
                else if (player.GetModPlayer<AAPlayer>().ZoneInferno)
                {
                    type = 3;
                }
                else if (player.GetModPlayer<AAPlayer>().ZoneMire)
                {
                    type = 4;
                }
                else if (player.ZoneHoly)
                {
                    type = 5;
                }

                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0, 0, 0, type);
                Main.npc[npcID].ai[2] = type;
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), -1000);
                Main.npc[npcID].netUpdate2 = true;
                string npcName = !string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName;
                if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                else
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                    {
                        NetworkText.FromLiteral(npcName)
                    }), new Color(175, 75, 255), -1);
                }
            }
        }

        public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }

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