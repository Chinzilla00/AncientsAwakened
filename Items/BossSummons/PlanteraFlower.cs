using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    public class PlanteraFlower : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Orchid");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"Summons Plantera
Can only be used in the underground jungle");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 20;
            item.value = 1000;
            item.rare = ItemRarityID.LightPurple;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            Globals.AAModGlobalNPC.SpawnBoss(player, NPCID.Plantera, true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.Plantera"), false);
            Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ZoneJungle && player.Center.Y >= (Main.worldSurface * 16) + 800f && !NPC.AnyNPCs(NPCID.Plantera))
            {
                return true;
            }
            return false;
        }

        public void SpawnBoss(Player player, int npc, string displayName)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = npc;
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
                string npcName = !string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName;
                if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
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
                recipe.AddIngredient(ItemID.JungleSpores, 10);
                recipe.AddIngredient(ItemID.SoulofFright, 5);
                recipe.AddIngredient(ItemID.SoulofMight, 5);
                recipe.AddIngredient(ItemID.SoulofSight, 5);
                recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(mod);

                recipe.AddIngredient(mod, "PlanteraPetal", 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
    }
}