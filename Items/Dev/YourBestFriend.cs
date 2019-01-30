using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class YourBestFriend : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Your Best Friend");
            Tooltip.SetDefault(@"Surrounds your enemies in small white pellets that are difficult to avoid 
'Eat seeds you damned Emus'
-Darkpuppy");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 140;
			item.magic = true;
			item.mana = 8;
			item.width = 56;
			item.height = 60;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 1000000;
			item.rare = 11;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Pellet");
			item.shootSpeed = 10f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            int i = Main.myPlayer;
            float num72 = item.shootSpeed;
            int num73 = damage;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(item, num74);
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X + 150, vector2.Y, num78 - 5, num79, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
            Projectile.NewProjectile(vector2.X + 150, vector2.Y + 150, num78 - 5, num79 - 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
            Projectile.NewProjectile(vector2.X, vector2.Y + 150, num78, num79 - 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
            Projectile.NewProjectile(vector2.X - 150, vector2.Y + 150, num78 + 5, num79 - 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
            Projectile.NewProjectile(vector2.X - 150, vector2.Y, num78 + 5, num79, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
            Projectile.NewProjectile(vector2.X - 150, vector2.Y - 150, num78 + 5, num79 + 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
            Projectile.NewProjectile(vector2.X, vector2.Y - 150, num78, num79 + 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
            Projectile.NewProjectile(vector2.X + 150, vector2.Y - 150, num78 - 5, num79 + 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 246, 124);
                }
            }
        }
	}
}