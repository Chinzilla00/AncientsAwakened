using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
	public class RadiumHat : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radium Hat");
			Tooltip.SetDefault(@"35% increased minion damage
Shines with the light of a starry night sky");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = 300000;
			item.defense = 18;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.35f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RadiumPlatemail") && legs.type == mod.ItemType("RadiumCuisses");
        }

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.RadiumHatBonus1") + (int)(DarkMinions.baseBlastDamage * player.minionDamage) + " " + Language.GetTextValue("Mods.AAMod.Common.RadiumHatBonus2");
            player.GetModPlayer<HatEffects>().setBonus = true;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumBar", 25);
            recipe.AddIngredient(null, "Stardust", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
    public class HatEffects : ModPlayer
    {
        public bool setBonus = false;
        public override void ResetEffects()
        {
            setBonus = false;

        }
    }
    public class DarkMinions : GlobalProjectile
    {
        //power settings
        const int cooldownRate = 120;
        const float radius = 300;
        public const int baseBlastDamage = 200;
        //

        int cooldown = 0;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override bool PreAI(Projectile projectile)
        {

            if (cooldown > 0)
            {
                cooldown--;
            }
            if (projectile.minion && projectile.minionSlots > 0 && projectile.active && Main.player[projectile.owner].GetModPlayer<HatEffects>().setBonus && cooldown == 0)
            {

                for (int n = 0; n < Main.npc.Length; n++)
                {
                    if ((Main.npc[n].Center - projectile.Center).Length() < radius - 100 && Main.npc[n].CanBeChasedBy())
                    {
                        SunBlast(projectile);
                        break;
                    }
                }
            }
            
            return base.PreAI(projectile);
        }
        void SunBlast(Projectile projectile)
        {
            for (int i = 0; i < 100; i++)
            {
                float theta = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                Dust dust = Dust.NewDustPerfect(projectile.Center, mod.DustType("RadiumDust"), PolarVector(radius / 30, theta));
                dust.noGravity = true;
            }
            cooldown = (int)(cooldownRate / projectile.minionSlots);
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("RadiumSetbonusBlast"), (int)(baseBlastDamage * Main.player[projectile.owner].minionDamage), 0f, projectile.owner, radius);
            
        }
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }


    }
}