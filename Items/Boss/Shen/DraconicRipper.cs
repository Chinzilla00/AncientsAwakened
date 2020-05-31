using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class DraconicRipper : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.autoReuse = true;
			item.useAnimation = 2;
			item.useTime = 2;
			item.width = 72;
            item.height = 34;
            item.shoot = ProjectileID.Bullet;
			item.UseSound = SoundID.Item41;
			item.damage = 65;
			item.shootSpeed = 16f;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 30, 0, 0);
			item.rare = ItemRarityID.Purple;
			item.knockBack = 3f;
			item.ranged = true;
            item.useAmmo = AmmoID.Bullet;
            AARarity = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity14;
                }
            }
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Draconic Ripper");
			Tooltip.SetDefault(@"Shoots dozens of high-caliber teeth
Ignores enemy defense
50% chance to not consume ammo");
        }
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -2);
        }

        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() >= .5f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 3; i++)
            {
                string tooth;

                if(i == 0)
                {
                    tooth = "ShenTooth";
                }
                else if(i == 1)
                {
                    tooth = "AkumaTooth";
                    knockBack += 2;
                    damage -= 10;
                }
                else
                {
                    tooth = "YamataTooth";
                    knockBack -= 2;
                    damage += 10;
                }
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType(tooth), damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void HoldItem(Player player)
		{
			player.armorPenetration += 500;
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddIngredient(null, "ChaosScale", 5);
			recipe.AddIngredient(ItemID.ChainGun);
            recipe.AddTile(mod.TileType("ACS"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
