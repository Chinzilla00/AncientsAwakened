using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
    public class DarkmatterVisor : BaseAAItem
    {

        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Darkmatter Visor");
            Tooltip.SetDefault(@"10% increased Ranged damage
Dark, yet still barely visible");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 14;
            item.value = 300000;
            item.rare = 11;
            item.defense = 26;

        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = "An unstable portal hovers above your head, shooting dark blasts when you shoot. \nDark blasts deal 30% of your weapon's damage, weakens enemies, and occasional dark locks them";
            player.GetModPlayer<VisorEffects>().setBonus = true;
            player.GetModPlayer<VisorEffects>().sunPortal = false;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 25);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
    public class VisorEffects : ModPlayer
    {
        public bool setBonus = false;
        public Vector2 portalOffset = new Vector2(0, -50);
        public int portalFrame = 0;
        public int portalFrameCount = 4;
        public bool sunPortal = false;
        int timer;
        bool shot = false;
        public override void ResetEffects()
        {
            setBonus = false;

        }
        
        public override void PreUpdate()
        {
            
            timer++;
            if (timer % 10 == 0)
            {
                portalFrame++;
                if (portalFrame >= portalFrameCount)
                {
                    portalFrame = 0;
                }
            }
            if(player.itemTime>1 && player.HeldItem.ranged)
            {
                
                if (!shot && setBonus)
                {
                    if(sunPortal)
                    {
                        Projectile.NewProjectile(player.Center + portalOffset, (Main.MouseWorld - (player.Center + portalOffset)).SafeNormalize(-Vector2.UnitY) * player.HeldItem.shootSpeed, mod.ProjectileType("SunSphere"), (int)((player.HeldItem.damage * player.rangedDamage) * .5f), 2f, player.whoAmI);
                    }
                    else
                    {
                        Projectile.NewProjectile(player.Center + portalOffset, (Main.MouseWorld - (player.Center + portalOffset)).SafeNormalize(-Vector2.UnitY) * player.HeldItem.shootSpeed, mod.ProjectileType("DarkmatterSphere"), (int)((player.HeldItem.damage * player.rangedDamage) * .3f), 2f, player.whoAmI);
                    }
                    
                }
                shot = true;
            }
            else
            {
                shot = false;
            }
        }
        public static readonly PlayerLayer Portal = new PlayerLayer("AAMod", "Portal", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("AAMod");
            Texture2D texture = mod.GetTexture("Items/Armor/Darkmatter/DarkPortal");
            if(drawPlayer.GetModPlayer<VisorEffects>().sunPortal)
            {
                texture = mod.GetTexture("Items/Armor/Radium/SunPortal");
            }
            if (drawPlayer.GetModPlayer<VisorEffects>().setBonus)
            {
                Vector2 Center = drawInfo.position + new Vector2(drawPlayer.width / 2, drawPlayer.height / 2) + drawPlayer.GetModPlayer<VisorEffects>().portalOffset - Main.screenPosition;

                DrawData data = new DrawData(texture, Center, texture.Frame(1, drawPlayer.GetModPlayer<VisorEffects>().portalFrameCount, 0, drawPlayer.GetModPlayer<VisorEffects>().portalFrame), Color.White, 0f, new Vector2(texture.Size().X, texture.Size().Y / 4) * .5f, 1f, drawInfo.spriteEffects, 0);
                data.shader = drawInfo.bodyArmorShader;
                Main.playerDrawData.Add(data);
            }
        });
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {


            int frontLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("MiscEffectsFront"));
            if (frontLayer != -1)
            {
                Portal.visible = true;
                layers.Insert(frontLayer + 1, Portal);
            }
        }
    }
    
}