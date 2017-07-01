using UnityEngine;
using System.Collections;
using System;

public class CalendarUtil {

	//任意の月の日数を求める
	public static int GetDaysInMonth(int year , int month){
		int daysInMonth = DateTime.DaysInMonth (year, month);
		return daysInMonth;
	}


	//任意の月の最初の日の曜日を求める
	public static int GetFirstDayOfWeek(string dateStr){
		DateTime target = DateTime.Parse (dateStr);
		DateTime firstDay = target.AddDays (-(target.Day - 1));
		int dayOfWeek = (int)firstDay.DayOfWeek;
		return dayOfWeek;
	}

}

