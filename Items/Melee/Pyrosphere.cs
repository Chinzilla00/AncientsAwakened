using System;
using System.Collections.Generic;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Melee
{
	public class Pyrosphere : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pyrosphere");			
		}		
		
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 1;
            item.rare = 2;
            item.value = BaseMod.BaseUtility.CalcValue(0, 0, 90, 50);
            item.useStyle = 5;
            item.useAnimation = 45;
            item.useTime = 45;
            item.UseSound = SoundID.Item1;
            item.damage = 20;
            item.knockBack = 7;
            item.melee = true;
            item.shoot = mod.ProjType("Pyrosphere");
            item.shootSpeed = 10;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.channel = true;		
        }
	}
}