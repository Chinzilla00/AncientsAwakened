using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class ArmAIs
    {
        public static void Shredder(Mod mod, NPC npc, Player player, int Zero)
        {
            NPC zero = Main.npc[Zero];
            Vector2 vector46 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num448 = zero.position.X + zero.width / 2 - 200f * npc.ai[0] - vector46.X;
            float num449 = zero.position.Y + 230f - vector46.Y;
            float num450 = (float)Math.Sqrt(num448 * num448 + num449 * num449);
            if (npc.ai[2] != 99f)
            {
                if (num450 > 800f)
                {
                    npc.ai[2] = 99f;
                }
            }
            else if (num450 < 400f)
            {
                npc.ai[2] = 0f;
            }
            npc.spriteDirection = -(int)npc.ai[0];
            if (!zero.active || zero.type != mod.NPCType<Zero>())
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50f || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
            if (npc.ai[2] == 99f)
            {
                if (npc.position.Y > zero.position.Y)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.96f;
                    }
                    npc.velocity.Y -= 0.1f;
                    if (npc.velocity.Y > 8f)
                    {
                        npc.velocity.Y = 8f;
                    }
                }
                else if (npc.position.Y < zero.position.Y)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y *= 0.96f;
                    }
                    npc.velocity.Y += 0.1f;
                    if (npc.velocity.Y < -8f)
                    {
                        npc.velocity.Y = -8f;
                    }
                }
                if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X *= 0.96f;
                    }
                    npc.velocity.X -= 0.5f;
                    if (npc.velocity.X > 12f)
                    {
                        npc.velocity.X = 12f;
                    }
                }
                if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X *= 0.96f;
                    }
                    npc.velocity.X += 0.5f;
                    if (npc.velocity.X < -12f)
                    {
                        npc.velocity.X = -12f;
                        return;
                    }
                }
            }
            else
            {
                if (npc.ai[2] == 0f || npc.ai[2] == 3f)
                {
                    if (zero.ai[1] == 3f && npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    if (zero.ai[1] != 0f)
                    {
                        npc.TargetClosest(true);
                        if (player.dead)
                        {
                            npc.velocity.Y += 0.1f;
                            if (npc.velocity.Y > 16f)
                            {
                                npc.velocity.Y = 16f;
                            }
                        }
                        else
                        {
                            Vector2 vector47 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            float num451 = player.position.X + player.width / 2 - vector47.X;
                            float num452 = player.position.Y + player.height / 2 - vector47.Y;
                            float num453 = (float)Math.Sqrt(num451 * num451 + num452 * num452);
                            num453 = 7f / num453;
                            num451 *= num453;
                            num452 *= num453;
                            npc.rotation = (float)Math.Atan2(num452, num451) - 1.57f;
                            if (npc.velocity.X > num451)
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X *= 0.97f;
                                }
                                npc.velocity.X -= 0.05f;
                            }
                            if (npc.velocity.X < num451)
                            {
                                if (npc.velocity.X < 0f)
                                {
                                    npc.velocity.X *= 0.97f;
                                }
                                npc.velocity.X += 0.05f;
                            }
                            if (npc.velocity.Y > num452)
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y *= 0.97f;
                                }
                                npc.velocity.Y -= 0.05f;
                            }
                            if (npc.velocity.Y < num452)
                            {
                                if (npc.velocity.Y < 0f)
                                {
                                    npc.velocity.Y *= 0.97f;
                                }
                                npc.velocity.Y += 0.05f;
                            }
                        }
                        npc.ai[3] += 1f;
                        if (npc.ai[3] >= 600f)
                        {
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                    }
                    else
                    {
                        npc.ai[3] += 1f;
                        if (npc.ai[3] >= 300f)
                        {
                            npc.ai[2] += 1f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        if (npc.position.Y > zero.position.Y + 320f)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y *= 0.96f;
                            }
                            npc.velocity.Y -= 0.04f;
                            if (npc.velocity.Y > 3f)
                            {
                                npc.velocity.Y = 3f;
                            }
                        }
                        else if (npc.position.Y < zero.position.Y + 260f)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y *= 0.96f;
                            }
                            npc.velocity.Y += 0.04f;
                            if (npc.velocity.Y < -3f)
                            {
                                npc.velocity.Y = -3f;
                            }
                        }
                        if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X *= 0.96f;
                            }
                            npc.velocity.X -= 0.3f;
                            if (npc.velocity.X > 12f)
                            {
                                npc.velocity.X = 12f;
                            }
                        }
                        if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 - 250f)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X *= 0.96f;
                            }
                            npc.velocity.X += 0.3f;
                            if (npc.velocity.X < -12f)
                            {
                                npc.velocity.X = -12f;
                            }
                        }
                    }
                    Vector2 vector48 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num454 = zero.position.X + zero.width / 2 - 200f * npc.ai[0] - vector48.X;
                    float num455 = zero.position.Y + 230f - vector48.Y;
                    Math.Sqrt(num454 * num454 + num455 * num455);
                    npc.rotation = (float)Math.Atan2(num455, num454) + 1.57f;
                    return;
                }
                if (npc.ai[2] == 1f)
                {
                    Vector2 vector49 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num456 = zero.position.X + zero.width / 2 - 200f * npc.ai[0] - vector49.X;
                    float num457 = zero.position.Y + 230f - vector49.Y;
                    npc.rotation = (float)Math.Atan2(num457, num456) + 1.57f;
                    npc.velocity.X *= 0.95f;
                    npc.velocity.Y -= 0.1f;
                    if (npc.velocity.Y < -8f)
                    {
                        npc.velocity.Y = -8f;
                    }
                    if (npc.position.Y < zero.position.Y - 200f)
                    {
                        npc.TargetClosest(true);
                        npc.ai[2] = 2f;
                        vector49 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        num456 = player.position.X + player.width / 2 - vector49.X;
                        num457 = player.position.Y + player.height / 2 - vector49.Y;
                        float num458 = (float)Math.Sqrt(num456 * num456 + num457 * num457);
                        num458 = 22f / num458;
                        npc.velocity.X = num456 * num458;
                        npc.velocity.Y = num457 * num458;
                        npc.netUpdate = true;
                        return;
                    }
                }
                else if (npc.ai[2] == 2f)
                {
                    if (npc.position.Y > player.position.Y || npc.velocity.Y < 0f)
                    {
                        npc.ai[2] = 3f;
                        return;
                    }
                }
                else
                {
                    if (npc.ai[2] == 4f)
                    {
                        npc.TargetClosest(true);
                        Vector2 vector50 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num459 = player.position.X + player.width / 2 - vector50.X;
                        float num460 = player.position.Y + player.height / 2 - vector50.Y;
                        float num461 = (float)Math.Sqrt(num459 * num459 + num460 * num460);
                        num461 = 7f / num461;
                        num459 *= num461;
                        num460 *= num461;
                        if (npc.velocity.X > num459)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X *= 0.97f;
                            }
                            npc.velocity.X -= 0.05f;
                        }
                        if (npc.velocity.X < num459)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X *= 0.97f;
                            }
                            npc.velocity.X += 0.05f;
                        }
                        if (npc.velocity.Y > num460)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y *= 0.97f;
                            }
                            npc.velocity.Y -= 0.05f;
                        }
                        if (npc.velocity.Y < num460)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y *= 0.97f;
                            }
                            npc.velocity.Y += 0.05f;
                        }
                        npc.ai[3] += 1f;
                        if (npc.ai[3] >= 600f)
                        {
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        vector50 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        num459 = zero.position.X + zero.width / 2 - 200f * npc.ai[0] - vector50.X;
                        num460 = zero.position.Y + 230f - vector50.Y;
                        npc.rotation = (float)Math.Atan2(num460, num459) + 1.57f;
                        return;
                    }
                    if (npc.ai[2] == 5f && ((npc.velocity.X > 0f && npc.position.X + npc.width / 2 > player.position.X + player.width / 2) || (npc.velocity.X < 0f && npc.position.X + npc.width / 2 < player.position.X + player.width / 2)))
                    {
                        npc.ai[2] = 0f;
                        return;
                    }
                }
            }
        }

        public static void Taser(Mod mod, NPC npc, Player player, int Zero)
        {
            NPC zero = Main.npc[Zero];
            npc.spriteDirection = -(int)npc.ai[0];
            Vector2 vector51 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num462 = zero.position.X + zero.width / 2 - 200f * npc.ai[0] - vector51.X;
            float num463 = zero.position.Y + 230f - vector51.Y;
            float num464 = (float)Math.Sqrt(num462 * num462 + num463 * num463);
            if (npc.ai[2] != 99f)
            {
                if (num464 > 800f)
                {
                    npc.ai[2] = 99f;
                }
            }
            else if (num464 < 400f)
            {
                npc.ai[2] = 0f;
            }
            if (!zero.active || zero.type != mod.NPCType<Zero>())
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50f || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
            if (npc.ai[2] == 99f)
            {
                if (npc.position.Y > zero.position.Y)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.96f;
                    }
                    npc.velocity.Y -= 0.1f;
                    if (npc.velocity.Y > 8f)
                    {
                        npc.velocity.Y = 8f;
                    }
                }
                else if (npc.position.Y < zero.position.Y)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y *= 0.96f;
                    }
                    npc.velocity.Y += 0.1f;
                    if (npc.velocity.Y < -8f)
                    {
                        npc.velocity.Y = -8f;
                    }
                }
                if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X *= 0.96f;
                    }
                    npc.velocity.X -= 0.5f;
                    if (npc.velocity.X > 12f)
                    {
                        npc.velocity.X = 12f;
                    }
                }
                if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X *= 0.96f;
                    }
                    npc.velocity.X += 0.5f;
                    if (npc.velocity.X < -12f)
                    {
                        npc.velocity.X = -12f;
                        return;
                    }
                }
            }
            else
            {
                if (npc.ai[2] == 0f || npc.ai[2] == 3f)
                {
                    if (zero.ai[1] == 3f && npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    if (zero.ai[1] != 0f)
                    {
                        npc.TargetClosest(true);
                        npc.TargetClosest(true);
                        if (player.dead)
                        {
                            npc.velocity.Y += 0.1f;
                            if (npc.velocity.Y > 16f)
                            {
                                npc.velocity.Y = 16f;
                            }
                        }
                        else
                        {
                            Vector2 vector52 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            float num465 = player.position.X + player.width / 2 - vector52.X;
                            float num466 = player.position.Y + player.height / 2 - vector52.Y;
                            float num467 = (float)Math.Sqrt(num465 * num465 + num466 * num466);
                            num467 = 12f / num467;
                            num465 *= num467;
                            num466 *= num467;
                            npc.rotation = (float)Math.Atan2(num466, num465) - 1.57f;
                            if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < 2f)
                            {
                                npc.rotation = (float)Math.Atan2(num466, num465) - 1.57f;
                                npc.velocity.X = num465;
                                npc.velocity.Y = num466;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                npc.velocity *= 0.97f;
                            }
                            npc.ai[3] += 1f;
                            if (npc.ai[3] >= 600f)
                            {
                                npc.ai[2] = 0f;
                                npc.ai[3] = 0f;
                                npc.netUpdate = true;
                            }
                        }
                    }
                    else
                    {
                        npc.ai[3] += 1f;
                        if (npc.ai[3] >= 600f)
                        {
                            npc.ai[2] += 1f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        if (npc.position.Y > zero.position.Y + 300f)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y *= 0.96f;
                            }
                            npc.velocity.Y -= 0.1f;
                            if (npc.velocity.Y > 3f)
                            {
                                npc.velocity.Y = 3f;
                            }
                        }
                        else if (npc.position.Y < zero.position.Y + 230f)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y *= 0.96f;
                            }
                            npc.velocity.Y += 0.1f;
                            if (npc.velocity.Y < -3f)
                            {
                                npc.velocity.Y = -3f;
                            }
                        }
                        if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 + 250f)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X *= 0.94f;
                            }
                            npc.velocity.X -= 0.3f;
                            if (npc.velocity.X > 9f)
                            {
                                npc.velocity.X = 9f;
                            }
                        }
                        if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X *= 0.94f;
                            }
                            npc.velocity.X += 0.2f;
                            if (npc.velocity.X < -8f)
                            {
                                npc.velocity.X = -8f;
                            }
                        }
                    }
                    Vector2 vector53 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num468 = zero.position.X + zero.width / 2 - 200f * npc.ai[0] - vector53.X;
                    float num469 = zero.position.Y + 230f - vector53.Y;
                    Math.Sqrt(num468 * num468 + num469 * num469);
                    npc.rotation = (float)Math.Atan2(num469, num468) + 1.57f;
                    return;
                }
                if (npc.ai[2] == 1f)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.9f;
                    }
                    Vector2 vector54 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num470 = zero.position.X + zero.width / 2 - 280f * npc.ai[0] - vector54.X;
                    float num471 = zero.position.Y + 230f - vector54.Y;
                    npc.rotation = (float)Math.Atan2(num471, num470) + 1.57f;
                    npc.velocity.X = (npc.velocity.X * 5f + zero.velocity.X) / 6f;
                    npc.velocity.X += 0.5f;
                    npc.velocity.Y -= 0.5f;
                    if (npc.velocity.Y < -9f)
                    {
                        npc.velocity.Y = -9f;
                    }
                    if (npc.position.Y < zero.position.Y - 280f)
                    {
                        npc.TargetClosest(true);
                        npc.ai[2] = 2f;
                        vector54 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        num470 = player.position.X + player.width / 2 - vector54.X;
                        num471 = player.position.Y + player.height / 2 - vector54.Y;
                        float num472 = (float)Math.Sqrt(num470 * num470 + num471 * num471);
                        num472 = 20f / num472;
                        npc.velocity.X = num470 * num472;
                        npc.velocity.Y = num471 * num472;
                        npc.netUpdate = true;
                        return;
                    }
                }
                else if (npc.ai[2] == 2f)
                {
                    if (npc.position.Y > player.position.Y || npc.velocity.Y < 0f)
                    {
                        if (npc.ai[3] >= 4f)
                        {
                            npc.ai[2] = 3f;
                            npc.ai[3] = 0f;
                            return;
                        }
                        npc.ai[2] = 1f;
                        npc.ai[3] += 1f;
                        return;
                    }
                }
                else if (npc.ai[2] == 4f)
                {
                    Vector2 vector55 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num473 = zero.position.X + zero.width / 2 - 200f * npc.ai[0] - vector55.X;
                    float num474 = zero.position.Y + 230f - vector55.Y;
                    npc.rotation = (float)Math.Atan2(num474, num473) + 1.57f;
                    npc.velocity.Y = (npc.velocity.Y * 5f + zero.velocity.Y) / 6f;
                    npc.velocity.X += 0.5f;
                    if (npc.velocity.X > 12f)
                    {
                        npc.velocity.X = 12f;
                    }
                    if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 - 500f || npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 + 500f)
                    {
                        npc.TargetClosest(true);
                        npc.ai[2] = 5f;
                        vector55 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        num473 = player.position.X + player.width / 2 - vector55.X;
                        num474 = player.position.Y + player.height / 2 - vector55.Y;
                        float num475 = (float)Math.Sqrt(num473 * num473 + num474 * num474);
                        num475 = 17f / num475;
                        npc.velocity.X = num473 * num475;
                        npc.velocity.Y = num474 * num475;
                        npc.netUpdate = true;
                        return;
                    }
                }
                else if (npc.ai[2] == 5f && npc.position.X + npc.width / 2 < player.position.X + player.width / 2 - 100f)
                {
                    if (npc.ai[3] >= 4f)
                    {
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        return;
                    }
                    npc.ai[2] = 4f;
                    npc.ai[3] += 1f;
                    return;
                }
            }
        }

        public static void Cannon(Mod mod, NPC npc, Player player, int Zero, int Proj, int Damage, float ProjTimer)
        {
            NPC zero = Main.npc[Zero];
            npc.spriteDirection = -(int)npc.ai[0];
            if (!zero.active || zero.type != mod.NPCType<Zero>())
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50f || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
            if (npc.ai[2] == 0f)
            {
                if (zero.ai[1] == 3f && npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (zero.ai[1] != 0f)
                {
                    ProjTimer += 2f;
                    if (npc.position.Y > zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y -= 0.07f;
                        if (npc.velocity.Y > 6f)
                        {
                            npc.velocity.Y = 6f;
                        }
                    }
                    else if (npc.position.Y < zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y += 0.07f;
                        if (npc.velocity.Y < -6f)
                        {
                            npc.velocity.Y = -6f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 - 120f * npc.ai[0])
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X -= 0.1f;
                        if (npc.velocity.X > 8f)
                        {
                            npc.velocity.X = 8f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 - 120f * npc.ai[0])
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X += 0.1f;
                        if (npc.velocity.X < -8f)
                        {
                            npc.velocity.X = -8f;
                        }
                    }
                }
                else
                {
                    npc.ai[3] += 1f;
                    if (npc.ai[3] >= 1100f)
                    {
                        ProjTimer = 0f;
                        npc.ai[2] = 1f;
                        npc.ai[3] = 0f;
                        npc.netUpdate = true;
                    }
                    if (npc.position.Y > zero.position.Y - 150f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y -= 0.04f;
                        if (npc.velocity.Y > 3f)
                        {
                            npc.velocity.Y = 3f;
                        }
                    }
                    else if (npc.position.Y < zero.position.Y - 150f)
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y += 0.04f;
                        if (npc.velocity.Y < -3f)
                        {
                            npc.velocity.Y = -3f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 + 200f)
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X -= 0.2f;
                        if (npc.velocity.X > 8f)
                        {
                            npc.velocity.X = 8f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 + 160f)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X += 0.2f;
                        if (npc.velocity.X < -8f)
                        {
                            npc.velocity.X = -8f;
                        }
                    }
                }
                Vector2 vector56 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num476 = zero.position.X + zero.width / 2 - 200f * npc.ai[0] - vector56.X;
                float num477 = zero.position.Y + 230f - vector56.Y;
                float num478 = (float)Math.Sqrt(num476 * num476 + num477 * num477);
                npc.rotation = (float)Math.Atan2(num477, num476) + 1.57f;
                if (Main.netMode != 1)
                {
                    ProjTimer += 1f;
                    if (ProjTimer > 140f)
                    {
                        ProjTimer = 0f;
                        float num479 = 12f;
                        num478 = num479 / num478;
                        num476 = -num476 * num478;
                        num477 = -num477 * num478;
                        num476 += Main.rand.Next(-40, 41) * 0.01f;
                        num477 += Main.rand.Next(-40, 41) * 0.01f;
                        vector56.X += num476 * 4f;
                        vector56.Y += num477 * 4f;
                        Projectile.NewProjectile(vector56.X, vector56.Y, num476, num477, Proj, Damage, 0f, Main.myPlayer, 0f, 0f);
                        return;
                    }
                }
            }
            else if (npc.ai[2] == 1f)
            {
                npc.ai[3] += 1f;
                if (npc.ai[3] >= 300f)
                {
                    ProjTimer = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                }
                Vector2 vector57 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num482 = zero.position.X + zero.width / 2 - vector57.X;
                float num483 = player.position.Y + player.height / 2 - 80f - vector57.Y;
                float num484 = (float)Math.Sqrt(num482 * num482 + num483 * num483);
                num484 = 6f / num484;
                num482 *= num484;
                num483 *= num484;
                if (npc.velocity.X > num482)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X *= 0.9f;
                    }
                    npc.velocity.X -= 0.04f;
                }
                if (npc.velocity.X < num482)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X *= 0.9f;
                    }
                    npc.velocity.X += 0.04f;
                }
                if (npc.velocity.Y > num483)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.9f;
                    }
                    npc.velocity.Y -= 0.08f;
                }
                if (npc.velocity.Y < num483)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y *= 0.9f;
                    }
                    npc.velocity.Y += 0.08f;
                }
                npc.TargetClosest(true);
                vector57 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                num482 = player.position.X + player.width / 2 - vector57.X;
                num483 = player.position.Y + player.height / 2 - vector57.Y;
                num484 = (float)Math.Sqrt(num482 * num482 + num483 * num483);
                npc.rotation = (float)Math.Atan2(num483, num482) - 1.57f;
                if (Main.netMode != 1)
                {
                    ProjTimer += 1f;
                    if (ProjTimer > 40f)
                    {
                        ProjTimer = 0f;
                        float num485 = 10f;
                        int num486 = 0;
                        num484 = num485 / num484;
                        num482 *= num484;
                        num483 *= num484;
                        num482 += Main.rand.Next(-40, 41) * 0.01f;
                        num483 += Main.rand.Next(-40, 41) * 0.01f;
                        vector57.X += num482 * 4f;
                        vector57.Y += num483 * 4f;
                        Projectile.NewProjectile(vector57.X, vector57.Y, num482, num483, Proj, num486, 0f, Main.myPlayer, 0f, 0f);
                        return;
                    }
                }
            }
        }

        public static void Laser(Mod mod, NPC npc, Player player, int Zero, int Proj, int Damage, float ProjTimer)
        {
            NPC zero = Main.npc[Zero];
            npc.spriteDirection = -(int)npc.ai[0];
            if (!zero.active || zero.type != mod.NPCType<Zero>())
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50f || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
            if (npc.ai[2] == 0f || npc.ai[2] == 3f)
            {
                if (zero.ai[1] == 3f && npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (zero.ai[1] != 0f)
                {
                    ProjTimer += 3f;
                    if (npc.position.Y > zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y -= 0.07f;
                        if (npc.velocity.Y > 6f)
                        {
                            npc.velocity.Y = 6f;
                        }
                    }
                    else if (npc.position.Y < zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y += 0.07f;
                        if (npc.velocity.Y < -6f)
                        {
                            npc.velocity.Y = -6f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 - 120f * npc.ai[0])
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X -= 0.1f;
                        if (npc.velocity.X > 8f)
                        {
                            npc.velocity.X = 8f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 - 120f * npc.ai[0])
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X += 0.1f;
                        if (npc.velocity.X < -8f)
                        {
                            npc.velocity.X = -8f;
                        }
                    }
                }
                else
                {
                    npc.ai[3] += 1f;
                    if (npc.ai[3] >= 800f)
                    {
                        npc.ai[2] += 1f;
                        npc.ai[3] = 0f;
                        npc.netUpdate = true;
                    }
                    if (npc.position.Y > zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y -= 0.1f;
                        if (npc.velocity.Y > 3f)
                        {
                            npc.velocity.Y = 3f;
                        }
                    }
                    else if (npc.position.Y < zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y += 0.1f;
                        if (npc.velocity.Y < -3f)
                        {
                            npc.velocity.Y = -3f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 - 180f * npc.ai[0])
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X -= 0.14f;
                        if (npc.velocity.X > 8f)
                        {
                            npc.velocity.X = 8f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 - 180f * npc.ai[0])
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X += 0.14f;
                        if (npc.velocity.X < -8f)
                        {
                            npc.velocity.X = -8f;
                        }
                    }
                }
                npc.TargetClosest(true);
                Vector2 vector58 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num488 = player.position.X + player.width / 2 - vector58.X;
                float num489 = player.position.Y + player.height / 2 - vector58.Y;
                float num490 = (float)Math.Sqrt(num488 * num488 + num489 * num489);
                npc.rotation = (float)Math.Atan2(num489, num488) - 1.57f;
                if (Main.netMode != 1)
                {
                    ProjTimer += 1f;
                    if (ProjTimer > 200f)
                    {
                        ProjTimer = 0f;
                        float num491 = 8f;
                        int num492 = 25;
                        int num493 = 100;
                        num490 = num491 / num490;
                        num488 *= num490;
                        num489 *= num490;
                        num488 += Main.rand.Next(-40, 41) * 0.05f;
                        num489 += Main.rand.Next(-40, 41) * 0.05f;
                        vector58.X += num488 * 8f;
                        vector58.Y += num489 * 8f;
                        Projectile.NewProjectile(vector58.X, vector58.Y, num488, num489, num493, num492, 0f, Main.myPlayer, 0f, 0f);
                        return;
                    }
                }
            }
            else if (npc.ai[2] == 1f)
            {
                npc.ai[3] += 1f;
                if (npc.ai[3] >= 200f)
                {
                    ProjTimer = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                }
                Vector2 vector59 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num494 = player.position.X + player.width / 2 - 350f - vector59.X;
                float num495 = player.position.Y + player.height / 2 - 20f - vector59.Y;
                float num496 = (float)Math.Sqrt(num494 * num494 + num495 * num495);
                num496 = 7f / num496;
                num494 *= num496;
                num495 *= num496;
                if (npc.velocity.X > num494)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X *= 0.9f;
                    }
                    npc.velocity.X -= 0.1f;
                }
                if (npc.velocity.X < num494)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X *= 0.9f;
                    }
                    npc.velocity.X += 0.1f;
                }
                if (npc.velocity.Y > num495)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.9f;
                    }
                    npc.velocity.Y -= 0.03f;
                }
                if (npc.velocity.Y < num495)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y *= 0.9f;
                    }
                    npc.velocity.Y += 0.03f;
                }
                npc.TargetClosest(true);
                vector59 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                num494 = player.position.X + player.width / 2 - vector59.X;
                num495 = player.position.Y + player.height / 2 - vector59.Y;
                num496 = (float)Math.Sqrt(num494 * num494 + num495 * num495);
                npc.rotation = (float)Math.Atan2(num495, num494) - 1.57f;
                if (Main.netMode == 1)
                {
                    ProjTimer += 1f;
                    if (ProjTimer > 80f)
                    {
                        ProjTimer = 0f;
                        float num497 = 10f;
                        num496 = num497 / num496;
                        num494 *= num496;
                        num495 *= num496;
                        num494 += Main.rand.Next(-40, 41) * 0.05f;
                        num495 += Main.rand.Next(-40, 41) * 0.05f;
                        vector59.X += num494 * 8f;
                        vector59.Y += num495 * 8f;
                        Projectile.NewProjectile(vector59.X, vector59.Y, num494, num495, Proj, Damage, 0f, Main.myPlayer, 0f, 0f);
                        return;
                    }
                }
            }
        }

        public static void Volley(Mod mod, NPC npc, Player player, int Zero, int Proj, int Damage, float ProjTimer)
        {
            NPC zero = Main.npc[Zero];
            npc.spriteDirection = -(int)npc.ai[0];
            if (!zero.active || zero.type != mod.NPCType<Zero>())
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50f || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
            if (npc.ai[2] == 0f || npc.ai[2] == 3f)
            {
                if (zero.ai[1] == 3f && npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (zero.ai[1] != 0f)
                {
                    ProjTimer += 3f;
                    if (npc.position.Y > zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y -= 0.07f;
                        if (npc.velocity.Y > 6f)
                        {
                            npc.velocity.Y = 6f;
                        }
                    }
                    else if (npc.position.Y < zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y += 0.07f;
                        if (npc.velocity.Y < -6f)
                        {
                            npc.velocity.Y = -6f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 - 120f * npc.ai[0])
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X -= 0.1f;
                        if (npc.velocity.X > 8f)
                        {
                            npc.velocity.X = 8f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 - 120f * npc.ai[0])
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X += 0.1f;
                        if (npc.velocity.X < -8f)
                        {
                            npc.velocity.X = -8f;
                        }
                    }
                }
                else
                {
                    npc.ai[3] += 1f;
                    if (npc.ai[3] >= 800f)
                    {
                        npc.ai[2] += 1f;
                        npc.ai[3] = 0f;
                        npc.netUpdate = true;
                    }
                    if (npc.position.Y > zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y -= 0.1f;
                        if (npc.velocity.Y > 3f)
                        {
                            npc.velocity.Y = 3f;
                        }
                    }
                    else if (npc.position.Y < zero.position.Y - 100f)
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y *= 0.96f;
                        }
                        npc.velocity.Y += 0.1f;
                        if (npc.velocity.Y < -3f)
                        {
                            npc.velocity.Y = -3f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 - 180f * npc.ai[0])
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X -= 0.14f;
                        if (npc.velocity.X > 8f)
                        {
                            npc.velocity.X = 8f;
                        }
                    }
                    if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 - 180f * npc.ai[0])
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X *= 0.96f;
                        }
                        npc.velocity.X += 0.14f;
                        if (npc.velocity.X < -8f)
                        {
                            npc.velocity.X = -8f;
                        }
                    }
                }
                npc.TargetClosest(true);
                Vector2 vector58 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num488 = player.position.X + player.width / 2 - vector58.X;
                float num489 = player.position.Y + player.height / 2 - vector58.Y;
                float num490 = (float)Math.Sqrt(num488 * num488 + num489 * num489);
                npc.rotation = (float)Math.Atan2(num489, num488) - 1.57f;
                if (Main.netMode != 1)
                {
                    ProjTimer += 1f;
                    if (ProjTimer > 200f)
                    {
                        if (ProjTimer > 300)
                        {
                            ProjTimer = 0;
                        }
                        float num491 = 8f;
                        int num492 = 25;
                        int num493 = 100;
                        num490 = num491 / num490;
                        num488 *= num490;
                        num489 *= num490;
                        num488 += Main.rand.Next(-40, 41) * 0.05f;
                        num489 += Main.rand.Next(-40, 41) * 0.05f;
                        vector58.X += num488 * 8f;
                        vector58.Y += num489 * 8f;
                        Projectile.NewProjectile(vector58.X, vector58.Y, num488, num489, num493, num492, 0f, Main.myPlayer, 0f, 0f);

                        return;
                    }
                }
            }
            else if (npc.ai[2] == 1f)
            {
                npc.ai[3] += 1f;
                if (npc.ai[3] >= 300f)
                {
                    ProjTimer = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                }
                Vector2 vector59 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num494 = player.position.X + player.width / 2 - 350f - vector59.X;
                float num495 = player.position.Y + player.height / 2 - 20f - vector59.Y;
                float num496 = (float)Math.Sqrt(num494 * num494 + num495 * num495);
                num496 = 7f / num496;
                num494 *= num496;
                num495 *= num496;
                if (npc.velocity.X > num494)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X *= 0.9f;
                    }
                    npc.velocity.X -= 0.1f;
                }
                if (npc.velocity.X < num494)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X *= 0.9f;
                    }
                    npc.velocity.X += 0.1f;
                }
                if (npc.velocity.Y > num495)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.9f;
                    }
                    npc.velocity.Y -= 0.03f;
                }
                if (npc.velocity.Y < num495)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y *= 0.9f;
                    }
                    npc.velocity.Y += 0.03f;
                }
                npc.TargetClosest(true);
                vector59 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                num494 = player.position.X + player.width / 2 - vector59.X;
                num495 = player.position.Y + player.height / 2 - vector59.Y;
                num496 = (float)Math.Sqrt(num494 * num494 + num495 * num495);
                npc.rotation = (float)Math.Atan2(num495, num494) - 1.57f;
                if (Main.netMode == 1)
                {
                    ProjTimer += 1f;
                    if (ProjTimer > 80f)
                    {
                        if (ProjTimer > 120)
                        {
                            ProjTimer = 0;
                        }
                        float num497 = 10f;
                        num496 = num497 / num496;
                        num494 *= num496;
                        num495 *= num496;
                        num494 += Main.rand.Next(-40, 41) * 0.05f;
                        num495 += Main.rand.Next(-40, 41) * 0.05f;
                        vector59.X += num494 * 8f;
                        vector59.Y += num495 * 8f;
                        Projectile.NewProjectile(vector59.X, vector59.Y, num494, num495, Proj, Damage, 0f, Main.myPlayer, 0f, 0f);
                        return;
                    }
                }
            }
        }

        public static void Focus(NPC npc, int Zero, float ProjTimer)
        {
            NPC zero = Main.npc[Zero];
            if (zero.ai[1] != 0f)
            {
                ProjTimer += 2f;
                if (npc.position.Y > zero.position.Y)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.96f;
                    }
                    npc.velocity.Y -= 0.07f;
                    if (npc.velocity.Y > 6f)
                    {
                        npc.velocity.Y = 6f;
                    }
                }
                else if (npc.position.Y < zero.position.Y)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y *= 0.96f;
                    }
                    npc.velocity.Y += 0.07f;
                    if (npc.velocity.Y < -6f)
                    {
                        npc.velocity.Y = -6f;
                    }
                }
                if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 - 120f * npc.ai[0])
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X *= 0.96f;
                    }
                    npc.velocity.X -= 0.1f;
                    if (npc.velocity.X > 8f)
                    {
                        npc.velocity.X = 8f;
                    }
                }
                if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 - 120f * npc.ai[0])
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X *= 0.96f;
                    }
                    npc.velocity.X += 0.1f;
                    if (npc.velocity.X < -8f)
                    {
                        npc.velocity.X = -8f;
                    }
                }
            }
            else
            {
                npc.ai[3] += 1f;
                if (npc.ai[3] >= 1100f)
                {
                    ProjTimer = 0f;
                    npc.ai[2] = 1f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                }
                if (npc.position.Y > zero.position.Y + 320f)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y *= 0.96f;
                    }
                    npc.velocity.Y -= 0.04f;
                    if (npc.velocity.Y > 3f)
                    {
                        npc.velocity.Y = 3f;
                    }
                }
                else if (npc.position.Y < zero.position.Y + 260f)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y *= 0.96f;
                    }
                    npc.velocity.Y += 0.04f;
                    if (npc.velocity.Y < -3f)
                    {
                        npc.velocity.Y = -3f;
                    }
                }
                if (npc.position.X + npc.width / 2 > zero.position.X + zero.width / 2 + 200f)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X *= 0.96f;
                    }
                    npc.velocity.X -= 0.2f;
                    if (npc.velocity.X > 8f)
                    {
                        npc.velocity.X = 8f;
                    }
                }
                if (npc.position.X + npc.width / 2 < zero.position.X + zero.width / 2 + 160f)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X *= 0.96f;
                    }
                    npc.velocity.X += 0.2f;
                    if (npc.velocity.X < -8f)
                    {
                        npc.velocity.X = -8f;
                    }
                }
            }
        }
    }
}