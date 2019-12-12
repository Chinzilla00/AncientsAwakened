using Microsoft.Xna.Framework;
using Terraria;
using BaseMod;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Dusts;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AAMod.Projectiles.Greed.WKG
{
    public class OreChunk : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
			projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 6;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Ore");
		}

        public override void AI()
        {
            OreEffect();
            if (projectile.velocity.X > 0)
            {
                projectile.direction = 1;
            }
            else
            {
                projectile.direction = -1;
            }
            projectile.rotation += .2f * projectile.direction;

            for (int m = projectile.oldPos.Length - 1; m > 0; m--)
            {
                projectile.oldPos[m] = projectile.oldPos[m - 1];
            }
            projectile.oldPos[0] = projectile.position;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.itemTexture[(int)projectile.ai[1]].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < 3; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((3 - k) / (float)3);
				spriteBatch.Draw(Main.itemTexture[(int)projectile.ai[1]], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
            
            /*
            Rectangle frame = BaseDrawing.GetFrame(1, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 0);

            if (projectile.ai[1] == ItemID.DemoniteOre || projectile.ai[1] == mod.ItemType("Abyssium") || projectile.ai[1] == ItemID.LunarOre || projectile.ai[1] == mod.ItemType("EventideAbyssiumOre"))
            {
                BaseDrawing.DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.oldPos, 1, projectile.rotation, projectile.direction, 1, frame, .8f, 1, 4, true, 0, 0, lightColor);
            }
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, lightColor, true);
            */
            return false;
        }

        public override void Kill(int timeLeft)
        {
            int DustType = DType();
            for (int num468 = 0; num468 < 5; num468++)
            {
                float VelX = -projectile.velocity.X * 0.2f;
                float VelY = -projectile.velocity.Y * 0.2f;
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustType, VelX, VelY);
            }
            if (projectile.ai[1] == ItemID.Meteorite)
            {
                for (int num291 = 0; num291 < 5; num291++)
                {
                    int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default, 2.1f);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else if (projectile.ai[1] == ItemID.ChlorophyteOre)
            {
                for (int s = 0; s < 3; s++)
                {
                    Projectile.NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<OreSpores>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0, s);
                }
            }
            else if (projectile.ai[1] == ItemID.LunarOre)
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0, 0);
            }
            else if (projectile.ai[1] == mod.ItemType("DaybreakIncineriteOre"))
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), projectile.damage, projectile.knockBack * 3, Main.myPlayer, 0, 0);
            }
            else if (projectile.ai[1] == mod.ItemType("Apocalyptite"))
            {
                for (int v = 0; v < 4; v++)
                {
                    int x = Main.rand.Next(-6, 6);
                    int y = -Main.rand.Next(3, 5);
                    int p = Projectile.NewProjectile(projectile.position, new Vector2(x, y), ModContent.ProjectileType<AFrag>(), projectile.damage, 0, Main.myPlayer, 0, Main.rand.Next(23));
                    Main.projectile[p].Center = projectile.Center;
                }
            }
            else
            {
                return;
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            int k = (int)projectile.ai[1];
            if(k == ItemID.GoldOre || k == ItemID.PlatinumOre)
            {
                target.AddBuff(BuffID.Midas, 180);
            }
            else if(k == mod.ItemType("Incinerite") || k == ItemID.Hellstone)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
            else if(k == mod.ItemType("DarkmatterOre"))
            {
                target.AddBuff(ModContent.BuffType<Buffs.Electrified>(), 180);
            }
            else if(k == mod.ItemType("DaybreakIncineriteOre"))
            {
                target.AddBuff(BuffID.Daybreak, 180);
            }
            else if(k == mod.ItemType("EventideAbyssiumOre"))
            {
                target.AddBuff(ModContent.BuffType<Buffs.Moonraze>(), 180);
            }
            else
            {
                return;
            }
        }

        public void OreEffect()
        {
            int k = (int)projectile.ai[1];
            Item item = new Item();
            item.SetDefaults(k, false);
            if(k == ItemID.DemoniteOre || k == mod.ItemType("Abyssium") || k == mod.ItemType("RadiumOre"))
            {
                projectile.extraUpdates = 1;
            }
            else if(k == ItemID.Hellstone || k == ItemID.CobaltOre)
            {
                for (int num291 = 0; num291 < 5; num291++)
                {
                    int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else if(k == mod.ItemType("DaybreakIncineriteOre"))
            {
                projectile.penetrate = 1;
            }
            else if(k == ItemID.LunarOre)
            {
                projectile.penetrate = 1; 
                projectile.extraUpdates = 2;
            }
            else if(k == mod.ItemType("EventideAbyssiumOre"))
            {
                projectile.extraUpdates = 2;
            }
            else if(Config.LuckyOre[k] > 650 && item.modItem.mod != ModLoader.GetMod("AAMod") && item.modItem.mod != ModLoader.GetMod("ModLoader"))
            {
                int dustid = DustID.Copper;
                switch (WorldGen.genRand.Next(10))
                {
                    case 0:
                        dustid = DustID.Copper; break;
                    case 1:
                        dustid = DustID.Tin; break;
                    case 2:
                        dustid = DustID.Iron; break;
                    case 3:
                        dustid = DustID.Lead; break;
                    case 4:
                        dustid = DustID.Silver; break;
                    case 5:
                        dustid = DustID.Tungsten; break;
                    case 6:
                        dustid = DustID.Gold; break;
                    case 7:
                        dustid = DustID.Platinum; break;
                    case 8:
                        dustid = DustID.t_Meteor; break;
                    case 9:
                        dustid = DustID.Fire; break;
                }
                for (int num291 = 0; num291 < 3; num291++)
                {
                    int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustid, 0f, 0f, 100);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else
            {
                return;
            }
        }

        public int Damage()
        {
            int orevalue = 0;
            if(Config.LuckyOre.TryGetValue((int)projectile.ai[1], out orevalue))
            {
                return (int)Math.Exp(orevalue * 0.67/100);
            }
            else if((int)projectile.ai[1] == ItemID.Hellstone)
            {
                return (int)Math.Exp(500 * 0.67/100);
            }
            else
            {
                return (int)Math.Exp(100 * 0.67/100);
            }
            /* 
            switch ((int)projectile.ai[1])
            {
                case 0:
                    return 8;
                case 1:
                    return 9;
                case 2:
                    return 10;
                case 3:
                case 4:
                    return 11;
                case 5:
                    return 12;
                case 6:
                    return 13;
                case 7:
                    return 15;
                case 8:
                    return 21;
                case 9:
                    return 19;
                case 10:
                    return 22;
                case 11:
                    return 14;
                case 12:
                    return 26;
                case 13:
                    return 36;
                case 14:
                    return 39;
                case 15:
                    return 41;
                case 16:
                    return 44;
                case 17:
                    return 47;
                case 18:
                    return 50;
                case 19:
                    return 52;
                case 20:
                    return 57;
                case 21:
                    return 75;
                case 22:
                    return 110;
                case 23:
                    return 130;
                case 24:
                    return 170;
                case 25:
                    return 160;
                case 26:
                    return 130;
                case 27:
                    return 150;
                default:
                    goto case 0;
            }
            */
        }

        public int DType()
        {
            int k = (int)projectile.ai[1];
            if(k == ItemID.CopperOre)
            {
                return DustID.Copper;
            }
            else if(k == ItemID.TinOre)
            {
                return DustID.Tin;
            }
            else if(k == ItemID.IronOre)
            {
                return DustID.Iron;
            }
            else if(k == ItemID.LeadOre)
            {
                return DustID.Lead;
            }
            else if(k == ItemID.SilverOre)
            {
                return DustID.Silver;
            }
            else if(k == ItemID.TungstenOre)
            {
                return DustID.Tungsten;
            }
            else if(k == ItemID.GoldOre)
            {
                return DustID.Gold;
            }
            else if(k == ItemID.PlatinumOre)
            {
                return DustID.Platinum;
            }
            else if(k == ItemID.Meteorite)
            {
                return DustID.t_Meteor;
            }
            else if (k == ItemID.DemoniteOre)
            {
                return 14;
            }
            else if (k == ItemID.CrimtaneOre)
            {
                return 117;
            }
            else if (k == mod.ItemType("Abyssium"))
            {
                return ModContent.DustType<AbyssiumDust>();
            }
            else if (k == mod.ItemType("Incinerite"))
            {
                return ModContent.DustType<IncineriteDust>();
            }
            else if (k == ItemID.Hellstone)
            {
                return DustID.Fire;
            }
            else if (k == ItemID.CobaltOre)
            {
                return 48;
            }
            else if (k == ItemID.PalladiumOre)
            {
                return 144;
            }
            else if (k == ItemID.MythrilOre)
            {
                return 49;
            }
            else if (k == ItemID.OrichalcumOre)
            {
                return 145;
            }
            else if (k == ItemID.AdamantiteOre)
            {
                return 50;
            }
            else if (k == ItemID.TitaniumOre)
            {
                return 146;
            }
            else if (k == mod.ItemType("HallowedOre"))
            {
                return DustID.Gold;
            }
            else if (k == ItemID.ChlorophyteOre)
            {
                return 128;
            }
            else if (k == ItemID.LunarOre)
            {
                return ModContent.DustType<LuminiteDust>();
            }
            else if (k == mod.ItemType("DarkmatterOre"))
            {
                return ModContent.DustType<DarkmatterDust>();
            }
            else if (k == mod.ItemType("RadiumOre"))
            {
                return ModContent.DustType<RadiumDust>();
            }
            else if (k == mod.ItemType("DaybreakIncineriteOre"))
            {
                return ModContent.DustType<DaybreakIncineriteDust>();
            }
            else if (k == mod.ItemType("EventideAbyssiumOre"))
            {
                return ModContent.DustType<YamataDust>();
            }
            else if (k == mod.ItemType("Apocalyptite"))
            {
                return ModContent.DustType<VoidDust>();
            }
            else if (Config.LuckyOre[k] <= 300)
            {
                return DustID.Copper;
            }
            else if (Config.LuckyOre[k] <= 700)
            {
                return DustID.Gold;
            }
            else
            {
                switch (WorldGen.genRand.Next(18))
                {
                    case 0:
                        return DustID.Copper;
                    case 1:
                        return DustID.Tin;
                    case 2:
                        return DustID.Iron;
                    case 3:
                        return DustID.Lead;
                    case 4:
                        return DustID.Silver;
                    case 5:
                        return DustID.Tungsten;
                    case 6:
                        return DustID.Gold;
                    case 7:
                        return DustID.Platinum;
                    case 8:
                        return DustID.t_Meteor;
                    case 9:
                        return ModContent.DustType<LuminiteDust>();
                    case 10:
                        return ModContent.DustType<DarkmatterDust>();
                    case 11:
                        return ModContent.DustType<RadiumDust>();
                    case 12:
                        return ModContent.DustType<DaybreakIncineriteDust>();
                    case 13:
                        return ModContent.DustType<YamataDust>();
                    case 14:
                        return ModContent.DustType<VoidDust>();
                    case 15:
                        return ModContent.DustType<IncineriteDust>();
                    case 16:
                        return ModContent.DustType<AbyssiumDust>();
                    case 17:
                        return DustID.Fire;
                }
            }

            switch ((int)projectile.ai[1])
            {
                case 0:
                    return DustID.Copper;
                case 1:
                    return DustID.Tin;
                case 2:
                    return DustID.Iron;
                case 3:
                    return DustID.Lead;
                case 4:
                    return DustID.Silver;
                case 5:
                    return DustID.Tungsten;
                case 6:
                    return DustID.Gold;
                case 7:
                    return DustID.Platinum;
                case 8:
                    return DustID.t_Meteor;
                case 9:
                    return 14;
                case 10:
                    return 117;
                case 11:
                    return ModContent.DustType<IncineriteDust>();
                case 12:
                    return ModContent.DustType<AbyssiumDust>();
                case 13:
                    return DustID.Fire;
                case 14:
                    return 48;
                case 15:
                    return 144;
                case 16:
                    return 49;
                case 17:
                    return 145;
                case 18:
                    return 50;
                case 19:
                    return 146;
                case 20:
                    return DustID.Gold;
                case 21:
                    return 128;
                case 22:
                    return ModContent.DustType<LuminiteDust>();
                case 23:
                    return ModContent.DustType<DarkmatterDust>();
                case 24:
                    return ModContent.DustType<RadiumDust>();
                case 25:
                    return ModContent.DustType<DaybreakIncineriteDust>();
                case 26:
                    return ModContent.DustType<YamataDust>();
                case 27:
                    return ModContent.DustType<VoidDust>();
                default:
                    goto case 0;
            }

        }
    }
}
