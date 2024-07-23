using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[System.Serializable]
public class GameTimestamp // This class manages the in-game time.
{
    public int year;
    public enum Season //Each season will have 30 days. There will be 4 months in the form of seasons.
    {
        Spring,
        Summer,
        Fall,
        Winter
    }

    public enum DayOfWeek
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }

    public Season season;
    public int day;
    public int hour;
    public int minute;

    public GameTimestamp(int year, Season season, int day, int hour, int minute) // Constructor to set up the class.
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public void UpdateClock()
    {
        minute++;

        if (minute >= 60) // 60 minutes in 1 hour.
        {
            minute = 0; // Reset minutes.
            hour++;
        }

        if (hour >= 24) // 24 hourse in 1 day.
        {
            // Reset hours.
            hour = 0;

            day++;
        }
        
        // 30 days in a season.
        if (day > 30)
        {
            // Reset days
            day = 1;

            if (season == Season.Winter) // If at the final season, reset and change to spring. Making winter rotate to spring after.
            {
                season = Season.Spring;
                year++; // Start of a new year.
            }
            else
            {
                season++;
            }
        }
    }

    //To be easily converted from any context.---------------------------------------
    
    public DayOfWeek GetDayOfTheWeek()
    {
        int daysPassed = YearsToDays(year) + SeasonToDays(season) + day; // Converts the total time passed into days.

        // Remainder after dividing daysPassed by 7
        int dayIndex = daysPassed % 7;

        return (DayOfWeek)dayIndex; // Casts into Day of the Week enum.
    }

    public static int HourToMinutes(int hour) // Convert hours to minutes.
    {
        return hour * 60; //60 minutes = 1 hour.
    }

    public static int DaysToHours(int days) // Convert days to hours.
    {
        return days * 24; // 24 hourse in a day.
    }

    public static int SeasonToDays(Season season) // Convert Seasons to days.
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }

    public static int YearsToDays(int years) //Years to Day
    {
        return years * 4 * 30;
    }
}
//Source: https://www.youtube.com/watch?v=jNbOXAGJbZ4&list=PLgXA5L5ma2Bu1sWc_-ZGRuPSUEAKPktHF&index=8
