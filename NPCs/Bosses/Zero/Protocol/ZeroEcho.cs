using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Zero.Protocol
{
    public class ZeroEcho : ModNPC
    {
        public override string Texture => "AAMod/NPCs/Bosses/Zero/Protocol/ZeroProtocol";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("3CH0");
            Main.npcFrameCount[npc.type] = 7; 
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 5000;
            npc.damage = 150;
            npc.defense = 70;
            npc.knockBackResist = 0f;
            npc.width = 146;
            npc.height = 152;
            npc.friendly = false;
            npc.aiStyle = -1;
            npc.value = Item.sellPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/Sounds/Zerohit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/ZeroDeath");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = (int)(npc.damage * .7f);
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return AAColor.Oblivion;
        }

        int body = -1;

        public override void AI()
        {
            npc.TargetClosest();
            AliveCheck(Main.player[npc.target]);

            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("ZeroProtocol"), 1000, null);
                if (npcID >= 0) body = npcID;
            }

            if (body == -1) return;

            NPC zero = Main.npc[body];
            if (zero == null || zero.life <= 0 || !zero.active || zero.type != mod.NPCType("ZeroProtocol")) { npc.active = false; return; }

            if (zero.ai[0] == 2)
            {
                npc.ai[0]++;
                if (npc.ai[0] == 60)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(10, 10), mod.ProjectileType("EchoRay"), npc.damage / 4, 0f, Main.myPlayer, 0, npc.whoAmI);
                    Projectile.NewProjectile(npc.Center, new Vector2(-10, 10), mod.ProjectileType("EchoRay"), npc.damage / 4, 0f, Main.myPlayer, 0, npc.whoAmI);
                    Projectile.NewProjectile(npc.Center, new Vector2(10, -10), mod.ProjectileType("EchoRay"), npc.damage / 4, 0f, Main.myPlayer, 0, npc.whoAmI);
                    Projectile.NewProjectile(npc.Center, new Vector2(-10, -10), mod.ProjectileType("EchoRay"), npc.damage / 4, 0f, Main.myPlayer, 0, npc.whoAmI);
                }
            }
            
        }

        public bool AliveCheck(Player player)
        {
            bool tooFar = Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 10000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 10000f;
            if (player.dead || tooFar)
            {
                npc.TargetClosest(true);

                if (Main.player[npc.target].dead || tooFar)
                {
                    npc.StrikeNPCNoInteraction(999999, 0, 0);
                    return false;
                }
            }
            return true;
        }

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ > 10)
            {
                npc.frameCounter = 0;
                Frame += 1;
            }

            if (Frame > 6)
            {
                Frame = 0;
            }

            npc.frame.Y = frameHeight * Frame;
        }
    }
}
