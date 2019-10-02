using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenSpawn : ModNPC
    {
        public override string Texture => "AAMod/NPCs/Bosses/Shen/ShenSpawn";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discord");
            Terraria.ID.NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.alpha = 255;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenIntro");
            npc.lifeMax = 1000000000;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10000000;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        int dustx = 50;

        public override void AI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            npc.Center = player.Center - new Vector2(0, 300f);
            npc.ai[0]++;
            if (npc.ai[0] <= 960)
            {
                for (int LOOP = 0; LOOP < 4; LOOP++)
                {
                    Dust dust1;
                    Dust dust2;
                    Vector2 position1 = new Vector2(npc.Center.X + dustx, npc.Center.Y);
                    Vector2 position2 = new Vector2(npc.Center.X - dustx, npc.Center.Y);
                    dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default, 1f)];
                    dust1.noGravity = false;
                    dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default, 1f)];
                    dust2.noGravity = true;
                    dust2.scale *= 1.3f;
                    dust2.velocity.Y -= 6;
                }
            }
            else if (npc.ai[0] > 960 && npc.ai[0] < 1640)
            {
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

            if (npc.ai[0] == 180)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenSpawn1"), new Color(180, 41, 32));
            }

            if (npc.ai[0] == 360)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenSpawn2"), new Color(45, 46, 70));
            }

            if (npc.ai[0] == 540)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenSpawn3"), new Color(180, 41, 32));
            }

            if (npc.ai[0] == 720)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenSpawn4"), new Color(45, 46, 70));
            }
            if (npc.ai[0] == 900)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenSpawn5"), new Color(180, 41, 32));
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("ShenSpawn5"), new Color(45, 46, 70));
            }

            if (dustx > 0 && npc.ai[0] >= 900)
            {
                dustx -= 1;
                if (dustx < 0)
                {
                    dustx = 0;
                }
            }

            if (npc.ai[0] == 960)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Heh....heheh...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (npc.ai[0] == 1140)
            {
                if (Main.netMode != 1) BaseUtility.Chat("You've made a grave mistake, child...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (npc.ai[0] == 1320)
            {
                if (Main.netMode != 1) BaseUtility.Chat("For you see....", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (npc.ai[0] >= 1500)
            {
                npc.alpha -= 5;
            }

            if (npc.ai[0] == 1680)
            {
                if (Main.netMode != 1) BaseUtility.Chat("I AM SHEN DORAGON, EMPEROR OF CHAOS AND ANARCHY!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            }

            if (npc.ai[0] == 1860)
            {
                if (Main.netMode != 1) BaseUtility.Chat("And you, my child...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            }

            if (npc.ai[0] >= 2040)
            {
                if (Main.netMode != 1) BaseUtility.Chat("WILL PERISH!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                SummonShen();
                npc.active = false;
            }
        }

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                npc.TargetClosest();
                Player player = Main.player[npc.target];
                npc.Center = player.Center - new Vector2(0, 300f); ;
                npc.ai[0]++;

                if (npc.ai[0] <= 960)
                {
                    for (int LOOP = 0; LOOP < 4; LOOP++)
                    {
                        Dust dust1;
                        Dust dust2;
                        Vector2 position1 = new Vector2(npc.Center.X + dustx, npc.Center.Y);
                        Vector2 position2 = new Vector2(npc.Center.X - dustx, npc.Center.Y);
                        dust1 = Main.dust[Dust.NewDust(position1, 1, 1, mod.DustType<Dusts.AkumaDust>(), 0, 0, 0, default, 1f)];
                        dust1.noGravity = false;
                        dust2 = Main.dust[Dust.NewDust(position2, 1, 1, mod.DustType<Dusts.YamataDust>(), 0, 0, 0, default, 1f)];
                        dust2.noGravity = true;
                        dust2.scale *= 1.3f;
                        dust2.velocity.Y -= 6;
                    }
                }
                else if (npc.ai[0] > 960 && npc.ai[0] < 1640)
                {
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

                if (npc.ai[0] >= 400)
                {
                    npc.alpha -= 5;
                }


                if (dustx > 0 && npc.ai[0] >= 900)
                {
                    dustx -= 1;
                    if (dustx < 0)
                    {
                        dustx = 0;
                    }
                }

                if (npc.ai[0] >= 600)
                {
                    SummonShen();
                    npc.active = false;
                    npc.netUpdate = true;
                }
                return false;
            }
            return true;
        }

        public void SummonShen()
        {
            AAModGlobalNPC.SpawnBoss(Main.player[npc.target], mod.NPCType("Shen"), false, npc.Center, "");
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
                BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, Color.White);
                BaseDrawing.DrawAura(sb, Main.npcTexture[npc.type], 0, npc, auraPercent, 1f, 0f, 0f, GetColorAlpha());
                BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, GetColorAlpha());
                return false;
            }
            return true;
        }
    }

    public class ShenDefeat : ModNPC
    {
        public override string Texture => "AAMod/NPCs/Bosses/Shen/ShenSpawn";
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
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<ShenDeath>(), 0, i);
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

    public class ShenDeath : ModNPC
    {
        public override string Texture => "AAMod/NPCs/Bosses/Shen/ShenSpawn";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discord's Death");
            Terraria.ID.NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.alpha = 255;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ChaosSissy");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                AAWorld.downedShen = true;
                npc.active = false;
                npc.netUpdate = true;
            }
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            npc.Center = player.Center;

            npc.ai[1]++;
            if (npc.ai[0] == 0)
            {
                if (npc.ai[1] == 180)
                {
                    BaseUtility.Chat("Split again...", new Color(180, 41, 32));
                }

                if (npc.ai[1] == 360)
                {
                    BaseUtility.Chat("This is YOUR fault you insolent worm..! I knew we should have been more aggressive but NOOOOOOOOO..! YOU said we could squash them without even trying!", new Color(45, 46, 70));
                }

                if (npc.ai[1] == 540)
                {
                    string Name = Main.netMode != 0 ? "Warriors" : player.name;
                    BaseUtility.Chat(Name + ", you will face our fury again one day...either when we gain enough power again...", new Color(180, 41, 32));
                }

                if (npc.ai[1] == 720)
                {
                    BaseUtility.Chat("...or you decide to use that Sigil again..!", new Color(45, 46, 70));
                }

                if (npc.ai[1] == 899)
                {
                    BaseUtility.Chat("Your choice, child.", new Color(45, 46, 70));
                    BaseUtility.Chat("Your choice, child.", new Color(180, 41, 32));
                }

                if (npc.ai[1] >= 900)
                {
                    AAWorld.downedShen = true;
                    npc.active = false;
                    npc.netUpdate = true;
                }
                return;
            }
            else
            {
                if (npc.ai[1] == 180)
                {
                    BaseUtility.Chat("YOU IMBECILE! WE LOST AGAAAAAAAAAAAAIN!!!", new Color(45, 46, 70));
                }

                if (npc.ai[1] == 360)
                {
                    BaseUtility.Chat("Rgh, my head...", new Color(180, 41, 32));
                }

                if (npc.ai[1] == 540)
                {
                    string Name = Main.netMode != 0 ? " BUNCH OF CLOWNS" : player.Male ? ", BOY" : ", GIRL";
                    BaseUtility.Chat("And YOU" + Name + "! NEXT TIME I'M GONNA SLIT YOUR HEADS OFF!!!", new Color(45, 46, 70));
                }

                if (npc.ai[1] == 720)
                {
                    BaseUtility.Chat("And trust us, kid.", new Color(180, 41, 32));
                }

                if (npc.ai[1] == 899)
                {
                    if (Main.netMode != 1)
                    {
                        BaseUtility.Chat("There's always a next time.", new Color(45, 46, 70));
                        BaseUtility.Chat("There's always a next time.", new Color(180, 41, 32));
                    }
                }
                if (npc.ai[1] >= 900)
                {
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
        }
    }

    public class ShenTransition : ModNPC
    {
        public override string Texture => "AAMod/NPCs/Bosses/Shen/ShenTransition";

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
            if (npc.alpha < 255 && npc.ai[0] > 375)
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
                    if (Main.netMode != 1) BaseUtility.Chat("Heh...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 475)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Heheheh...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 600)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("HAHAHAHAHAHAHA!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 820)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Have you forgotten about our last battles...?", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] == 960)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("There's always a last stand, kiddo...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    npc.netUpdate = true;
                }
                if (npc.ai[0] >= 1100)
                {
                    npc.alpha -= 5;
                }
                if (npc.ai[0] == 1100)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("But now, you stand in my path...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
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

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                npc.TargetClosest();
                Player player = Main.player[npc.target];
                npc.Center = player.Center - new Vector2(0, 300f); ;
                npc.ai[0]++;
                if (npc.alpha < 255 && npc.ai[0] > 200)
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

                if (npc.ai[0] >= 400)
                {
                    npc.alpha -= 5;
                }


                if (npc.ai[0] >= 600)
                {
                    SummonShen();
                    npc.active = false;
                    npc.netUpdate = true;
                }
                return false;
            }
            return true;
        }

        public void SummonShen()
        {
            Player player = Main.player[npc.target];
            if (Main.netMode != 1) BaseUtility.Chat("Shen Doragon has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            if (Main.netMode != 1) BaseUtility.Chat("SO YOU WILL BURN IN THE FLAMES OF DISCORDIAN HELL!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

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