using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAMod.Items.Blocks;
using BaseMod;

namespace AAMod.Items.Boss.Greed.WKG
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
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.knockBack = 0;
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item14;
            item.shootSpeed = 12f;
            item.expert = true; item.expertOnly = true;
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
            if (BasePlayer.HasItem(player, Ores, ref itemIndex, default, false, false))
            {
                Item itemFired = player.inventory[itemIndex];

                BasePlayer.ReduceSlot(player, itemIndex, 1);


                return projType > 0;
            }
            return false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            Projectile.NewProjectile(position.X, position.Y, speedX * 1f, speedY * 1f, mod.ProjectileType("ChaosShot1"), damage, knockBack, player.whoAmI, 0, 1);
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
