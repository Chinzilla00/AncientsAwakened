using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Summoning
{
    public class ChaosRitualEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perfect Chaos Ritual");
            Tooltip.SetDefault(@"Summons a small chaos dragon to fight for you");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 6));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 400;
            item.summon = true;
            item.mana = 25;
            item.width = 24;
            item.height = 24;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 3;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("XiaoDoragon");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("XiaoDoragon");
            item.autoReuse = true;
            item.rare = 11;
            item.expert = true;
            item.expertOnly = true;
            item.value = Item.sellPrice(0, 30, 0, 0);
        }
		
		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, mod.ProjectileType("XiaoDoragon"), damage, player.GetWeaponKnockback(item, knockBack), Main.myPlayer, 0f, 0f);
            return false;
        }
    }
}