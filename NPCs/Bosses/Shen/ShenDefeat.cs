using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenDefeat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discord's Defeat");
            Terraria.ID.NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.height = 100;
            npc.width = 444;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.alpha = 255;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        } 

        public override void AI()
        {
            if (npc.ai[1] > 240)
            {
                int i = AAWorld.downedShen ? 1 : 0;
                int nPc = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<ShenDeath>(), 0, i);
                Main.npc[nPc].netUpdate = true;
                npc.active = false;
                npc.netUpdate = true;
            }
            else
            {
                npc.ai[1]++;
                npc.ai[0]++;
                if (npc.ai[0] > 4)
                {
                    npc.ai[0] = 0;
                    Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 Pos = new Vector2(npc.position.X + Main.rand.Next(0, 444), npc.position.Y - Main.rand.Next(0, 100));
                        Projectile.NewProjectile(Pos, Vector2.Zero, mod.ProjectileType<ShenDeathBoom>(), 0, 0, Main.myPlayer, Main.rand.Next(3));
                    }
                }
            }
        }
    }
}