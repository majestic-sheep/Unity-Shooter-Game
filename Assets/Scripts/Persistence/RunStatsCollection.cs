using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RunStatsCollection
{
    public int Month;
    public int Day;
    public int Year;
    public int Hour;
    public int Minute;
    public int Second;
    public int Level;
    public int Score;
    public float RunTime;
    public DateTime DateTime
    {
        set
        {
            Month = value.Month;
            Day = value.Day;
            Year = value.Year;
            Hour = value.Hour;
            Minute = value.Minute;
            Second = value.Second;
        }
    }
    public string DateTimeString
    {
        get
        {
            return $"{Month}/{Day}/{Year} at {FormattedTime}";
        }
    }
    public string DateTimeShortString
    {
        get
        {
            if (Year != DateTime.Now.Year)
                return Year.ToString();
            else if (Month != DateTime.Now.Month || Day != DateTime.Now.Day)
                return $"{Month}/{Day}";
            else
            {
                return FormattedTime;
            }
        }
    }
    private string FormattedTime
    {
        get
        {
            bool isAm = true;
            int hour = Hour;
            if (Hour > 12)
            {
                hour -= 12;
                isAm = false;
            }
            if (hour == 12)
                isAm = !isAm;
            return $"{hour}:{Minute} {(isAm ? "AM" : "PM")}";
        }
    }
    public string RunTimeString
    {
        get
        {
            int seconds = (int)(RunTime % 60);
            int minutes = (int)((RunTime / 60) % 60);
            return $"{minutes}:{seconds}";
        }
    }
}
