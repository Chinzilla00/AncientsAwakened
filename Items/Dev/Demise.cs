using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class Demise : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Demise");
			Tooltip.SetDefault(@"A legendary sword that was once wielded by the demon king
 Left Click to unleash destructive demonic energy
Right Click to unleash demon blades that fall from the sky");
		}
		public override void SetDefaults()
		{
			item.damage = 150;
			item.melee = true;
			item.width = 58;
			item.height = 58;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 5;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("DemiseSphere");
            item.shootSpeed = 9f;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                Item.staff[item.type] = false;
                item.useStyle = 1;
                item.noMelee = false;
                item.shoot = mod.ProjectileType("DemiseBlade");
            }
            else
            {
                Item.staff[item.type] = true;
                item.useStyle = 5;
                item.noMelee = true;
                item.shoot = mod.ProjectileType("DemiseSphere");
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 vector12 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                float num75 = item.shootSpeed;
                for (int num120 = 0; num120 < 3; num120++)
                {
                    Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                    vector2.Y -= 100 * num120;
                    Vector2 vector13 = vector12 - vector2;
                    if (vector13.Y < 0f)
                    {
                        vector13.Y *= -1f;
                    }
                    if (vector13.Y < 20f)
                    {
                        vector13.Y = 20f;
                    }
                    vector13.Normalize();
                    vector13 *= num75;
                    float num82 = vector13.X;
                    float num83 = vector13.Y;
                    float speedX5 = num82;
                    float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
                    Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType("DemiseBlade"), damage * 3 / 2, knockBack, Main.myPlayer);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("DemiseSphere"), damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(77, 20, 102);
                }
            }
        }
	}
}
