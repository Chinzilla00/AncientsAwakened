using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class ShroomGrub : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroom Grub");
            Tooltip.SetDefault(@"An overcharged truffle worm
Summons Emperor Fishron
Can only be used at the beach");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 24;
            item.maxStack = 20;
            item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            SpawnBoss(player, "EmperorFishron", "Emperor Fishron");
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneBeach)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("You wiggle the worm around like a weirdo. Stop it.", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B, false);
                return true;
            }
            if (NPC.AnyNPCs(mod.NPCType("EmperorFishron")))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Emperor Fishron wants to eat you, not the worm", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B, false);
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

        public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }
    }
}