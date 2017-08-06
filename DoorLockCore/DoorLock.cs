
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

public class DoorLock
{
    private bool _IsLocked = false;
    private List<string> ValidCodes = new List<string>();
    private int _AttemptNumber = 1;
    private int _AttemptsAllowed = 0;
    private int _LockOutTime;
    public DoorLockLog Log = new DoorLockLog();
    public int AttemptNumber { get { return _AttemptNumber; } private set { } }
    public int AttemptsAllowed { get { return _AttemptsAllowed; } private set { } }
    public bool IsDisabled { get { return _AttemptsAllowed < _AttemptNumber; } }
    public int LockOutTime { get { return _LockOutTime; } private set { } }
    public bool IsLocked
    {
        get { return _IsLocked; }
    }



    public DoorLock()
    {
        string[] validCodes = ConfigurationManager.AppSettings.AllKeys.Where(key => key.StartsWith("Code"))
                             .Select(key => ConfigurationManager.AppSettings[key])
                             .ToArray();

        for (int i = 0; i < validCodes.Length; i++)
        {
            ValidCodes.Add(validCodes[i]);
        }
        int.TryParse(DoorLockCore.Properties.Settings.Default["CodeAttempts"].ToString(), out _AttemptsAllowed);
        //Allows the LockOutTime to be configured in a config file.
        int.TryParse(ConfigurationManager.AppSettings.AllKeys.Where(key => key.StartsWith("LockOutTime")).Select(key => ConfigurationManager.AppSettings[key]).ToArray()[0], out _LockOutTime);
        //int.TryParse(DoorLockCore.Properties.Settings.Default["LockOutTime"].ToString(), out _LockOutTime);

    }


    public virtual bool SubmitCode(string codeAttempt)
    {
        _AttemptNumber++;
        //Evaluate
        foreach (string code in ValidCodes)
        {
            if (codeAttempt == code)
            {
                ToggleState();
                _AttemptNumber = 1;
                Log.LogState(this, codeAttempt);
                int.TryParse(DoorLockCore.Properties.Settings.Default["LockOutTime"].ToString(), out _LockOutTime);
                return true;
            }
        }
        Log.LogState(this, codeAttempt);
        if (IsDisabled)
        {
            //If the lock is disabled, we need to know how many times a code has been entered after it was disabled
            //I could use a new int, but instead the difference of attempt number and attemtps allowed +1 will suffice
            //EG 30000 * ((3-3)+1) or 30000 * 1 on the first attempt that hits the limit
            //Second attempt it will be 30000 * ((4-3)+1) ir 30000*2
            LockOutTime = LockOutTime * ((AttemptNumber - AttemptsAllowed) + 1);
        }

        return false;
    }

    private void ToggleState()
    {
        _IsLocked = !_IsLocked;
    }


}

