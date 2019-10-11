using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using BaseMod;

namespace AAMod.Projectiles   //The directory for your .cs and .png; Example: TutorialMOD/Projectiles
{
    public class DragonBreathP : ModProjectile   //make sure the sprite file is named like the class name (CustomYoyoProjectile)
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Breath");
        }

        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 12f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 300f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 14f;
        }

        public float[] internalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }

        public override void PostAI()
        {
            int Target = BaseAI.GetNPC(projectile.Center, -1, 500);
            if (Target != -1)
            {
                NPC target = Main.npc[Target];
                BaseAI.ShootPeriodic(projectile, target.position, 14, 14, Terraria.ModLoader.ModContent.ProjectileType<DragonBreath>(), ref internalAI[0], 5, projectile.damage, 4, true);
            }
        }
    }
}
