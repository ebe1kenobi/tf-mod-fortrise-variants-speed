using System;
using System.Diagnostics;
using FortRise;
using Microsoft.Extensions.Logging;
using Monocle;
using TowerFall;

namespace TFModFortRiseVariantSpeed
{
  public class TFModFortRiseVariantSpeedModule : Mod
  {
    public static TFModFortRiseVariantSpeedModule Instance;

    private static Type[] Registerables = [
        typeof(TextureRegistry),
        typeof(Variants)

     ];
    internal Type[] Hookables = [
        typeof(MyLevel),
    ];
    //public static Atlas SpeedAtlas;

    public TFModFortRiseVariantSpeedModule(IModContent content, IModuleContext context, ILogger logger) : base(content, context, logger)
    {
      if (!Debugger.IsAttached)
      {
        //Debugger.Launch(); // Proposera d’attacher Visual Studio
      }
      Instance = this;
      //Logger.Init("VariantSpeedLOG");

      foreach (var hookable in Hookables)
      {
        hookable.GetMethod(nameof(IHookable.Load))!.Invoke(null, [context.Harmony]);
      }

      foreach (var registerable in Registerables)
      {
        registerable.GetMethod(nameof(IRegisterable.Register))!.Invoke(null, [content, context.Registry]);
      }
    }

    //public override void LoadContent()
    //{
    //  SpeedAtlas = Content.LoadAtlas("Atlas/atlas.xml", "Atlas/atlas.png");
    //}

    //public override void Load()
    //{
    //  MyLevel.Load();
    //}

    //public override void Unload()
    //{
    //  MyLevel.Unload();
    //}

    //public override void OnVariantsRegister(VariantManager manager, bool noPerPlayer = false)
    //{
    //  var info1x1 = new CustomVariantInfo(
    //      "SpeedGamex1.1", VariantManager.GetVariantIconFromName("SpeedGamex1.1", SpeedAtlas),
    //      CustomVariantFlags.None
    //      );
    //  var info1x2 = new CustomVariantInfo(
    //      "SpeedGamex1.2", VariantManager.GetVariantIconFromName("SpeedGamex1.2", SpeedAtlas),
    //      CustomVariantFlags.None
    //      );
    //  var info1x3 = new CustomVariantInfo(
    //      "SpeedGamex1.3", VariantManager.GetVariantIconFromName("SpeedGamex1.3", SpeedAtlas),
    //      CustomVariantFlags.None
    //      );
    //  var info1x4 = new CustomVariantInfo(
    //      "SpeedGamex1.4", VariantManager.GetVariantIconFromName("SpeedGamex1.4", SpeedAtlas),
    //      CustomVariantFlags.None
    //      );
    //  var info1x5 = new CustomVariantInfo(
    //      "SpeedGamex1.5", VariantManager.GetVariantIconFromName("SpeedGamex1.5", SpeedAtlas),
    //      CustomVariantFlags.None
    //      );

    //  manager.AddVariant(info1x1);
    //  manager.AddVariant(info1x2);
    //  manager.AddVariant(info1x3);
    //  manager.AddVariant(info1x4);
    //  manager.AddVariant(info1x5);
    //}
  }
}
