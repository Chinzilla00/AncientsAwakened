using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.SoC;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class SpatialWheel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spatial Wheel");
            Tooltip.SetDefault(@"Unnerving energy radiates from this old ship's wheel");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 20;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(mod.NPCType<SoC>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The wheel doesn't do anything", Color.DarkCyan, false);
                return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            SpawnBoss(player, "SoC", "The Soul of Cthulhu");
            Main.NewText("The Soul of Cthulhu shreds through reality into this world", Color.DarkCyan);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-300f, 300f, (float)Main.rand.NextDouble()), 300f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }
    }
}