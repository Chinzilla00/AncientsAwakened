using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;

namespace AAMod.Items.Dev
{
    public class Skullshot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Skullshot");
            Tooltip.SetDefault(@"fires a massive spread of bullets at your foes
Right click to fire a spinning bone at your foe
Uses Bullets and Bones as ammo
'I have an irrational hatred for gods`
-Gibs");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.knockBack = 7f;
            item.useStyle = 5;
            item.useAnimation = 34;
            item.useTime = 34;
            item.width = 50;
            item.height = 14;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item38;
            item.damage = 30;
            item.shootSpeed = 6f;
            item.noMelee = true;
            item.value = 700000;
            item.rare = 9;
            item.ranged = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
			glowmaskDrawType = GLOWMASKTYPE_GUN;
			glowmaskDrawColor = Color.White;
			customNameColor = new Color(255, 128, 0);
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.damage = 30;
                item.shoot = 10;
                item.useAmmo = AmmoID.Bullet;
            }
            else
            {
                item.damage = 80;
                item.shoot = ProjectileID.BoneGloveProj;
                item.useAmmo = AAMod.BoneAmmo;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num81 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num82 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            float num176 = num81;
            float num177 = num82;
            if (player.altFunctionUse == 2)
            {
                for (int num175 = 0; num175 < 6; num175++)
                {
                    num176 += Main.rand.Next(-40, 41) * 0.05f;
                    num177 += Main.rand.Next(-40, 41) * 0.05f;
                    Projectile.NewProjectile(vector2.X, vector2.Y, num176, num177, item.shoot, damage, knockBack, Main.myPlayer, 0f, 0f);
                }
            }
            else
            {
                int proj = Projectile.NewProjectile(vector2.X, vector2.Y, num176, num177, ProjectileID.BoneGloveProj, damage, knockBack, Main.myPlayer, 0f, 0f);
                Main.projectile[proj].ranged = true;
            }
            return false;
        }
    }
}