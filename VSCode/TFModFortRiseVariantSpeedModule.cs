using System;
using System.IO;
using System.Reflection;
using System.Xml;
using FortRise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;
using MonoMod.ModInterop;
using MonoMod.Utils;
using TowerFall;

namespace TFModFortRiseVariantSpeed
{
  [Fort("com.ebe1.kenobi.tfmodfortrisevariantspeed", "TFModFortRiseVariantSpeed")]
  public class TFModFortRiseVariantSpeedModule : FortModule
  {
    public static TFModFortRiseVariantSpeedModule Instance;
    public static Atlas SpeedAtlas;

    public override Type SettingsType => typeof(TFModFortRiseVariantSpeedSettings);
    public TFModFortRiseVariantSpeedSettings Settings => (TFModFortRiseVariantSpeedSettings)Instance.InternalSettings;

    public TFModFortRiseVariantSpeedModule()
    {
      Instance = this;
      Logger.Init("VariantSpeedLOG");
    }

    public override void LoadContent()
    {
      SpeedAtlas = Content.LoadAtlas("Atlas/atlas.xml", "Atlas/atlas.png");

    }

    public override void Load()
    {
      MyLevel.Load();
    }

    public override void Unload()
    {
      MyLevel.Unload();
    }

    public override void OnVariantsRegister(VariantManager manager, bool noPerPlayer = false)
    {
      var info1x1 = new CustomVariantInfo(
          "SpeedGame1x1", VariantManager.GetVariantIconFromName("NoHeadBounce", SpeedAtlas),
          CustomVariantFlags.None
          );
      var info1x2 = new CustomVariantInfo(
          "SpeedGame1x2", VariantManager.GetVariantIconFromName("NoHeadBounce", SpeedAtlas),
          CustomVariantFlags.None
          );
      var info1x3 = new CustomVariantInfo(
          "SpeedGame1x3", VariantManager.GetVariantIconFromName("NoHeadBounce", SpeedAtlas),
          CustomVariantFlags.None
          );
      var info1x4 = new CustomVariantInfo(
          "SpeedGame1x4", VariantManager.GetVariantIconFromName("NoHeadBounce", SpeedAtlas),
          CustomVariantFlags.None
          );
      var info1x5 = new CustomVariantInfo(
          "SpeedGame1x5", VariantManager.GetVariantIconFromName("NoHeadBounce", SpeedAtlas),
          CustomVariantFlags.None
          );

      manager.AddVariant(info1x1);
      manager.AddVariant(info1x2);
      manager.AddVariant(info1x3);
      manager.AddVariant(info1x4);
      manager.AddVariant(info1x5);
    }
  }
}
