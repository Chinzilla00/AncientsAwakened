using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.Orthrus;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class ScrapHeap : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scrap Heap");
            Tooltip.SetDefault(@"A bunch of metal garbage
Summons the Orthrus X
Can only be used at night");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 20;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = true;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                return false;
            }
            if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
            {
                if (NPC.AnyNPCs(mod.NPCType<Orthrus>()))
                {
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType<Orthrus>()))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public override bool UseItem(Player player)
        {
            SpawnBoss(player, "Orthrus", "Orthrus X");
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
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

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "AbyssiumBar", 6);
                recipe.AddRecipeGroup("AAMod:Iron", 6);
                recipe.AddIngredient(null, "SoulOfSpite", 6);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
    }
}