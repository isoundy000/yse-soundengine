﻿using System;
using System.Collections.Generic;
using System.Text;
using YSE;

namespace YSENET
{
  class PatcherEventHandler : Yse.guiHandler, IPatcherEventHandler
  {
    public override void ToGui(int objID, int value)
    {
      OnInt(objID, value);
    }

    public override void ToGui(int objID, float value)
    {
      OnFloat(objID, value);
    }

    public event OnPatcherIntEventHandler OnInt;
    public event OnPatcherFloatEventHandler OnFloat;
  }

  class Patcher : Yse.patcher, YSE.IPatcher
  {
    public void Connect(IHandle from, int pinOut, IHandle to, int pinIn)
    {
      base.Connect(((Handle)from).GetSource(), pinOut, ((Handle)to).GetSource(), pinIn);
    }

    public void Create(int mainOutputs)
    {
      create(mainOutputs);
    }

    public void Disconnect(IHandle from, int outlet, IHandle to, int inlet)
    {
      base.Disconnect(((Handle)from).GetSource(), outlet, ((Handle)to).GetSource(), inlet);
    }

    public void DeleteObject(IHandle handle)
    {
      base.DeleteObject(((Handle)handle).GetSource());
    }

    IHandle IPatcher.CreateObject(string type, string args)
    {
      Yse.pHandle handle = base.CreateObject(type, args);
      if (handle == null) return null;
      else return new Handle(handle);
    }

    public uint NumObjects()
    {
      return base.Objects();
    }

    IHandle IPatcher.GetHandleFromList(uint obj)
    {
      Yse.pHandle handle = base.GetHandleFromList(obj);
      if (handle == null) return null;
      else return new Handle(handle);
    }

    IHandle IPatcher.GetHandleFromID(uint obj)
    {
      Yse.pHandle handle = base.GetHandleFromID(obj);
      if (handle == null) return null;
      else return new Handle(handle);
    }

    public void Clear()
    {
      base.Clear();
    }

    private PatcherEventHandler PEH = null;
    public IPatcherEventHandler GetEventHandler()
    {
      if (PEH == null)
      {
        PEH = new PatcherEventHandler();
        base.SetGuiHandler(PEH);
      }
      return PEH;
    }
  }
}
