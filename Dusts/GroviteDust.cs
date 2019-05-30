using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Dusts
{
    public class GroviteDust : ModDust
    {

        public override bool Update(Dust dust)
        {
            dust.rotation += (float)(Math.PI / 16f) * dust.scale;
            dust.scale *= .8f;
            dust.velocity *= 0.98f;
            dust.position += dust.velocity;
            if (dust.scale <= 0.2f) { dust.active = false; }
            return false;
        }

        public override bool MidUpdate(Dust dust)
        {
            dust.rotation += dust.velocity.X / 3f;
            if (!dust.noLight)
            {
                float strength = dust.scale * 1.4f;
                if (strength > 1f)
                {
                    strength = 1f;
                }
                Lighting.AddLight(dust.position, (AAPlayer.groviteColor.R / 255) * 0.3f * strength, (AAPlayer.groviteColor.G / 255) * 0.3f * strength, (AAPlayer.groviteColor.B / 255) * 0.3f * strength);
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}