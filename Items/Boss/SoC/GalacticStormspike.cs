using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;
using AAMod;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Audio;

namespace AAMod.Items.Boss.SoC
{
	public class GalacticStormspike : ModItem
	{
        public override void SetStaticDefaults()
        {
            Item.staff[item.type] = true;
            DisplayName.SetDefault("Galactic Stormspike");
            BaseUtility.AddTooltips(item, new string[] { "Shoots a branching ray of dark electricity" });
        }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 25;
            item.maxStack = 1;
            item.rare = 8;
            item.value = BaseUtility.CalcValue(0, 35, 55, 20);

            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
            item.UseSound = new LegacySoundStyle(2, 15, Terraria.Audio.SoundType.Sound);
            item.damage = 190;
            item.knockBack = 4;
            item.magic = true;
            item.mana = 10;
            item.autoReuse = true;
            item.noMelee = true;	
            item.shoot = mod.ProjType("Stormray");
            item.shootSpeed = 4;	
        }

        public override bool Shoot(Player p, ref Vector2 shootPos, ref float speedX, ref float speedY, ref int projType, ref int damage, ref float knockback)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
			int pID = Projectile.NewProjectile(shootPos.X, shootPos.Y, velocity.X, velocity.Y, mod.ProjType("Stormray"), damage, knockback, p.whoAmI);
			return false;
		}
	}
}