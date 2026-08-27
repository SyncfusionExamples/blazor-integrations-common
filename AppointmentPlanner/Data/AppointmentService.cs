using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using AppointmentPlanner.Models;

namespace AppointmentPlanner.Data
{
    public class AppointmentService
    {
        public AppointmentService()
        {
            this.Activities = new Activity().GetActivityData();
            this.StartDate = new DateTime(2026, 2, 5, 0, 0, 0, 0);
            this.ActiveDoctors = new Doctor().GetDoctorsData().FirstOrDefault();
            this.ActivePatients = new Patient().GetPatientsData().FirstOrDefault();
            this.StartHours = new TextValueData().GetStartHours();
            this.EndHours = new TextValueData().GetEndHours();
            this.Views = new TextValueData().GetViews();
            this.ColorCategory = new TextValueData().GetColorCategory();
            this.BloodGroups = new TextValueData().GetBloodGroupData();
            this.DayOfWeekList = new TextValueNumericData().GetDayOfWeekList();
            this.TimeSlot = new TextValueNumericData().GetTimeSlot();
            this.Hospitals = new Hospital().GetHospitalData();
            this.Patients = new Patient().GetPatientsData();
            this.Doctors = new Doctor().GetDoctorsData();
            this.WaitingLists = new WaitingList().GetWaitingList();
            this.Specializations = new Specialization().GetSpecializationData();
            this.DutyTimings = new TextIdData().DutyTimingsData();
            this.Experience = new TextIdData().ExperienceData();
            this.NavigationMenu = new NavigationMenu().GetNavigationMenuItems();
            this.CalendarSettings = new CalendarSetting { BookingColor = "Doctors", Calendar = new AppointmentPlanner.Models.Calendar { Start = "08:00", End = "21:00" }, CurrentView = "Week", Interval = 60, FirstDayOfWeek = 0 };
        }
        public DateTime StartDate { get; set; }
        public Doctor ActiveDoctors { get; set; }

