﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DoorLock
{
    public virtual bool IsLocked { get; set; }
    private List<string> ValidCodes { get; set; }
    private int _AttemptNumber { get; set; }
    private int _AttemptsAllowed { get; set; }
    private int _LockOutTime;
    private bool _IsLocked;
    public virtual DoorLockLog Log { get; set; }
    public virtual int AttemptsAllowed { get; set; }
    public virtual int AttemptNumber { get; set; }
    public virtual bool IsDisabled { get; set; }
    public virtual object LockOutTime { get; set; }
    public virtual bool SubmitCode(string codeAttempt)
    {
        throw new System.NotImplementedException();
    }
    private void ToggleState()
    {
        throw new System.NotImplementedException();
    }

}

