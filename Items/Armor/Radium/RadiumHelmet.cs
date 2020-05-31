using AAMod.Items.Armor.Darkmatter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
	public class RadiumHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radium Helmet");
			Tooltip.SetDefault(@"15% increased melee damage
Shines with the light of a starry night sky");

        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 300000;
			item.defense = 30;
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
			player.meleeDamage += 0.15f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("RadiumPlatemail") && legs.type == mod.ItemType("RadiumCuisses");
        }

		public override void UpdateArmorSet(Player player)
		{
            const float effectRange = 500;
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.RadiumHelmetBonus");
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                for (int p = 0; p < Main.player.Length; p++)
                {
                    if (Main.player[p].active && (Main.player[p].Center - player.Center).Length() < effectRange && player.team != Main.player[p].team)
                    {
                        Main.player[p].GetModPlayer<HelmetEffects>().ShieldTime = 2;
                        Main.player[p].GetModPlayer<HelmetEffects>().badShield = true;
                    }
                }
            }
            for(int n = 0; n < Main.npc.Length; n++)
            {
                if ((Main.npc[n].Center - player.Center).Length() < effectRange && Main.npc[n].CanBeChasedBy(ignoreDontTakeDamage: false))
                {
                    Main.npc[n].GetGlobalNPC<RadiumWeaken>().BrokenShield = 2;
                }
            }
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
    public class RadiumWeaken : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public int BrokenShield = 0;
        public override void ResetEffects(NPC npc)
        {
            if(BrokenShield > 0)
            {
                BrokenShield--;
            }
        }
        public float yetAnotherTrigCounter = 0;
        public override void AI(NPC npc)
        {
            yetAnotherTrigCounter += (float)Math.PI / 60;
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if(BrokenShield > 0)
            {
                damage = (int)(damage * 1.4f);
            }
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (BrokenShield > 0)
            {
                damage = (int)(damage * 1.4f);
            }
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if(BrokenShield > 0)
            {
                Texture2D texture = mod.GetTexture("Items/Armor/Radium/RadiumShield");
                spriteBatch.Draw(texture, npc.Top + Vector2.UnitY * -30 - Main.screenPosition, null, Color.White, 0f, texture.Size() * .5f, 1f + (.1f * (float)Math.Sin(yetAnotherTrigCounter)), SpriteEffects.None, 0f);
            }
            
        }
    }
}