        public Patient ActivePatients { get; set; }
        public List<TextValueData> StartHours { get; set; } 
        public List<TextValueData> EndHours { get; set; }
        public List<TextValueData> Views { get; set; }
        public List<TextValueData> ColorCategory { get; set; }
        public List<TextValueData> BloodGroups { get; set; }
        public List<TextValueNumericData> DayOfWeekList { get; set; }
        public List<TextValueNumericData> TimeSlot { get; set; }
        public List<Hospital> Hospitals { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<WaitingList> WaitingLists { get; set; }
        public List<Specialization> Specializations { get; set; }
        public List<TextIdData> DutyTimings { get; set; }
        public List<TextIdData> Experience { get; set; }
        public List<Activity> Activities { get; set; }
        public List<NavigationMenu> NavigationMenu { get; set; }
        public CalendarSetting CalendarSettings { get; set; }
        public bool ShowDeleteMsg { get; set; }

        public DateTime GetWeekFirstDate(DateTime date)
        { 
            return date.AddDays(DayOfWeek.Monday - date.DayOfWeek);
        }

        public string GetFormatDate(DateTime date, string type)
        {
            return date.ToString(type, CultureInfo.InvariantCulture);
        }

        public string TimeSince(DateTime activityTime)
        {
            if(Math.Round((DateTime.Now - activityTime).Days / (365.25 / 12)) > 0)
            {
                return Math.Round((DateTime.Now - activityTime).Days / (365.25 / 12)).ToString() + " months ago";
            } else if(Math.Round((DateTime.Now - activityTime).TotalDays) > 0)
            {
                return Math.Round((DateTime.Now - activityTime).TotalDays).ToString() + " days ago";
            } else if(Math.Round((DateTime.Now - activityTime).TotalHours) > 0)
            {
                return Math.Round((DateTime.Now - activityTime).TotalHours).ToString() + " hours ago";
            } else if (Math.Round((DateTime.Now - activityTime).TotalMinutes) > 0)
            {
                return Math.Round((DateTime.Now - activityTime).TotalMinutes).ToString() + " mins ago";
            } else if (Math.Round((DateTime.Now - activityTime).TotalSeconds) > 0)
            {
                return Math.Round((DateTime.Now - activityTime).TotalSeconds).ToString() + " seconds ago";
            }
            return Math.Round((DateTime.Now - activityTime).TotalMilliseconds).ToString() + " milliSeconds ago";
        }

        public Doctor GetDoctorDetails(int id)
        {
            return Doctors.Where(i => i.Id.Equals(id)).FirstOrDefault();
        }

        public string GetSpecializationText(string text)
        {
            return Specializations.Where(item => item.Id.Equals(text)).FirstOrDefault().Text;
        }
        public string GetAvailability(Doctor doctor)
        {
            var workDays = doctor.WorkDays;
            if (workDays != null)
            {
                var result = workDays.Where(item => item.Enable.Equals(true)).Select(item => item.Day.Substring(0, 3).ToUpper());
                return string.Join(",", result).ToString();
                
            }
            return string.Empty;
        }

        public List<Hospital> GetFilteredData(DateTime StartDate, DateTime EndDate)
        {
            return this.Hospitals.Where(hospital => (hospital.StartTime >= StartDate && hospital.EndTime <= EndDate)).ToList();
        }

        public ChartData GetChartData(List<Hospital> hospitals, DateTime startDate)
        {
            List<Hospital> chartData = new List<Hospital>();
            var eventCount = hospitals.Where(hospital => ResetTime(hospital.StartTime) == ResetTime(startDate)).Count();
            return new ChartData() { Date = startDate, EventCount = eventCount };
        }


        public DateTime ResetTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public List<ChartData> GetAllChartPoints(List<Hospital> hospitals, DateTime date)
        {
            List<ChartData> chartPoints = new List<ChartData>();
            for (int i = 0; i < 7; i++)
            {
                chartPoints.Add(GetChartData(hospitals, date));
                date = date.AddDays(1);
            }
            return chartPoints;
        }

        // ----- Dashboard helpers -----

        public List<DepartmentSeries> GetDepartmentSeries(List<Hospital> events, DateTime firstDay, List<Specialization> specializations)
        {
            var result = new List<DepartmentSeries>();
            foreach (var spec in specializations)
            {
                var deptEvents = events.Where(e => e.DepartmentId == spec.DepartmentId).ToList();
                var points = new List<ChartData>();
                for (int i = 0; i < 7; i++)
                {
                    var day = firstDay.AddDays(i);
                    var count = deptEvents.Count(e => ResetTime(e.StartTime) == ResetTime(day));
                    points.Add(new ChartData { Date = day, EventCount = count });
                }
                result.Add(new DepartmentSeries
                {
                    Name = spec.Text,
                    Color = spec.Color,
                    Data = points
                });
            }
            return result;
        }

        public List<SparklinePoint> GetSparkline(DateTime anchorDay, Func<DateTime, int> projector)
        {
            var result = new List<SparklinePoint>();
            for (int i = 6; i >= 0; i--)
            {
                var day = anchorDay.AddDays(-i);
                result.Add(new SparklinePoint { Date = day, Count = projector(day) });
            }
            return result;
        }

        public AppointmentStatus ResolveStatus(Hospital h, DateTime today)
        {
            var d = ResetTime(h.StartTime);
            var t = ResetTime(today);
            if (d < t) return AppointmentStatus.Completed;
            if (d == t)
            {
                if (h.EndTime < DateTime.Now) return AppointmentStatus.Completed;
                if (h.StartTime <= DateTime.Now && h.EndTime >= DateTime.Now) return AppointmentStatus.InProgress;
                return AppointmentStatus.Waiting;
            }
            return AppointmentStatus.Confirmed;
        }

        public DashboardKpi BuildKpi(string label, int todayValue, int yesterdayValue, string icon, string accent, List<int> sparkline)
        {
            return new DashboardKpi
            {
                Label = label,
                Value = todayValue,
                PreviousValue = yesterdayValue,
                Icon = icon,
                Accent = accent,
                Sparkline = sparkline ?? new List<int>()
            };
        }

        public List<AlertItem> BuildAlerts(List<Doctor> doctors, List<Hospital> todayEvents)
        {
            var alerts = new List<AlertItem>();
            var onLeave = doctors.Count(d => d.Availability == "away");
            if (onLeave > 0)
            {
                alerts.Add(new AlertItem
                {
                    Severity = "warning",
                    Message = $"{onLeave} doctor{(onLeave > 1 ? "s are" : " is")} currently away. Plan bookings accordingly."
                });
            }
            var pending = todayEvents.Count(e => ResetTime(e.StartTime) == ResetTime(StartDate) && e.StartTime > DateTime.Now);
            if (pending > 0)
            {
                alerts.Add(new AlertItem
                {
                    Severity = "info",
                    Message = $"{pending} appointment{(pending > 1 ? "s" : "")} pending confirmation today."
                });
            }
            if (alerts.Count == 0)
            {
                alerts.Add(new AlertItem
                {
                    Severity = "success",
                    Message = "All clear — no operational alerts for today."
                });
            }
            return alerts;
        }

        
    } 
}

    public class DepartmentSeries
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public List<ChartData> Data { get; set; } = new();
    }