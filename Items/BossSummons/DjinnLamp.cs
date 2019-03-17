using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
	//imported from my tAPI mod because I'm lazy
	public class DjinnLamp : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Lamp");
            Tooltip.SetDefault(@"Summons the Desert Djinn
Only usable during the day");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 26;
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
            SpawnBoss(player, "Djinn", "The Desert Djinn");
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The lamp shimmers in the moonlight, yet does nothing", Color.Goldenrod.R, Color.Goldenrod.G, Color.Goldenrod.B, false);
                return false;
            }
            if (!player.ZoneDesert)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The lamp spits out sand as you rub it", Color.Goldenrod.R, Color.Goldenrod.G, Color.Goldenrod.B, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType("Djinn")))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("No ammount of rubbing the lamp will save you here", Color.Goldenrod.R, Color.Goldenrod.G, Color.Goldenrod.B, false);
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
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-200f, 200f, (float)Main.rand.NextDouble()), 500);
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
                recipe.AddIngredient(null, "DesertMana", 3);
                recipe.AddIngredient(ItemID.Sandstone, 30);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
	}
}