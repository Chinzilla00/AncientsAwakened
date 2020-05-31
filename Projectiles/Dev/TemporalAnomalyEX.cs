using AAMod.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Dev
{
    public class TemporalAnomalyEX : ModProjectile
    {
        private enum AffectType
        {
            Attract,
            Damage
        }

        public override string Texture => "AAMod/BlankTex";

        private const int AIStateSlot = 0;
        private const int ProgressSlot = 1;
        private const int StateJustSpawned = 0;
        private const int StateCreating = 1;
        private const int StateActive = 2;
        private const int attractRange = 896;
        private const int shrinkThreshold = 60;
        private const int shakeRange = 2048;
        private const int numBalls = 7;

        private const float singularitySize = 0.175f;
        private const float shine = 0.05f;
        private const float radiusDivisor = 3.5f;
        private const float pulseSpeedMult = 3;
        private const float pulseSizeMult = 0.025f;
        private const float armCoefficient = 2.9f;
        private const float armThreshold = 0.125f;
        private const float smoothness = 0.0035f;
        private const float rotationIncrement = 6.5f;
        private const float quietMusic = 0.15f;
        private const int dustDistance = 192;

        private static Filter BlackHole 
        {
            get => Filters.Scene["AAMod:TemporalAnomaly"];
            set => Filters.Scene["AAMod:TemporalAnomaly"] = value;
        }

        private float AIState
        {
            get => projectile.ai[AIStateSlot];
            set => projectile.ai[AIStateSlot] = value;
        }

        private float CreationProgress
        {
            get => projectile.ai[ProgressSlot];
            set => projectile.ai[ProgressSlot] = value;
        }

        private float uOpacity;
        private float totalSize;
        private float shakePower;
        private float light;
        private float musicVolume;
        private float rotationOffset;

        private SoundEffectInstance rumble;

        private int dustAlpha;

        public override void SetDefaults()
        {
            projectile.alpha = 255;
            projectile.Size = new Vector2(20);
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.timeLeft = 1800;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(uOpacity);
            writer.Write(totalSize);
            writer.Write(light);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            uOpacity = reader.ReadFloat();
            totalSize = reader.ReadFloat();
            light = reader.ReadFloat();
        }

        public override void AI()
        {
            projectile.netUpdate = true;

            switch (AIState)
            {
                case StateJustSpawned:

                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 velocity = new Vector2(-projectile.velocity.X * 0.4f, -projectile.velocity.Y * 0.4f).RotatedByRandom(MathHelper.ToRadians(10));
                        Color color = Color.Lerp(Color.Black, Color.Purple, Main.rand.NextFloat());
                        Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Smoke, velocity.X, velocity.Y, default, color);
                    }

                    if (projectile.timeLeft <= 1620)
                    {
                        InitialiseCreation();
                    }

                    break;

                case StateCreating:

                    if (CreationProgress == 0)
                    {
                        musicVolume = Main.musicVolume;
                        Main.musicVolume = quietMusic;

                        rumble = Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Rumble"), projectile.Center);
                        AAMod.activeRumbleSounds.Add(rumble);

                        dustAlpha = 255;
                    }

                    shakePower = CreationProgress / 60 * 4;

                    if (Main.LocalPlayer.DistanceSQ(projectile.Center) < shakeRange * shakeRange) // I used LocalPlayer so that the shake doesn't apply to clients that are too far away.
                    {
                        Main.LocalPlayer.GetModPlayer<ShakePlayer>().shakePower = shakePower;
                    }

                    CreationProgress++;

                    projectile.tileCollide = false;
                    projectile.velocity = Vector2.Zero;

                    for (int i = 0; i < 60; i++)
                    {
                        Vector2 offsetPos = projectile.Center + new Vector2(144 + ((float)Math.Sin(projectile.timeLeft / 8) + 1) * 52, 0).RotatedBy(MathHelper.ToRadians(i * 6));
                        Color color = Color.Lerp(Color.Black, Color.Purple, Main.rand.NextFloat());
                        Dust dust = Dust.NewDustPerfect(offsetPos, DustID.Smoke, Vector2.Zero, default, color, Main.rand.NextFloat(1f, 1.25f));
                        dust.noGravity = true;
                    }

                    if (CreationProgress >= 120)
                    { 
                        dustAlpha -= 8;

                        PartialClamp(ref dustAlpha, false);

                        rotationOffset += rotationIncrement;
                        float rotationPerBall = 360f / numBalls;
                        for (int i = 0; i < numBalls; i++)
                        {
                            Vector2 spawnPosition = projectile.Center + new Vector2(dustDistance + ((float)Math.Sin(projectile.timeLeft / 12) * 60), 0).RotatedBy(MathHelper.ToRadians((rotationPerBall * i) + rotationOffset));
                            Color color = Color.Lerp(Color.Black, Color.Purple, Main.rand.NextFloat());
                            for (int j = 0; j < 7; j++)
                            {
                                Dust.NewDust(spawnPosition, 4, 4, DustID.Smoke, 0, 0, dustAlpha, color);
                            }
                        }
                    }

                    if (CreationProgress >= 240 && Main.netMode != NetmodeID.Server && !BlackHole.IsActive())
                    {
                        light += 0.01f;
                    }

                    if (light >= 1f && CreationProgress >= 300 && Main.netMode != NetmodeID.Server)
                    {
                        if (!BlackHole.IsActive())
                        {
                            Filters.Scene.Activate("AAMod:TemporalAnomaly", projectile.Center).GetShader().UseOpacity(uOpacity)
                                .UseColor(1, 1, 1).UseProgress(totalSize).UseSecondaryColor(shine, radiusDivisor, 0)
                                .UseImageOffset(new Vector2(pulseSpeedMult, pulseSizeMult)).UseDirection(new Vector2(armCoefficient, 0)).UseIntensity(armThreshold);
                            BlackHole.GetShader().Shader.Parameters["smoothness"].SetValue(smoothness);
                            Main.PlaySound(SoundID.Zombie, -1, -1, 92);

                            AffectHostileNPCsAndPlayers(AffectType.Damage, projectile.damage * 10, false);
                        }
                        else
                        {
                            BlackHole.GetShader().UseOpacity(uOpacity).UseTargetPosition(projectile.Center)
                                .UseColor(1, 1, 1).UseProgress(totalSize).UseSecondaryColor(shine, radiusDivisor, 0)
                                .UseImageOffset(new Vector2(pulseSpeedMult, pulseSizeMult)).UseDirection(new Vector2(armCoefficient, 0)).UseIntensity(armThreshold);
                            BlackHole.GetShader().Shader.Parameters["smoothness"].SetValue(smoothness);
                        }

                        totalSize += 0.005f;
                        uOpacity += 0.01f;

                        PartialClamp(ref uOpacity, true);

                        if (totalSize > singularitySize)
                        {
                            totalSize = singularitySize;
                        }

                        if (uOpacity >= 1 && totalSize >= singularitySize)
                        {
                            light = 0;
                            AIState = StateActive;
                        }
                    }

                    MoonlordDeathDrama.RequestLight(light, projectile.Center);

                    break;

                case StateActive:

                    if (Main.musicVolume == quietMusic && musicVolume != 0)
                    {
                        Main.musicVolume = musicVolume;
                    }

                    if (rumble != null && rumble.State == SoundState.Playing)
                    {
                        rumble.Stop();
                        if (AAMod.activeRumbleSounds.Contains(rumble))
                        {
                            AAMod.activeRumbleSounds.Remove(rumble);
                        }
                    }

                    AffectHostileNPCsAndPlayers(AffectType.Attract, 0, false);

                    if (projectile.timeLeft % 8 == 0)
                    {
                        AffectHostileNPCsAndPlayers(AffectType.Damage, projectile.damage, true);
                    }

                    if (projectile.timeLeft <= shrinkThreshold)
                    {
                        totalSize -= 0.0015f;
                        uOpacity -= 0.016f;
                        projectile.damage *= projectile.timeLeft / shrinkThreshold;

                        if (Main.netMode != NetmodeID.Server)
                        {
                            BlackHole.GetShader().UseOpacity(uOpacity).UseTargetPosition(projectile.Center)
                                .UseColor(1, 1, 1).UseProgress(totalSize).UseSecondaryColor(shine, radiusDivisor, 0)
                                .UseImageOffset(new Vector2(pulseSpeedMult, pulseSizeMult)).UseDirection(new Vector2(armCoefficient, 0)).UseIntensity(armThreshold);
                            BlackHole.GetShader().Shader.Parameters["smoothness"].SetValue(smoothness);
                        }
                    }

                    break;
            }
        }

        private void AffectHostileNPCsAndPlayers(AffectType affectType, int damage, bool accountForDistance)
        {
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (player.active && player.whoAmI != projectile.owner && player.hostile && player.DistanceSQ(projectile.Center) < attractRange * attractRange)
                {
                    switch (affectType)
                    {
                        case AffectType.Attract:
                            player.velocity = player.DirectionTo(projectile.Center) * 16;
                            break;
                        case AffectType.Damage:
                            float proximity = (player.Center - projectile.Center).Length();
                            if (proximity != 0)
                            {
                                player.Hurt(PlayerDeathReason.ByCustomReason(string.Format("{0} became one with the void.", player.name)),
                                    accountForDistance ? (int)(damage * (20 / proximity)) : damage,
                                0, true);
                            }
                            break;
                    }
                }
            }
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy() && npc.DistanceSQ(projectile.Center) < attractRange * attractRange)
                {
                    switch (affectType)
                    {
                        case AffectType.Attract:
                            npc.velocity = npc.DirectionTo(projectile.Center) * 16;
                            break;
                        case AffectType.Damage:
                            float proximity = (npc.Center - projectile.Center).Length();
                            if (proximity != 0)
                            {
                                npc.StrikeNPCNoInteraction(accountForDistance ? (int)(damage * (20 / proximity)) : damage, 16f, 0);
                            }
                            break;
                    }
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            InitialiseCreation();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                BlackHole.Deactivate();
            }

            if (rumble != null && rumble.State == SoundState.Playing)
            {
                rumble.Stop();
            }
        }

        public override bool? CanHitNPC(NPC target) => false;

        public override bool CanHitPvp(Player target) => false;

        public static void LoadBlackHoleShader()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> tAnomaly = new Ref<Effect>(AAMod.instance.GetEffect("Effects/TemporalAnomaly"));
                BlackHole = new Filter(new ScreenShaderData(tAnomaly, "CreateAnomaly"), EffectPriority.VeryHigh);
                BlackHole.Load();
            }
        }

        private void InitialiseCreation()
        {
            AIState = StateCreating;
            projectile.Size = new Vector2(120);
            projectile.position -= new Vector2(50, 50);
        }

        private void PartialClamp(ref float input, bool upper) => input = upper ? Math.Min(input, 1) : Math.Max(input, 0);
        private void PartialClamp(ref int input, bool upper) => input = upper ? Math.Min(input, 1) : Math.Max(input, 0);
    }
}
