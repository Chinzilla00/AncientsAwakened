using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.Anubis;
using AAMod.NPCs.Bosses.Anubis.Forsaken;
using Terraria.ModLoader;

using Terraria.ID;

namespace AAMod.Items.BossSummons
{
    public class Scepter : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ra's Scepter");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"Summons Anubis
Can only be used in the desert on the surface
'I uh...borrowed this from a bird friend of mine.'");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.value = 0;
            item.rare = ItemRarityID.LightPurple;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.consumable = false;
        }

        public override bool UseItem(Player player)
        {
            if (!player.ZoneDesert && !player.ZoneUndergroundDesert)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ScepterBossFalse1"), Color.Gold, false);
                return true;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Anubis>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ScepterBossFalse2"), Color.Gold, false);
                return true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<FATransition>()) || NPC.AnyNPCs(ModContent.NPCType<FATransition2>()) || NPC.AnyNPCs(ModContent.NPCType<ForsakenAnubis>()))
            {
                return true;
            }



            int a = NPC.NewNPC((int)player.position.X + Main.rand.Next(-300, 300), (int)player.position.Y - 400, ModContent.NPCType<Anubis>());
            Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);

            NPC npc = Main.npc[a];

            Vector2 position = npc.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += npc.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = npc.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += npc.DirectionTo(Main.dust[num92].position) * 3f;
            }
            return true;
        }
    }
}