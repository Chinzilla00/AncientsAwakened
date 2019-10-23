using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.Anubis;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    public class Scepter : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ra's Scepter");
            Tooltip.SetDefault(@"Summons Anubis
Can only be used in the desert on the surface
'I uh...borrowed this from a bird friend of mine.'");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.value = 0;
            item.rare = 6;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 1;
            item.noMelee = true;
            item.consumable = false;
        }

        public override bool UseItem(Player player)
        {
            NPC.NewNPC((int)player.position.X + Main.rand.Next(-300, 300), (int)player.position.Y - 400, ModContent.NPCType<Anubis>());
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneDesert || player.ZoneUndergroundDesert)
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("You wave the staff around like a psychopath. Nothing happens.", Color.Gold, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Anubis>()))
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("Uh...what are you doing?", Color.Gold, false);
                return false;
            }
            return true;
        }
    }
}