using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace AAMod.Items.Boss.Greed.WKG
{
    public class OreCannon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ore Cannon");
            Tooltip.SetDefault(@"Uses Some Ores as Ammunition
Certain ores have special effects when shot");
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
			item.shoot = 10;
            item.UseSound = SoundID.Item14;
            item.shootSpeed = 14f;
            item.expert = true; 
			item.expertOnly = true;
            item.autoReuse = true;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -3);
        }

        readonly int[] Ores = new int[]
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
            AAMod.instance.ItemType("Abyssium"),
            AAMod.instance.ItemType("Incinerite"),
            ItemID.Hellstone,
            ItemID.CobaltOre,
            ItemID.PalladiumOre,
            ItemID.MythrilOre,
            ItemID.OrichalcumOre,
            ItemID.AdamantiteOre,
            ItemID.TitaniumOre,
            AAMod.instance.ItemType("HallowedOre"),
            ItemID.ChlorophyteOre,
            ItemID.LunarOre,
            AAMod.instance.ItemType("DarkmatterOre"),
            AAMod.instance.ItemType("RadiumOre"),
            AAMod.instance.ItemType("DaybreakIncineriteOre"),
            AAMod.instance.ItemType("EventideAbyssiumOre"),
            AAMod.instance.ItemType("Apocalyptite"),
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
                    if (itemFired.type == mod.ItemType("DarkmatterOre")) projType = 23;
                    if (itemFired.type == mod.ItemType("RadiumOre")) projType = 24;
                    if (itemFired.type == mod.ItemType("DaybreakIncineriteOre")) projType = 25;
                    if (itemFired.type == mod.ItemType("EventideAbyssiumOre")) projType = 26;
                    if (itemFired.type == mod.ItemType("Apocalyptite")) projType = 27;
                    return true;
 				}
 			}
            return false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
 		{
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("OreChunkHM"), damage, knockBack, player.whoAmI, 0, projType);
            if (Main.projectile[p].ai[1] == 10)
            {
                 Main.projectile[p].knockBack *= 1.5f;
            }
            if (Main.projectile[p].ai[1] == 19)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0, Main.projectile[p].ai[1]);
                }
            }
            Main.projectile[p].magic = false;
            Main.projectile[p].ranged = true;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GildedGlock", 1);
            recipe.AddIngredient(null, "CovetiteBar", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
