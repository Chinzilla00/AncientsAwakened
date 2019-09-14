using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenTransition : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Awakening");
            Terraria.ID.NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.alpha = 255;
            npc.lifeMax = 10000000;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            npc.Center = player.Center - new Vector2(0, 300f); ;
            npc.ai[0]++;
            if (npc.timeLeft <= 10)
            {
                npc.timeLeft = 10;
            }
            if (npc.alpha > 0 && npc.ai[0] > 375)
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
                for (int LOOP = 0; LOOP < 8; LOOP++)
                {
                    Dust dust1;
                    Vector2 position1 = npc.Center;
                    dust1 = Main.dust[Dust.NewDust(position1, 20, 20, mod.DustType<Dusts.Discord>(), 0, 0, 0, default, 1f)];
                    dust1.noGravity = false;
                    dust1.scale *= 1.3f;
                    dust1.velocity.Y -= 6;
                }
            }

            if (Main.netMode != 1)
            {
                if (npc.ai[0] == 375)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenTransition1"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 475)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenTransition2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 600)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenTransition3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 820)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenTransition4"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 960)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenTransition5"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] >= 1100)
                {
                    npc.alpha -= 5;
                }
                if (npc.ai[0] == 1100)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenTransition6"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] >= 1400)
                {
                    SummonShen();
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
            

        }

        public void SummonShen()
        {
            Player player = Main.player[npc.target];
            if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenTransition7"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenTransition8"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            int b = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ShockwaveBoom"), 0, 1, Main.myPlayer, 0, 0);
            Main.projectile[b].Center = npc.Center;


            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("ShenA"), false, npc.Center, "Shen Awakened", false);
        }

        public Color GetColorAlpha()
        {
            return new Color(233, 0, 233) * (Main.mouseTextColor / 255f);
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            if (npc.alpha <= 0)
            {
                BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor);
                BaseDrawing.DrawAura(sb, Main.npcTexture[npc.type], 0, npc, auraPercent, 1f, 0f, 0f, GetColorAlpha());
                BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, GetColorAlpha());
                return false;
            }

            return true;
        }
    }
}