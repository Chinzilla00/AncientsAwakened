using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAMod.Items.Blocks;
using BaseMod;

namespace AAMod.Items.Boss.Greed
{
    public class OreCannon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ore Cannon");
            Tooltip.SetDefault("Uses Ore as Ammunition");
        }

        public override void SetDefaults()
        {

            item.damage = 300;
            item.noMelee = true;
            item.ranged = true;
            item.width = 50;
            item.height = 20;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 5;
            item.knockBack = 0;
            item.value = Item.sellPrice(5, 0, 0, 0);
			item.shoot = 10;
            item.rare = 8;
            item.UseSound = SoundID.Item14;
            item.shootSpeed = 12f;
            item.expert = true; 
			item.expertOnly = true;
            item.autoReuse = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -3);
        }

        int[] Ores = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ItemID.IronOre,
            ItemID.LeadOre,
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ModContent.ItemType<Abyssium>(),
            ModContent.ItemType<Incinerite>(),
            ItemID.Hellstone,
            ItemID.CobaltOre,
            ItemID.PalladiumOre,
            ItemID.MythrilOre,
            ItemID.OrichalcumOre,
            ItemID.AdamantiteOre,
            ItemID.TitaniumOre,
            ModContent.ItemType<HallowedOre>(),
            ItemID.ChlorophyteOre,
            ItemID.LunarOre
        };

        public int projType = -1;

        public override bool CanUseItem(Player player)
        {
            int itemIndex = -1;
			if (player.itemAnimation == 0)
			{
				if (BasePlayer.HasItem(player, Ores, ref itemIndex, default, false, false))
				{
					Item itemFired = player.inventory[itemIndex];

					BasePlayer.ReduceSlot(player, itemIndex, 1);
					
					if (itemFired.type == ItemID.CopperOre) projType = 0;
					if (itemFired.type == ItemID.TinOre) projType = 1;
					if (itemFired.type == ItemID.IronOre) projType = 2;
					if (itemFired.type == ItemID.LeadOre) projType = 3;
					if (itemFired.type == ItemID.SilverOre) projType = 4;
					if (itemFired.type == ItemID.TungstenOre) projType = 5;
					if (itemFired.type == ItemID.GoldOre) projType = 6;
					if (itemFired.type == ItemID.PlatinumOre) projType = 7;
					if (itemFired.type == ItemID.Meteorite) projType = 8;
					if (itemFired.type == ItemID.DemoniteOre) projType = 9;
					if (itemFired.type == ItemID.CrimtaneOre) projType = 10;
					if (itemFired.type == mod.ItemType("Abyssium")) projType = 11;
					if (itemFired.type == mod.ItemType("Incinerite")) projType = 12;
					if (itemFired.type == ItemID.Hellstone) projType = 13;
					if (itemFired.type == ItemID.CobaltOre) projType = 14;
					if (itemFired.type == ItemID.PalladiumOre) projType = 15;
					if (itemFired.type == ItemID.MythrilOre) projType = 16;
					if (itemFired.type == ItemID.OrichalcumOre) projType = 17;
					if (itemFired.type == ItemID.AdamantiteOre) projType = 18;
					if (itemFired.type == ItemID.TitaniumOre) projType = 19;
					if (itemFired.type == mod.ItemType("HallowedOre")) projType = 20;
					if (itemFired.type == ItemID.ChlorophyteOre) projType = 21;
					if (itemFired.type == ItemID.LunarOre) projType = 22;
					return true;
				}
			}
            return false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("OreChunk"), damage, knockBack, player.whoAmI);
			Main.projectile[p].ai[1] = projType;
            if (Main.projectile[p].ai[1] == 10)
            {
                Main.projectile[p].knockBack *= 1.5f;
            }
            if (Main.projectile[p].ai[1] == 19)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0, 5);
                }
            }
            return false;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosShot", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
